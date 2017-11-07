using System;
using com.guojin.core.io;
using com.guojin.core.io.message;


/* ***********************************************
 * Describe:该类功能描述
 * Author :  MuLongFei
 * Email: 424113771@qq.com
 * DATA: 2017/7/28 16:01:39 
 * FileName: DNCreateRoomRet 
 * Version: V1.0.1
 * ***********************************************/

namespace com.guojin.dn.net.message {
    /// <summary>
    /// 服务器返回创建房间的结果
    /// </summary>
    public class DouniuCreateRoomRet : Message {

        public static int TYPE = 5;
        public static int ID = 1;

        public static DouniuCreateRoomRet create(bool result, string roomCheckId) {
            DouniuCreateRoomRet createRoomRet = new DouniuCreateRoomRet();
            createRoomRet._result = result;
            createRoomRet._roomCheckId = roomCheckId;
            return createRoomRet;
        }
        protected bool _result;//创建房间结果
        protected string _roomCheckId;//返回的房间ID

        public DouniuCreateRoomRet() {

        }

        public void decode(Input _in) {
            _result = _in.readBoolean();
            _roomCheckId = _in.readString();
        }

        public void encode(Output _out) {
            _out.writeBoolean(result);
            _out.writeString(roomCheckId);
        }

        public bool result {
            get {
                return _result;
            }
            set {
                _result = value;
            }
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
            return "CreateRoomRet [result=" + _result + ",roomCheckId=" + _roomCheckId + ", ]";
        }
    }
}