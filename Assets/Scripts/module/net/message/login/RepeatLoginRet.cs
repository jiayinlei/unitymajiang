
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.login
{
    //登陆结果
    public class RepeatLoginRet : Message
    {
        public static int TYPE = 7;
        public static int ID = 17;

        public RepeatLoginRet()
        {

        }
        public void decode(Input _in)
        {
            
        }

        public void encode(Output _out)
        {
            
        }

        public string  toString()
		{
			return "RepeatLoginRet [ ]";
		}
		
		public int  getMessageType()
		{
			return TYPE;
		}

        public int getMessageId()
		{
			return ID;
		}
    }
}