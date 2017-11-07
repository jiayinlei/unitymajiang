
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.login
{
    //登录错误
    public class LoginError : Message
    {
        public static int TYPE = 7;
        public static int ID = 9;

        public LoginError()
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
            return TYPE;
        }

        public int getMessageType()
        {
            return ID;
        }

        public string toString()
        {
            return "LoginError [ ]";
        }
    }
}