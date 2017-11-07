
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.login
{
    //加入房间
    public class JoinRoom : Message
    {
        public static int TYPE = 7;
        public static int ID = 6;

        public static JoinRoom create(string roomCheckId)
        {
            JoinRoom joinRoom = new JoinRoom();
            joinRoom._roomCheckId = roomCheckId;
            return joinRoom;
        }
        protected string _roomCheckId;//要加入的房间的房间号
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
            return "JoinRoom [roomCheckId=" + _roomCheckId + ", ]";
        }
    }
}