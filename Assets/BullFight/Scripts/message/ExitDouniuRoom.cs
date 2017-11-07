
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
    public class ExitDouniuRoom : Message {
        public static int TYPE = 5;
        public static int ID = 6;

        string roomID;

        public string RoomID {
            get {
                return roomID;
            }

            set {
                roomID = value;
            }
        }
        public ExitDouniuRoom() {

        }
        public ExitDouniuRoom(string roomID) {
            this.roomID = roomID;
        }

        public void decode(Input _in) {
            roomID = _in.readString();
        }

        public void encode(Output _out) {
            _out.writeString(RoomID);
        }


        public String toString() {
            return "ExitDouniuRoom [ ]";
        }

        public int getMessageType() {
            return TYPE;
        }

        public int getMessageId() {
            return ID;
        }
    }
}
