using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.game
{

    public class SelectQuePaiType : Message
    {

        public static int TYPE = 1;
        public static int ID = 41;

        public static SelectQuePaiType Creat(int paiindex, int duanmen)//0筒 1条 2万
        {
            SelectQuePaiType selectZuiType = new SelectQuePaiType();
            selectZuiType._paiindex = paiindex;
            selectZuiType._duanmen = duanmen;
            return selectZuiType;
        }
        private int _paiindex;
        private int _duanmen;//断门

        public int duanmen
        {
            get { return _duanmen; }
            set { _duanmen = value; }
        }
        public int paiindex
        {
            get { return _paiindex; }
            set { _paiindex = value; }
        }
        public void decode(Input _in)
        {
            paiindex = _in.readInt();
            duanmen = _in.readInt();
        }
        public void encode(Output _out)
        {
            _out.writeInt(paiindex);
            _out.writeInt(duanmen);
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
            return "SelectQuePaiType[ duanmen=" + duanmen + ",paiindex="+ paiindex + "]";
        }
    }
}

