
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.login
{
    //创建房间结果
    public class CreatePdkRoom_Ret : Message
    {

        public static int TYPE = 4;
        public static int ID = 1;

        public static CreatePdkRoom_Ret create(bool result, string roomCheckId)
        {
            CreatePdkRoom_Ret createRoomRet = new CreatePdkRoom_Ret();
            createRoomRet._result = result;
            createRoomRet._roomId = roomCheckId;
            return createRoomRet;
        }
        protected bool _result;
        protected string _roomId;

        public CreatePdkRoom_Ret()
        {

        }

        public void decode(Input _in)
        {
            _result = _in.readBoolean();
            _roomId = _in.readString();
        }

        public void encode(Output _out)
        {
            _out.writeBoolean(result);
            _out.writeString(_roomId);
        }

        public bool result
        {
            get { return _result; }
            set { _result = value; }
        }
        public string roomCheckId
        {
            get { return _roomId; }
            set { _roomId = value; }
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
            return "CreateRoomRet [result=" + _result + ",roomCheckId=" + _roomId + ", ]";
        }
    }
}