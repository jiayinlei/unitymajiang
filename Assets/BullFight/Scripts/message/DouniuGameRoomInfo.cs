
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.dn.net.message {
    /// <summary>
    /// 准备就绪，通知服务器可以开始发送房间信息了
    /// </summary>
    public class DouniuGameRoomInfo : Message {

        public static int TYPE = 5;
        public static int ID = 28;
        public static DouniuGameRoomInfo create(string roomCheckId) {
            DouniuGameRoomInfo joinRoom = new DouniuGameRoomInfo();
            joinRoom._roomCheckId = roomCheckId;
            return joinRoom;
        }
        protected string _roomCheckId;
        public void decode(Input _in) {
            UnityEngine.Debug.Log("deaaaaa");
            _roomCheckId = _in.readString();
        }

        public void encode(Output _out) {
            _out.writeString(roomCheckId);
        }
        public string roomCheckId {
            get {
                return _roomCheckId;
            }
            set {
                _roomCheckId = value;
            }
        }
        public int getMessageId() {
            return ID;
        }

        public int getMessageType() {
            return TYPE;
        }

        public string toString() {
            return "DNGameJoinRoom [ ]通知服务器可以开始发送房间信息了";
        }
    }
}