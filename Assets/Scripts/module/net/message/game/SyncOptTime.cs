using System;
using com.guojin.core.io.message;
using com.guojin.core.io;
namespace com.guojin.mj.net.message.game
{
    //同步操作
    public class SyncOptTime : Message
    {

        public static int TYPE = 1;
        public static int ID = 17;

        public static SyncOptTime create(int index,int leftTime)
        {
            SyncOptTime syncOptTime = new SyncOptTime();
            syncOptTime._index = index;
            syncOptTime._leftTime = leftTime;
            return syncOptTime;
        }

        //位置
        protected int _index;

        //毫秒
        protected int _leftTime;
        public SyncOptTime()
        {
            
        }
        public void decode(Input _in)
        {
            _index = _in.readInt();
            _leftTime = _in.readInt();
        }

        public void encode(Output _out)
        {
            _out.writeInt(index);
            _out.writeInt(leftTime);
        }
        public int index
        {
            get { return _index; }
            set { _index = value; }
        }
        public int leftTime
        {
            get { return _leftTime; }
            set { _leftTime = value; }
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
            return "SyncOptTime [index=" + _index + ",leftTime=" + _leftTime + ", ]";
        }
    }
}
