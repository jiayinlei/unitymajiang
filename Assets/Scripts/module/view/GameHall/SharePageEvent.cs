using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SharePageEvent : EventManager {

    public bool hasRoomID  = false;
    public bool isPngShare = false;
    public bool closeFrendsShare = false;
    public bool closeLineShare   = false;
    public override void InformationSetting()
    {
        if (closeFrendsShare)
        {
            SetActive(this.BindingSource[1],false);
        }
        if (closeLineShare)
        {
            SetActive(this.BindingSource[0], false);
        }
    }
    void WxShareBtnClick()
    {
        //com.guojin.mj.net.message.login.WeiXinShow weiXinShow = com.guojin.mj.net.message.login.WeiXinShow.create();
        //SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(weiXinShow));
        //Debug.Log("分享测试++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
        if (hasRoomID)
        {
            LoginAndShare.Controller.isPngShare = false;
            LoginAndShare.Controller.OnClickShareInfoForAndroid(0);
        }
        else if (isPngShare)
        {
            LoginAndShare.Controller.isPngShare = true;
#if UNITY_ANDROID && !UNITY_EDITOR

         StartCoroutine(GetCapture_version2(0));

#elif UNITY_IPHONE
            LoginAndShare.Controller.SharePngIos();

#endif
        }
        else
        {
            LoginAndShare.Controller.isPngShare = true;
#if UNITY_ANDROID && !UNITY_EDITOR

         StartCoroutine(GetCapture_version2(0));

#elif UNITY_IPHONE
            LoginAndShare.Controller.SharePngIos();

#endif
        }
        //CancelBtnClick();
    }

    void WxFrendsShareBtnClick()
    {
        if (hasRoomID)
        {
            LoginAndShare.Controller.isPngShare = false;
            LoginAndShare.Controller.OnClickShareInfoForAndroid(1);
        }
        else if (isPngShare)
        {
            LoginAndShare.Controller.isPngShare = true;
#if UNITY_ANDROID && !UNITY_EDITOR

         StartCoroutine(GetCapture_version2(1));

#elif UNITY_IPHONE
            LoginAndShare.Controller.SharePngIos();

#endif
        }
        else
        {
            LoginAndShare.Controller.isPngShare = true;
#if UNITY_ANDROID && !UNITY_EDITOR

         StartCoroutine(GetCapture_version2(1));

#elif UNITY_IPHONE
            LoginAndShare.Controller.SharePngIos();

#endif
           


        }
    }

    void StoreBtnClick()
    {
        if (hasRoomID)
        {
            LoginAndShare.Controller.OnClickShareInfoForAndroid(2);
        }
        else
        {
            LoginAndShare.Controller.OnClickShared(2);
        }
        CancelBtnClick();
    }
    IEnumerator GetCapture_version2(int shareType)

    {
        yield return new WaitForEndOfFrame();
        System.Random random = new System.Random();
        int i = random.Next(4);
        Texture2D tex = Resources.Load<Texture2D>("Atlas/BigSprite/lineShare");

        byte[] imagebytes = tex.EncodeToPNG();//转化为png图  
        tex.Compress(false);//对屏幕缓存进行压缩  
        //File.WriteAllBytes(Application.dataPath + "/123.txt", imagebytes);
        File.WriteAllBytes(Application.persistentDataPath + "/sharecapture.png", imagebytes);
        yield return null;
        LoginAndShare.Controller.GetCapture_version2(shareType);
        CancelBtnClick();
    }
    void CancelBtnClick()
    {
        DestroyImmediate(this .gameObject );
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
