using UnityEngine;
using System.Collections.Generic;
using com.guojin.core.io;
using com.guojin.core.io.message;
using com.guojin.mj.net.handler;
using com.guojin.mj.net.message;

public class SocketMgr : MonoBehaviour, WebSocketUnityDelegate {

    // boolean to manage display
    private bool sendingMessage = false;
    private bool receivedMessage = false;
    private MessageHandlerFactory msgHandler = MessageHandlerFactory.instance;
    private MessageFactoryImpi msgFactory = MessageFactoryImpi.instance;
    public WebSocketUnity webSocket;


    private static SocketMgr instance = null;
    public bool isClose = false;
    public bool isInit = false;
    public bool isNetOpen = false;
    public static bool isCreated = false;
    public bool isConnecting = true;
    public static SocketMgr GetInstance() {
        return SocketMgr.instance;

    }

    void Awake() {
        if (SocketMgr.isCreated == false) {
            SocketMgr.instance = this;
            SocketMgr.isCreated = true;
            DontDestroyOnLoad(this);
            
            //SocketMessageQueue.GetInstance().addMsg(GameGlobalMsg.StartNet, null);
        }

    }
    public void InitNet() {
        if (this.isInit == false) {
            isConnecting = true;
            Debug.Log("[SocketMgr] 服务器连接地址: " + URLConst.ServerAdress);
            webSocket = new WebSocketUnity(URLConst.ServerAdress, this);
            webSocket.Open();        
            isClose = false;
            this.isInit = true;
        } else {
            Debug.Log("[NetSocketMgr] 网络已经链接");
        }

    }
    public void CloseSocket()
    {
        webSocket.Close();
    }
    private List<byte[]> mesdataList = new List<byte[]>();
    public void Send(byte[] data) {
        if (data != null) {
            this.webSocket.Send(data);
            //if (isNetOpen)
            //{
            //    this.webSocket.Send(data);
            //}
            //else
            //{
            //    mesdataList.Insert(0,data);
            //    GameObject obj_0 = GameObject.Find("NetConnectPage(Clone)");
            //    if (obj_0)
            //    {
            //        Debug.Log("正在重连。。。。。");
            //    }
            //    else
            //    {
            //        UIManager.ChangeUI(UIManager.PageState.NetConnectPage, (GameObject obj) =>
            //        {
            //            obj.GetComponent<NetConnectPageEvent>().Init();
            //            obj.GetComponent<NetConnectPageEvent>().isUpdateStop = false;
            //        });
            //    }

            //}
            
        }
    }
    //  websocket is opened 的时候执行
    public void OnWebSocketUnityOpen(string sender) {
        this.isNetOpen = true;
        isConnecting = false;
        Debug.Log("[NetSocketMgr] WebSocket connected, " + sender);
    }
    public void OnWebScoketclose() {
        isNetOpen = false;
        isInit = false;
        isConnecting = false;
        webSocket.Close();
    }
    public void OnWebSocketUnityOnMessage(string sender) {
        Debug.Log("[NetSocketMgr] OnWebSocketUnityOnMessage");
    }
    // websocket is closed 的时候执行
    public void OnWebSocketUnityClose(string reason) {
        Debug.Log("WebSocket Close : " + reason);
        isInit = false;
        isNetOpen = false;
        isConnecting = false;
    }
    // websocket接收到 string 类型的message是执行
    public void OnWebSocketUnityReceiveMessage(string message) {
        Debug.Log("Received from server : " + message);
    }

    public void OnWebSocketUnityReceiveDataOnMobile(string base64EncodedData) {
        // it's a limitation when we communicate between plugin and C# scripts, we need to use string
        byte[] decodedData = webSocket.decodeBase64String(base64EncodedData);
        OnWebSocketUnityReceiveData(decodedData);
    }

    private void Update() {
        SocketMessageQueue.GetInstance().UpdateQueue();
    }
    // This event happens when the websocket did receive data
    public void OnWebSocketUnityReceiveData(byte[] data) {
        Byte readBuffer = new Byte(new List<byte>(data));
        readBuffer.endian = Byte.BIG_ENDIAN;
        readBuffer.pos = 0;
        MarkCompressInput in0 = MarkCompressInput.create(readBuffer);
        int type = in0.readInt();
        int id = in0.readInt();
        Message msg = msgFactory.getMessage(type, id);
        if (Method.CanAddMessage && id != 15)//&& !GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback)
        {
            Method.messagelist.Add(msg);
        }
        msg.decode(in0);  
        if (!(type==7&&id==15))
        {
            Debug.Log("[NetSocketMgr]addNetMsg: type:" + type + ",id: " + id);
        }
        SocketMessageQueue.GetInstance().addMsg(msg);//添加消息
    }

    // This event happens when you get an error@
    public void OnWebSocketUnityError(string error) {
        Debug.Log("[NetSocketMgr] socket error");
        isClose = true;
        isInit = false;
        isNetOpen = false;
        isConnecting = false;
    }
}
