using System;
using com.guojin.core.io;
using com.guojin.core.io.message;

/* ***********************************************
 * Describe:消息体:俱乐部信息
 * Author :  MuLongFei
 * Email: 424113771@qq.com
 * DATA: 2017/7/14 16:03:11 
 * FileName: ClubInfo 
 * Version: V1.0.1
 * ***********************************************/

namespace com.guojin.mj.net.message.club {
    class ClubInfo : Message{
        public static int TYPE = 7;
        public static int ID = 36;

        
        private String clubId;    //俱乐部Id
        private String createUserName;//创建者真实姓名
        private String wxNo;    // 微信号
        private String notice;    //公告
        
        public string ClubId {
            get {
                return clubId;
            }

            set {
                clubId = value;
            }
        }

        public string CreateUserName {
            get {
                return createUserName;
            }

            set {
                createUserName = value;
            }
        }

        public string WxNo {
            get {
                return wxNo;
            }

            set {
                wxNo = value;
            }
        }

        public string Notice {
            get {
                return notice;
            }

            set {
                notice = value;
            }
        }

        public void decode(Input _in) {
            clubId = _in.readString();
            createUserName = _in.readString();
            wxNo = _in.readString();
            notice = _in.readString();
        }

        public void encode(Output _out) {
            _out.writeString(clubId);
            _out.writeString(createUserName);
            _out.writeString(wxNo);
            _out.writeString(notice);
        }

        public int getMessageType() {
            return TYPE;
        }

        public int getMessageId() {
            return ID;
        }

        public string toString() {
            return string.Format("ClubInfo [clubId={0},createUserName={1},wxNo={2},notice={3}", ClubId, CreateUserName,WxNo,Notice);
        }
    }
}
