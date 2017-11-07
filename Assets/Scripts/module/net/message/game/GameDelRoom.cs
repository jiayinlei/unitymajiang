
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.game
{
    //解散了房间，房间被删除
    public class GameDelRoom : Message
    {
        public static int TYPE = 1;
        public static int ID = 3;

        public static GameDelRoom create(bool isEnd,bool isStart)
        {
            GameDelRoom gameDelRoom = new GameDelRoom();
            gameDelRoom._isEnd = isEnd;
            gameDelRoom._isStart = isStart;
            return gameDelRoom;
        }

        protected bool _isEnd;
        protected bool _isStart;

        public GameDelRoom()
        {

        }

        public void decode(Input _in)
        {
            _isEnd = _in.readBoolean();
            _isStart = _in.readBoolean();
        }

        public void encode(Output _out)
        {
            _out.writeBoolean(isEnd);
			_out.writeBoolean(isStart);
        }
        public bool isEnd
        {
            get { return _isEnd; }
            set { _isStart = value; }
        }
        public bool isStart
        {
            get { return _isStart; }
            set { _isStart = value; }
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
            return "GameDelRoom [isEnd=" + _isEnd + ",isStart=" + _isStart + ", ]";
        }
    }
}