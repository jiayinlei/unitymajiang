using System;
using com.guojin.core.io;
using com.guojin.core.io.message;

/**
 * 
* @ClassName: PdkStartGame
* @Description: 斗牛开始游戏消息
* @author qixianghui
* 
* 前段发送给服务端，投票消息
* @date 2017年7月10日
* */

namespace com.guojin.dn.net.message {

    public class DouniuVote : Message {

        public static int TYPE = 5;
        public static int ID = 43;

        private bool result;//投票的内容，是否同意
        private int userId;//投票的玩家的用户ID

        public DouniuVote() {

        }

        public DouniuVote(bool result, int userId) {
            this.result = result;
            this.userId = userId;
        }
        public void decode(Input _in) {
            result = _in.readBoolean();
            userId = _in.readInt();
        }
        public void encode(Output _out) {
            _out.writeBoolean(getResult());
            _out.writeInt(getUserId());
        }

        public bool getResult() {
            return result;
        }

        public void setResult(bool result) {
            this.result = result;
        }

        public int getUserId() {
            return userId;
        }

        public void setUserId(int userId) {
            this.userId = userId;
        }

        public String toString() {
            return "DouniuVoteDelSelectRet [result=" + result + ",userId=" + userId + ", ]";
        }

        public int getMessageType() {
            return TYPE;
        }

        public int getMessageId() {
            return ID;
        }
    }
}
