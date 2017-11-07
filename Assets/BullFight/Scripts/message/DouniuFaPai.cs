using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
/**
 * 发牌
 * FA:发牌，OUT:亮牌,OUT_OK:亮牌成功，没人用这个哎,X_NIU:小牛牛,Z_NIU:炸弹牛,N_NIU:牛牛,M_NIU:没牛
 * 
 * <b>生成器生成代码，请勿修改，扩展请继承</b>
 * @author isnowfox消息生成器
 */
namespace com.guojin.dn.net.message {
    public class DouniuFaPai : Message {
        public static int TYPE = 5;
        public static int ID = 10;

        /**
         * 位置
         */
        private int index;
        private int pai;
        private bool win;


        public DouniuFaPai() {

        }

        public DouniuFaPai(int index, int pai, bool niuNiu, bool xNiuNiu, bool zNiuNiu, bool mNiuNiu) {
            this.index = index;
            this.pai = pai;
            this.win = niuNiu;
            /*this.xNiuNiu = xNiuNiu;
            this.zNiuNiu = zNiuNiu;
            this.mNiuNiu = mNiuNiu;*/
        }


        public void decode(Input _in) {
            index = _in.readInt();
            pai = _in.readInt();
            win = _in.readBoolean();
            //		xNiuNiu = in.readbool();
            //		zNiuNiu = in.readbool();
            //		mNiuNiu = in.readbool();
        }


        public void encode(Output _out) {
            _out.writeInt(getIndex());
            _out.writeInt(getPai());
            _out.writeBoolean(isWin());
            /*	out.writebool(getXNiuNiu());
                out.writebool(getZNiuNiu());
                out.writebool(getMNiuNiu());*/
        }

        /**
         * 位置
         */
        public int getIndex() {
            return index;
        }

        /**
         * 位置
         */
        public void setIndex(int index) {
            this.index = index;
        }

        public int getPai() {
            return pai;
        }

        public void setPai(int pai) {
            this.pai = pai;
        }


        //	public bool getXNiuNiu() {
        //		return xNiuNiu;
        //	}
        //	
        //	public void setXNiuNiu(bool xNiuNiu) {
        //		this.xNiuNiu = xNiuNiu;
        //	}
        //
        //	public bool getZNiuNiu() {
        //		return zNiuNiu;
        //	}
        //	
        //	public void setZNiuNiu(bool zNiuNiu) {
        //		this.zNiuNiu = zNiuNiu;
        //	}
        //
        //	public bool getMNiuNiu() {
        //		return mNiuNiu;
        //	}
        //	
        //	public void setMNiuNiu(bool mNiuNiu) {
        //		this.mNiuNiu = mNiuNiu;
        //	}


        public bool isWin() {
            return win;
        }

        public void setWin(bool win) {
            this.win = win;
        }


        public string toString() {
            return "DouNiuFaPai [index=" + index + ",pai=" + pai + ",niuNiu=" + win + ", ]";
        }


        public int getMessageType() {
            return TYPE;
        }


        public int getMessageId() {
            return ID;
        }
    }
}
