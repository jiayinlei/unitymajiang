
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.game
{
    //用户退出游戏
    public class GameExitUser : Message
    {
        public static int TYPE = 1;
        public static int ID = 4;

        public static GameExitUser create(int userId)
        {
            GameExitUser gameExitUser = new GameExitUser();
            gameExitUser._userId = userId;
            return gameExitUser;
        }
        protected int _userId;

        public GameExitUser()
        {
                     
        }
        public void decode(Input _in)
        {
            _userId = _in.readInt();
        }

        public void encode(Output _out)
        {
           _out.writeInt(userId);
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
            return "GameExitUser [userId=" + _userId + ", ]";
        }
    }
}