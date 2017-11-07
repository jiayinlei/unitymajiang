using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
using System.Collections.Generic;

/**
 * 返回其他用户的下注数量
 * 
 * <b>生成器生成代码，请勿修改，扩展请继承</b>
 * @author isnowfox消息生成器
 */
namespace com.guojin.dn.net.message {
    public class DouniuCompareResult : Message {
        public static int TYPE = 5;
        public static int ID = 36;

        private int winCount;//赢的数量
        private bool isZhuang;//是否是庄
        private int locationIndex;//玩家位置索引
        private List<DouniuOnePai> paiList;//第一手牌的信息
        private List<DouniuOnePai> paiList2;//第二手牌的信息
        private int paiType; //第一手牌的类型
        private int paiType2; //第二手牌的类型
        private String winCountNum;  //第一副牌的输赢分
        private String winCountNum2; //第二副牌的输赢分
        private String scores;  // 总分
        private int currentCount; //当前局数  
        private String winNum; //玩家赢的局数
        private String loseNum; // 玩家输的局数
        private String playsNum; // 玩家实际玩的局数
        private String playName; // 玩家名字
        private String equalCount; //平局数量
        private String avator; // 头像地址
        private int maxChapter; //当前局数  

        public int WinCount {
            get {
                return winCount;
            }

            set {
                winCount = value;
            }
        }

        public bool IsZhuang {
            get {
                return isZhuang;
            }

            set {
                isZhuang = value;
            }
        }

        public int LocationIndex {
            get {
                return locationIndex;
            }

            set {
                locationIndex = value;
            }
        }

        public List<DouniuOnePai> PaiList {
            get {
                return paiList;
            }

            set {
                paiList = value;
            }
        }

        public List<DouniuOnePai> PaiList2 {
            get {
                return paiList2;
            }

            set {
                paiList2 = value;
            }
        }

        public int PaiType {
            get {
                return paiType;
            }

            set {
                paiType = value;
            }
        }

        public int PaiType2 {
            get {
                return paiType2;
            }

            set {
                paiType2 = value;
            }
        }

        public string WinCountNum {
            get {
                return winCountNum;
            }

            set {
                winCountNum = value;
            }
        }

        public string WinCountNum2 {
            get {
                return winCountNum2;
            }

            set {
                winCountNum2 = value;
            }
        }

        public string Scores {
            get {
                return scores;
            }

            set {
                scores = value;
            }
        }

        public int CurrentCount {
            get {
                return currentCount;
            }

            set {
                currentCount = value;
            }
        }

        public string WinNum {
            get {
                return winNum;
            }

            set {
                winNum = value;
            }
        }

        public string LoseNum {
            get {
                return loseNum;
            }

            set {
                loseNum = value;
            }
        }

        public string PlaysNum {
            get {
                return playsNum;
            }

            set {
                playsNum = value;
            }
        }

        public string PlayName {
            get {
                return playName;
            }

            set {
                playName = value;
            }
        }

        public string EqualCount {
            get {
                return equalCount;
            }

            set {
                equalCount = value;
            }
        }

        public string Avator {
            get {
                return avator;
            }

            set {
                avator = value;
            }
        }

        public int MaxChapter {
            get {
                return maxChapter;
            }

            set {
                maxChapter = value;
            }
        }

        public DouniuCompareResult() {

        }

        public DouniuCompareResult(int winCount, bool isZhuang, int locationIndex, List<DouniuOnePai> paiList,
                List<DouniuOnePai> paiList2, int paiType, int paiType2, String winCountNum, String winCountNum2,
                String scores, int currentCount,string winNum, string loseNum, string playersNum,string playerName) {
            this.WinCount = winCount;
            this.IsZhuang = isZhuang;
            this.LocationIndex = locationIndex;
            this.PaiList = paiList;
            this.PaiList2 = paiList2;
            this.PaiType = paiType;
            this.PaiType2 = paiType2;
            this.WinCountNum = winCountNum;
            this.WinCountNum2 = winCountNum2;
            this.Scores = scores;
            this.CurrentCount = currentCount;
            this.WinNum = winNum;
            this.LoseNum = loseNum;
            this.PlaysNum = playersNum;
            this.PlayName = playerName;
        }

        


        public void decode(Input _in) {

            WinCount = _in.readInt();
            IsZhuang = _in.readBoolean();
            LocationIndex = _in.readInt();
            PaiType = _in.readInt();
            PaiType2 = _in.readInt();
            int paiCount = _in.readInt();
            if (paiCount == -1) {
                PaiList = null;
            } else {
                PaiList = new List<DouniuOnePai>(paiCount);
                for (int i = 0; i < paiCount; i++) {
                    DouniuOnePai onePaiItem = new DouniuOnePai();
                    onePaiItem.decode(_in);
                    PaiList.Add(onePaiItem);
                }
            }

            //第二副牌
            int paiCount2 = _in.readInt();
            if (paiCount2 == -1) {
                PaiList2 = null;
            } else {
                PaiList2 = new List<DouniuOnePai>(paiCount2);
                for (int i = 0; i < paiCount2; i++) {
                    DouniuOnePai onePaiItem = new DouniuOnePai();
                    onePaiItem.decode(_in);
                    PaiList2.Add(onePaiItem);
                }
            }
            WinCountNum = _in.readString();
            WinCountNum2 = _in.readString();
            Scores = _in.readString();
            CurrentCount = _in.readInt();
            WinNum = _in.readString();
            LoseNum = _in.readString();
            PlaysNum = _in.readString();
            PlayName = _in.readString();
            EqualCount = _in.readString();
            Avator = _in.readString();
            MaxChapter = _in.readInt();
        }

        public void encode(Output _out) {

            _out.writeInt(WinCount);
            _out.writeBoolean(IsZhuang);
            _out.writeInt(LocationIndex);
            _out.writeInt(PaiType);
            _out.writeInt(PaiType2);

            if (PaiList == null) {
                _out.writeInt(-1);
            } else {
                List<DouniuOnePai> onePaiList = PaiList;
                int shouPaiLength = onePaiList.Count;
                _out.writeInt(shouPaiLength);
                foreach (DouniuOnePai onePaiItem in onePaiList) {
                    onePaiItem.encode(_out);
                }
            }

            if (PaiList2 == null) {
                _out.writeInt(-1);
            } else {
                List<DouniuOnePai> onePaiList2 = PaiList2;
                int shouPaiLength2 = onePaiList2.Count;
                _out.writeInt(shouPaiLength2);
                foreach (DouniuOnePai onePaiItem in onePaiList2) {
                    onePaiItem.encode(_out);
                }
            }
            _out.writeString(WinCountNum);
            _out.writeString(WinCountNum2);
            _out.writeString(Scores);
            _out.writeInt(CurrentCount);
            _out.writeString(WinNum);
            _out.writeString(LoseNum);
            _out.writeString(PlaysNum);
            _out.writeString(PlayName);
            _out.writeString(EqualCount);
            _out.writeString(Avator);
            _out.writeInt(MaxChapter);
        }


        public String toString() {
            return "CompareResult [winCount=" + WinCount + ", isZhuang=" + IsZhuang + ", locationIndex=" + LocationIndex
                    + ", paiList=" + PaiList + ", paiList2=" + PaiList2 + ", paiType=" + PaiType + ", paiType2=" + PaiType2
                    + ", winCountNum=" + WinCountNum + ", winCountNum2=" + WinCountNum2 + ", scores=" + Scores
                    + ", currentCount=" + CurrentCount + "]";
        }


        public int getMessageType() {
            return TYPE;
        }


        public int getMessageId() {
            return ID;
        }

    }
}