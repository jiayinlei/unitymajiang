
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.login
{
    public class RoomHistory : Message
    {
        public static int TYPE = 7;
        public static int ID = 18;

        public static RoomHistory create(string roomCheckId, string startDate, int chapterNums, int [] scores, string [] userNames,int[] winCount,int[] loseCount)
        {
            RoomHistory roomHistory = new RoomHistory();
            roomHistory._roomCheckId = roomCheckId;
            roomHistory._startDate = startDate;
            roomHistory._chapterNums = chapterNums;
            roomHistory._userNames = userNames;
            roomHistory._scores = scores;
            //roomHistory._winCount = winCount;
            //roomHistory._loseCount = loseCount;
            return roomHistory;
        }

        protected string _roomCheckId;
        protected string _startDate;
        protected int _chapterNums;
        protected string [] _userNames;
        protected int  [] _scores;
        //protected int[] _winCount;
        //protected int[] _loseCount;


        public RoomHistory()
        {

        }
        public void decode(Input _in)
        {
            _roomCheckId = _in.readString();
            _startDate = _in.readString();
            _chapterNums = _in.readInt();
            _userNames = _in.readStringArray();
            _scores = _in.readIntArray();
            //_winCount = _in.readIntArray();
            //_loseCount = _in.readIntArray();

        }

        public void encode(Output _out)
        {
            _out.writeString(roomCheckId);
            _out.writeString(startDate);
            _out.writeInt(chapterNums);
            _out.writeStringArray(userNames);
            _out.writeIntArray(scores);
            //_out.writeIntArray(_winCount);
            //_out.writeIntArray(_loseCount);
        }

        public string roomCheckId
        {
            get { return _roomCheckId; }
            set { _roomCheckId = value; }
        }
        public string startDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }
        public int chapterNums
        {
            get { return _chapterNums; }
            set { _chapterNums = value; }
        }
        public string [] userNames
        {
            get { return _userNames; }
            set { _userNames = value; }
        }
        public int  [] scores
        {
            get { return _scores; }
            set { _scores = value; }
        }
        //public int[] WinCount
        //{
        //    get { return _winCount; }
        //    set { _winCount = value; }
        //}
        //public int[] LoseCount
        //{
        //    get { return _loseCount; }
        //    set { _loseCount = value; }
        //}
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
            return "RoomHistory [roomCheckId=" + _roomCheckId + ",startDate=" + _startDate + ",chapterNums=" + _chapterNums + ",userNames=" + _userNames + ",scores=" + _scores +  ", ]";
        }
    }
}