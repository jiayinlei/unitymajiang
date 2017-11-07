using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.flower
{
    /// <summary>
    /// 炸金花加注信息  发送
    /// </summary>
    public class AddMoneyInfoZJH : Message
    {
        public static int TYPE = 3;
        public static int ID = 12;
        public static AddMoneyInfoZJH Create(int zhuNum)
        {
            AddMoneyInfoZJH lcf = new AddMoneyInfoZJH();
            lcf._zhuNum = zhuNum;
            return lcf;
        }
        /// <summary>
        /// 玩家加的注数
        /// </summary>
        protected int _zhuNum;

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

        public AddMoneyInfoZJH()
        {

        }
        public void decode(Input _in)
        {
            _zhuNum = _in.readInt();
        }

        public void encode(Output _out)
        {
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
            return "AddMoneyInfoZJH [ " + "zhuNum" + _zhuNum + ",]";
        }
    }
}

