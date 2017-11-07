using com.guojin.core.io;
using com.guojin.core.io.message;

namespace com.guojin.mj.net.message.login
{
	/**
 	 * pong
 	 * 
 	 * <b>生成器生成代码，请勿修改，扩展请继承</b>
 	 * @author isnowfox消息生成器
 	 */
	public class Pong : Message
	{
		public static int TYPE					=7;
		public static int ID					=15;
		
		public static Pong create(string time)
		{
			Pong pong = new Pong();
			pong._time = time;         

            return pong;
		}
	
		protected string _time;       
		public Pong()
		{
		}
			
		public void decode(Input in0)
		{
			_time = in0.readString();          
            System.Console.WriteLine("Pong decode:{0}", _time);
		}
		
		public void encode(Output _out)
		{
			_out.writeString(time);        

        }
		
        

		public string time
        {
		    get{
			return _time;
            }

		    set
            {
                _time = value;
            }
        }
		
		public string toString()
		{
			return "Pong [time=" + _time + ", ]";
		}
		
		public int getMessageType()
		{
			return TYPE;
		}
		
		public int getMessageId()
		{
			return ID;
		}
	}
}