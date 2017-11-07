using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.guojin.core.io.message;
using com.guojin.mj.net.message;
using UnityEngine;

class SocketMessageQueue
{
    public Queue<Dictionary<string, Message>> netListMsg = new Queue<Dictionary<string, Message>>();



    private static SocketMessageQueue instance = new SocketMessageQueue();
    public static SocketMessageQueue GetInstance()
    {
        return SocketMessageQueue.instance;
    }
    public void UpdateQueue()
    {
        int count = this.netListMsg.Count;
        if (count > 0)
        {
            Dictionary<string, Message> dealMsg = this.netListMsg.Dequeue();//从头取出

            foreach (KeyValuePair<string, Message> msg in dealMsg)
            {
                string dispatchMsg = msg.Key;
                Message dispatchData = msg.Value;

                if (dispatchData == null)// 派发本地消息
                {
                    Debug.Log("[MsgQueue] 处理本地消息:" + dispatchMsg);
                } 
                else// 派发网络消息
                {
                    int type = dispatchData.getMessageType();
                    int id = dispatchData.getMessageId();
                    if (!(type==7 && id==15))
                    {
                        Debug.Log("[MsgQueue] 处理网络消息[type:" + type + ", id:" + id
                         + ",msg: " + dispatchMsg + ",data: " + dispatchData.toString());
                    }

                }
                ObserverMgr.DispatchMsg(dispatchMsg, dispatchData);

            }
        }


    }




    // 添加网络消息
    public void addMsg(Message msg)
    {
        // <stirng, Message>
        int type = msg.getMessageType();
        int id = msg.getMessageId();
        string messageString = MessageFactoryImpi.instance.getMessageString(type, id);

        this.addMsg(messageString, msg);
    }



    // 添加本地消息
    public void addMsg(string msg, Message data)
    {
        Dictionary<string, Message> item = new Dictionary<string, Message>();
        item.Add(msg, data);
        this.netListMsg.Enqueue(item); // 将对象添加到 Queue 的结尾处。
    }
}
