using System;
using com.guojin.core.io;
using com.guojin.core.io.message;

/**
 * 
* @ClassName: PdkStartGame
* @Description: 斗牛开始游戏消息
*  如果房间里边除了房主还有其他的用户，返回给你这个消息，开始给服务端发送投票消息 
*  
* @author qixianghui
* @date 2017年7月10日
* */

namespace com.guojin.dn.net.message {
    public class DouniuDelRoomRet : Message {
        public static int TYPE = 5;
        public static int ID = 42;

        private int userId;//请求的解散房间的玩家的索引
        private String userName;//请求的解散房间的玩家的名字

        public DouniuDelRoomRet() {

        }

        public DouniuDelRoomRet(int userId, String userName) {
            this.userId = userId;
            this.userName = userName;
        }
        public void decode(Input _in) {
            userId = _in.readInt();
            userName = _in.readString();
        }

        public void encode(Output _out) {
            _out.writeInt(getUserId());
            _out.writeString(getUserName());
        }

        public int getUserId() {
            return userId;
        }

        public void setUserId(int userId) {
            this.userId = userId;
        }

        public String getUserName() {
            return userName;
        }

        public void setUserName(String userName) {
            this.userName = userName;
        }


        public String toString() {
            return "DouniuVoteDelSelect [userId=" + userId + ",userName=" + userName + ", ]";
        }

        public int getMessageType() {
            return TYPE;
        }

        public int getMessageId() {
            return ID;
        }
    }
}
