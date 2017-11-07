using UnityEngine;
using System.Collections;
using com.guojin.core.io;
using com.guojin.core.io.message;
using System;

namespace com.guojin.mj.net.message.game
{
    public class PlayerReceiveChatInfo : Message
    {
        //接收聊天信息
        public static int TYPE = 1;
        public static int ID = 25;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="receiveChatInfo"></param>
        /// <param name="num">1.文字 2.表情 3.语音</param>
        /// <returns></returns>
        public static PlayerReceiveChatInfo create(int userIndex,string receiveChatInfo,int num)
        {
            PlayerReceiveChatInfo playerReceiveChatInfo = new PlayerReceiveChatInfo();
            playerReceiveChatInfo._receiveChatInfo = receiveChatInfo;
            playerReceiveChatInfo.Num = num;
            playerReceiveChatInfo.userIndex = userIndex;
            return playerReceiveChatInfo;
        }
        protected int userIndex;
        protected string _receiveChatInfo;
        protected int num;//类型

        public string ReceiveChatInfo
        {
           get{return _receiveChatInfo;}
           set {_receiveChatInfo = value;}
        }

        public int Num
        {
            get{return num;}
            set{num = value;}
        }

        public int UserIndex
        {
            get{return userIndex;}
            set{userIndex = value;}
        }

        public void decode(core.io.Input _in)
        {
            userIndex = _in.readInt();
            _receiveChatInfo = _in.readString();
            num = _in.readInt();
            

        }

        public void encode(Output _out)
        {
            
            _out.writeInt(userIndex);
            _out.writeString(_receiveChatInfo);
            _out.writeInt(num);
        }

        public int getMessageId()
        {
            return ID;
        }

        public int getMessageType()
        {
            return TYPE;
        }

        public string toString()
        {
            return "PlayerReveiveChatInfo [userIndex"+ userIndex+", ReceiveChatInfo1" + _receiveChatInfo + ",num"+num+ "]";
        }
    }
}
