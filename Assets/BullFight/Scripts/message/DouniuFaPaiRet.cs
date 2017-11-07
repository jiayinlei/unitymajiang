using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
using System.Collections.Generic;
/**
* 玩家自己的发牌消息
* 
* <b>生成器生成代码，请勿修改，扩展请继承</b>
* @author isnowfox消息生成器
*/
namespace com.guojin.dn.net.message {
    public class DouniuFaPaiRet : Message {
        public static int TYPE = 5;
        public static int ID = 11; //玩家自己的发牌信息
        private int index;//最后一张牌的索引
        private List<DouniuOnePai> onePai; //最后一张牌的第一手牌的信息
        private List<DouniuOnePai> onePai2;//最后一张牌的第二手牌的信息

        public int Index {
            get {
                return index;
            }

            set {
                index = value;
            }
        }

        public List<DouniuOnePai> OnePai {
            get {
                return onePai;
            }

            set {
                onePai = value;
            }
        }

        public List<DouniuOnePai> OnePai2 {
            get {
                return onePai2;
            }

            set {
                onePai2 = value;
            }
        }

        public DouniuFaPaiRet() {

        }

        public DouniuFaPaiRet(int index, List<DouniuOnePai> onePai, List<DouniuOnePai> onePai2) {
            this.Index = index;
            this.OnePai = onePai;
            this.OnePai2 = onePai2;
        }


        public void decode(Input _in) {

            Index = _in.readInt();
            int onePaiLength = _in.readInt();
            if (onePaiLength == -1) {
                OnePai = null;
            } else {
                OnePai = new List<DouniuOnePai>(onePaiLength);
                for (int i = 0; i < onePaiLength; i++) {
                    DouniuOnePai onePaiItem = new DouniuOnePai();
                    onePaiItem.decode(_in);
                    OnePai.Add(onePaiItem);
                }
            }

            int onePaiLength2 = _in.readInt();
            if (onePaiLength2 == -1) {
                OnePai2 = null;
            } else {
                OnePai2 = new List<DouniuOnePai>(onePaiLength2);
                for (int i = 0; i < onePaiLength2; i++) {
                    DouniuOnePai onePaiItem = new DouniuOnePai();
                    onePaiItem.decode(_in);
                    OnePai2.Add(onePaiItem);
                }
            }
        }


        public void encode(Output _out) {
            _out.writeInt(Index);
            if (OnePai == null) {
                _out.writeInt(-1);
            } else {
                List<DouniuOnePai> onePaiList = OnePai;
                int shouPaiLength = onePaiList.Count;
                _out.writeInt(shouPaiLength);
                foreach (DouniuOnePai onePaiItem in onePaiList) {
                    onePaiItem.encode(_out);
                }
            }

            if (OnePai2 == null) {
                _out.writeInt(-1);
            } else {
                List<DouniuOnePai> onePaiList2 = OnePai2;
                int shouPaiLength2 = onePaiList2.Count;
                _out.writeInt(shouPaiLength2);
                foreach (DouniuOnePai onePaiItem in onePaiList2) {
                    onePaiItem.encode(_out);
                }
            }
        }

        public String toString() {
            return "DouniuFaPaiRet [index=" + Index + ", onePai=" + OnePai + ", onePai2=" + OnePai2 + "]";
        }


        public int getMessageType() {
            return TYPE;
        }


        public int getMessageId() {
            return ID;
        }
    }
}