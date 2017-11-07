using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.flower
{
    /// <summary>
    /// 炸金花扣除底分消息 返回
    /// </summary>
    public class KouDiFenInfoRetZJH : Message
    {
        public static int TYPE = 3;
        public static int ID = 9;

        public static KouDiFenInfoRetZJH create(int[] index,int diFen)
        {
            KouDiFenInfoRetZJH ChapterMsg = new KouDiFenInfoRetZJH();
            ChapterMsg._index = index;
            ChapterMsg._diFen = diFen;
            return ChapterMsg;
        }
        //剩余玩家位置
        protected int[] _index;
        protected int _diFen;

        public KouDiFenInfoRetZJH()
        {

        }
        public void decode(Input _in)
        {
            _index = _in.readIntArray();
            _diFen = _in.readInt();
        }

        public void encode(Output _out)
        {
            _out.writeIntArray(_index);
            _out.writeInt(_diFen);
        }
        public int[] Index
        {
            get
            {
                return _index;
            }

            set
            {
                _index = value;
            }
        }
        public int DiFen
        {
            get
            {
                return _diFen;
            }

            set
            {
                _diFen = value;
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
            return "KouDiFenInfoRetZJH [index=" + _index + ",diFen = " + _diFen + ", ]";
        }
    }
}
