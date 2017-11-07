using System;
using com.guojin.core.io;
using com.guojin.core.io.message;

/* ***********************************************
 * Describe:消息体:俱乐部信息
 * Author :  MuLongFei
 * Email: 424113771@qq.com
 * DATA: 2017/7/14 16:03:11 
 * FileName: ClubInfo 
 * Version: V1.0.1
 * ***********************************************/

namespace com.guojin.mj.net.message.club
{
    class SanJiFenXiaoRet : Message
    {
        public static int TYPE = 7;
        public static int ID = 43;


        private int _higherID;                  //上级Id
        private int _peopeleNum;          //我的会员
        private double _missMoney;     //错过收益
        private bool _agency;                //代理

        public int HigherID
        {
            get
            {
                return _higherID;
            }

            set
            {
                _higherID = value;
            }
        }

        public int PeopeleNum
        {
            get
            {
                return _peopeleNum;
            }

            set
            {
                _peopeleNum = value;
            }
        }

        public double MissMoney
        {
            get
            {
                return _missMoney;
            }

            set
            {
                _missMoney = value;
            }
        }

        public bool Agency
        {
            get
            {
                return _agency;
            }

            set
            {
                _agency = value;
            }
        }

        public void decode(Input _in)
        {
            HigherID = _in.readInt();
            PeopeleNum = _in.readInt();
            MissMoney = _in.readDouble();
            Agency = _in.readBoolean();
        }

        public void encode(Output _out)
        {
        }

        public int getMessageType()
        {
            return TYPE;
        }

        public int getMessageId()
        {
            return ID;
        }

        public string toString()
        {
            return string.Format("sanjifenxiaoRet[]");
        }
    }
}
