
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
    public class DouniuDelRoom : Message {
        public static int TYPE = 5;
        public static int ID = 41;


        public DouniuDelRoom() {

        }

        public void decode(Input _in) {
        }
        public void encode(Output _out) {
        }


        public String toString() {
            return "DouniuVoteDelStart [ ]";
        }

        public int getMessageType() {
            return TYPE;
        }

        public int getMessageId() {
            return ID;
        }
    }
}