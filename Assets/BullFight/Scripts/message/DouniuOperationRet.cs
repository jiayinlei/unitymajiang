using System;
using com.guojin.core.io;
using com.guojin.core.io.message;

/**
 * 服务器返回下注的结果
 * 
 * <b>生成器生成代码，请勿修改，扩展请继承</b>
 * @author isnowfox消息生成器
 */
namespace com.guojin.dn.net.message {
    public class DouniuOperationRet : Message {
        public static int TYPE = 5;
        public static int ID = 13;
        private string opt;//操作的类型
        private int index;//操作的用户位置索引
        private int zhuNum;//第一手牌下注的数量
        private int zhuNum2;//第二手牌下注的数量（没用）
        private int userZhu;  //用户现在的注数
        private int totalZhu;  //注池中所有的总注数

        public string Opt {
            get {
                return opt;
            }

            set {
                opt = value;
            }
        }

        public int Index {
            get {
                return index;
            }

            set {
                index = value;
            }
        }

        public int ZhuNum {
            get {
                return zhuNum;
            }

            set {
                zhuNum = value;
            }
        }

        public int ZhuNum2 {
            get {
                return zhuNum2;
            }

            set {
                zhuNum2 = value;
            }
        }

        public int UserZhu {
            get {
                return userZhu;
            }

            set {
                userZhu = value;
            }
        }

        public int TotalZhu {
            get {
                return totalZhu;
            }

            set {
                totalZhu = value;
            }
        }

        public DouniuOperationRet() {

        }



        public DouniuOperationRet(String opt, int index, int zhuNum, int zhuNum2) {
            this.Opt = opt;
            this.Index = index;
            this.ZhuNum = zhuNum;
            this.ZhuNum2 = zhuNum2;
        }


        public void decode(Input _in) {

            Opt = _in.readString();
            Index = _in.readInt();
            ZhuNum = _in.readInt();
            ZhuNum2 = _in.readInt();
        }


        public void encode(Output _out) {
            _out.writeString(Opt);
            _out.writeInt(Index);
            _out.writeInt(ZhuNum);
            _out.writeInt(ZhuNum2);
        }





        public String toString() {
            return "DouniuZhuRet [opt=" + Opt + ", index=" + Index + ", zhuNum=" + ZhuNum + ", zhuNum2=" + ZhuNum2
                    + ", userZhu=" + UserZhu + ", totalZhu=" + TotalZhu + "]";
        }



        public int getMessageType() {
            return TYPE;
        }


        public int getMessageId() {
            return ID;
        }
    }
}
