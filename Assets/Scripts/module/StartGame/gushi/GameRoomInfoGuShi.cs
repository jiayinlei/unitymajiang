using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
using com.guojin.mj.net.message.game;
using System.Collections.Generic;

namespace com.guojin.mj.net.message.game
{
    /// <summary>
    ///  同步游戏信息 0东 1南 2西 3北 逆时针顺序
    /// </summary>
    public class GameRoomInfoGuShi : Message
    {
        public static int TYPE = 1;
        public static int ID = 37;

        public static GameRoomInfoGuShi create(List<GameUserInfo> sceneUser, bool start, string roomCheckId, int leftChapterNums, int createUserId, MajiangChapterMsg chapter
            , int chapterMax, int playerNum, int state, bool currentChapterPao, bool isbudaifeng, int isfangpao)
        {
            GameRoomInfoGuShi gameRoomInfo = new GameRoomInfoGuShi();
            gameRoomInfo._sceneUser = sceneUser;
            gameRoomInfo._start = start;
            gameRoomInfo._roomCheckId = roomCheckId;
            gameRoomInfo._leftChapterNums = leftChapterNums;
            gameRoomInfo._createUserId = createUserId;
            gameRoomInfo._chapter = chapter;
            gameRoomInfo._chapterMax = chapterMax;
            gameRoomInfo._playerNum = playerNum;
            gameRoomInfo._state = state;
            gameRoomInfo._currentChapterPao = currentChapterPao;
            gameRoomInfo._isbudaifeng = isbudaifeng;
            gameRoomInfo._isFangPao = isfangpao;
            return gameRoomInfo;
        }

        // 返回数据
        protected List<GameUserInfo> _sceneUser = new List<GameUserInfo>();
        protected bool _start;
        protected string _roomCheckId;
        protected int _leftChapterNums;
        protected int _createUserId;
        protected MajiangChapterMsg _chapter = new MajiangChapterMsg();
        protected int _chapterMax;//局数
        protected int _playerNum;// 玩家数量,3/4
        protected int _state;// 状态 1 为选跑完毕 4 5 6
        protected bool _currentChapterPao;
        protected bool _isbudaifeng;//带风牌不带风 ``
        protected int _isFangPao;//点炮胡 放炮胡 ` 

        public string majiangType ;//麻将的类型，在这里是 gushi
        public bool is_que_men;  //是否缺门
        public bool isPaoZui;//是否跑嘴(这个暂时的去掉)
        public int diFen ;  //用户的底分
        public int xuanpao;
        public bool zhuangjia;
        public string[] xuanzui;
        public GameRoomInfoGuShi()
        {

        }
        public void decode(Input _in)
        {
            Method.MJType = "MaJongGuShi";
            if (!Method.isHor)
            {
                Method.CanAddMessage = true;
            }            
            Method.messagelist.Clear();
            _sceneUser = _in.readList<GameUserInfo>(_in);
            _start = _in.readBoolean();
            _roomCheckId = _in.readString();
            _leftChapterNums = _in.readInt();
            _createUserId = _in.readInt();
            _chapter = _in.readT<MajiangChapterMsg>(_in);
            _chapterMax = _in.readInt();
            _playerNum = _in.readInt();// 玩家数量 
            _state = _in.readInt();// 状态值
            _currentChapterPao = _in.readBoolean();
            _isbudaifeng = _in.readBoolean();
            _isFangPao = _in.readInt();
            majiangType = _in.readString();
            is_que_men = _in.readBoolean();
            isPaoZui = _in.readBoolean();
            diFen = _in.readInt();           
            zhuangjia = _in.readBoolean();
            xuanpao = _in.readInt();
            xuanzui = _in.readStringArray();
        }

        public void encode(Output _out)
        {
            
            _out.writeList(_out, sceneUser);
            _out.writeBoolean(start);
            _out.writeString(roomCheckId);
            _out.writeInt(leftChapterNums);
            _out.writeInt(createUserId);
            _out.writeT(_out, chapter);
            _out.writeInt(ChapterMax);
            _out.writeInt(PlayerNum);
            _out.writeInt(State);
            _out.writeBoolean(CurrentChapterPao);
            _out.writeBoolean(Isbudaifeng);
            _out.writeInt(IsFangPao);
        }
        public List<GameUserInfo> sceneUser
        {
            get { return _sceneUser; }
            set { _sceneUser = value; }
        }
        public bool start
        {
            get { return _start; }
            set { _start = value; }
        }
        public string roomCheckId
        {
            get { return _roomCheckId; }
            set { _roomCheckId = value; }
        }
        public int leftChapterNums
        {
            get { return _leftChapterNums; }
            set { _leftChapterNums = value; }
        }
        public int createUserId
        {
            get { return _createUserId; }
            set { _createUserId = value; }
        }

        public MajiangChapterMsg chapter
        {
            get { return _chapter; }
            set { _chapter = value; }
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
        public int PlayerNum
        {
            get { return this._playerNum; }
            set { this._playerNum = value; }
        }
        public int State
        {
            get { return this._state; }
            set { this._state = value; }
        }
        public bool CurrentChapterPao
        {
            get
            {
                return _currentChapterPao;
            }

            set
            {
                _currentChapterPao = value;
            }
        }
        public bool Isbudaifeng
        {
            get
            {
                return _isbudaifeng;
            }

            set
            {
                _isbudaifeng = value;
            }
        }
        public int IsFangPao
        {
            get
            {
                return _isFangPao;
            }

            set
            {
                _isFangPao = value;
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
            return "GameRoomInfoGuShi [sceneUser=" + _sceneUser + ",start=" + _start + ",roomCheckId=" + _roomCheckId + ",leftChapterNums=" + _leftChapterNums + ",createUserId=" + _createUserId + ",chapter=" + _chapter +"____"+ xuanpao + ","+ _playerNum + ", ]";
        }
    }

}