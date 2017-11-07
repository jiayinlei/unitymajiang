using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.game
{
    public class AllDingZuiZi : Message
    {
        public static int TYPE = 1;
        public static int ID = 46;
    

        private string[] _userDingQues;//每个玩家跑的嘴

        public string[] userDingQues
        {
            get { return _userDingQues; }
            set { _userDingQues = value; }
        }
        public void decode(Input _in)
        {
            userDingQues = _in.readStringArray();
        }
        public void encode(Output _out)
        {
            _out.writeStringArray(userDingQues);
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
            return "AllDingZuiZi";
        }
    }
}
