using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.flower
{
    /// <summary>
    /// 炸金花看、弃牌、跟注 信息，发送
    /// </summary>
    public class LookCardInfoZJH : Message
    {
        public static int TYPE = 3;
        public static int ID = 10;
        public static LookCardInfoZJH Create(int type)
        {
            LookCardInfoZJH lcf = new LookCardInfoZJH();
            lcf._type = type;
            return lcf;
        }
        protected int _type;//看牌0，弃牌1，跟注2

        public int Type
        {
            get
            {
                return _type;
            }

            set
            {
                _type = value;
            }
        }
        public LookCardInfoZJH()
        {

        }
        public void decode(Input _in)
        {
            _type = _in.readInt();
        }

        public void encode(Output _out)
        {
            _out.writeInt(_type);
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
            return "LookCardInfoZJH [ "+ "type" + _type + ",]";
        }
    }
}
