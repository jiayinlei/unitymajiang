using System.Collections;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.login
{

   public  class payPrice : Message
    {

        public static int TYPE = 99;
        public static int ID = 98;

        public payPrice()
        {
        }

        public int BtnPrice
        {
            get { return _btnPrice; }
            set { _btnPrice = value; }
        }

        public int DiamondPrice
        {
            get { return _diamondPrice; }
            set { _diamondPrice = value; }
        }

        public static payPrice create(int btnprice, int diamondprice)
        {
            payPrice pp = new payPrice();
            pp._btnPrice = btnprice;
            pp._diamondPrice = diamondprice;
            return pp;
        }

        private int _btnPrice;                //按钮显示价格
        private int _diamondPrice;       //房卡数量


        public void decode(Input _in)
        {
            BtnPrice = _in.readInt();
            DiamondPrice = _in.readInt();
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
            return "WeiXinShow [ ]";
        }
    }
}
