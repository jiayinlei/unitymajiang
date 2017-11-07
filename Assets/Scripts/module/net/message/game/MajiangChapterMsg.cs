using System;
using com.guojin.core.io.message;
using com.guojin.core.io;
using com.guojin.mj.net.message.game;
using System.Collections.Generic;
namespace com.guojin.mj.net.message.game
{
    //一局麻将的信息
    public class MajiangChapterMsg : Message
    {
        public static int TYPE = 1;
        public static int ID = 9;

        public static MajiangChapterMsg create(int freeLength,int baoliuLength, int[] huiEr,string bianType,int bianSource,List<UserPlaceMsg> userPlace,int currentIndex,int chapterNums,int chapterNumsMax,int quanIndex,int zhuangIndex,OperationCPGH optCpgh,OperationFaPai optFaPai,OperationOut optOut,SyncOptTime syncOptTime,GameChapterEnd gameChapterEnd, TingPai tingPai, bool currentPaoFlage)
        {
            MajiangChapterMsg majiangChapterMsg = new MajiangChapterMsg();
            majiangChapterMsg._freeLength = freeLength;
            majiangChapterMsg._baoliuLength = baoliuLength;
            majiangChapterMsg._huiEr = huiEr;
            majiangChapterMsg._bianType = bianType;
            majiangChapterMsg._bianSource = bianSource;
            majiangChapterMsg._userPlace = userPlace;
            majiangChapterMsg._currentIndex = currentIndex;
            majiangChapterMsg._chapterNums = chapterNums;
            majiangChapterMsg._chapterNumsMax = chapterNumsMax;
            majiangChapterMsg._quanIndex = quanIndex;
            majiangChapterMsg._zhuangIndex = zhuangIndex;
            majiangChapterMsg._optCpgh = optCpgh;
            majiangChapterMsg._optFaPai = optFaPai;
            majiangChapterMsg._optOut = optOut;
            majiangChapterMsg._syncOptTime = syncOptTime;
            majiangChapterMsg._gameChapterEnd = gameChapterEnd;
            majiangChapterMsg._tingPai = tingPai;
            majiangChapterMsg._currentPaoFlage = currentPaoFlage;
            return majiangChapterMsg;
        }
        //剩余张数
        protected int _freeLength;
        //保留张数
        protected int _baoliuLength;
        //会儿牌
        protected int [] _huiEr;
        //变化类型
        protected string _bianType;
        //变化类型
        protected int _bianSource;
        protected List<UserPlaceMsg> _userPlace=new List<UserPlaceMsg>();
        //当前操作用户
        protected int _currentIndex;
        //局数，0开始
        protected int _chapterNums;
        //局数，0开始
        protected int _chapterNumsMax;
        //圈index 0 东 1南 2西 3北 逆时针顺序
        protected int _quanIndex;
        //庄index 0 东 1南 2西 3北 逆时针顺序
        protected int _zhuangIndex;
        protected OperationCPGH _optCpgh;
        protected OperationFaPai _optFaPai;
        protected OperationOut _optOut;
        protected SyncOptTime _syncOptTime;
        protected GameChapterEnd _gameChapterEnd;
        protected TingPai _tingPai;
        protected bool _currentPaoFlage;
        public MajiangChapterMsg()
        {

        }
        public void decode(Input _in)
        {
            _freeLength = _in.readInt();
            _baoliuLength = _in.readInt();
            _huiEr = _in.readIntArray();
            _bianType = _in.readString();
            _bianSource = _in.readInt();

            int userPlaceLen = _in.readInt();
            if (userPlaceLen==-1)
            {
                _userPlace = null;
            }
            else
            {
                for (int userPlaceI = 0; userPlaceI < userPlaceLen; userPlaceI++)
                {
                    UserPlaceMsg userPlaceItem = new UserPlaceMsg();
                    userPlaceItem.decode(_in);
                    //  _userPlace[userPlaceI] = (userPlaceItem);
                    this._userPlace.Add(userPlaceItem);
                }
            }
            _currentIndex = _in.readInt();
            _chapterNums = _in.readInt();
            _chapterNumsMax = _in.readInt();
            _quanIndex = _in.readInt();
            _zhuangIndex = _in.readInt();

            bool optCpghIsNotNull = _in.readBoolean();
            if (optCpghIsNotNull)
            {
                _optCpgh = new OperationCPGH();
                _optCpgh.decode(_in);
            }
            else
            {
                _optCpgh = null;
            }

            bool optFaPaiIsNotNull = _in.readBoolean();
            if (optFaPaiIsNotNull)
            {
                _optFaPai = new OperationFaPai();
                _optFaPai.decode(_in);
            }
            else
            {
                _optFaPai = null;
            }

            bool optOutIsNotNull = _in.readBoolean();
            if (optOutIsNotNull)
            {
                _optOut = new OperationOut();
                _optOut.decode(_in);
            }
            else
            {
                _optOut = null;
            }

            bool syncOptTimeIsNotNull = _in.readBoolean();
            if (syncOptTimeIsNotNull)
            {
                _syncOptTime = new SyncOptTime();
                _syncOptTime.decode(_in);
            }
            else
            {
                _syncOptTime = null;
            }

            bool gameChapterEndIsNotNull = _in.readBoolean();
            if (gameChapterEndIsNotNull)
            {
                _gameChapterEnd = new GameChapterEnd();
                _gameChapterEnd.decode(_in);
            }
            else
            {
                _gameChapterEnd = null;
            }

            bool tingPaiIsNotNull = _in.readBoolean();

            if (tingPaiIsNotNull)
            {
                _tingPai = new TingPai();
                _tingPai.decode(_in);
            }
            else
            {
                _tingPai = null;
            }

            _currentPaoFlage = _in.readBoolean();


        }

        public void encode(Output _out)
        {
            _out.writeInt(freeLength);
            _out.writeInt(baoliuLength);
            _out.writeIntArray(huiEr);
            _out.writeString(bianType);
            _out.writeInt(bianSource);

            if (userPlace==null)
            {
                _out.writeInt(-1);
            }
            else
            {
                int userPlaceLen = userPlace.Count;
                _out.writeInt(userPlaceLen);
                for (int userPlaceI = 0; userPlaceI < userPlaceLen; userPlaceI++)
                {
                    userPlace[userPlaceI].encode(_out);
                }
            }


            _out.writeInt(currentIndex);
			_out.writeInt(chapterNums);
			_out.writeInt(chapterNumsMax);
			_out.writeInt(quanIndex);
			_out.writeInt(zhuangIndex);

            if (optCpgh==null)
            {
                _out.writeBoolean(false);
            }
            else
            {
                _out.writeBoolean(true);
                optFaPai.encode(_out);
            }

            if (optFaPai==null)
            {
                _out.writeBoolean(false);
            }
            else
            {
                _out.writeBoolean(true);
                optFaPai.encode(_out);
            }

            if (optOut==null)
            {
                _out.writeBoolean(false);
            }
            else
            {
                _out.writeBoolean(true);
                optOut.encode(_out);
            }

            if (syncOptTime==null)
            {
                _out.writeBoolean(false);
            }
            else
            {
                _out.writeBoolean(true);
                _syncOptTime.encode(_out);
            }

            if (gameChapterEnd==null)
            {
                _out.writeBoolean(false);
            }
            else
            {
                _out.writeBoolean(true);
                gameChapterEnd.encode(_out);
            }

            if (tingPai==null)
            {
                _out.writeBoolean(false);
            }
            else
            {
                _out.writeBoolean(true);
                tingPai.encode(_out);
            }
        }

        public int freeLength
        {
            get { return _freeLength; }
            set { _freeLength = value; }
        }
        public int baoliuLength
        {
            get { return _baoliuLength; }
            set { _baoliuLength = value; }
        }
        public int[] huiEr
        {
            get { return _huiEr; }
            set { _huiEr = value; }
        }
        public string bianType
        {
            get { return _bianType; }
            set { _bianType = value; }
        }
        public int bianSource
        {
            get { return _bianSource ; }
            set { _bianSource = value; }
        }
        public List<UserPlaceMsg> userPlace
        {
            get { return _userPlace; }
            set { _userPlace = value; }
        }
        public int currentIndex
        {
            get { return _currentIndex; }
            set { _currentIndex = value; }
        }
        public int chapterNums
        {
            get { return _chapterNums; }
            set { _chapterNums = value; }
        }
        public int chapterNumsMax
        {
            get { return _chapterNumsMax; }
            set { _chapterNumsMax = value; }
        }
        public int quanIndex
        {
            get { return _quanIndex; }
            set { _quanIndex = value; }
        }
        public int zhuangIndex
        {
            get { return _zhuangIndex; }
            set { _zhuangIndex = value; }
        }
        public OperationCPGH optCpgh
        {
            get { return _optCpgh; }
            set { _optCpgh = value; }
        }
        public OperationFaPai optFaPai
        {
            get { return _optFaPai; }
            set { _optFaPai = value; }
        }
        public OperationOut optOut
        {
            get { return _optOut; }
            set { _optOut = value; }
        }
        public SyncOptTime syncOptTime
        {
            get { return _syncOptTime; }
            set { _syncOptTime = value; }
        }
        public GameChapterEnd gameChapterEnd
        {
            get { return _gameChapterEnd; }
            set { _gameChapterEnd = value; }
        }
        public TingPai tingPai
        {
            get { return _tingPai; }
            set { _tingPai = value; }
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
            return "MajiangChapterMsg [freeLength=" + _freeLength + ",baoliuLength=" + _baoliuLength + ",huiEr=" + _huiEr + ",bianType=" + _bianType + ",bianSource=" + _bianSource + ",userPlace=" + _userPlace + ",currentIndex=" + _currentIndex + ",chapterNums=" + _chapterNums + ",chapterNumsMax=" + _chapterNumsMax + ",quanIndex=" + _quanIndex + ",zhuangIndex=" + _zhuangIndex + ",optCpgh=" + _optCpgh + ",optFaPai=" + _optFaPai + ",optOut=" + _optOut + ",syncOptTime=" + _syncOptTime + ",gameChapterEnd=" + _gameChapterEnd + ",tingPai=" + _tingPai + ", ]";
        }
    }
}