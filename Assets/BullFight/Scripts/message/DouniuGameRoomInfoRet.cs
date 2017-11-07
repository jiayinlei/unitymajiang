using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
using System.Collections.Generic;

/**
 * 斗牛创建房间同步消息
 * 同步游戏信息
 * 
 * <b>生成器生成代码，请勿修改，扩展请继承</b>
 * @author isnowfox消息生成器
 */
namespace com.guojin.dn.net.message {
    public class DouniuGameRoomInfoRet : Message {
        public static int TYPE = 5;
        public static int ID = 24;
        protected List<DouniuGameUserInfo> sceneUser;// 用户信息列表
        protected bool start;//游戏是否开始
        protected string roomCheckId;//房间号（重连用）
        protected int leftChapterNums;//上一局的局数
        protected int createUserId;//创建房间的用户的ID（房主）
        protected DouniuChapterMsg chapter;//回合信息
        protected int chapterMax;//最大局数
        protected int initScore;//初始分
        protected int userNum;//玩家的数量
        protected string moShi;//玩法模式（随机庄，房主庄等）
        private string fangShi;//换庄方式
        protected int niuNum;//牛牛倍数（没有使用）
        private int localIndex;//玩家的场景位置索引
        /// <summary>
        /// 0:等待选庄
        /// 1:等待选手牌
        /// 2:等待加注
        /// 3:等待第五张牌
        /// 4:等待亮牌
        /// 5:已经亮牌
        /// </summary>
        private string roomState;//房间的状态
        private string isCheck;//是否看牌
        private DouniuGameChapterEnd chapterEnd;//重连后处于已经亮牌的界面时显示的结算信息


        public List<DouniuGameUserInfo> SceneUser {
            get {
                return sceneUser;
            }

            set {
                sceneUser = value;
            }
        }

        public bool Start {
            get {
                return start;
            }

            set {
                start = value;
            }
        }

        public string RoomCheckId {
            get {
                return roomCheckId;
            }

            set {
                roomCheckId = value;
            }
        }

        public int LeftChapterNums {
            get {
                return leftChapterNums;
            }

            set {
                leftChapterNums = value;
            }
        }

        public int CreateUserId {
            get {
                return createUserId;
            }

            set {
                createUserId = value;
            }
        }

        public DouniuChapterMsg Chapter {
            get {
                return chapter;
            }

            set {
                chapter = value;
            }
        }

        public int ChapterMax {
            get {
                return chapterMax;
            }

            set {
                chapterMax = value;
            }
        }

        public int InitScore {
            get {
                return initScore;
            }

            set {
                initScore = value;
            }
        }

        public int UserNum {
            get {
                return userNum;
            }

            set {
                userNum = value;
            }
        }

        public string MoShi {
            get {
                return moShi;
            }

            set {
                moShi = value;
            }
        }

        public int NiuNum {
            get {
                return niuNum;
            }

            set {
                niuNum = value;
            }
        }

        public string FangShi {
            get {
                return fangShi;
            }

            set {
                fangShi = value;
            }
        }

        public int LocalIndex {
            get {
                return localIndex;
            }

            set {
                localIndex = value;
            }
        }

        public string RoomState {
            get {
                return roomState;
            }

            set {
                roomState = value;
            }
        }

        public string IsCheck {
            get {
                return isCheck;
            }

            set {
                isCheck = value;
            }
        }

        public DouniuGameChapterEnd ChapterEnd {
            get {
                return chapterEnd;
            }

            set {
                chapterEnd = value;
            }
        }


        //public DouniuGameRoomInfoRet() {

        //}

        public static DouniuGameRoomInfoRet create(
            List<DouniuGameUserInfo> sceneUser, 
            bool start, 
            string roomCheckId,
            int leftChapterNums, 
            int createUserId, 
            DouniuChapterMsg chapter, 
            int chapterMax, 
            string isType,
            int initScore,  int userNum,string moShi, string fangShi, int niuNum,int localIndex,string state) {
            DouniuGameRoomInfoRet infoRet = new DouniuGameRoomInfoRet();
            infoRet.SceneUser = sceneUser;
            infoRet.Start = start;
            infoRet.RoomCheckId = roomCheckId;
            infoRet.LeftChapterNums = leftChapterNums;
            infoRet.CreateUserId = createUserId;
            infoRet.Chapter = chapter;
            infoRet.ChapterMax = chapterMax;
            infoRet.InitScore = initScore;
            infoRet.UserNum = userNum;
            infoRet.MoShi = moShi;
            infoRet.FangShi = fangShi;
            infoRet.NiuNum = niuNum;
            infoRet.LocalIndex = localIndex;
            infoRet.RoomState = state;
            return infoRet;
        }


        public void decode(Input _in) {

            int sceneUserLen = _in.readInt();
            if (sceneUserLen == -1) {
                SceneUser = null;
            } else {
                SceneUser = new List<DouniuGameUserInfo>(sceneUserLen);
                for (int i = 0; i < sceneUserLen; i++) {
                    DouniuGameUserInfo sceneUserItem = new DouniuGameUserInfo();
                    sceneUserItem.decode(_in);
                    SceneUser.Add(sceneUserItem);
                }
            }
            Start = _in.readBoolean();
            //UnityEngine.Debug.Log("start:"+Start);
            RoomCheckId = _in.readString();
            //UnityEngine. Debug.Log("roomCheckId:" + RoomCheckId);
            LeftChapterNums = _in.readInt();
            //UnityEngine.Debug.Log("leftChapterNums:" + LeftChapterNums);
            CreateUserId = _in.readInt();
            //UnityEngine. Debug.Log("createUserId:" + CreateUserId);

            bool chapterIsNotNull = _in.readBoolean();
            if (chapterIsNotNull) {
                Chapter = new DouniuChapterMsg();
                Chapter.decode(_in);
            } else {
                Chapter = null;
            }
            ChapterMax = _in.readInt();
            InitScore = _in.readInt();
            UserNum = _in.readInt();
            MoShi = _in.readString();
            FangShi = _in.readString();
            NiuNum = _in.readInt();
            LocalIndex = _in.readInt();
            RoomState = _in.readString();
            IsCheck = _in.readString();
            bool chapterEnd = _in.readBoolean();
            if (chapterEnd) {
                ChapterEnd = new DouniuGameChapterEnd();
                ChapterEnd.decode(_in);
            } else {
                ChapterEnd = null;
            }
        }


        public void encode(Output _out) {

            if (SceneUser == null) {
                _out.writeInt(-1);
            } else {
                List<DouniuGameUserInfo> sceneUserList = SceneUser;
                int sceneUserLen = sceneUserList.Count;
                _out.writeInt(sceneUserLen);
                foreach (DouniuGameUserInfo sceneUserItem in sceneUserList) {
                    sceneUserItem.encode(_out);
                }
            }
            _out.writeBoolean(Start);
            _out.writeString(RoomCheckId);
            _out.writeInt(LeftChapterNums);
            _out.writeInt(CreateUserId);
            DouniuChapterMsg chapterItem = Chapter;
            if (chapterItem == null) {
                _out.writeBoolean(false);
            } else {
                _out.writeBoolean(true);
                chapterItem.encode(_out);
            }
            _out.writeInt(ChapterMax);
            _out.writeInt(InitScore);
            _out.writeInt(UserNum);
            _out.writeString(MoShi);
            _out.writeString(FangShi);
            _out.writeInt(NiuNum);
            _out.writeInt(LocalIndex);
            _out.writeString(RoomState);
            _out.writeString(IsCheck);
            DouniuGameChapterEnd chapterEnd = ChapterEnd;
            if (chapterEnd == null) {
                _out.writeBoolean(false);
            } else {
                _out.writeBoolean(true);
                chapterEnd.encode(_out);
            }
        }
        


        public void addSceneUser(DouniuGameUserInfo sceneUser) {
            if (this.SceneUser == null) {
                this.SceneUser = new List<DouniuGameUserInfo>();
            }
            this.SceneUser.Add(sceneUser);
        }


        public string toString() {
            return "DouniuGameRoomInfo [sceneUser=" + SceneUser + ",start=" + Start + ",roomCheckId=" + RoomCheckId + ",leftChapterNums=" + LeftChapterNums + ",createUserId=" + CreateUserId + ",chapter=" + Chapter + ",chapterMax=" + ChapterMax + ",initScore=" + InitScore + ",userNum=" + 
                UserNum + ",moShi=" + MoShi + ",fangShi=" + FangShi + ",niuNum=" + NiuNum + ",localIndex=" + LocalIndex + ",roomState=" + RoomState + ", ]";
        }


        public int getMessageType() {
            return TYPE;
        }


        public int getMessageId() {
            return ID;
        }
    }
}
