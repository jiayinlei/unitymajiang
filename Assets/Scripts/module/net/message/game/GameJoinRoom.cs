
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.game
{
    //准备就绪，通知服务器可以开始发送房间信息了
    public class GameJoinRoom : Message
    {

        public static int TYPE = 1;
        public static int ID = 6;

        public GameJoinRoom()
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
            return "GameJoinRoom [ ]";
        }
    }
}