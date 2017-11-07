using UnityEngine;
using System.Collections;
using com.guojin.mj.net.message;
using com.guojin.core.io.message;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using com.guojin.mj.net.message.game;
using com.guojin.mj.net.message.club;
using com.guojin.mj.net.message.login;
using GameCore;
using Assets.Scripts.BullFight.Manager;

public class LoginGame : Observer
{


    string tempRoomID;
    void Awake()
    {
        DontDestroyOnLoad(this);
        DynamicAudioManager.Instance.InitAudioSource();
        Assets.Scripts.BullFight.Manager.DynamicUIManager.Instance.InitDynamicUIManager();
    }
    void Start()
    {
        // 优先注册GameData的事件
        GameData.GetInstance().initEvent();
        this.initMsg();
        PlayerData.dictionary.Clear();
        PlayerData.Setdictionary();

    }

    protected override string[] GetMsgList() {
        return new string[] {
            MessageFactoryImpi.instance.getMessageString(7,10),
            MessageFactoryImpi.instance.getMessageString(7,20),//战绩
            MessageFactoryImpi.instance.getMessageString(7,15),
            MessageFactoryImpi.instance.getMessageString(1,7),  
            MessageFactoryImpi.instance.getMessageString(7,7),
              MessageFactoryImpi.instance.getMessageString(7,11),           
              MessageFactoryImpi.instance.getMessageString(1,10),
            MessageFactoryImpi.instance.getMessageString(1,37),//固始麻将
            GameGlobalMsg.HallOnClickZZMJ,
            GameGlobalMsg.MainOnClickReturn,
            GameGlobalMsg.NetClose,//网路断开连接
            GameGlobalMsg.GamHoRetBeturn,//游戏大厅返回
            GameGlobalMsg.AddYuanbaoBtn,//添加元宝
            GameGlobalMsg.StartNet,
            GameGlobalMsg.DissolveRoom,
            GameGlobalMsg.HallOnClickZJH,
            MessageFactoryImpi.instance.getMessageString(7,36),//俱乐部
           MessageFactoryImpi.instance.getMessageString(1,43),
           MessageFactoryImpi.instance.getMessageString(1,39),
        };
    }
    public override void OnMsg(string msg, com.guojin.core.io.message.Message data)
    {
     
        ReciveGameInfoGuShi(msg, data); //固始麻将房间配置
        //ShowFaPaiOther(msg, data);
        //ShowFaPai(msg, data);      
        ReciveNotice(msg, data);
        ReciveJoinRoomRet(msg, data);
        ReciveGameInfo(msg, data);
    
        //俱乐部信息返回
        if (msg == MessageFactoryImpi.instance.getMessageString(7, 36))
        {
            ClubInformationCache.instance.HasInClub = true;
            ClubInfo clubInfo = data as ClubInfo;
            ClubInformationCache.instance.ClubId = clubInfo.ClubId;
            ClubInformationCache.instance.CreateUserName = clubInfo.CreateUserName;
            ClubInformationCache.instance.Notice = clubInfo.Notice;
            ClubInformationCache.instance.WxNo = clubInfo.WxNo;
        }

        //心跳包返回
        if (msg == MessageFactoryImpi.instance.getMessageString(7, 15))
        {
            com.guojin.mj.net.message.login.Pong pong = (com.guojin.mj.net.message.login.Pong)data;
            GameObject obj = GameObject.Find("StaticSource");
            if (obj)
            {
                obj.GetComponent<WorldTime>().Stoptimeout();
                obj.GetComponent<WorldTime>().CheckHeartBeat(pong.time);
            }
        }

        if (msg == MessageFactoryImpi.instance.getMessageString(7, 10))
        {
            // 登录返回来
            com.guojin.mj.net.message.login.Login_Ret LR = (com.guojin.mj.net.message.login.Login_Ret)data;
            PlayerPrefs.SetString(DefaultKeyConst.VisitorTokenKey, LR.loginToken);
            PlayerPrefs.Save();
            Method.level = LR.level;
            DNGlobalData.token = LR.loginToken;
            DNGlobalData.roomID = LR.roomCheckId;
            tempRoomID = LR.roomCheckId;
            DNGlobalData.currentUserID = LR.id;
            DNGlobalData.currentUserName = LR.name;
            Debug.Log("++++++++++++++++++s" + LR.level + "+++++++++++++++++++++++++");
            Debug.Log("服务器返回token》》" + LR.loginToken);

            if (LR.roomCheckId != null) {
                Method.isChongLian = true;
                Method.userID = LR.id;
                Method.GameRoomID = LR.roomCheckId;
                Method.userName = LR.name;
            } else {
                Method.GameRoomID = LR.roomCheckId;
                Method.userID = LR.id;
                Method.userName = LR.name;
                Method.isChongLian = false;

                DNGlobalData.isChongLian = false;
                DNGlobalData.confirmChongLian = false;
            }
            if (SceneManager.GetActiveScene().name == "Login") {
                GameObject obj_0 = GameObject.Find("LoginPage(Clone)");
                if (obj_0) {
                    obj_0.GetComponent<LoginPageEvent>().IsNeedWxPutOn = false;
                    StopCoroutine(obj_0.GetComponent<LoginPageEvent>().HandleWxToken());
                }
            }
            if (Method.isDuanwang && Method.isChongLian) {
                com.guojin.mj.net.message.login.JoinRoom jr = com.guojin.mj.net.message.login.JoinRoom.create(Method.GameRoomID);
                SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(jr));
                Method.isDuanwang = false;
            }
            if (!Method.isChongLian && Method.isHor) {
                Debug.Log("UIState_LoadingPage--Logingame--7,10");
                GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_LoadingPage);
            }
            GameObject obj = GameObject.Find("NetConnectPage(Clone)");
            GameObject dnObj = GameObject.Find("DNNetConnectPage(Clone)");
            if (obj || dnObj) {
                DestroyImmediate(obj);
                GameObject.Find("StaticSource").GetComponent<WorldTime>().isUpdateStop = false;
                GameObject.Find("StaticSource").GetComponent<WorldTime>().isConnecting = false;
            }
            if (SceneManager.GetActiveScene().name == "Login")
            {
                Debug.Log("UIState_LoadingPage--Logingame--7,10");
                GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_LoadingPage);
            }
        } 
        if (msg == MessageFactoryImpi.instance.getMessageString(7, 20))
        {
            com.guojin.mj.net.message.login.RoomHistoryListRet rhl = (com.guojin.mj.net.message.login.RoomHistoryListRet)data;

            ResultPageEvent.Instance.ShowResultRoom(rhl);
        }

    }
    IEnumerator waiteOne()
    {
        yield return new WaitForSeconds(1);
        com.guojin.mj.net.message.login.JoinRoom jr = com.guojin.mj.net.message.login.JoinRoom.create(Method.GameRoomID);
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(jr));
    }
    void ReciveNotice(string msg, com.guojin.core.io.message.Message data)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(7, 11))
        {
            com.guojin.mj.net.message.login.Notice notice = (com.guojin.mj.net.message.login.Notice)data;

            if (notice.reboot)
            {
                //todo:sunfei 踢人操作
            }
            else
            {
                if (PlayerData.dictionary.ContainsKey(notice.key))
                {
                    GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/JoinRoomDef"));
                    GameObject addnode = GameObject.Find("addNode");
                    temp.transform.SetParent(addnode.transform);
                    temp.transform.localPosition = Vector3.zero;
                    temp.transform.localScale = Vector3.one;
                    temp.GetComponentInChildren<RuleLayer>().ShowText(PlayerData.dictionary[notice.key]);
                }
            }
        }
    }
    void ReciveGameInfoGuShi(string msg, Message data)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 37))
        {
            MainLogic.Controller.gameType = GameType.MJ;
            com.guojin.mj.net.message.game.GameRoomInfoGuShi GJM = (com.guojin.mj.net.message.game.GameRoomInfoGuShi)data;
   
            Method.MJType = "MaJongGuShi";  
            Method.message = GJM;
            MaJong.MaJongManage.Instance().CreatMaJong("MaJongGuShi");
          
            if (GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback)
            {
                Debug.Log("UIState_LoadingPage--Logingame--ReciveGameInfoGuShi");
                MainLogic.Controller.gameType = GameType.MJ;
                GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_LoadingPage);
            }
            else
            {
                if (Method.isHor)
                {
                    MainLogic.Controller.gameType = GameType.MJ;
                    SceneManager.LoadScene("Horizontal");
                }
                else
                {
                    if (GJM.State == 0 && GJM.ChapterMax == GJM.leftChapterNums)
                    {
                        if (Method.isChongLian)
                        {
                            if (Method.readyGame != null)
                            {
                                DestroyImmediate(Method.readyGame);
                            }
                        }
                        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/WaitStartGame"));
                        temp.transform.SetParent(GameObject.Find("addNode").transform);
                        temp.transform.localScale = Vector3.one;
                        temp.transform.localPosition = Vector3.zero;
                        Method.readyGame = temp;
                    }
                    else
                    {
                        MainLogic.Controller.gameType = GameType.MJ;
                        GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_LoadingPage);
                    }
                }
            }
        }
    }

    void ReciveGameInfo(string msg, Message data)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 7))
        {
            MainLogic.Controller.gameType = GameType.MJ;
            GameRoomInfo GJM = (GameRoomInfo)data;
            Method.MJType = "MaJongZhengZhou";         
            Method.message = GJM;
            MaJong.MaJongManage.Instance().CreatMaJong("MaJongZhengZhou");         
            if (Method.IsSeverError)
            {
                Method.IsSeverError = false;
                Method.isChongLian = true;
                SceneManager.LoadScene("Horizontal");
                return;
            }
            if (GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback)
            {
                Debug.Log("UIState_LoadingPage--LoginGame--ReciveGameInfo");
                MainLogic.Controller.gameType = GameType.MJ;
                GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_LoadingPage);
            }
            else
            {

                if (GJM.State == 0 && GJM.ChapterMax == GJM.leftChapterNums)
                {
                    if (Method.isHor)
                    {                      
                        SceneManager.LoadScene("Horizontal");                       
                    }
                    else
                    {
                        if (Method.isChongLian)
                        {
                            if (Method.readyGame!=null)
                            {
                                DestroyImmediate(Method.readyGame);
                            }
                        }
                        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/WaitStartGame"));
                        temp.transform.SetParent(GameObject.Find("addNode").transform);
                        temp.transform.localScale = Vector3.one;
                        temp.transform.localPosition = Vector3.zero;
                        Method.readyGame = temp;
                    }

                }
                else
                {
                    if (Method.isHor)
                    {
                        MainLogic.Controller.gameType = GameType.MJ;
                        SceneManager.LoadScene("Horizontal");
                    }
                    else
                    {
                        Debug.Log("UIState_LoadingPage--LoginGame--ReciveGameInfo");
                        MainLogic.Controller.gameType = GameType.MJ;
                        GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_LoadingPage);
                    }
                }
            }
        }
    }
    void ReciveJoinRoomRet(string mes, Message message) {
        if (mes == MessageFactoryImpi.instance.getMessageString(7, 7)) {

            com.guojin.mj.net.message.login.JoinRoomRet jrt = (com.guojin.mj.net.message.login.JoinRoomRet)message;
            if (GameCore.UIState_GameHallPage.comeFromState==UIState_GameHallPage.ComeFromState.FromH5) {
                UIState_GameHallPage.comeFromState = UIState_GameHallPage.ComeFromState.Null;

                if (jrt.result && jrt.Type == 0) {
                    com.guojin.mj.net.message.game.GameJoinRoom Gjr = new com.guojin.mj.net.message.game.GameJoinRoom();
                    SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(Gjr));
                } else if (jrt.result && jrt.Type == 1) {

                    GameObject obj1 = GameObject.Find("WaitPanel(Clone)");
                    if (obj1) {
                        GameObject.DestroyImmediate(obj1);
                    }
                    UIManager.ChangeUI(UIManager.PageState.DNWaitPanel, (GameObject obj) => {
                        // obj.GetComponent<DNCreatRoomPage>().InformationSetting();
                    });
                    DNGlobalData.isChongLian = true;
                    DNGlobalData.confirmChongLian = true;
                    //tempRoomID = null;
                    //if (tempRoomID != null) {
                    //}
                } else if (jrt.Type == 0) {
                    StartCoroutine(waitLogin());
                } else if (jrt.Type == 1) {
                    StartCoroutine(ErrorLoginDN());
                }
            }

            //if (Method.isChongLian&&!jrt.result)
            //{
            //    Method.isChongLian = false;
            //    UIManager.ChangeUI(UIManager.PageState.MJCreatRoomPage, (GameObject obj) =>
            //    {
            //        obj.GetComponent<MJCreatRoomPageEvent>().InformationSetting();
            //    });
            //}

            if (Method.isChongLian && jrt.Type == 0) {
                com.guojin.mj.net.message.game.GameJoinRoom Gjr = new com.guojin.mj.net.message.game.GameJoinRoom();
                SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(Gjr));
            } else if (Method.isChongLian && jrt.Type == 1) {
                if (SceneManager.GetActiveScene().name == "BullFight") {
                    GameManager.instance.ClearAllDictAndList();
                    DNGlobalData.isChongLian = true;
                    DNGlobalData.confirmChongLian = true;
                    SceneManager.LoadScene("BullFight");
                } else if (tempRoomID != null) {
                    GameObject obj1 = GameObject.Find("WaitPanel(Clone)");
                    if (obj1) {
                        GameObject.DestroyImmediate(obj1);
                    }
                    UIManager.ChangeUI(UIManager.PageState.DNWaitPanel, (GameObject obj) => {
                        // obj.GetComponent<DNCreatRoomPage>().InformationSetting();
                    });
                    DNGlobalData.isChongLian = true;
                    DNGlobalData.confirmChongLian = true;
                    tempRoomID = null;
                }
            }


        }
    }
    IEnumerator ErrorLoginDN() {
        yield return new WaitForSeconds(1.5f);
        UIManager.ChangeUI(UIManager.PageState.DNMainPanelPage, (GameObject obj) => {

        });
    }
    IEnumerator waitLogin()
    {
        yield return new WaitForSeconds(1.5f);
        UIManager.ChangeUI(UIManager.PageState.MJCreatRoomPage, (GameObject obj) =>
        {
            obj.GetComponent<MJCreatRoomPageEvent>().InformationSetting();
        });
    }
 
}
