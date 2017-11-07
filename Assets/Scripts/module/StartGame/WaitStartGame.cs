using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using com.guojin.mj.net.message;
using com.guojin.mj.net.message.login;
using com.guojin.mj.net.message.game;
using UnityEngine.SceneManagement;
using System.Reflection;
using System;

public class WaitStartGame : Observer
{
    public static int readynum = 0;
    public GameObject[] TouXiang;
    public Text roomID;
    public Text[] distance;//01 02 12 03 13 23
    [Header("退出游戏按钮")]
    public GameObject ExitBtn;
    [Header("邀请好友按钮")]
    public GameObject inviteFriend;
    [Header("玩家距离按钮")]
    public GameObject distanceBtn;
    [Header("本场规则按钮")]
    public GameObject rulerBtn;
    [Header("退出或解散按钮")]
    public GameObject dissolveAndExitBtn;
    [Header("GPS功能按钮")]
    public GameObject GPS;
    public static GameUserInfo[] GUArr;
    // Use this for initialization
    void Start()
    {
        Method.CanAddMessage = false;
        Method.messagelist.Clear();
        InitData();
        this.initMsg();
        addrulerBtn();
        Method.State = 1;
        Debug.Log(":::::::::::::::::::::::::::::::::" + Method.State);
        SendPlace();
        MaJong.MaJongManage.Instance().MaJong.ReciveRoomInfoWait(Method.message);
        reciveGameRoomInfoLater();
    }   
    void SendPlace()
    {
        string longitude = GameData.GetInstance().playerData.longitude;
        string latitude = GameData.GetInstance().playerData.latitude;
        if (!longitude.Equals(""))
        {
            //向服务器发送经纬度
            ReqLocation req = ReqLocation.Creat(0, longitude, latitude);
            SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(req));
        }

    }  
    /// <summary>
    /// 给游戏规则按钮添加事件
    /// </summary>
    void addrulerBtn()
    {
        rulerBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/Ruler"));
            temp.transform.SetParent(transform);
            temp.transform.localScale = Vector3.one;
            temp.transform.localPosition = Vector3.zero;
            temp.GetComponent<RuleLayer>().ShowRuler();          
        });
        distanceBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            if (Method.joinroonnum > 2)
            {
                GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/dictanceP"));
                temp.transform.SetParent(transform);
                temp.transform.localScale = Vector3.one;
                temp.transform.localPosition = Vector3.zero;
                temp.GetComponent<ShowDistance>().showHeadDistance(TouXiang, Method.showDistanceInfowai(Method.Index));
            }
            else
            {
                Debug.Log("不可查看");
                GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/JoinRoomDef"));
                temp.transform.SetParent(transform);
                temp.transform.localPosition = Vector3.zero;
                temp.transform.localScale = Vector3.one;
                temp.GetComponentInChildren<RuleLayer>().ShowText("暂时不可查看！");

            }

        });

        inviteFriend.GetComponent<Button>().onClick.AddListener(delegate
        {
            //邀请好友
            LoginAndShare.Controller.isPngShare = false;
            LoginAndShare.Controller.OnClickShareInfo();

        });
        GPS.GetComponent<Button>().onClick.AddListener(delegate
        {
            //GPS功能
            LoginAndShare.Controller.GoToProcessView();
        });
    }
    void InitData()
    {
        Method.GUIList.Clear();
        Method.diaoxianNum = 0;
        readynum = 0;
        Method.joinroonnum = 0;
    }
    protected override string[] GetMsgList()
    {
        return new string[] {
                  MessageFactoryImpi.instance.getMessageString(7,1),
                  MessageFactoryImpi.instance.getMessageString(7,7),
                  MessageFactoryImpi.instance.getMessageString(1,7),
                  MessageFactoryImpi.instance.getMessageString(1,4),
                  MessageFactoryImpi.instance.getMessageString(7,11),
                  MessageFactoryImpi.instance.getMessageString(7,5),
                  MessageFactoryImpi.instance.getMessageString(1,33),
                  MessageFactoryImpi.instance.getMessageString(1,35),
                  MessageFactoryImpi.instance.getMessageString(1,8),
                  MessageFactoryImpi.instance.getMessageString(1,3),
                  MessageFactoryImpi.instance.getMessageString(1,21),
                  MessageFactoryImpi.instance.getMessageString(1,19),
                  MessageFactoryImpi.instance.getMessageString(1,32),
        };
    }
    void reciveGameRoomInfoLater()
    {
        Method.isChongLian = false;
        GUArr = new GameUserInfo[Method.PeopleNum];
        roomID.text = Method.GameRoomID;
        List<GameUserInfo>  GJM = MaJong.MaJongManage.Instance().MaJong.GetGameUserInfo;
        for (int i = 0; i < GJM.Count; i++)
        {
            if (GJM[i].userId == Method.userID)
            {
                Method.Index = i;
                Method.userName = GJM[i].userName;
            }
        }
        for (int i = 0; i < GJM.Count; i++)
        {                 
            GUArr[GJM[i].locationIndex] = GJM[i];
            TouXiang[GJM[i].locationIndex].GetComponent<HeadInfo>().ShowAvatar(GJM[i]);           
            if (GJM[i].isready)
            {
                readynum++;
                if (Method.Index == i && i != 0)
                {
                    inviteFriend.GetComponent<Image>().color = new Color(110 / 255f, 110 / 255f, 110 / 255f, 1);
                    inviteFriend.GetComponent<Button>().enabled = false;
                    isReady = true;
                }
            }
        }
        Method.joinroonnum = GJM.Count;
        AddClick();
        Method.InitDistance(GJM);    

        GameObject obj = GameObject.Find("Canvas/addNode/WaitStartGame(Clone)/backgroundup/GridLayout/HeadPhoto3");
        GameObject obj_0 = GameObject.Find("Canvas/addNode/WaitStartGame(Clone)/backgroundup/GridLayout");

        if (Method.PeopleNum==4)
        {
            if (GJM.Count == 4)
            {
                ReadyBtnClick();
            }

            if (obj)
            {
                obj.SetActive(true);
            }
            if (obj_0)
            {
                obj_0.GetComponent<GridLayoutGroup>().spacing = new Vector2(0f, 0f);
            }
        }
        else
        {
            if (GJM.Count == 3)
            {
                ReadyBtnClick();
            }

            if (obj)
            {
                obj.SetActive(false);
            }
            if (obj_0)
            {
                obj_0.GetComponent<GridLayoutGroup>().spacing = new Vector2(75.6f, 0f);
            }
        }
    }       
    public override void OnMsg(string msg, com.guojin.core.io.message.Message data)
    {
        JoinUserInfo(msg, data);
        ReciveOtherOffLine(msg, data);
        DeleRoom(msg, data);
        ExitRoom(msg, data);
        ExitRoomGet(msg, data);
        DissolutionRoom(msg, data);
        reciveReadyMessage(msg, data);
        RecivePing(msg, data);
        ReciveGPSPosition(msg, data);
    }
    void ReciveGPSPosition(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 35))
        {
            ReqLocation Gps = (ReqLocation)message;
            if (!Gps.lontitude.Equals("-1.0"))
            {
                Method.GetPosition(Gps.locationIndex)[0] = Gps.lontitude;
                Method.GetPosition(Gps.locationIndex)[1] = Gps.latitude;
            }

        }
    }
    void RecivePing(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 33))
        {
            for (int i = 0; i < GUArr.Length; i++)
            {
                Method.GUIList.Add(GUArr[i]);
            }
            Debug.Log("UIState_LoadingPage__RecivePing");
            GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_LoadingPage);
        }
    }   
    void DissolutionRoom(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 3))
        {
            //SceneManager.LoadScene("GameHall");
            //SceneManager.LoadScene("GameHall");
            com.guojin.mj.net.message.game.GameDelRoom GDR = (com.guojin.mj.net.message.game.GameDelRoom)message;
            Debug.Log("++++++++++++++");
            Debug.Log(GDR.isStart);
            Debug.Log(GDR.isEnd);

            Debug.Log("++++++++++++++");
            if (!GDR.isEnd)
            {
                Debug.Log("YES,EXIT");
                ReturnGameHall();
                Destroy(gameObject);


            }
        }
    }
    void DeleRoom(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 21))
        {
            if (Method.JieSanPanl == null)
            {
                com.guojin.mj.net.message.game.VoteDelSelect VD = (com.guojin.mj.net.message.game.VoteDelSelect)message;
                GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/ExitRoonRequest"));
                temp.transform.SetParent(transform);
                temp.transform.localPosition = Vector3.zero;
                temp.transform.localScale = Vector3.one;
                Method.JieSanPanl = temp;
                temp.GetComponent<PlayerInfo>().CreatUserInfo1(VD.userName);
                Button[] btnArr = temp.GetComponentsInChildren<Button>();
                Debug.Log(btnArr[0].name);
                btnArr[0].onClick.AddListener(delegate
                {
                    VoteDelSelectRet VDS = VoteDelSelectRet.create(true, VD.userId);
                    SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(VDS));
                    Destroy(Method.JieSanPanl);
                });
                btnArr[1].onClick.AddListener(delegate
                {
                    VoteDelSelectRet VDSR = VoteDelSelectRet.create(false, VD.userId);
                    SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(VDSR));
                    Destroy(Method.JieSanPanl);
                });
                btnArr[2].onClick.AddListener(delegate
                {
                    Destroy(Method.JieSanPanl);
                });
            }

        }
    }
    void reciveReadyMessage(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 32))
        {
            ReadyType RT = (ReadyType)message;
            if (RT.index == Method.Index && Method.Index != 0)
            {
                inviteFriend.GetComponent<Image>().color = new Color(110 / 255f, 110 / 255f, 110 / 255f, 1);
                inviteFriend.GetComponent<Button>().enabled = false;
                isReady = true;
            }
            TouXiang[RT.index].GetComponent<HeadInfo>().SetReady();
            readynum = RT.readynum;
        }
    }
    void ExitRoomGet(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(7, 5))
        {
            //返回大厅
            ReturnGameHall();
            Destroy(gameObject);
        }
    }
    void ReciveOtherOffLine(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 19))
        {
            ///Method.isOtherOut = true;
            Method.diaoxianNum++;
            UserOffline UO = (UserOffline)message;
            TouXiang[UO.index].GetComponent<HeadInfo>().SetOffLine();
            GUArr[UO.index].online = false;
        }
    }
    /// <summary>
    /// 加入房间
    /// </summary>
    /// <param name="mes"></param>
    /// <param name="message"></param>
    void JoinUserInfo(string mes, com.guojin.core.io.message.Message message)
    {
        if (mes == MessageFactoryImpi.instance.getMessageString(1, 8))
        {

            GameUserInfo GUIO = (GameUserInfo)message;
            GUArr[GUIO.locationIndex] = GUIO;
            bool isduanxian = false;
            Debug.Log(Method.PeopleNum);           
            if (TouXiang[GUIO.locationIndex].GetComponent<HeadInfo>().Isoffline)
            {
                isduanxian = true;
            }
            if (isduanxian)
            {
                //Method.diaoxianNum--;
                TouXiang[GUIO.locationIndex].GetComponent<HeadInfo>().SetOnLine();
                GUArr[GUIO.locationIndex].online = true;
            }
            else
            {
                Method.joinroonnum++;
                if (Method.PeopleNum == 4)
                {
                    if (GUIO.locationIndex == 1)
                    {
                        Method.user01Distance = GUIO.user0Distance;
                        Method.username1 = GUIO.userName;

                    }
                    else if (GUIO.locationIndex == 2)
                    {
                        Method.user02Distance = GUIO.user0Distance;
                        Method.user12Distance = GUIO.user1Distance;
                        Method.username2 = GUIO.userName;
                    }
                    else if (GUIO.locationIndex == 3)
                    {

                        Method.user03Distance = GUIO.user0Distance;
                        Method.user13Distance = GUIO.user1Distance;
                        Method.user23Distance = GUIO.user2Distance;
                        Method.username3 = GUIO.userName;

                    }
                    if (!GUIO.jing.Equals("-1.0"))
                    {
                        Method.GetPosition(GUIO.locationIndex)[0] = GUIO.jing;
                        Method.GetPosition(GUIO.locationIndex)[1] = GUIO.wei;
                    }
                    if (Method.joinroonnum == 4)
                    {
                        ReadyBtnClick();
                    }
                    if (GUIO.isready)
                    {
                        readynum++;
                    }

                }
                else
                {

                    if (GUIO.locationIndex == 1)
                    {
                        Method.user01Distance = GUIO.user0Distance;
                        Method.username1 = GUIO.userName;
                    }
                    else if (GUIO.locationIndex == 2)
                    {

                        Method.user02Distance = GUIO.user0Distance;
                        Method.user12Distance = GUIO.user1Distance;
                        Method.username2 = GUIO.userName;
                        ReadyBtnClick();
                    }
                    if (!GUIO.jing.Equals("-1.0"))
                    {
                        Method.GetPosition(GUIO.locationIndex)[0] = GUIO.jing;
                        Method.GetPosition(GUIO.locationIndex)[1] = GUIO.wei;
                    }
                    if (GUIO.isready)
                    {
                        readynum++;
                    }
                    if (Method.joinroonnum == 3)
                    {
                        ReadyBtnClick();
                    }
                }
                //Method.GUIList.Add(GUIO);               
                TouXiang[GUIO.locationIndex].GetComponent<HeadInfo>().ShowAvatar(GUIO);
            }

            SendPlace();
        }
    }

    bool isReady = false;
    void ReadyBtnClick()
    {
        inviteFriend.GetComponent<Button>().onClick.RemoveAllListeners();
        if (Method.Index == 0)
        {
            //ReadyBtn.GetComponent<Button>().enabled = false;
            inviteFriend.GetComponent<Image>().sprite = PPTextureManage.getInstance().LoadAtlasSprite("Atlas/CreatRoom", "begingame_btn");//开始
            inviteFriend.GetComponent<Button>().onClick.AddListener(delegate
            {
                if (Method.PeopleNum == 4)
                {
                    if (readynum == 4)
                    {
                        // 发送开始游戏
                        StartGameFW st = new StartGameFW();
                        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(st));
                    }
                    else
                    {
                        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/JoinRoomDef"));
                        temp.transform.SetParent(transform);
                        temp.transform.localPosition = Vector3.zero;
                        temp.transform.localScale = Vector3.one;
                        temp.GetComponentInChildren<RuleLayer>().ShowText("玩家尚未全部准备！");
                    }
                }
                else
                {
                    if (readynum == 3)
                    {
                        // 发送开始游戏
                        StartGameFW st = new StartGameFW();
                        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(st));
                    }
                    else
                    {
                        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/JoinRoomDef"));
                        temp.transform.SetParent(transform);
                        temp.transform.localPosition = Vector3.zero;
                        temp.transform.localScale = Vector3.one;
                        temp.GetComponentInChildren<RuleLayer>().ShowText("玩家尚未全部准备！");
                    }
                }
            });

        }
        else
        {
            inviteFriend.GetComponent<Image>().sprite = PPTextureManage.getInstance().LoadAtlasSprite("Atlas/CreatRoom", "Ready_btn");//开始
            inviteFriend.GetComponent<Button>().onClick.AddListener(delegate
            {
                isReady = true;
                ReadyType readytype = ReadyType.create(true, Method.Index, 0);
                SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(readytype));

            });
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
            GameExitUser Gu = (GameExitUser)message;
            for (int i = 0; i < TouXiang.Length; i++)
            {
                if (TouXiang[i].GetComponent<HeadInfo>().id == Gu.userId)
                {
                    TouXiang[i].GetComponent<HeadInfo>().ExitRoom();
                    Method.GetPosition(i)[0] = "-1.0";
                    Method.GetPosition(i)[1] = "-1.0";
                }

            }

            Method.joinroonnum--;
            readynum--;
            ShowInviteFriendBtn();           

        }
    }
    void ShowInviteFriendBtn()
    {
        if (Method.PeopleNum == 4)
        {
            if (Method.Index == 0 && readynum != 4)
            {
                inviteFriend.GetComponent<Button>().onClick.RemoveAllListeners();
                inviteFriend.GetComponent<Image>().sprite = PPTextureManage.getInstance().LoadAtlasSprite("Atlas/CreatRoom", "askfrends_btn");//开始
                inviteFriend.GetComponent<Button>().onClick.AddListener(delegate
                {

                    //邀请好友
                    LoginAndShare.Controller.OnClickShareInfo();
                });
            }
            else if (!isReady)
            {
                inviteFriend.GetComponent<Button>().onClick.RemoveAllListeners();
                inviteFriend.GetComponent<Image>().sprite = PPTextureManage.getInstance().LoadAtlasSprite("Atlas/CreatRoom", "askfrends_btn");//开始
                inviteFriend.GetComponent<Button>().onClick.AddListener(delegate
                {
                    //邀请好友
                    LoginAndShare.Controller.OnClickShareInfo();
                });
            }
        }
        else
        {
            if (Method.Index == 0 && readynum != 3)
            {
                inviteFriend.GetComponent<Button>().onClick.RemoveAllListeners();
                inviteFriend.GetComponent<Image>().sprite = PPTextureManage.getInstance().LoadAtlasSprite("Atlas/CreatRoom", "askfrends_btn");//开始
                inviteFriend.GetComponent<Button>().onClick.AddListener(delegate
                {

                    //邀请好友
                    LoginAndShare.Controller.OnClickShareInfo();
                });
            }
            else if (!isReady)
            {
                inviteFriend.GetComponent<Button>().onClick.RemoveAllListeners();
                inviteFriend.GetComponent<Image>().sprite = PPTextureManage.getInstance().LoadAtlasSprite("Atlas/CreatRoom", "askfrends_btn");//开始
                inviteFriend.GetComponent<Button>().onClick.AddListener(delegate
                {
                    //邀请好友
                    LoginAndShare.Controller.OnClickShareInfo();
                });
            }
        }

    }
    float timen = 0;
    int timenum = 10;
    bool isrun = false;
    void ReturnGameHall()
    {
        if (!GameObject.Find("MJCreatRoomPage(Clone)"))
        {
            UIManager.ChangeUI(UIManager.PageState.MJCreatRoomPage, (GameObject obj) =>
            {
                obj.GetComponent<MJCreatRoomPageEvent>().InformationSetting();
            });
        }
    }
    void AddClick()
    {

        if (Method.Index == 0)
        {
            dissolveAndExitBtn.GetComponent<Button>().onClick.AddListener(delegate
            {
                GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/DeleRoomP"));
                temp.transform.SetParent(transform);
                temp.transform.localScale = Vector3.one;
                temp.transform.localPosition = Vector3.zero;
                Button[] btnArr = temp.GetComponentsInChildren<Button>();
                btnArr[0].onClick.AddListener(delegate
                {
                    VoteDelStart VDS = new VoteDelStart();
                    SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(VDS));
                    Destroy(temp);
                });
                btnArr[1].onClick.AddListener(delegate
                {
                    Destroy(temp);
                });
            });
        }
        else
        {
            dissolveAndExitBtn.GetComponent<Image>().sprite = PPTextureManage.getInstance().LoadAtlasSprite("Atlas/CreatRoom_1", "exitRoom_btn");
            dissolveAndExitBtn.GetComponent<Button>().onClick.AddListener(delegate
            {
                ExitRoom exitroom = new ExitRoom();
                SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(exitroom));
            });

        }
        //代理退出房间的功能
        //if (Method.Index == 0 && Method.level != 0)
        //{
        //    ExitBtn.SetActive(true);
        //    ExitBtn.GetComponent<Button>().onClick.AddListener(delegate
        //    {
        //        ExitRoom exitroom = new ExitRoom();
        //        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(exitroom));
        //    });
        //}


    }
    //测量距离----------测量距离------------测量距离------------测量距离--------测量距离---------------测量距离------------测量距离---------
    //地球半径，单位米
    private const double EARTH_RADIUS = 6378137;

    /// <summary>
    /// 计算两点位置的距离，返回两点的距离，单位：米
    /// 该公式为GOOGLE提供，误差小于0.2米
    /// </summary>
    /// <param name="lng1">第一点经度</param>
    /// <param name="lat1">第一点纬度</param>        
    /// <param name="lng2">第二点经度</param>
    /// <param name="lat2">第二点纬度</param>
    /// <returns></returns>
    public static double GetDistance(double lng1, double lat1, double lng2, double lat2)
    {
        double radLat1 = Rad(lat1);
        double radLng1 = Rad(lng1);
        double radLat2 = Rad(lat2);
        double radLng2 = Rad(lng2);
        double a = radLat1 - radLat2;
        double b = radLng1 - radLng2;
        double result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2))) * EARTH_RADIUS;
        return result;
    }

    /// <summary>
    /// 经纬度转化成弧度
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    private static double Rad(double d)
    {
        return (double)d * Math.PI / 180d;
    }   
   
}
