
using com.guojin.core.io.message;
using com.guojin.core.io;
using System;

namespace com.guojin.mj.net.message.login
{
    public  class SysSetting : Message
    {

        public static int TYPE = 7;
        public static int ID = 24;
        /// <summary>
        /// 接收系统提示
        /// </summary>
        /// <param name="radio"></param>
        /// <param name="notice"></param>
        /// <param name="payInfo"></param>
        /// <param name="agreement"></param>
        /// <returns></returns>
        public static SysSetting create(string radio,string notice,string payInfo,string agreement )
        {
            SysSetting sysSetting = new SysSetting();
            sysSetting._radio = radio;
            sysSetting._notice = notice;
            sysSetting._payInfo = payInfo;
            sysSetting._agreement = agreement;
            return sysSetting;
        }

        //广播（跑马灯）
        protected string _radio;
        protected string _notice;
        protected string _payInfo;
        protected string _agreement;

        public SysSetting()
        {

        }
        public void decode(Input _in)
        {
            _radio = _in.readString();
            _notice = _in.readString();
            _payInfo = _in.readString();
            _agreement = _in.readString();
        }

        public void encode(Output _out)
        {
           _out.writeString(radio);
			_out.writeString(notice);
			_out.writeString(payInfo);
			_out.writeString(agreement);
        }
        public string radio
        {
            get { return _radio; }
            set { _radio = value; }
        }
        public string notice
        {
            get { return _notice; }
            set { _notice = value; }
        }
        public string payInfo
        {
            get { return _payInfo; }
            set { _payInfo = value; }
        }
        public string agreement
        {
            get { return _agreement; }
            set { _agreement = value; }
        }
        public int getMessageType()
        {
            return TYPE;
        }

        public int getMessageId()
        {
            return ID;
        }

        public string toString()
        {
            return "SysSetting [radio=" + _radio + ",notice=" + _notice + ",payInfo=" + _payInfo + ",agreement=" + _agreement + ", ]";
        }
    }
}