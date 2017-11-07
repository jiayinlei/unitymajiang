using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.game
{
    public class AllQuePaiTypedRet : Message
    {

        public static int TYPE = 1;
        public static int ID = 40;

     

        private int[] _userDingQues;//断的门 筒 条 万 1 2 3
        private int[] _userDingQueOutPais;//断门出的牌的id

        public int [] userDingQues
        {
            get { return _userDingQues; }
            set { _userDingQues = value; }
        }
        public int[] userDingQueOutPais
        {
            get { return _userDingQueOutPais; }
            set { _userDingQueOutPais = value; }
        }
        public void decode(Input _in)
        {
            _userDingQues = _in.readIntArray();
            _userDingQueOutPais = _in.readIntArray();
        }
        public void encode(Output _out)
        {
            //_out.writeString(PaoZuiNum);
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
            return "AllQuePaiTypedRet";
        }
    }
}