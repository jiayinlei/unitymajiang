
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.login
{
    //解散房间结果
    public class DelRoomRet : Message
    {
        public static int TYPE = 7;
        public static int ID = 3;

        public static DelRoomRet create(bool result )
        {
            DelRoomRet delRoomRet = new DelRoomRet();
            delRoomRet._result = result;
            return delRoomRet;
        }

        protected bool _result;

        public DelRoomRet()
        {

        }

        public void decode(Input _in)
        {
            _result = _in.readBoolean();
        }

        public void encode(Output _out)
        {
            _out.writeBoolean(result);
        }

        public bool result
        {
            get { return _result; }
            set { _result = value; }
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
            return "DelRoomRet [result=" + _result + ", ]";
        }
    }
}
