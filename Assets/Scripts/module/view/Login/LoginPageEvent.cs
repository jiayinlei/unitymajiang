using System.Collections;
using com.guojin.mj.net;
using com.guojin.mj.net.message.login;
using UnityEngine;
using UnityEngine.UI;
/*老版登录
#region 
public class LoginPageEvent : EventManager  {
    public override void InformationSetting()
    {
        SetActive(BindingSource[2], false);
        SetLable(BindingSource[3], "版本号：" + DefaultKeyConst.version);
#if UNITY_ANDROID && !UNITY_EDITOR
            LoginAndShare.Controller.GetRoomNoFromAD();
#elif UNITY_IPHONE

#endif
        StartCoroutine(waitLogin());
    }

    public void OnClickWeiXinLogin()
    {
        Debug.Log("OnClickWeiXinLogin");
        if (BindingSource[0].GetComponent<Toggle>().isOn)
        {
            //PlayerPrefs.DeleteAll();
#if UNITY_EDITOR || ANONYMOUS
            com.guojin.mj.net.message.login.Login Log = com.guojin.mj.net.message.login.Login.create("ANONYMOUS", null, null, "34.7545", null);
            SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(Log));

#elif WX_SDK
            LoginAndShare.Controller.OnClickWXLogin();
#endif
        }
        else
        {
            TooL.showTips("请勾选同意用户协议，感谢您的配合，祝您游戏愉快!", () => { AgreeBtnClick(); });
        }


    }
    IEnumerator waitLogin()
    {
        yield return new WaitForSeconds(1f);
        CheckToken();
    }
    void CheckToken()
    {
        //判断Socket连接状态
        bool isNetOpen =  SocketMgr.GetInstance().isNetOpen;
        if (isNetOpen)
        {
            string token = PlayerPrefs.GetString(DefaultKeyConst.VisitorTokenKey);
            if (!token.Equals(""))
            {
                string longitude = GameData.GetInstance().playerData.longitude;
                string latitude = GameData.GetInstance().playerData.latitude;
                Debug.Log("longitude>>" + longitude + "  latitude>>" + latitude);
                Login log = Login.create("TOKEN", token, null, longitude, latitude);
                SocketMgr.GetInstance().Send(Net.instance.write(log));
                StartCoroutine(HandleWxToken());
            }
            else
            {
                SetActive(BindingSource[2], true);
            }
        }
        else
        {
            TooL.showNetErroTips("网络无法连接,是否重试！",()=>
            {
                reConnect();

            });
        }

    }
    void reConnect()
    {
        SetActive(BindingSource[2], true);//显示登录按钮
        UIManager.ChangeUI(UIManager.PageState.NetConnectPage, (GameObject obj) =>
        {
            obj.GetComponent<NetConnectPageEvent>().isLogin = true;
            obj.GetComponent<NetConnectPageEvent>().Init();
            obj.GetComponent<NetConnectPageEvent>().isUpdateStop = false;
        });
    }
    //是否需要吊起微信
    public bool IsNeedWxPutOn = true;

    public IEnumerator HandleWxToken()
    {

        yield return new WaitForSeconds(1.5f);
        if (IsNeedWxPutOn)
        {
            LoginAndShare.Controller.OnClickWXLogin();
        }

    }

    private void AgreeBtnClick()
    {
        BindingSource[0].GetComponent<Toggle>().isOn = true; 
    }
    private void OpenAgreeMent()
    {
        SetActive(BindingSource[1], true);
    }
    private void ClosePlayerNotice()
    {
        SetActive(BindingSource[1], false);
        BindingSource[0].GetComponent<Toggle>().isOn = true;
    }
    private void OnVisitorLogin()
    {
        // 第一次登陆时匿名，之后登陆之前的匿名账号
        //PlayerPrefs.DeleteAll();
        string token = PlayerPrefs.GetString(DefaultKeyConst.VisitorTokenKey);
        //string token = "0904c35e-c692-4713-b395-0484a6fa8e34";
        string longitude = GameData.GetInstance().playerData.longitude;
        string latitude = GameData.GetInstance().playerData.latitude;
        Debug.Log(token);
        if (!token.Equals(""))
        {
            Login Log = Login.create("TOKEN", token, null, longitude, latitude);
            SocketMgr.GetInstance().Send(Net.instance.write(Log));
        }
        else
        {
            Login Log = Login.create("ANONYMOUS", null, null, longitude, latitude);
            SocketMgr.GetInstance().Send(Net.instance.write(Log));
        }
        
    }   
    void ClosePage()
    {
        Destroy(gameObject);
    }

}
#endregion
*/
public class LoginPageEvent : EventManager
{
    public override void InformationSetting()
    {
        SetActive(BindingSource[2], false);
        SetActive(BindingSource[4], false);
        SetActive(BindingSource[5], false);
        SetLable(BindingSource[3], "版本号：" + DefaultKeyConst.version);
#if UNITY_ANDROID && !UNITY_EDITOR
            LoginAndShare.Controller.GetRoomNoFromAD();
#elif UNITY_IPHONE

#endif

        ConnectSever();
    }
    void ConnectSever()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            TooL.showNetErroTips("无法连接网络！请打开wifi或4G网络后重试。", () =>
            {
                ConnectSever();

            });
        }
        else
        {
            SetActive(BindingSource[4], true);
            StartConnectSever();
        }

    }
    private void StartConnectSever()
    {
        StartCoroutine(ConnectSeverPerSecond());
        StartCoroutine(waitConnectSuccess());
    }
    int connectNum = 1;
    IEnumerator ConnectSeverPerSecond()
    {
        SocketMgr.GetInstance().isInit = false;
        SocketMgr.GetInstance().InitNet();
        SetActive(BindingSource[4], true);
        SetLable(BindingSource[4], string.Format("正在尝试连接服务器：{0}", connectNum.ToString()));
        connectNum++;
        yield return new WaitForSeconds(2f);
        //yield return new WaitUntil(() => SocketMgr.GetInstance().isNetOpen == false
        //                       && SocketMgr.GetInstance().isConnecting == false);
        StartCoroutine(ConnectSeverPerSecond());
    }
    IEnumerator waitConnectSuccess()
    {
        yield return new WaitUntil(() => SocketMgr.GetInstance().isNetOpen);
        SetActive(BindingSource[4],false);
        StopAllCoroutines();
        CheckToken();
    }
    /// <summary>
    /// 开始自动登录
    /// </summary>
    void CheckToken()
    {
        //判断Socket连接状态
        bool isNetOpen = SocketMgr.GetInstance().isNetOpen;
        if (isNetOpen)
        {
          // PlayerPrefs.DeleteKey(DefaultKeyConst.VisitorTokenKey);
            string token = PlayerPrefs.GetString(DefaultKeyConst.VisitorTokenKey);
            if (!token.Equals(""))
            {
                string longitude = GameData.GetInstance().playerData.longitude;
                string latitude = GameData.GetInstance().playerData.latitude;
                Debug.Log("longitude>>" + longitude + "  latitude>>" + latitude);
                Login log = Login.create("TOKEN", token, null, longitude, latitude);
                SocketMgr.GetInstance().Send(Net.instance.write(log));
                StartCoroutine(HandleWxToken());
            }
            else
            {
                SetActive(BindingSource[2], true);
                SetActive(BindingSource[5], true);
            }
        }
        else
        {
            StartConnectSever();
        }

    }

    public void OnClickWeiXinLogin()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            TooL.showNetErroTips("无法连接网络！请打开wifi或4G网络后重试。", () =>
            {
                ConnectSever();

            });
        }
        else
        {
            Debug.Log("OnClickWeiXinLogin");
            if (BindingSource[0].GetComponent<Toggle>().isOn)
            {
                //PlayerPrefs.DeleteAll();
#if UNITY_EDITOR || ANONYMOUS
                com.guojin.mj.net.message.login.Login Log = com.guojin.mj.net.message.login.Login.create("ANONYMOUS", null, null, "34.7545", null);
                SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(Log));

#elif WX_SDK
            LoginAndShare.Controller.OnClickWXLogin();
#endif
            }
            else
            {
                TooL.showTips("请勾选同意用户协议，感谢您的配合，祝您游戏愉快!", () => { AgreeBtnClick(); });
            }
        }

        


    }



    
    void reConnect()
    {
        SetActive(BindingSource[2], true);//显示登录按钮
        UIManager.ChangeUI(UIManager.PageState.NetConnectPage, (GameObject obj) =>
        {
            obj.GetComponent<NetConnectPageEvent>().isLogin = true;
            obj.GetComponent<NetConnectPageEvent>().Init();
            obj.GetComponent<NetConnectPageEvent>().isUpdateStop = false;
        });
    }
    //是否需要吊起微信
    public bool IsNeedWxPutOn = true;

    public IEnumerator HandleWxToken()
    {

        yield return new WaitForSeconds(1.5f);
#if UNITY_EDITOR
        com.guojin.mj.net.message.login.Login Log = com.guojin.mj.net.message.login.Login.create("ANONYMOUS", null, null, "34.7545", null);
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(Log));
#else
        if (IsNeedWxPutOn)
        {
            // LoginAndShare.Controller.OnClickWXLogin();
            PlayerPrefs.DeleteAll();
            InformationSetting();
        }
#endif
    }

    private void AgreeBtnClick()
    {
        BindingSource[0].GetComponent<Toggle>().isOn = true;
    }
    private void OpenAgreeMent()
    {
        SetActive(BindingSource[1], true);
    }
    private void ClosePlayerNotice()
    {
        SetActive(BindingSource[1], false);
        BindingSource[0].GetComponent<Toggle>().isOn = true;
    }
    private void OnVisitorLogin()
    {
        // 第一次登陆时匿名，之后登陆之前的匿名账号
        PlayerPrefs.DeleteAll();
        string token = PlayerPrefs.GetString(DefaultKeyConst.VisitorTokenKey);
        //string token = "0904c35e-c692-4713-b395-0484a6fa8e34";
        string longitude = GameData.GetInstance().playerData.longitude;
        string latitude = GameData.GetInstance().playerData.latitude;
        Debug.Log(token);
        if (!token.Equals(""))
        {
            Login Log = Login.create("TOKEN", token, null, longitude, latitude);
            SocketMgr.GetInstance().Send(Net.instance.write(Log));
        }
        else
        {
            Login Log = Login.create("ANONYMOUS", null, null, longitude, latitude);
            SocketMgr.GetInstance().Send(Net.instance.write(Log));
        }

    }
    void ClosePage()
    {
        Destroy(gameObject);
    }

}