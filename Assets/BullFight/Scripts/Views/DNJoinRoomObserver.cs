using Assets.Scripts.BullFight.Data;
using com.guojin.core.io.message;
using com.guojin.dn.net.message;
using com.guojin.mj.net.message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DNJoinRoomObserver : Observer {
    private Text roomText;
    private Text noticeText;
    //private Button btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9, btn10, btn11, btnClose;
    void Start() {
        initMsg();
        //if (SceneManager.GetActiveScene().name != "GameHall") {
            noticeText = transform.FindChild("JoinRoom/Notice").GetComponent<Text>();
            //roomText = transform.FindChild("JoinRoom/RoomTextBG/Text").GetComponent<Text>();
            noticeText.text = "";
        //}

    }
    protected override string[] GetMsgList() {
        return new string[] {
                MessageFactoryImpi.instance.getMessageString(5, 3),
                MessageFactoryImpi.instance.getMessageString(5, 24)
            };
    }

    public override void OnMsg(string msg, Message data) {
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 3)) {
            DouniuJoinRoomRet resp = data as DouniuJoinRoomRet;
            if (resp.result) {
                Debug.Log("加入房间成功");
                DNGlobalData.isFirstCreate = false;
                Destroy(transform.gameObject);
                UIManager.ChangeUI(UIManager.PageState.DNWaitPanel, (GameObject obj) => {
                    // obj.GetComponent<DNCreatRoomPage>().InformationSetting();
                });
                //GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_LoadingPage);
            } else {
                if (SceneManager.GetActiveScene().name != "Login") {
                    roomText.text = "";
                    noticeText.text = "房间不存在";
                    Invoke("ClearNoticeText", 1.5f);
                    Debug.Log("加入房间失败");
                }
            }
        }
        print("Join");
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 24)) {
            DouniuGameRoomInfoRet resp = data as DouniuGameRoomInfoRet;
            DNGlobalData.gameInfo = resp;
        }
    }
}
