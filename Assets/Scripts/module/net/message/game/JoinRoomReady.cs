using System;
using System.Collections.Generic;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.game
{

    public class JoinRoomReady : Message
    {
        public static int TYPE = 1;
        public static int ID = 34;
       
        public static JoinRoomReady creat(int num, List<int> userlist)
        {
            JoinRoomReady joinRoomReady = new JoinRoomReady();
            joinRoomReady._num1 = num;
            joinRoomReady._userList = userlist;
            return joinRoomReady;
        }

        protected int _num1;
        protected List<int> _userList = new List<int>();

        public int num
        {
            get
            {
                return _num1;
            }

            set
            {
                _num1 = value;
            }
        }
        public List<int> userList
        {
            get { return _userList; }
            set { _userList = value; }
        }
        public void decode(Input _in)
        {
            _num1= _in.readInt();
            int lengthint = _in.readInt();
            for (int i = 0; i < lengthint; i++)
            {
                _userList.Add(_in.readInt());
            }
        }
        public void encode(Output _out)
        {
            _out.writeInt(_num1);
            _out.writeInt(-1);
        }

        public int getMessageId()
        {
            return ID;
        }

        public int getMessageType()
        {
            return TYPE;
        }

        string returns()
        {
            List<string> str = new List<string>();
            for (int i = 0; i < userList.Count; i++)
            {
                str.Add(userList[i].ToString());
            }
            return String.Join(",",str.ToArray());
        }
        public string toString()
        {
            return "JoinRoomReady +]" + returns();
        }
    }
}
