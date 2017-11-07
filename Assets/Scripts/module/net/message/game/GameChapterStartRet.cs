
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;

namespace com.guojin.mj.net.message.game
{
    public class GameChapterStartRet : Message
    {
        public static int TYPE = 1;
        public static int ID = 2;

        public GameChapterStartRet()
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
            return "GameChapterStartRet [ ]";
        }
    }
   
}