
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.game
{
    public class GameChapterStart : Message
    {
        public static int TYPE = 1;
        public static int ID = 1;

        public GameChapterStart()
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
            return "GameChapterStart [ ]";
        }
    }
}