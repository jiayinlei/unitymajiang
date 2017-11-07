
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.login
{
    //登录信息
    public class Login : Message
    {

        public static int TYPE = 7;
        public static int ID = 8;

        public static Login create(string type ,string openId,string code,string longitude,string latitude)
        {
            Login login = new Login();

            login._type = type;
            login._openId = openId;
            login._code = code;
            login._longitude = longitude;
            login._latitude = latitude;
            return login;
        }

        protected string _type;
        protected string _openId;
        protected string _code;

        //经度
        protected string _longitude;
        //纬度
        protected string _latitude;

        public Login()
        {

        }
        public void decode(Input _in)
        {
            _type = _in.readString();
            _openId = _in.readString();
            _code = _in.readString();
            _longitude = _in.readString();
            _latitude = _in.readString();
        }

        public void encode(Output _out)
        {
            _out.writeString(type);
			_out.writeString(openId);
			_out.writeString(code);
			_out.writeString(longitude);
			_out.writeString(latitude);
        }

        public string type
        {
            get { return _type; }
            set { _type = value; }
        }
        public string openId
        {
            get { return _openId; }
            set { _openId = value; }
        }
        public string code
        {
            get { return _code; }
            set { _code = value; }
        }

        //经度
        public string longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }
        //纬度
        public string latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        public string toString()
        {
            return "Login [type=" + _type + ",openId=" + _openId + ",code=" + _code + ",longitude=" + _longitude + ",latitude=" + _latitude + ", ]";
        }
        public int getMessageId()
        {
            return ID;
        }

        public int getMessageType()
        {
            return TYPE;
        }

      
    }
}