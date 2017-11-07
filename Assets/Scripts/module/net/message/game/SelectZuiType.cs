using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.game
{

    public class SelectZuiType : Message
    {
        public static int TYPE = 1;
        public static int ID = 44;

        public static SelectZuiType Creat(string  paoZuiNum)
        {
            SelectZuiType selectZuiType = new SelectZuiType();
            selectZuiType._PaoZuiNum = paoZuiNum;
            return selectZuiType;
        }

        private string _PaoZuiNum;//跑的嘴

        public string PaoZuiNum
        {
            get { return _PaoZuiNum; }
            set { _PaoZuiNum = value; }
        }
        public void decode(Input _in)
        {
            PaoZuiNum = _in.readString();
        }
        public void encode(Output _out)
        {
            _out.writeString(PaoZuiNum);
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
            return "SelectZuiType";
        }
    }
}