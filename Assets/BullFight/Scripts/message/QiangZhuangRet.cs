using System;
using com.guojin.core.io;
using com.guojin.core.io.message;

/**
 * ���������û�����ע����
 * 
 * <b>���������ɴ��룬�����޸ģ���չ��̳�</b>
 * @author isnowfox��Ϣ������
 */
namespace com.guojin.dn.net.message {
    public class QiangZhuangRet : Message {
        public static int TYPE = 5;
        public static int ID = 35;

        private int locationIndex;//ׯ������
        private bool lookPoker;//�Ƿ���

        public bool LookPoker {
            get {
                return lookPoker;
            }

            set {
                lookPoker = value;
            }
        }

        public int LocationIndex {
            get {
                return locationIndex;
            }

            set {
                locationIndex = value;
            }
        }

        public QiangZhuangRet() {

        }

        public QiangZhuangRet(int index,bool lookPoker) {
            LocationIndex = index;
            this.LookPoker = lookPoker;
        }

        public void decode(Input _in) {
            // TODO Auto-generated method stub
            LocationIndex = _in.readInt();
            LookPoker = _in.readBoolean();

        }

        public void encode(Output _out) {
            // TODO Auto-generated method stub
            _out.writeInt(LocationIndex);
            _out.writeBoolean(LookPoker);

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
            return "QiangZhuangRet";
        }
    }
}
