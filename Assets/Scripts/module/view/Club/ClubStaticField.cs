
/* ***********************************************
 * Describe:俱乐部模块消息提示
 * Author :  MuLongFei
 * Email: 424113771@qq.com
 * DATA: 2017/7/17 9:08:40 
 * FileName: ClubStaticField 
 * Version: V1.0.1
 * ***********************************************/

namespace com.guojin.mj.net.message.club {
    public static class ClubStaticField {
        
        public static object DEBUG_SEND_JOIN_CLUB_REQ = "向服务器发送加入俱乐部请求";

        public static string ERROR_UNKNOW = "发生未知错误,请重试";
        public static string ERROR_INPUT = "您输入的俱乐部ID有误, 请重新输入";

        public static string CLUB_CREATOR = " 的俱乐部";
        public static string CLUB_INFO = "俱乐部ID:{0}\t创建者:{1}\t微信号:{2}";
        public static string CLUB_NOT_FOUND = "没有此俱乐部,请重新输入";
        public static string CLUB_REVEIVE_MSG = "收到服务器的消息:TYPE: {0},ID: {1}";
        public static string CLUB_NO_NOTICE = "该俱乐部暂时没有公告";
    }
}
