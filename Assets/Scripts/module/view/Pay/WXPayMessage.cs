using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using com.guojin.mj.net.message;
using UnityEngine.SceneManagement;
using com.guojin.mj.net.message.login;
using com.guojin.mj.net.message.game;

public class WXPayMessage : Observer
{

    // Use this for initialization
    void Start()
    {
        initMsg();
    }

    protected override string[] GetMsgList()
    {
        return new string[] {
            MessageFactoryImpi.instance.getMessageString(7,28),// JoinRoomReady 创建房间返回          
        };
    }


    public override void OnMsg(string msg, com.guojin.core.io.message.Message data)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(7,28))
        {
            WXPayRet wxpr = (WXPayRet)data;

            if (wxpr.Result)
            {
                LoginAndShare.Controller.OnRecePayRet(wxpr.Appid,wxpr.Noncestr,wxpr.Package,wxpr.Partnerid,wxpr.Prepayid,wxpr.Timestamp,wxpr.Sign);
            }
            else
            {
                Debug.Log("服务器返回购买申请失败");
            }
        }
    }
}
