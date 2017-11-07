using System.Collections.Generic;
using com.guojin.core.io;
using com.guojin.core.io.message;
using com.guojin.core.utils;
using com.guojin.mj.net;
using com.guojin.mj.net.message;
using com.guojin.mj.net.handler;
using UnityEngine;

namespace com.guojin.mj.net
{
    /*
     * 网络操作（模拟）
     */
    class Net:MonoBehaviour
    {
        public static Net instance = new Net();
        //public WebSocketUnity websocket1;
        //public WebSocket websocket;
        private MessageFactoryImpi msgFactory = MessageFactoryImpi.instance;
        private MessageHandlerFactory msgHandler = MessageHandlerFactory.instance;
        /**
         *
         * @throws SingletonError
         */
        public Net()
        {
            if (instance != null)
            {
                throw new System.Exception("ResourceManager 是单例模式");
            }
        }
        private void connectHandler(object sender, System.EventArgs e)
        {
            
        }       
        public byte[] write(Message msg)
        {
            Byte buffer = new Byte();
            buffer.endian = Byte.BIG_ENDIAN;
            buffer.length = 0;
            buffer.pos = 0;
            MarkCompressOutput _out = MarkCompressOutput.create(buffer);
            _out.writeInt(msg.getMessageType());
            _out.writeInt(msg.getMessageId());
            //_out.writeString(msg.toString());
            msg.encode(_out);
            if (buffer.length > MessageProtocol.MESSAGE_MAX)
            {
                throw new ProtocolException("严重错误，消息长度超过最大值:" + MessageProtocol.MESSAGE_MAX);
            }

            buffer.pos = 0;         
            byte[] byteArr = buffer.buffer.ToArray(); 
            string msgString = msg.toString();
            int type =msg.getMessageType();
            int id = msg.getMessageId();
            if (!(type==7&&id==14))
            {
                Debug.Log("[Net] send type:" + type + ", id:" + id + ", msg: " + msgString);
            }
            return byteArr;
        }

        

    }
}