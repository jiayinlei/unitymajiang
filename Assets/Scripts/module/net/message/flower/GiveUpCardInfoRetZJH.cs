using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.flower
{
    /// <summary>
    /// 炸金花弃牌结果，返回
    /// </summary>
    public class GiveUpCardInfoRetZJH : Message
    {
        public static int TYPE = 3;
        public static int ID = 31;
        public GiveUpCardInfoRetZJH Create(int index)
        {
            GiveUpCardInfoRetZJH lcf = new GiveUpCardInfoRetZJH();
            lcf._index = index;
            return lcf;
        }
        /// <summary>
        /// 玩家位置
        /// </summary>
        protected int _index;
        public int Index
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


        public GiveUpCardInfoRetZJH()
        {

        }
        public void decode(Input _in)
        {
            _index = _in.readInt();
        }

        public void encode(Output _out)
        {
            _out.writeInt(_index);
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
            return "GiveUpCardInfoRetZJH [ " + "index" + _index + ",]";
        }
    }
}

