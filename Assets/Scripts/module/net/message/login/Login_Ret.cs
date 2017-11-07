
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.login
{
    //登录结果
    public class Login_Ret : Message
    {
        public static int TYPE = 7;
        public static int ID  = 10;

        public static Login_Ret create(int id,string name ,string openId,string uuid,string avatar,int sex,string roomCheckId,int gold,string loginToken,string ip,int level)
        {
            Login_Ret loginRet = new Login_Ret();
            loginRet._id = id;
            loginRet._name = name;
            loginRet._openId = openId;
            loginRet._uuid = uuid;
            loginRet._avatar = avatar;
            loginRet._sex = sex;
            loginRet._roomCheckId = roomCheckId;
            loginRet._gold = gold;
            loginRet._loginToken = loginToken;
            loginRet._ip = ip;
            loginRet._level = level;
            return loginRet;
        }
        //id
        protected int _id;
        //昵称
        protected string _name;
        //昵称
        protected string _openId;
        //UUID
        protected string _uuid;
        //头像
        protected string _avatar;
        //0：女生，1：男生，2 ：未知
        protected int _sex;

        //如果用户进入过房间 未主动退出房间id
        protected string _roomCheckId;
        protected int _gold;
        protected string _loginToken;
        protected string _ip;
        protected int _level;
        public Login_Ret()
        {

        }

     
        public void decode(Input _in)
        {
            _id = _in.readInt();
            _name = _in.readString();
            _openId = _in.readString();
            _uuid = _in.readString();
            _avatar = _in.readString();
            _sex = _in.readInt();
            _roomCheckId = _in.readString();
            _gold = _in.readInt();
            _loginToken = _in.readString();
            _ip = _in.readString();
            _level = _in.readInt();
        }

        public void encode(Output _out)
        {
            _out.writeInt(id);
			_out.writeString(name);
			_out.writeString(openId);
			_out.writeString(uuid);
			_out.writeString(avatar);
			_out.writeInt(sex);
			_out.writeString(roomCheckId);
			_out.writeInt(gold);
			_out.writeString(loginToken);
			_out.writeString(ip);
            _out.writeInt(level);
        }
        public int level
        {
            get { return _level; }
            set { _level = value; }
        }
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string openId
        {
            get { return _openId; }
            set { _openId = value; }
        }
        public string uuid
        {
            get { return _uuid; }
            set { _uuid = value; }
        }
        public string avatar
        {
            get { return _avatar; }
            set { _avatar = value; }
        }
        public int sex
        {
            get { return _sex; }
            set { _sex = value; }
        }
        public string roomCheckId
        {
            get { return _roomCheckId; }
            set { _roomCheckId = value; }
        }
        public int gold
        {
            get { return _gold; }
            set { _gold = value; }
        }
        public string loginToken
        {
            get { return _loginToken; }
            set { _loginToken = value; }
        }
        public string ip
        {
            get { return _ip; }
            set { _ip = value; }
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
            return "LoginRet [id=" + _id + ",name=" + _name + ",openId=" + _openId + ",uuid=" + _uuid + ",avatar=" + _avatar + ",sex=" + _sex + ",roomCheckId=" + _roomCheckId + ",gold=" + _gold + ",loginToken=" + _loginToken + ",ip=" + _ip + ", ]";
        }
    }
}
