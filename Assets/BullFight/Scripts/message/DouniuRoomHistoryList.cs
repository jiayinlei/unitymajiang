using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.dn.net.message {
    public class DouniuRoomHistoryList : Message {
        public static int TYPE = 5;
        public static int ID = 48;


        public DouniuRoomHistoryList() {

        }


        public void decode(Input _in) {
        }


        public void encode(Output _out) {
        }



        public String toString() {
            return "DouniuRoomHistoryList [ ]";
        }


        public int getMessageType() {
            return TYPE;
        }


        public int getMessageId() {
            return ID;
        }
    }
}
