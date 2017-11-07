using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
using System.Collections.Generic;

/**
 * ���������û�����ע����
 * 
 * <b>���������ɴ��룬�����޸ģ���չ��̳�</b>
 * 1. �ڽ�ɢ�����ǣ� ���������ֻ��һ���ˣ�����˾ͷ��������Ϣ��ֱ��ɾ������
 * 2. 
 * 
 * @author isnowfox��Ϣ������
 */
namespace com.guojin.dn.net.message {

    public class DouniuVoteRet : Message {
        
        
        public static int TYPE = 5;
        public static int ID = 45;

        private bool result;//ͶƱ�Ľ��
        private string userName;//ͶƱ����ҵ�����
        private bool operation;//ȷ�������Ծ֣�������ͬ��ͶƱ��

        public string UserName {
            get {
                return userName;
            }

            set {
                userName = value;
            }
        }

        public bool Operation {
            get {
                return operation;
            }

            set {
                operation = value;
            }
        }

        public DouniuVoteRet() {
        }


        public DouniuVoteRet(bool result,string userName) {
            this.result = result;
            UserName = userName;
        }


        public void decode(Input _in) {
            this.result = _in.readBoolean();
            this.UserName = _in.readString();
            this.Operation = _in.readBoolean();
        }

        public void encode(Output _out) {
            _out.writeBoolean(isResult());
            _out.writeString(UserName);
            _out.writeBoolean(Operation);
        }

        public bool isResult() {
            return result;
        }


        public void setResult(bool result) {
            this.result = result;
        }


        public int getMessageId() {
            // TODO Auto-generated method stub
            return ID;
        }

        public int getMessageType() {
            // TODO Auto-generated method stub
            return TYPE;
        }

        public string toString() {
            return "DouniuVoteRet";
        }
    }
}
