using System;
using com.guojin.core.io;
using com.guojin.core.io.message;

/**
 * 开始游戏回复
 * @author chenmengchen
 *
 */
namespace com.guojin.dn.net.message {

    /**
     * 用户离线
     * 
     * <b>生成器生成代码，请勿修改，扩展请继承</b>
     * @author isnowfox消息生成器
     */
    public class DouniuUserOffline : Message {
        public static int TYPE = 5;
        public static int ID = 44;

        private int index;//离线的玩家的索引

        public DouniuUserOffline() {

        }

        public DouniuUserOffline(int index) {
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



        public String toString() {
            return "DouniuUserOffline [index=" + index + ", ]";
        }


        public int getMessageType() {
            return TYPE;
        }


        public int getMessageId() {
            return ID;
        }
    }
}