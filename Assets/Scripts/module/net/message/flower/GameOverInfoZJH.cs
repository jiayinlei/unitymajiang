using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.flower
{
    /// <summary>
    /// 炸金花房间局数完，发送
    /// </summary>
    public class GameOverInfoZJH : Message
    {
        public static int TYPE = 3;
        public static int ID = 19;

        public GameOverInfoZJH()
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
            return "GameOverInfoZJH [ ]";
        }
    }
}
