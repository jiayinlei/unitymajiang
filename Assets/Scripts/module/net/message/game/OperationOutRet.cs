
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.game
{
    public class OperationOutRet : Message
    {
        //出牌回复
        public static int TYPE = 1;
        public static int ID = 15;

        public static OperationOutRet create(int pai)
        {
            OperationOutRet operationOutRet = new OperationOutRet();
            operationOutRet._pai = pai;
            return operationOutRet;
        }
        protected int _pai;
        public void decode(Input _in)
        {
            _pai = _in.readInt();
        }

        public void encode(Output _out)
        {
            _out.writeInt(pai);
        }
        public int pai
        {
            get { return _pai; }
            set { _pai = value; }
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
            return "OperationOutRet [pai=" + _pai + ", ]";
        }
    }
}