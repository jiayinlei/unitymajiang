using System;
using com.guojin.core.io;
using com.guojin.core.io.message;

/**
 * 开始游戏回复
 * @author chenmengchen
 *
 */
namespace com.guojin.dn.net.message {
    public class DouniuStartGameRet : Message {
        public static int TYPE = 5;
        public static int ID = 9;
        private int index;//准备的玩家的索引
        private bool isReady;//准备的玩家是准备还是取消准备

        public int Index {
            get {
                return index;
            }

            set {
                index = value;
            }
        }

        public bool IsReady {
            get {
                return isReady;
            }

            set {
                isReady = value;
            }
        }

        public DouniuStartGameRet() {

        }

        public DouniuStartGameRet(int index, bool isReady) {
            this.Index = index;
            this.IsReady = isReady;
        }

        
    public void decode(Input _in){

         Index= _in.readInt();
        IsReady = _in.readBoolean();
    }

    
    public void encode(Output _out) {
		 _out.writeInt(Index);
		 _out.writeBoolean(IsReady);
	}


    public String toString() {
    return "DouniuStartGameRet [index=" + Index + ", isReady=" + IsReady + "]";
}



    public  int getMessageType() {
    return TYPE;
}


    public  int getMessageId() {
    return ID;
}
    }
}
