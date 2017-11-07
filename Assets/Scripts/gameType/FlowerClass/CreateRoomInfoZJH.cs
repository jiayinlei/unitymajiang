using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using com.guojin.mj.net.message;
using com.guojin.mj.net.message.flower;

public class CreateRoomInfoZJH : Observer
{
    private string playNumber = "10";// 牌局数
    private string straightbet = "5";// 单注
    private string beginMoney = "500";// 初始筹码
    private string zhuangjiaZJH = "true";// 庄家 true:赢家坐庄  false： 轮流坐庄
    private string playerNum = "5";//人数

    //10: 10局(2房卡)
    //20: 20局(4房卡) 
    //30: 30局(6房卡)
    public void OnClickPlayNumber10()
    {
        this.playNumber = "10";
    }
    public void OnClickPlayNumber20()
    {
        this.playNumber = "20";
    }
    public void OnClickPlayNumber30()
    {
        this.playNumber = "30";
    }
    // 单注
    public void ONClickStraightbet5()
    {
        this.straightbet = "5";
    }
    public void ONClickStraightbet10()
    {
        this.straightbet = "10";
    }
    public void ONClickStraightbe20()
    {
        this.straightbet = "20";
    }
    /// <summary>
    /// 初始筹码
    /// </summary>
    public void ONClickBeginMoney500()
    {
        this.beginMoney = "500";
    }
    public void ONClickBeginMoney1000()
    {
        this.beginMoney = "1000";
    }
    public void ONClickBeginMoney2000()
    {
        this.beginMoney = "2000";
    }
    //玩家人数
    public void OnClickPlayerZJH4()
    {
        playerNum = "5";
    }
    // 庄家
    public void OnClickZhuangJiaYinZJH()
    {
        //赢家坐庄
        this.zhuangjiaZJH = "true";
    }
    public void OnClickZhuangJiaLunZJH()
    {
        //轮流坐庄
        this.zhuangjiaZJH = "false";
    }
    //选好配置，点击创建房间按钮
    public void sendCreatRoomInfoZJH()
    {
        List<com.guojin.mj.net.message.flower.OptionEntryZJH> OptionListZJH = new List<com.guojin.mj.net.message.flower.OptionEntryZJH>();
        OptionListZJH.Add(com.guojin.mj.net.message.flower.OptionEntryZJH.create("chapterMax", this.playNumber));//局数  
        OptionListZJH.Add(com.guojin.mj.net.message.flower.OptionEntryZJH.create("danZhu", this.straightbet));// 单注
        OptionListZJH.Add(com.guojin.mj.net.message.flower.OptionEntryZJH.create("chuShiFen", this.beginMoney));// 初始筹码
        OptionListZJH.Add(com.guojin.mj.net.message.flower.OptionEntryZJH.create("moShi", this.zhuangjiaZJH));// 坐庄方式 
        OptionListZJH.Add(com.guojin.mj.net.message.flower.OptionEntryZJH.create("userNum", this.playerNum));// 人数
        com.guojin.mj.net.message.flower.CreateRoomZJH CR = com.guojin.mj.net.message.flower.CreateRoomZJH.create(OptionListZJH);
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(CR));
    }
    // 点击确定创建房间
    public void OnClickCreateRoomBtn()
    {
        sendCreatRoomInfoZJH();
        MainSceneZJH.allPeopleNum = int.Parse(playerNum);
    }
    void Start()
    {
        this.initMsg();
    }
    // 点击取消
    public void OnClickCancleBtn()
    {
        Destroy(this.gameObject);
    }
}
