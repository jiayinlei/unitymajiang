using com.guojin.core.io.message;
using com.guojin.core.utils;
using com.guojin.mj.net.handler.login;

namespace com.guojin.mj.net.handler
{
	public class MessageHandlerFactory : Singleton<MessageHandlerFactory>
    {
		private static object[,] vector = new object[8, 25];
		
		
		public MessageHandlerFactory()
		{
			vector[7, 15] = PongHandler.instance;
		}
		
		public MessageHandler getHandler(int type, int id)
		{
            System.Console.WriteLine("getHandler({0})", vector[type, id]);
            return (MessageHandler) vector[type, id];

        }
    }
}