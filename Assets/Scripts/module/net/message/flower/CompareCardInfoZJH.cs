using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.flower
{
    /// <summary>
    /// 炸金花比牌信息，发送
    /// </summary>
    public class CompareCardInfoZJH : Message
    {
        public static int TYPE = 3;
        public static int ID = 15;
        public static CompareCardInfoZJH Create(int indexed)
        {
            CompareCardInfoZJH lcf = new CompareCardInfoZJH();
            //lcf._index = index;
            lcf._indexed = indexed;
            //lcf._zhuNum = zhuNum;
            return lcf;
        }
        ///// <summary>
        ///// 比牌玩家位置
        ///// </summary>
        //protected int _index;
        ///// <summary>
        ///// 玩家ID
        ///// </summary>
        //protected int _id;
        /// <summary>
        /// 被比牌玩家位置
        /// </summary>
        protected int _indexed;
        ///// <summary>
        ///// 被比牌玩家ID
        ///// </summary>
        //protected int _ided;
        ///// <summary>
        ///// 比牌发起玩家的注数，没看单注，看牌翻倍
        ///// </summary>
        //protected int _zhuNum;

        //public int Index
        //{
        //    get
        //    {
        //        return _index;
        //    }

        //    set
        //    {
        //        _index = value;
        //    }
        //}

        public int Indexed
        {
            get
            {
                return _indexed;
            }

            set
            {
                _indexed = value;
            }
        }

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

        public CompareCardInfoZJH()
        {

        }
        public void decode(Input _in)
        {
            //_index = _in.readInt();
            _indexed = _in.readInt();
            //_zhuNum = _in.readInt();
        }

        public void encode(Output _out)
        {
            //_out.writeInt(_index);
            _out.writeInt(_indexed);
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
            return "CompareCardInfoZJH [ "+ "indexed" + _indexed + ",]";
        }
    }
}

