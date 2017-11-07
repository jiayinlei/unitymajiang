namespace com.guojin.core.io
{
    /*
     * 输出异常类
     */
    class AbstractMethodException : System.Exception
    {
        public AbstractMethodException() { }
        public AbstractMethodException( string message ) : base( message ) { }
        public AbstractMethodException( string message, System.Exception inner ) : base( message, inner ) { }
        protected AbstractMethodException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context ) : base( info, context ) { }
    }
}