using UnityEngine;
using System.Collections;
using com.guojin.core.io.message;
using com.guojin.core.io;
using System;
namespace com.guojin.mj.net.message.game
{
    public class PlayerSendChatInfo : Message
    {
        //发送聊天信息
        public static int TYPE = 1;
        public static int ID = 24;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendChatInfo"></param>
        /// <param name="num">1,文字 2，表情 3，语音</param>
        /// <returns></returns>
        public static PlayerSendChatInfo create(string sendChatInfo,int num) 
        {
            PlayerSendChatInfo playerSendChatInfo = new PlayerSendChatInfo();
            playerSendChatInfo.SendChatInfo = sendChatInfo;
            playerSendChatInfo.num = num;
            return playerSendChatInfo;
        }
        protected string sendChatInfo;
        protected int num;

        protected string SendChatInfo
        {
            get { return sendChatInfo;}
            set {sendChatInfo = value;}
        }

        protected int Num
        {
            get{ return num; }
            set {num = value;}
        }

        public void decode(com.guojin.core.io.Input _in)
        {
            sendChatInfo = _in.readString();
            num = _in.readInt();
        }

        public void encode(Output _out)
        {
            _out.writeString(sendChatInfo);
            _out.writeInt(num);
        }

        public int getMessageType()
        {
            return TYPE;
        }

        public int getMessageId()
        {
            return ID;
        }

        public string toString()
        {
            return "PlayerSendChatInfo [sendChatInfo"+ sendChatInfo+",num"+num+", ]";
        }
    }
}
