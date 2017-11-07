namespace com.guojin.core.io
{
     /*
     * 协议异常类
     */
    public class ProtocolException : System.Exception
    {
        public ProtocolException(string message = "", int id = 0) : base(message)
        {
        }

        public static ProtocolException newTypeException(int b)
        {
            return new ProtocolException("error type :" + b);
        }

        public static ProtocolException newIn64Exception(long v)
        {
            return new ProtocolException("不支持的int64值,不能是小数或者负数 error value :" + v);
        }

        public static ProtocolException newIn64ExceptionByHight(uint v)
        {
            return new ProtocolException("不支持的int64值,不能是小数或者负数 hight value :" + v);
        }
    }
}