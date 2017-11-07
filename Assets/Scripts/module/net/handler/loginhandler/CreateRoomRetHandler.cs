using System;

using com.guojin.core.io.message;
using com.guojin.mj.net.message.login;

namespace com.guojin.mj.net.handler.login
{
    public class CreateRoomRetHandler 
    {
        public static CreateRoomRetHandler instance = new CreateRoomRetHandler();
        public CreateRoomRetHandler()
        {
            if (instance != null)
            {
                throw new Exception("ResourceManager 是单例模式");
            }
        }

        //public bool handler(Message msg)
        //{
            
        //}
    }
}