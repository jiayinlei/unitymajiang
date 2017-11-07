using com.guojin.core.io;
using com.guojin.core.io.message;
using System.Collections.Generic;
namespace com.guojin.mj.net.message.game
{
    public class RecordDetailsRet : Message
    {
        public static int TYPE = 1;
        public static int ID = 48;
        //private string daMingGang;//大明刚
        //private string xiaoMingGang;//小明杠
        //private string anGang;//暗杠
        //private string huFen;//胡分
        //private List<ZhanJiZui> zhanJIZui = new List<ZhanJiZui>();//嘴子

        public string[] daMingGang;//大明刚
        public string[] xiaoMingGang;//小明杠
        public string[] anGang;//暗杠
        public string huFen;//胡分
        //public string[] zui;//嘴子
        public string zuiscore;//嘴分数
        public bool isZiMo;
        public bool isZhuangJiaDi;
        //public string[] zuiT;//嘴子
        public string content;//嘴分数
        public void decode(Input _in)
        {
            daMingGang = _in.readStringArray();
            xiaoMingGang = _in.readStringArray();
            anGang = _in.readStringArray();
            huFen = _in.readString();
           // zui = _in.readStringArray();
            zuiscore = _in.readString();
            isZiMo = _in.readBoolean();
            isZhuangJiaDi = _in.readBoolean();
           // zuiT = _in.readStringArray();
             content = _in.readString();
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
            return "RecordDetailsRet";
        }
    }
}