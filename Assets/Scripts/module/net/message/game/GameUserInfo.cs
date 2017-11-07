
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.game
{
    //同步游戏信息
    public class GameUserInfo : Message
    {
        public static int TYPE = 1;
        public static int ID = 8;
        
        public static GameUserInfo create(string userName,string avatar,int sex,int gold,int score,int locationIndex,int userId,bool online,string ip   ,string user0Distance, string user1Distance,string user2Distance, string user3Distance,bool isready, string jing, string wei)
        {
            GameUserInfo gameUserInfo = new GameUserInfo();
            gameUserInfo._userName = userName;
            gameUserInfo._avatar = avatar;
            gameUserInfo._sex = sex;
            gameUserInfo._gold = gold;
            gameUserInfo._score = score;
            gameUserInfo._locationIndex = locationIndex;
            gameUserInfo._userId = userId;
            gameUserInfo._online = online;
            gameUserInfo._ip = ip;
            gameUserInfo._user0Distance = user0Distance;
            gameUserInfo._user1Distance = user1Distance;
            gameUserInfo._user2Distance = user2Distance;
            gameUserInfo._user3Distance = user3Distance;
            gameUserInfo._isready = isready;
            gameUserInfo._jing = jing;
            gameUserInfo._wei = wei;
            return gameUserInfo;
        }

        protected string _userName;
        protected string _avatar;
        //0:女生 1 男生 2 未知
        protected int _sex;
        protected int _gold;
        protected int _score;
        protected int _locationIndex;
        protected int _userId;
        protected bool _online;
        protected string _ip;
        protected string _user0Distance;
        protected string _user1Distance;
        protected string _user2Distance;
        protected string _user3Distance;
        protected bool _isready;
        protected string _jing;
        protected string _wei;
        public GameUserInfo()
        {

        }


        public void decode(Input _in)
        {
            _userName = _in.readString();
            _avatar = _in.readString();
            _sex = _in.readInt();
            _gold = _in.readInt();
            _score = _in.readInt();
            _locationIndex = _in.readInt();
            _userId = _in.readInt();
            _online = _in.readBoolean();
            _ip = _in.readString();
            _user0Distance = _in.readString();
            _user1Distance = _in.readString();
            _user2Distance = _in.readString();
            _user3Distance = _in.readString();
            _isready = _in.readBoolean();
            _jing = _in.readString();
            _wei = _in.readString();
        }

        public void encode(Output _out)
        {
            _out.writeString(userName);
            _out.writeString(avatar);
            _out.writeInt(sex);
            _out.writeInt(gold);
            _out.writeInt(score);
            _out.writeInt(locationIndex);
            _out.writeInt(userId);
            _out.writeBoolean(online);
            _out.writeString(ip);
            _out.writeString(user0Distance);
            _out.writeString(user1Distance);
            _out.writeString(user2Distance);
            _out.writeString(user3Distance);
            _out.writeBoolean(isready);
            _out.writeString(jing);
            _out.writeString(wei);
        }
        public string jing
        {
            get { return _jing; }
            set { _jing = value; }
        }
        public string wei
        {
            get { return _wei; }
            set { _wei = value; }
        }

        public bool isready
        {
            get{ return _isready;}
            set{_isready = value;}
        }
        public string userName
        {
            get { return _userName; }
            set { _userName = value; }
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
        public int gold
        {
            get { return _gold; }
            set { _gold = value; }
        }
        public int score
        {
            get { return _score; }
            set { _score = value; }
        }
        public int locationIndex
        {
            get { return _locationIndex; }
            set { _locationIndex = value; }
        }
        public int userId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        public bool online
        {
            get { return _online; }
            set { _online = value; }
        }
        public string  ip
        {
            get { return _ip; }
            set { _ip = value; }
        }
        public string  user0Distance
        {
            get { return _user0Distance; }
            set { _user0Distance = value; }
        }
        public string user1Distance
        {
            get { return _user1Distance; }
            set { _user1Distance = value; }
        }
        public string user2Distance
        {
            get { return _user2Distance; }
            set { _user2Distance = value; }
        }
        public string user3Distance
        {
            get { return _user3Distance; }
            set { _user3Distance = value; }
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
            return "GameUserInfo [userName=" + _userName + ",avatar=" + _avatar + ",sex=" + _sex + ",gold=" + _gold + ",score=" + _score + ",locationIndex=" + _locationIndex + ",userId=" + _userId + ",online=" + _online + ",ip=" + _ip + ",user0Distance=" + _user0Distance + ",user1Distance=" + _user1Distance + ",user2Distance=" + _user2Distance + ",user3Distance=" + _user3Distance + ", ]";
        }
    }
}