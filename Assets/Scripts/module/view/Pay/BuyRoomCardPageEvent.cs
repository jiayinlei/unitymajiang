using com.guojin.mj.net.message.login;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyRoomCardPageEvent : EventManager
{

    void Start()
    {
        Open();
        InformationSetting();
    }

    public override void InformationSetting()
    {
        //BindingSource[0].GetComponent<Text>().text = Method.listPrice[0].DiamondPrice.ToString()+ "房卡";
        //BindingSource[1].GetComponent<Text>().text = Method.listPrice[0].BtnPrice.ToString() + "元";
        //BindingSource[2].GetComponent<Text>().text = Method.listPrice[1].DiamondPrice.ToString() + "房卡";
        //BindingSource[3].GetComponent<Text>().text = Method.listPrice[1].BtnPrice.ToString() + "元";
        //BindingSource[4].GetComponent<Text>().text = Method.listPrice[2].DiamondPrice.ToString() + "房卡";
        //BindingSource[5].GetComponent<Text>().text = Method.listPrice[2].BtnPrice.ToString() + "元";
        //BindingSource[6].GetComponent<Text>().text = Method.listPrice[3].DiamondPrice.ToString() + "房卡";
        //BindingSource[7].GetComponent<Text>().text = Method.listPrice[3].BtnPrice.ToString() + "元";
        BindingSource[0].GetComponent<Text>().text =  "10房卡";
        BindingSource[1].GetComponent<Text>().text =  "10元";
        BindingSource[2].GetComponent<Text>().text = "21房卡";
        BindingSource[3].GetComponent<Text>().text = "20元";
        BindingSource[4].GetComponent<Text>().text = "53房卡";
        BindingSource[5].GetComponent<Text>().text = "50元";
        BindingSource[6].GetComponent<Text>().text ="110房卡";
        BindingSource[7].GetComponent<Text>().text ="100元";
    }


    void ClosePage()
    {
        Destroy(this.gameObject);
    }


    void OnClickBtn1()
    {
        // SendPay(Method.listPrice[0].DiamondPrice);
        SendPay(10);
    }

    void OnClickBtn2()
    {
        //SendPay(Method.listPrice[1].DiamondPrice);
        SendPay(21);
    }
    void OnClickBtn3()
    {
        //SendPay(Method.listPrice[2].DiamondPrice);
        SendPay(53);
    }
    void OnClickBtn4()
    {
        //SendPay(Method.listPrice[3].DiamondPrice);
        SendPay(110);
    }


    void SendPay(int  goldCount)
    {
        //发送微信支付消息给服务器 7, 27
        WXPay wxp = WXPay.Create(goldCount, 0);
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(wxp));
    }

    void CallBtnClick()
    {
#if WX_SDK
        TooL.showTips("确认拨打电话？", () => LoginAndShare.Controller.CallPhone());  
#else
#endif
    }


	
	// Update is called once per frame
	void Update () {
		
	}
}
