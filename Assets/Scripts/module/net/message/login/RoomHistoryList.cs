
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.login
{
    public class RoomHistoryList : Message
    {
        public static int TYPE = 7;
        public static int ID = 19;
      
        public RoomHistoryList()
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
            return "RoomHistoryList [ ]";
        }
    }
}