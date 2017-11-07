namespace com.guojin.core.io.message
{
    public interface Message
    {
        void decode(Input _in);

        void encode(Output _out);

        int getMessageType();

        int getMessageId();

        string toString();
    }
}