
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
using System.Collections.Generic;

/**
 * 一局斗牛的消息
 * 
 * <b>生成器生成代码，请勿修改，扩展请继承</b>
 * @author isnowfox消息生成器
 */
namespace com.guojin.dn.net.message {
    public class DouniuExitRoomRet : Message {
        public static int TYPE = 5;
        public static int ID = 23;


        int userID;

        public DouniuExitRoomRet() {

        }

        public DouniuExitRoomRet(int userID) {
            this.userID = userID;
        }


        public void decode(Input _in) {

            userID = _in.readInt();
        }


        public void encode(Output _out) {
            _out.writeInt(userID);
        }



        public int getUserID() {
            return userID;
        }

        public void setUserID(int userID) {
            this.userID = userID;
        }

        public String toString() {
            return "ExitDouniuRoomResult [userID=" + userID + "]";
        }


        public int getMessageType() {
            return TYPE;
        }


        public int getMessageId() {
            return ID;
        }
    }
}
