using System;

using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.game
{
    /// <summary>
    /// 用户离线
    /// </summary>
    public class UserOffline : Message
    {
        public static int TYPE = 1;
        public static int ID = 19;

        public static UserOffline create(int index)
        {
            UserOffline userOffline = new UserOffline();
            userOffline._index = index;
            return userOffline;

        }

        protected int _index;
        public UserOffline()
        {

        }

        public void decode(Input _in)
        {
            _index = _in.readInt();
        }

        public void encode(Output _out)
        {
            _out.writeInt(index);
        }
        public int index
        {
            get { return _index; }
            set { _index = value; }
        }
        public int getMessageId()
        {
            return ID;
        }

        public int getMessageType()
        {
            return TYPE;
        }

        public string toString()
        {
            return "UserOffline [index=" + _index + ", ]";
        }
    }
}