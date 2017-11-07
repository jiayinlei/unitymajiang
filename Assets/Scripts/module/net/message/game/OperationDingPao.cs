using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.game
{
    public class OperationDingPao : Message
    {
        //发送定跑信息
        public static int TYPE = 1;
        public static int ID = 28;
        public static OperationDingPao create(int userId, int dingPaoCount)
        {
            OperationDingPao operationDingPao = new OperationDingPao();
            operationDingPao._userId = userId;
            operationDingPao._dingPaoCount = dingPaoCount;
            return operationDingPao;
        }

        private int _userId;
        private int _dingPaoCount;

        protected int userId
        {
            get { return _userId; }
            set { _userId = value; }            
        }

        protected int dingPaoCount
        {
            get { return _dingPaoCount; }
            set { _dingPaoCount = value; }            
        }

        public void decode(Input _in)
        {
            _userId = _in.readInt();
            _dingPaoCount = _in.readInt();            
        }

        public void encode(Output _out)
        {
            _out.writeInt(userId);
            _out.writeInt(dingPaoCount);
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
            return "OperationDingPao [userId=" + _userId + ",dingPaoCount=" + _dingPaoCount + ", ]";
        }
    }
}