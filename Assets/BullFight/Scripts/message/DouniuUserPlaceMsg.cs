
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
using System.Collections.Generic;

/**
 * 一局斗牛的消息
 * 
 * <b>生成器生成代码，请勿修改，扩展请继承</b>
 * @author isnowfox消息生成器
 */
namespace com.guojin.dn.net.message {
    public class DouniuUserPlaceMsg : Message {

        public static int TYPE = 5;
        public static int ID = 27;
        private List<DouniuOnePai> shouPai;//第一手牌牌内容
        private List<DouniuOnePai> shouPai2; //第二手牌牌内容
        private int shouPaiLen;//手牌的牌长度（不确定没使用）       
        private int locationIndex;//用户位置索引

        public List<DouniuOnePai> ShouPai {
            get {
                return shouPai;
            }

            set {
                shouPai = value;
            }
        }

        public List<DouniuOnePai> ShouPai2 {
            get {
                return shouPai2;
            }

            set {
                shouPai2 = value;
            }
        }

        public int ShouPaiLen {
            get {
                return shouPaiLen;
            }

            set {
                shouPaiLen = value;
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

        public DouniuUserPlaceMsg() {

        }

        public DouniuUserPlaceMsg(List<DouniuOnePai> shouPai, List<DouniuOnePai> shouPai2, int shouPaiLen) {
            this.ShouPai = shouPai;
            this.ShouPai2 = shouPai2;
            this.ShouPaiLen = shouPaiLen;
        }

        public void decode(Input _in) {

            int shouPaiLength = _in.readInt();
            if (shouPaiLength == -1) {
                ShouPai = null;
            } else {
                ShouPai = new List<DouniuOnePai>(shouPaiLength);
                for (int i = 0; i < shouPaiLength; i++) {
                    DouniuOnePai onePaiItem = new DouniuOnePai();
                    onePaiItem.decode(_in);
                    ShouPai.Add(onePaiItem);
                }
            }

            int shouPaiLength2 = _in.readInt();
            if (shouPaiLength2 == -1) {
                ShouPai2 = null;
            } else {
                ShouPai2 = new List<DouniuOnePai>(shouPaiLength2);
                for (int i = 0; i < shouPaiLength2; i++) {
                    DouniuOnePai onePaiItem = new DouniuOnePai();
                    onePaiItem.decode(_in);
                    ShouPai2.Add(onePaiItem);
                }
            }
            ShouPaiLen = _in.readInt();
            LocationIndex = _in.readInt();
        }


        public void encode(Output _out) {
            if (ShouPai == null) {
                _out.writeInt(-1);
            } else {
                List<DouniuOnePai> onePaiList = ShouPai;
                int shouPaiLength = onePaiList.Count;
                _out.writeInt(shouPaiLength);
                foreach (DouniuOnePai onePaiItem in onePaiList) {
                    onePaiItem.encode(_out);
                }
            }
            //第二副牌
            if (ShouPai2 == null) {
                _out.writeInt(-1);
            } else {
                List<DouniuOnePai> onePaiList2 = ShouPai2;
                int shouPaiLength2 = onePaiList2.Count;
                _out.writeInt(shouPaiLength2);
                foreach (DouniuOnePai onePaiItem in onePaiList2) {
                    onePaiItem.encode(_out);
                }
            }
            _out.writeInt(ShouPaiLen);
            _out.writeInt(LocationIndex);
        }




        public String toString() {
            return "DouniuUserPlaceMsg [shouPai=" + ShouPai + ", shouPai2=" + ShouPai2 + ", shouPaiLen=" + ShouPaiLen + ", locationIndex=" + LocationIndex + "]";
        }


        public int getMessageType() {
            return TYPE;
        }


        public int getMessageId() {
            return ID;
        }
    }
}
