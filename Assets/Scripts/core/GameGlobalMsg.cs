using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// 字段命名规范: class + var 全部大写下划线区分单词
class GameGlobalMsg
{
    public static string NET_OPEN = "GameGlobalMsg_NetOpen";
    public static string NetSend= "GameGlobalMsg_NetSend";
    public static string NetRecv = "GameGlobalMsg_NetRecv";
    public static string NetClose = "GameGlobalMsg_NetClose";//网络断开连接
    public static string NetError = "GameGlobalMsg_NetError";
    public static string HallOnClickZZMJ = "GameGlobalMsg_HallOnClickZZMJ";// 大厅点击郑州麻将按钮
    public static string MainOnClickReturn = "GameGlobalMsg_MainOnClickReturn";// 主界面点击返回大厅按钮
    public static string SceneOnClickReturn = "GameGlobalMsg_SceneOnClickReturn";//所有游戏场景的退出游戏按钮
    public static string GamHoRetBeturn = "GameGlobalMsg_GamHoRetBeturn";//游戏大厅返回登录按钮
    public static string AddYuanbaoBtn = "GameGlobalMsg_AddYuanbaoBtn";//添加元宝
    public static string LoginOK = "GameGlobalMsg_LoginOK";

    

    public static string WeChatBtn = "GameGlobalMsg_WeChatBtn";
    public static string StartNet = "GameGlobalMsg_GameStart";
    public static String DissolveRoom = "GameGlobalMsg_DissolveRoom";


    public static string HallOnClickZJH = "GameGlobalMsg_HallOnClickZJH";//大厅点击炸金花按钮

}