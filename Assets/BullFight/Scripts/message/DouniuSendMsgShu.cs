using System;
using com.guojin.core.io;
using com.guojin.core.io.message;

/**
 * 发送服务器下注的倍数
 * 
 * <b>生成器生成代码，请勿修改，扩展请继承</b>
 * @author isnowfox消息生成器
 */
namespace com.guojin.dn.net.message {
    public class DouniuSendMsgShu : Message {
        public static int TYPE = 5;
        public static int ID = 26;

        /**
         * 下注，跟注，弃牌操作类型
         */
        private string opt;
        /**
         * 下注的倍数
         */
        private int value;

        public DouniuSendMsgShu() {

        }

        public DouniuSendMsgShu(string opt, int value) {
            this.opt = opt;
            this.value = value;
        }


        public void decode(Input _in) {
            opt = _in.readString();
            value = _in.readInt();
        }


        public void encode(Output _out) {
            _out.writeString(getOpt());
            _out.writeInt(getValue());
        }

        /**
         * 下注，跟注，弃牌操作类型
         */
        public string getOpt() {
            return opt;
        }

        /**
         * 下注，跟注，弃牌操作类型
         */
        public void setOpt(string opt) {
            this.opt = opt;
        }

        /**
         * 下注的倍数
         */
        public int getValue() {
            return value;
        }

        /**
         * 下注的倍数
         */
        public void setValue(int value) {
            this.value = value;
        }



        public string toString() {
            return "DouNiuSendMsgZhu [opt=" + opt + ",value=" + value + ", ]";
        }


        public int getMessageType() {
            return TYPE;
        }


        public int getMessageId() {
            return ID;
        }
    }
}
