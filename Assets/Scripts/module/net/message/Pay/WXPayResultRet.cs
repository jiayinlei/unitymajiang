using System.Collections;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.login
{

    class WXPayResultRet : Message
    {

        public static int TYPE = 7;
        public static int ID = 29;

       private  bool _result;     //查询结果
       private  int _money;      //金额,单位为分
       private  int _goldNum;  //房卡个数

        public bool Result
        {
            get
            {
                return Result;
            }

            set
            {
                Result = value;
            }
        }

        public int Money
        {
            get
            {
                return _money;
            }

            set
            {
                _money = value;
            }
        }

        public int GoldNum
        {
            get
            {
                return _goldNum;
            }

            set
            {
                _goldNum = value;
            }
        }

        public WXPayResultRet()
        {
        }

        public static WXPayResultRet create()
        {
            WXPayResultRet wxprr = new WXPayResultRet();
            return wxprr;
        }

        public void decode(Input _in)
        {
            Result = _in.readBoolean();
            Money = _in.readInt();
            GoldNum = _in.readInt();
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
            return "WXPay[result=" + Result + "money=" + Money + "goldNum="+GoldNum+" ]";
        }
    }
}
