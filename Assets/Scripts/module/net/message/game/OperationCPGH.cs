
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.game
{
    //通知客户端“吃碰杠胡”
    public class OperationCPGH : Message
    {
        public static int TYPE = 1;
        public static int ID = 10;

        public static OperationCPGH create(int index,int [] chi,bool isPeng,bool isGang,bool isHu,int pai)
        {
            OperationCPGH operationCPGH = new OperationCPGH();
            operationCPGH._index = index;
            operationCPGH._chi = chi;
            operationCPGH._isPeng = isPeng;
            operationCPGH._isGang = isGang;
            operationCPGH._isHu = isHu;
            operationCPGH._pai = pai;
            return operationCPGH;
        }
        //位置
        protected int _index;

        //三个一组
        protected int[] _chi;
        protected bool _isPeng;
        protected bool _isGang;
        protected bool _isHu;
        protected int  _pai;

        public OperationCPGH()
        {

        }
        public void decode(Input _in)
        {
            _index = _in.readInt();
            _chi = _in.readIntArray();
            _isPeng = _in.readBoolean();
            _isGang = _in.readBoolean();
            _isHu = _in.readBoolean();
            _pai = _in.readInt();
        }

        public void encode(Output _out)
        {
            _out.writeInt(index);
            _out.writeIntArray(chi);
            _out.writeBoolean(isPeng);
            _out.writeBoolean(isGang);
            _out.writeBoolean(isHu);
            _out.writeInt(pai);
        }
        public int index
        {
            get { return _index; }
            set { _index = value; }
        }
        public int[] chi
        {
            get { return _chi; }
            set { _chi = value; }
        }
        public bool isPeng
        {
            get { return _isPeng; }
            set { _isPeng = value; }
        }
        public bool isGang
        {
            get { return _isGang; }
            set { _isGang = value; }
        }
        public bool isHu
        {
            get { return _isHu; }
            set { _isHu = value; }
        }
        public int pai
        {
            get { return _pai; }
            set { _pai = value; }
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
            return "OperationCPGH [index=" + _index + ",chi=" + _chi + ",isPeng=" + _isPeng + ",isGang=" + _isGang + ",isHu=" + _isHu + ",pai=" + _pai + ", ]";
        }
    }
}