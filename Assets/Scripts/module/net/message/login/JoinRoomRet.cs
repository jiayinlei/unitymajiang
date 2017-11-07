
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.login
{
    //进入房间结果信息
    public class JoinRoomRet : Message
    {

        public static int TYPE = 7;
        public static int ID = 7;

        public static JoinRoomRet create(bool result)
        {
            JoinRoomRet joinRoomRet = new JoinRoomRet();
            joinRoomRet._result = result;
            return joinRoomRet;
        }
        protected bool _result;
        /// <summary>
        /// 0:亲友局
        /// 1：斗牛
        /// 2：固始
        /// </summary>
        private int type;

        public JoinRoomRet()
        {

        }
        public void decode(Input _in)
        {
            _result = _in.readBoolean();
            Type = _in.readInt();
        }

        public void encode(Output _out)
        {
            _out.writeBoolean(result);
            _out.writeInt(Type);
        }
        public bool result
        {
            get { return _result; }
            set { _result = value; }
        }

        public int Type {
            get {
                return type;
            }

            set {
                type = value;
            }
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
            return "JoinRoomRet [result=" + _result + ", ]";
        }
    }
}