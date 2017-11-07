
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.game
{
    public class OperationFaPai : Message
    {
        //自己的发牌的信息
        public static int TYPE = 1;
        public static int ID = 12;

        public  static OperationFaPai create(int index,int pai,int[] anGang, int[] mingGang,bool hu)
        {
            OperationFaPai operationFaPai = new OperationFaPai();
            operationFaPai._index = index;
            operationFaPai._pai = pai;
            operationFaPai._anGang = anGang;
            operationFaPai._mingGang = mingGang;
            operationFaPai._hu = hu;
            return operationFaPai;
        }

        //位置
        protected int _index;
        protected int _pai;
        protected int[] _anGang;
        protected int[] _mingGang;
        protected bool _hu;

        public OperationFaPai()
        {

        }
        public void decode(Input _in)
        {
            _index = _in.readInt();
            _pai = _in.readInt();
            _anGang = _in.readIntArray();
            _mingGang = _in.readIntArray();
            _hu = _in.readBoolean();
        }

        public void encode(Output _out)
        {
            _out.writeInt(index);
            _out.writeInt(pai);
            _out.writeIntArray(anGang);
            _out.writeIntArray(mingGang);
            _out.writeBoolean(hu);
        }
        public int index
        {
            get { return _index; }
            set { _index = value; }
        }
        public int pai
        {
            get { return _pai; }
            set { _pai = value; }
        }
        public int[] anGang
        {
            get { return _anGang; }
            set { _anGang = value; }
        }
        public int[] mingGang
        {
            get { return _mingGang; }
            set { _mingGang = value; }
        }
        public bool hu
        {
            get { return _hu; }
            set { _hu = value; }
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
            return "OperationFaPai [index=" + _index + ",pai=" + _pai + ",anGang=" + _anGang + ",mingGang=" + _mingGang + ",hu=" + _hu + ", ]";
        }
    }
}