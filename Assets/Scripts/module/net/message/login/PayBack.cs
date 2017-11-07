using com.guojin.core.io.message;
using com.guojin.core.io;
using System;

namespace com.guojin.mj.net.message.login
{
    public class PayBack : Message
    {
        public static int TYPE = 7;
        public static int ID = 29;
        protected bool _result;
        protected int _money;
        protected int _goldNum;
        public static PayBack create(bool  result, int money, int goldNum)
        {
            PayBack payBack = new PayBack();
            payBack._result = result;
            payBack._money = money;
            payBack._goldNum = goldNum;
            return payBack;
        }
        public bool result
        {
            get { return _result; }
            set { _result = value; }
        }
        public int money
        {
            get { return _money; }
            set { _money = value; }
        }
        public int goldNum
        {
            get { return _goldNum; }
            set { _goldNum = value; }
        }
        public void decode(Input _in)
        {
            _result = _in.readBoolean();
            _money = _in.readInt();
            _goldNum = _in.readInt();
        }

        public void encode(Output _out)
        {
            _out.writeBoolean(_result );
            _out.writeInt(_money  );
            _out.writeInt(_goldNum );
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
            return "Pay [_money=" + _money + ", ]"; ;
        }


  
    }
}
