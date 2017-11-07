using System;
using com.guojin.core.io;
using com.guojin.core.io.message;


/* ***********************************************
 * Describe:消息体:客户端消息请求   加入俱乐部
 * Author :  MuLongFei
 * Email: 424113771@qq.com
 * DATA: 2017/7/14 11:47:04 
 * FileName: JoinClub 
 * Version: V1.0.1
 * ***********************************************/

namespace com.guojin.mj.net.message.club {
    class JoinClub : Message{
        public static int TYPE = 7;
        public static int ID = 33;

        private String clubId;

        public string ClubId {
            get {
                return clubId;
            }

            set {
                clubId = value;
            }
        }

        public void decode(Input _in) {
            clubId = _in.readString();
        }

        public void encode(Output _out) {
            _out.writeString(ClubId);
        }

        public int getMessageType() {
            return TYPE;
        }

        public int getMessageId() {
            return ID;
        }

        public string toString() {
            return string.Format("JoinClub [clubId={0}]",ClubId);
        }
    }
}
