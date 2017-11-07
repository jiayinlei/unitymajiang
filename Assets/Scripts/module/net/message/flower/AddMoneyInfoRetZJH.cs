using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.flower
{
    /// <summary>
    /// 炸金花加注信息，返回
    /// </summary>
    public class AddMoneyInfoRetZJH : Message
    {
        public static int TYPE = 3;
        public static int ID = 30;
        public static AddMoneyInfoRetZJH Create(int index,int score)
        {
            AddMoneyInfoRetZJH lcf = new AddMoneyInfoRetZJH();
            lcf._index = index;
            //lcf._score = score;
            return lcf;
        }
        //玩家位置
        protected int _index;
        //// 玩家加的注
        //protected int _score;
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
        //public int Score
        //{
        //    get
        //    {
        //        return _score;
        //    }

        //    set
        //    {
        //        _score = value;
        //    }
        //}

        public AddMoneyInfoRetZJH()
        {

        }
        public void decode(Input _in)
        {
            _index = _in.readInt();
            //_score = _in.readInt();
        }

        public void encode(Output _out)
        {
            _out.writeInt(_index);
            //_out.writeInt(_score);
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
            return "AddMoneyInfoRetZJH [ " + "index" + _index + ",]";
        }
    }
}
