

using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
using System.Collections.Generic;
namespace com.guojin.mj.net.message.game
{
    //牌局结束
    /// <summary>
    /// 牌局结束
    /// </summary>
    public class GameChapterEnd : Message
    {
        public static int TYPE = 1;
        public static int ID = 0;

        public static GameChapterEnd create( int huPaiIndex,int fangPaoIndex,int zaMaType,int [] zaMaPai,int zaMaFan, List<GameFanResult> fanResults)
        {
            GameChapterEnd gameChapterEnd = new GameChapterEnd();
            gameChapterEnd._huPaiIndex = huPaiIndex;
            gameChapterEnd._fangPaoIndex = fangPaoIndex;
            gameChapterEnd._zaMaType = zaMaType;
            gameChapterEnd._zaMaPai = zaMaPai;
            gameChapterEnd._zaMaFan = zaMaFan;
            gameChapterEnd._fanResults = fanResults;
            return gameChapterEnd;
        }

        protected int _huPaiIndex;
        protected int _fangPaoIndex;
        protected int _zaMaType;
        protected int[] _zaMaPai;
        protected int _zaMaFan;
        protected List<GameFanResult> _fanResults = new List<GameFanResult>();


        public GameChapterEnd()
        {

        }
        public void decode(Input _in)
        {
            _huPaiIndex = _in.readInt();
            _fangPaoIndex = _in.readInt();
            _zaMaType = _in.readInt();
            _zaMaPai = _in.readIntArray();
            _zaMaFan = _in.readInt();

            int fanResultsLen = _in.readInt();
            if (fanResultsLen==-1)
            {
                _fanResults = null;
            }
            else
            {
                for (int fanResultsI = 0; fanResultsI < fanResultsLen; fanResultsI++)
                {
                    GameFanResult fanResultsItem = new GameFanResult();
                    fanResultsItem.decode(_in);
                    this._fanResults.Add(fanResultsItem);

                }
            }
        }

        public void encode(Output _out)
        {
           _out.writeInt(huPaiIndex);
			_out.writeInt(fangPaoIndex);
			_out.writeInt(zaMaType);
			_out.writeIntArray(zaMaPai);
			_out.writeInt(zaMaFan);

            if (_fanResults==null)
            {
                _out.writeInt(-1);
            }
            else
            {
                int fanResultsLen = fanResults.Count; ;
                _out.writeInt(fanResultsLen);
                for (int fanResultsI = 0; fanResultsI < fanResultsLen; fanResultsI++)
                {
                    fanResults[fanResultsI].encode(_out);
                }
            }
        }
        public int huPaiIndex
        {
            get { return _huPaiIndex; }
            set { _huPaiIndex = value; }
        }
        public int fangPaoIndex
        {
            get { return _fangPaoIndex; }
            set { _fangPaoIndex = value; }
        }
        public int zaMaType
        {
            get { return _zaMaType; }
            set { _zaMaType = value; }
        }
        public int[] zaMaPai
        {
            get { return _zaMaPai; }
            set { _zaMaPai = value; }
        }
        public int zaMaFan
        {
            get { return _zaMaFan; }
            set { _zaMaFan = value; }
        }
        public List<GameFanResult> fanResults
        {
            get { return _fanResults; }
            set { _fanResults = value; }
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
            return "GameChapterEnd [huPaiIndex=" + _huPaiIndex + ",fangPaoIndex=" + _fangPaoIndex + ",zaMaType=" + _zaMaType + ",zaMaPai=" + _zaMaPai + ",zaMaFan=" + _zaMaFan + ",fanResults=" + _fanResults + ", ]";
        }
    }
}