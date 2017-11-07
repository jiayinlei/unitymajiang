using System;
using com.guojin.core.io;
using com.guojin.core.io.message;

/**
 * 
* @ClassName: PdkStartGame
* @Description: 斗牛开始游戏消息
* @author qixianghui
* @date 2017年7月10日
* */

namespace com.guojin.dn.net.message {
    public class DouniuStartGame : Message {
        public static int TYPE = 5;
        public static int ID = 8;

        private bool isReady;

        public bool IsReady {
            get {
                return isReady;
            }

            set {
                isReady = value;
            }
        }

        public DouniuStartGame() {

        }
        public DouniuStartGame(bool ready) {
            IsReady = ready;
        }


        public void decode(Input _in) {
            IsReady= _in.readBoolean();
        }


        public void encode(Output _out) {
            _out.writeBoolean(IsReady);
        }


        public int getMessageId() {
            return ID;
        }

        public int getMessageType() {
            return TYPE;
        }

        public string toString() {
            return "GameChapterStart";
        }
    }
}
