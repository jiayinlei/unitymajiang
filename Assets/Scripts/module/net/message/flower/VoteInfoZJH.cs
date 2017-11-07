using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.flower
{
    /// <summary>
    /// 炸金花投票信息，发送
    /// </summary>
    public class VoteInfoZJH : Message
    {
        public static int TYPE = 3;
        public static int ID = 26;
        public static VoteInfoZJH Create(bool isAgree,int index)
        {
            VoteInfoZJH lcf = new VoteInfoZJH();
            lcf._isAgree = isAgree;
            lcf._index = index;
            return lcf;
        }
        protected bool _isAgree;
        /// <summary>
        /// 玩家位置
        /// </summary>
        protected int _index;
        public int Index
        {
            get
            {
                return _index;
            }

            set
            {
                _index = value;
            }
        }
        public bool IsAgree
        {
            get
            {
                return _isAgree;
            }

            set
            {
                _isAgree = value;
            }
        }
        public VoteInfoZJH()
        {

        }
        public void decode(Input _in)
        {
            _index = _in.readInt();
            _isAgree = _in.readBoolean();
        }

        public void encode(Output _out)
        {
            _out.writeInt(_index);
            _out.writeBoolean(_isAgree);
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
            return "VoteInfoZJH [ " + "isAgree" + _isAgree + "index" + _index + ",]";
        }
    }
}


