
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;

namespace com.guojin.mj.net.message.login
{
    //退出房间
    public class ExitRoom : Message
    {

        public static int TYPE = 7;
        public static int ID = 4;
        
        public ExitRoom()
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
            return "ExitRoom [ ]";
        }
    }
}