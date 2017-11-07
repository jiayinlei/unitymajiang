
using com.guojin.core.io.message;
using com.guojin.core.io;
using System;
using System.Collections.Generic;

namespace com.guojin.mj.net.message.game
{

    //牌局结束
    public class GameFanResult : Message
    {

        public static int TYPE = 1;
        public static int ID = 5;

        public static GameFanResult create(int queTou,int[] shunZi, int[] keZi, int[] shouPai, int[] huiErBian, int[] anGang, int[] xiaoMingGang, int[] daMingGang, int[] peng, int[] chi,string baseFanType,string fanString,int fan,string userName,int score,int guaFengXiaYu)
        {
            GameFanResult gameFanResult = new GameFanResult();
            gameFanResult._queTou = queTou;
            gameFanResult._shunZi = shunZi;
            gameFanResult._keZi = keZi;
            gameFanResult._shouPai = shouPai;
            gameFanResult._huiErBian = huiErBian;
            gameFanResult._anGang = anGang;
            gameFanResult._xiaoMingGang = xiaoMingGang;
            gameFanResult._daMingGang = daMingGang;
            gameFanResult._peng = peng;
            gameFanResult._chi = chi;
            gameFanResult._baseFanType = baseFanType;
            gameFanResult._fanString = fanString;
            gameFanResult._fan = fan;
            gameFanResult._userName = userName;
            gameFanResult._score = score;
            gameFanResult._guaFengXiaYu = guaFengXiaYu;
            return gameFanResult;
        }

        protected int _queTou;
        protected int[] _shunZi;
        protected int[] _keZi;
        protected int[] _shouPai;

        protected int[] _huiErBian;

        //已经显示的暗杠  如果是自己的则全部显示  不是自己的且如果还不能显示 那么传递-1
        protected int[] _anGang;
        protected int[] _xiaoMingGang;
        protected int[] _daMingGang;
        protected int[] _peng;

        //3个一组

        protected int[] _chi;
        protected string _baseFanType;
        protected string _fanString;
        protected int _fan;
        protected string _userName;

        //改变值
        protected int _score;
        protected int _guaFengXiaYu;

        public GameFanResult()
        {

        }
        public void decode(Input _in)
        {
            _queTou = _in.readInt();
            _shunZi = _in.readIntArray();
            _keZi = _in.readIntArray();
            _shouPai = _in.readIntArray();
            _huiErBian = _in.readIntArray();
            _anGang = _in.readIntArray();
            _xiaoMingGang = _in.readIntArray();
            _daMingGang = _in.readIntArray();
            _peng = _in.readIntArray();
            _chi = _in.readIntArray();
            _baseFanType = _in.readString();
            _fanString = _in.readString();
            _fan = _in.readInt();
            _userName = _in.readString();
            _score = _in.readInt();
            _guaFengXiaYu = _in.readInt();
        }

        public void encode(Output _out)
        {
            _out.writeInt(queTou);
			_out.writeIntArray(shunZi);
			_out.writeIntArray(keZi);
			_out.writeIntArray(shouPai);
            _out.writeIntArray(huiErBian);
            _out.writeIntArray(anGang);
            _out.writeIntArray(xiaoMingGang);
            _out.writeIntArray(daMingGang);
            _out.writeIntArray(peng);
            _out.writeIntArray(chi);
            _out.writeString(baseFanType);
            _out.writeString(fanString);
            _out.writeInt(fan);
            _out.writeString(userName);
            _out.writeInt(score);
            _out.writeInt(guaFengXiaYu);
        }
        public int queTou
        {
            get { return _queTou; }
            set { _queTou = value; }
        } 
        public int[] shunZi
        {
            get { return _shunZi; }
            set { _shunZi = value; }
        }
        public int[] keZi
        {
            get { return _keZi; }
            set { _keZi = value; }
        }
        public int[] shouPai
        {
            get { return _shouPai; }
            set { _shouPai = value; }
        }
        public int[] huiErBian
        {
            get { return _huiErBian; }
            set { _huiErBian = value; }
        }
        public int[] anGang
        {
            get { return _anGang; }
            set { _anGang = value; }
        }
        public int[] xiaoMingGang
        {
            get { return _xiaoMingGang; }
            set { _xiaoMingGang = value; }
        }
        public int[] daMingGang
        {
            get { return _daMingGang; }
            set { _daMingGang = value; }
        }
        public int[] peng
        {
            get { return _peng; }
            set { _peng = value; }
        }
        public int[] chi
        {
            get { return _chi; }
            set { _chi = value; }
        }
        public string baseFanType
        {
            get { return _baseFanType; }
            set { _baseFanType = value; }
        }
        public string fanString
        {
            get { return _fanString; }
            set { _fanString = value; }
        }
        public int fan
        {
            get { return _fan; }
            set { _fan = value; }
        }
        public string userName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        public int score
        {
            get { return _score; }
            set { _score = value; }
        }
        public int guaFengXiaYu
        {
            get { return _guaFengXiaYu; }
            set { _guaFengXiaYu = value; }
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
            return "GameFanResult [queTou=" + _queTou + ",shunZi=" + _shunZi + ",keZi=" + _keZi + ",shouPai=" + _shouPai + ",huiErBian=" + _huiErBian + ",anGang=" + _anGang + ",xiaoMingGang=" + _xiaoMingGang + ",daMingGang=" + _daMingGang + ",peng=" + _peng + ",chi=" + _chi + ",baseFanType=" + _baseFanType + ",fanString=" + _fanString + ",fan=" + _fan + ",userName=" + _userName + ",score=" + _score + ",guaFengXiaYu=" + _guaFengXiaYu + ", ]";
        }
    }
}