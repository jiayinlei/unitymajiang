using com.guojin.core.io.message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.guojin.core.io;
using System;

namespace com.guojin.dn.net.message {

    public class RecordChapterInfoDecode : Message {

        public static int TYPE = 5;
        public static int ID = 55;

        private string _chapterInfo;

        public string ChapterInfo {
            get {
                return _chapterInfo;
            }
            set {
                _chapterInfo = value;
            }
        }

        public static RecordChapterInfoDecode Creat(string chapterInfo) {
            RecordChapterInfoDecode rcir = new RecordChapterInfoDecode();
            rcir.ChapterInfo = chapterInfo;
            return rcir;
        }

        public void decode(com.guojin.core.io.Input _in) {
            _chapterInfo = _in.readString();
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
            return "RecordChapterInfoDecode[]";
        }

    }
}
