using System;
using com.guojin.core.io;
using com.guojin.core.io.message;

/**
 * 
* @ClassName: PdkStartGame
* @Description: ��ţ��ʼ��Ϸ��Ϣ
* @author qixianghui
* 
* ǰ�η��͸�����ˣ�ͶƱ��Ϣ
* @date 2017��7��10��
* */

namespace com.guojin.dn.net.message {

    public class DouniuVote : Message {

        public static int TYPE = 5;
        public static int ID = 43;

        private bool result;//ͶƱ�����ݣ��Ƿ�ͬ��
        private int userId;//ͶƱ����ҵ��û�ID

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
