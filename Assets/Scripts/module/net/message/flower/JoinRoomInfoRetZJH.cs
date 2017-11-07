using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.flower
{
    /// <summary>
    /// 炸金花进入房间结果信息 返回
    /// </summary>
    public class JoinRoomInfoRetZJH : Message
    {

        public static int TYPE = 3;
        public static int ID = 4;

        public static JoinRoomInfoRetZJH create(bool result)
        {
            JoinRoomInfoRetZJH joinRoomRet = new JoinRoomInfoRetZJH();
            joinRoomRet._result = result;
            return joinRoomRet;
        }
        protected bool _result;

        public JoinRoomInfoRetZJH()
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
            return "JoinRoomInfoRetZJH [result=" + _result + ", ]";
        }
    }
}
