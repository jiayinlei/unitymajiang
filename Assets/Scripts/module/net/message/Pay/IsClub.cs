using System.Collections;
using com.guojin.core.io;
using com.guojin.core.io.message;


    //登录是否询问服务器是否加入俱乐部
    class IsClub : Message
    {

        public static int TYPE = 7;
        public static int ID = 42;

        public IsClub()
        {

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
            return "IsClub[ ]";
        }
    }
