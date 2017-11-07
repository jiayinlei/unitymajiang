using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.game
{
    public class ZhanJiZui : Message
    {
        public static int TYPE = 1;
        public static int ID = 49;

        public string zuiScore;
        public string zuiType;    

       
        public void decode(Input _in)
        {
            zuiScore = _in.readString();
            zuiType = _in.readString();
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
            return "ZhanJiZui";
        }
    }
}
