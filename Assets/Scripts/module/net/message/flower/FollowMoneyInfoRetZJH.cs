using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.flower
{
    /// <summary>
    /// 炸金花跟注结果，返回
    /// </summary>
    public class FollowMoneyInfoRetZJH : Message
    {
        public static int TYPE = 3;
        public static int ID = 13;
        public FollowMoneyInfoRetZJH Create(int index)
        {
            FollowMoneyInfoRetZJH lcf = new FollowMoneyInfoRetZJH();
            lcf._index = index;
            //lcf._id = id;
            //lcf._zhuNum = zhuNum;
            return lcf;
        }
        /// <summary>
        /// 玩家位置
        /// </summary>
        protected int _index;
        ///// <summary>
        ///// 玩家ID
        ///// </summary>
        //protected int _id;
        ///// <summary>
        ///// 玩家跟的注数，没看单注，看牌翻倍
        ///// </summary>
        //protected int _zhuNum;

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

        //public int Id
        //{
        //    get
        //    {
        //        return _id;
        //    }

        //    set
        //    {
        //        _id = value;
        //    }
        //}
        //public int ZhuNum
        //{
        //    get
        //    {
        //        return _zhuNum;
        //    }

        //    set
        //    {
        //        _zhuNum = value;
        //    }
        //}

        public FollowMoneyInfoRetZJH()
        {

        }
        public void decode(Input _in)
        {
            _index = _in.readInt();
            //_id = _in.readInt();
            //_zhuNum = _in.readInt();
        }

        public void encode(Output _out)
        {
            _out.writeInt(_index);
            //_out.writeInt(_id);
            //_out.writeInt(_zhuNum);
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
            return "FollowMoneyInfoRetZJH [ " + "index" + _index + ",]";
        }
    }
}

