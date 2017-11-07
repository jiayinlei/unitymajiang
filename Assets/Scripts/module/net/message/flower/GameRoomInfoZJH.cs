using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
using com.guojin.mj.net.message.game;
using System.Collections.Generic;

namespace com.guojin.mj.net.message.flower
{
    /// <summary>
    ///  同步游戏信息 0东 1南 2西 3北 逆时针顺序
    /// </summary>
    public class GameRoomInfoZJH : Message
    {
        public static int TYPE = 3;
        public static int ID = 6;

        public static GameRoomInfoZJH create(List<GameUserInfoZJH> sceneUser, bool start, string roomId, int chapterNums, int chapterMax, int danZhu, int beginMoney,bool zhuangjia, int playerNum)
        {
            GameRoomInfoZJH gameRoomInfoZJH = new GameRoomInfoZJH();
            gameRoomInfoZJH._sceneUser = sceneUser;
            gameRoomInfoZJH._start = start;
            gameRoomInfoZJH._roomId = roomId;
            gameRoomInfoZJH._playerNum = playerNum;
            gameRoomInfoZJH._chapterNums = chapterNums;
            gameRoomInfoZJH._chapterMax = chapterMax;
            gameRoomInfoZJH._danZhu = danZhu;
            gameRoomInfoZJH._beginMoney = beginMoney;
            gameRoomInfoZJH._zhuangjia = zhuangjia;
            return gameRoomInfoZJH;
        }

        // 返回数据
        protected List<GameUserInfoZJH> _sceneUser = new List<GameUserInfoZJH>();//玩家信息
        protected bool _start;//是否开始
        protected string _roomId;//房间号
        protected int _playerNum;// 玩家数量,3/4
        protected int _chapterNums;//当前局数
        protected int _chapterMax;//总局数
        protected int _danZhu;//单注
        protected int _beginMoney;//初始筹码
        protected bool _zhuangjia;//坐庄方式
        protected ChapterInfoZJH _chapterInfo;//一局炸金花的信息

        public GameRoomInfoZJH()
        {

        }
        public void decode(Input _in)
        {
            var sceneUserLen = _in.readInt();
            if (sceneUserLen == -1)
            {
                sceneUser = null;
            }
            else
            {
                for (int sceneUserI = 0; sceneUserI < sceneUserLen; sceneUserI++)
                {
                    GameUserInfoZJH sceneUserItemZJH = new GameUserInfoZJH();
                    sceneUserItemZJH.decode(_in);
                    sceneUser.Add(sceneUserItemZJH);
                }
            }
          
            _start = _in.readBoolean();
            _roomId = _in.readString();
            _playerNum = _in.readInt();// 玩家数量 
            _chapterNums = _in.readInt();
            _chapterMax = _in.readInt();
            _danZhu = _in.readInt();
            _beginMoney = _in.readInt();
            _zhuangjia = _in.readBoolean();
            bool chapterInfoIsNotNull = _in.readBoolean();

            if (chapterInfoIsNotNull)
            {
                _chapterInfo = new ChapterInfoZJH();
                _chapterInfo.decode(_in);
            }
            else
            {
                _chapterInfo = null;
            }
        }

        public void encode(Output _out)
        {
            if (sceneUser == null)
            {
                _out.writeInt(-1);
            }
            else
            {
                int sceneUserLen = sceneUser.Count;
                _out.writeInt(sceneUserLen);
                for (int sceneUserI = 0; sceneUserI < sceneUserLen; sceneUserI++)
                {
                    sceneUser[sceneUserI].encode(_out);
                }
            }
            _out.writeBoolean(_start);
            _out.writeString(_roomId);
            _out.writeInt(_playerNum);
            _out.writeInt(_chapterNums);
            _out.writeInt(_chapterMax);
            _out.writeInt(_danZhu);
            _out.writeInt(_beginMoney);
            _out.writeBoolean(_zhuangjia);
            if (_chapterInfo == null)
            {
                _out.writeBoolean(false);
            }
            else
            {
                _out.writeBoolean(true);
                _chapterInfo.encode(_out);
            }
        }
        public List<GameUserInfoZJH> sceneUser
        {
            get { return _sceneUser; }
            set { _sceneUser = value; }
        }

        public int PlayerNum
        {
            get { return this._playerNum; }
            set { this._playerNum = value; }
        }
        public bool start
        {
            get { return _start; }
            set { _start = value; }
        }
        public string roomId
        {
            get { return _roomId; }
            set { _roomId = value; }
        }
        public int ChapterNums
        {
            get { return this._chapterNums; }
            set { this._chapterNums = value; }
        }
        public int ChapterMax
        {
            get
            {
                return _chapterMax;
            }

            set
            {
                _chapterMax = value;
            }
        }

        public int DanZhu
        {
            get
            {
                return _danZhu;
            }

            set
            {
                _danZhu = value;
            }
        }

        public int BeginMoney
        {
            get
            {
                return _beginMoney;
            }

            set
            {
                _beginMoney = value;
            }
        }

        public bool Zhuangjia
        {
            get
            {
                return _zhuangjia;
            }

            set
            {
                _zhuangjia = value;
            }
        }

        public ChapterInfoZJH ChapterInfo
        {
            get
            {
                return _chapterInfo;
            }

            set
            {
                _chapterInfo = value;
            }
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
            return "GameRoomInfoZJH [sceneUser=" + _sceneUser + ",start=" + _start + ",roomId=" + _roomId +  ",playerNum=" + _playerNum + ",chapterNums=" + _chapterNums + ",chapterMax=" + _chapterMax + ",danZhu=" + _danZhu + ",beginMoney=" + _beginMoney + ",zhuangjia=" + _zhuangjia + ",chapterInfo=" + _chapterInfo + ", ]";
        }
    }
}
