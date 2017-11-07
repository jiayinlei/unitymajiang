
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;

namespace com.guojin.dn.net.message {

    public class DouniuChat : Message {

        public static int TYPE = 5;
        public static int ID = 50;


        private int index; // 聊天的定义的类型：1 文字 2 表情 3 快捷短语聊天 4 语音
        private String chatContent;//聊天的内容

        public void decode(Input _in) {
            // TODO Auto-generated method stub
            chatContent = _in.readString();
            index = _in.readInt();

        }

        public void encode(Output _out) {
            // TODO Auto-generated method stub
            _out.writeString(chatContent);
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



        public int getIndex() {
            return index;
        }

        public void setIndex(int index) {
            this.index = index;
        }

        public String toString() {
            return "DouniuChat [index=" + index + ", chatContent=" + chatContent
                    + "]";
        }

        public String getChatContent() {
            return chatContent;
        }

        public void setChatContent(String chatContent) {
            this.chatContent = chatContent;
        }


    }
}