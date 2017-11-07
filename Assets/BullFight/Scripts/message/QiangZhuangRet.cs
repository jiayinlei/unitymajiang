using System;
using com.guojin.core.io;
using com.guojin.core.io.message;

/**
 * 返回其他用户的下注数量
 * 
 * <b>生成器生成代码，请勿修改，扩展请继承</b>
 * @author isnowfox消息生成器
 */
namespace com.guojin.dn.net.message {
    public class QiangZhuangRet : Message {
        public static int TYPE = 5;
        public static int ID = 35;

        private int locationIndex;//庄的索引
        private bool lookPoker;//是否看牌

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
