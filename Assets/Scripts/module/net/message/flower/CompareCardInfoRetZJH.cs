using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.flower
{
    /// <summary>
    /// 炸金花比牌结果，返回
    /// </summary>
    public class CompareCardInfoRetZJH : Message
    {
        public static int TYPE = 3;
        public static int ID = 16;
        public CompareCardInfoRetZJH Create(int winIndex,int defeatedIndex)
        {
            CompareCardInfoRetZJH lcf = new CompareCardInfoRetZJH();
            lcf._winIndex = winIndex; 
            lcf._defeatedIndex = defeatedIndex;
            return lcf;
        }
        /// <summary>
        /// 胜利玩家位置
        /// </summary>
        protected int _winIndex;
        /// <summary>
        /// 失败玩家位置
        /// </summary>
        protected int _defeatedIndex;

        public int WinIndex
        {
            get
            {
                return _winIndex;
            }

            set
            {
                _winIndex = value;
            }
        }

        public int DefeatedIndex
        {
            get
            {
                return _defeatedIndex;
            }

            set
            {
                _defeatedIndex = value;
            }
        }
        public CompareCardInfoRetZJH()
        {

        }
        public void decode(Input _in)
        {
            _winIndex = _in.readInt();
            _defeatedIndex = _in.readInt();
        }

        public void encode(Output _out)
        {
            _out.writeInt(_winIndex);
            _out.writeInt(_defeatedIndex);
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
            return "CompareCardInfoRetZJH [ " + "winIndex" + _winIndex + "defeatedIndex" + _defeatedIndex + ",]";
        }
    }
}
