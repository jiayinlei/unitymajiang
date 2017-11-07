using com.guojin.core.io;
using com.guojin.core.io.message;
using System;
/**
 * 一张牌的信息
 * @author Administrator
 *
 */
namespace com.guojin.dn.net.message {
    public class DouniuOnePai : Message {

        public static int TYPE = 5;
        public static int ID = 33;


        
        private int pokerNum;//真实的值
        private int pokerSuit;//牌的花色
        private int pokerValue;//对应的斗牛中的值


        public DouniuOnePai() {
        }

        public DouniuOnePai(int pokerNum, int pokerSuit, int pokerValue) {
            this.pokerNum = pokerNum;
            this.pokerSuit = pokerSuit;
            this.pokerValue = pokerValue;
        }

        public int getPokerNum() {
            return pokerNum;
        }

        public void setPokerNum(int pokerNum) {
            this.pokerNum = pokerNum;
        }

        public int getPokerSuit() {
            return pokerSuit;
        }

        public void setPokerSuit(int pokerSuit) {
            this.pokerSuit = pokerSuit;
        }

        public int getPokerValue() {
            return pokerValue;
        }

        public void setPokerValue(int pokerValue) {
            this.pokerValue = pokerValue;
        }

        public void decode(Input _in) {

            pokerNum = _in.readInt();
            pokerSuit = _in.readInt();
            pokerValue = _in.readInt();
        }

        public void encode(Output _out) {
            _out.writeInt(getPokerNum());
            _out.writeInt(getPokerSuit());
            _out.writeInt(getPokerValue());
        }

        public int getMessageId() {
            return TYPE;
        }
        public int getMessageType() {
            return ID;
        }

        public string toString() {
            return "DouniuOnePai"+"[Number"+getPokerNum()+"Suit"+getPokerSuit()+"Value"+getPokerValue()+"]";
        }
    }
}
