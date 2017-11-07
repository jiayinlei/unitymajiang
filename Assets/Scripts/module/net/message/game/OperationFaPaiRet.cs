
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;

namespace com.guojin.mj.net.message.game
{
    public class OperationFaPaiRet : Message
    {
        //自己的发牌的信息
        public static int TYPE = 1;
        public static int ID = 13;

        public static OperationFaPaiRet create(string opt,int pai)
        {
            OperationFaPaiRet operationFaPaiRet = new OperationFaPaiRet();
            operationFaPaiRet._opt = opt;
            operationFaPaiRet._pai = pai;
            return operationFaPaiRet;
        }

        
	 	// OUT:打牌,AN_GANG:暗杠牌,XIAO_MING_GANG:暗杠牌,HU:胡牌	 	 
        protected string _opt;
        protected int _pai;
        public OperationFaPaiRet()
        {

        }
        public void decode(Input _in)
        {
            _opt = _in.readString();
            _pai = _in.readInt();
        }

        public void encode(Output _out)
        {
            _out.writeString(opt);
            _out.writeInt(pai);
        }
        public string opt
        {
            get { return _opt; }
            set { _opt = value; }
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
            return "OperationFaPaiRet [opt=" + _opt + ",pai=" + _pai + ", ]";
        }
    }
}