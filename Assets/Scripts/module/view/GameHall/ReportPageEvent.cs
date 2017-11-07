using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReportPageEvent : EventManager {
    public override void InformationSetting()
    {
        
    }
    void Send()
    {
        string str = this.BindingSource [0].GetComponent<InputField>().text;

        if (str != null && !(str.Equals("")))
        {

            //MainLogic.HttpPostData("http://122.114.150.35:8080/", "content=" + str);

                com.guojin.mj.net.message.login.Question rhl = com.guojin.mj.net.message.login.Question.create(str);
                SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(rhl));
  


            TooL.showNetErroTips("感谢您的反馈！", () => { DestroyImmediate(this.gameObject); });
            //DestroyImmediate(this.gameObject);

        }
        else
        {
            TooL.showNetErroTips("内容不能为空！", () => { DestroyImmediate(this.gameObject); });
//#if WX_SDK
//       LoginAndShare.Controller.ShowAndroidMsg("内容不能为空！");
//#else

//#endif

        }

    }
    void ClosePage()
    {
        Destroy(this.gameObject);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
