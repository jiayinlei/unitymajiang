using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.flower
{
    //炸金花创建房间消息返回
    public class CreateRoomRetZJH : Message
    {

        public static int TYPE = 3;
        public static int ID = 1;

        public static CreateRoomRetZJH create(bool result, string roomCheckId)
        {
            CreateRoomRetZJH createRoomRetZJH = new CreateRoomRetZJH();
            createRoomRetZJH._result = result;
            createRoomRetZJH._roomCheckId = roomCheckId;
            return createRoomRetZJH;
        }
        protected bool _result;
        protected string _roomCheckId;

        public CreateRoomRetZJH()
        {

        }

        public void decode(Input _in)
        {
            _result = _in.readBoolean();
            _roomCheckId = _in.readString();
        }

        public void encode(Output _out)
        {
            _out.writeBoolean(result);
            _out.writeString(roomCheckId);
        }

        public bool result
        {
            get { return _result; }
            set { _result = value; }
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
            return "CreateRoomRetZJH [result=" + _result + ",roomCheckId=" + _roomCheckId + ", ]";
        }
    }
}

