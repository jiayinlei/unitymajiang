using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using com.guojin.mj.net.message;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MessageManger : Observer
{
    private Transform m_gameStartParent;//gameStart按钮的父对象
    private GameObject m_gameOverParent;//结算界面Parent;
    void Start()
    {
        initMsg();
        m_gameStartParent = GameObject.Find("ChoicePlayeWnd_Img").transform;
        m_gameOverParent = GameObject.Find("GameOverParent");
    }
    protected override string[] GetMsgList()
    {
        return new string[] {         
            MessageFactoryImpi.instance.getMessageString(1,34),// JoinRoomReady 创建房间返回
            MessageFactoryImpi.instance.getMessageString(1,19),// UserOffline 创建房间返回
            MessageFactoryImpi.instance.getMessageString(7, 7),// 创建房间返回
            MessageFactoryImpi.instance.getMessageString(7, 5),// 退出房间返回
            MessageFactoryImpi.instance.getMessageString(1, 4),// 退出房间GameExitUser
            MessageFactoryImpi.instance.getMessageString(7,11),//Notice   
            MessageFactoryImpi.instance.getMessageString(1,8),//GameUserInfo
            MessageFactoryImpi.instance.getMessageString(1,3),//GameDelRoom[1,3]
            MessageFactoryImpi.instance.getMessageString(1,30),//ShowPaoRet
            MessageFactoryImpi.instance.getMessageString(1,29),//OperationDingPaoRet
            MessageFactoryImpi.instance.getMessageString(1,9),//服务器发牌接口【1，9】MajiangChapterMsg
            MessageFactoryImpi.instance.getMessageString(1,12),//接到牌后的显示操作【1，12】OperationFaPai
            MessageFactoryImpi.instance.getMessageString(1,16),//同步发牌信息[1,16]SyncOpt
            MessageFactoryImpi.instance.getMessageString(1,17),//同步时间信息[1,17]SyncOptTime
            MessageFactoryImpi.instance.getMessageString(1,18),//收到是否听牌的消息之后进行的操作[1,18]TingPai
            MessageFactoryImpi.instance.getMessageString(1,10),//玩家出完牌后其他玩家接收到吃碰扛胡的信息【1，10】OperationCPGH
            MessageFactoryImpi.instance.getMessageString(1,31),//接收到所有的定跑信息
            MessageFactoryImpi.instance.getMessageString(1,14), //通知用户出牌的消息【1，14】OperationOut
            MessageFactoryImpi.instance.getMessageString(1,0), //牌局结束【1，0】
            MessageFactoryImpi.instance.getMessageString(1,21),//VoteDelSelect[1.21] 解散房间之后收到的消息
            MessageFactoryImpi.instance.getMessageString(1,26),//StaticsResultRet[1.26] 4局以后的总结算界面
            MessageFactoryImpi.instance.getMessageString(1,38),//Busying[1.38] 忙碌状态
            MessageFactoryImpi.instance.getMessageString(1,27),//Busying[1.38] 忙碌状态
            MessageFactoryImpi.instance.getMessageString(1,43),//Busying[1.43] 选嘴状态
            MessageFactoryImpi.instance.getMessageString(1,45),//
            MessageFactoryImpi.instance.getMessageString(1,46),//接收到所有人选嘴的消息
            MessageFactoryImpi.instance.getMessageString(1,40),//接收到所有人都定缺的消息
            MessageFactoryImpi.instance.getMessageString(1,39),//接收到显示定缺的消息
            MessageFactoryImpi.instance.getMessageString(1,42),
             MessageFactoryImpi.instance.getMessageString(1,48),
            //MessageFactoryImpi.instance.getMessageString(1,10),
        };
    }
    public override void OnMsg(string msg, com.guojin.core.io.message.Message data)
    {
        ReciveSeverError(msg, data);
        ReciveNotice(msg, data);
        ReciveOtherOffLine(msg, data);
        //Reciveroomret(msg, data);
        ExitRoomGet(msg, data);
        ExitRoom(msg, data);
        //GameRoominfo(msg, data);
        JoinUserInfo(msg, data);
        ShowPaoRetOrNot(msg, data);
        ReciveAllDingPao(msg, data);
        ShowDingPaoPosition(msg, data);
        ShowAllFaPaiInfo(msg, data);
        ShowFaPai(msg, data);
        ShowFaPaiOther(msg, data);
        ShowTime(msg, data);
        OutPlayerIsTingPai(msg, data);
        ReciveCPGHInfo(msg, data);
        NoticePlayerOutPai(msg, data);
        GameChapterEnd(msg, data);
        DissolutionRoom(msg, data);
        DeleRoom(msg, data);
        StaticsResultRet(msg, data);
        ReceiveChatInfo(msg, data);
        ReciveReadyMessage(msg, data);
        ReciveBusying(msg, data);
        ReciveShowSelectZuiTypeRet(msg, data);
        ReciveOtherSelectZuiTypeRet(msg, data);
        ReciveAllDingZuiZi(msg, data);
        ReciveAllQuePaiTypedRet(msg, data);
        ReciveShowSelectQueRet(msg, data);
        ReciveSelectQuePaiTypedRet(msg, data);
        ReciveRecordDetailsRet(msg, data);
    }

    int num1 = 0;

    void ReciveSeverError(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 27))
        {
            com.guojin.mj.net.message.game.ServerError SE = new com.guojin.mj.net.message.game.ServerError();
            SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(SE));
            Method.IsSeverError = true;
        }
    }
    void ReciveReadyMessage(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 34))
        {
            num1++;
            com.guojin.mj.net.message.game.JoinRoomReady jr = (com.guojin.mj.net.message.game.JoinRoomReady)message;
            Debug.Log("来了一次" + num1);
            for (int i = 0; i < jr.userList.Count; i++)
            {
                if (jr.userList[i] != Method.Index)
                {
                    Method.touxiangL[jr.userList[i]].GetComponent<PlayerInfo>().changeState();
                }
                else
                {
                    Method.isReciveReady = true;
                }

            }
            if (jr.num == Method.PeopleNum && Method.Index == 0)
            {
                //ReciveReady();//去掉Method.Index == 0和下面的东西
                com.guojin.mj.net.message.game.GameChapterStart cr = new com.guojin.mj.net.message.game.GameChapterStart();
                SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(cr));
            }
            if (jr.num == Method.PeopleNum)
            {
                for (int i = 0; i < Method.touxiangL.Count; i++)
                {
                    Method.touxiangL[jr.userList[i]].GetComponent<PlayerInfo>().setFalseHold();
                }
            }
        }
    }

    void ReciveBusying(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 38))
        {
            com.guojin.mj.net.message.game.Busying busy = (com.guojin.mj.net.message.game.Busying)message;
            if (busy.isbusy)
            {
                showBusy(busy.index);
            }
            else
            {
                GameObject.Find("HeadPhoto" + busy.index).GetComponent<PlayerInfo>().HiddenOffLine();
            }

        }
    }


    void showBusy(int weizhi)
    {
        GameObject.Find("HeadPhoto" + weizhi).GetComponent<PlayerInfo>().showBusyType();

    }   
    void CloneShaizi()
    {
        GameObject temp = GameObject.Instantiate(Resources.Load<GameObject>("MjPrefab/shaizi"));
        temp.transform.SetParent(GameObject.Find("Camera").transform);
        temp.transform.localScale = new Vector3(2, 2, 2);
        temp.transform.localPosition = new Vector3(-1, 0.8f, 1);
        StartCoroutine(desshaizi(temp));

    }
    IEnumerator desshaizi(GameObject temp)
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(temp);
        if (Method.Index == 0)
        {
            com.guojin.mj.net.message.game.GameChapterStart cr = new com.guojin.mj.net.message.game.GameChapterStart();
            SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(cr));
        }
    }  
    void ReciveOtherOffLine(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 19))
        {          
            com.guojin.mj.net.message.game.UserOffline UO = (com.guojin.mj.net.message.game.UserOffline)message;
            GameObject.Find("HeadPhoto" + UO.index).GetComponent<PlayerInfo>().ShowOffLine();

        }
    }



    //out and in
    void ReciveNotice(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(7, 11))
        {
            com.guojin.mj.net.message.login.Notice Notice = (com.guojin.mj.net.message.login.Notice)message;         
            if (Notice.type == -1)
            {
                GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/NoticeTip"));
                temp.transform.SetParent(gameObject.transform);
                temp.transform.localPosition = Vector3.zero;
                temp.transform.localScale = Vector3.one;
                temp.GetComponentInChildren<Text>().text = Notice.args[0];
            }
            if (PlayerData.dictionary.ContainsKey(Notice.key))
            {
                GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/Notice"));
                temp.transform.SetParent(transform);
                temp.transform.localPosition = Vector3.zero;
                temp.transform.localScale = Vector3.one;
                temp.GetComponentInChildren<RuleLayer>().ShowText(PlayerData.dictionary[Notice.key]);
            }         
            Debug.Log(Notice.key);
        }
    }
    public void DesTroyNotice()
    {
        GameObject obj = GameObject.Find("Canvas/NoticeTip");
        if (obj)
        {
            DestroyImmediate(obj);
        }
    }
    //out
    void ExitRoomGet(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(7, 5))
        {           
            Method.LoadMainCity();
        }
    }
    /// <summary>
    /// 其他玩家退出房间申请返回  //out
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="message"></param>
    void ExitRoom(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 4))
        {
            com.guojin.mj.net.message.game.GameExitUser Gu = (com.guojin.mj.net.message.game.GameExitUser)message;
            PlayerInfo[] pl = GetComponentsInChildren<PlayerInfo>();
            for (int i = 0; i < pl.Length; i++)
            {
                if (pl[i].userID.text == Gu.userId.ToString())
                {
                    Destroy(pl[i].gameObject);
                }
            }
        }
    }   
    void ReceiveChatInfo(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 25))
        {
            com.guojin.mj.net.message.game.PlayerReceiveChatInfo PRCI = (com.guojin.mj.net.message.game.PlayerReceiveChatInfo)message;
            ChatInfo chatInfo = new ChatInfo();
            chatInfo.ShowChatContent(PRCI.UserIndex, PRCI.ReceiveChatInfo, PRCI.Num);
        }
    }
    int num = 0;  

    /// <summary>
    /// 其他用户加入房间的操作【1，8】GameUserInfo  //out and in
    /// </summary>
    /// <param name="mes"></param>
    /// <param name="message"></param>
    void JoinUserInfo(string mes, com.guojin.core.io.message.Message message)
    {
        if (mes == MessageFactoryImpi.instance.getMessageString(1, 8))
        {
            isThree = 0;
            com.guojin.mj.net.message.game.GameUserInfo GUIO = (com.guojin.mj.net.message.game.GameUserInfo)message;     
            GameObject.Find("HeadPhoto" + GUIO.locationIndex).GetComponent<PlayerInfo>().HiddenOffLine();   
        }
    }
   
    public int isThree = 0;

    public Text junum;
    void ShowDingPao()
    {
        if (isThree < 1)
        {
            isThree++;
            GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/ChoicePao"));
            temp.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
            temp.transform.localScale = Vector3.one;
            temp.transform.localPosition = new Vector3(0, -100, 0);
            CloneCenter();
            ShowGuShiPao();
            //if (Method.MJType == "MaJongGuShi")
            //{
            //    ShowGuShiPao();
            //}

        }

    }
    void ShowGuShiPao()
    {
        if (Method.StateText != null)
        {
            Method.StateText.GetComponent<OpenAndClose>().ShowPao();
        }
        else
        {
            GameObject temp1 = Instantiate(Resources.Load<GameObject>("MjPrefab/ShowState"));
            temp1.transform.SetParent(m_gameStartParent);
            temp1.transform.localScale = Vector3.one;
            temp1.transform.localPosition = Vector3.zero;
            Method.StateText = temp1;
            temp1.GetComponent<OpenAndClose>().InitShowPao();
        }
    }


    void ReciveAllDingPao(string mes, com.guojin.core.io.message.Message message)
    {
        if (mes == MessageFactoryImpi.instance.getMessageString(1, 31))
        {
            com.guojin.mj.net.message.game.AllDingPao AD = (com.guojin.mj.net.message.game.AllDingPao)message;
            int[] paoArr = AD.userDingPaos;
            if (Method.ChoicePaoObj != null)
            {
                Destroy(Method.ChoicePaoObj);
            }
            Method.DesStartGameCollMetho();
            if (Method.ob == null)
            {
                CloneCenter();
            }
            Debug.Log("_______________________+++++++++++++++++++");
            for (int i = 0; i < AD.userDingPaos.Length; i++)
            {
                Method.SwitchM(AD.userDingPaos[i], GameObject.Find("Center_Img").transform, i);
            }
            ShowGuShiAllDingPao();
            //if (Method.MJType == "MaJongGuShi")
            //{
            //    ShowGuShiAllDingPao();
            //}
        }
    }


    void ShowGuShiAllDingPao()
    {
       
        if (Method.StateText != null)
        {
            Destroy(Method.StateText);
        }

    }
    /// <summary>
    /// 接收到定跑消息之后的处理【1，30】ShowPaoRet
    /// </summary>
    /// <param name="message"></param>
    void ShowPaoRetOrNot(string mes, com.guojin.core.io.message.Message message)
    {
        if (mes == MessageFactoryImpi.instance.getMessageString(1, 30))
        {
            Method.DesStartGameCollMetho();
            Method.StartGameInit();
            if (Method.ob != null)
            {
                Destroy(Method.ob);
            }
            ShowDingPao();
        }
    }


    void CloneCenter()
    {
        GameObject temp1 = Instantiate(Resources.Load<GameObject>("MjPrefab/Center_Img"));
        Method.ob = temp1;
        temp1.name = "Center_Img";
        temp1.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
        temp1.transform.SetSiblingIndex(0);
        temp1.transform.localScale = Vector3.one;
        temp1.transform.localPosition = new Vector3(0, 58, 0);
    }
    /// <summary>
    /// 接收到其他玩家定跑消息之后的处理【1，29】OperationDingPaoRet
    /// </summary>
    /// <param name="message"></param>
    void ShowDingPaoPosition(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 29))
        {
            Method.times++;
            com.guojin.mj.net.message.game.OperationDingPaoRet opr = (com.guojin.mj.net.message.game.OperationDingPaoRet)message;
            if (Method.ob == null)
            {
                CloneCenter();
            }
            if (opr.locationIndex == Method.Index)
            {
                Method.SwitchM(opr.dingPaoCount, GameObject.Find("Center_Img").transform, opr.locationIndex);

            }

            if (Method.StateText == null)
            {
                GameObject temp1 = Instantiate(Resources.Load<GameObject>("MjPrefab/ShowState"));
                temp1.transform.SetParent(m_gameStartParent);
                temp1.transform.localScale = Vector3.one;
                temp1.transform.localPosition = Vector3.zero;
                Method.StateText = temp1;
                temp1.GetComponent<OpenAndClose>().InitShowPao();
            }

            if (opr.locationIndex != Method.Index)
            {
                Method.StateText.GetComponent<OpenAndClose>().ShowWeiZhiDingPaoAlready(opr.locationIndex);
            }           
        }
    }

    /// <summary>
    /// 接收到解散房间请求之后的处理
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="message"></param>
    void DeleRoom(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 21))
        {
            if (Method.JieSanPanl == null)
            {
                com.guojin.mj.net.message.game.VoteDelSelect VD = (com.guojin.mj.net.message.game.VoteDelSelect)message;
                GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/ExitPanel"));
                temp.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
                temp.transform.localPosition = Vector3.zero;
                temp.transform.localScale = Vector3.one;
                Method.JieSanPanl = temp;
                temp.GetComponent<PlayerInfo>().CreatUserInfo1(VD.userName);
                Button[] btnArr = temp.GetComponentsInChildren<Button>();
                Debug.Log(btnArr[0].name);
                btnArr[0].onClick.AddListener(delegate
                {
                    VoiceManger.Instance.BtnVoice();
                    com.guojin.mj.net.message.game.VoteDelSelectRet VDS = com.guojin.mj.net.message.game.VoteDelSelectRet.create(true, VD.userId);
                    SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(VDS));
                    Destroy(Method.JieSanPanl);
                });
                btnArr[1].onClick.AddListener(delegate
                {
                    VoiceManger.Instance.BtnVoice();
                    com.guojin.mj.net.message.game.VoteDelSelectRet VDSR = com.guojin.mj.net.message.game.VoteDelSelectRet.create(false, VD.userId);
                    SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(VDSR));
                    Destroy(Method.JieSanPanl);
                });
                btnArr[2].onClick.AddListener(delegate
                {
                    VoiceManger.Instance.BtnVoice();
                    Destroy(Method.JieSanPanl);
                });
            }

        }
    }
    /// <summary>
    /// 解散房间
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="message"></param>
    public void DissolutionRoom(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 3))
        {
            com.guojin.mj.net.message.game.GameDelRoom GDR = (com.guojin.mj.net.message.game.GameDelRoom)message;
            Debug.Log("++++++++++++++");
            Debug.Log(GDR.isStart);
            Debug.Log(GDR.isEnd);

            Debug.Log("++++++++++++++");
            if (!GDR.isEnd)
            {
                Method.isChongLian = false;       
                Method.LoadMainCity();
                Debug.Log("YES,EXIT");
            }
        }
    }


    public static int shengyu;
    /// <summary>
    /// 服务器发牌接口【1，9】MajiangChapterMsg
    /// </summary>
    /// <param name="message"></param>
    void ShowAllFaPaiInfo(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 9))
        {
            com.guojin.mj.net.message.game.MajiangChapterMsg mm = (com.guojin.mj.net.message.game.MajiangChapterMsg)message;
            Method.StartGameInit();
            // reciveMaJong(mm.chapterNums,Method.MJType); 固始麻将
            Method.ShowMaJongInfo(mm.chapterNums, mm.freeLength);
            shengyu = mm.freeLength;
            Method.isRun = true;
            Method.isUpdate = true;
            Method.FaPaiMaJong(mm);
        }
    }
    void reciveMaJong(int ju, int type)
    {
        if (ju == 0 && type == 1)
        {
            Method.DesStartGameCollMetho();
            CloneCenter();
        }
    }
    /// <summary>
    /// 接到牌后的显示操作【1，12】OperationFaPai
    /// </summary>
    /// <param name="message"></param>
    void ShowFaPai(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 12))
        {
            com.guojin.mj.net.message.game.OperationFaPai ofp = (com.guojin.mj.net.message.game.OperationFaPai)message;
            //接收麻将到前端           
            Method.ReceiveMaJong(ofp.pai);
            Method.isMine = true;
            //判断扛 胡
            Method.BehaReciveMaJong(ofp);            
        }
    }
    /// <summary>
    /// 同步发牌信息[1,16]SyncOpt
    /// </summary>
    /// <param name="message"></param>
    void ShowFaPaiOther(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 16))
        {
            com.guojin.mj.net.message.game.SyncOpt SO = (com.guojin.mj.net.message.game.SyncOpt)message;
            if (SO.opt == "OUT")
            {
                if (GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback || SO.index != Method.Index)
                {
                    GameObject obj = Method.intArr[SO.index] == 0 ? Method.goParent.gameObject : Method.tranList[Method.intArr[SO.index] + 11].gameObject;
                    if (obj.activeSelf)
                    {
                        if (!GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback)
                            Method.AddMajongOutMajong(SO.pai, SO.index);
                        Method.setFalse();
                        obj.SetActive(false);
                    }
                    Method.ChuOther(SO.pai, SO.index);

                    if (GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback)
                    {
                        if (Method.intArr[SO.index] == 0)
                        {
                            Method.intlist.Remove(SO.pai);
                        }
                        GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().HandCard[Method.intArr[SO.index]].Remove(SO.pai);
                        GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().ShowHandCard(Method.intArr[SO.index]);
                    }
                    if (Method.tingArr.Count != 0)
                    {
                        if (Method.TingMaJong != null)
                        {
                            Destroy(Method.TingMaJong);
                        }
                        VoiceManger.Instance.TingMaJong(Method.tingArr);
                    }
                }
            }

            if (SO.opt == "FA")
            {
                shengyu--;
                Method.ShengyuMaJong.text = shengyu.ToString();
                if (SO.pai == -2)
                {
                    //Method.AddMajongOutMajong(SO.pai, SO.index);
                    Method.FaOnePai(SO.index);
                }
                else
                {
                    //Method.ReceiveMaJong(SO.pai, SO.index);

                    if (GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback)
                    {
                        GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().HandCard[Method.intArr[SO.index]].Add(SO.pai);
                        Method.ReceiveMaJong(SO.pai, SO.index);
                        if (Method.intArr[SO.index] == 0)
                        {
                            Method.intlist.Add(SO.pai);
                        }
                    }
                }
            }
            if (SO.opt == "PENG")
            {
                Debug.Log("_____________");
                Debug.Log(SO.index);
                Debug.Log(SO.otherUserIndex);
                Method.fangXiangNum = Method.returnFangxiang(SO.index, SO.otherUserIndex);
                Debug.Log(Method.fangXiangNum);
                Debug.Log("_____________");
                Method.MaJongBehavior(SO.pai, SO.index, 0);
            }
            if (SO.opt == "DA_MING_GANG")
            {
                Method.fangXiangNum = Method.returnFangxiang(SO.index, SO.otherUserIndex);
                Method.MaJongBehavior(SO.pai, SO.index, 1);
            }
            if (SO.opt == "XIAO_MING_GANG")
            {
                Debug.Log(SO.index);
                Debug.Log(SO.otherUserIndex);
                Method.fangXiangNum = Method.returnFangxiang(SO.index, SO.otherUserIndex);
                Method.SolveXiaoMingGang(SO.index, SO.pai);
            }
            if (SO.opt == "AN_GANG")
            {
                Method.MaJongBehavior(SO.pai, SO.index, 2);

            }
            if (SO.opt == "HU")
            {
                Method.PGH = 37;
              //  Method.CloneShow("hu", SO.index);
            }   
        }
    }

    /// <summary>
    /// 同步时间信息[1,17]SyncOptTime
    /// </summary>
    /// <param name="message"></param>
    void ShowTime(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 17))
        {
            com.guojin.mj.net.message.game.SyncOptTime SOT = (com.guojin.mj.net.message.game.SyncOptTime)message;
            //显示出牌剩余的时间信息
            Method.isUpdate = true;
            // 根据 SOT.index 显示位置信息
            Method.SetPositionStar(SOT.index);
        }
    }
    /// <summary>
    /// 收到是否听牌的消息之后进行的操作[1,18]TingPai
    /// </summary>
    /// <param name="message"></param>
    void OutPlayerIsTingPai(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 18))
        {
            com.guojin.mj.net.message.game.TingPai TP = (com.guojin.mj.net.message.game.TingPai)message;
            List<int> ARR = Method.OutList(TP.pais);
            Method.tingArr.Clear();
            if (ARR.Count != 0)
            {
                //听牌
                if (Method.TingMaJong != null)
                {
                    Destroy(Method.TingMaJong);
                }
                for (int i = 0; i < ARR.Count; i++)
                {
                    Method.tingArr.Add(ARR[i]);
                }
                VoiceManger.Instance.TingMaJong(ARR);
            }
            else
            {
                if (Method.TingMaJong != null)
                {
                    Destroy(Method.TingMaJong);
                }
            }

        }
    }
    /// <summary>
    /// 玩家出完牌后其他玩家接收到吃碰扛胡的信息【1，10】OperationCPGH
    /// </summary>
    /// <param name="message"></param>
    void ReciveCPGHInfo(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 10))
        {
            com.guojin.mj.net.message.game.OperationCPGH CPGH = (com.guojin.mj.net.message.game.OperationCPGH)message;
            if (CPGH.index == Method.Index)
            {
                Method.PGHShow(CPGH.index, CPGH.pai, CPGH.isPeng, false, CPGH.isGang, false, CPGH.isHu, true);
            }
        }
    }
    /// <summary>
    /// 通知用户出牌的消息【1，14】OperationOut
    /// </summary>
    /// <param name="message"></param>
    void NoticePlayerOutPai(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 14))
        {
            com.guojin.mj.net.message.game.OperationOut OPO = (com.guojin.mj.net.message.game.OperationOut)message;
            if (OPO.index == Method.Index)
            {
                Method.isMine = true;
            }
        }
    }

    //public static GameObject HoldGameOBJ;//等待玩家显示
    //public static GameObject startBTN;//房主开始游戏按钮
    /// <summary>
    /// 接收到游戏结束后的处理
    /// </summary>
    /// <param name="message"></param>
    public void GameChapterEnd(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 0))
        {
            isThree = 0;
            Method.tingArr.Clear();
            Debug.Log("接收到（1，0）");
            if (!GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback)
            {
                Method.isChongLian = false;
            }
            com.guojin.mj.net.message.game.GameChapterEnd End = (com.guojin.mj.net.message.game.GameChapterEnd)message;
            Method.END = End;
            Method.ShowScore(End.fanResults);
            if (End.huPaiIndex != -1)
            {
                Method.PGH = 37;
                Method.CloneShow("hu", End.huPaiIndex);
            }                     

            Method.ShowStartGame();
            Transform aprent = GameObject.Find("ChoicePlayeWnd_Img").transform;
            GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/GameOverpanle"));
            temp.transform.SetParent(aprent, true);
            temp.transform.SetSiblingIndex(aprent.childCount);
            temp.transform.localScale = Vector3.one;
            temp.transform.localPosition = Vector3.zero;
            temp.GetComponent<GameOverPlayer>().ShowInfo(End);
            if (Method.MJType == "MaJongGuShi")
            {
                PlayerData.PaoZuiDictionary.Clear();
                Method.DestoryAllChildren();
                Method.setFalse();
                for (int i = 0; i < Method.PeopleNum; i++)
                {
                    GameObject.Find("HeadPhoto" + i).GetComponent<PlayerInfo>().SetFalseZui();
                }
            }
        }

    }
    /// <summary>
    /// 接收到（1，26）显示总结算界面
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="message"></param>
    public void StaticsResultRet(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 26))
        {
            Debug.Log("接收到(1,26)");
            com.guojin.mj.net.message.game.StaticsResultRet srt = (com.guojin.mj.net.message.game.StaticsResultRet)message;

            //GameObject bt = GameObject.Find("GameStart");
            //if (bt != null)
            //    Destroy(bt);


            GameObject temp1 = TooL.loadPrefab(m_gameStartParent.gameObject, "ReturnHall");
            temp1.transform.SetSiblingIndex(5);
            temp1.GetComponent<Button>().onClick.AddListener(delegate
            {
                VoiceManger.Instance.BtnVoice();
                //SocketMessageQueue.GetInstance().addMsg(GameGlobalMsg.DissolveRoom, null);
                Method.isChongLian = false;
                //GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_LoadingPage);
                Method.LoadMainCity();
            });
            //GameObject playerResult = TooL.loadPrefab(m_gameOverParent, "GameOveRall");

            //playerResult.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            //GameResult pr = playerResult.GetComponent<GameResult>();
            //pr.GetPlayerResult(srt);
            Transform aprent = GameObject.Find("ChoicePlayeWnd_Img").transform;
            GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/GameOverEnd"));
            temp.transform.SetParent(aprent);
            temp.transform.SetSiblingIndex(aprent.childCount - 2);
            temp.transform.localScale = Vector3.one;
            temp.transform.localPosition = Vector3.zero;
            temp.GetComponent<GameOverEndManger>().ShowEndInfo(srt);
        }
    }


    //**********************************************************************************
    //**********************************************************************************
    // 以下为固始麻将新增消息
    //**********************************************************************************
    //**********************************************************************************
    /// <summary>
    /// 接收到跑嘴消息   ShowSelectZuiTypeRet
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="message"></param>
    void ReciveShowSelectZuiTypeRet(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 43))
        {
            if (Method.StartGameBtn != null)
            {
                Destroy(Method.StartGameBtn);

            }
            PlayerData.PaoZuiDictionary.Clear();
            ShowZui();
        }
    }
    void ShowZui()
    {
        if (Method.StateText == null)
        {
            GameObject temp1 = Instantiate(Resources.Load<GameObject>("MjPrefab/ShowState"));
            temp1.transform.SetParent(m_gameStartParent);
            temp1.transform.localScale = Vector3.one;
            temp1.transform.localPosition = Vector3.zero;
            Method.StateText = temp1;
            temp1.GetComponent<OpenAndClose>().InitState();
        }
        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/ChoiceZui"));
        temp.transform.SetParent(m_gameStartParent);
        temp.name = "ChoiceZui";
        temp.transform.localScale = Vector3.one;
        temp.transform.localPosition = new Vector3(0, -180, 0);
        ChoiceZui[] choiceArr = temp.GetComponentsInChildren<ChoiceZui>();
        for (int i = 0; i < choiceArr.Length; i++)
        {
            choiceArr[i].CreateOne(i);
        }
    }
    /// <summary>
    /// 接收到其他玩家跑嘴
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="message"></param>
    void ReciveOtherSelectZuiTypeRet(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 45))
        {
            com.guojin.mj.net.message.game.SelectZuiTypeRet SR = (com.guojin.mj.net.message.game.SelectZuiTypeRet)message;


            if (Method.intArr[SR.index] != 0)
            {
                GameObject.Find("HeadPhoto" + SR.index).GetComponent<PlayerInfo>().ShowZui();
                Method.StateText.GetComponent<OpenAndClose>().ShowWeiZhiDingZuiAlready(SR.index);
            }
            //PlayerData.PaoZuiDictionary.Add(SR.index, SR.PaoZuiNum);
        }
    }
    /// <summary>
    /// 接收到所有玩家跑嘴的消息
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="message"></param>
    void ReciveAllDingZuiZi(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 46))
        {
            com.guojin.mj.net.message.game.AllDingZuiZi ADZ = (com.guojin.mj.net.message.game.AllDingZuiZi)message;
            for (int i = 0; i < ADZ.userDingQues.Length; i++)
            {
                PlayerData.PaoZuiDictionary.Add(i, ADZ.userDingQues[i]);
            }
        }
    }
    /// <summary>
    /// 接收到所有玩家定缺的消息
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="message"></param>
    void ReciveAllQuePaiTypedRet(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 40))
        {
            com.guojin.mj.net.message.game.AllQuePaiTypedRet AQP = (com.guojin.mj.net.message.game.AllQuePaiTypedRet)message;
            //所有定缺结束的操作
            if (Method.StateText != null)
            {
                Destroy(Method.StateText);
            }
            for (int i = 0; i < AQP.userDingQues.Length; i++)
            {
                if (i != Method.Index)
                {
                    Method.CloneDuanmen(i, Resources.Load<Sprite>("NewUIPicture/gushi/duan" + AQP.userDingQues[i]));
                    Method.ChuOthergushi(AQP.userDingQueOutPais[i], i);
                }
            }
            //for (int i = 0; i < AQP.userDingQueOutPais.Length; i++)
            //{
            //    if (Method.intArr[i]!=0)
            //    {
            //        Method.ChuOthergushi(AQP.userDingQueOutPais[i], i);
            //    }                
            //}

        }
    }
    /// <summary>
    /// 接收到缺门的消息到前端
    /// </summary>
    void ReciveShowSelectQueRet(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 39))
        {
            if (Method.StateText == null)
            {
                GameObject temp1 = Instantiate(Resources.Load<GameObject>("MjPrefab/ShowState"));
                temp1.transform.SetParent(m_gameStartParent);
                temp1.transform.localScale = Vector3.one;
                temp1.transform.localPosition = Vector3.zero;
                Method.StateText = temp1;
                temp1.GetComponent<OpenAndClose>().ShowQueMen();
                Method.setTrue();
            }

            //显示自己的
            GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/DingQue"));
            temp.transform.SetParent(m_gameStartParent);
            temp.transform.localScale = Vector3.one;
            temp.transform.localPosition = new Vector3(0, -94, 0);
            Method.Dingque = temp;
            temp.GetComponent<OpenAndClose>().ShowDingQue(Method.GetRecommend());
            Method.tranList[1].localPosition = new Vector3(490, -179, 0);
            Method.isFirstOutPai = true;
            Method.isMine = true;
        }
    }
    /// <summary>
    /// 接收到玩家选完缺门的操作
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="message"></param>
    void ReciveSelectQuePaiTypedRet(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 42))
        {
            com.guojin.mj.net.message.game.SelectQuePaiTypedRet SQPT = (com.guojin.mj.net.message.game.SelectQuePaiTypedRet)message;

            if (GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback)
            {
                RecivePlayBack(SQPT);
            }
            else
            {
                if (Method.StateText == null)
                {
                    GameObject temp1 = Instantiate(Resources.Load<GameObject>("MjPrefab/ShowState"));
                    temp1.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
                    temp1.transform.localScale = Vector3.one;
                    temp1.transform.localPosition = Vector3.zero;
                    Method.StateText = temp1;
                    temp1.GetComponent<OpenAndClose>().ShowQueMen();
                }
                if (SQPT.index == Method.Index)
                {
                    Method.tranList[1].localPosition = new Vector3(444, -179, 0);
                }
                else
                {
                    Method.StateText.GetComponent<OpenAndClose>().ShowWeiZhiDingQueAlready(SQPT.index);
                    Method.setFalseOne(SQPT.index);
                }
            }            

        }
    }

    void RecivePlayBack(com.guojin.mj.net.message.game.SelectQuePaiTypedRet SQPT)
    {
        Method.CloneDuanmen(SQPT.index, Resources.Load<Sprite>("NewUIPicture/gushi/duan" + SQPT.paiTypeIndex));
        GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().HandCard[Method.intArr[SQPT.index]].Remove(SQPT.paiindex);
        if (SQPT.index == Method.Index)
        {
            Method.intlist.Remove(SQPT.paiindex);

            Method.CloneChild();
            Method.tranList[1].localPosition = new Vector3(444, -179, 0);
        }
        else
        {
            //GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().HandCard[Method.intArr[SQPT.index]].Remove(SQPT.paiindex);
            GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().ShowHandCard(Method.intArr[SQPT.index]);
        }
        
        Method.ChuOthergushi(SQPT.paiindex, SQPT.index);

    }


    void ReciveRecordDetailsRet(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 48))
        {
            com.guojin.mj.net.message.game.RecordDetailsRet SQPT = (com.guojin.mj.net.message.game.RecordDetailsRet)message;

            GameObject temp1 = Instantiate(Resources.Load<GameObject>("MjPrefab/ParticularsPanel"));
            temp1.transform.SetParent(m_gameStartParent);
            temp1.transform.localPosition = Vector3.zero;
            temp1.transform.localScale = Vector3.one;
            temp1.GetComponentInChildren<ParticularsPanel>().ShowInfoSoce(SQPT);
            //\r\n
        }
    }
}
