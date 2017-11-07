using System;
using System.Collections.Generic;

using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.game
{
    public class VoteDelStart : Message
    {
        public static int TYPE = 1;
        public static int ID = 23;
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
            return "VoteDelStart [ ]";
        }
    }
}