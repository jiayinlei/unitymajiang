using UnityEngine;

using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using GameCore;
using System;
using System.Linq;
using com.guojin.mj.net.message.login;

public class Method : MonoBehaviour
{
    public static List<com.guojin.core.io.message.Message> messagelist=new List<com.guojin.core.io.message.Message>();//接收加载过程中可能漏掉的消息

    public static bool isPlayerBack=false;
    public static bool CanAddMessage=false;//是否可以开始往messagelist存消息了
    public static List<int> tingArr = new List<int>();
    public static bool IsSeverError = false;
    public static bool isReciveReady = false;
    public static List<GameObject> touxiangL = new List<GameObject>();
    public static GameObject lastm;
    public static string MJType =""; //0郑州 1 固始
    public static int DuanMan = -1;//0饼 1条 2万
    public static bool isDuanwang = false;
    public static com.guojin.mj.net.message.game.ShowPaoRet SPR;
    public static com.guojin.mj.net.message.game.OperationFaPai OFP;
    public static com.guojin.mj.net.message.game.MajiangChapterMsg MM;
    public static com.guojin.mj.net.message.game.OperationCPGH CPGH;
    public static com.guojin.mj.net.message.game.GameChapterEnd END;
    public static bool isFirJu = true;
    public static bool isHor = false;//判断是否为麻将战斗界面
    public static GameObject hungameobj;//混牌
    public static bool isPutong = false;
    public static bool isOtherOut = false;//其他玩家掉线
    public static int recivePaiId = -1;//接到的牌的ID
    public static bool isChongLian;//判断是否重连
    public static int State;//游戏状态
    public static GameObject readyGame;
    public static int diaoxianNum = 0;
    public static int fangXiangNum = -1;
    public static int level = 0;//是否为代理 0 的时候是普通用户 其他为代理
    public static int joinroonnum = 0;//加入房间的人数
    public static List<payPrice>  listPrice;
    public static string user01Distance;
    public static string user02Distance;
    public static string user03Distance;
    public static string user12Distance;
    public static string user13Distance;
    public static string user23Distance;
    public static string username0;
    public static string username1;
    public static string username2;
    public static string username3;
    public static string[] GPSPosition0 = new string[2];
    public static string[] GPSPosition1 = new string[2];
    public static string[] GPSPosition2 = new string[2];
    public static string[] GPSPosition3 = new string[2];

    public static int userID;
    public static string userName;
    public static string maJongJuNum;
    public static com.guojin.core.io.message.Message message;

    public static com.guojin.mj.net.message.game.GameRoomInfo GRI;
    public static com.guojin.mj.net.message.game.GameRoomInfoGuShi GRIGuShi;
    public static com.guojin.mj.net.message.game.ShowSelectZuiTypeRet SSZ;
    public static string GameRoomID;//房间id
    public static string joinRoomID;//加入的房间id
    public static bool isCreater = false;//是否是创建者
    public static GameObject ob;//选跑的父对象
    public static int IPaoTimes;//选择跑的次数
    public static Text ShengyuMaJong;//剩余麻将的张数
    public static GameObject zhuang;//克隆出来的庄
    public static bool isUpdate;
    public static GameObject TingMaJong;//停牌的麻将
    public static GameObject InviteFriends;//邀请好友按钮
    public static GameObject StartGameBtn;
    public static GameObject JieSanPanl;
    public static int huier = -1;//混儿牌的id
    public static bool isRun = false;
    public static Text daojishi;
    public static GameObject ooo;
    public static bool isChoicePao;//是否选过跑
    public static int times = 0;//已跑的人数；
    public static Sprite[] MjNameSp;//图片精灵
    public static Sprite[] mjdise;//麻将底色 
    public static int choiceHuaSe;//选择的花色的
    public static Sprite[] fangxiang;//碰扛的人的方向显示
    public static bool isMine;//是否该自己出牌
    public static Transform parent;//手牌的父对象
    public static GameObject clon;//要克隆的牌 objList[0];
    public static GameObject go;//接收服务器发的牌
    public static int goId;//接收发牌的id 发完牌之后把值赋给goId
    public static Transform goParent;//接收牌的父对象
    public static List<Transform> tranList = new List<Transform>();//0 【0、1、2】0位置碰牌、手牌、出牌的父对象 ；【3、4、5】 1位置碰牌、手牌、出牌的父对象；【6、7、8】2位置碰牌、手牌、出牌的父对象；【9、10、11】3位置碰牌、手牌、出牌的父对象；[12,13,14]123玩家接到的牌
    public static List<GameObject> objList = new List<GameObject>();//【0，1】0位置的手牌、出的牌；【2，3】1位置的手牌、出的牌；【4，5】2位置的手牌、出的牌；【6，7】3位置的手牌、出的牌【8、9、10、11】0、1、2、3位置的碰牌；【12、13、14、15】0、1、2、3位置的明扛；【16、17、18、19】0、1、2、3位置的暗扛；

    public static int PeopleNum;
    public static List<int> intlist = new List<int>();//接收到的手牌
    public static bool isPeng;//是不是为碰
    public static bool OtherCPGH = false;//其他用户是否吃碰扛胡；
    public static int PGH;//34碰35扛37胡
    public static GameObject lastMajong;//最后出的麻将
    public static GameObject guangbiao;
    public static Vector3[] VectARR = { new Vector3(0, -92, 0), new Vector3(200, 0, 0), new Vector3(0, 109, 0), new Vector3(-166, 0, 0) };
    public static Vector3[] VectArrPosition = { new Vector3(0, -50, 0), new Vector3(70, 0, 0), new Vector3(0, 50, 0), new Vector3(-70, 0, 0) };
    public static int[] intArr = new int[4];
    public static int Index;//玩家座的位置 东0 西2 南1 北3
    public static int timeNum = 15;
    public static GameObject positionStar;//显示到哪位玩家出牌  
    public static int zhaungweizhi;
    public static GameObject ErrorTiShi;
    //public static bool isErrorTiShi = false;
    public static int Score = 0;//分数
    public static List<int> ScoreList = new List<int>();
    public static int[] IntARR;
    public static GameObject SettingBTN;
    public static Transform OutPai0;
    public static GameObject ChoicePaoObj;
    public static Queue messageQueue;//消息的队列
    public static GameObject StateText;
    public static GameObject Dingque;
    public static bool isFirstOutPai = false;
    public static int duanmenindex;
   


    public static List<com.guojin.mj.net.message.game.GameUserInfo> GUIList = new List<com.guojin.mj.net.message.game.GameUserInfo>();
    /// <summary>
    /// 接收到信息之后克隆次数到前端显示
    /// </summary>
    /// <param name="times"> 跑的次数</param>
    /// <param name="parent"> 设置父对象</param>
    /// <param name="weizhi"> 局部位置</param>
    public static void SwitchM(int times, Transform parent, int weizhi)
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("MjPrefab/Run"));
        if (weizhi == Index)
        {
            ChoicePaoObj = go;
        }
        switch (times)
        {
            case 0:
                go.GetComponent<Image>().sprite = Resources.Load<Sprite>("MJSprite/MJDipai/bupao");
                go.transform.SetParent(parent);
                go.transform.localPosition = VectArrPosition[intArr[weizhi]];
                go.transform.localScale = Vector3.one;
                //ChoicePao(weizhi);
                break;
            case 1:
                go.GetComponent<Image>().sprite = Resources.Load<Sprite>("MJSprite/MJDipai/paoyi");
                go.transform.SetParent(parent);
                go.transform.localPosition = VectArrPosition[intArr[weizhi]];
                go.transform.localScale = Vector3.one;
                //ChoicePao(weizhi);
                break;
            case 2:
                go.GetComponent<Image>().sprite = Resources.Load<Sprite>("MJSprite/MJDipai/paoer");
                go.transform.SetParent(parent);
                go.transform.localPosition = VectArrPosition[intArr[weizhi]];
                go.transform.localScale = Vector3.one;
                //ChoicePao(weizhi);
                break;
            case 3:
                go.GetComponent<Image>().sprite = Resources.Load<Sprite>("MJSprite/MJDipai/paosan");
                go.transform.SetParent(parent);
                go.transform.localPosition = VectArrPosition[intArr[weizhi]];
                go.transform.localScale = Vector3.one;
                //ChoicePao(weizhi);
                break;
        }
    }


    private const double EARTH_RADIUS = 6371010;
    /// <summary>
    /// 计算两点位置的距离，返回两点的距离，单位：米
    /// 该公式为GOOGLE提供，误差小于0.2米
    /// </summary>
    /// <param name="lng1">第一点经度</param>
    /// <param name="lat1">第一点纬度</param>        
    /// <param name="lng2">第二点经度</param>
    /// <param name="lat2">第二点纬度</param>
    /// 34.65 33.22  18.99 17.11
    /// <returns></returns>
    public static string GetDistance(string[] pos1, string[] pos2)
    {
        if (pos1 == null || pos2 == null || pos1[0] == null || pos2[0] == null)
        {
            return "位置未知";
        }
        if (!pos1[0].Equals("-1.0") && !pos2[0].Equals("-1.0"))
        {
            double radLat1 = Rad(double.Parse(pos1[0]));
            double radLng1 = Rad(double.Parse(pos1[1]));
            double radLat2 = Rad(double.Parse(pos2[0]));
            double radLng2 = Rad(double.Parse(pos2[1]));
            double a = radLat1 - radLat2;
            double b = radLng1 - radLng2;
            double result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2))) * EARTH_RADIUS;

            if (result > 1000)
            {
                double resultG = result / 1000;
                return Convert.ToDouble(resultG).ToString("0.00") + "公里";
            }
            else
            {
                return Convert.ToDouble(result).ToString("0.00") + "米";
            }
        }
        else
        {
            return "位置未知";
        }



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
    public static void GPS(int weizhi)
    {
        switch (weizhi)
        {            
            case 0:
                user12Distance = GetDistance(GPSPosition1, GPSPosition2);
                user23Distance = GetDistance(GPSPosition2, GPSPosition3);
                user13Distance = GetDistance(GPSPosition1, GPSPosition3);
                break;
            case 1:
                user02Distance = GetDistance(GPSPosition0, GPSPosition2);
                user23Distance = GetDistance(GPSPosition2, GPSPosition3);
                user03Distance = GetDistance(GPSPosition0, GPSPosition3);
                break;
            case 2:
                user01Distance = GetDistance(GPSPosition0, GPSPosition1);
                user13Distance = GetDistance(GPSPosition1, GPSPosition3);
                user03Distance = GetDistance(GPSPosition0, GPSPosition3);
                break;
            case 3:
                user01Distance = GetDistance(GPSPosition0, GPSPosition1);
                user12Distance = GetDistance(GPSPosition1, GPSPosition2);
                user02Distance = GetDistance(GPSPosition0, GPSPosition2);
                break;

        }
    }

    public static void PaoOther(int weizhi)
    {
        ChoicePao(weizhi);
    }
    public static void ShowError()
    {
        string name;
        if (isHor)
        {
            name = "Notice";

        }
        else
        {
            name = "JoinRoomDef";
        }
        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/" + name));
        temp.transform.SetParent(GameObject.Find("Canvas").transform);
        temp.transform.localPosition = Vector3.zero;
        temp.transform.localScale = Vector3.one;
        temp.GetComponentInChildren<RuleLayer>().ShowText("服务器断开!");
    }
    public static void DestoryAllChildren()
    {
        for (int i = 0; i < 12; i++)
        {
            DestoryAll(tranList[i]);
        }
        if (go != null)
        {
            Destroy(go);
        }
        Destroy(TingMaJong);
        Destroy(zhuang);
        //if (hungameobj!=null)
        //{
        //    Destroy(hungameobj);
        //}    
        VoiceManger.Instance.OnLoadGuangBiao();
        isRun = false;
        isMine = false;
        intlist.Clear();
    }
    public static void InitGame()
    {
        Destroy(GameObject.Find("ChoicePlayeWnd_Img"));
        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/ChoicePlayeWnd_Img"));
        temp.transform.SetParent(GameObject.Find("Canvas").transform);
        temp.name = "ChoicePlayeWnd_Img";
        temp.transform.localScale = Vector3.one;
        temp.transform.localPosition = Vector3.zero;
    }
    /// <summary>
    /// 显示邀请好友和退出游戏按钮
    /// </summary>
    public static void ShowInviteFriends()
    {
        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/ShareAndExit"));
        temp.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
        temp.name = "ShareAndExit";
        temp.transform.localScale = Vector3.one;
        temp.transform.localPosition = Vector3.zero;
        InviteFriends = temp;
    }

    /// <summary>
    ///显示一位玩家的牌的信息 
    /// </summary>D:\MyProject\GjMaJiang\Assets\
    /// <param name="up"></param>
    /// <param name="weizhi"></param>
    /// <param name="parent1"></param>
    public static void ShowUser(com.guojin.mj.net.message.game.UserPlaceMsg up, int weizhi, Transform parent1,int state)
    {

        if (intArr[weizhi] == 0 && up.shouPai.Length != 0)
        {
            intlist.Clear();
            for (int i = 0; i < up.shouPai.Length; i++)
            {
                intlist.Add(up.shouPai[i]);
                Debug.Log("+++++++++++++" + up.shouPai[i]);
            }
            Debug.Log("+++++++++++++" + up.shouPai.Length);
            if (recivePaiId != -1)
            {
                intlist.Remove(recivePaiId);
            }
            intlist.Sort();
            for (int i = intlist.Count - 1; i >= 0; i--)
            {
                GameObject temp = Instantiate(clon);
                temp.GetComponentInChildren<MJInfo>().CreatMJInfo(intlist[i], 0, MjNameSp[intlist[i]]);
                temp.transform.SetParent(parent);
                temp.transform.localPosition = Vector3.zero;
                temp.transform.localScale = Vector3.one;
                listMaJong.Remove(intlist[i]);//新加
            }
        }
        else
        {
            // up.shouPaiLen = 13;
            if (up.shouPaiLen != 0)
            {
                CloneShouPai(weizhi, up.shouPaiLen);
            }
        }
        if (up.shouPai != null && up.shouPai.Length != 0 && GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback)
        {
            GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().HandCard[intArr[weizhi]] = new List<int>(up.shouPai);
            GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().ShowHandCard(intArr[weizhi]);

        }
        if (up.peng != null)
        {
            for (int i = 0; i < up.peng.Length; i++)
            {
                GameObject temp = Instantiate(objList[8 + intArr[weizhi]]);
                temp.GetComponent<OpenAndClose>().Id = up.peng[i];
                MaJongOutInfo[] mjArr = temp.GetComponentsInChildren<MaJongOutInfo>();
                for (int j = 0; j < mjArr.Length; j++)
                {
                    if (j == 1)
                    {
                        mjArr[j].ShowOnePai(up.peng[i], MjNameSp[up.peng[i]], returnFangxiang(weizhi, up.pengIndex[i]));
                    }
                    else
                    {
                        mjArr[j].IsHun(up.peng[i], MjNameSp[up.peng[i]]);
                    }
                    listMaJong.Remove(up.peng[i]);//新加
                }
                temp.transform.SetParent(tranList[3 * intArr[weizhi]]);
                temp.transform.localScale = Vector3.one;
                temp.transform.localPosition = Vector3.zero;
                if (intArr[weizhi] == 1 || intArr[weizhi] == 3)
                {
                    temp.transform.Rotate(0, 0, -270, Space.Self);
                }
            }
        }
        if (up.anGang != null)
        {
            for (int i = 0; i < up.anGang.Length; i++)
            {
                GameObject temp = Instantiate(objList[16 + intArr[weizhi]]);
                temp.transform.SetParent(tranList[3 * intArr[weizhi]]);
                temp.transform.localScale = Vector3.one;
                temp.transform.localPosition = Vector3.zero;
                if (intArr[weizhi] == 0)
                {
                    temp.GetComponentInChildren<MaJongOutInfo>().IsHun(up.anGang[i], MjNameSp[up.anGang[i]]);
                    RemoveMaJong(4, up.anGang[i]);//新加 移除扛
                }
                if (intArr[weizhi] == 1 || intArr[weizhi] == 3)
                {
                    temp.transform.Rotate(0, 0, -270, Space.Self);
                }
            }
        }
        if (up.daMingGang != null)
        {
            for (int i = 0; i < up.daMingGang.Length; i++)
            {
                GameObject temp = Instantiate(objList[12 + intArr[weizhi]]);
                MaJongOutInfo[] mjArr1 = temp.GetComponentsInChildren<MaJongOutInfo>();
                for (int j = 0; j < mjArr1.Length; j++)
                {
                    if (j == 3)
                    {
                        mjArr1[j].ShowOnePai(up.daMingGang[i], MjNameSp[up.daMingGang[i]], returnFangxiang(weizhi, up.daMingGangIndex[i]));
                    }
                    else
                    {
                        mjArr1[j].IsHun(up.daMingGang[i], MjNameSp[up.daMingGang[i]]);
                    }
                    RemoveMaJong(4, up.daMingGang[i]);//新加
                }
                temp.transform.SetParent(tranList[3 * intArr[weizhi]]);
                temp.transform.localScale = Vector3.one;
                temp.transform.localPosition = Vector3.zero;
                if (intArr[weizhi] == 1 || intArr[weizhi] == 3)
                {
                    temp.transform.Rotate(0, 0, -270, Space.Self);
                }
            }
        }
        if (up.xiaoMingGang != null)
        {
            for (int i = 0; i < up.xiaoMingGang.Length; i++)
            {
                GameObject temp = Instantiate(objList[12 + intArr[weizhi]]);
                MaJongOutInfo[] mjArr1 = temp.GetComponentsInChildren<MaJongOutInfo>();
                for (int j = 0; j < mjArr1.Length; j++)
                {
                    if (j == 3)
                    {
                        mjArr1[j].ShowOnePai(up.xiaoMingGang[i], MjNameSp[up.xiaoMingGang[i]], returnFangxiang(weizhi, up.xiaoMingGangIndex[i]));
                    }
                    else
                    {
                        mjArr1[j].IsHun(up.xiaoMingGang[i], MjNameSp[up.xiaoMingGang[i]]);
                    }
                    RemoveMaJong(4, up.xiaoMingGang[i]);//新加
                }
                temp.transform.SetParent(tranList[3 * intArr[weizhi]]);
                temp.transform.localScale = Vector3.one;
                temp.transform.localPosition = Vector3.zero;
                if (intArr[weizhi] == 1 || intArr[weizhi] == 3)
                {
                    temp.transform.Rotate(0, 0, -270, Space.Self);
                }
            }
        }

        if (up.xuanpaocount != -1)
        {
            SwitchM(up.xuanpaocount, parent1, weizhi);
        }

        if (up.outPai != null)
        {
            if (state==4&& intArr[weizhi]!=0)
            {
                return;
            }
            for (int i = 0; i < up.outPai.Length; i++)
            {
                if (intArr[weizhi] == 1 || intArr[weizhi] == 3)
                {
                    CloneOne(up.outPai[i], weizhi, -270);
                }
                else
                {
                    CloneOne(up.outPai[i], weizhi, 0);
                }
                RemoveMaJong(1, up.outPai[i]);//新加
            }
        }
       
        //if (MJType==1)
        //{
        //    if (up.zuiIndex != null)
        //    {
        //        PlayerData.PaoZuiDictionary.Add(weizhi, up.zuiIndex);
        //    }           
        //}
    }



    /// <summary>
    /// 克隆一张牌到桌面
    /// </summary>
    /// <param name="pai"></param>
    /// <param name="weizhi"></param>
    public static void CloneOne(int pai, int weizhi, int ration)
    {
        GameObject temp = Instantiate(objList[intArr[weizhi] * 2 + 1]);
        temp.GetComponent<MaJongOutInfo>().IsHun(pai, MjNameSp[pai]);
        temp.transform.SetParent(tranList[intArr[weizhi] * 3 + 2]);
        temp.transform.localPosition = Vector3.zero;
        temp.transform.Rotate(0, 0, ration, Space.Self);
        temp.transform.localScale = Vector3.one;
    }

    /// <summary>
    /// 克隆玩家的手牌
    /// </summary>
    /// <param name="weizhi"></param>
    /// <param name="zhang"></param>
    public static void CloneShouPai(int weizhi, int zhang)
    {
        switch (intArr[weizhi])
        {
            case 1:
                for (int i = 0; i < zhang; i++)
                {
                    GameObject temp = Instantiate(objList[2]);
                    temp.transform.SetParent(tranList[4]);
                    temp.transform.Rotate(0, 0, -270, Space.Self);
                    temp.transform.localPosition = Vector3.zero;
                    temp.transform.localScale = Vector3.one;
                }
                break;
            case 2:
                for (int i = 0; i < zhang; i++)
                {
                    GameObject temp = Instantiate(objList[4]);
                    temp.transform.SetParent(tranList[7]);
                    temp.transform.localPosition = Vector3.zero;
                    temp.transform.localScale = Vector3.one;
                }
                break;
            case 3:
                for (int i = 0; i < zhang; i++)
                {
                    GameObject temp = Instantiate(objList[6]);
                    temp.transform.SetParent(tranList[10]);
                    temp.transform.Rotate(0, 0, -270, Space.Self);
                    temp.transform.localPosition = Vector3.zero;
                    temp.transform.localScale = Vector3.one;
                }
                break;
        }
    }
    /// <summary>
    /// 销毁 开始游戏按钮和等待房主开始游戏显示
    /// </summary>
    public static void StartGameInit()
    {
        if (StartGameBtn != null)
        {
            Destroy(StartGameBtn);
            DestoryAllChildren();
        }

    }

    /// <summary>
    /// 重连只后显示开始按钮或者等待。。
    /// </summary>
    /// <param name="istart"></param>
    public static void ShowStatr(bool istart)
    {
        if (!istart)
        {
            ShowStartGame();
        }
    }
    public static void showUserZhuang(int weizhi)
    {
        if (intArr[weizhi] == 0)
        {
            GameObject.Find("userInfo").GetComponent<PlayerInfo>().showZhuang();
        }
        else
        {
            GameObject.Find("userInfo").GetComponent<PlayerInfo>().NoshowZhuang();
        }
    }
    public static void AddMessage(com.guojin.core.io.message.Message message)
    {
        messageQueue.Enqueue(message);
    }

    public static void showStartGameCollMetho(string roomid)
    {
        GameObject.Find("ChoicePlayeWnd_Img").GetComponent<StartGameController>().ShowGameRoomInfo(roomid);
    }
    public static void showStartGameJu()
    {
        GameObject.Find("ChoicePlayeWnd_Img").GetComponent<StartGameController>().showJUnum();
    }
    public static void DesStartGameCollMetho()
    {
        GameObject.Find("ChoicePlayeWnd_Img").GetComponent<StartGameController>().destroyGameOBJ();
    }


    public static void InitDistance(List<com.guojin.mj.net.message.game.GameUserInfo> GUIOList)
    {
        if (GUIOList.Count == 1)
        {
            username0 = GUIOList[0].userName;
        }
        else if (GUIOList.Count == 2)
        {
            username0 = GUIOList[0].userName;
            username1 = GUIOList[1].userName;
            user01Distance = GUIOList[1].user0Distance;

        }
        else if (GUIOList.Count == 3)
        {
            username0 = GUIOList[0].userName;
            username1 = GUIOList[1].userName;
            username2 = GUIOList[2].userName;
            user01Distance = GUIOList[1].user0Distance;
            user02Distance = GUIOList[2].user0Distance;
            user12Distance = GUIOList[2].user1Distance;
        }
        else if (GUIOList.Count == 4)
        {
            username0 = GUIOList[0].userName;
            username1 = GUIOList[1].userName;
            username2 = GUIOList[2].userName;
            username3 = GUIOList[3].userName;

            user01Distance = GUIOList[1].user0Distance;
            user02Distance = GUIOList[2].user0Distance;
            user12Distance = GUIOList[2].user1Distance;
            user03Distance = GUIOList[3].user0Distance;
            user13Distance = GUIOList[3].user1Distance;
            user23Distance = GUIOList[3].user2Distance;
        }

        for (int i = 0; i < GUIOList.Count; i++)
        {
            if (!GUIOList[i].jing.Equals("-1.0"))
            {
                GetPosition(i)[0] = GUIOList[i].jing;
                GetPosition(i)[1] = GUIOList[i].wei;
            }
        }

    }

    public static string[] GetPosition(int weizhi)
    {

        if (weizhi == 0)
        {
            return GPSPosition0;
        }
        else if (weizhi == 1)
        {
            return GPSPosition1;
        }
        else if (weizhi == 2)
        {
            return GPSPosition2;
        }
        else
        {
            return GPSPosition3;
        }
    } 
    public static void ReciveMessageVertical()
    {        
        showStartGameCollMetho(GameRoomID);    
        SetPlayerPosition();
        ShowRoomID(GameRoomID);
        for (int i = 0; i < GUIList.Count; i++)
        {
            StartShowHeadImage(GUIList[i]);
            IntARR[i] = GUIList[i].score;
        }
        for (int i = 0; i < touxiangL.Count; i++)
        {
            if (Index != i)
            {
                touxiangL[i].GetComponent<PlayerInfo>().showHold();
            }
        }
    }

    /// <summary>
    /// 显示玩家已选跑
    /// </summary>
    /// <param name="weizhi">玩家的位置0 1 2 3</param>
    public static void ChoicePao(int weizhi)
    {
        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/xuanpao"));
        temp.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
        temp.transform.localScale = Vector3.one;
        temp.transform.localPosition = VectARR[intArr[weizhi]];
    }
    /// <summary>
    /// 初始化玩家的位置
    /// </summary>
    public static void SetPlayerPosition()
    {
        if (PeopleNum == 4)
        {
            IntARR = new int[4] { 0, 0, 0, 0 };
            int[] ARR0 = { 0, 1, 2, 3 };
            int[] ARR1 = { 3, 0, 1, 2 };
            int[] ARR2 = { 2, 3, 0, 1 };
            int[] ARR3 = { 1, 2, 3, 0 };
            switch (Index)
            {
                case 0:
                    intArr = ARR0;
                    break;
                case 1:
                    intArr = ARR1;
                    break;
                case 2:
                    intArr = ARR2;
                    break;
                case 3:
                    intArr = ARR3;
                    break;
            }

        }
        else
        {
            IntARR = new int[3] { 0, 0, 0 };
            ThreePeople();
        }
    }
    public static void ThreePeople()
    {
        if (PeopleNum == 3)
        {
            int[] ARR0 = { 0, 1, 3 };
            int[] ARR1 = { 3, 0, 1 };
            int[] ARR3 = { 1, 3, 0 };
            switch (Index)
            {
                case 0:
                    intArr = ARR0;
                    break;
                case 1:
                    intArr = ARR1;
                    break;
                case 2:
                    intArr = ARR3;
                    break;

            }
        }

    }
    /// <summary>
    /// 显示房间信息
    /// </summary>
    /// <param name="RoomID"></param>
    /// <param name="Junum"></param>
    /// <param name="maJongNum"></param>
    public static void ShowRoomID(string RoomID)
    {
        Text RoomText = GameObject.Find("RoomID").GetComponent<Text>();
        RoomText.text = "房号：" + RoomID;
        if (!GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback)
        {
            GameRoomID = RoomID;
        }
        //GameRoomID = RoomID;
    }
    /// <summary>
    /// 显示麻将信息
    /// </summary>
    /// <param name="Junum"></param>
    /// <param name="maJongNum"></param>
    public static void ShowMaJongInfo(int Junum, int maJongNum)
    {
        Text JuText = GameObject.Find("JuNum").GetComponent<Text>();

        //Text MaJongNum = GameObject.Find("MaJongNun").GetComponent<Text>();
        if (GameObject.Find("MaJongNun") == null)
        {
            GameObject temp1 = Instantiate(Resources.Load<GameObject>("MjPrefab/Center_Img"));
            Method.ob = temp1;
            temp1.name = "Center_Img";
            temp1.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
            temp1.transform.SetSiblingIndex(0);
            temp1.transform.localScale = Vector3.one;
            temp1.transform.localPosition = new Vector3(0, 58, 0);
        }
        ShengyuMaJong = GameObject.Find("MaJongNun").GetComponent<Text>();
        JuText.text = (Junum + 1).ToString();
        Debug.Log("1231231231" + Junum);

        if (Junum > 0)
        {
            isFirJu = false;
        }
        else
        {
            isFirJu = true;
        }
        Debug.Log("1231231231" + isFirJu);
        ShengyuMaJong.text = maJongNum.ToString();

    }
    /// <summary>
    /// 开局 克隆显示头像 
    /// </summary>
    public static void StartShowHeadImage(com.guojin.mj.net.message.game.GameUserInfo GUIO)
    {

        if (intArr[GUIO.locationIndex] == 0)
        {
            GameObject temp1 = Instantiate(Resources.Load<GameObject>("MjPrefab/userInfo"));
            temp1.name = "userInfo";
            temp1.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
            temp1.transform.localPosition = new Vector3(0, -335, 0);
            temp1.transform.localScale = Vector3.one;
            temp1.GetComponent<PlayerInfo>().showplayerinfo(GUIO.userName, GUIO.score.ToString());
        }
        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/HeadPhoto0"));
        temp.name = "HeadPhoto" + GUIO.locationIndex;
        temp.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
        touxiangL.Add(temp);
        AnchorPresets ap;
        switch (intArr[GUIO.locationIndex])
        {
            case 0:
                ap = AnchorPresets.MiddleLeft;
                SetAnchor(temp.GetComponent<RectTransform>(), ap);
                temp.transform.localPosition = new Vector3(-639, -356, 0);
                temp.GetComponent<PlayerInfo>().HideHead();
                Debug.Log(temp.transform.localPosition);
                temp.GetComponent<PlayerInfo>().joinRoom.transform.localPosition = new Vector3(114, 0, 0);
                break;
            case 1:
                ap = AnchorPresets.MiddleRight;
                SetAnchor(temp.GetComponent<RectTransform>(), ap);
                temp.transform.localPosition = new Vector3(584, 75, 0);
                Debug.Log(temp.transform.localPosition);
                temp.GetComponent<PlayerInfo>().joinRoom.transform.localPosition = new Vector3(-114, 0, 0);
                break;
            case 2:
                ap = AnchorPresets.MiddleRight;
                SetAnchor(temp.GetComponent<RectTransform>(), ap);
                temp.transform.localPosition = new Vector3(357, 315, 0);
                Debug.Log(temp.transform.localPosition);
                temp.GetComponent<PlayerInfo>().setweizhi();
                temp.GetComponent<PlayerInfo>().joinRoom.transform.localPosition = new Vector3(-114, 0, 0);
                break;
            case 3:
                ap = AnchorPresets.MiddleLeft;
                SetAnchor(temp.GetComponent<RectTransform>(), ap);
                temp.transform.localPosition = new Vector3(-579, 75, 0);
                Debug.Log(temp.transform.localPosition);
                temp.GetComponent<PlayerInfo>().joinRoom.transform.localPosition = new Vector3(114, 0, 0);
                break;

        }
        temp.GetComponent<PlayerInfo>().CreatPlayerInfo(GUIO.score, GUIO.userName, GUIO.userId, GUIO.avatar, GUIO.online);
        temp.GetComponent<PlayerInfo>().GetPlayerInfo(GUIO);
        //测试
        //for (int i = 0; i < PlayerInfo.playerIndex.Count; i++)
        //{
        //    Debug.Log(PlayerInfo.playerIndex[i].ToString() + "==" + PlayerInfo.playerName[i] + "==" + PlayerInfo.playerID[i].ToString());
        //}

        temp.transform.localScale = Vector3.one;

    }
    /// <summary>
    /// 克隆庄的位置 不用管
    /// </summary>
    /// <param name="go"></param>
    /// <param name="weizhi"></param>
    public static void SetZhuangPosition(GameObject go, int weizhi)
    {
        Debug.Log(weizhi);
        go.transform.SetParent(GameObject.Find("HeadPhoto" + weizhi).transform);
    }
    /// <summary>
    ///从服务器得到数据 MajiangChapterMsg 消息之后 开始发牌
    /// </summary>
    /// <param name="mc"></param>
    public static void FaPaiMaJong(com.guojin.mj.net.message.game.MajiangChapterMsg mc)
    {
        com.guojin.mj.net.message.game.UserPlaceMsg up = mc.userPlace[Index];
        if (mc.huiEr != null)
        {
            Destroy(hungameobj);
            huier = mc.huiEr[0];
            GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/Huner"));
            temp.GetComponentInChildren<MaJongOutInfo>().IsHun(huier, MjNameSp[huier]);
            temp.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
            temp.transform.localPosition = new Vector3(0, 59, 0);
            temp.transform.localScale = Vector3.one;
            hungameobj = temp;
        }
        Debug.Log(huier);
        DestoryAllChildren();
        //if (GameObject.Find("GameStart") != null)
        //    Destroy(GameObject.Find("GameStart"));

        Method.InitMaJong();

        ShowMajongFaPai(up.shouPai, mc.zhuangIndex);
    }
    /// <summary>
    /// 出牌的方法
    /// </summary>
    /// <param name="id">牌的id</param>
    /// <param name="weizhi"></param>
    //public static void ChuPaiMaJong(int id, int weizhi)
    //{
    //    com.guojin.mj.net.message.game.OperationOutRet OFP = com.guojin.mj.net.message.game.OperationOutRet.create(id);
    //    //WebSocket.send(com.guojin.mj.net.Net.instance.write(OFP));
    //}


    /// <summary>
    /// 初始化发牌的方法
    /// </summary>
    /// <param name="intArr"></param>
    /// <param name="Banker"></param>
    public static void ShowMajongFaPai(int[] intArr1, int zhuangPosition)
    {
        zhaungweizhi = zhuangPosition;     
        setzhuangweizhi(zhuangPosition);    
        if (intArr[zhuangPosition] == 0)
        {
            isMine = true;
        }
        else
        {
            isMine = false;
        }
        for (int i = 0; i < intArr1.Length; i++)
        {
            intlist.Add(intArr1[i]);
            listMaJong.Remove(intArr1[i]);//将手牌从所有牌中移除
        }
        intlist.Sort();
        for (int i = intlist.Count - 1; i >= 0; i--)
        {
            GameObject temp = Instantiate(clon);
            temp.GetComponentInChildren<MJInfo>().CreatMJInfo(intlist[i], 0, MjNameSp[intlist[i]]);
            temp.transform.SetParent(parent);
            temp.transform.localPosition = Vector3.zero;
            temp.transform.localScale = Vector3.one;
        }
        SetPositionStar(zhuangPosition);//设置指示庄的位置的图标
        if (PeopleNum == 4)
        {
            LoadMajong(intArr[zhuangPosition], 13);
        }
        else
        {
            Debug.Log("3.2");
            LoadMajongThree(13);
        }

    }

    public static void LoadMajongThree(int shoupai)
    {
        for (int i = 0; i < 3; i++)
        {
            if (i != 1)
            {
                for (int j = 0; j < shoupai; j++)
                {
                    GameObject temp = Instantiate(objList[2 * i + 2]);
                    temp.transform.SetParent(tranList[3 * i + 4]);
                    temp.transform.Rotate(0, 0, -270, Space.Self);
                    temp.transform.localPosition = Vector3.zero;
                    temp.transform.localScale = Vector3.one;
                }
            }
        }
    }
    /// <summary>
    /// 返回大厅
    /// </summary>
    public static void LoadMainCity()
    {
        tingArr.Clear();
        isHor = false;
        isReciveReady = false;
        isFirstOutPai = false;        
        fangXiangNum = -1;
        MJType = "";
        DuanMan = -1;
        duanmenindex = -1;
        Debug.Log("UIState_LoadingPage--Method");
        if (GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback)
        {
            GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().resetPlayback();
            UIState_GameHallPage.comeFromState = UIState_GameHallPage.ComeFromState.FromGameMjBack;
            GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_LoadingPage);
        }
        else
        {
            GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_LoadingPage);

        }


    }
    /// <summary>
    /// 显示积分
    /// </summary>
    /// <param name="fanResults"></param>
    public static void ShowScore(List<com.guojin.mj.net.message.game.GameFanResult> fanResults)
    {
        Debug.Log("+++++++++++++++++" + ScoreList.Count + "===============");

        for (int i = 0; i < fanResults.Count; i++)
        {            
            if (i == Index)
            {
                GameObject.Find("userInfo").GetComponent<PlayerInfo>().UpdateScore(IntARR[i] + fanResults[i].score);
            }
            GameObject.Find("HeadPhoto" + i).GetComponent<PlayerInfo>().UpdateScore(IntARR[i] + fanResults[i].score);
            int a = IntARR[i] + fanResults[i].score;
            IntARR[i] = a;

        }
    }
    /// <summary>
    /// 其他位置发牌
    /// </summary>
    /// <param name="weizhi"></param>
    public static void LoadMajong(int weizhi, int shoupai)
    {
        switch (weizhi)
        {
            case 0:
                for (int i = 0; i < shoupai; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (j == 1)
                        {
                            GameObject temp = Instantiate(objList[2 * j + 2]);
                            temp.transform.SetParent(tranList[3 * j + 4]);
                            temp.transform.localPosition = Vector3.zero;
                            temp.transform.localScale = Vector3.one;
                        }
                        else
                        {
                            GameObject temp = Instantiate(objList[2 * j + 2]);
                            temp.transform.SetParent(tranList[3 * j + 4]);
                            temp.transform.Rotate(0, 0, -270, Space.Self);
                            temp.transform.localPosition = Vector3.zero;
                            temp.transform.localScale = Vector3.one;
                        }
                    }
                }
                break;
            case 1:
                for (int i = 0; i < shoupai; i++)
                {
                    GameObject temp = Instantiate(objList[2]);
                    temp.transform.SetParent(tranList[4]);
                    temp.transform.Rotate(0, 0, -270, Space.Self);
                    temp.transform.localPosition = Vector3.zero;
                    temp.transform.localScale = Vector3.one;
                }
                for (int i = 0; i < shoupai; i++)
                {
                    for (int j = 1; j < 3; j++)
                    {
                        if (j == 1)
                        {
                            GameObject temp = Instantiate(objList[2 * j + 2]);
                            temp.transform.SetParent(tranList[3 * j + 4]);
                            temp.transform.localPosition = Vector3.zero;
                            temp.transform.localScale = Vector3.one;
                        }
                        else
                        {
                            GameObject temp = Instantiate(objList[2 * j + 2]);
                            temp.transform.SetParent(tranList[3 * j + 4]);
                            temp.transform.Rotate(0, 0, -270, Space.Self);
                            temp.transform.localPosition = Vector3.zero;
                            temp.transform.localScale = Vector3.one;
                        }
                    }
                }
                break;
            case 2:
                for (int i = 0; i < shoupai; i++)
                {
                    GameObject temp = Instantiate(objList[4]);
                    temp.transform.SetParent(tranList[7]);
                    temp.transform.localPosition = Vector3.zero;
                    temp.transform.localScale = Vector3.one;
                }
                for (int i = 0; i < shoupai; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (j == 0 || j == 2)
                        {
                            GameObject temp = Instantiate(objList[2 * j + 2]);
                            temp.transform.SetParent(tranList[3 * j + 4]);
                            temp.transform.Rotate(0, 0, -270, Space.Self);
                            temp.transform.localPosition = Vector3.zero;
                            temp.transform.localScale = Vector3.one;
                        }
                    }
                }
                break;
            case 3:
                for (int i = 0; i < shoupai; i++)
                {
                    GameObject temp = Instantiate(objList[6]);
                    temp.transform.SetParent(tranList[10]);
                    temp.transform.Rotate(0, 0, -270, Space.Self);
                    temp.transform.localPosition = Vector3.zero;
                    temp.transform.localScale = Vector3.one;
                }
                for (int i = 0; i < shoupai; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (j == 1)
                        {
                            GameObject temp = Instantiate(objList[2 * j + 2]);
                            temp.transform.SetParent(tranList[3 * j + 4]);
                            temp.transform.localPosition = Vector3.zero;
                            temp.transform.localScale = Vector3.one;
                        }
                        else
                        {
                            GameObject temp = Instantiate(objList[2 * j + 2]);
                            temp.transform.SetParent(tranList[3 * j + 4]);
                            temp.transform.Rotate(0, 0, -270, Space.Self);
                            temp.transform.localPosition = Vector3.zero;
                            temp.transform.localScale = Vector3.one;
                        }
                    }
                }
                break;
        }
    }

    public static void Voluntarily(int id, GameObject go, Transform parent)
    {
        MJInfo[] tranChild = parent.GetComponentsInChildren<MJInfo>();
        for (int i = 0; i < tranChild.Length; i++)
        {
            //tranChild[i].localPosition = new Vector3(i*-47,0,0);   
            if (id <= tranChild[0].Id1)
            {
                Debug.Log("1");
                go.transform.SetParent(parent);
                go.transform.localPosition = new Vector3(0, 0, 0);
                go.transform.localScale = Vector3.one;
                return;
            }
            else if (id < tranChild[i].Id1)
            {
                Debug.Log("2");
                go.transform.SetParent(parent);
                go.transform.localPosition = new Vector3(i * -47, 0, 0);
                go.transform.localScale = Vector3.one;
                return;
            }
            else if (id > tranChild[tranChild.Length - 1].Id1)
            {
                Debug.Log("3");
                go.transform.SetParent(parent);
                go.transform.localPosition = new Vector3((tranChild.Length - 1) * -47, 0, 0);
                go.transform.localScale = Vector3.one;
                return;
            }
        }
    }
    /// <summary>
    /// 克隆显示接收到的牌
    /// </summary>
    /// <param name="id"></param>
    public static void ReceiveMaJong(int id)
    {
        goId = id;
        go = Instantiate(clon);
        go.GetComponentInChildren<MJInfo>().CreatMJInfo(goId, 1, MjNameSp[goId]);
        go.transform.SetParent(goParent);
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;
        listMaJong.Remove(id);//新加
    }

    public static float suo;
    public static void ReceiveMaJong(int id, int index)
    {
        goId = id;
        go = Instantiate(clon);
        go.GetComponentInChildren<MJInfo>().CreatMJInfo(goId, 1, MjNameSp[goId]);
        //Transform ReceiveContent = GameObject.Find("ReceiveContent" + (intArr[index] + 11).ToString()).transform;
        Transform ReceiveContent = intArr[index] > 0 ? Method.tranList[intArr[index] + 11] : Method.goParent;
        for (int i = 0; i < ReceiveContent.transform.childCount; i++)
        {
            Destroy(ReceiveContent.GetChild(i).gameObject);
        }
        Debug.Log(ReceiveContent.ToString());
        go.transform.SetParent(ReceiveContent);

        ReceiveContent.gameObject.SetActive(true);

        if (intArr[index] == 0)
        {
            //go.transform.Rotate(0, 0, 90);
            go.transform.localPosition = new Vector3(0, 0, 0);
            go.transform.localScale = Vector3.one;
        }
        if (intArr[index] == 1)
        {
            go.transform.Rotate(0, 0, 90);
            go.transform.localPosition = new Vector3(-4, 0, 0);
            //go.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
            go.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }
        if (intArr[index] == 2)
        {
            go.transform.Rotate(0, 0, 180);
            go.transform.localPosition = new Vector3(25, -19, 0);
            go.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        if (intArr[index] == 3)
        {
            go.transform.Rotate(0, 0, -90);
            go.transform.localPosition = new Vector3(0, -8, 0);
            go.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }
    }


    /// <summary>
    /// 克隆
    /// </summary>
    public static void CloneChild()
    {
        DestoryAll(tranList[1]);
        intlist.Sort();
        for (int i = intlist.Count - 1; i >= 0; i--)
        {
            GameObject temp = Instantiate(clon);
            temp.GetComponentInChildren<MJInfo>().CreatMJInfo(intlist[i], 1, MjNameSp[intlist[i]]);
            temp.transform.SetParent(tranList[1]);
            temp.transform.localPosition = Vector3.zero;
            temp.transform.localScale = Vector3.one;
        }
    }

    /// <summary>
    /// 销毁parent的子对象
    /// </summary>
    /// <param name="parent"></param>
    public static void DestoryAll(Transform parent)
    {
        Transform[] Transformchilds = parent.GetComponentsInChildren<Transform>();
        for (int i = 0; i < Transformchilds.Length; i++)
        {
            if (Transformchilds[i].name != parent.name)
            {
                Destroy(Transformchilds[i].gameObject);
            }
        }
    }
    /// <summary>
    /// 加载声音资源
    /// </summary>
    /// <returns></returns>
    public static AudioClip[] LoadVoice()
    {
        AudioClip[] AcArr = new AudioClip[38];
        for (int i = 0; i < AcArr.Length; i++)
        {
            AudioClip sp = Resources.Load<AudioClip>("VoiceMan/ZZ" + i);
            AcArr[i] = sp;

        }
        return AcArr;
    }
    public static AudioClip[] LoadPuTongVoice()
    {
        AudioClip[] AcArr = new AudioClip[38];
        for (int i = 0; i < AcArr.Length; i++)
        {
            AudioClip sp = Resources.Load<AudioClip>("PUTONGVoiceMan/PT" + i);
            AcArr[i] = sp;

        }
        return AcArr;
    }
    /// <summary>
    /// 加载图片精灵
    /// </summary>
    /// <returns></returns>
    public static Sprite[] LoadSprite()
    {
        Sprite[] SpArr = new Sprite[34];
        for (int i = 0; i < SpArr.Length; i++)
        {
            Sprite sp = Resources.Load<Sprite>("MJSprite/MjName/" + i);
            SpArr[i] = sp;

        }
        return SpArr;
    }
    public static Sprite[] LoadDiSe()
    {
        Sprite[] SpArr = new Sprite[6];
        for (int i = 0; i < SpArr.Length; i++)
        {
            Sprite sp = Resources.Load<Sprite>("MJSprite/MJDipai/" + i);
            SpArr[i] = sp;

        }
        return SpArr;
    }

    public static Sprite[] loadFangxiang()
    {
        Sprite[] SpArr = new Sprite[3];
        for (int i = 0; i < SpArr.Length; i++)
        {
            Sprite sp = Resources.Load<Sprite>("MJSprite/MJDipai/fangxiang0" + i);
            SpArr[i] = sp;

        }
        return SpArr;
    }
    /// <summary>
    /// 加载不同位置的手牌和要出的牌
    /// </summary>
    /// <param name="name"></param>
    public static void LoadPrefabs(string name)
    {
        for (int i = 0; i < 4; i++)
        {
            objList.Add(Resources.Load<GameObject>("MjPrefab/" + name + i));
        }
    }
    /// <summary>
    /// 四人进入房间之后，显示开始游戏和等待按钮
    /// </summary>
    public static void ShowStartGame()
    {

        if (Index == 0)
        {
            GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/StartGameBtn"));
            temp.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
            temp.name = "StartGameBtn";
            temp.transform.localScale = Vector3.one;
            temp.transform.localPosition = new Vector3(0, -205, 0);
            StartGameBtn = temp;
            //temp.GetComponent<Image>().sprite = sp;
            temp.AddComponent<Button>().onClick.AddListener(delegate
            {
                VoiceManger.Instance.BtnVoice();
                com.guojin.mj.net.message.game.GameChapterStart cr = new com.guojin.mj.net.message.game.GameChapterStart();
                SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(cr));
            });
        }
        else
        {

            GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/holdGameBtn"));
            temp.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
            temp.name = "StartGameBtn";
            temp.transform.localScale = Vector3.one;
            temp.transform.localPosition = new Vector3(0, 185, 0);
            StartGameBtn = temp;
            //temp.GetComponent<Image>().sprite = sp1;
        }
    }
    /// <summary>
    /// 发送开始消息到服务器
    /// </summary>
    public static void SendMessageStartGame()
    {
        com.guojin.mj.net.message.game.GameChapterStart cr = new com.guojin.mj.net.message.game.GameChapterStart();
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(cr));
    }

    /// <summary>
    /// 主视角碰、扛的时候从手牌中移除
    /// </summary>
    /// <param name="id">牌的id大小</param>
    /// <param name="infonum">0碰、1大明扛、2暗扛</param>
    public static void SearchDes(int id, int infonum, int weizhi)
    {
        MJInfo[] mjArr = tranList[1].GetComponentsInChildren<MJInfo>();
        int num = 0;
        for (int i = 0; i < mjArr.Length; i++)
        {
            if (infonum == 0 && num < 2 && mjArr[i].Id1 == id)
            {
                num++;
                Destroy(mjArr[i].transform.parent.gameObject);
                intlist.Remove(id);
            }
            else if (mjArr[i].Id1 == id && num == 0)
            {
                Destroy(mjArr[i].transform.parent.gameObject);
                intlist.Remove(id);
            }
        }
        if (infonum == 0)
        {
            ShowGangOrPeng(id, infonum, weizhi);
            isMine = true;
            isPeng = true;
        }
        else if (infonum == 1)
        {
            ShowGangOrPeng(id, infonum, weizhi);
        }
        else
        {
            if (goId == id)
            {
                Destroy(go);
                ShowGangOrPeng(id, infonum, weizhi);
            }
            else
            {
                intlist.Add(goId);
                Destroy(go);
                CloneChild();
                ShowGangOrPeng(id, infonum, weizhi);
            }

        }

    }
    /// <summary>
    /// 解决小明杠的方法
    /// </summary>
    /// <param name="weizhi"></param>
    /// <param name="id"></param>
    public static void SolveXiaoMingGang(int weizhi, int id)
    {

        if (intArr[weizhi] == 0)
        {
            if (id != goId)
            {
                intlist.Remove(id);
                intlist.Add(goId);
                CloneChild();
            }
            Destroy(go);
        }
        else
        {
            setFalse();
            listMaJong.Remove(id);//新加
        }

        if (GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback)
        {
            GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().HandCard[intArr[weizhi]].Remove(id);
            GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().ShowHandCard(intArr[weizhi]);
        }

        OpenAndClose[] opArr = tranList[3 * intArr[weizhi]].GetComponentsInChildren<OpenAndClose>();
        for (int i = 0; i < opArr.Length; i++)
        {
            if (opArr[i].Id == id)
            {
                Destroy(opArr[i].gameObject);
                ShowGangOrPeng(id, 1, weizhi);
            }
        }

    }

    public static void setFalse()
    {
        tranList[12].gameObject.SetActive(false);
        tranList[13].gameObject.SetActive(false);
        tranList[14].gameObject.SetActive(false);
    }
    public static void setFalseOne(int weizhi)
    {
        tranList[11 + intArr[weizhi]].gameObject.SetActive(false);
    }
    public static void setTrue()
    {
        if (PeopleNum == 3)
        {
            tranList[13].gameObject.SetActive(false);
        }
        else
        {
            tranList[13].gameObject.SetActive(true);
        }
        tranList[12].gameObject.SetActive(true);
        tranList[14].gameObject.SetActive(true);
    }
    /// <summary>
    /// 其他三个位置的发牌显示
    /// </summary>
    public static void FaOnePai(int weizhi)
    {
        if (intArr[weizhi] == 1)
        {
            tranList[12].gameObject.SetActive(true);
            tranList[13].gameObject.SetActive(false);
            tranList[14].gameObject.SetActive(false);
        }
        else if (intArr[weizhi] == 2)
        {
            tranList[12].gameObject.SetActive(false);
            tranList[13].gameObject.SetActive(true);
            tranList[14].gameObject.SetActive(false);
        }
        else if (intArr[weizhi] == 3)
        {
            tranList[12].gameObject.SetActive(false);
            tranList[13].gameObject.SetActive(false);
            tranList[14].gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 1 2 3 位置的出牌方法
    /// </summary>
    /// <param name="id"></param>
    /// <param name="weizhi"></param>
    public static void ChuOther(int id, int weizhi)
    {
        List<Transform> listT = new List<Transform>();
        MaJongOutInfo[] trasArr = tranList[3 * intArr[weizhi] + 1].GetComponentsInChildren<MaJongOutInfo>();
        Debug.Log(trasArr.Length);
        for (int i = 0; i < trasArr.Length; i++)
        {
            listT.Add(trasArr[i].transform);
        }
        if (listT.Count > 0) Destroy(listT[0].gameObject);
        if (intArr[weizhi] == 2)
        {
            GameObject temp = Instantiate(objList[intArr[weizhi] * 2 + 1]);
            temp.GetComponent<MaJongOutInfo>().IsHun(id, MjNameSp[id]);
            temp.transform.SetParent(tranList[intArr[weizhi] * 3 + 2]);
            temp.transform.localPosition = Vector3.zero;
            temp.transform.localScale = Vector3.one;
            VoiceManger.Instance.LoadMusiceByName(id);
            lastMajong = temp;
            SetGuangBiao(temp, -1);
        }
        else
        {
            GameObject temp = Instantiate(objList[intArr[weizhi] * 2 + 1]);
            temp.GetComponent<MaJongOutInfo>().IsHun(id, MjNameSp[id]);
            temp.transform.SetParent(tranList[intArr[weizhi] * 3 + 2]);
            if (intArr[weizhi] != 0) temp.transform.Rotate(0, 0, -270, Space.Self);
            temp.transform.localPosition = Vector3.zero;
            temp.transform.localScale = Vector3.one;
            VoiceManger.Instance.LoadMusiceByName(id);
            lastMajong = temp;
            if (intArr[weizhi] == 1)
            {
                SetGuangBiao(temp, 2);
            }
            else
            {
                SetGuangBiao(temp, 1);
            }
        }
        listMaJong.Remove(id);//新加
    }

    public static void ChuOthergushi(int id, int weizhi)
    {
        //List<Transform> listT = new List<Transform>();
        //MaJongOutInfo[] trasArr = tranList[3 * intArr[weizhi] + 1].GetComponentsInChildren<MaJongOutInfo>();
        //Debug.Log(trasArr.Length);
        //for (int i = 0; i < trasArr.Length; i++)
        //{
        //    listT.Add(trasArr[i].transform);
        //}
        //if (listT.Count > 0) Destroy(listT[0].gameObject);
        if (intArr[weizhi] == 2)
        {
            GameObject temp = Instantiate(objList[intArr[weizhi] * 2 + 1]);
            temp.GetComponent<MaJongOutInfo>().IsHun(id, MjNameSp[id]);
            temp.transform.SetParent(tranList[intArr[weizhi] * 3 + 2]);
            temp.transform.localPosition = Vector3.zero;
            temp.transform.localScale = Vector3.one;
            //VoiceManger.Instance.LoadMusiceByName(id);
            //lastMajong = temp;
            //SetGuangBiao(temp, -1);
        }
        else
        {
            GameObject temp = Instantiate(objList[intArr[weizhi] * 2 + 1]);
            temp.GetComponent<MaJongOutInfo>().IsHun(id, MjNameSp[id]);
            temp.transform.SetParent(tranList[intArr[weizhi] * 3 + 2]);
            if (intArr[weizhi] != 0) temp.transform.Rotate(0, 0, -270, Space.Self);
            temp.transform.localPosition = Vector3.zero;
            temp.transform.localScale = Vector3.one;
            //VoiceManger.Instance.LoadMusiceByName(id);
            //lastMajong = temp;
            //if (intArr[weizhi] == 1)
            //{
            //    SetGuangBiao(temp, 2);
            //}
            //else
            //{
            //    SetGuangBiao(temp, 1);
            //}
        }
        listMaJong.Remove(id);//新加
    }
    public static void CloneDuanmen(int weizhi, Sprite huase)
    {
        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/OutPaiduanmen" + intArr[weizhi]));
        temp.GetComponent<MaJongOutInfo>().IsDuanMen(huase);

        if (intArr[weizhi] == 2 || intArr[weizhi] == 0)
        {
            temp.transform.SetParent(tranList[intArr[weizhi] * 3 + 2]);
            temp.transform.localPosition = Vector3.zero;
            temp.transform.localScale = Vector3.one;
        }
        else
        {
            temp.transform.SetParent(tranList[intArr[weizhi] * 3 + 2]);
            if (intArr[weizhi] != 0) temp.transform.Rotate(0, 0, -270, Space.Self);
            temp.transform.localPosition = Vector3.zero;
            temp.transform.localScale = Vector3.one;

        }
    }

    /// <summary>
    /// 1、2、3 位置碰扛
    /// </summary>
    /// <param name="id">牌的id</param>
    /// <param name="weizhi">位置信息</param>
    /// <param name="zhangshu">张数1出牌2碰3扛</param>
    /// <param name="infonum">0碰、1扛、2暗扛</param>
    public static void SearchDesOthers(int id, int weizhi, int infonum)
    {
        int zhangshu;
        if (infonum == 0)
        {
            zhangshu = 2;
        }
        else
        {
            zhangshu = 3;
        }
        List<Transform> listT = new List<Transform>();
        MaJongOutInfo[] trasArr = tranList[3 * intArr[weizhi] + 1].GetComponentsInChildren<MaJongOutInfo>();
        Debug.Log(trasArr.Length);
        for (int i = 0; i < trasArr.Length; i++)
        {
            listT.Add(trasArr[i].transform);
        }

        for (int i = 0; i < zhangshu; i++)
        {
            if (listT.Count > i) Destroy(listT[i].gameObject);
            listMaJong.Remove(id);//新加
        }

        ShowGangOrPeng(id, infonum, weizhi);



    }
    /// <summary>
    /// 接收到消息之后 麻将的行为
    /// </summary>
    /// <param name="id">麻将的id</param>
    /// <param name="weizhi">位置0 1 2 3</param>
    /// <param name="infonum">行为0碰、1扛、2暗扛</param>
    public static void MaJongBehavior(int id, int weizhi, int infonum)
    {
        if (intArr[weizhi] == 0)
        {
            SearchDes(id, infonum, weizhi);
            Debug.Log("来了");
        }
        else
        {
            SearchDesOthers(id, weizhi, infonum);
        }
    }
    /// <summary>
    /// 显示碰、扛后的信息显示
    /// </summary>
    /// <param name="id">碰牌的id</param>   
    /// <param name="weizhi">玩家位置信息</param>  
    /// <param name="PGA">0碰、1扛、2暗扛</param>  
    public static void ShowGangOrPeng(int id, int PGA, int weizhi)
    {
        GameObject temp;
        switch (PGA)
        {
            case 0:
                PGH = 34;
                CloneShow("peng", weizhi);
                VoiceManger.Instance.Guangbiao.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
                VoiceManger.Instance.Guangbiao.transform.localScale = Vector3.one;
                VoiceManger.Instance.Guangbiao.transform.localPosition = new Vector3(1000, 1000, 1000);
                Destroy(lastMajong);
                temp = Instantiate(objList[8 + intArr[weizhi]]);
                temp.GetComponent<OpenAndClose>().Id = id;
                MaJongOutInfo[] mjArr = temp.GetComponentsInChildren<MaJongOutInfo>();
                for (int i = 0; i < mjArr.Length; i++)
                {
                    if (i == 1)
                    {
                        mjArr[i].ShowOnePai(id, MjNameSp[id], fangXiangNum);
                    }
                    mjArr[i].IsHun(id, MjNameSp[id]);
                }
                temp.transform.SetParent(tranList[3 * intArr[weizhi]]);
                temp.transform.localScale = Vector3.one;
                temp.transform.localPosition = Vector3.zero;
                if (intArr[weizhi] == 1 || intArr[weizhi] == 3)
                {
                    temp.transform.Rotate(0, 0, -270, Space.Self);
                }

                break;
            case 1:
                PGH = 35;
                CloneShow("gang", weizhi);
                VoiceManger.Instance.Guangbiao.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
                VoiceManger.Instance.Guangbiao.transform.localPosition = new Vector3(1000, 1000, 1000);
                Destroy(lastMajong);
                temp = Instantiate(objList[12 + intArr[weizhi]]);
                MaJongOutInfo[] mjArr1 = temp.GetComponentsInChildren<MaJongOutInfo>();
                for (int i = 0; i < mjArr1.Length; i++)
                {
                    if (i == 3)
                    {
                        mjArr1[i].ShowOnePai(id, MjNameSp[id], fangXiangNum);
                    }
                    mjArr1[i].IsHun(id, MjNameSp[id]);
                }
                temp.transform.SetParent(tranList[3 * intArr[weizhi]]);
                temp.transform.localScale = Vector3.one;
                temp.transform.localPosition = Vector3.zero;
                if (intArr[weizhi] == 1 || intArr[weizhi] == 3)
                {
                    temp.transform.Rotate(0, 0, -270, Space.Self);
                }
                break;
            case 2:
                PGH = 36;
                CloneShow("gang", weizhi);
                setFalse();
                temp = Instantiate(objList[16 + intArr[weizhi]]);
                temp.transform.SetParent(tranList[3 * intArr[weizhi]]);
                temp.transform.localScale = Vector3.one;
                temp.transform.localPosition = Vector3.zero;

                //todo jia 2017/8/22
                if (intArr[weizhi] == 0)
                {
                    MaJongOutInfo mjot = temp.GetComponentInChildren<MaJongOutInfo>();
                    mjot.IsHun(id, MjNameSp[id]);
                }
                //over

                if (intArr[weizhi] == 1 || intArr[weizhi] == 3)
                {
                    temp.transform.Rotate(0, 0, -270, Space.Self);
                }
                break;
        }
        if (GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback)
        {
            if (PGA == 0)
            {
                GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().HandCard[intArr[weizhi]].Remove(id);
                GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().HandCard[intArr[weizhi]].Remove(id);
            }
            else if (PGA == 1)
            {
                GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().HandCard[intArr[weizhi]].Remove(id);
                GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().HandCard[intArr[weizhi]].Remove(id);
                GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().HandCard[intArr[weizhi]].Remove(id);
            }
            else
            {
                GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().HandCard[intArr[weizhi]].Remove(id);
                GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().HandCard[intArr[weizhi]].Remove(id);
                GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().HandCard[intArr[weizhi]].Remove(id);
                GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().HandCard[intArr[weizhi]].Remove(id);
            }
            GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().ShowHandCard(intArr[weizhi]);
        }
        //ZiDongPosition();
    }
    /// <summary>
    /// 添加一张牌到weizhi的地方
    /// </summary>
    /// <param name="id"></param>
    /// <param name="weizhi"></param>
    public static void AddMajongOutMajong(int id, int weizhi)
    {
        switch (intArr[weizhi])
        {
            case 0:
                GameObject temp0 = Instantiate(objList[0]);
                temp0.transform.SetParent(tranList[1]);
                temp0.transform.Rotate(0, 0, -90);
                temp0.transform.localPosition = Vector3.zero;
                temp0.transform.localScale = Vector3.one;
                break;
            case 1:
                GameObject temp = Instantiate(objList[2]);
                temp.transform.SetParent(tranList[4]);
                temp.transform.Rotate(0, 0, -270, Space.Self);
                temp.transform.localPosition = Vector3.zero;
                temp.transform.localScale = Vector3.one;
                break;
            case 2:
                GameObject temp1 = Instantiate(objList[4]);
                temp1.transform.SetParent(tranList[7]);
                temp1.transform.localPosition = Vector3.zero;
                temp1.transform.localScale = Vector3.one;
                break;
            case 3:
                GameObject temp2 = Instantiate(objList[6]);
                temp2.transform.SetParent(tranList[10]);
                temp2.transform.Rotate(0, 0, -270, Space.Self);
                temp2.transform.localPosition = Vector3.zero;
                temp2.transform.localScale = Vector3.one;
                break;
        }
        //ChuOther(id, weizhi);
    }

    /// <summary>
    /// 碰完之后的出牌指令
    /// </summary>
    /// <param name="Pai"></param>
    public static void PengOverOutMaJong(int Pai)
    {
        com.guojin.mj.net.message.game.OperationOutRet OOR = com.guojin.mj.net.message.game.OperationOutRet.create(Pai);
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(OOR));
    }
    /// <summary>
    /// 出牌操作
    /// </summary>
    public static void OutMaJong(int Pai)
    {
        com.guojin.mj.net.message.game.OperationFaPaiRet OFRR = com.guojin.mj.net.message.game.OperationFaPaiRet.create("OUT", Pai);
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(OFRR));
    }

    public static void CloneSingBtn(Transform parent, string name, int id, string type)
    {
        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/PGHSinglebtn"));
        temp.transform.SetParent(parent);
        temp.GetComponentInChildren<MaJongOutInfo>().IsHun(id, MjNameSp[id]);
        temp.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("MJSprite/MjName/" + name);
        temp.transform.localScale = Vector3.one;
        temp.transform.localPosition = Vector3.zero;
        temp.GetComponentInChildren<Button>().onClick.AddListener(delegate
        {
            SendFaPGHToService(type, id);
            PGH = 35;
            CloneShow("gang", Index);
            isMine = false;
            //MaJongBehavior(id, weizhi, 2);
            Destroy(parent.parent.gameObject);
        }); ;
    }

    public static GameObject CloneSingleBtn(Transform parent, string name, int id)
    {
        if (name == "qi")
        {
            GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/QiSinglebtn"));
            temp.transform.SetParent(parent);
            temp.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("MJSprite/MjName/" + name);
            temp.transform.localScale = Vector3.one;
            temp.transform.localPosition = Vector3.zero;
            return temp;
        }
        else
        {
            GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/PGHSinglebtn"));
            temp.transform.SetParent(parent);
            temp.GetComponentInChildren<MaJongOutInfo>().IsHun(id, MjNameSp[id]);
            temp.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("MJSprite/MjName/" + name);
            temp.transform.localScale = Vector3.one;
            temp.transform.localPosition = Vector3.zero;
            return temp;
        }

    }
    public static GameObject chipenggang;
    /// <summary>
    /// 碰杠胡显示
    /// </summary>
    /// <param name="weizhi"></param>
    /// <param name="id"></param>
    /// <param name="peng"></param>
    /// <param name="xiaominggang"></param>
    /// <param name="daminggang"></param>
    /// <param name="angang"></param>
    /// <param name="hu"></param>
    /// HU:胡牌、PENG:碰、DA_MING_GANG:大明扛、XIAO_MING_GANG:小明扛、AN_GANG:暗杠牌    
    public static void PGHShow(int weizhi, int id, bool peng, bool xiaominggang, bool daminggang, bool angang, bool hu, bool isOther)
    {
        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/PGHbtnParent"));
        temp.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
        temp.transform.localScale = Vector3.one;
        temp.transform.localPosition = Vector3.zero;
        chipenggang = temp;
        GameObject go = temp.transform.GetComponentInChildren<GridLayoutGroup>().gameObject;
        if (hu)
        {
            if (isOther)
            {
                CloneSingleBtn(go.transform, "Dianpaohu", id).GetComponentInChildren<Button>().onClick.AddListener(delegate
                {
                    SendCPGHToService("HU");
                    PGH = 37;
                    CloneShow("hu", weizhi);
                    Destroy(temp);
                });

            }
            else
            {
                CloneSingleBtn(go.transform, "Zimohu", id).GetComponentInChildren<Button>().onClick.AddListener(delegate
                {
                    SendFaPGHToService("HU", id);
                    PGH = 37;
                    CloneShow("hu", weizhi);
                    Destroy(temp);
                });

            }
            //CloneSingleBtn(go.transform, "hu", id).GetComponentInChildren<Button>().onClick.AddListener(delegate
            // {
            //     //发送胡的消息到服务器
            //     if (isOther)
            //     {
            //         SendCPGHToService("HU");
            //     }
            //     else
            //     {
            //         SendFaPGHToService("HU", id);
            //     }
            //     PGH = 37;
            //     CloneShow("hu", weizhi);
            //     Destroy(temp);
            // });
        }
        if (xiaominggang)
        {
            CloneSingleBtn(go.transform, "gang", id).GetComponentInChildren<Button>().onClick.AddListener(delegate
             {
                 //发送小明扛的消息到服务器
                 SendFaPGHToService("XIAO_MING_GANG", id);
                 PGH = 35;
                 CloneShow("gang", weizhi);
                 //SolveXiaoMingGang(weizhi, id);
                 isMine = false;
                 Destroy(temp);
             });
        }
        if (daminggang)
        {
            CloneSingleBtn(go.transform, "gang", id).GetComponentInChildren<Button>().onClick.AddListener(delegate
             {
                 //发送大明扛的消息到服务器
                 SendCPGHToService("DA_MING_GANG");
                 PGH = 35;
                 CloneShow("gang", weizhi);
                 //MaJongBehavior(id, weizhi, 1);
                 Destroy(temp);
             });
        }
        if (angang)
        {
            CloneSingleBtn(go.transform, "gang", id).GetComponentInChildren<Button>().onClick.AddListener(delegate
             {
                 //发送暗扛的消息到服务器
                 SendFaPGHToService("AN_GANG", id);
                 PGH = 35;
                 CloneShow("gang", weizhi);
                 isMine = false;
                 //MaJongBehavior(id, weizhi, 2);
                 Destroy(temp);
             });
        }
        if (peng)
        {
            CloneSingleBtn(go.transform, "peng", id).GetComponentInChildren<Button>().onClick.AddListener(delegate
             {
                 //发送碰的消息到服务器
                 SendCPGHToService("PENG");
                 PGH = 34;
                 CloneShow("peng", weizhi);
                 isMine = true;
                 isPeng = true;
                 //MaJongBehavior(id, weizhi, 0);
                 Destroy(temp);
             });
        }
        CloneSingleBtn(go.transform, "qi", id).GetComponentInChildren<Button>().onClick.AddListener(delegate
         {
             //发送不操作的消息到服务器
             if (isOther)
             {
                 SendCPGHToService("GUO");
             }
             Destroy(temp);
         });
    }

    public static List<int> OutList(int[] arr)
    {
        List<int> list = new List<int>();
        for (int i = 0; i < arr.Length; i++)
        {
            list.Add(arr[i]);
        }
        return list;
    }
    public static void BehaReciveMaJong(com.guojin.mj.net.message.game.OperationFaPai ofp)
    {
        if (ofp.anGang == null || ofp.mingGang == null)
        {
            return;
        }
        if (ofp.anGang.Length != 0 || ofp.mingGang.Length != 0 || ofp.hu)
        {
            GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/PGHbtnParent"));
            temp.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
            temp.transform.localScale = Vector3.one;
            temp.transform.localPosition = Vector3.zero;
            chipenggang = temp;
            GameObject go = temp.transform.GetComponentInChildren<GridLayoutGroup>().gameObject;
            if (ofp.anGang.Length != 0)
            {
                int[] arrAnGang = ofp.anGang;
                for (int i = 0; i < arrAnGang.Length; i++)
                {

                    CloneSingBtn(go.transform, "gang", arrAnGang[i], "AN_GANG");
                }
            }
            if (ofp.mingGang.Length != 0)
            {
                int[] arrXiaoMingGang = ofp.mingGang;
                for (int i = 0; i < arrXiaoMingGang.Length; i++)
                {
                    CloneSingBtn(go.transform, "gang", arrXiaoMingGang[i], "XIAO_MING_GANG");
                }
            }
            if (ofp.hu)
            {
                CloneSingleBtn(go.transform, "Zimohu", ofp.pai).GetComponentInChildren<Button>().onClick.AddListener(delegate
                {
                    //发送胡的消息到服务器                   
                    SendFaPGHToService("HU", ofp.pai);
                    PGH = 37;
                    CloneShow("hu", Index);
                    Destroy(temp);
                });
            }
            CloneSingleBtn(go.transform, "qi", 1).GetComponentInChildren<Button>().onClick.AddListener(delegate
            {
                Destroy(temp);
            });

        }


    }


    /// <summary>
    /// 克隆单个碰扛胡到前端
    /// </summary>
    /// <param name="PrefabName"></param>
    /// <param name="SpriteName"></param>
    /// <param name="V3"></param>

    public static void CloneShow(string SpriteName, int weizhi)
    {
        Vector3 V3;
        GameObject temp1 = Instantiate(Resources.Load<GameObject>("MjPrefab/PGH"));
        temp1.GetComponent<OpenAndClose>().CreatShow(Resources.Load<Sprite>("MJSprite/MjName/" + SpriteName));
        temp1.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
        temp1.transform.localScale = Vector3.one;
        switch (intArr[weizhi])
        {
            case 0:
                V3 = new Vector3(190, -100, 0);
                temp1.transform.localPosition = V3;
                break;
            case 1:
                V3 = new Vector3(374, 73, 0);
                temp1.transform.localPosition = V3;
                break;
            case 2:
                V3 = new Vector3(-122, 234, 0);
                temp1.transform.localPosition = V3;
                break;
            default:
                V3 = new Vector3(-375, 33, 0);
                temp1.transform.localPosition = V3;
                break;
        }
    }
    /// <summary>
    /// 设置光标的位置
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="a"></param>
    public static void SetGuangBiao(GameObject parent, int a)
    {
        VoiceManger.Instance.Guangbiao.transform.SetParent(parent.transform);
        VoiceManger.Instance.Guangbiao.transform.localScale = Vector3.one;
        if (a == -1 || a == 1)
        {
            VoiceManger.Instance.Guangbiao.transform.localPosition = new Vector3(0, a * 24, 0);
        }
        else
        {
            VoiceManger.Instance.Guangbiao.transform.localPosition = new Vector3(19, 0, 0);
        }
    }
    public static void SetPositionStar(int weizhi)
    {

        if (!GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback)
        {

            if (positionStar == null)
            {
                positionStar = Instantiate(Resources.Load<GameObject>("MjPrefab/PositionStar"));
                positionStar.transform.SetParent(GameObject.Find("Center_Img").transform);
                positionStar.transform.localPosition = Vector3.zero;
                positionStar.transform.localScale = Vector3.one;
                positionStar.transform.Rotate(new Vector3(0, 0, 90 * intArr[weizhi] + 90));
            }
            else
            {
                Destroy(positionStar);
                positionStar = Instantiate(Resources.Load<GameObject>("MjPrefab/PositionStar"));
                positionStar.transform.SetParent(GameObject.Find("Center_Img").transform);
                positionStar.transform.localPosition = Vector3.zero;
                positionStar.transform.localScale = Vector3.one;
                positionStar.transform.Rotate(new Vector3(0, 0, 90 * intArr[weizhi] + 90));
            }
        }




    }

    /// <summary>
    /// 其他玩家出牌阶段发送碰、大明扛、胡的消息到服务器
    /// </summary>
    /// <param name="ws"></param>
    /// <param name="type"></param>
    public static void SendCPGHToService(string type)
    {
        com.guojin.mj.net.message.game.OperationCPGHRet OPCPGH = com.guojin.mj.net.message.game.OperationCPGHRet.create(type, null);
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(OPCPGH));
    }
    /// <summary>
    /// 起牌阶段的扛、胡
    /// </summary>
    /// <param name="ws"></param>
    /// <param name="type"></param>
    /// <param name="pai"></param>
    public static void SendFaPGHToService(string type, int pai)
    {
        com.guojin.mj.net.message.game.OperationFaPaiRet OFPP = com.guojin.mj.net.message.game.OperationFaPaiRet.create(type, pai);
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(OFPP));
    }
    public static void SetAnchor(RectTransform source, AnchorPresets allign/*, int offsetX , int offsetY*/ )
    {
        switch (allign)
        {
            case (AnchorPresets.TopLeft):
                {
                    source.anchorMin = new Vector2(0, 1);
                    source.anchorMax = new Vector2(0, 1);
                    break;
                }
            case (AnchorPresets.TopCenter):
                {
                    source.anchorMin = new Vector2(0.5f, 1);
                    source.anchorMax = new Vector2(0.5f, 1);
                    break;
                }
            case (AnchorPresets.TopRight):
                {
                    source.anchorMin = new Vector2(1, 1);
                    source.anchorMax = new Vector2(1, 1);
                    break;
                }

            case (AnchorPresets.MiddleLeft):
                {
                    source.anchorMin = new Vector2(0, 0.5f);
                    source.anchorMax = new Vector2(0, 0.5f);
                    break;
                }
            case (AnchorPresets.MiddleCenter):
                {
                    source.anchorMin = new Vector2(0.5f, 0.5f);
                    source.anchorMax = new Vector2(0.5f, 0.5f);
                    break;
                }
            case (AnchorPresets.MiddleRight):
                {
                    source.anchorMin = new Vector2(1, 0.5f);
                    source.anchorMax = new Vector2(1, 0.5f);
                    break;
                }

            case (AnchorPresets.BottomLeft):
                {
                    source.anchorMin = new Vector2(0, 0);
                    source.anchorMax = new Vector2(0, 0);
                    break;
                }
            case (AnchorPresets.BottonCenter):
                {
                    source.anchorMin = new Vector2(0.5f, 0);
                    source.anchorMax = new Vector2(0.5f, 0);
                    break;
                }
            case (AnchorPresets.BottomRight):
                {
                    source.anchorMin = new Vector2(1, 0);
                    source.anchorMax = new Vector2(1, 0);
                    break;
                }

            case (AnchorPresets.HorStretchTop):
                {
                    source.anchorMin = new Vector2(0, 1);
                    source.anchorMax = new Vector2(1, 1);
                    break;
                }
            case (AnchorPresets.HorStretchMiddle):
                {
                    source.anchorMin = new Vector2(0, 0.5f);
                    source.anchorMax = new Vector2(1, 0.5f);
                    break;
                }
            case (AnchorPresets.HorStretchBottom):
                {
                    source.anchorMin = new Vector2(0, 0);
                    source.anchorMax = new Vector2(1, 0);
                    break;
                }

            case (AnchorPresets.VertStretchLeft):
                {
                    source.anchorMin = new Vector2(0, 0);
                    source.anchorMax = new Vector2(0, 1);
                    break;
                }
            case (AnchorPresets.VertStretchCenter):
                {
                    source.anchorMin = new Vector2(0.5f, 0);
                    source.anchorMax = new Vector2(0.5f, 1);
                    break;
                }
            case (AnchorPresets.VertStretchRight):
                {
                    source.anchorMin = new Vector2(1, 0);
                    source.anchorMax = new Vector2(1, 1);
                    break;
                }

            case (AnchorPresets.StretchAll):
                {
                    source.anchorMin = new Vector2(0, 0);
                    source.anchorMax = new Vector2(1, 1);
                    break;
                }
        }
        //source.localPosition = new Vector3(offsetX, offsetY, 0);
    }
    public static int returnFangxiang(int myindex, int otherindex)
    {
        int a = myindex - otherindex;
        if (PeopleNum == 4)
        {
            if (a == -3 || a == 1)
            {
                //左
                return 2;
            }
            else if (a == -1 || a == 3)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        else
        {
            if (myindex == Index || otherindex == Index)
            {
                if (a == -1 || a == 2)
                {
                    return 0;
                }
                else
                {
                    return 2;
                }
            }
            //else if(otherindex==Index)
            //{
            //    if (a == -2 || a == -1 || a == 3)
            //    {
            //        return 0;
            //    }
            //    else
            //    {
            //        return 2;
            //    }

            //}
            else
            {
                return 1;
            }
        }
    }
    /// <summary>
    /// 客户端托管的处理 如果不需要注掉就行了
    /// </summary>
    public static void TuoGuan()
    {
        if (!GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback)
        {
            if (chipenggang != null)
            {
                if (!isMine)
                {
                    SendCPGHToService("GUO");
                }

                Destroy(chipenggang);
            }
            if (go != null)
            {
                Destroy(go);
                GameObject CloneGameobj = Instantiate(objList[1]);
                CloneGameobj.GetComponent<MaJongOutInfo>().IsHun(goId, Method.MjNameSp[goId]);
                CloneGameobj.transform.SetParent(tranList[2]);
                CloneGameobj.transform.localPosition = Vector3.zero;
                CloneGameobj.transform.localScale = Vector3.one;
                VoiceManger.Instance.LoadMusiceByName(goId);
                //向服务器发送出牌的指令
                OutMaJong(goId);
                lastMajong = CloneGameobj;
                SetGuangBiao(CloneGameobj, 1);
                isMine = false;
                go = null;

            }
            if (isPeng)
            {
                //intlist.Remove(intlist[intlist.Count-1]);


                GameObject CloneGameobj = Instantiate(objList[1]);
                CloneGameobj.GetComponent<MaJongOutInfo>().IsHun(intlist[intlist.Count - 1], Method.MjNameSp[intlist[intlist.Count - 1]]);
                CloneGameobj.transform.SetParent(tranList[2]);
                CloneGameobj.transform.localPosition = Vector3.zero;
                CloneGameobj.transform.localScale = Vector3.one;
                VoiceManger.Instance.LoadMusiceByName(intlist[intlist.Count - 1]);
                lastMajong = CloneGameobj;
                go = null;
                int num = intlist[intlist.Count - 1];
                intlist.Remove(intlist[intlist.Count - 1]);
                CloneChild();
                Method.SetGuangBiao(CloneGameobj, 1);
                // 向服务器发送出牌的指令
                Method.PengOverOutMaJong(num);
                Method.isMine = false;
                Method.isPeng = false;
            }
        }


    }



    public static List<string> ReturnName(int locaindex)
    {
        List<string> username = new List<string>();
        if (PeopleNum == 4)
        {
            switch (locaindex)
            {
                case 0:
                    username.Add(username1);
                    username.Add(username2);
                    username.Add(username3);
                    return username;
                case 1:
                    username.Add(username0);
                    username.Add(username2);
                    username.Add(username3);
                    return username;
                case 2:
                    username.Add(username0);
                    username.Add(username1);
                    username.Add(username3);
                    return username;
                case 3:
                    username.Add(username0);
                    username.Add(username1);
                    username.Add(username2);
                    return username;

            }
        }
        else
        {
            switch (locaindex)
            {
                case 0:
                    username.Add(username1);
                    username.Add(username2);
                    return username;
                case 1:
                    username.Add(username0);
                    username.Add(username2);
                    return username;
                case 2:
                    username.Add(username0);
                    username.Add(username1);
                    return username;

            }
        }

        return username;
    }
    public static List<string> showDistanceInfo(int locaindex)
    {
        List<string> distance = new List<string>();
        if (PeopleNum == 4)
        {
            switch (locaindex)
            {
                case 0:
                    distance.Add(user01Distance);
                    distance.Add(user02Distance);
                    distance.Add(user03Distance);
                    return distance;
                case 1:
                    distance.Add(user01Distance);
                    distance.Add(user12Distance);
                    distance.Add(user13Distance);
                    return distance;
                case 2:
                    distance.Add(user02Distance);
                    distance.Add(user12Distance);
                    distance.Add(user23Distance);
                    return distance;
                case 3:
                    distance.Add(user03Distance);
                    distance.Add(user13Distance);
                    distance.Add(user23Distance);
                    return distance;

            }
        }
        else
        {
            switch (locaindex)
            {
                case 0:
                    distance.Add(user01Distance);
                    distance.Add(user02Distance);
                    return distance;
                case 1:
                    distance.Add(user01Distance);
                    distance.Add(user12Distance);
                    return distance;
                case 2:
                    distance.Add(user02Distance);
                    distance.Add(user12Distance);
                    return distance;

            }
        }
        return distance;
    }
    public static List<string> showDistanceInfowai(int locaindex)
    {
        GPS(locaindex);
        List<string> distance = new List<string>();
        if (PeopleNum == 4)
        {
            switch (locaindex)
            {
                case 0:
                    distance.Add(user12Distance);
                    distance.Add(user13Distance);
                    distance.Add(user23Distance);
                    return distance;
                case 1:
                    distance.Add(user02Distance);
                    distance.Add(user03Distance);
                    distance.Add(user23Distance);
                    return distance;
                case 2:
                    distance.Add(user01Distance);
                    distance.Add(user03Distance);
                    distance.Add(user13Distance);
                    return distance;
                case 3:
                    distance.Add(user01Distance);
                    distance.Add(user02Distance);
                    distance.Add(user12Distance);
                    return distance;

            }
        }
        else
        {
            switch (locaindex)
            {
                case 0:
                    distance.Add(user12Distance);
                    return distance;
                case 1:
                    distance.Add(user02Distance);
                    return distance;
                case 2:
                    distance.Add(user01Distance);
                    return distance;
            }
        }

        return distance;

    }


    public static void setzhuangweizhi(int weizhi)
    {
        GameObject temp1 = Instantiate(Resources.Load<GameObject>("MjPrefab/zhuang"));
        SetZhuangPosition(temp1, weizhi);
        zhuang = temp1;
        temp1.transform.localScale = Vector3.one;
        if (weizhi == Index)
        {
            temp1.transform.localPosition = new Vector3(28, 23, 0);
        }
        else
        {
            temp1.transform.localPosition = new Vector3(30, 30, 0);
        }

    }
    public static void ShowZui()
    {
        if (StateText==null)
        {
            GameObject temp1 = Instantiate(Resources.Load<GameObject>("MjPrefab/ShowState"));
            temp1.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
            temp1.transform.localScale = Vector3.one;
            temp1.transform.localPosition = Vector3.zero;
            Method.StateText = temp1;
            temp1.GetComponent<OpenAndClose>().InitState();
        }       
        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/ChoiceZui"));
        temp.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
        temp.name = "ChoiceZui";
        temp.transform.localScale = Vector3.one;
        temp.transform.localPosition = new Vector3(0, -180, 0);
        ChoiceZui[] choiceArr = temp.GetComponentsInChildren<ChoiceZui>();
        for (int i = 0; i < choiceArr.Length; i++)
        {
            choiceArr[i].CreateOne(i);
        }
    }
    public static void CloneShouPaiGuShi(int type, List<int> list)
    {
        switch (type)
        {
            case 1:
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] >= 0 && list[i] <= 8)
                    {

                    }
                }
                break;
            default:
                break;
        }
    }
    public static bool OutPaiGuShi(int id)
    {
        int num1 = DuanMan - 1;
        bool CanOut = false;
        int num = 0;
        for (int i = 0; i < intlist.Count; i++)
        {
            if (intlist[i] >= num1 * 9 && intlist[i] <= (num1 * 9 + 8))
            {
                num++;
            }
        }
        if (goId >= num1 * 9 && goId <= (num1 * 9 + 8))
        {
            num++;
        }
        if (num > 0)
        {
            if (id >= num1 * 9 && id <= (num1 * 9 + 8))
            {
                CanOut = true;
            }
            else
            {
                CanOut = false;
            }
        }
        else
        {
            CanOut = true;
        }
        return CanOut;

    }

    public static List<int> listMaJong = new List<int>();
    /// <summary>
    /// 初始化麻将
    /// </summary>
    public static void InitMaJong()
    {
        listMaJong.Clear();
        for (int i = 0; i < 34; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                listMaJong.Add(i);
            }
        }

    }
    /// <summary>
    /// 显示剩余麻将的张数
    /// </summary>
    public static int ShowSurplusMaJong(int id)
    {
        int num = 0;
        for (int i = 0; i < listMaJong.Count; i++)
        {
            if (listMaJong[i] == id)
            {
                num++;
            }
        }
        return num;
    }
    /// <summary>
    /// 移除已知的麻将
    /// </summary>
    /// <param name="type">1 出牌或者接收到的牌 2 其他玩家碰的牌 3其他玩家大明扛的牌 4自己位置的暗杠(4仅限重连时候用)</param>
    /// <param name="id">牌的id</param>
    public static void RemoveMaJong(int type, int id)
    {
        for (int i = 0; i < type; i++)
        {
            listMaJong.Remove(id);
        }
    }

    /// <summary>
    /// 获得推荐断门的先后推荐顺序 （固始麻将专用）
    /// </summary>
    /// <returns></returns>
    public static int[] GetRecommend()
    {
        int numTong = 0;
        int numTiao = 0;
        int numWan = 0;
        for (int i = 0; i < intlist.Count; i++)
        {
            if (intlist[i] <= 26 && intlist[i] >= 18)
            {
                numWan++;
            }
            else if (intlist[i] <= 17 && intlist[i] >= 9)
            {
                numTiao++;
            }
            else if (intlist[i] <= 8 && intlist[i] >= 0)
            {
                numTong++;
            }
        }
        int[] arr;
        if (numWan >= numTong)
        {
            if (numTong >= numTiao)
            {
                arr = new[] { 2, 1, 3 };
            }
            else
            {
                if (numWan >= numTiao)
                {
                    arr = new[] { 1, 2, 3 };
                }
                else
                {
                    arr = new[] { 1, 3, 2 };
                }
            }
        }
        else
        {
            if (numWan >= numTiao)
            {
                arr = new[] { 2, 3, 1 };
            }
            else
            {
                if (numTong >= numTiao)
                {
                    arr = new[] { 3, 2, 1 };
                }
                else
                {
                    arr = new[] { 3, 1, 2 };
                }
            }
        }
        return arr;
    }

    /// <summary>
    /// 把字符串转换成一个int[]
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public static int[] ReturnIntArr(string content)
    {
        int[] list;
        if (content == "" || content == null)
        {
            list = new[] { -1 };
        }
        else
        {
            list = Array.ConvertAll<string, int>(content.Split(','), s => int.Parse(s));
        }

        return list;
    }
    public static string ReturnString(List<string> content)
    {
        string strArray = string.Join(",", content.ToArray());
        return strArray;
    }
    public static void CloneMask(Transform gob)
    {
        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/CreatroomBtnMask"));
        temp.transform.SetParent(gob);
        temp.transform.localPosition = Vector3.zero;
        temp.transform.localScale = Vector3.one;
    }
}


public enum AnchorPresets
{
    TopLeft,
    TopCenter,
    TopRight,
    MiddleLeft,
    MiddleCenter,
    MiddleRight,
    BottomLeft,
    BottonCenter,
    BottomRight,
    BottomStretch,
    VertStretchLeft,
    VertStretchRight,
    VertStretchCenter,
    HorStretchTop,
    HorStretchMiddle,
    HorStretchBottom,
    StretchAll
}
public enum PivotPresets
{
    TopLeft,
    TopCenter,
    TopRight,
    MiddleLeft,
    MiddleCenter,
    MiddleRight,
    BottomLeft,
    BottomCenter,
    BottomRight,
}
public enum State
{
    gameOutside,
    readyGame,
    gameStart,
}

