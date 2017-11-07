using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;
using LitJson;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using GameCore;

public class LoginAndShare : MonoBehaviour
{
    public static string titli = "亲友局，无外挂[点击进入下载]";
    public static string content = "玩家：" + Method.userName + " ID：" + Method.userID + " 邀请您加入[亲友局]，点击此消息进入下载页面！";
    public static string title1 = "亲友局-郑州麻将：房号[ " + Method.GameRoomID + " ]";
    public string content1 = "玩家：" + Method.userName + " 在[亲友局]开了 " + Method.maJongJuNum + " 局，快来一起玩吧. 输入房间号进入游戏.";
    public static string url = URLConst.IosShareURL;
    public static string dailiURL = URLConst.IosDailiURL;
    public static string telephoneNum= NumConst.IosTelNum;

    public static bool isMicrophoneOk;
    public string returnTitle(string name, string id)
    {
        string content = "玩家：" + name + " ID：" + id + " 邀请您加入[亲友局]，点击此消息进入下载页面！";
        return content;
    }
    public string returnTitle1(string roomid)
    {
        string title1;
        if (Method.MJType== "MaJongZhengZhou")
        {
            title1 = "亲友局-郑州麻将 ：房号[ " + roomid + " ]";
        }
        else
        {
            title1 = "亲友局-固始麻将 ：房号[ " + roomid + " ]";
        }
        return title1;
    }
    public string returnContent(string name, string jushu)
    {
        string content1 = "玩家：" + name + " 在[亲友局]开了 " + jushu + " 局，快来一起玩吧. 输入房间号进入游戏.";
        return content1;
    }
    //郑州麻将分享内容
    public string returnContentRoom()
    {

      //  (PlayerData.jutype, PlayerData.people, PlayerData.huntype, PlayerData.hupaitype, PlayerData.fengtype, PlayerData.xuanpaotype, PlayerData.daipaotype, PlayerData.zhuangjiatype, PlayerData.gangkaitype, PlayerData.qiduitype);
        string content1;
        if (Method.MJType == "MaJongZhengZhou")
        {
            content1 = PlayerData.people + "人" + PlayerData.jutype + "局," + PlayerData.huntype + "," + PlayerData.hupaitype + "," + PlayerData.fengtype + "," + PlayerData.xuanpaotype + "," + PlayerData.daipaotype + "," + PlayerData.zhuangjiatype + "," + PlayerData.gangkaitype + "," + PlayerData.qiduitype + ",赶快来!";
        }
        else
        {
            content1 = PlayerData.people + "人" + PlayerData.jutype + "局," + PlayerData.hupaitype + "," + PlayerData.difen + "," + PlayerData.xuanpaotype + "," + PlayerData.duanmenType + "," + PlayerData.paozuiType + "," + PlayerData.fengtype + "," + PlayerData.zhuangjiatype + " ,赶快来!";
        }        
        return content1;
    }   
    private static LoginAndShare _Controller = null;

    public static LoginAndShare Controller
    {
        get
        {
            if (_Controller == null)
            {
                _Controller = GameObject.Find("StaticSource").GetComponent<LoginAndShare>();
            }
            return _Controller;
        }
    }
    /// <summary>
    /// Ios审核，从服务器获取功能开关对应的数字，
    /// 0：开启游客登陆，
    /// </summary>
    public List<int> functionNum = new List<int>();
    public void callAndroidJava(string methodName, params object[] args)
    {
#if UNITY_ANDROID
        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
        {
            jo.Call(methodName, args);
            Debug.LogError(methodName + " Called");
        }
#endif
    }

    public void sharePng()
    {
#if WX_SDK
        StartCoroutine(GetCapture());

#else
        StartCoroutine(GetCapture());
#endif

    }
    public void GoToProcessView()
    {
        callAndroidJava("GoToProcessView");
    }

    public void GetPlaceInfo()
    {
        callAndroidJava("SendPlaceToUnity");
    }
    public void GetPlayerPlaceIOS(string place)
    {
        Debug.Log("unity获取的地理位置" + place);
        List<string> p = place.Split('_').ToList();
        GameData.GetInstance().playerData.longitude = p[0];
        GameData.GetInstance().playerData.latitude  = p[1];
    }
    public void GetPlayerPlace( string place)
    {
        Debug.Log("unity获取的地理位置" + place);
        List<string>  p = place.Split('_').ToList();


        GameData.GetInstance().playerData.place     = p[0];
        GameData.GetInstance().playerData.longitude = p[1];
        GameData.GetInstance().playerData.latitude  = p[2];
    }

    IEnumerator GetCapture()

    {

        yield return new WaitForEndOfFrame();

        int width = Screen.width;

        int height = Screen.height;

        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0, true);

        byte[] imagebytes = tex.EncodeToPNG();//转化为png图  
        tex.Compress(false);//对屏幕缓存进行压缩  
        //File.WriteAllBytes(Application.dataPath + "/123.txt", imagebytes);
        File.WriteAllBytes(Application.persistentDataPath + "/screencapture.png", imagebytes);//存储png图 
#if UNITY_ANDROID


        this.callAndroidJava("saveCurrentImage",
            new object[]
                        {
                                Application.persistentDataPath + "/screencapture.png",0,0        
                        });
#elif UNITY_IPHONE
        SharePNG();
#endif
    }
    public void GetCapture_version2(int shareType)
    {
        this.callAndroidJava("saveCurrentImage",
                        new object[]
                        {
                                Application.persistentDataPath + "/sharecapture.png",shareType,1
                        });
    }
    public void GetNewVersion()
    {
#if UNITY_ANDROID
        this.callAndroidJava("GetNewVersion");
#elif UNITY_IPHONE
        openDownloadUrl(url);
#endif
    }
    public void GetAgency()
    {
#if UNITY_ANDROID
       this.callAndroidJava("OpenWebView");
#elif UNITY_IPHONE
        GetAgentios(dailiURL);
#endif
    }
    public void CallPhone()
    {
#if UNITY_ANDROID
      this.callAndroidJava("CallTelBtnClick");
#elif UNITY_IPHONE
        CallPhoneios(telephoneNum);
#endif

    }
    public void CheckMicrophone()
    {
        this.callAndroidJava("HasRecordPermission");
    }
    public void CheckMicrophoneCallBack(string req)
    {
        if (req.Equals("true"))
        {
            isMicrophoneOk = true;
        }
        else
        {
            isMicrophoneOk = false;
        }
    }
    public void ShowAndroidMsg(string msg)
    {
        this.callAndroidJava("shoumsg", msg);
    }
    public void SharePngIos()
    {
#if UNITY_IPHONE
        ShareQRPng();
#endif
    }
    #region //unity iphone交互
    public void ReCreate(string url, string name)
    {
#if UNITY_IPHONE
        _ReCreate(url,name);
#endif
    }

    public void ReConnect()
    {
#if UNITY_IPHONE
        _ReConnect();
#endif
    }

    public void ReClose()
    {
#if UNITY_IPHONE
        _ReClose();
#endif
    }

    public void ReSend(string message)
    {
#if UNITY_IPHONE
        _ReSend(message);
#endif
    }

    public void ReSendData(byte[] message, uint size)
    {
#if UNITY_IPHONE
        _ReSendData(message,size);
#endif
    }

    public void ReIsOpend()
    {
#if UNITY_IPHONE
        _ReIsOpened();
#endif
    }
    // Use this for initialization  

#if UNITY_IPHONE
    [DllImport("__Internal")]
    private static extern void OnClickSharedios(string roomid, string sec,string url);//unity 调用ios的分享方法传入了两个参数 roomid 和描述
#endif
#if UNITY_IPHONE
    [DllImport("__Internal")]
    private static extern void CallPhoneios(string phonenum);//unity 调用ios的分享方法传入了两个参数 roomid 和描述
#endif
#if UNITY_IPHONE
    [DllImport("__Internal")]
    private static extern void OnClickWXLoginios();//unity 调用ios的登录方法，需要回调OnWXloginCallBack 带参数code
#endif
#if UNITY_IPHONE
    [DllImport("__Internal")]
    private static extern void GetAgentios(string dailiurl);//unity 调用ios的登录方法，需要回调OnWXloginCallBack 带参数code
#endif
#if UNITY_IPHONE
    [DllImport("__Internal")]
    private static extern void SharePNG();//unity 调用ios的登录方法，需要回调OnWXloginCallBack 带参数code
#endif
#if UNITY_IPHONE
    [DllImport("__Internal")]
    private static extern void openDownloadUrl(string downloadUrl);//打开下载页面

    [DllImport("__Internal")]
    private static extern void _ReCreate(string url, string gameObjectName);

    [DllImport("__Internal")]
    private static extern void _ReConnect();

    [DllImport("__Internal")]
    private static extern void _ReClose();

    [DllImport("__Internal")]
    private static extern void _ReSend(string message);

    [DllImport("__Internal")]
    private static extern void _ReSendData(byte[] message, uint size);

    [DllImport("__Internal")]
    private static extern bool _ReIsOpened();
    [DllImport("__Internal")]
    private static extern bool ShareQRPng();
#endif

    #endregion


    //接收服务器返回支付信息，吊起微信
    public void OnRecePayRet(string appid, string noncestr, string packageid, string partnerid, string prepayid, string timestamp, string sign)
    {
        Debug.Log(appid);
        Debug.Log(noncestr);
        Debug.Log(packageid);
        Debug.Log(partnerid);
        Debug.Log(prepayid);
        Debug.Log(timestamp);
        Debug.Log(sign);
#if UNITY_ANDROID
        this.callAndroidJava("genPayReq", appid, noncestr, packageid, partnerid, prepayid, timestamp, sign);
#elif UNITY_IPHONE
        //TODO
#endif
    }


    public static string sec = "";
    //分享
    /// <summary>
    /// 分享按钮事件
    /// </summary>
    public void OnClickShared(int shareType)
    {

        content = returnTitle(Method.userName, Method.userID.ToString());
#if UNITY_ANDROID

        if (shareType == 1)
        {
            callAndroidJava("sharePngToTimeline", Application.streamingAssetsPath + "/lineShare.png");
        }
        else
        {
            Debug.Log("---c# onClickShared ---");
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            jo.Call("OnWeChatShared", titli, content, shareType);
        }
#elif UNITY_IPHONE
        OnClickSharedios( titli, content,url);
#endif

    }



    public void OnClickShareInfo()
    {
        //title1 = returnTitle1(Method.GameRoomID);
        //content1 = returnContentRoom();
        title1 = MaJong.MaJongManage.Instance().MaJong.MaJongTitle();
        content1= MaJong.MaJongManage.Instance().MaJong.MaJongContent();
#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("OnWeChatShared", title1, content1, Method.GameRoomID,0);
#elif UNITY_IPHONE
        OnClickSharedios(title1, content1,url+ "?roomNo=" + Method.GameRoomID);
#endif
    }
    public void OnClickShareInfoForAndroid(int shareType)
    {
        //title1 = returnTitle1(Method.GameRoomID);
        title1 = MaJong.MaJongManage.Instance().MaJong.MaJongTitle();
        Debug.Log("GameRoomID>>" + Method.GameRoomID);
        content1 = returnContent(Method.userName, Method.maJongJuNum);
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("OnWeChatShared", title1, content1, Method.GameRoomID,shareType);

    }
    public bool isPngShare = false;
    //ios 调用InviteFriendsBtn 的OnSharedCallBack 带 string 参数
    public void OnSharedCallBack(string result)
    {
        if (isPngShare && result.Equals ("succeed"))
        {
            Debug.Log("--- c# onSharedCallBack");
            com.guojin.mj.net.message.login.WeiXinShow weiXinShow = com.guojin.mj.net.message.login.WeiXinShow.create();
            SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(weiXinShow));
        }

    }
    //android分享成功回调
    /// <summary>
    ///微信登录按钮事件
    /// </summary>
    public void OnClickWXLogin()
    {
#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("OnClickWXLogin");
#elif UNITY_IPHONE
        OnClickWXLoginios();
#endif
    }

    /// <summary>
    /// 登陆回调
    /// </summary>
    /// <param name="code"></param>
    //ios 调用WEIXINLogin 的OnWXloginCallBack 带 string 参数
    void OnWXloginCallBack(string code)
    {
        string url = URLConst.WXAppLoginCallBackUrl;
        JsonData data = new JsonData();
        data["code"] = code;
        string content = HttpPostData(url, "data=" + data.ToJson());
        JsonData jd = JsonMapper.ToObject(content);
        if (jd["status"].ToJson() == "-1")
        {
            Debug.Log("登录失败");//todo:sunfei 微信登录回调失败处理
        }
        else
        {
            GameObject obj = GameObject.Find("Canvas/addNode/LoginPage(Clone)");
            if (obj)
            {
                obj.GetComponent<LoginPageEvent>().BindingSource[2].GetComponent<Button>().enabled = false;
                StartCoroutine(HandleWxLogin());
            }
            string longitude = GameData.GetInstance().playerData.longitude;
            string latitude = GameData.GetInstance().playerData.latitude;
            Debug.Log("longitude>>" + longitude + "  latitude>>" + latitude);

            com.guojin.mj.net.message.login.Login Log = com.guojin.mj.net.message.login.Login.create("WEIXIN", null, jd["data"].ToJson(), longitude, latitude);
            SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(Log));
        }

    }
    public IEnumerator HandleWxLogin()
    {
        yield return new WaitForSeconds(1.5f);
        GameObject obj = GameObject.Find("Canvas/addNode/LoginPage(Clone)");
        if (obj)
        {
            obj.GetComponent<LoginPageEvent>().BindingSource[2].GetComponent<Button>().enabled = true;
        }
        

    }

    public string HttpPostData(string url, string param)
    {
        var result = string.Empty;
        //注意提交的编码 这边是需要改变的 这边默认的是Default：系统当前编码
        byte[] postData = System.Text.Encoding.UTF8.GetBytes(param);
        // 设置提交的相关参数 
        HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
        System.Text.Encoding myEncoding = System.Text.Encoding.UTF8;
        request.Method = "POST";
        request.KeepAlive = false;
        request.AllowAutoRedirect = true;
        request.ContentType = "application/x-www-form-urlencoded";
        request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR  3.0.04506.648; .NET CLR 3.5.21022; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)";
        request.ContentLength = postData.Length;

        // 提交请求数据 
        Stream outputStream = request.GetRequestStream();
        outputStream.Write(postData, 0, postData.Length);
        outputStream.Close();
        HttpWebResponse response;
        Stream responseStream;
        StreamReader reader;
        string srcString;
        response = request.GetResponse() as HttpWebResponse;
        responseStream = response.GetResponseStream();
        reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("UTF-8"));
        srcString = reader.ReadToEnd();
        result = srcString;   //返回值赋值 通过返回值判断是否登录成功
        reader.Close();
        return result;
    }

    public void SendExitRoom()
    {
        if (Method.Index == 0&&Method.level==0)
        {
            com.guojin.mj.net.message.game.VoteDelStart VDS = new com.guojin.mj.net.message.game.VoteDelStart();
            SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(VDS));
        }
        else
        {
            com.guojin.mj.net.message.login.ExitRoom exitroom = new com.guojin.mj.net.message.login.ExitRoom();
            SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(exitroom));
        }

    }
    public void GetRoomNoFromAD()
    {
        callAndroidJava("GotoRoom");
    }
    public string h5RoomId = "";
    public void GotoRoom(string RoomID)
    {
        Debug.Log("这是从H5--AD 获得的RoomID》》>00>" + RoomID);
        if (RoomID != null && RoomID != "")
        {
            if (SceneManager.GetActiveScene().name.Equals("GameHall"))
            {
                GameObject obj = GameObject.Find("WaitStartGame(Clone)");
                if (!obj)
                {
                    UIState_GameHallPage.comeFromState = GameCore.UIState_GameHallPage.ComeFromState.FromH5;
                    SendMessageMgr.JoinRoom(RoomID);
                }
            }
            else
            {
                //走继续游戏逻辑
                h5RoomId = RoomID;
                UIState_GameHallPage.comeFromState = GameCore.UIState_GameHallPage.ComeFromState.FromH5;
                Debug.Log("这是从H5--AD 获得的RoomID》》11>>" + RoomID);
            }
        }

    }
    public void SendExitRoomDaili()
    {
        com.guojin.mj.net.message.game.VoteDelStart VDS = new com.guojin.mj.net.message.game.VoteDelStart();
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(VDS));
    }
    IEnumerator GetRoomIDFromAD()
    {
        yield return new WaitForSeconds(1f);
        this.callAndroidJava("GetRoomIDFromAD");
    }
    //实时定位返回
    public void GetPlayerPlace2(string str)
    {
        Debug.Log("unity获取的实时地理位置" + str);
        List<string> p = str.Split('_').ToList();
        GameData.GetInstance().playerData.place = p[0];
        GameData.GetInstance().playerData.longitude = p[1];
        GameData.GetInstance().playerData.latitude = p[2];
        //向服务器发送经纬度
        ReqLocation req = ReqLocation.Creat(0, p[1],p[2]);
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(req));
    }
    void OnApplicationPause(bool isPause)
    {

#if UNITY_ANDROID && !UNITY_EDITOR

        if (!isPause && SceneManager.GetActiveScene().name == "GameHall")
        {
            Debug.Log("OnApplicationPause>>GetRoomIDFromAD>>  0");
            StartCoroutine(GetRoomIDFromAD());
            Debug.Log("OnApplicationPause>>GetRoomIDFromAD>>  1");
        }
        if (!isPause && SceneManager.GetActiveScene().name == "Horizontal")
        {
            CheckMicrophone();
        }
        if (!isPause && SceneManager.GetActiveScene().name == "GameHall")
        {
            GameObject obj = GameObject.Find("Canvas/addNode/WaitStartGame(Clone)");
            if (obj)
            {
                callAndroidJava("GetPlayerPlace2");
            }
        }
#elif UNITY_IPHONE

#endif

    }

    public AudioClip ac;

    Transform cam;
    private void Start()
    {
        cam = GameObject.Find("Camera").transform;
    }
    public void BtnVoice()
    {
        if (PlayerPrefs.GetFloat("AudioSet", 1) >=0.1f)
        {
            AudioSource.PlayClipAtPoint(ac, Vector3.zero);
        }

    }
}

