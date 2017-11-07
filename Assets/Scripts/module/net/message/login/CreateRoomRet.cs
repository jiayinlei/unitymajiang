
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.login
{
    //创建房间结果
    public class CreateRoomRet : Message
    {

        public static int TYPE = 7;
        public static int ID = 1;

        public static CreateRoomRet create(bool result,string roomCheckId)
        {
            CreateRoomRet createRoomRet = new CreateRoomRet();
            createRoomRet._result = result;
            createRoomRet._roomCheckId = roomCheckId;
            return createRoomRet;
        }
        protected bool _result;
        protected string _roomCheckId;

        public CreateRoomRet()
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
            return "CreateRoomRet [result=" + _result + ",roomCheckId=" + _roomCheckId + ", ]";
        }
    }
}