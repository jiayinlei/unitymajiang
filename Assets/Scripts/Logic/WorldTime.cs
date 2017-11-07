/* ============================================================================
** Author	: 1130282072@qq.com
** Comments	: 世界时间，心跳包，重连
** ----------------------------------------------------------------------------
** History	:	DATE			DESC
**				2017-7-23		Create by sunfei
** ============================================================================
*/

using GameCore;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldTime : MonoBehaviour
{
    public PlaybackLayer pb;

    /// <summary>
    /// 单键
    /// </summary>
    private static WorldTime _current = null;

    public static WorldTime Current
    {
        get
        {
            if (_current == null)
            {
                _current = FindObjectOfType<WorldTime>();
            }
            return _current;
        }
    }

    private int LocalTime;
    public  bool isUpdateStop = false;
    private bool isNetErro = false;
    private float t = 0;
    public bool isHallReConnect = false;
    public bool isConnecting = false;
    public bool doNothing = false;
    private void Update()
    {
        if (!SceneManager.GetActiveScene().name.Equals("Login") && !pb.inPlayback)
        {
            t += Time.deltaTime;
            if (t >= 3 && isUpdateStop == false && !isConnecting)
            {
                t = 0;
                isUpdateStop = true;
                UpdateTime();
            }
            if (!doNothing)
            {
                StartCoroutine(Open());
                doNothing = true;
            }
        }

    }
    public IEnumerator Open()
    {
        if (!SocketMgr.GetInstance().isNetOpen)
        {
            StartCoroutine(TimeOut2(0f));
            StopCoroutine(Open());
            yield return new WaitForSeconds(0.1f);

        }
        else
        {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(Open());
            
        }
        

    }
    private void UpdateTime()
    {
        StartCoroutine(TimeOut(1f));
        com.guojin.mj.net.message.login.Ping ping = com.guojin.mj.net.message.login.Ping.create("1");
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(ping));
    }

    public void Stoptimeout()
    {
        StopCoroutine("TimeOut");
    }

    public IEnumerator TimeOut(float t)
    {
        
        yield return new WaitForSeconds(t);
        //Debug.Log("TimeOut==>>"+t);
        if (isUpdateStop == true)
        {
            Debug.Log("心跳包服务器超2秒未响应");
            GameObject obj_0 = GameObject.Find("NetConnectPage(Clone)");
            GameObject obj_1 = GameObject.Find("DNNetConnectPage(Clone)");
            Method.isDuanwang = true;
            //Application.StreamingAssets
            if (obj_0|| obj_1)
            {
            }
            else
            {
                isConnecting = true;
                SocketMgr.GetInstance().CloseSocket();
                if (SceneManager.GetActiveScene().name == "GameHall")
                {
                    isHallReConnect = true;
                }
                else
                {
                    isHallReConnect = false;
                }
                if (SceneManager.GetActiveScene().name == "BullFight") {

                    UIManager.ChangeUI(UIManager.PageState.DNNetConnectPage, (GameObject obj) => {
                        obj.GetComponent<NetConnectPageEvent>().Init();
                        obj.GetComponent<NetConnectPageEvent>().isUpdateStop = false;
                        Assets.Scripts.BullFight.Manager.GameManager.instance.NetClose();
                    });
                } else {
                    UIManager.ChangeUI(UIManager.PageState.NetConnectPage, (GameObject obj) => {
                        obj.GetComponent<NetConnectPageEvent>().Init();
                        obj.GetComponent<NetConnectPageEvent>().isUpdateStop = false;
                    });
                }
            }
        }
    }
    public IEnumerator TimeOut2(float t)
    {

        yield return new WaitForSeconds(t);
        //Debug.Log("TimeOut==>>" + t);
        if (isUpdateStop == true)
        {
            Debug.Log("心跳包服务器超2秒未响应");
            GameObject obj_0 = GameObject.Find("NetConnectPage(Clone)");
            Method.isDuanwang = true;
            if (obj_0)
            {
            }
            else
            {
                isConnecting = true;
                SocketMgr.GetInstance().CloseSocket();
                if (SceneManager.GetActiveScene().name == "GameHall")
                {
                    isHallReConnect = true;
                }
                else
                {
                    isHallReConnect = false;
                }
                if (SceneManager.GetActiveScene().name == "BullFight") {

                    UIManager.ChangeUI(UIManager.PageState.DNNetConnectPage, (GameObject obj) => {
                        obj.GetComponent<NetConnectPageEvent>().Init();
                        obj.GetComponent<NetConnectPageEvent>().isUpdateStop = false;
                        Assets.Scripts.BullFight.Manager.GameManager.instance.NetClose();
                    });
                }
                else {
                    UIManager.ChangeUI(UIManager.PageState.NetConnectPage, (GameObject obj) => {
                        obj.GetComponent<NetConnectPageEvent>().Init();
                        obj.GetComponent<NetConnectPageEvent>().isUpdateStop = false;
                    });
                }
            }
        }
    }
    public void CheckHeartBeat(string serverTime)
    {
        //int int_serverTime = int.Parse(serverTime);
        //Debug.Log("心跳包服务器返回时间 ==》》" + int_serverTime.ToString());
        //if ((int_serverTime - LocalTime) > 3)
        //{
        //    Debug.Log("心跳超时》》需要重连！！！！");
        //    ConnectSever();
        //    UIManager.ChangeUI(UIManager.PageState.NetConnectPage, (GameObject obj) =>
        //    {
        //        obj.GetComponent<NetConnectPageEvent>().Init();

        //    });
        //}
        isUpdateStop = false;
        //Debug.Log("服务器心跳包返回时间 ==》》" + serverTime);
    }

    /// <summary>
    /// 初始化事件 login时候调用刷新时间
    /// </summary>
    public void InitTime()
    {
    }

    /// <summary>
    /// 注销时使用
    /// </summary>
    public void ResetTime()
    {
    }
    private void ReLogin()
    {
    }
    void OnApplicationPause(bool isPause)
    {
        if (!isPause )
        {
            if (SceneManager.GetActiveScene().name != "Login")
            {
                Debug.Log("time==>>" + t);
                t = 4;
            }
        }
    }
  }