
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.game
{
    //通知客户端"吃碰杠胡" 回复
    public class OperationCPGHRet : Message
    {

        public static int TYPE = 1;
        public static int ID = 11;

        public static OperationCPGHRet create( string opt ,int [] chi)
        {
            OperationCPGHRet operationCPGHRet = new OperationCPGHRet();
            operationCPGHRet._opt = opt;
            operationCPGHRet._chi = chi;
            return operationCPGHRet;
        }
        //HU:胡牌、PENG:碰、DA_MING_GANG:大明扛、XIAO_MING_GANG:小明扛、AN_GANG:暗杠牌       
        protected string _opt;
        //如果是吃牌  回复吃的组合
        protected int[] _chi;

        public OperationCPGHRet()
        {

        }

        public void decode(Input _in)
        {
            _opt = _in.readString();
            _chi = _in.readIntArray();

        }

        public void encode(Output _out)
        {
            _out.writeString(opt);
            _out.writeIntArray(chi);
        }
        public string opt
        {
            get { return _opt; }

            set { _opt = value; }
        }
        public int[] chi
        {
            get { return _chi; }
            set { _chi = value; }
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
            return "OperationCPGHRet [opt=" + _opt + ",chi=" + _chi + ", ]";
        }
    }
}