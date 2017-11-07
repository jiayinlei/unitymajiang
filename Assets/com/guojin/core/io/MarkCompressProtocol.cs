namespace com.guojin.core.io
{
    /*
     * 消息打包协议
     */
    public class MarkCompressProtocol
    {
        public const string DEFAULT_CHARSET = "utf-8";
        //public static const bool DEFAULT_IS_BIG_ENDIAN						= true; 


        public const int THREE_MAX = 0x7fffff;

        public const int THREE_MIN = -8388608;
        /**
         * 第一个字节只有7位数可以用
         */
        public const int FIRST_WIDTH = 7;

        /**
         * 其他字节宽度
         */
        public const int OTHER_WIDTH = 8;

        //类型只能小于等于127
        public const int TYPE_NULL_OR_ZERO_OR_FALSE = 0xFF - 1;
        public const int TYPE_TRUE = 0xFF - 2;

        public const int TYPE_INT_1BYTE = 0xFF - 3;
        public const int TYPE_INT_2BYTE = 0xFF - 4;
        public const int TYPE_INT_3BYTE = 0xFF - 5;
        public const int TYPE_INT_4BYTE = 0xFF - 6;

        public const int TYPE_INT_5BYTE = 0xFF - 7;
        public const int TYPE_INT_6BYTE = 0xFF - 8;
        public const int TYPE_INT_7BYTE = 0xFF - 9;
        public const int TYPE_INT_8BYTE = 0xFF - 10;


        public const int TYPE_STRING = 0xFF - 11;
        public const int TYPE_BYTES = 0xFF - 12;

        public const int TYPE_MAX = 0xFF - 14;
        public const int TYPE_MIN = 0;

        public const int TYPE_MINUS = 4;

        public const int TYPE_ONE_BYTE_MAX = TYPE_MAX - TYPE_MINUS;
        public const int TYPE_ONE_BYTE_MIN = 0 - TYPE_MINUS;
    }
}