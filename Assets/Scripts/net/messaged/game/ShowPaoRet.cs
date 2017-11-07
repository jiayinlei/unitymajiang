using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.game
{ 
    //是否显示定跑信息 收到消息就显示
    public class ShowPaoRet : Message
    {
        public static int TYPE = 1;
        public static int ID = 30;

        public ShowPaoRet()
        {

        }
        public void decode(Input _in)
        {

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
            return "ShowPaoRet [ ]";
        }
    }
}
