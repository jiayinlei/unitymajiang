using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.flower
{
    /// <summary>
    /// 炸金花总局数结果，返回
    /// </summary>
    public class GameOverInfoRetZJH : Message
    {
        public static int TYPE = 3;
        public static int ID = 20;
        public static GameOverInfoRetZJH create(int playerIndex1, int playerID1, int playerMoney1,
            int playerIndex2, int playerID2, int playerMoney2, 
            int playerIndex3, int playerID3, int playerMoney3,
            int playerIndex4, int playerID4, int playerMoney4)
        {
            GameOverInfoRetZJH userPlaceMsg = new GameOverInfoRetZJH();
            userPlaceMsg._playerIndex1 = playerIndex1;
            userPlaceMsg._playerID1 = playerID1;
            userPlaceMsg._playerMoney1 = playerMoney1;
            userPlaceMsg._playerIndex1 = playerIndex2;
            userPlaceMsg._playerID1 = playerID2;
            userPlaceMsg._playerMoney1 = playerMoney2;
            userPlaceMsg._playerIndex1 = playerIndex3;
            userPlaceMsg._playerID1 = playerID3;
            userPlaceMsg._playerMoney1 = playerMoney3;
            userPlaceMsg._playerIndex1 = playerIndex4;
            userPlaceMsg._playerID1 = playerID4;
            userPlaceMsg._playerMoney1 = playerMoney4;
            return userPlaceMsg;
        }
        protected int _playerIndex1;
        protected int _playerID1;
        protected int _playerMoney1;
        protected int _playerIndex2;
        protected int _playerID2;
        protected int _playerMoney2;
        protected int _playerIndex3;
        protected int _playerID3;
        protected int _playerMoney3;
        protected int _playerIndex4;
        protected int _playerID4;
        protected int _playerMoney4;

        public int PlayerIndex1
        {
            get
            {
                return _playerIndex1;
            }

            set
            {
                _playerIndex1 = value;
            }
        }

        public int PlayerID1
        {
            get
            {
                return _playerID1;
            }

            set
            {
                _playerID1 = value;
            }
        }

        public int PlayerMoney1
        {
            get
            {
                return _playerMoney1;
            }

            set
            {
                _playerMoney1 = value;
            }
        }

        public int PlayerIndex2
        {
            get
            {
                return _playerIndex2;
            }

            set
            {
                _playerIndex2 = value;
            }
        }

        public int PlayerID2
        {
            get
            {
                return _playerID2;
            }

            set
            {
                _playerID2 = value;
            }
        }

        public int PlayerMoney2
        {
            get
            {
                return _playerMoney2;
            }

            set
            {
                _playerMoney2 = value;
            }
        }

        public int PlayerIndex3
        {
            get
            {
                return _playerIndex3;
            }

            set
            {
                _playerIndex3 = value;
            }
        }

        public int _PlayerID3
        {
            get
            {
                return _playerID3;
            }

            set
            {
                _playerID3 = value;
            }
        }

        public int PlayerMoney3
        {
            get
            {
                return _playerMoney3;
            }

            set
            {
                _playerMoney3 = value;
            }
        }

        public int PlayerIndex4
        {
            get
            {
                return _playerIndex4;
            }

            set
            {
                _playerIndex4 = value;
            }
        }

        public int PlayerID4
        {
            get
            {
                return _playerID4;
            }

            set
            {
                _playerID4 = value;
            }
        }

        public int PlayerMoney4
        {
            get
            {
                return _playerMoney4;
            }

            set
            {
                _playerMoney4 = value;
            }
        }

        public GameOverInfoRetZJH()
        {

        }
        public void decode(Input _in)
        {

        }

        public void encode(Output _out)
        {

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
            return "GameOverInfoRetZJH [ ]";
        }
    }
}
