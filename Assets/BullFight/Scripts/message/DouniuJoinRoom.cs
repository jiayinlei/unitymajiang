using com.guojin.core.io;
using com.guojin.core.io.message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/* ***********************************************
 * Describe:该类功能描述
 * Author :  MuLongFei
 * Email: 424113771@qq.com
 * DATA: 2017/7/29 16:44:17 
 * FileName: DNJoinRoom 
 * Version: V1.0.1
 * ***********************************************/

namespace com.guojin.dn.net.message {
    /// <summary>
    /// 加入房间信息
    /// </summary>
    class DouniuJoinRoom : Message {
        public static int TYPE = 5;
        public static int ID = 2;
        /// <summary>
        /// 根据传入的房间信息，对_roomcheckID进行赋值，并返回创建房间的信息类
        /// </summary>
        /// <param name="roomCheckId"></param>
        /// <returns></returns>
        public static DouniuJoinRoom create(string roomCheckId) {
            DouniuJoinRoom joinRoom = new DouniuJoinRoom();
            joinRoom._roomCheckId = roomCheckId;
            return joinRoom;
        }
        protected string _roomCheckId;
        public void decode(Input _in) {
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
            return "DNJoinRoom [roomCheckId=" + _roomCheckId + ", ]";
        }
    }
}
