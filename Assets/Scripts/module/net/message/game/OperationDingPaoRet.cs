using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.game
{
    public class OperationDingPaoRet : Message
    { 
        //定跑结果返回
        public static int TYPE = 1;
        public static int ID = 29;

        public static OperationDingPaoRet create(int locationIndex, int dingPaoCount)
        {
            OperationDingPaoRet operationDingPao = new OperationDingPaoRet();
            operationDingPao.locationIndex = locationIndex;
            operationDingPao.dingPaoCount = dingPaoCount;
            return operationDingPao;



        }
        private int _locationIndex;
        private int _dingPaoCount;

        public int locationIndex
        {
            get { return _locationIndex; }
            set { _locationIndex = value; }
        }

        public int dingPaoCount
        {
            get { return _dingPaoCount; }
            set { _dingPaoCount = value; }
        }

        public void decode(Input _in)
        {
            _locationIndex = _in.readInt();
            _dingPaoCount = _in.readInt();
        }

        public void encode(Output _out)
        {
            _out.writeInt(locationIndex);
            _out.writeInt(dingPaoCount);
        }

        public int getMessageId()
        {
            return ID;
        }

        public int getMessageType()
        {
            return TYPE;
        }

        public string toString()
        {
            return "OperationDingPaoRet [locationIndex=" + _locationIndex + ",dingPaoCount=" + _dingPaoCount + ", ]";
        }
    }
}