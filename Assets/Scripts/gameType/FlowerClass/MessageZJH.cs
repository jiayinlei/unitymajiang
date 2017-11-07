using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using com.guojin.mj.net.message;
using System;
using com.guojin.mj.net.message.flower;
using com.guojin.mj.net.message.game;
using com.guojin.mj.net.message.login;
using UnityEngine.UI;

/// <summary>
/// 处理接收到的服务器消息
/// </summary>
public class MessageZJH : Observer{
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        //GameData.GetInstance().initEvent();// 优先注册GameData的事件
        initMsg();
    }
    protected override string[] GetMsgList()
    {
        return new string[] {
            MessageFactoryImpi.instance.getMessageString(CreateRoomRetZJH.TYPE,CreateRoomRetZJH.ID),//创建房间返回[3][1]
            MessageFactoryImpi.instance.getMessageString(JoinRoomInfoRetZJH.TYPE,JoinRoomInfoRetZJH.ID),// 炸金花加入房间返回[3][4]
            MessageFactoryImpi.instance.getMessageString(GameRoomInfoZJH.TYPE,GameRoomInfoZJH.ID),//炸金花同步房间信息,包含用户信息[3][6]
            MessageFactoryImpi.instance.getMessageString(FaPaiInfoRetZJH.TYPE,FaPaiInfoRetZJH.ID),//发牌信息[3][8]
            MessageFactoryImpi.instance.getMessageString(KouDiFenInfoRetZJH.TYPE,KouDiFenInfoRetZJH.ID),//扣除玩家低分信息[3][9]
            MessageFactoryImpi.instance.getMessageString(LookCardInfoRetZJH.TYPE,LookCardInfoRetZJH.ID),//玩家看牌信息返回[3][11]
            MessageFactoryImpi.instance.getMessageString(FollowMoneyInfoRetZJH.TYPE,FollowMoneyInfoRetZJH.ID),//玩家跟注信息返回[3][13]
            MessageFactoryImpi.instance.getMessageString(KouFenInfoRetZJH.TYPE,KouFenInfoRetZJH.ID),//玩家扣分信息返回[3][14]
            MessageFactoryImpi.instance.getMessageString(CompareCardInfoRetZJH.TYPE,CompareCardInfoRetZJH.ID),//玩家比牌结果返回[3][16]
            MessageFactoryImpi.instance.getMessageString(PresentGameOverInfoRetZJH.TYPE,PresentGameOverInfoRetZJH.ID),//本局结束结果返回[3][18]
            MessageFactoryImpi.instance.getMessageString(DissolveRoomInfoRetZJH.TYPE,DissolveRoomInfoRetZJH.ID),//解散房间返回[3][25]
            MessageFactoryImpi.instance.getMessageString(GiveUpCardInfoRetZJH.TYPE,GiveUpCardInfoRetZJH.ID),//炸金花弃牌信息返回[3][31]
            MessageFactoryImpi.instance.getMessageString(GameUserInfoZJH.TYPE,GameUserInfoZJH.ID),//炸金花同步玩家信息[3][32]
            MessageFactoryImpi.instance.getMessageString(PlayerHandleRetZJH.TYPE,PlayerHandleRetZJH.ID),//同步玩家操作[3][33]
            MessageFactoryImpi.instance.getMessageString(StartGameZJH.TYPE,StartGameZJH.ID),// //炸金花加入房间成功，可以开始游戏，返回[3][34]
        };
    }
    public override void OnMsg(string msg, com.guojin.core.io.message.Message data)
    {
        PlayerJoinRoom(msg, data);
        ShowFaPai(msg, data);
        KouDiFen(msg, data);
        PlayerHandle(msg, data);
        KanPai(msg, data);
        GenZhuInfo(msg, data);
        OneGameOver(msg, data);
        CompareCardRet(msg, data);
        GiveUpCard(msg, data);
        DeductScore(msg, data);
        DissolveRoom(msg, data);
        CreateRoomZJH(msg, data);
        JoinRoomZJH(msg, data);
        JoinSuccessZJH(msg, data);
        GetRoomInfoZJH(msg, data);
        OutRoom(msg, data);
    }
    /// <summary>
    /// 收到创建房间返回消息的处理
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="message"></param>
    void CreateRoomZJH(string msg, com.guojin.core.io.message.Message message)
    {
        if(msg == MessageFactoryImpi.instance.getMessageString(CreateRoomRetZJH.TYPE, CreateRoomRetZJH.ID))
        {
            // 炸金花创建房间返回
            com.guojin.mj.net.message.flower.CreateRoomRetZJH Jiz = (com.guojin.mj.net.message.flower.CreateRoomRetZJH)message;
            if (Jiz.result)
            {
                //发送加入房间消息
                com.guojin.mj.net.message.flower.JoinRoomInfoZJH ji = com.guojin.mj.net.message.flower.JoinRoomInfoZJH.create(Jiz.roomCheckId);
                SocketMgr.GetInstance().webSocket.Send(com.guojin.mj.net.Net.instance.write(ji));
            }
            else
            {
                Debug.Log("创建房间失败");
            }
        }

    }
    /// <summary>
    /// 收到加入房间信息的处理
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="message"></param>
    void JoinRoomZJH(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(JoinRoomInfoRetZJH.TYPE, JoinRoomInfoRetZJH.ID))
        {// 炸金花加入房间返回
            com.guojin.mj.net.message.flower.JoinRoomInfoRetZJH Jiz = (com.guojin.mj.net.message.flower.JoinRoomInfoRetZJH)message;
            if (Jiz.result)
            {
                Debug.Log("加入房间成功");
                ////向服务器发送玩家准备就绪消息
                //com.guojin.mj.net.message.flower.ReadyRetZJH rrz = new com.guojin.mj.net.message.flower.ReadyRetZJH();
                //SocketMgr.GetInstance().webSocket.Send(com.guojin.mj.net.Net.instance.write(rrz));
            }
            else
            {
                //弹出进入房间失败面板
                Debug.Log("加入房间失败");
            }
        }
    }
    /// <summary>
    /// 收到加入房间成功，可以开始游戏信息的处理
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="message"></param>
    void JoinSuccessZJH(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(StartGameZJH.TYPE, StartGameZJH.ID))//[3][34]
        {
            //向服务器发送玩家准备就绪消息
            com.guojin.mj.net.message.flower.ReadyZJH rrz = new com.guojin.mj.net.message.flower.ReadyZJH();
            SocketMgr.GetInstance().webSocket.Send(com.guojin.mj.net.Net.instance.write(rrz));
        }
    }
    /// <summary>
    /// 收到同步房间信息的处理
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="message"></param>
    void GetRoomInfoZJH(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(GameRoomInfoZJH.TYPE, GameRoomInfoZJH.ID))
        {// 炸金花同步房间信息
            Debug.Log("收到【3】【6】");
            MainSceneZJH.GRI = (com.guojin.mj.net.message.flower.GameRoomInfoZJH)message;
            SceneManager.LoadScene("PokerFlowerGame");
        }
    }
    /// <summary>
    /// 玩家加入房间（收到同步玩家信息）后的处理
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="data"></param>
    void PlayerJoinRoom(string msg, com.guojin.core.io.message.Message message)
    {
        if(msg == MessageFactoryImpi.instance.getMessageString(GameUserInfoZJH.TYPE, GameUserInfoZJH.ID))
        {
            Debug.Log("收到【3】【22】");
            com.guojin.mj.net.message.flower.GameUserInfoZJH GUIO = (com.guojin.mj.net.message.flower.GameUserInfoZJH)message;
            MainSceneZJH.StartShowHeadImage(GUIO);
            if (MainSceneZJH.allPeopleNum == 4)
            {
                if (GUIO.locationIndex == 3)
                {
                    MainSceneZJH.peopleNum = 4;
                    //发送开始游戏消息
                    Destroy(MainSceneZJH.InviteFriends);
                    MainSceneZJH.SendMessageStartGame();
                }
            }
        }
    }
    /// <summary>
    /// 收到解散房间返回后的处理
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="message"></param>
    void DissolveRoom(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(DissolveRoomInfoRetZJH.TYPE, DissolveRoomInfoRetZJH.ID))
        {
            Debug.Log("收到【3】【25】");
            com.guojin.mj.net.message.flower.DissolveRoomInfoRetZJH drir = (com.guojin.mj.net.message.flower.DissolveRoomInfoRetZJH)message;
            if(MainSceneZJH.intArr[drir.UserIndex] != 0)
            {
                //弹出某某玩家申请解散房间界面
                GameObject temp = TooL.clone(Resources.Load<GameObject>("ZJHPrefab/DissolutionRoomZJH"), GameObject.Find("Canvas"));
                Button[] btnArr = temp.GetComponentsInChildren<Button>();
                btnArr[0].onClick.AddListener(delegate
                {
                    com.guojin.mj.net.message.flower.VoteInfoZJH VDS = com.guojin.mj.net.message.flower.VoteInfoZJH.Create(true, MainSceneZJH.Index);
                    SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(VDS));
                });
                btnArr[1].onClick.AddListener(delegate
                {
                    com.guojin.mj.net.message.flower.VoteInfoZJH VDSR = com.guojin.mj.net.message.flower.VoteInfoZJH.Create(false, MainSceneZJH.Index);
                    SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(VDSR));
                    Destroy(temp);
                });
                btnArr[2].onClick.AddListener(delegate {
                    Destroy(temp);
                });
            }
        }

    }
    /// <summary>
    /// 收到退出房间消息的处理
    /// </summary>
    void OutRoom(string msg, com.guojin.core.io.message.Message message)
    {
       
    }
    /// <summary>
    /// 收到扣分信息后的处理
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="message"></param>
    void DeductScore(string msg, com.guojin.core.io.message.Message message)
    {
        if(msg == MessageFactoryImpi.instance.getMessageString(KouFenInfoRetZJH.TYPE, KouFenInfoRetZJH.ID))
        {
            com.guojin.mj.net.message.flower.KouFenInfoRetZJH kfir = (com.guojin.mj.net.message.flower.KouFenInfoRetZJH)message;
            GameObject.Find("HeadPhotoZJH" + kfir.Index).GetComponent<PlayerInfoZJH>().ChangedScore(-kfir.ZhuNum);
            RoomSetInfoZJH.Instance.GetAllMoney(kfir.ZhuNum);
            //扔注动画

        }
    }
    /// <summary>
    /// 收到弃牌信息后的处理
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="message"></param>
    void GiveUpCard(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(GiveUpCardInfoRetZJH.TYPE, GiveUpCardInfoRetZJH.ID))
        {
            com.guojin.mj.net.message.flower.GiveUpCardInfoRetZJH GUIO = (com.guojin.mj.net.message.flower.GiveUpCardInfoRetZJH)message;
            MainSceneZJH.peopleNum--;
            if (MainSceneZJH.intArr[GUIO.Index] == 0)
            {
                MainSceneZJH.isQiPai = true;
                Debug.Log("玩家" + GUIO.Index + "弃牌");
            }
            //克隆弃牌图标
            PlayGameZJH.Instance.ShowQi(GUIO.Index);
        }
    }
    //根据Index获取Score
    int GetScore(int index)
    {
        return GameObject.Find("HeadPhotoZJH" + index).GetComponent<PlayerInfoZJH>().score;
    }
    /// <summary>
    /// 收到比牌信息后的处理
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="message"></param>
    void CompareCardRet(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(CompareCardInfoRetZJH.TYPE, CompareCardInfoRetZJH.ID))
        {
            Debug.Log("收到【3】【16】");
            com.guojin.mj.net.message.flower.CompareCardInfoRetZJH ccir = (com.guojin.mj.net.message.flower.CompareCardInfoRetZJH)message;
            MainSceneZJH.isBiPai = false;
            MainSceneZJH.peopleNum--;
            if (MainSceneZJH.intArr[ccir.DefeatedIndex] == 0)
            {
                MainSceneZJH.isQiPai = true;
            }
            //克隆胜图标
            //克隆负图标
        }
    }
    /// <summary>
    /// 收到发牌信息后的处理
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="message"></param>
    void ShowFaPai(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(FaPaiInfoRetZJH.TYPE, FaPaiInfoRetZJH.ID))
        {
            Debug.Log("收到【3】【8】");
            com.guojin.mj.net.message.flower.FaPaiInfoRetZJH fpif = (com.guojin.mj.net.message.flower.FaPaiInfoRetZJH)message;
            PlayGameZJH.Instance.DestoryAllChildren();
            MainSceneZJH.LunNum = 1;
            MainSceneZJH.peopleNum = fpif.PlayerNum;
            MainSceneZJH.isStart = true;
            MainSceneZJH.chaperNum++;
            RoomSetInfoZJH.Instance.GetChapter(MainSceneZJH.chaperNum, MainSceneZJH.chaper);
            RoomSetInfoZJH.allMoney = 0;
            GameObject bt = GameObject.Find("GameStart");//寻找开始游戏按钮如果有，删除
            if (bt != null)
                Destroy(bt);
            
            for (int i = 0; i < fpif.Index.Length; i++)
            {
                if(MainSceneZJH.intArr[fpif.Index[i]]==0)
                {
                    MainSceneZJH.isKanPai = false;
                    MainSceneZJH.isQiPai = false;
                }
                //发牌动画
                GameObject obj = Instantiate(Resources.Load<GameObject>("ZJHPrefab/GrantCard" + fpif.Index[i]));
                obj.transform.SetParent(GameObject.Find("Canvas").transform);
                obj.transform.localPosition = new Vector3(0, 30, 0);
            }
            PlayGameZJH.Instance.ShowZhuang(fpif.ZhuangIndex);
           
        }
    }
    /// <summary>
    /// 收到扣除底分信息后的处理
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="message"></param>
    void KouDiFen(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(KouDiFenInfoRetZJH.TYPE, KouDiFenInfoRetZJH.ID))
        {
            Debug.Log("收到【3】【9】");
            com.guojin.mj.net.message.flower.KouDiFenInfoRetZJH kdfif = (com.guojin.mj.net.message.flower.KouDiFenInfoRetZJH)message;
            //for (int i = 0; i < kdfif.Index.Length; i++)
            //{
            //    GameObject.Find("HeadPhotoZJH" + kdfif.Index[i]).GetComponent<PlayerInfoZJH>().ChangedScore(-kdfif.DiFen);
            //    RoomSetInfoZJH.Instance.GetAllMoney(kdfif.DiFen);
            //}
            GameObject.Find("HeadPhotoZJH" + kdfif.Index[0]).GetComponent<PlayerInfoZJH>().ChangedScore(-kdfif.DiFen);
            RoomSetInfoZJH.Instance.GetAllMoney(kdfif.DiFen);
        }
    }
    /// <summary>
    /// 判断该不该自己操作
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="message"></param>
    void PlayerHandle(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(3, 33))
        {
            Debug.Log("收到【3】【33】");
            MainSceneZJH.timeNum = 20;
            com.guojin.mj.net.message.flower.PlayerHandleRetZJH kdfif = (com.guojin.mj.net.message.flower.PlayerHandleRetZJH)message;
            MainSceneZJH.addZhu = kdfif.Zhu;
            if(MainSceneZJH.intArr[kdfif.Index] == 0)
            {
                if(MainSceneZJH.LunNum < 2)
                {
                    Button btn = PlayGameZJH.Instance.Bi.GetComponent<Button>();
                    btn.interactable = false;
                }
                else
                {
                    Button btn = PlayGameZJH.Instance.Bi.GetComponent<Button>();
                    btn.interactable = true;
                }
                if (MainSceneZJH.LunNum <= 10)
                {
                    MainSceneZJH.isMine = true;
                    MainSceneZJH.LunNum++;
                }
                if(!MainSceneZJH.isKanPai)
                {
                    Button kanBtn = GameObject.Find("Kan").GetComponent<Button>();
                    kanBtn.interactable = true;
                }
            }
            else
            {
                MainSceneZJH.isMine = false;
            }
        }
    }
    /// <summary>
    /// 玩家看牌信息返回
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="message"></param>
    void KanPai(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(3, 11))
        {
            Debug.Log("收到【3】【11】");
            com.guojin.mj.net.message.flower.LookCardInfoRetZJH lcif = (com.guojin.mj.net.message.flower.LookCardInfoRetZJH)message;
            Button kanBtn = GameObject.Find("Kan").GetComponent<Button>();
            kanBtn.interactable = false;
            Array.Sort(lcif.ShouPai);
            PlayGameZJH.Instance.ShowShoupai(lcif.Index, lcif.ShouPai);
        }
    }
    /// <summary>
    /// 玩家跟注信息返回后的处理
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="message"></param>
    void GenZhuInfo(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(3, 13))
        {
            Debug.Log("收到【3】【13】");
            com.guojin.mj.net.message.flower.FollowMoneyInfoRetZJH fmio = (com.guojin.mj.net.message.flower.FollowMoneyInfoRetZJH)message;
            //GameObject.Find("HeadPhotoZJH" + fmio.Index).GetComponent<PlayerInfoZJH>().ChangedScore(-fmio.ZhuNum);
        }

    }
    /// <summary>
    /// 收到本局结束的信息的处理
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="message"></param>
    void OneGameOver(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(3, 18))
        {
            Debug.Log("收到【3】【18】");
            com.guojin.mj.net.message.flower.PresentGameOverInfoRetZJH pgoi = (com.guojin.mj.net.message.flower.PresentGameOverInfoRetZJH)message;
            MainSceneZJH.isStart = false;
            GameObject.Find("HeadPhotoZJH" + pgoi.WinIndex).GetComponent<PlayerInfoZJH>().ChangedScore(pgoi.WinScore);
            //克隆开始下一局的按钮,或者计时开始下一局
        }
    }
	void Update () {
     
	
	}
}
