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
    class SanJiFenXiao : Message
    {
        public static int TYPE = 7;
        public static int ID = 42;



        public void decode(Input _in)
        {

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
            return string.Format("SanJiFenXiao[]");
        }
    }
}
