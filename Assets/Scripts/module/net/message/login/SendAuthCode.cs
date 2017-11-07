using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.login
{
    //发送验证码
    public class SendAuthCode : Message
    {

        public static int TYPE = 7;
        public static int ID = 21;

        public static SendAuthCode create(string phone )
        {
            SendAuthCode sendAuthCode = new SendAuthCode();
            sendAuthCode._phone = phone;
            return sendAuthCode;
        }

        protected string _phone;
        public void decode(Input _in)
        {
            _phone = _in.readString();
        }

        public void encode(Output _out)
        {
           _out.writeString(phone);
        }
        public string phone
        {
            get { return _phone; }
            set { _phone = value; }
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
            return "SendAuthCode [phone=" + _phone + ", ]";
        }
    }
}