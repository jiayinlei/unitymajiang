using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text;
using System;
using com.guojin.mj.net.message.game;
/* ***********************************************
 * Describe: 录音WebSocket
 * Author : 赵中阳
 * Email: 1148312315@qq.com
 * DATA: 2017/7/28 16:00:21 
 * FileName: ocordWebSocket
 * Version: V1.0.1
 * ***********************************************/
public class RocordWebSocket : MonoBehaviour, WebSocketUnityDelegate {

    private bool  isInit = false;
    public static bool isClose = false;
    private string ServerAdress = "ws://121.40.90.67:8091/";
    public static WebSocketUnity webSocket;
    public string FileAdress = "";
    public int msgType = 0;
    public static bool isNetOpen = false;
    private static RocordWebSocket _instance = null;
    private RocordWebSocket() { }
    public static RocordWebSocket GetInstance() {
        if (_instance == null) {
            _instance = new RocordWebSocket();
        }
        return _instance;
    }


    void Start() {

        InitNet();
        InitPlayerInfo();
    }
    public void InitPlayerInfo()
    {
       FileAdress = "";
        msgType = 4;
    }


    public void InitNet() {
        if (this.isInit == false) {
#if UNITY_ANDROID
            webSocket = new WebSocketUnity(ServerAdress, this);
            webSocket.Open();
#elif UNITY_IPHONE
            LoginAndShare.Controller.ReCreate(ServerAdress, transform.gameObject.name);
            LoginAndShare.Controller.ReConnect();
#endif
            isClose = false;
            this.isInit = true;

        } else {
            Debug.Log("[NetSocketMgr] 网络已经链接");
        }
    }


    //发送录音文件字节流
    public void Send(byte[] data) {
        string result = Convert.ToBase64String(data);
        if (isNetOpen) {
#if UNITY_ANDROID
            webSocket.Send("{ \"type\":\"ogg\",\"data\":\"data:audio/x-wav;base64," + result + "\"}");
#elif UNITY_IPHONE
            LoginAndShare.Controller.ReSend("{ \"type\":\"ogg\",\"data\":\"data:audio/x-wav;base64," + result + "\"}");
#endif
        } else {
            chatStr.Add(result);
        }
    }
    public static List<string> chatStr = new List<string>();
    float t = 0;
    void Update()
    {
        t += Time.deltaTime;
        if (t >= 5)
        {
            t = 0;
            if (!isNetOpen)
            {
                isInit = false;
                InitNet();
            }
            else if (chatStr.Count > 0)
            {
#if UNITY_ANDROID
                webSocket.Send("{ \"type\":\"ogg\",\"data\":\"data:audio/x-wav;base64," + chatStr[0] + "\"}");
#elif UNITY_IPHONE
                LoginAndShare.Controller.ReSend("{ \"type\":\"ogg\",\"data\":\"data:audio/x-wav;base64," + chatStr[0] + "\"}");
#endif
                chatStr.RemoveAt(0);
            }
        }
    }
    public void OnWebSocketUnityOpen(string sender) {
        Debug.Log("语音聊天socket连接成功>>" + sender);
        isNetOpen = true;
    }

    public void OnWebSocketUnityClose(string reason) {
        Debug.Log("语音聊天socket断开>>" + reason);
        isInit = false;
        isClose = true;
        isNetOpen = false;
    }

    public void OnWebSocketUnityReceiveMessage(string message) {
        Debug.Log("接收返回消息" + message);
        PlayerSendChatInfo req = PlayerSendChatInfo.create(message, msgType);
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(req));
    }

    public void OnWebSocketUnityReceiveDataOnMobile(string base64EncodedData)
    {
        Debug.Log("接收返回消息" + base64EncodedData);
        PlayerSendChatInfo req = PlayerSendChatInfo.create(base64EncodedData, msgType);
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(req));
    }

    public void OnWebSocketUnityReceiveData(byte[] data) {
    }

    public void OnWebSocketUnityError(string error) {
        Debug.Log("语音聊天socketError=>>" + error);
        isClose = true;
        isInit = false;
        isNetOpen = false;
    }
}
