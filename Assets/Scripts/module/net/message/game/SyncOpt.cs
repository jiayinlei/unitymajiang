
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.game
{
    //同步操作
    public class SyncOpt : Message
    {
        public static int TYPE = 1;
        public static int ID = 16;

        public static SyncOpt create(string opt,int index,int pai,int [] chi,int otherUserIndex)
        {
            SyncOpt syncOpt = new SyncOpt();
            syncOpt._index = index;
            syncOpt._pai = pai;
            syncOpt._chi = chi;
            syncOpt._otherUserIndex = otherUserIndex;
            return syncOpt;
        }

        //位置 OUT OUT_OK FA
        protected string _opt;
        protected int _index;
        protected int _pai;
        protected int[] _chi;
        private int _otherUserIndex;

        public SyncOpt()
        {

        }

        public void decode(Input _in)
        {
            _opt = _in.readString();
            _index = _in.readInt();
            _pai = _in.readInt();
            _chi = _in.readIntArray();
            _otherUserIndex= _in.readInt();
        }

        public void encode(Output _out)
        {
            _out.writeString(opt);
            _out.writeInt(index);
            _out.writeInt(pai);
            _out.writeIntArray(chi);
            _out.writeInt(otherUserIndex);
        }
        public int otherUserIndex
        {
            get { return _otherUserIndex; }
            set { _otherUserIndex = value; }
        }
        public string opt
        {
            get { return _opt; }
            set { _opt = value; }
        }
        public int index
        {
            get { return _index; }
            set { _index = value; }
        }
        public int pai
        {
            get { return _pai; }
            set { _pai = value; }
        }
        public int[] chi
        {
            get { return _chi; }
            set { _chi = value; }
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
            return "SyncOpt [opt=" + _opt + ",index=" + _index + ",pai=" + _pai + ",chi=" + _chi + ", ]";
        }
    }
}
