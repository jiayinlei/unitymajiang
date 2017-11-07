using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.flower
{
    /// <summary>
    /// 炸金花看牌信息，返回
    /// </summary>
    public class LookCardInfoRetZJH : Message
    {
        public static int TYPE = 3;
        public static int ID = 11;
        public LookCardInfoRetZJH Create(int index,string cardType, int[] shouPai)
        {
            LookCardInfoRetZJH lcf = new LookCardInfoRetZJH();
            lcf._index = index;
            lcf._cardType = cardType;
            lcf._shouPai = shouPai;
            return lcf;
        }
        /// <summary>
        /// 看牌玩家位置
        /// </summary>
        protected int _index;
        /// <summary>
        /// 玩家牌型
        /// </summary>
        protected string _cardType;
        /// <summary>
        /// 玩家手牌
        /// </summary>
        protected int[] _shouPai;

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
        public string CardType
        {
            get
            {
                return _cardType;
            }

            set
            {
                _cardType = value;
            }
        }
        public int[] ShouPai
        {
            get
            {
                return _shouPai;
            }

            set
            {
                _shouPai = value;
            }
        }

        public LookCardInfoRetZJH()
        {

        }
        public void decode(Input _in)
        {
            _shouPai = _in.readIntArray();
            _cardType = _in.readString();
            _index = _in.readInt();
        }

        public void encode(Output _out)
        {
            _out.writeIntArray(_shouPai);
            _out.writeString(_cardType);
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
            return "LookCardInfoRetZJH [ " + "index" + _index + "cardType" + _cardType + "shouPai" + _shouPai + ",]";
        }
    }
}

