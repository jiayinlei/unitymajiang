using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.guojin.core.io.message;
using com.guojin.mj.net.message.game;
using System.Reflection;
using UnityEngine.UI;

namespace MaJong
{
    class MaJongManage
    {
        private static MaJongManage instance = null;
        public static MaJongManage Instance()
        {
            if (instance == null)
            {
                instance = new MaJongManage();
            }
            return instance;

        }
        public MaJongType MaJong;
        /// <summary>
        /// 创建麻将类型
        /// </summary>
        /// <param name="MaJongClassName"></param>
        public void CreatMaJong(string MaJongClassName)
        {
            MaJong = (MaJongType)Assembly.GetExecutingAssembly().CreateInstance("MaJong." + MaJongClassName);
        }

    }
    abstract class MaJongType
    {
        public abstract void ReciveRoomInfoWait(Message message);
        public List<GameUserInfo> GetGameUserInfo;
        public abstract void ChongLian(Message message);
        public abstract int PeopleNum();
        public abstract void ReciveRoomInfo(Message message);
        public abstract string MaJongTitle();
        public abstract string MaJongContent();
    }
    class MaJongZhengZhou : MaJongType
    {
        public override void ReciveRoomInfoWait(Message message)
        {
            GameRoomInfo GJM = (GameRoomInfo)message;
            Method.GameRoomID = GJM.roomCheckId;
            Method.maJongJuNum = GJM.leftChapterNums.ToString();
            Method.PeopleNum = GJM.PlayerNum;
            PlayerData.SetRoomInfo(GJM.PlayerNum, GJM.ChapterMax, GJM.IsHuiEr, GJM.IsDaiZiPai, GJM.XuanPaoCount, GJM.IsFangPao, GJM.IsQiDuiFanBei, GJM.IsZhuangJiaDi, GJM.IsGangKaiFan, GJM.IsGangDaiPao);

            GetGameUserInfo = GJM.sceneUser;
        }
        public override void ChongLian(Message message)
        {
            GameRoomInfo GTI = (GameRoomInfo)message;
            PlayerData.SetRoomInfo(GTI.PlayerNum, GTI.ChapterMax, GTI.IsHuiEr, GTI.IsDaiZiPai, GTI.XuanPaoCount, GTI.IsFangPao, GTI.IsQiDuiFanBei, GTI.IsZhuangJiaDi, GTI.IsGangKaiFan, GTI.IsGangDaiPao);
            List<GameUserInfo> GUIOList = GTI.sceneUser;
            Method.showStartGameCollMetho(GTI.roomCheckId);
            if (GTI.chapter != null)
            {

                List<UserPlaceMsg> USERList = GTI.chapter.userPlace;
                Method.PeopleNum = USERList.Count;
                Method.zhaungweizhi = GTI.chapter.zhuangIndex;
                if (GTI.chapter.huiEr != null)
                {
                    Method.huier = GTI.chapter.huiEr[0];
                    GameObject temp = Method.Instantiate(Resources.Load<GameObject>("MjPrefab/Huner"));
                    temp.GetComponentInChildren<MaJongOutInfo>().IsHun(Method.huier, Method.MjNameSp[Method.huier]);
                    temp.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
                    temp.transform.localPosition = new Vector3(0, 59, 0);
                    temp.transform.localScale = Vector3.one;
                    Method.hungameobj = temp;
                }

                Method.InitDistance(GUIOList);

                for (int i = 0; i < GUIOList.Count; i++)
                {
                    if (GUIOList[i].userId == Method.userID)
                    {
                        //Index = i;
                        Method.Index = GUIOList[i].locationIndex;
                    }
                }
                Method.joinroonnum = GUIOList.Count;
                Method.SetPlayerPosition();
                Method.ShowRoomID(GTI.roomCheckId);
                Method.ScoreList.Clear();
                for (int i = 0; i < GUIOList.Count; i++)
                {
                    Method.StartShowHeadImage(GUIOList[i]);
                    Method.IntARR[i] = GUIOList[i].score;
                    Method.ScoreList.Add(GUIOList[i].score);
                }
                Method.setzhuangweizhi(GTI.chapter.zhuangIndex);
                GameObject temp2 = Method.Instantiate(Resources.Load<GameObject>("MjPrefab/Center_Img"));
                temp2.name = "Center_Img";
                Method.ob = temp2;
                temp2.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
                temp2.transform.SetSiblingIndex(0);
                temp2.transform.localScale = Vector3.one;
                temp2.transform.localPosition = new Vector3(0, 58, 0);
                Debug.Log(GTI.leftChapterNums);
                Method.ShengyuMaJong = GameObject.Find("MaJongNun").GetComponent<Text>();
                Method.ShowMaJongInfo(GTI.chapter.chapterNums, GTI.chapter.freeLength);
                MessageManger.shengyu = GTI.chapter.freeLength;
                Method.isRun = true;
                Method.isUpdate = true;
                Method.InitMaJong();//初始化牌局信息
                if (GTI.chapter.optFaPai != null)
                {
                    Method.ReceiveMaJong(GTI.chapter.optFaPai.pai);
                    Method.isMine = true;
                    Method.recivePaiId = GTI.chapter.optFaPai.pai;
                    //判断扛 胡
                    Method.BehaReciveMaJong(GTI.chapter.optFaPai);
                }
                for (int i = 0; i < USERList.Count; i++)
                {
                    Method.ShowUser(USERList[i], i, temp2.transform, GTI.State);
                }

                if (GTI.chapter.optCpgh != null)
                {
                    Method.PGHShow(GTI.chapter.optCpgh.index, GTI.chapter.optCpgh.pai, GTI.chapter.optCpgh.isPeng, false, GTI.chapter.optCpgh.isGang, false, GTI.chapter.optCpgh.isHu, true);
                }
                if (GTI.chapter.tingPai != null)
                {
                    List<int> ARR = Method.OutList(GTI.chapter.tingPai.pais);

                    if (ARR.Count != 0)
                    {
                        //听牌
                        if (Method.TingMaJong != null)
                        {
                            Method.Destroy(Method.TingMaJong);
                        }
                        VoiceManger.Instance.TingMaJong(ARR);
                    }
                    else
                    {
                        if (Method.TingMaJong != null)
                        {
                            Method.Destroy(Method.TingMaJong);
                        }
                    }
                }
                if (GTI.chapter.optOut != null)
                {
                    Method.isMine = true;
                    Method.isPeng = true;
                }
                if (GTI.chapter.syncOptTime != null)
                {
                    Method.isUpdate = true;
                    // 根据 SOT.index 显示位置信息
                    Method.SetPositionStar(GTI.chapter.syncOptTime.index);
                }
                if (!GTI.start && GTI.State == 0 && GTI.leftChapterNums != GTI.ChapterMax)
                {
                    Method.ShowStartGame();
                }
                Method.DesStartGameCollMetho();

                if (GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback)
                {
                    GameObject playbackBar = Method.Instantiate(Resources.Load<GameObject>("MjPrefab/PlaybackBar"));
                    playbackBar.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
                    playbackBar.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    playbackBar.transform.localPosition = new Vector3(500, -270, 0f);
                    GameObject.Find("dangweiT").GetComponent<Text>().text = "X " + 2;

                    playbackBar.transform.Find("returnBtn").GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        VoiceManger.Instance.BtnVoice();
                        GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().exitPlayback();
                    });
                    playbackBar.transform.Find("backwardBtn").GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        VoiceManger.Instance.BtnVoice();
                        GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().backwardPlayback();
                    });
                    playbackBar.transform.Find("forwardBtn").GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        VoiceManger.Instance.BtnVoice();
                        GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().forwardPlayback();
                    });
                    playbackBar.transform.Find("pauseBtn").GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        VoiceManger.Instance.BtnVoice();
                        GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().pauseOrResumePlayback();
                    });
                    GameObject.Find("Chat_Btn").SetActive(false);
                    GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().startTimer();
                }
            }
            else
            {
                ReciveRoomInfo(GTI);
            }
        }
        public override int PeopleNum()
        {
            throw new System.NotImplementedException();
        }
        public override void ReciveRoomInfo(Message message)
        {
            GameRoomInfo GTI = (GameRoomInfo)message;
            PlayerData.SetRoomInfo(GTI.PlayerNum, GTI.ChapterMax, GTI.IsHuiEr, GTI.IsDaiZiPai, GTI.XuanPaoCount, GTI.IsFangPao, GTI.IsQiDuiFanBei, GTI.IsZhuangJiaDi, GTI.IsGangKaiFan, GTI.IsGangDaiPao);
            Method.showStartGameCollMetho(GTI.roomCheckId);
            List<GameUserInfo> GUIOList = GTI.sceneUser;
            //todo 2017/8/1
            Method.InitDistance(GUIOList);

            for (int i = 0; i < GUIOList.Count; i++)
            {
                if (GUIOList[i].userId == Method.userID)
                {
                    Method.Index = i;
                }
            }
            Method.joinroonnum = GUIOList.Count;
            GameUserInfo GUIO = GUIOList[Method.Index];
            Method.PeopleNum = GTI.PlayerNum;
            Debug.Log(Method.PeopleNum);
            Method.SetPlayerPosition();
            Method.ShowRoomID(GTI.roomCheckId);
            Method.ScoreList.Clear();
            for (int i = 0; i < GTI.sceneUser.Count; i++)
            {
                Method.StartShowHeadImage(GTI.sceneUser[i]);
                Method.ScoreList.Add(GTI.sceneUser[i].score);
                Method.IntARR[i] = GTI.sceneUser[i].score;
            }
            if (GTI.State == 0)
            {
                if (GTI.leftChapterNums != GTI.ChapterMax)
                {
                    Method.ShowStartGame();
                }
                else if(!GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback)
                {
                    for (int i = 0; i < Method.touxiangL.Count; i++)
                    {
                        if (Method.Index != i)
                        {
                            Method.touxiangL[i].GetComponent<PlayerInfo>().showHold();
                        }
                    }
                }
            }           
        }
        public override string MaJongTitle()
        {
            string title1;
            title1 = "亲友局-郑州麻将 ：房号[ " + Method.GameRoomID + " ]";
            return title1;
        }
        public override string MaJongContent()
        {
          string content1 = PlayerData.people + "人" + PlayerData.jutype + "局," + PlayerData.huntype + "," + PlayerData.hupaitype + "," + PlayerData.fengtype + "," + PlayerData.xuanpaotype + "," + PlayerData.daipaotype + "," + PlayerData.zhuangjiatype + "," + PlayerData.gangkaitype + "," + PlayerData.qiduitype + ",赶快来!";
            return content1;
        }
    }
    class MaJongGuShi : MaJongType
    {
        public override void ReciveRoomInfoWait(Message message)
        {
            GameRoomInfoGuShi GJM = (GameRoomInfoGuShi)message;
            Method.GameRoomID = GJM.roomCheckId;
            Method.maJongJuNum = GJM.leftChapterNums.ToString();
            Method.PeopleNum = GJM.PlayerNum;
            PlayerData.SetRoomInfogushi(GJM.PlayerNum, GJM.ChapterMax, GJM.Isbudaifeng, GJM.IsFangPao, GJM.xuanpao, GJM.diFen, GJM.isPaoZui, GJM.is_que_men, GJM.zhuangjia);
            GetGameUserInfo = GJM.sceneUser;

        }
        public override void ChongLian(Message message)
        {
            com.guojin.mj.net.message.game.GameRoomInfoGuShi GTI = (com.guojin.mj.net.message.game.GameRoomInfoGuShi)message;
            PlayerData.SetRoomInfogushi(GTI.PlayerNum, GTI.ChapterMax, GTI.Isbudaifeng, GTI.IsFangPao, GTI.xuanpao, GTI.diFen, GTI.isPaoZui, GTI.is_que_men, GTI.zhuangjia);
            List<com.guojin.mj.net.message.game.GameUserInfo> GUIOList = GTI.sceneUser;
            Method.showStartGameCollMetho(GTI.roomCheckId);
            PlayerData.PaoZuiDictionary.Clear();
            if (GTI.chapter != null)
            {
                List<com.guojin.mj.net.message.game.UserPlaceMsg> USERList = GTI.chapter.userPlace;
                Method.PeopleNum = USERList.Count;
                Method.zhaungweizhi = GTI.chapter.zhuangIndex;
                Method.InitDistance(GUIOList);
                for (int i = 0; i < GUIOList.Count; i++)
                {
                    if (GUIOList[i].userId == Method.userID)
                    {
                        //Index = i;
                        Method.Index = GUIOList[i].locationIndex;
                    }
                }
                Method.joinroonnum = GUIOList.Count;
                Method.SetPlayerPosition();
                Method.ShowRoomID(GTI.roomCheckId);
                Method.ScoreList.Clear();
                for (int i = 0; i < GUIOList.Count; i++)
                {
                    Method.StartShowHeadImage(GUIOList[i]);
                    Method.IntARR[i] = GUIOList[i].score;
                    Method.ScoreList.Add(GUIOList[i].score);
                }

                Method.setzhuangweizhi(GTI.chapter.zhuangIndex);
                GameObject temp2 = Method.Instantiate(Resources.Load<GameObject>("MjPrefab/Center_Img"));
                temp2.name = "Center_Img";
                Method.ob = temp2;
                temp2.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
                temp2.transform.SetSiblingIndex(0);
                temp2.transform.localScale = Vector3.one;
                temp2.transform.localPosition = new Vector3(0, 58, 0);

                //Method.daojishi = GameObject.Find("CenterText").GetComponent<Text>();
                Debug.Log(GTI.leftChapterNums);
                Method.ShengyuMaJong = GameObject.Find("MaJongNun").GetComponent<Text>();
                Method.ShowMaJongInfo(GTI.chapter.chapterNums, GTI.chapter.freeLength);
                MessageManger.shengyu = GTI.chapter.freeLength;
                Method.isRun = true;
                Method.isUpdate = true;
                Method.InitMaJong();//初始化牌局信息

                if (GTI.is_que_men)
                {
                    if (GTI.State == 4)//判断是不是在定缺们中
                    {
                        if (Method.StateText == null)
                        {
                            GameObject temp1 = Method.Instantiate(Resources.Load<GameObject>("MjPrefab/ShowState"));
                            temp1.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
                            temp1.transform.localScale = Vector3.one;
                            temp1.transform.localPosition = Vector3.zero;
                            Method.StateText = temp1;
                            temp1.GetComponent<OpenAndClose>().ShowQueMen();
                        }
                        Method.setTrue();
                        for (int i = 0; i < USERList.Count; i++)
                        {
                            if (USERList[i].queIndex != -1)
                            {
                                if (i == Method.Index)
                                {
                                    Method.CloneDuanmen(Method.Index, Resources.Load<Sprite>("NewUIPicture/gushi/duan" + USERList[i].queIndex));
                                    Method.DuanMan = USERList[i].queIndex;
                                }
                                else
                                {
                                    Method.StateText.GetComponent<OpenAndClose>().ShowWeiZhiDingQueAlready(i);
                                    Method.setFalseOne(i);
                                }
                            }
                        }
                    }
                    if (GTI.State == 2)
                    {
                        for (int i = 0; i < USERList.Count; i++)
                        {
                            Method.CloneDuanmen(i, Resources.Load<Sprite>("NewUIPicture/gushi/duan" + USERList[i].queIndex));
                            if (i == Method.Index)
                            {
                                Method.DuanMan = USERList[i].queIndex;
                            }
                        }
                    }
                }

                if (GTI.isPaoZui)
                {
                    //PlayerData.PaoZuiDictionary.Clear();

                    for (int i = 0; i < GTI.chapter.userPlace.Count; i++)
                    {
                        PlayerData.PaoZuiDictionary.Add(i, GTI.chapter.userPlace[i].zuiIndex);//string.Join(",", (GTI.chapter.userPlace[i].zuiIndex).Select(j => j.ToString()).ToArray())
                        GameObject.Find("HeadPhoto" + i).GetComponent<PlayerInfo>().ShowZui();
                    }
                }
                if (GTI.chapter.optFaPai != null)
                {

                    Method.ReceiveMaJong(GTI.chapter.optFaPai.pai);
                    Method.isMine = true;
                    Method.recivePaiId = GTI.chapter.optFaPai.pai;
                    //判断扛 胡
                    Method.BehaReciveMaJong(GTI.chapter.optFaPai);
                }
                for (int i = 0; i < USERList.Count; i++)
                {
                    Method.ShowUser(USERList[i], i, temp2.transform, GTI.State);
                }



                if (GTI.chapter.optCpgh != null)
                {
                    Method.PGHShow(GTI.chapter.optCpgh.index, GTI.chapter.optCpgh.pai, GTI.chapter.optCpgh.isPeng, false, GTI.chapter.optCpgh.isGang, false, GTI.chapter.optCpgh.isHu, true);
                }
                if (GTI.chapter.tingPai != null)
                {
                    List<int> ARR = Method.OutList(GTI.chapter.tingPai.pais);
                    if (ARR.Count != 0)
                    {
                        //听牌
                        if (Method.TingMaJong != null)
                        {
                            Method.Destroy(Method.TingMaJong);
                        }
                        VoiceManger.Instance.TingMaJong(ARR);
                    }
                    else
                    {
                        if (Method.TingMaJong != null)
                        {
                            Method.Destroy(Method.TingMaJong);
                        }
                    }
                }
                if (GTI.chapter.optOut != null)
                {
                    Method.isMine = true;
                    Method.isPeng = true;
                }
                if (GTI.chapter.syncOptTime != null)
                {
                    Method.isUpdate = true;
                    // 根据 SOT.index 显示位置信息
                    Method.SetPositionStar(GTI.chapter.syncOptTime.index);
                }
                if (!GTI.start && GTI.State == 0 && GTI.leftChapterNums != GTI.ChapterMax)
                {
                    Method.ShowStartGame();
                }
                Method.DesStartGameCollMetho();

                if (GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback)
                {
                    GameObject playbackBar = Method.Instantiate(Resources.Load<GameObject>("MjPrefab/PlaybackBar"));
                    playbackBar.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
                    playbackBar.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    playbackBar.transform.localPosition = new Vector3(500, -270, 0f);
                    GameObject.Find("dangweiT").GetComponent<Text>().text = "X " + 2;
                    /*
                    Vector3[] corners = new Vector3[4];
                    GameObject.Find("ChoicePlayeWnd_Img").GetComponent<RectTransform>().GetWorldCorners(corners);

                    foreach (var item in corners)
                    {
                        Debug.Log(item);
                    }*/
                    //playbackBar.transform.localPosition = new Vector3(310, -200, 0);
                    playbackBar.transform.Find("returnBtn").GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        VoiceManger.Instance.BtnVoice();
                        GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().exitPlayback();
                    });
                    playbackBar.transform.Find("backwardBtn").GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        VoiceManger.Instance.BtnVoice();
                        GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().backwardPlayback();
                    });
                    playbackBar.transform.Find("forwardBtn").GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        VoiceManger.Instance.BtnVoice();
                        GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().forwardPlayback();
                    });
                    playbackBar.transform.Find("pauseBtn").GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        VoiceManger.Instance.BtnVoice();
                        GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().pauseOrResumePlayback();
                    });
                    GameObject.Find("Chat_Btn").SetActive(false);
                    GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().startTimer();
                }
            }
            else
            {
                ReciveRoomInfo(GTI);
            }
        }
        public override int PeopleNum()
        {
            throw new System.NotImplementedException();
        }
        public override void ReciveRoomInfo(Message message)
        {
            com.guojin.mj.net.message.game.GameRoomInfoGuShi GTI = (com.guojin.mj.net.message.game.GameRoomInfoGuShi)message;
            Method.showStartGameCollMetho(GTI.roomCheckId);
            List<com.guojin.mj.net.message.game.GameUserInfo> GUIOList = GTI.sceneUser;
            //todo 2017/8/1
            Method.InitDistance(GUIOList);

            for (int i = 0; i < GUIOList.Count; i++)
            {
                if (GUIOList[i].userId == Method.userID)
                {
                    Method.Index = i;
                }
            }
            Method.joinroonnum = GUIOList.Count;
            com.guojin.mj.net.message.game.GameUserInfo GUIO = GUIOList[Method.Index];
            Method.PeopleNum = GTI.PlayerNum;
            Debug.Log(Method.PeopleNum);
            Method.SetPlayerPosition();
            Method.ShowRoomID(GTI.roomCheckId);
            Method.ScoreList.Clear();
            for (int i = 0; i < GTI.sceneUser.Count; i++)
            {
                Method.StartShowHeadImage(GTI.sceneUser[i]);
                Method.ScoreList.Add(GTI.sceneUser[i].score);
                Method.IntARR[i] = GTI.sceneUser[i].score;
            }
            if (GTI.State == 0)
            {
                if (GTI.leftChapterNums != GTI.ChapterMax)
                {
                    Method.ShowStartGame();
                }
                else if (!GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback)
                {
                    for (int i = 0; i < Method.touxiangL.Count; i++)
                    {
                        if (Method.Index != i)
                        {
                            Method.touxiangL[i].GetComponent<PlayerInfo>().showHold();
                        }
                    }
                }
            }
            if (GTI.State == 1)
            {
                if (GTI.isPaoZui)
                {
                    for (int i = 0; i < GTI.xuanzui.Length; i++)
                    {
                        PlayerData.PaoZuiDictionary.Add(i, GTI.xuanzui[i]);
                        GameObject.Find("HeadPhoto" + i).GetComponent<PlayerInfo>().ShowZui();
                    }
                }
            }
            if (GTI.State == 3)
            {
                if (Method.StateText == null)
                {
                    GameObject temp1 = Method.Instantiate(Resources.Load<GameObject>("MjPrefab/ShowState"));
                    temp1.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
                    temp1.transform.localScale = Vector3.one;
                    temp1.transform.localPosition = Vector3.zero;
                    Method.StateText = temp1;
                    temp1.GetComponent<OpenAndClose>().InitState();
                }
                for (int i = 0; i < GTI.xuanzui.Length; i++)
                {
                    if (Method.Index != i && GTI.xuanzui[i] != null)
                    {
                        Method.StateText.GetComponent<OpenAndClose>().ShowWeiZhiDingZuiAlready(i);
                        GameObject.Find("HeadPhoto" + i).GetComponent<PlayerInfo>().ShowZui();
                    }
                }
            }
        }
        public override string MaJongTitle()
        {
            string title1;
            title1 = "亲友局-固始麻将 ：房号[ " + Method.GameRoomID + " ]";
            return title1;
        }
        public override string MaJongContent()
        {
            string content1 = PlayerData.people + "人" + PlayerData.jutype + "局," + PlayerData.hupaitype + "," + PlayerData.difen + "," + PlayerData.xuanpaotype + "," + PlayerData.duanmenType + "," + PlayerData.paozuiType + "," + PlayerData.fengtype + "," + PlayerData.zhuangjiatype + " ,赶快来!";
            return content1;
        }
    }

}
