
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
using System.Collections.Generic;
namespace com.guojin.mj.net.message.login
{
    public class RoomHistoryListRet : Message
    {
        public static int TYPE = 7;
        public static int ID = 20;

        public static RoomHistoryListRet create(List<RoomHistory> list)
        {
            RoomHistoryListRet roomHistoryListRet = new RoomHistoryListRet();
            roomHistoryListRet._list = list;
            return roomHistoryListRet;
        }

        protected List<RoomHistory> _list=new List<RoomHistory>();

        public RoomHistoryListRet()
        {

        }
        public void decode(Input _in)
        {
            int listLen = _in.readInt();
            if (listLen== -1)
            {
                _list = null;
            }
            else
            {
                for (int listI = 0; listI < listLen; listI++)
                {
                    RoomHistory listItem = new RoomHistory();
                    listItem.decode(_in);
                    _list.Add(listItem);
                }
            }
        }

        public void encode(Output _out)
        {
            if (list == null)
            {
				_out.writeInt(-1);
            }
            else
            {
                int  listLen = list.Count;
				_out.writeInt(listLen);
                for (int listI = 0; listI < listLen; listI++)
                {
                    list[listI].encode(_out);
                }
            }
        }
        public List<RoomHistory> list
        {
            get { return _list; }
            set { _list = value; }
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
            return "RoomHistoryListRet [list=" + _list + ", ]";
        }
    }
}