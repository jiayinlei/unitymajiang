using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.game
{
    public class SelectZuiTypeRet : Message
    {
        public static int TYPE = 1;
        public static int ID = 45;

        private int _index;//玩家的位置索引
        //private string _PaoZuiNum;//跑的嘴       

        //public string PaoZuiNum
        //{
        //    get { return _PaoZuiNum; }
        //    set { _PaoZuiNum = value; }
        //}
        public int index
        {
            get { return _index; }
            set { _index = value; }
        }
        public void decode(Input _in)
        {
            index = _in.readInt();
            //PaoZuiNum = _in.readString();
        }
        public void encode(Output _out)
        {
            _out.writeInt(index);
            //_out.writeString(PaoZuiNum);
        }
        public int getMessageType()
        {
            return TYPE;
        }
        public int getMessageId()
        {
            return ID;
        }
        public string toString()
        {
            return "SelectZuiTypeRet[index="+index+ ",]";
        }

    }
}