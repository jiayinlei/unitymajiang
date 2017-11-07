
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;

/**
 * 
* @ClassName: PdkStartGame
* @Description: 斗牛开始游戏消息
* 
* 前段发送房主开始解散房间 
* @author qixianghui
* @date 2017年7月10日
* */

namespace com.guojin.dn.net.message {

    public class DouniuChatRet : Message {

        public static int TYPE = 5;
        public static int ID = 51;

        private int userindex;//用户的位置索引
        private String chatContent;//聊天的neirogn
        private int index;//定义的聊天类型： 1 文字 2 表情 3 快捷短语聊天 4 语音


        public int getUserindex() {
            return userindex;
        }

        public void setUserindex(int userindex) {
            this.userindex = userindex;
        }

        public String getChatContent() {
            return chatContent;
        }

        public void setChatContent(String chatContent) {
            this.chatContent = chatContent;
        }

        public static int getType() {
            return TYPE;
        }

        public static int getId() {
            return ID;
        }

        public void decode(Input _in) {
            // TODO Auto-generated method stub
            this.userindex = _in.readInt();
            this.chatContent = _in.readString();
            this.index = _in.readInt();
        }


        public void encode(Output _out) {
            // TODO Auto-generated method stub
            _out.writeInt(this.userindex);
            _out.writeString(this.chatContent);
            _out.writeInt(index);

        }

        public int getMessageId() {
            // TODO Auto-generated method stub
            return ID;
        }

        public int getMessageType() {
            // TODO Auto-generated method stub
            return TYPE;
        }

        public String toString() {
            return "ChatRet [chatContent=" + chatContent + "]";
        }

        public int getIndex() {
            return index;
        }

        public void setIndex(int index) {
            this.index = index;
        }
    }
}
