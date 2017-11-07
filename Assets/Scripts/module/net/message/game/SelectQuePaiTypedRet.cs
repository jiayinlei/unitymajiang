using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.game
{

    public class SelectQuePaiTypedRet : Message
    {

        public static int TYPE = 1;
        public static int ID = 42;

        public int paiindex;
        public int index;
        public int paiTypeIndex;

        public void decode(Input _in)
        {
            index = _in.readInt();
            paiindex = _in.readInt();
            paiTypeIndex = _in.readInt();
        }
        public void encode(Output _out)
        {

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
            return "SelectQuePaiTypedRet [index="+index+ ",paiindex = " + paiindex + ",paiTypeIndex = " + paiTypeIndex + " ]";
        }

    }
}