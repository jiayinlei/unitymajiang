using com.guojin.core.io;
using com.guojin.core.io.message;

namespace com.guojin.mj.net.message.login
{
    
    
    /**
     * ping
     * 
     */
    public class Ping : Message
    {
        public static int TYPE                  =7;
        public static int ID                    =14;
        
        public static Ping create(string time)
        {
            Ping ping = new Ping();
            ping._time = time;
            return ping;
        }
    
        protected string _time;
        
        public Ping()
        {
        }
            
        public void decode(Input _in)
        {
            _time = _in.readString();
        }
        
        public void encode(Output _out)
        {
            _out.writeString(time);
        }
        

        public string time
        {
            get { return _time; }
            set { _time = value; }
        }

        public string toString()
        {
            return "Ping [time=" + _time + ", ]";
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