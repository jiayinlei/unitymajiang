using com.guojin.core.io.message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.guojin.core.io;
using System;


namespace com.guojin.dn.net.message {
    public class RecordChapterInfoRet : Message {

        public static int TYPE = 5;
        public static int ID = 56;

        private List<RecordChapterInfoDecode> _list;

        public List<RecordChapterInfoDecode> List {
            get {
                return _list;
            }
            set {
                _list = value;
            }
        }

        public void decode(com.guojin.core.io.Input _in) {
            List = new List<RecordChapterInfoDecode>();
            int len = _in.readInt();
            if (len > 0) {
                for (int i = 0; i < len; i++) {
                    var rcid = new RecordChapterInfoDecode();
                    rcid.decode(_in);
                    _list.Add(rcid);
                }
            }
        }

        public void encode(Output _out) {

        }

        public int getMessageId() {
            return ID;
        }

        public int getMessageType() {
            return TYPE;
        }

        public string toString() {
            return "RecordChapterInfoRet[]";
        }

    }
}
