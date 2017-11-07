using System.Collections;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.login
{
    //7，27 payType等于0时候也有返回
    class WXPayRet : Message
    {

        public static int TYPE = 7;
        public static int ID = 28;

       private  bool  _result;    //结果
       private  string _appid;    //appId
       private  string _noncestr;    //随机字符串
       private  string _package;    //package=Sign=WXPay;
       private  string _partnerid;    //商户号
       private  string _prepayid;    //prepay_id;
       private  string _timestamp;  //时间戳
       private  string _sign;

        public bool Result
        {
            get
            {
                return _result;
            }

            set
            {
                _result = value;
            }
        }

        public string Appid
        {
            get
            {
                return _appid;
            }

            set
            {
                _appid = value;
            }
        }

        public string Noncestr
        {
            get
            {
                return _noncestr;
            }

            set
            {
                _noncestr = value;
            }
        }

        public string Package
        {
            get
            {
                return _package;
            }

            set
            {
                _package = value;
            }
        }

        public string Partnerid
        {
            get
            {
                return _partnerid;
            }

            set
            {
                _partnerid = value;
            }
        }

        public string Prepayid
        {
            get
            {
                return _prepayid;
            }

            set
            {
                _prepayid = value;
            }
        }

        public string Timestamp
        {
            get
            {
                return _timestamp;
            }

            set
            {
                _timestamp = value;
            }
        }

        public string Sign
        {
            get
            {
                return _sign;
            }

            set
            {
                _sign = value;
            }
        }

        public WXPayRet()
        {
        }

        public static WXPay create()
        {
            WXPay wxp = new WXPay();
            return wxp;
        }

        public void decode(Input _in)
        {
            Result = _in.readBoolean();
            Appid = _in.readString();
            Noncestr = _in.readString();
           Package = _in.readString();
           Partnerid = _in.readString();
           Prepayid = _in.readString();
           Timestamp = _in.readString();
            Sign = _in.readString();
        }

        public void encode(Output _out)
        {

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
            return "WXPayRet[result="+Result+ "appid=" + Appid +"noncestr="+Noncestr+"package="+Package+"partnerid="+Partnerid+"prepayid="+Prepayid+"timestamp="+Timestamp+"sign="+Sign+"]";
        }
    }
}
