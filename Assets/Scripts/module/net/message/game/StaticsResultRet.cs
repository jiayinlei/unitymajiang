using com.guojin.core.io;
using com.guojin.core.io.message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.guojin.mj.net.message.game
{
    /// <summary>
    /// 总结算
    /// </summary>
    public class StaticsResultRet:Message
    {
        public static int TYPE = 1;
        public static int ID = 26;


        protected int _locationIndex0;
		protected int _zimo0;
		protected int _fangpao0;
		protected int _jiepao0;
		protected int _angang0;
		protected int _minggang0;
		protected int _score0;
		
		protected int _locationIndex1;
		protected int _zimo1;
		protected int _fangpao1;
		protected int _jiepao1;
		protected int _angang1;
		protected int _minggang1;
		protected int _score1;
		
		protected int _locationIndex2;
		protected int _zimo2;
		protected int _fangpao2;
		protected int _jiepao2;
		protected int _angang2;
		protected int _minggang2;
		protected int _score2;
		
		protected int _locationIndex3;
		protected int _zimo3;
		protected int _fangpao3;
		protected int _jiepao3;
		protected int _angang3;
		protected int _minggang3;
		protected int _score3;
        protected int _minutime;

        public StaticsResultRet()
        {

        }
        public static StaticsResultRet create(int _locationIndex0, int _zimo0, int _fangpao0, int _jiepao0, int _angang0, int _minggang0, int _score0, int _locationIndex1, int _zimo1, int _fangpao1, int _jiepao1, int _angang1, int _minggang1, int _score1, int _locationIndex2, int _zimo2, int _fangpao2, int _jiepao2, int _angang2, int _minggang2, int _score2, int _locationIndex3, int _zimo3, int _fangpao3, int _jiepao3, int _angang3, int _minggang3, int _score3,int _minutime)
        {
            StaticsResultRet staticsResultRet = new StaticsResultRet();
            staticsResultRet._locationIndex0 = _locationIndex0;
            staticsResultRet._zimo0 = _zimo0;
            staticsResultRet._fangpao0 = _fangpao0;
            staticsResultRet._jiepao0 = _jiepao0;
            staticsResultRet._angang0 = _angang0;
            staticsResultRet._minggang0 = _minggang0;
            staticsResultRet._score0 = _score0;
            staticsResultRet._locationIndex1 = _locationIndex1;
            staticsResultRet._zimo1 = _zimo1;
            staticsResultRet._fangpao1 = _fangpao1;
            staticsResultRet._jiepao1 = _jiepao1;
            staticsResultRet._angang1 = _angang1;
            staticsResultRet._minggang1 = _minggang1;
            staticsResultRet._score1 = _score1;
            staticsResultRet._locationIndex2 = _locationIndex2;
            staticsResultRet._zimo2 = _zimo2;
            staticsResultRet._fangpao2 = _fangpao2;
            staticsResultRet._jiepao2 = _jiepao2;
            staticsResultRet._angang2 = _angang2;
            staticsResultRet._minggang2 = _minggang2;
            staticsResultRet._score2 = _score2;
            staticsResultRet._locationIndex3 = _locationIndex3;
            staticsResultRet._zimo3 = _zimo3;
            staticsResultRet._fangpao3 = _fangpao3;
            staticsResultRet._jiepao3 = _jiepao3;
            staticsResultRet._angang3 = _angang3;
            staticsResultRet._minggang3 = _minggang3;
            staticsResultRet._score3 = _score3;
            staticsResultRet._minutime = _minutime;
            return staticsResultRet;
        }

        public void decode(Input in0)
        {
            _locationIndex0 = in0.readInt();
            _zimo0 = in0.readInt();
            _fangpao0 = in0.readInt();
            _jiepao0 = in0.readInt();
            _angang0 = in0.readInt();
            _minggang0 = in0.readInt();
            _score0 = in0.readInt();

            _locationIndex1 = in0.readInt();
            _zimo1 = in0.readInt();
            _fangpao1 = in0.readInt();
            _jiepao1 = in0.readInt();
            _angang1 = in0.readInt();
            _minggang1 = in0.readInt();
            _score1 = in0.readInt();

            _locationIndex2 = in0.readInt();
            _zimo2 = in0.readInt();
            _fangpao2 = in0.readInt();
            _jiepao2 = in0.readInt();
            _angang2 = in0.readInt();
            _minggang2 = in0.readInt();
            _score2 = in0.readInt();

            _locationIndex3 = in0.readInt();
            _zimo3 = in0.readInt();
            _fangpao3 = in0.readInt();
            _jiepao3 = in0.readInt();
            _angang3 = in0.readInt();
            _minggang3 = in0.readInt();
            _score3 = in0.readInt();
            _minutime = in0.readInt();
        }

        public void encode(Output _out)
        {
            _out.writeInt(_locationIndex0);
            _out.writeInt(_zimo0);
            _out.writeInt(_fangpao0);
            _out.writeInt(_jiepao0);
            _out.writeInt(_angang0);
            _out.writeInt(_minggang0);
            _out.writeInt(_score0);

            _out.writeInt(_locationIndex1);
            _out.writeInt(_zimo1);
            _out.writeInt(_fangpao1);
            _out.writeInt(_jiepao1);
            _out.writeInt(_angang1);
            _out.writeInt(_minggang1);
            _out.writeInt(_score1);

            _out.writeInt(_locationIndex2);
            _out.writeInt(_zimo2);
            _out.writeInt(_fangpao2);
            _out.writeInt(_jiepao2);
            _out.writeInt(_angang2);
            _out.writeInt(_minggang2);
            _out.writeInt(_score2);

            _out.writeInt(_locationIndex3);
            _out.writeInt(_zimo3);
            _out.writeInt(_fangpao3);
            _out.writeInt(_jiepao3);
            _out.writeInt(_angang3);
            _out.writeInt(_minggang3);
            _out.writeInt(_score3);
            _out.writeInt(_minutime);            

        }
        public int locationIndex0
        {
            get { return _locationIndex0; }
            set { _locationIndex0 = value; }
        }
        public int minutime
        {
            get { return _minutime; }
            set { _minutime = value; }
        }
        public int zimo0
        {
            get { return _zimo0; }
            set { _zimo0 = value; }
        }
        public int fangpao0
        {
            get { return _fangpao0; }
            set { _fangpao0 = value; }
        }
        public int jiepao0
        {
            get { return _jiepao0; }
            set { _jiepao0 = value; }
        }
        public int angang0
        {
            get { return _angang0; }
            set { _angang0 = value; }
        }
        public int minggang0
        {
            get { return _minggang0; }
            set { _minggang0 = value; }
        }
        public int score0
        {
            get { return _score0; }
            set { _score0 = value; }
        }
        public int locationIndex1
        {
            get { return _locationIndex1; }
            set { _locationIndex1 = value; }
        }
        public int zimo1
        {
            get { return _zimo1; }
            set { _zimo1 = value; }
        }
        public int fangpao1
        {
            get { return _fangpao1; }
            set { _fangpao1 = value; }
        }
        public int jiepao1
        {
            get { return _jiepao1; }
            set { _jiepao1 = value; }
        }
        public int angang1
        {
            get { return _angang1; }
            set { _angang1 = value; }
        }
        public int minggang1
        {
            get { return _minggang1; }
            set { _minggang1 = value; }
        }
        public int score1
        {
            get { return _score1; }
            set { _score1 = value; }
        }
        public int locationIndex2
        {
            get { return _locationIndex2; }
            set { _locationIndex2 = value; }
        }
        public int zimo2
        {
            get { return _zimo2; }
            set { _zimo2 = value; }
        }
        public int fangpao2
        {
            get { return _fangpao2; }
            set { _fangpao2 = value; }
        }
        public int jiepao2
        {
            get { return _jiepao2; }
            set { _jiepao2 = value; }
        }
        public int angang2
        {
            get { return _angang2; }
            set { _angang2 = value; }
        }
        public int minggang2
        {
            get { return _minggang2; }
            set { _minggang2 = value; }
        }
        public int score2
        {
            get { return _score2; }
            set { _score2 = value; }
        }
        public int locationIndex3
        {
            get { return _locationIndex3; }
            set { _locationIndex3 = value; }
        }
        public int zimo3
        {
            get { return _zimo3; }
            set { _zimo3 = value; }
        }
        public int fangpao3
        {
            get { return _fangpao3; }
            set { _fangpao3 = value; }
        }
        public int jiepao3
        {
            get { return _jiepao3; }
            set { _jiepao3 = value; }
        }
        public int angang3
        {
            get { return _angang3; }
            set { _angang3 = value; }
        }
        public int minggang3
        {
            get { return _minggang3; }
            set { _minggang3 = value; }
        }
        public int score3
        {
            get { return _score3; }
            set { _score3 = value; }
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
            return "OperationCPGHRet [locationIndex0=" + _locationIndex0 + ",zimo0=" + _zimo0 + ",fangpao0=" + _fangpao0 + ",jiepao0="
                + _jiepao0 + ",angang0=" + _angang0 + ",minggang0=" + _minggang0 + ",score0=" + _score0 + 
                "locationIndex1=" + _locationIndex1 + ",zimo1=" + _zimo1 + ",fangpao1=" + _fangpao1 + ",jiepao1="
                + _jiepao1 + ",angang1=" + _angang1 + ",minggang1=" + _minggang1 + ",score1=" + _score1 +
                "locationIndex2=" + _locationIndex2 + ",zimo2=" + _zimo2 + ",fangpao2=" + _fangpao2 + ",jiepao2="
                + _jiepao2 + ",angang2=" + _angang2 + ",minggang2=" + _minggang2 + ",score2=" + _score2 +
                "locationIndex3=" + _locationIndex3 + ",zimo3=" + _zimo3 + ",fangpao3=" + _fangpao3 + ",jiepao3="
                + _jiepao3 + ",angang3=" + _angang3 + ",minggang3=" + _minggang3 + ",score3=" + _score3 + ", ]";
        }
    }
}
