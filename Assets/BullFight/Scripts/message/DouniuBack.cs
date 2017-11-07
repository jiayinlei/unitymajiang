using System;
using com.guojin.core.io;
using com.guojin.core.io.message;

/**
 * 开始游戏回复
 * @author chenmengchen
 *
 */
namespace com.guojin.dn.net.message {
    public class DouniuBack : Message {
        public static int TYPE = 5;
        public static int ID = 46;

        public DouniuBack() {

        }


        public void decode(Input _in) {
        }


        public void encode(Output _out) {
        }



        public string toString() {
            return "DouniuBack [ ]";
        }


        public int getMessageType() {
            return TYPE;
        }


        public int getMessageId() {
            return ID;
        }
    }
}
