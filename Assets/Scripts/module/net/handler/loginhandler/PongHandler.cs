
using com.guojin.core.io.message;
using com.guojin.mj.net.message.login;

namespace com.guojin.mj.net.handler.login
{
	
	public  class PongHandler : MessageHandler
	{
		public static PongHandler instance = new PongHandler();
		
		public PongHandler()
		{
			if(instance!=null){
				throw new System.Exception("ResourceManager 是单例模式");
			}
		}
		
		public bool handler(Message msg)
		{
			return inHandler((Pong)msg);
		}
		
		/**
		 * @return 返回true 表示需要脱离缓冲，不然这个消息的内容可能被覆盖
		 */
		private bool inHandler(Pong msg)
		{
			System.Console.WriteLine("ping:{0}", msg.time);
			return false;
		}
	}
}