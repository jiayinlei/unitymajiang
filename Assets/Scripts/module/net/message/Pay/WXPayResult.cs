using System.Collections;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.login
{

    class WXPayResult : Message
    {

        public static int TYPE = 7;
        public static int ID = 30;

        string _out_trade_no;    //商户订单号

        public string Out_trade_no
        {
            get
            {
                return _out_trade_no;
            }

            set
            {
                _out_trade_no = value;
            }
        }

        public WXPayResult()
        {

        }

        public static WXPayResult create()
        {
            WXPayResult wxps = new WXPayResult();
            return wxps;
        }

        public void decode(Input _in)
        {
        }

        public void encode(Output _out)
        {
            _out.writeString(_out_trade_no);
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
            return "WXPayResult[out_trade_no=" + _out_trade_no +" ]";
        }
    }
}
