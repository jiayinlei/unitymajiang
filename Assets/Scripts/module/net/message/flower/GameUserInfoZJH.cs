using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.flower
{
    /// <summary>
    /// 炸金花玩家信息
    /// </summary>
    public class GameUserInfoZJH : Message
    {

        public static int TYPE = 3;
        public static int ID = 32;

        public static GameUserInfoZJH create(string userName, string avatar, int sex, string score, int locationIndex, int userId, bool online, string ip)
        {
            GameUserInfoZJH gameUserInfoZJH = new GameUserInfoZJH();
            gameUserInfoZJH._userName = userName;
            gameUserInfoZJH._avatar = avatar;
            gameUserInfoZJH._sex = sex;
            gameUserInfoZJH._score = score;
            gameUserInfoZJH._locationIndex = locationIndex;
            gameUserInfoZJH._userId = userId;
            gameUserInfoZJH._online = online;
            gameUserInfoZJH._ip = ip;
            return gameUserInfoZJH;
        }

        protected string _userName;
        protected string _avatar;
        //0:女生 1 男生 2 未知
        protected int _sex;
        protected string _score;
        protected int _locationIndex;
        protected int _userId;
        protected bool _online;
        protected string _ip;

        public GameUserInfoZJH()
        {

        }


        public void decode(Input _in)
        {
            _userName = _in.readString();
            _avatar = _in.readString();
            _sex = _in.readInt();
            _score = _in.readString();
            _locationIndex = _in.readInt();
            _userId = _in.readInt();
            _online = _in.readBoolean();
            _ip = _in.readString();
        }

        public void encode(Output _out)
        {
            _out.writeString(userName);
            _out.writeString(avatar);
            _out.writeInt(sex);
            _out.writeString(score);
            _out.writeInt(locationIndex);
            _out.writeInt(userId);
            _out.writeBoolean(online);
            _out.writeString(ip);
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
        public string score
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
            return "GameUserInfoZJH [userName=" + _userName + ",avatar=" + _avatar + ",sex=" + _sex + ",score=" + _score + ",locationIndex=" + _locationIndex + ",userId=" + _userId + ",online=" + _online + ",ip=" + _ip + ", ]";
        }
    }
}

