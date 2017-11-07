
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.login
{
   //进入房间成功,游戏开始  前端收到这个消息开始发送命令加载 游戏信息,
    public class StartGame : Message
    {
        public static int TYPE = 7;
        public static int ID = 23;

        public StartGame()
        {

        }
         public void decode(Input _in)
        {
            //throw new NotImplementedException();
            //_key = _in.readString();
        }

        public void encode(Output _out)
        {
            //throw new NotImplementedException();
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
            return "StartGame [ ]";
        }
    }
}