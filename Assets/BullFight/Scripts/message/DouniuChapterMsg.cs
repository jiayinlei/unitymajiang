using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
using System.Collections.Generic;

/**
 * 一局斗牛的信息
 * 
 * <b>生成器生成代码，请勿修改，扩展请继承</b>
 * @author isnowfox消息生成器
 */
namespace com.guojin.dn.net.message {
    public class DouniuChapterMsg : Message {
        public static int TYPE = 5;
        public static int ID = 20;

        private List<DouniuUserPlaceMsg> userPlace;//用户的牌内容
        private int currentIndex;//当前操作用户
        private int chapterNums;//当前回合的回合数
        private int chapterNumsMax;//最大局数
        private int quanIndex;//圈index 0 A 1B 2C 3D 4E 5F 6G 逆时针顺序
        private int zhuangIndex;//庄的位置索引

        public DouniuChapterMsg() {

        }

        public DouniuChapterMsg(List<DouniuUserPlaceMsg> userPlace, int currentIndex, int chapterNums, int chapterNumsMax, int quanIndex, int zhuangIndex) {
            //this.userPlace = userPlace;
            //this.currentIndex = currentIndex;
            //this.chapterNums = chapterNums;
            //this.chapterNumsMax = chapterNumsMax;
            //this.quanIndex = quanIndex;
            //this.zhuangIndex = zhuangIndex;
            //this.douniuFaPai = douniuFaPai;
            //this.douniuOut = douniuOut;
            //this.syncDouniuTime = syncDouniuTime;
            //this.douniugameChapterEnd = douniugameChapterEnd;
        }
        public static DouniuChapterMsg create(List<DouniuUserPlaceMsg> userPlace, int currentIndex, int chapterNums, int chapterNumsMax, int quanIndex, int zhuangIndex) {
            DouniuChapterMsg chapterMsg = new DouniuChapterMsg();
            chapterMsg.userPlace = userPlace;
            chapterMsg.currentIndex = currentIndex;
            chapterMsg.chapterNums = chapterNums;
            chapterMsg.chapterNumsMax = chapterNumsMax;
            chapterMsg.quanIndex = quanIndex;
            chapterMsg.zhuangIndex = zhuangIndex;
            //chapterMsg.douniuFaPai = douniuFaPai;
            //chapterMsg.douniuOut = douniuOut;
            //chapterMsg.syncDouniuTime = syncDouniuTime;
            //chapterMsg.douniugameChapterEnd = douniugameChapterEnd;
            return chapterMsg;
        }

        public void decode(Input _in) {

            int userPlaceLen = _in.readInt();
            if (userPlaceLen == -1) {
                userPlace = null;
            } else {
                userPlace = new List<DouniuUserPlaceMsg>();
                for (int i = 0; i < userPlaceLen; i++) {
                    DouniuUserPlaceMsg userPlaceItem = new DouniuUserPlaceMsg();
                    userPlaceItem.decode(_in);
                    userPlace.Add(userPlaceItem);
                }
            }
            currentIndex = _in.readInt();
            chapterNums = _in.readInt();
            chapterNumsMax = _in.readInt();
            quanIndex = _in.readInt();
            zhuangIndex = _in.readInt();

            //bool douniuFaPaiIsNotNull = _in.readBoolean();
            //if (douniuFaPaiIsNotNull) {
            //    douniuFaPai = new DouniuFaPai();
            //    douniuFaPai.decode(_in);
            //} else {
            //    douniuFaPai = null;
            //}

            //bool douniuOutIsNotNull = _in.readBoolean();
            //if (douniuOutIsNotNull) {
            //    douniuOut = new DouniuOut();
            //    douniuOut.decode(_in);
            //} else {
            //    douniuOut = null;
            //}

            //bool syncDouniuTimeIsNotNull = _in.readBoolean();
            //if (syncDouniuTimeIsNotNull) {
            //    syncDouniuTime = new SyncDouniuTime();
            //    syncDouniuTime.decode(_in);
            //} else {
            //    syncDouniuTime = null;
            //}

            //bool douniugameChapterEndIsNotNull = _in.readBoolean();
            //if (douniugameChapterEndIsNotNull) {
            //    douniugameChapterEnd = new DouniuGameChapterEnd();
            //    douniugameChapterEnd.decode(_in);
            //} else {
            //    douniugameChapterEnd = null;
            //}
        }

        public void encode(Output _out) {

            if (userPlace == null) {
                _out.writeInt(-1);
            } else {
                List<DouniuUserPlaceMsg> userPlaceList = getUserPlace();
                int userPlaceLen = userPlaceList.Count;
                _out.writeInt(userPlaceLen);
                foreach (DouniuUserPlaceMsg userPlaceItem in userPlaceList) {
                    userPlaceItem.encode(_out);
                }
            }
            _out.writeInt(getCurrentIndex());
            _out.writeInt(getChapterNums());
            _out.writeInt(getChapterNumsMax());
            _out.writeInt(getQuanIndex());
            _out.writeInt(getZhuangIndex());
            //DouniuFaPai douniuFaPaiItem = getDouniuFaPai();
            //if (douniuFaPaiItem == null) {
            //    _out.writeBoolean(false);
            //} else {
            //    _out.writeBoolean(true);
            //    douniuFaPaiItem.encode(_out);
            //}
            //DouniuOut douniuOutItem = getDouniuOut();
            //if (douniuOutItem == null) {
            //    _out.writeBoolean(false);
            //} else {
            //    _out.writeBoolean(true);
            //    douniuOutItem.encode(_out);
            //}
            //SyncDouniuTime syncDouniuTimeItem = getSyncDouniuTime();
            //if (syncDouniuTimeItem == null) {
            //    _out.writeBoolean(false);
            //} else {
            //    _out.writeBoolean(true);
            //    syncDouniuTimeItem.encode(_out);
            //}
            //DouniuGameChapterEnd douniugameChapterEndItem = getDouniugameChapterEnd();
            //if (douniugameChapterEndItem == null) {
            //    _out.writeBoolean(false);
            //} else {
            //    _out.writeBoolean(true);
            //    douniugameChapterEndItem.encode(_out);
            //}
        }

        public List<DouniuUserPlaceMsg> getUserPlace() {
            return userPlace;
        }

        public void setUserPlace(List<DouniuUserPlaceMsg> userPlace) {
            this.userPlace = userPlace;
        }

        /**
         * 当前操作用户
         */
        public int getCurrentIndex() {
            return currentIndex;
        }

        /**
         * 当前操作用户
         */
        public void setCurrentIndex(int currentIndex) {
            this.currentIndex = currentIndex;
        }

        /**
         * 局数, 0开始
         */
        public int getChapterNums() {
            return chapterNums;
        }

        /**
         * 局数, 0开始
         */
        public void setChapterNums(int chapterNums) {
            this.chapterNums = chapterNums;
        }

        /**
         * 局数, 0开始
         */
        public int getChapterNumsMax() {
            return chapterNumsMax;
        }

        /**
         * 局数, 0开始
         */
        public void setChapterNumsMax(int chapterNumsMax) {
            this.chapterNumsMax = chapterNumsMax;
        }

        /**
         * 圈index 0 A 1B 2C 3D 4E 5F 6G 逆时针顺序
         */
        public int getQuanIndex() {
            return quanIndex;
        }

        /**
         * 圈index 0 A 1B 2C 3D 4E 5F 6G 逆时针顺序
         */
        public void setQuanIndex(int quanIndex) {
            this.quanIndex = quanIndex;
        }

        /**
         * 庄index 0 A 1B 2C 3D 4E 5F 6G 逆时针顺序
         */
        public int getZhuangIndex() {
            return zhuangIndex;
        }

        /**
         * 庄index 0 A 1B 2C 3D 4E 5F 6G 逆时针顺序
         */
        public void setZhuangIndex(int zhuangIndex) {
            this.zhuangIndex = zhuangIndex;
        }

        //public DouniuFaPai getDouniuFaPai() {
        //    return douniuFaPai;
        //}

        //public void setDouniuFaPai(DouniuFaPai douniuFaPai) {
        //    this.douniuFaPai = douniuFaPai;
        //}

        //public DouniuOut getDouniuOut() {
        //    return douniuOut;
        //}

        //public void setDouniuOut(DouniuOut douniuOut) {
        //    this.douniuOut = douniuOut;
        //}

        //public SyncDouniuTime getSyncDouniuTime() {
        //    return syncDouniuTime;
        //}

        //public void setSyncDouniuTime(SyncDouniuTime syncDouniuTime) {
        //    this.syncDouniuTime = syncDouniuTime;
        //}

        //public DouniuGameChapterEnd getDouniugameChapterEnd() {
        //    return douniugameChapterEnd;
        //}

        //public void setDouniugameChapterEnd(DouniuGameChapterEnd douniugameChapterEnd) {
        //    this.douniugameChapterEnd = douniugameChapterEnd;
        //}


        public void addUserPlace(DouniuUserPlaceMsg userPlace) {
            if (this.userPlace == null) {
                this.userPlace = new List<DouniuUserPlaceMsg>();
            }
            this.userPlace.Add(userPlace);
        }

        public string toString() {
            return "DouniuChapterMsg [userPlace=" + userPlace + ",currentIndex=" + currentIndex + ",chapterNums=" + chapterNums + ",chapterNumsMax=" + chapterNumsMax + ",quanIndex=" + quanIndex + ",zhuangIndex=" + zhuangIndex     + ", ]";
        }

        public int getMessageType() {
            return TYPE;
        }

        public int getMessageId() {
            return ID;
        }
    }
}