using System;
using com.guojin.core.io;
using com.guojin.core.io.message;

/**
 * 同步玩家游戏信息
 * 
 * <b>生成器生成代码，请勿修改，扩展请继承</b>
 * @author isnowfox消息生成器
 */
namespace com.guojin.dn.net.message {
    public class DouniuGameUserInfo : Message {
        public static int TYPE = 5;
        public static int ID = 25;

        private string userName;//玩家名字
        private string avatar;//头像地址
        private int sex;//性别（0:女生,1:男生,2:未知）
        private int gold;//金币（房卡）
        private int score;//分数
        private int locationIndex;//玩家索引
        private int userId;//玩家ID
        private bool online;//是否在线
        private string ip;//IP地址
        private string user0Distance;
        private string user1Distance;
        private string user2Distance;
        private string user3Distance;
        private string user4Distance;
        private string user5Distance;
        private int playerNumber;//玩家数量
        private int handPokerCount;//手牌数量
        private bool isReady;//是否准备了
        private bool isOperation;//用户在当前的房间状态中是否操作

        public int PlayerNumber {
            get {
                return playerNumber;
            }

            set {
                playerNumber = value;
            }
        }

        public int HandPokerCount {
            get {
                return handPokerCount;
            }

            set {
                handPokerCount = value;
            }
        }

        public bool IsReady {
            get {
                return isReady;
            }

            set {
                isReady = value;
            }
        }

        public bool IsOperation {
            get {
                return isOperation;
            }

            set {
                isOperation = value;
            }
        }

        public DouniuGameUserInfo() {

        }

        public DouniuGameUserInfo(string userName, string avatar, int sex, int gold, int score, int locationIndex, int userId, bool online, string ip, string user0Distance, string user1Distance, string user2Distance, string user3Distance, string user4Distance, string user5Distance,int playerNum) {
            this.userName = userName;
            this.avatar = avatar;
            this.sex = sex;
            this.gold = gold;
            this.score = score;
            this.locationIndex = locationIndex;
            this.userId = userId;
            this.online = online;
            this.ip = ip;
            this.user0Distance = user0Distance;
            this.user1Distance = user1Distance;
            this.user2Distance = user2Distance;
            this.user3Distance = user3Distance;
            this.user4Distance = user4Distance;
            this.user5Distance = user5Distance;
            PlayerNumber = playerNum;
        }


        public void decode(Input _in) {
            userName = _in.readString();
            avatar = _in.readString();
            sex = _in.readInt();
            gold = _in.readInt();
            score = _in.readInt();
            locationIndex = _in.readInt();
            userId = _in.readInt();
            online = _in.readBoolean();
            ip = _in.readString();
            user0Distance = _in.readString();
            user1Distance = _in.readString();
            user2Distance = _in.readString();
            user3Distance = _in.readString();
            user4Distance = _in.readString();
            user5Distance = _in.readString();
            PlayerNumber = _in.readInt();
            HandPokerCount = _in.readInt();
            IsReady = _in.readBoolean();
            IsOperation = _in.readBoolean();
        }


        public void encode(Output _out) {
            _out.writeString(getUserName());
            _out.writeString(getAvatar());
            _out.writeInt(getSex());
            _out.writeInt(getGold());
            _out.writeInt(getScore());
            _out.writeInt(getLocationIndex());
            _out.writeInt(getUserId());
            _out.writeBoolean(getOnline());
            _out.writeString(getIp());
            _out.writeString(getUser0Distance());
            _out.writeString(getUser1Distance());
            _out.writeString(getUser2Distance());
            _out.writeString(getUser3Distance());
            _out.writeString(getUser4Distance());
            _out.writeString(getUser5Distance());
            _out.writeInt(PlayerNumber);
            _out.writeInt(HandPokerCount);
            _out.writeBoolean(IsReady);
            _out.writeBoolean(IsOperation);
        }

        public string getUserName() {
            return userName;
        }

        public void setUserName(string userName) {
            this.userName = userName;
        }

        public string getAvatar() {
            return avatar;
        }

        public void setAvatar(string avatar) {
            this.avatar = avatar;
        }

        /**
         * 0:女生,1:男生,2:未知
         */
        public int getSex() {
            return sex;
        }

        /**
         * 0:女生,1:男生,2:未知
         */
        public void setSex(int sex) {
            this.sex = sex;
        }

        public int getGold() {
            return gold;
        }

        public void setGold(int gold) {
            this.gold = gold;
        }

        public int getScore() {
            return score;
        }

        public void setScore(int score) {
            this.score = score;
        }

        public int getLocationIndex() {
            return locationIndex;
        }

        public void setLocationIndex(int locationIndex) {
            this.locationIndex = locationIndex;
        }

        public int getUserId() {
            return userId;
        }

        public void setUserId(int userId) {
            this.userId = userId;
        }

        public bool getOnline() {
            return online;
        }

        public void setOnline(bool online) {
            this.online = online;
        }

        public string getIp() {
            return ip;
        }

        public void setIp(string ip) {
            this.ip = ip;
        }

        public string getUser0Distance() {
            return user0Distance;
        }

        public void setUser0Distance(string user0Distance) {
            this.user0Distance = user0Distance;
        }

        public string getUser1Distance() {
            return user1Distance;
        }

        public void setUser1Distance(string user1Distance) {
            this.user1Distance = user1Distance;
        }

        public string getUser2Distance() {
            return user2Distance;
        }

        public void setUser2Distance(string user2Distance) {
            this.user2Distance = user2Distance;
        }

        public string getUser3Distance() {
            return user3Distance;
        }

        public void setUser3Distance(string user3Distance) {
            this.user3Distance = user3Distance;
        }


        public string getUser4Distance() {
            return user4Distance;
        }

        public void setUser4Distance(string user4Distance) {
            this.user4Distance = user4Distance;
        }

        public string getUser5Distance() {
            return user5Distance;
        }

        public void setUser5Distance(string user5Distance) {
            this.user5Distance = user5Distance;
        }


        public string toString() {
            return "DouniuGameUserInfo [userName=" + userName + ", avatar="
                    + avatar + ", sex=" + sex + ", gold=" + gold + ", score="
                    + score + ", locationIndex=" + locationIndex + ", userId="
                    + userId + ", online=" + online + ", ip=" + ip
                    + ", user0Distance=" + user0Distance + ", user1Distance="
                    + user1Distance + ", user2Distance=" + user2Distance
                    + ", user3Distance=" + user3Distance + ", user4Distance="
                    + user4Distance + ", user5Distance=" + user5Distance + ", playerNumber=" + PlayerNumber + "]";
        }


        public int getMessageType() {
            return TYPE;
        }


        public int getMessageId() {
            return ID;
        }
    }
}
