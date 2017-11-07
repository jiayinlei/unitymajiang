using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
using System.Collections.Generic;

namespace com.guojin.dn.net.message {
    public class DouniuRoomHistoryListRet : Message {
        public static int TYPE = 5;
        public static int ID = 49;

        private List<DouniuRoomHistory> list;//所有的战绩的内容列表

        public DouniuRoomHistoryListRet() {

        }

        public DouniuRoomHistoryListRet(List<DouniuRoomHistory> list) {
            this.list = list;
        }


        public void decode(Input _in) {

            int listLen = _in.readInt();
            if (listLen == -1) {
                list = null;
            } else {
                list = new List<DouniuRoomHistory>(listLen);
                for (int i = 0; i < listLen; i++) {
                    DouniuRoomHistory listItem = new DouniuRoomHistory();
                    listItem.decode(_in);
                    list.Add(listItem);
                }
            }
        }


        public void encode(Output _out) {

            if (list == null) {
                _out.writeInt(-1);
            } else {
                List<DouniuRoomHistory> listList = getList();
                int listLen = listList.Count;
                _out.writeInt(listLen);
                foreach (DouniuRoomHistory listItem in listList) {
                    listItem.encode(_out);
                }
            }
        }

        public List<DouniuRoomHistory> getList() {
            return list;
        }

        public void setList(List<DouniuRoomHistory> list) {
            this.list = list;
        }

        public void addList(DouniuRoomHistory list) {
            if (this.list == null) {
                this.list = new List<DouniuRoomHistory>();
            }
            this.list.Add(list);
        }




        public String toString() {
            return "DouniuRoomHistoryListRet [list=" + list + "]";
        }


        public int getMessageType() {
            return TYPE;
        }


        public int getMessageId() {
            return ID;
        }
    }
}
