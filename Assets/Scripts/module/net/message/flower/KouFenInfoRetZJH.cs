using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.flower
{
    /// <summary>
    /// 炸金花扣除分数 返回
    /// </summary>
    public class KouFenInfoRetZJH : Message
    {
        public static int TYPE = 3;
        public static int ID = 14;
        public KouFenInfoRetZJH Create(int index, int zhuNum)
        {
            KouFenInfoRetZJH lcf = new KouFenInfoRetZJH();
            lcf._index = index;
            lcf._zhuNum = zhuNum;
            return lcf;
        }
        /// <summary>
        /// 扣分玩家位置
        /// </summary>
        protected int _index;
        /// <summary>
        /// 玩家扣得分数
        /// </summary>
        protected int _zhuNum;

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
        public int ZhuNum
        {
            get
            {
                return _zhuNum;
            }

            set
            {
                _zhuNum = value;
            }
        }

        public KouFenInfoRetZJH()
        {

        }
        public void decode(Input _in)
        {
            _index = _in.readInt();
            _zhuNum = _in.readInt();
        }

        public void encode(Output _out)
        {
            _out.writeInt(_index);
            _out.writeInt(_zhuNum);
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
            return "KouFenInfoRetZJH [ " + "index" + _index + "zhuNum" + _zhuNum + ",]";
        }
    }
}

