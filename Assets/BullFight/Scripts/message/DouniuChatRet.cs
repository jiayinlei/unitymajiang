
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;

/**
 * 
* @ClassName: PdkStartGame
* @Description: ��ţ��ʼ��Ϸ��Ϣ
* 
* ǰ�η��ͷ�����ʼ��ɢ���� 
* @author qixianghui
* @date 2017��7��10��
* */

namespace com.guojin.dn.net.message {

    public class DouniuChatRet : Message {

        public static int TYPE = 5;
        public static int ID = 51;

        private int userindex;//�û���λ������
        private String chatContent;//�����neirogn
        private int index;//������������ͣ� 1 ���� 2 ���� 3 ��ݶ������� 4 ����


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
