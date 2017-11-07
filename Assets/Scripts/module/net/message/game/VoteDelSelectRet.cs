using System;
using com.guojin.core.io;
using com.guojin.core.io.message;

namespace com.guojin.mj.net.message.game
{
    public class VoteDelSelectRet : Message
    {
        public static int TYPE = 1;
        public static int ID = 22;

        public static VoteDelSelectRet create(bool result,int userId)
        {
            VoteDelSelectRet voteDelSelectRet = new VoteDelSelectRet();
            voteDelSelectRet._result = result;
            voteDelSelectRet._userId = userId;
            return voteDelSelectRet;
        }  
              
        protected bool _result;
        protected int _userId;
        public VoteDelSelectRet()
        {

        }
        public void decode(Input _in)
        {
            _result = _in.readBoolean();
            _userId = _in.readInt();
        }

        public void encode(Output _out)
        {
            _out.writeBoolean(result);
            _out.writeInt(userId);
        }
        public bool result
        {
            get { return _result; }
            set { _result = value; }
        }
        public int userId
        {
            get { return _userId; }
            set { _userId = value; }
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
            return "VoteDelSelectRet [result=" + _result + ",userId=" + _userId + ", ]";
        }
    }
}