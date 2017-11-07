
using com.guojin.core.io.message;
using com.guojin.core.io;
using System;

namespace com.guojin.mj.net.message.game
{
    public class OperationOut : Message
    {
        //通知用户出牌
        public static int TYPE = 1;
        public static int ID = 14;

        public static OperationOut create(int index)
        {
            OperationOut operationOut = new OperationOut();
            operationOut._index = index;
            return operationOut;
        }

        protected int _index;

        public void decode(Input _in)
        {
            _index = _in.readInt();
        }

        public void encode(Output _out)
        {
            _out.writeInt(index);
        }
        public int index
        {
            get { return _index; }
            set { _index = value; }
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
            return "OperationOut [index=" + _index + ", ]";
        }
    }
}