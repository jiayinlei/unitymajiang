using UnityEngine;
using System.Collections;
using com.guojin.mj.net.message;
using System.IO;
using System.Text;

// 不允许挂在到任何节点上
public class GameData  {
    
    private static GameData instance = new GameData();
    public static GameData GetInstance() {
        return GameData.instance;
    }
    public PlayerData playerData = new PlayerData();


    private bool isInit = false;
    public void initEvent() {
        if (this.isInit == false) {
            this.isInit = true;
            ObserverMgr.CleanDataEventListener();
            string[] msgList = this.GetMsgList();
            foreach(string msg in msgList) {

                ObserverMgr.addEventListenerWithDelegate(msg, new ObserverMgr.OnMsg(this.OnMsg));
            }
        }
    }

    protected   string[] GetMsgList() {
        return new string[] {
             MessageFactoryImpi.instance.getMessageString(7,10),// 登录返回
             MessageFactoryImpi.instance.getMessageString(7,1),
             MessageFactoryImpi.instance.getMessageString(1,7),
             MessageFactoryImpi.instance.getMessageString(7,24),
             MessageFactoryImpi.instance.getMessageString(7,20),//获取用户战绩
             MessageFactoryImpi.instance.getMessageString(7,29),//微信分享成功
             MessageFactoryImpi.instance.getMessageString(7,35),//胜率
             MessageFactoryImpi.instance.getMessageString(7,13),//更新房卡
        };
    }
    public   void OnMsg(string msg, com.guojin.core.io.message.Message data) {
        if (msg == MessageFactoryImpi.instance.getMessageString(7, 10)) {
            com.guojin.mj.net.message.login.Login_Ret gameData = (com.guojin.mj.net.message.login.Login_Ret)data;
            this.playerData.id = gameData.id;
            this.playerData.name = gameData.name;
            this.playerData.loginToken = gameData.loginToken;
            this.playerData.avatar = gameData.avatar;
            this.playerData.gold = gameData.gold;


            //把用户临时信息存储在Json文件里
            //string JsonInfo = this.playerData.loginToken;
            //string savePath = Application.dataPath + "/Resources/JsonCfg/LoginInfo.json";
            //File.WriteAllText(savePath, JsonInfo, Encoding.UTF8);
        }
        if (msg == MessageFactoryImpi.instance.getMessageString(7, 35))
        {
            com.guojin.mj.net.message.login.Record recordData = (com.guojin.mj.net.message.login.Record)data;
            this.playerData.mjWinTimes = recordData.majiangWinCount;
            this.playerData.playMjTimes = recordData.majiangNum;
            this.playerData.playPokerTimes = recordData.pokerNum;
            this.playerData.winPokerTimes = recordData.pokerWinCount;
        }
        if (msg == MessageFactoryImpi.instance.getMessageString(7, 29))
        {
            com.guojin.mj.net.message.login.PayBack payBackData = (com.guojin.mj.net.message.login.PayBack)data;
            this.playerData.gold += payBackData.goldNum;
            GameObject obj = GameObject.Find("Canvas/addNode/MJCreatRoomPage(Clone)");
            if (obj)
            {
                obj.GetComponent<MJCreatRoomPageEvent>().ResetGoldNum();
                UIManager.ChangeUI(UIManager.PageState.RewardPage, (GameObject obj_0) =>
                {
                    obj_0.GetComponent<RewardPageEvent>().getRoomCard = payBackData.goldNum;
                    obj_0.GetComponent<RewardPageEvent >().InformationSetting();
                });
            }
            Debug.Log("房卡获得+"+ payBackData.goldNum.ToString ());
        }
        if (msg == MessageFactoryImpi.instance.getMessageString(7, 1)) {
            com.guojin.mj.net.message.login.CreateRoomRet gameData = (com.guojin.mj.net.message.login.CreateRoomRet)data;
            this.playerData.roomId = gameData.roomCheckId;
        }
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 7)) {
            // this.playerData.GRI = (com.guojin.mj.net.message.game.GameRoomInfo)data;
               Method.GRI = (com.guojin.mj.net.message.game.GameRoomInfo)data;
        }
        if (msg == MessageFactoryImpi.instance.getMessageString(7, 24)) {
            this.playerData.SystemMessages = (com.guojin.mj.net.message.login.SysSetting)data;
        }
        //if (msg == MessageFactoryImpi.instance.getMessageString(7, 20)) {
        //    playerData.Result.Add ((com.guojin.mj.net.message.login.RoomHistory)data);
        //}
        if (msg == MessageFactoryImpi.instance.getMessageString(7, 20)) {
            playerData.Result.Add ((com.guojin.mj.net.message.login.RoomHistoryListRet)data);
        }
        if (msg == MessageFactoryImpi.instance.getMessageString(7, 13))
        {
            com.guojin.mj.net.message.login.Pay pay = (com.guojin.mj.net.message.login.Pay)data;
            playerData.gold += pay.gold;
        }
    }

}
