
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
    public class DouniuCasiPai : Message {
        public static int TYPE = 5;
        public static int ID = 52;

        private int paiLens;//牌的数量

        public DouniuCasiPai() {

        }

        public DouniuCasiPai(int paiLens) {
            this.paiLens = paiLens;
        }


        public void decode(Input _in) {
            paiLens = _in.readInt();
        }


        public void encode(Output _out) {
            _out.writeInt(paiLens);
        }


        public int getPaiLens() {
            return paiLens;
        }

        public void setPaiLens(int paiLens) {
            this.paiLens = paiLens;
        }


        public String toString() {
            return "DouniuCasiPai [paiLens=" + paiLens + "]";
        }


        public int getMessageId() {
            return ID;
        }

        public int getMessageType() {
            return TYPE;
        }


    }
}
