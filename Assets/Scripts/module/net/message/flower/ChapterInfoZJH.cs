using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
using System.Collections.Generic;

namespace com.guojin.mj.net.message.flower
{
    /// <summary>
    /// 炸金花一局游戏信息
    /// </summary>
    public class ChapterInfoZJH : Message
    {

        public static int TYPE = 3;
        public static int ID = 35;

        public static ChapterInfoZJH create(int currentIndex, int chapterNums, int chapterMax, bool[] isOut, bool[] isKan, bool[] isQi, bool isStart, int zhuangIndex)
        {
            ChapterInfoZJH RoomInfo = new ChapterInfoZJH();
            RoomInfo._currentIndex = currentIndex;
            RoomInfo._chapterNums = chapterNums;
            RoomInfo._chapterMax = chapterMax;
            RoomInfo._isOut = isOut;
            RoomInfo._isKan = isKan;
            RoomInfo._isQi = isQi;
            RoomInfo._isStart = isStart;
            RoomInfo._zhuangIndex = zhuangIndex;
            return RoomInfo;
        }
        protected bool _isStart;
        protected bool[] _isOut;
        protected bool[] _isQi;
        protected bool[] _isKan;
        protected int _currentIndex;
        protected int _chapterNums;
        protected int _chapterMax;
        protected int _zhuangIndex;

        public bool IsStart
        {
            get
            {
                return _isStart;
            }

            set
            {
                _isStart = value;
            }
        }
        public int ZhuangIndex
        {
            get
            {
                return _zhuangIndex;
            }

            set
            {
                _zhuangIndex = value;
            }
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
        public int ChapterNums
        {
            get
            {
                return _chapterNums;
            }

            set
            {
                _chapterNums = value;
            }
        }
        public int CurrentIndex
        {
            get
            {
                return _currentIndex;
            }

            set
            {
                _currentIndex = value;
            }
        }
        public bool[] IsOut
        {
            get
            {
                return _isOut;
            }

            set
            {
                _isOut = value;
            }
        }

        public bool[] IsQi
        {
            get
            {
                return _isQi;
            }

            set
            {
                _isQi = value;
            }
        }

        public bool[] IsKan
        {
            get
            {
                return _isKan;
            }

            set
            {
                _isKan = value;
            }
        }

        public ChapterInfoZJH()
        {

        }
        public void decode(Input _in)
        {
            _currentIndex = _in.readInt();
            _chapterNums = _in.readInt();
            _chapterMax = _in.readInt();
            IsOut = _in.readBooleanArray();
            IsKan = _in.readBooleanArray();
            IsQi = _in.readBooleanArray();
            _isStart = _in.readBoolean();
            _zhuangIndex = _in.readInt();
        }

        public void encode(Output _out)
        {
            _out.writeInt(_currentIndex);
            _out.writeInt(_chapterNums);
            _out.writeInt(_chapterMax);
            _out.writeBooleanArray(IsOut);
            _out.writeBooleanArray(IsKan);
            _out.writeBooleanArray(IsQi);
            _out.writeBoolean(_isStart);
            _out.writeInt(_zhuangIndex);
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
            return "ChapterInfoZJH [" + "currentIndex" + _currentIndex + "chapterNums" + _chapterNums + "chapterMax" + _chapterMax + "isOut" + IsOut + "isKan" + IsKan + "isQi" + IsQi + "isStart" + _isStart + "zhuangIndex" + _zhuangIndex + ", ]";
        }
    }
}

