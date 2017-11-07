using System;
using com.guojin.core.io;
using com.guojin.core.io.message;

namespace com.guojin.dn.net.message {
    public class DouniuRoomHistory : Message {
        public static int TYPE = 5;
        public static int ID = 47;

        private String roomCheckId;//房间号
        private String startDate;//开始日期
        private int chapterNums;//玩了多少局
        private String[] userNames;//所有用户的名字
        private int[] scores;//所有用户的分数

        public DouniuRoomHistory() {

        }

        public DouniuRoomHistory(String roomCheckId, String startDate, int chapterNums, String[] userNames, int[] scores) {
            this.roomCheckId = roomCheckId;
            this.startDate = startDate;
            this.chapterNums = chapterNums;
            this.userNames = userNames;
            this.scores = scores;
        }


        public void decode(Input _in) {
            roomCheckId = _in.readString();
            startDate = _in.readString();
            chapterNums = _in.readInt();
            userNames = _in.readStringArray();
            scores = _in.readIntArray();
        }


        public void encode(Output _out) {
            _out.writeString(getRoomCheckId());
            _out.writeString(getStartDate());
            _out.writeInt(getChapterNums());
            _out.writeStringArray(getUserNames());
            _out.writeIntArray(getScores());
        }

        public String getRoomCheckId() {
            return roomCheckId;
        }

        public void setRoomCheckId(String roomCheckId) {
            this.roomCheckId = roomCheckId;
        }

        public String getStartDate() {
            return startDate;
        }

        public void setStartDate(String startDate) {
            this.startDate = startDate;
        }

        public int getChapterNums() {
            return chapterNums;
        }

        public void setChapterNums(int chapterNums) {
            this.chapterNums = chapterNums;
        }

        public String[] getUserNames() {
            return userNames;
        }

        public void setUserNames(String[] userNames) {
            this.userNames = userNames;
        }

        public int[] getScores() {
            return scores;
        }

        public void setScores(int[] scores) {
            this.scores = scores;
        }


        public String toString() {
            return "DouniuRoomHistory [roomCheckId=" + roomCheckId + ", startDate="
                    + startDate + ", chapterNums=" + chapterNums + ", userNames="
                    + userNames.ToString() + ", scores="
                    + scores.ToString() + "]";
        }


        public int getMessageType() {
            return TYPE;
        }


        public int getMessageId() {
            return ID;
        }
    }
}