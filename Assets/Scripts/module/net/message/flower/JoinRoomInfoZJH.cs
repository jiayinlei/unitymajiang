using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.flower
{
    //炸金花加入房间信息  发送
    public class JoinRoomInfoZJH : Message
    {
        public static int TYPE = 3;
        public static int ID = 3;

        public static JoinRoomInfoZJH create(string roomCheckId)
        {
            JoinRoomInfoZJH joinRoom = new JoinRoomInfoZJH();
            joinRoom._roomCheckId = roomCheckId;
            return joinRoom;
        }
        protected string _roomCheckId;
        public void decode(Input _in)
        {
            _roomCheckId = _in.readString();
        }

        public void encode(Output _out)
        {
            _out.writeString(roomCheckId);
        }
        public string roomCheckId
        {
            get { return _roomCheckId; }
            set { _roomCheckId = value; }
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
            return "JoinRoomInfoZJH [roomCheckId=" + _roomCheckId + ", ]";
        }
    }
}
