using System;

using com.guojin.core.io;
using com.guojin.core.io.message;

namespace com.guojin.mj.net.message.game
{
    //通知用户出牌
    public class TingPai : Message
    {
        public static int TYPE = 1;
        public static int ID = 18;

        public static TingPai create(int [] pais,int []zhang)
        {
            TingPai tingPai = new TingPai();
            tingPai._pais = pais;
            tingPai._zhang = zhang;
            return tingPai;
        }
        protected int[] _pais;
        protected int[] _zhang;
        public TingPai()
        {

        }
        public void decode(Input _in)
        {
            _pais = _in.readIntArray();
            _zhang = _in.readIntArray();
        }

        public void encode(Output _out)
        {
            _out.writeIntArray(pais);
            _out.writeIntArray(zhang);
        }
        public int[] pais
        {
            get { return _pais; }
            set { _pais = value; }
        }
        public int[] zhang
        {
            get { return _zhang; }
            set { _zhang = value; }
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
            return "TingPai [pais=" + _pais + "zhang = " + _zhang + ", ]";
        }
    }
}