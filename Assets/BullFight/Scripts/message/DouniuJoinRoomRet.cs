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
 * DATA: 2017/7/29 16:52:05 
 * FileName: DNJoinRoomRet 
 * Version: V1.0.1
 * ***********************************************/

namespace com.guojin.dn.net.message {
    /// <summary>
    /// 服务器返回的加入房间信息
    /// </summary>
    class DouniuJoinRoomRet : Message {
        public static int TYPE = 5;
        public static int ID = 3;

        public static DouniuJoinRoomRet create(bool result) {
            DouniuJoinRoomRet joinRoomRet = new DouniuJoinRoomRet();
            joinRoomRet._result = result;
            return joinRoomRet;
        }
        protected bool _result;

        public DouniuJoinRoomRet() {

        }
        public void decode(Input _in) {
            _result = _in.readBoolean();
        }

        public void encode(Output _out) {
            _out.writeBoolean(result);
        }
        public bool result {
            get {
                return _result;
            }
            set {
                _result = value;
            }
        }
        public int getMessageId() {
            return ID;
        }

        public int getMessageType() {
            return TYPE;
        }

        public string toString() {
            return "DNJoinRoomRet [result=" + _result + ", ]";
        }
    }
}
