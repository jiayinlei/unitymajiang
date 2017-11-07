namespace com.guojin.core.io.message
{
    public interface MessageFactory
    {
        Message getMessage(int type, int id);
    }
}