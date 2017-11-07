using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using com.guojin.mj.net.message.login;

public class PlayerData
{


    public int id;
    public string name;
    public string roomId;
    public string loginToken;
    public long gold;
    public string avatar;
    public int playMjTimes;
    public int mjWinTimes;
    public int playPokerTimes;
    public int winPokerTimes;
    public string radio;
    public static  Dictionary<string, string> dictionary = new Dictionary<string, string>();
    public static string[] zuiContent = {"独赢","夹子","边张","单调","风赢","风将","一九将","六连飘","一四七","二五八","三六九"};
    public string place = "未知";
    public string longitude = "";//经度
    public string latitude = "";//纬度
    public bool isClub;
    public static Dictionary<int, string> PaoZuiDictionary = new Dictionary<int, string>();


    public List<RoomHistoryListRet> Result = new List<RoomHistoryListRet>();

    public com.guojin.mj.net.message.game.GameRoomInfo GRI;
    public com.guojin.mj.net.message.login.SysSetting SystemMessages;

    public static void Setdictionary()
    {
        //dictionary.Add("room.createRoomSuccess","房间创建成功");
        dictionary.Add("room.createRoomError", "房间创建失败,请重试");
        dictionary.Add("room.alreadyJoinRoom", "已经进入房间");
        dictionary.Add("room.full", "房间满了");
        dictionary.Add("room.errorRoomCheckId", "不存在的房间!");
        dictionary.Add("room.noGold", "房卡不够创建房间");
        dictionary.Add("room.alreadyExitRoom", "已经退出房间!");
        dictionary.Add("room.alreadyDelRoom", "已经解散房间!");
        dictionary.Add("room.delRoom", "房间被解散!");
        dictionary.Add("room.cannotExitRoom", "已经开始，不能退出！!");
        dictionary.Add("room.cannotDelRoom", "游戏已经开始，不能解散！!");
        dictionary.Add("room.notEnoughUser", "在线人数不够，不能开始！!");
        dictionary.Add("room.endRoom", "游戏结束");
        dictionary.Add("room.alreadyCreateRoom", "已经创建过房间");

    }
    public static string people;
    public static string paijutype;
    public static string peopletype;    
    public static string xuanpaotype;
    public static string hupaitype;
    public static string huntype;
    public static string zhuangjiatype;
    public static string qiduitype;
    public static string gangkaitype;
    public static string daipaotype;
    public static string fengtype;
    public static string difenType;
    public static string paozuiType;
    public static string duanmenType;

    public static string difen;
    public static string jutype;
    public static string peopleNum;
    public  static string juNum;
    public static string hun;
    public static string feng;
    public static string pao;
    public static string hupai;
    public static string qidui;
    public static string zhuangjia;
    public static string gangkai;
    public static string daipao;
    public static string duanmen;
    public static string paozui;
    public static void SetRoomInfo(int peoplenum, int junum, bool ishun, bool isfeng, int paonum, bool isfangpao, bool isqidui, bool iszhuangjia, bool isgangkai, bool isdaipao)
    {
        peopleNum = "人数：" + peoplenum + "人局";
        if (peoplenum==3)
        {
            people = "3";
               peopletype = "三人麻将";
        }
        else
        {
            people = "4";
            peopletype = "四人麻将";
        }     
        juNum = "局数："+ junum + "局";
        jutype = junum.ToString();
        if (junum==4)
        {
            paijutype = "4局（2房卡）";
        }
        else if(junum == 8)
        {
            paijutype = "8局（4房卡）";
        }
        else
        {
            paijutype = "16局（8房卡）";
        }
        if (paonum == 1)
        {
            pao = "选跑类型：每局选跑";
            xuanpaotype = "每局选跑";
        }
        else
        {
            pao = "选跑类型：四局选跑";
            xuanpaotype = "四局选跑";
        }
        if (isfangpao)
        {
            hupai = "胡牌类型：点炮胡";
            hupaitype = "点炮胡";
        }
        else
        {
            hupai = "胡牌类型：自摸胡";
            hupaitype = "自摸胡";
        }
        if (ishun)
        {
            hun = "混牌：带混";
            huntype = "带混";
        }
        else
        {
            hun = "混牌：不带混";
            huntype = "不带混";
        }
        if (isfeng)
        {
            feng = "风牌：带风";
            fengtype = "带风";
        }
        else
        {
            feng = "风牌：不带风";
            fengtype = "不带风";
        }
        if (isqidui)
        {
            qidui = "七对加倍：加倍";
            qiduitype = "七对加倍";
        }
        else
        {
            qidui = "七对加倍：不加倍";
            qiduitype = "七对不加倍";
        }
        if (iszhuangjia)
        {
            zhuangjia = "庄家加底：加底";
            zhuangjiatype = "庄家加底";
        }
        else
        {
            zhuangjia = "庄家加底：不加底";
            zhuangjiatype = "庄家不加底";
        }
        if (isgangkai)
        {
            gangkai = "杠上花加倍：加倍";
            gangkaitype = "杠上花加倍";
        }
        else
        {
            gangkai = "杠上花加倍：不加倍";
            gangkaitype = "杠上花不加倍";
        }
        if (isdaipao)
        {
            daipao = "杠带跑：带跑";
            daipaotype = "杠带跑";
        }
        else
        {
            daipao = "杠带跑：不带跑";
            daipaotype = "杠不带跑";
        }
        //SetContent(isfangpao, "胡牌类型：点炮胡", "胡牌类型：自摸胡", hupai);
        //SetContent(ishun, "混牌：带混", "混牌：不带混",hun);
        //SetContent(isfeng, "风牌：带风", "风牌：不带风", feng);
        //SetContent(isqidui, "七对加倍：加倍", "七对加倍：不加倍", qidui);
        //SetContent(iszhuangjia, "庄家加倍：加倍", "庄家加倍：不加倍", zhuangjia);
        //SetContent(isgangkai, "杠开加倍：加倍", "杠开加倍：不加倍", gangkai);
        //SetContent(isdaipao, "带跑：带跑", "带跑：不带跑", daipao);
       
    }
    public static void SetRoomInfogushi(int peoplenum, int junum, bool isfeng, int isfangpao,int xuanpao,int difen1,bool paozui1,bool duanmen1,bool zhuangjia1)
    {
        peopleNum = "人数：" + peoplenum + "人局";
        if (peoplenum == 3)
        {
            people = "3";
            peopletype = "三人麻将";
        }
        else
        {
            people = "4";
            peopletype = "四人麻将";
        }
        juNum = "局数：" + junum + "局";
        jutype = junum.ToString();
        if (junum == 4)
        {
            paijutype = "4局（2房卡）";
        }
        else if (junum == 8)
        {
            paijutype = "8局（4房卡）";
        }
        else
        {
            paijutype = "16局（8房卡）";
        }
        if (isfangpao==0)
        {
            hupai = "胡牌类型：点炮赢一家";
            hupaitype = "点炮赢一家";
        }
        else if(isfangpao == 1)
        {
            hupai = "胡牌类型：点炮赢三家";
            hupaitype = "点炮赢三家";
        }
        else
        {
            hupai = "胡牌类型：自摸胡";
            hupaitype = "自摸胡";
        }
        if (isfeng)
        {
            feng = "风牌：带风";
            fengtype = "带风";
        }
        else
        {
            feng = "风牌：不带风";
            fengtype = "不带风";
        }
        if (xuanpao == 1)
        {
            pao = "选跑类型：每局选跑";
            xuanpaotype = "每局选跑";
        }
        else
        {
            pao = "选跑类型：四局选跑";
            xuanpaotype = "四局选跑";
        }
        
            difenType = "底分："+difen1+"分";
           difen = difen1+"分";
        if (paozui1)
        {
            paozuiType = "每局跑嘴";
            paozui = "跑嘴：每局跑嘴";
        }
        else
        {
            paozuiType = "不跑嘴";
            paozui = "跑嘴：每局不跑嘴";
        }
        if (duanmen1)
        {
            duanmen = "断门：每局断门";
               duanmenType = "每局断门";
        }
        else
        {
            duanmen="断门：每局不断门";
            duanmenType = "不断门";
        }
        if (zhuangjia1)
        {
            zhuangjiatype = "庄家跑底";
            zhuangjia = "庄家跑底：跑底";
        }
        else
        {
            zhuangjiatype = "庄家不跑底";
            zhuangjia = "庄家跑底：不跑底";
        }
        //SetContent(isfangpao, "胡牌类型：点炮胡", "胡牌类型：自摸胡", hupai);
        //SetContent(ishun, "混牌：带混", "混牌：不带混",hun);
        //SetContent(isfeng, "风牌：带风", "风牌：不带风", feng);
        //SetContent(isqidui, "七对加倍：加倍", "七对加倍：不加倍", qidui);
        //SetContent(iszhuangjia, "庄家加倍：加倍", "庄家加倍：不加倍", zhuangjia);
        //SetContent(isgangkai, "杠开加倍：加倍", "杠开加倍：不加倍", gangkai);
        //SetContent(isdaipao, "带跑：带跑", "带跑：不带跑", daipao);

    }


    public static void SetContent(bool istrue,string name1,string name2,string name)
    {
        if (istrue)
        {
            name = name1;
        }
        else
        {
            name = name2;
        }
    }
    //ReadFile.LoadJsonFromFile("/Resources/JsonCfg/LoginInfo.json");
}
