namespace com.guojin.core.io.message
{
    public class MessageProtocol
    {
        public static int MESSAGE_MAX = 0xFFFFFF;

        /**
         * 注意，修改这个必须修改decoder和encoder
         */
        public static int LENGTH_BYTE_NUMS = 3;
        public static int HEAD_LENGTH = 3;

        public static int TYPE_NORMAL = 0;
        public static int TYPE_GZIP = 1;

        public static int TYPE_OR_ID_MAX = MarkCompressProtocol.TYPE_ONE_BYTE_MAX;

        /**
         * 自定义消息id
         */
        public static int EXPAND_MESSAGE_TYPE = 0;
    }
}