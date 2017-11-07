using com.guojin.core.io;
using com.guojin.core.io.message;
using com.guojin.dn.net.message;
using com.guojin.mj.net.message;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DNVoiceSocket : MonoBehaviour,WebSocketUnityDelegate {
    private bool isInit = false;
    private bool isClose = false;
    private string ServerAdress = "ws://121.40.90.67:8091/";
    private static WebSocketUnity webSocket;

    private static DNVoiceSocket _instance = null;
    public static DNVoiceSocket GetInstance() {
        if (_instance == null) {
            _instance = new DNVoiceSocket();
        }
        return _instance;
    }
    public void InitNet() {
        if (this.isInit == false) {

            Debug.Log("[SocketMgr] 服务器连接地址: " + ServerAdress);
            webSocket = new WebSocketUnity(ServerAdress, this);
            webSocket.Open();
            isClose = false;
            this.isInit = true;
        } else {
            Debug.Log("[NetSocketMgr] 网络已经链接");
        }

    }
    void Start() {
        
        webSocket = new WebSocketUnity(ServerAdress, this);
        InitNet();
    }
    //发送录音文件字节流
    public void Send(byte[] data) {
        //Debug.Log("发送录音消息");
        string result = Convert.ToBase64String(data);
        webSocket.Send("{ \"type\":\"ogg\",\"data\":\"data:audio/x-wav;base64," + result + "\"}");
    }
    public void OnWebSocketUnityClose(string reason) {
        isInit=false;
    }

    public void OnWebSocketUnityError(string error) {
        throw new NotImplementedException();
    }

    public void OnWebSocketUnityOpen(string sender) {
        isClose = false;
        Debug.Log("connect");
    }

    public void OnWebSocketUnityReceiveData(byte[] data) {
        Debug.Log("ReceiveData");
        //com.guojin.core.io.Byte readBuffer = new com.guojin.core.io.Byte(new List<byte>(data));
        //readBuffer.endian = com.guojin.core.io.Byte.BIG_ENDIAN;
        ////Debug.Log("收到消息debug:{0}" + ',' + Utils.debug(readBuffer.buffer));
        //readBuffer.pos = 0;
        //MarkCompressInput in0 = MarkCompressInput.create(readBuffer);
        //int type = in0.readInt();
        //int id = in0.readInt();
        //Message msg = MessageFactoryImpi.instance.getMessage(type, id);
        //msg.decode(in0);
        //Debug.Log("[NetSocketMgr]addNetMsg: type:" + type + ",id: " + id);
    }

    public void OnWebSocketUnityReceiveDataOnMobile(string base64EncodedData) {
        Debug.Log("ReceiveMobile");
        DouniuChat req = new DouniuChat();
        req.setChatContent(base64EncodedData);
        req.setIndex(4);
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(req));
    }
    public void OnWebSocketUnityReceiveMessage(string message) {
        Debug.Log("ReceiveMessage");
        DouniuChat req = new DouniuChat();
        req.setChatContent(message);
        req.setIndex(4);
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(req));
    }

    // Use this for initialization
    
	
	// Update is called once per frame
	//void Update () {
		
	//}
}
