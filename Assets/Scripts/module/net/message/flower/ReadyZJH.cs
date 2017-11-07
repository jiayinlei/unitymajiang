using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.flower
{
    //炸金花进入游戏，准备就绪  发送
    public class ReadyZJH : Message
    {

        public static int TYPE = 3;
        public static int ID = 5;

        public ReadyZJH()
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
            return "ReadyZJH [ ]";
        }
    }
}
