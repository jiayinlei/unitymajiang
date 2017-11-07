using com.guojin.core.io;
using com.guojin.core.io.message;
using com.guojin.mj.net.handler;
using com.guojin.mj.net.message;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SocketClient : MonoBehaviour, WebSocketUnityDelegate
{
    private MessageHandlerFactory msgHandler = MessageHandlerFactory.instance;
    private MessageFactoryImpi msgFactory = MessageFactoryImpi.instance;
    public WebSocketUnity webSocket;


    private static SocketClient instance = null;
    bool isInit = false;
    bool isNetOpen = false;
    public static bool isCreated = false;
    public static bool isWaitingForReceive = false;
    public static SocketClient GetInstance()
    {
        return SocketClient.instance;
    }

    void Awake()
    {
        if (SocketClient.isCreated == false)
        {
            SocketClient.instance = this;
            SocketClient.isCreated = true;
            DontDestroyOnLoad(this);
        }

    }
    //string ServerAdress = "ws://122.114.150.35:8010/g";
    string ServerAdress = "ws://192.168.1.82:8010/g";//王曦服务器
    //string ServerAdress = "ws://192.168.1.55:8010/g";//本地服务器

    public void InitNet()
    {
        if (this.isInit == false)
        {

            Debug.Log("[SocketMgr] 服务器连接地址: " + ServerAdress);
            webSocket = new WebSocketUnity(ServerAdress, this);
            webSocket.Open();
            this.MsgListInit();
            this.isInit = true;
        }
        else
        {
            Debug.Log("[NetSocketMgr] 网络已经链接");
        }

    }
    //  websocket is opened 的时候执行
    public void OnWebSocketUnityOpen(string sender)
    {
        this.isNetOpen = true;
        Debug.Log("[NetSocketMgr] WebSocket connected, " + sender);
        //ObserverMgr.DispatchMsg(GameGlobalMsg.NetOpen, null);
        //SocketMessageQueue.GetInstance().addMsg(GameGlobalMsg.NET_OPEN, null);
    }
    public void OnWebScoketciose()
    {
        isInit = false;
        webSocket.Close();
    }
    public void OnWebSocketUnityOnMessage(string sender)
    {
        Debug.Log("[NetSocketMgr] OnWebSocketUnityOnMessage");
    }
    // websocket is closed 的时候执行
    public void OnWebSocketUnityClose(string reason)
    {
        //ObserverMgr.DispatchMsg(GameGlobalMsg.NetClose, null);//断开网络发送消息
        //SocketMessageQueue.GetInstance().addMsg(GameGlobalMsg.NetClose, null);
        Debug.Log("WebSocket Close : " + reason);
        this.isNetOpen = false;
        isInit = false;
    }
    // websocket接收到 string 类型的message是执行
    public void OnWebSocketUnityReceiveMessage(string message)
    {
        Debug.Log("Received from server : " + message);
    }

    public void OnWebSocketUnityReceiveDataOnMobile(string base64EncodedData)
    {
        // it's a limitation when we communicate between plugin and C# scripts, we need to use string
        byte[] decodedData = webSocket.decodeBase64String(base64EncodedData);
        OnWebSocketUnityReceiveData(decodedData);
    }


    // This event happens when the websocket did receive data
    public void OnWebSocketUnityReceiveData(byte[] data)
    {
        //Debug.Log(com.guojin.core.utils.Utils.debug(new List<byte>(data)));
        com.guojin.core.io.Byte readBuffer = new com.guojin.core.io.Byte(new List<byte>(data));
        readBuffer.endian = com.guojin.core.io.Byte.BIG_ENDIAN;
        //Debug.Log("收到消息debug:{0}" + ',' + Utils.debug(readBuffer.buffer));
        readBuffer.pos = 0;
        MarkCompressInput in0 = MarkCompressInput.create(readBuffer);
        int type = in0.readInt();
        int id = in0.readInt();
        Message msg = msgFactory.getMessage(type, id);
        msg.decode(in0);
        Debug.Log("[NetSocketMgr]addNetMsg: type:" + type + ",id: " + id);



        List<string> strList = MessageFactoryImpi.instance.getMessageString(type, id).Split('_').ToList();
        if (strList.Count == 1)
        {

        }
        else
        {
            string str = strList[0];
            Data theSamedata = this.msgList.FirstOrDefault(p => str.Equals(MessageFactoryImpi.instance.getMessageString(p.type, p.id)));
            if (theSamedata != null)
            {
                //this.msgList.RemoveAt(0);
                //非主动推
                CallBack callback = theSamedata.callBack;
                this.msgList.Remove(theSamedata);
                this.msgCallBackList.Add(new MsgCallBackData() { type = type, id = id, msg = msg, callBack = callback });
            }
        }

        //else if (str.Equals("com.guojin.mj.net.message.login.SysSetting"))
        //{
        //    Debug.Log("主动推");


        //}

        //Data theSamedata = this.msgList.ElementAt(0);
        //this.msgList.RemoveAt(0);
        //CallBack callback = theSamedata.callBack;
        //this.msgCallBackList.Add(new MsgCallBackData() { type = type, id = id, msg = msg, callBack = callback });

        //SocketMessageQueue.GetInstance().addMsg(msg);//添加消息

    }

    // This event happens when you get an error@
    public void OnWebSocketUnityError(string error)
    {
        Debug.Log("[NetSocketMgr] socket error");
    }
    ////----------------------------------------------------------------------
    ////----------------------------------------------------------------------

    public delegate void CallBack(Message msg, int type, int id);
    /// <summary>
    /// 消息列表
    /// </summary>
    public List<Data> msgList { get; set; }
    public List<MsgCallBackData> msgCallBackList { get; set; }
    /// <summary>
    /// 消息机制
    /// </summary>
    public class Data
    {
        public int type { get; set; }
        public int id { get; set; }
        public CallBack callBack { get; set; }
        /// <summary>
        /// 加密数据
        /// </summary>
        public byte[] msg { get; set; }
    }
    public class MsgCallBackData
    {
        public int type { get; set; }
        public int id { get; set; }
        public CallBack callBack { get; set; }
        /// <summary>
        /// 加密数据
        /// </summary>
        public Message msg { get; set; }
    }
    public void MsgListInit()
    {
        this.msgList = new List<Data>();
        this.msgCallBackList = new List<MsgCallBackData>();
    }

    public void Send(Message msg, int type, int id, CallBack callBack, bool check = true)
    {
        if (check)
        {
            if (msgList != null && msgList.Any((p) => p.type == type && p.id == id))
            {
                Debug.LogError("不发送重复消息 TYPE=>" + type + "  ID=>" + id + "  msgStr=>" +
                                MessageFactoryImpi.instance.getMessageString(type, id));
                return;
            }
        }
        
        byte[] data = com.guojin.mj.net.Net.instance.write(msg);
        msgList.Add(new Data() { callBack = callBack, type = type, id = id, msg = data });
    }
    private void UpdateReceive()
    {
        if (this.msgCallBackList == null || this.msgCallBackList.Count == 0)
        {
            return;
        }
        isWaitingForReceive = false;
        MsgCallBackData msgCallBackData = this.msgCallBackList[0];
        msgCallBackData.callBack(msgCallBackData.msg, msgCallBackData.type, msgCallBackData.id);
        this.msgCallBackList.RemoveAt(0);

    }
    private void UpdateSend()
    {
        if (this.msgList == null || this.msgList.Count == 0)
        {
            ///不操作
            return;
        }
        if (isWaitingForReceive == false && this.isNetOpen)
        {
            Data data = msgList.ElementAt(0);

            try
            {
                isWaitingForReceive = true;
                this.webSocket.Send(data.msg);
                Debug.Log("信息发送成功type=>" + data.type + "  id=>" + data.id);

            }
            catch (Exception ex)
            {
                Debug.LogError("信息无法发送erro=>" + ex.Message);
            }
        }

    }
    public static void AddActivePushCall(Message msg,int type, int id)
    {
        //ReceiveMsgDic.Add(getType, callback);
    }
    private void Update()
    {
        if (isInit == true && webSocket != null)
        {
            UpdateSend();
            UpdateReceive();
        }
    }

}

