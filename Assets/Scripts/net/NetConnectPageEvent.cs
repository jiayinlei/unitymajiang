using GameCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetConnectPageEvent : EventManager
{
    public static int[] rotation = { 0,-10,-20, -30,-40,-50, -60,-70,-80, -90,-100,-110 ,-120,-130,-140 ,-150,-160,-170, -180,
        170,160,150,140,130, 120,110,100, 90,80,70, 60,50,40, 30,20,10 };

    public bool isUpdateStop = true;
    public override void InformationSetting()
    {

    }
    /// <summary>
    /// 后台切换回调
    /// </summary>
    public static Action BackFromBackgroud { get; set; }
    /// <summary>
    /// 超时时间设置
    /// </summary>
    /// <param name="TimeOut"></param>
    public void Init()
    {
        if (SceneManager.GetActiveScene().name == "Horizontal")
        {
            SetActive(this.BindingSource[1], true);
            SetActive(this.BindingSource[0], false);
            StartCoroutine(this.StoryBegin(this.BindingSource[1].GetComponent<Image>()));
            SetActive(this.BindingSource[3], true);
            SetActive(this.BindingSource[2], false);
        }
        else
        {
            SetActive(this.BindingSource[0], true);
            SetActive(this.BindingSource[1], false);
            StartCoroutine(this.StoryBegin(this.BindingSource[0].GetComponent<Image>()));
            SetActive(this.BindingSource[3], false);
            SetActive(this.BindingSource[2], true);
            StartCoroutine(PlayText(0));
        }
        SocketMgr.GetInstance().isNetOpen = false;
        ConnectSever();
    }
    private string[] stringArr = { ". ", ". .", ". . ." };
    IEnumerator PlayText(int index)
    {
        SetLable(this.BindingSource[4], stringArr[index]);
        yield return new WaitForSeconds(0.5f);
        index++;
        if (index >= stringArr.Length) index = 0;
        StartCoroutine(PlayText(index));
    }
    /// <summary>
    /// 旋转动画
    /// </summary>
    /// <param name="sprite"></param>
    /// <returns></returns>
    IEnumerator StoryBegin(Image image)
    {
        int index = 0;
        int len = rotation.Length;
        while (true)
        {
            image.transform.localRotation = Quaternion.Euler(0, 0, rotation[index]);
            index++;
            if (index >= len)
            {
                index = 0;
            }
            yield return new WaitForSeconds(0.03f);
        }
    }
    /// <summary>
    /// Token登录超时设置,需要重新挂起微信登录
    /// </summary>
    /// <param name="TimeOut"></param>
    /// <returns></returns>
    IEnumerator TimeOut(int TimeOut)
    {
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        yield return new WaitForSeconds(TimeOut);
        stopwatch.Stop();
        Debug.Log(" Net WaitForSeconds" + stopwatch.Elapsed.TotalSeconds);
        if (BackFromBackgroud == null)
        {
            //MySmallInforController.NetworkErrorPopup(NetworkErrorEventState);
            TooL.showNetErroTips("连接超时，需要重新登录游戏！", () => LoginAgain());
        }
        else
        {
            BackFromBackgroud();
            BackFromBackgroud = null;
        }
        yield return 0;
        this.ClosePopup();
    }
    void LoginAgain()
    {
        //LoginPageEvent _loginPageEvent = FindObjectOfType<LoginPageEvent>();
        //_loginPageEvent.OnClickWeiXinLogin();
        GameObject staticSource = GameObject.Find("StaticSource");
        int uiStateId = staticSource.GetComponent<MainLogic>().uiStateManager.CurrentSubStateID();
        if (uiStateId != (int)UIState.UIState_LoginPage && uiStateId != (int)UIState.UIState_LoadingPage)
        {
            //staticSource.GetComponent<MainLogic>().ChangeState((int)UIState.UIState_LoginPage);
            DestroyImmediate(staticSource);

            //PlayerPrefs.DeleteKey(DefaultKeyConst.VisitorTokenKey);
            LoadingScenePageEvent.callBack = delegate { };
            SceneManager.LoadScene("Login");
        }
    }
    /// <summary>
    /// 关闭页面
    /// </summary>
    public void ClosePopup()
    {
        Destroy(this.gameObject);
    }

    public bool isLogin = false;
    void Start()
    {
        //SocketMgr.GetInstance().isNetOpen = false;
        //ConnectSever();
    }
    void ConnectSever()
    {
        /*
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            TooL.showNetErroTips("无法连接网络！请打开wifi或4G网络后重试。", () =>
            {
                ConnectSever();

            });
        }
        else
        {
            StartConnectSever();
        }
        */
        StartCoroutine(waitfour());
        StartConnectSever();
    }
    private void StartConnectSever()
    {
        StartCoroutine(ConnectSeverPerSecond());
        //StartCoroutine(waitConnectSuccess());
    }
    IEnumerator ConnectSeverPerSecond()
    {
        #region//第一版
        //SocketMgr.GetInstance().isInit = false;
        //SocketMgr.GetInstance().InitNet();
        //yield return new WaitUntil(() => SocketMgr.GetInstance().isNetOpen == false 
        //                              && SocketMgr.GetInstance().isConnecting == false);
        //StartCoroutine(ConnectSeverPerSecond());
        #endregion
        Debug.Log("拼命连接。。。。。");
        SocketMgr.GetInstance().isInit = false;
        SocketMgr.GetInstance().InitNet();
        yield return new WaitUntil(() => SocketMgr.GetInstance().isConnecting == false);
        if (SocketMgr.GetInstance().isNetOpen)
        {
            CheckToken();
        }
        else
        {
            StartCoroutine(ConnectSeverPerSecond());
        }


        //yield return StartCoroutine("SomeCortoutineMethod");
    }


    IEnumerator waitfour()
    {
        yield return new WaitForSeconds(4f);
        StopCoroutine(ConnectSeverPerSecond());
        StartCoroutine(ConnectSeverPerSecond());
        StartCoroutine(waitfour());
    }
    //IEnumerator waitConnectSuccess()
    //{
    //    yield return new WaitUntil(() => SocketMgr.GetInstance().isNetOpen);
    //    StopCoroutine(ConnectSeverPerSecond());
    //    StopCoroutine(waitConnectSuccess());
    //    CheckToken();
    //    //StopAllCoroutines();

    //}
    void CheckToken()
    {
        //判断Socket连接状态
        bool isNetOpen = SocketMgr.GetInstance().isNetOpen;
        if (isNetOpen)
        {
            StopCoroutine(ConnectSeverPerSecond());
            StopCoroutine(waitfour());
            string token = PlayerPrefs.GetString(DefaultKeyConst.VisitorTokenKey);
            if (!token.Equals(""))
            {
                //token = PlayerPrefs.GetString(DefaultKeyConst.VisitorTokenKey);
                //if (DNGlobalData.isChongLian) {
                //    if (!token.Equals("")) {
                //        string longitude = GameData.GetInstance().playerData.longitude;
                //        string latitude = GameData.GetInstance().playerData.latitude;
                //        com.guojin.mj.net.message.login.Login Log = com.guojin.mj.net.message.login.Login.create("TOKEN", token, null, longitude, latitude);
                //        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(Log));
                //    }
                //}
                string longitude = GameData.GetInstance().playerData.longitude;
                string latitude = GameData.GetInstance().playerData.latitude;
                com.guojin.mj.net.message.login.Login Log = com.guojin.mj.net.message.login.Login.create("TOKEN", token, null, longitude, latitude);
                SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(Log));
                StartCoroutine(TimeOut(6));
            }
        }
        else
        {
            StartConnectSever();
        }

    }
    // Update is called once per frame
    //void Update () {

    //    t += Time.deltaTime;
    //    if (isUpdateStop == false  && t >= 2)
    //    {
    //        t = 0;
    //        SocketMgr.GetInstance().isInit = false;
    //        SocketMgr.GetInstance().InitNet();
    //        if (SocketMgr.GetInstance().isNetOpen)
    //        {
    //            Debug.Log("SocketMgr.GetInstance().NetOpen ==>>true");
    //        }
    //        if (isLogin)
    //        {
    //            Debug.Log("isLogin ==>>true");
    //        }
    //        if (SocketMgr.GetInstance().isNetOpen && !isLogin)
    //        {
    //            Method.isErrorTiShi = true;
    //            StartCoroutine(waitOne());
    //        }
    //        else if (SocketMgr.GetInstance().isNetOpen && isLogin)
    //        {
    //            DestroyImmediate(this.gameObject);
    //        }

    //    }

    //}
}
