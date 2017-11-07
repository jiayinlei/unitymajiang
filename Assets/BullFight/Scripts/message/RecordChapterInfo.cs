using com.guojin.core.io.message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.guojin.core.io;
using System;

namespace com.guojin.dn.net.message {
    
    public class RecordChapterInfo : Message {

        public static int TYPE = 5;
        public static int ID = 54;

        private string _roomNo;
        private string _chapterTime;

        public static RecordChapterInfo Creat(string room, string chaptertime) {
            RecordChapterInfo rci = new RecordChapterInfo();
            rci._roomNo = room;
            rci._chapterTime = chaptertime;
            return rci;
        }

        public string RoomNo {
            get {
                return _roomNo;
            }

            set {
                _roomNo = value;
            }
        }

        public string ChapterTime {
            get {
                return _chapterTime;
            }

            set {
                _chapterTime = value;
            }
        }

        public void decode(com.guojin.core.io.Input _in) {

        }

        public void encode(Output _out) {
            _out.writeString(_roomNo);
            _out.writeString(_chapterTime);

        }

        public int getMessageId() {
            return ID;
        }

        public int getMessageType() {
            return TYPE;
        }

        public string toString() {
            return "RecordChapterInfo[roomNo=" + _roomNo + "chapter=" + _chapterTime + "]";
        }

    }
}
