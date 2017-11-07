using com.guojin.core.io;
using com.guojin.core.io.message;
using System;
/**
 * 一张牌的信息
 * @author Administrator
 *
 */
namespace com.guojin.dn.net.message {
    public class DouniuOut : Message {

        public static int TYPE = 5;
        public static int ID = 18;

        private int index;

        public DouniuOut() {

        }

        public DouniuOut(int index) {
            this.index = index;
        }

        public void decode(Input _in) {

            index = _in.readInt();
        }

        public void encode(Output _out) {
            _out.writeInt(getIndex());
        }

        public int getIndex() {
            return index;
        }

        public void setIndex(int index) {
            this.index = index;
        }

        public string toString() {
            return "DouNiuSay [index=" + index + ", ]";
        }

        public int getMessageType() {
            return TYPE;
        }

        public int getMessageId() {
            return ID;
        }
    }
}
