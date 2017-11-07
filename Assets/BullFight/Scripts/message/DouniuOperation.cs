using System;
using com.guojin.core.io;
using com.guojin.core.io.message;

/**
 * 返回其他用户的下注数量
 * 
 * <b>生成器生成代码，请勿修改，扩展请继承</b>
 * @author isnowfox消息生成器
 */
namespace com.guojin.dn.net.message {
    public class DouniuOperation : Message {
        public static int TYPE = 5;
        public static int ID = 12;

        /*
         * opt_qiangzhuang , opt_xiazhu, opt_liangpai
         */
        private string opt;//操作类型
        private int zhuNum;//第一手牌的注数
        private int zhuNum2;//第一手牌的注数（没用）

        public string Opt {
            get {
                return opt;
            }

            set {
                opt = value;
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

        //  /**
        //* 返回结果
        //*/
        //  private bool flage;
        /**
         * 下住的数量
         */

        public DouniuOperation() {

        }

        public static DouniuOperation create(string opt, int zhuNum,int zhuNum2) {
            DouniuOperation operation = new DouniuOperation();
            operation.Opt = opt;
            operation.ZhuNum = zhuNum;
            operation.ZhuNum2 = zhuNum2;
            return operation;
        }

        //public void decode(Input _in) {
        //    UnityEngine.Debug.Log("deaaaaa");
        //    Opt = _in.readString();
        //    ZhuNum = _in.readInt();
        //}

        public void decode(Input _in) {
            Opt = _in.readString();
            ZhuNum = _in.readInt();
            ZhuNum2 = _in.readInt();
            //throw new NotImplementedException();
        }
        public void encode(Output _out) {
            // UnityEngine.Debug.Log("enaaaaa");
            _out.writeString(Opt);
            //UnityEngine.Debug.Log("en2aaaaa");
            _out.writeInt(ZhuNum);
            _out.writeInt(ZhuNum2);
            //UnityEngine.Debug.Log("en3aaaaa");
        }


        ///**
        // * 返回结果
        // */
        //public bool getFlage() {
        //    return flage;
        //}

        ///**
        // * 返回结果
        // */
        //public void setFlage(bool flage) {
        //    this.flage = flage;
        //}



        public string toString() {
            return "DouniuShu [opt=" + Opt + ",zhuNum=" + ZhuNum + ",zhuNum2=" + ZhuNum2 + ", ]";
        }

        public int getMessageType() {
            return TYPE;
        }
        public int getMessageId() {
            return ID;
        }

    }
}
