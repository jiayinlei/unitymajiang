using System.Collections;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.login
{

    class WeiXinShow:Message
    {

        public static int TYPE = 7;
        public static int ID = 32;

        public WeiXinShow()
        {

        }
        public static WeiXinShow create()
        {
            WeiXinShow weiXinShow = new WeiXinShow();
 
            return weiXinShow;
        }
        public void decode(Input _in)
        {

        }

        public void encode(Output _out)
        {

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
            return "WeiXinShow [ ]";
        }
    }
}
