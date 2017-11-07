using System;
using com.guojin.core.io;
using com.guojin.core.io.message;


/* ***********************************************
 * Describe:消息体:服务器消息响应   加入俱乐部   
 * Author :  MuLongFei
 * Email: 424113771@qq.com
 * DATA: 2017/7/14 11:57:01 
 * FileName: JoinClubRet 
 * Version: V1.0.1
 * ***********************************************/

namespace com.guojin.mj.net.message.club {
    class JoinClubRet : Message {
        public static int TYPE = 7;
        public static int ID = 34;

        private bool result;
        private int reasonType;

        public bool Result {
            get {
                return result;
            }

            set {
                result = value;
            }
        }

        public int ReasonType {
            get {
                return reasonType;
            }

            set {
                reasonType = value;
            }
        }

        public void decode(Input _in) {
            result = _in.readBoolean();
            if (!result) {
                reasonType = _in.readInt();
            }
        }

        public void encode(Output _out) {
            _out.writeBoolean(result);
            if (!result) {
                _out.writeInt(reasonType);
            }
        }

        public int getMessageId() {
            return ID;
        }

        public int getMessageType() {
            return TYPE;
        }

        public string toString() {
            return string.Format("JoinClubRet [result={0},reasonType={1}",Result,ReasonType);
        }
    }
}
