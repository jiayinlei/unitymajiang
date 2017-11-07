using System.Collections;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.login
{

    class WXPay : Message
    {

        public static int TYPE = 7;
        public static int ID = 27;

        int _goldCount;  //房卡个数
        int _payType;      //支付类型 0为微信 1为支付宝

        public int GoldCount
        {
            get
            {
                return _goldCount;
            }

            set
            {
                _goldCount = value;
            }
        }

        public int PayType
        {
            get
            {
                return _payType;
            }

            set
            {
                _payType = value;
            }
        }

        public WXPay()
        {

        }
        public static WXPay Create(int goldCount, int paytype)
        {
            WXPay wxp = new WXPay();
            wxp._goldCount = goldCount;
            wxp._payType = paytype;
            return wxp;
        }

        public void decode(Input _in)
        {
        }

        public void encode(Output _out)
        {
            _out.writeInt(_goldCount);
            _out.writeInt(_payType);
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
            return "WXPay[goldCount="+_goldCount+"payType="+_payType+" ]";
        }
    }
}
