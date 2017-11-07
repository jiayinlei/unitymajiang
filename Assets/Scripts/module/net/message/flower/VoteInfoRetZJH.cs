using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.flower
{
    /// <summary>
    /// 炸金花投票结果，返回
    /// </summary>
    public class VoteInfoRetZJH : Message
    {
        public static int TYPE = 3;
        public static int ID = 26;
        public static VoteInfoRetZJH Create(bool result)
        {
            VoteInfoRetZJH lcf = new VoteInfoRetZJH();
            lcf._result = result;
            return lcf;
        }
        protected bool _result;
        public bool Result
        {
            get
            {
                return _result;
            }

            set
            {
                _result = value;
            }
        }
        public VoteInfoRetZJH()
        {

        }
        public void decode(Input _in)
        {
            _result = _in.readBoolean();
        }

        public void encode(Output _out)
        {
            _out.writeBoolean(_result);
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
            return "VoteInfoRetZJH [ " + "result" + _result + ",]";
        }
    }
}



