
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.login
{
    public class Radio : Message
    {
        public static int TYPE = 7;
        public static int ID = 16;

        public static Radio create(string radio)
        {
            Radio radioR = new Radio();
            radioR._radio = radio;
            return radioR;
        }
        //广播 （跑马灯）
        protected string _radio;

        public Radio()
        {

        }
        public void decode(Input _in)
        {
            _radio = _in.readString();
        }

        public void encode(Output _out)
        {
           _out.writeString(radio);
        }
        public string radio
        {
            get { return _radio; }
            set { _radio = value; }
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
            return "Radio [radio=" + _radio + ", ]";
        }
    }
}