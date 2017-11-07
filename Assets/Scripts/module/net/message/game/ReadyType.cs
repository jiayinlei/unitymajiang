using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.game
{
    public class ReadyType : Message
    {
        public static int TYPE = 1;
        public static int ID = 32;

        public static ReadyType create(bool isready,int index,int readynum)
        {
            ReadyType readyType = new ReadyType();
            readyType._isready = isready;
            readyType._index = index;
            readyType._readynum = readynum;
            return readyType;
        }

        protected bool _isready;
        protected int _index;
        protected int _readynum;

        public bool isready
        {
            get { return _isready; }
            set { _isready = value; }
        }
        public int index
        {
            get { return _index; }
            set { _index = value; }
        }
        public int readynum
        {
            get { return _readynum; }
            set { _readynum = value; }
        }
        public void decode(Input _in)
        {
            _isready = _in.readBoolean();
            _index = _in.readInt();
            _readynum = _in.readInt();
        }

        public void encode(Output _out)
        {
            _out.writeBoolean(isready);
            _out.writeInt(index);
            _out.writeInt(readynum);
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
            return "ReadyType [isready=" + _isready + ", ]";
        }
    }
}