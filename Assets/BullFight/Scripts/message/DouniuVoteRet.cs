using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
using System.Collections.Generic;

/**
 * 返回其他用户的下注数量
 * 
 * <b>生成器生成代码，请勿修改，扩展请继承</b>
 * 1. 在解散房间是， 如果房间内只有一个人，服务端就发送这个消息，直接删除房间
 * 2. 
 * 
 * @author isnowfox消息生成器
 */
namespace com.guojin.dn.net.message {

    public class DouniuVoteRet : Message {
        
        
        public static int TYPE = 5;
        public static int ID = 45;

        private bool result;//投票的结果
        private string userName;//投票的玩家的名字
        private bool operation;//确定结束对局（所有人同意投票）

        public string UserName {
            get {
                return userName;
            }

            set {
                userName = value;
            }
        }

        public bool Operation {
            get {
                return operation;
            }

            set {
                operation = value;
            }
        }

        public DouniuVoteRet() {
        }


        public DouniuVoteRet(bool result,string userName) {
            this.result = result;
            UserName = userName;
        }


        public void decode(Input _in) {
            this.result = _in.readBoolean();
            this.UserName = _in.readString();
            this.Operation = _in.readBoolean();
        }

        public void encode(Output _out) {
            _out.writeBoolean(isResult());
            _out.writeString(UserName);
            _out.writeBoolean(Operation);
        }

        public bool isResult() {
            return result;
        }


        public void setResult(bool result) {
            this.result = result;
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
            return "DouniuVoteRet";
        }
    }
}
