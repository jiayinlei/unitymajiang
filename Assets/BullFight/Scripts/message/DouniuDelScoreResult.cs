using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
/**
 * 斗牛扣分的消息
 * @author Administrator
 *
 */
namespace com.guojin.dn.net.message {
    public class DouniuDelScoreResult : Message {
        public static int TYPE = 5;
        public static int ID = 21;

        private int index;
        private int score;

        public DouniuDelScoreResult() {
            // TODO Auto-generated constructor stub
        }

        public DouniuDelScoreResult(int index, int score) {
            //super();
            this.index = index;
            this.score = score;
        }

        public int getIndex() {
            return index;
        }

        public void setIndex(int index) {
            this.index = index;
        }

        public int getScore() {
            return score;
        }

        public void setScore(int score) {
            this.score = score;
        }


        public void decode(Input _in) {
            this.index = _in.readInt();
            this.score = _in.readInt();

        }

        public void encode(Output _out) {
            _out.writeInt(getIndex());
            _out.writeInt(getScore());
        }

        public int getMessageId() {
            // TODO Auto-generated method stub
            return ID;
        }

        public int getMessageType() {
            // TODO Auto-generated method stub
            return TYPE;
        }

        public string toString() {
            return "DouniuDelScoreResult";
        }
    }
}

