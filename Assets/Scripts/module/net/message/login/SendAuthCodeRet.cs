
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.login
{
    //发送验证码回执
    public class SendAuthCodeRet : Message
    {
        public static int TYPE = 7;
        public static int ID = 22;

        public static SendAuthCodeRet create(bool success)
        {
            SendAuthCodeRet sendAuthCodeRet = new SendAuthCodeRet();
            sendAuthCodeRet._success = success;
            return sendAuthCodeRet;


        }

        protected bool _success;
        public SendAuthCodeRet()
        {

        }
        public void decode(Input _in)
        {
            _success = _in.readBoolean();
        }

        public void encode(Output _out)
        {
            _out.writeBoolean(success);
        }
        public bool success
        {
            get { return _success; }
            set { _success = value; }
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
            return "SendAuthCodeRet [success=" + _success + ", ]";
        }
    }
}