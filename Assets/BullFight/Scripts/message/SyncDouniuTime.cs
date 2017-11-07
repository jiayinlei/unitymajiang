using System;
using com.guojin.core.io;
using com.guojin.core.io.message;

/**
 * 同步操作
 * 
 * <b>生成器生成代码，请勿修改，扩展请继承</b>
 * @author isnowfox消息生成器
 */
namespace com.guojin.dn.net.message {
    public class SyncDouniuTime : Message {
        public static int TYPE = 5;
        public static int ID = 32;

        /**
         * 位置
         */
        private int index;
        /**
         * 毫秒
         */
        private int leftTime;

        public SyncDouniuTime() {

        }

        public SyncDouniuTime(int index, int leftTime) {
            this.index = index;
            this.leftTime = leftTime;
        }

        public void decode(Input _in) {
            index = _in.readInt();
            leftTime = _in.readInt();
        }
        public void encode(Output _out) {
            _out.writeInt(getIndex());
            _out.writeInt(getLeftTime());
        }

        /**
         * 位置
         */
        public int getIndex() {
            return index;
        }

        /**
         * 位置
         */
        public void setIndex(int index) {
            this.index = index;
        }

        /**
         * 毫秒
         */
        public int getLeftTime() {
            return leftTime;
        }

        /**
         * 毫秒
         */
        public void setLeftTime(int leftTime) {
            this.leftTime = leftTime;
        }


        public string toString() {
            return "SyncDouniuTime [index=" + index + ",leftTime=" + leftTime + ", ]";
        }
        public int getMessageType() {
            return TYPE;
        }

        public int getMessageId() {
            return ID;
        }
    }
}
