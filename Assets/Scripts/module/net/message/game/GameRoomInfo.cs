
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
    public class GameRoomInfo : Message
    {
        public static int TYPE = 1;
        public static int ID = 7;

        public static GameRoomInfo create(List<GameUserInfo> sceneUser, bool start, string roomCheckId, int leftChapterNums, int createUserId, MajiangChapterMsg chapter
            , int chapterMax, bool isHuiEr, bool isGangDaiPao, bool isDaiZiPai, bool isQiDuiFanBei, bool isZhuangJiaDi, bool isGangKaiFan, int xuanPaoCount, bool isFangPao, bool currentChapterPa, int playerNum, int state)
        {
            GameRoomInfo gameRoomInfo = new GameRoomInfo();
            gameRoomInfo._sceneUser = sceneUser;
            gameRoomInfo._start = start;
            gameRoomInfo._roomCheckId = roomCheckId;
            gameRoomInfo._leftChapterNums = leftChapterNums;
            gameRoomInfo._createUserId = createUserId;
            gameRoomInfo._chapter = chapter;
            gameRoomInfo.ChapterMax = chapterMax;
            gameRoomInfo.IsHuiEr = isHuiEr;
            gameRoomInfo.IsGangDaiPao = isGangDaiPao;
            gameRoomInfo.IsDaiZiPai = isDaiZiPai;
            gameRoomInfo.IsQiDuiFanBei = isQiDuiFanBei;
            gameRoomInfo.IsZhuangJiaDi = isZhuangJiaDi;
            gameRoomInfo.IsGangKaiFan = isGangKaiFan;
            gameRoomInfo.XuanPaoCount = xuanPaoCount;
            gameRoomInfo.IsFangPao = isFangPao;
            gameRoomInfo.CurrentChapterPao = currentChapterPa;
            gameRoomInfo._playerNum = playerNum;
            gameRoomInfo._state = state;          
            return gameRoomInfo;
        }

        // 返回数据
        protected List<GameUserInfo> _sceneUser = new List<GameUserInfo>();
        protected bool _start;
        protected string _roomCheckId;
        protected int _leftChapterNums;
        protected int _createUserId;
        protected MajiangChapterMsg _chapter = new MajiangChapterMsg();
        protected int _playerNum;// 玩家数量,3/4
        protected int _state;// 状态 1 为选跑完毕
      

        // 发送数据
        protected int chapterMax;//局数
        protected bool isHuiEr;//带混
        protected bool isGangDaiPao;//杠带跑 ``
        protected bool isDaiZiPai;//带字牌 ``
        protected bool isQiDuiFanBei;//7对翻倍 7对胡本来就是有的 ``
        protected bool isZhuangJiaDi;//庄加底 ``
        protected bool isGangKaiFan;//杠开翻倍 ``
        protected int xuanPaoCount;//选跑局数 4局 每局``
        protected bool isFangPao;//点炮胡  `
        protected bool currentChapterPao;



        public GameRoomInfo()
        {

        }
        public void decode(Input _in)
        {
            Method.MJType = "MaJongZhengZhou";
            if (!Method.isHor)
            {
                Method.CanAddMessage = true;
            }            
            Method.messagelist.Clear();
            _sceneUser        = _in.readList<GameUserInfo>(_in);
            _start            = _in.readBoolean();
            _roomCheckId      = _in.readString();
            _leftChapterNums  = _in.readInt();
            _createUserId     = _in.readInt();
            _chapter          = _in.readT<MajiangChapterMsg>(_in);
            chapterMax        = _in.readInt();
            isHuiEr           = _in.readBoolean();
            isGangDaiPao      = _in.readBoolean();
            isDaiZiPai        = _in.readBoolean();
            isQiDuiFanBei     = _in.readBoolean();
            isZhuangJiaDi     = _in.readBoolean();
            isGangKaiFan      = _in.readBoolean();
            xuanPaoCount      = _in.readInt();
            isFangPao         = _in.readBoolean();
            currentChapterPao = _in.readBoolean();
            _playerNum        = _in.readInt();// 玩家数量 
            _state            = _in.readInt();// 状态值
           
        }

        public void encode(Output _out)
        {
            _out.writeList(_out, sceneUser);
            _out.writeBoolean(start);
            _out.writeString(roomCheckId);
            _out.writeInt(leftChapterNums);
            _out.writeInt(createUserId);
            _out.writeT(_out,chapter);
            _out.writeInt(chapterMax);
            _out.writeBoolean(isHuiEr);
            _out.writeBoolean(isGangDaiPao);
            _out.writeBoolean(isDaiZiPai);
            _out.writeBoolean(isQiDuiFanBei);
            _out.writeBoolean(isZhuangJiaDi);
            _out.writeBoolean(isGangKaiFan);
            _out.writeInt(xuanPaoCount);
            _out.writeBoolean(isFangPao);
            _out.writeBoolean(currentChapterPao);
            _out.writeInt(_state);
            
        }
        public List<GameUserInfo> sceneUser
        {
            get { return _sceneUser; }
            set { _sceneUser = value; }
        }
        public int State
        {
            get { return this._state; }
            set { this._state = value; }
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
                return chapterMax;
            }

            set
            {
                chapterMax = value;
            }
        }

        public bool IsHuiEr
        {
            get
            {
                return isHuiEr;
            }

            set
            {
                isHuiEr = value;
            }
        }

        public bool IsGangDaiPao
        {
            get
            {
                return isGangDaiPao;
            }

            set
            {
                isGangDaiPao = value;
            }
        }

        public bool IsDaiZiPai
        {
            get
            {
                return isDaiZiPai;
            }

            set
            {
                isDaiZiPai = value;
            }
        }

        public bool IsQiDuiFanBei
        {
            get
            {
                return isQiDuiFanBei;
            }

            set
            {
                isQiDuiFanBei = value;
            }
        }

        public bool IsZhuangJiaDi
        {
            get
            {
                return isZhuangJiaDi;
            }

            set
            {
                isZhuangJiaDi = value;
            }
        }

        public bool IsGangKaiFan
        {
            get
            {
                return isGangKaiFan;
            }

            set
            {
                isGangKaiFan = value;
            }
        }

        public int XuanPaoCount
        {
            get
            {
                return xuanPaoCount;
            }

            set
            {
                xuanPaoCount = value;
            }
        }

        public bool IsFangPao
        {
            get
            {
                return isFangPao;
            }

            set
            {
                isFangPao = value;
            }
        }

        public bool CurrentChapterPao
        {
            get
            {
                return currentChapterPao;
            }

            set
            {
                currentChapterPao = value;
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
            return "GameRoomInfo [sceneUser=" + _sceneUser + ",start=" + _start + ",roomCheckId=" + _roomCheckId + ",leftChapterNums=" + _leftChapterNums + ",createUserId=" + _createUserId + ",chapter=" + _chapter + ", ]";
        }
    }

}