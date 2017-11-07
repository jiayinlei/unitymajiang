using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
using com.guojin.mj.net.message.login;
using System.Collections.Generic;

/* ***********************************************
 * Describe:该类功能描述
 * Author :  MuLongFei
 * Email: 424113771@qq.com
 * DATA: 2017/7/28 15:50:49 
 * FileName: DNCreateRoom 
 * Version: V1.0.1
 * ***********************************************/
namespace com.guojin.dn.net.message {
    /// <summary>
    /// 创建房间
    /// </summary>
    public class DouniuCreateRoom : Message {

        public static int TYPE = 5;
        public static int ID = 0;

        public static DouniuCreateRoom create(string profile, List<OptionEntry> options) {
            DouniuCreateRoom createRoom = new DouniuCreateRoom();
            createRoom._profile = profile;
            createRoom._options = options;
            return createRoom;
        }

        protected string _profile;//创建的房间类型
        protected List<OptionEntry> _options;//包含的操作列表

        public DouniuCreateRoom() {

        }
        public void decode(Input _in) {
            _profile = _in.readString();
            var optionsLen = _in.readInt();

            if (optionsLen == -1) {
                _options = null;
            } else {

                for (int optionsI = 0; optionsI < optionsLen; optionsI++) {
                    OptionEntry optionsItem = new OptionEntry();

                    optionsItem.decode(_in);
                    _options[optionsI] = optionsItem;

                }
            }
        }

        public void encode(Output _out) {
            _out.writeString(profile);

            if (options == null) {
                _out.writeInt(-1);
            } else {
                int optionsLen = options.Count;
                _out.writeInt(optionsLen);
                for (int optionsI = 0; optionsI < optionsLen; optionsI++) {
                    options[optionsI].encode(_out);
                }
            }
        }
        public string profile {
            get {
                return _profile;
            }
            set {
                _profile = value;
            }
        }
        public List<OptionEntry> options {
            get {
                return _options;
            }
            set {
                _options = value;
            }
        }
        public int getMessageId() {
            return ID;
        }

        public int getMessageType() {
            return TYPE;
        }

        public string toString() {
            return "DNCreateRoom [profile=" + _profile + ",options=" + _options + ", ]";
        }
    }
}


