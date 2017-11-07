
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;

namespace com.guojin.dn.net.message {
    /**
     * 
    * @Description: 斗牛选几首牌
    * @author qixianghui
    * @date 2017年7月10日
    *
     */
    public class DouniuCasiPaiRet : Message {
        public static int TYPE = 5;
        public static int ID = 53;

        private int index;//操作的玩家位置索引
        private int paiLens;//牌的数量
        

        public DouniuCasiPaiRet() {

        }

        public DouniuCasiPaiRet(int index, int paiLens) {
            this.index = index;
            this.paiLens = paiLens;
        }


        public void decode(Input _in) {
            index = _in.readInt();
            paiLens = _in.readInt();
        }


        public void encode(Output _out) {
            _out.writeInt(getIndex());
            _out.writeInt(getPaiLens());
        }


        public int getIndex() {
            return index;
        }

        public void setIndex(int index) {
            this.index = index;
        }

        public int getPaiLens() {
            return paiLens;
        }

        public void setPaiLens(int paiLens) {
            this.paiLens = paiLens;
        }


        public String toString() {
            return "DouniuCasiPaiRet [index=" + index + ", paiLens=" + paiLens + "]";
        }


        public int getMessageId() {
            return ID;
        }

        public int getMessageType() {
            return TYPE;
        }


    }
}
