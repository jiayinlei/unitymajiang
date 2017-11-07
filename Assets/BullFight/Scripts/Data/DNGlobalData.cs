using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.guojin.dn.net.message;
using UnityEngine.UI;
using UnityEngine;

/* ***********************************************
 * Describe:该类功能描述
 * Author :  MuLongFei
 * Email: 424113771@qq.com
 * DATA: 2017/7/31 9:51:54 
 * FileName: DNGlobalData 
 * Version: V1.0.1
 * ***********************************************/

/// <summary>
/// 全局变量类，包含：游戏信息类gameInfo，是否是首次创建isFirstCreate，加入的房间的ID：joinRoomID，玩家的数量roomPlayerNumber，是否准备好haZhunBei，是不是庄家，押注倍数，LocationIndex
/// </summary>
class DNGlobalData {
    public static DouniuGameRoomInfoRet gameInfo;
    public static QiangZhuangRet zhuangInfo;
    public static bool isFirstCreate = true;
    public static List<Sprite> userImage = new List<Sprite>();
    public static bool isFirstIn = true;
    //public static string joinRoomID;
    public static int roomPlayerNumber = 0;
    public static bool hasZhunBei;//是否准备
    public static bool isZhuangJia = true;//是不是庄家
    public static int yazhuNumber;//押注倍数
    public static int locationIndex;//
    public static string roomID = "";
    public static string countDownOpt = "";
    //public static int currentPlayerIndex = 0;
    public static int robNumber = 0;//押注倍数
    public static string mode;//模式
    public static int fangZhuUserID;
    public static int currentUserID;
    public static string currentUserName;
    public static int currentChapter;
    public static int maxChapter;
    public static string maxPlayerNumber;
    public static string token;
    public static bool hintDouble;
    public static bool confirmOutDouble;
    public static bool confirmOutSingle;
    public static bool doubleIsDone;
    public static bool hintSingle;
    public static bool waitEnd;
    public static bool currentPlayerIsReady;
    public static bool socketStop=false;
   // public static bool isConnecting = false;
    public static string roomState;

    public static List<GameObject> tempBackPokerList = new List<GameObject>();

    public static GameObject blur;

    public static List<int> userIDList = new List<int>();
    public static List<string> userNameList = new List<string>();
    public static List<string> userScoreList = new List<string>();
    public static List<string> userChapterList = new List<string>();
    public static List<string> winCountList = new List<string>();
    public static List<string> loseCountList = new List<string>();
    public static List<string> equalCountList = new List<string>();
    //public static List<string> userImagePathList = new List<string>();
    public static int chapterCount;
    public static int winCount;
    public static int loseCount;

    public static Dictionary<int, int> paiCountDic = new Dictionary<int, int>();

    public static Dictionary<int, byte[]> spriteDict = new Dictionary<int, byte[]>();
    public static Dictionary<int, Sprite> userImageDict = new Dictionary<int,Sprite>();
    public static List<string> spritePathList = new List<string>();

    public static int emojiCount;
    public static Sprite clickUserImage;
    public static string clickUserID;
    public static string clickUserSocre;
    public static string clickUserName;
    public static string chongLianType ;
    public static bool isChongLian = false;
    public static bool confirmChongLian = false;
    public static bool lookPoker = true;
    public static int zhuangIndex;
    public static int handPokerNum = 1;
    public static string changeMode;
    public static string noticeText;
    public static string changeUserName;
    public static Sprite changeNiuType;
    public static string changeNiuTypeStr;
    public static bool isStart;
    /// <summary>
    /// 删除房间的玩家名字
    /// </summary>
    public static string delUserName;
    public static bool show = true;
    public static string loginMode;
    //public static List<int> robHandPokerCount=new List<int>();




}
