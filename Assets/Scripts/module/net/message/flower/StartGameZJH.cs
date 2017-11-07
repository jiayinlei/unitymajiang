using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.flower
{
    //进入房间成功,游戏开始  前端收到这个消息开始发送命令加载 游戏信息,
    public class StartGameZJH : Message
    {
        public static int TYPE = 3;
        public static int ID = 34;

        public StartGameZJH()
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
            return "StartGameZJH [ ]";
        }
    }
}
