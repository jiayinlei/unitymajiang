using System;

using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.game
{
   public class VoteDelSelect : Message
    {
        public static int TYPE = 1;
        public static int ID = 21;

        public static VoteDelSelect create(int userId,string userName)
        {
            VoteDelSelect voteDelSelect = new VoteDelSelect();
            voteDelSelect._userId = userId;
            voteDelSelect._userName = userName;
            return voteDelSelect;
        }
        protected int _userId;
        protected string  _userName;
        public void decode(Input _in)
        {
            _userId = _in.readInt();
            _userName = _in.readString();
        }

        public void encode(Output _out)
        {
            _out.writeInt(userId);
            _out.writeString(userName);
        }
        public int userId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        public string  userName
        {
            get { return _userName; }
            set { _userName = value; }
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
            return "VoteDelSelect [userId=" + _userId + ",userName=" + _userName + ", ]";
        }
    }
}