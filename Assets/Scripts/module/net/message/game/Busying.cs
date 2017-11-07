
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.game
{
    public class Busying:Message
    {
        public static int TYPE = 1;
        public static int ID = 38;

        public static Busying creat(int Index,bool isbusy)
        {
            Busying busying = new Busying();
            busying._index = Index;
            busying._isbusy = isbusy;
            return busying;

        }
        protected int _index;
        protected bool _isbusy;
        public int index
        {
            get
            {
                return _index;
            }

            set
            {
                _index = value;
            }
        }
        public bool isbusy
        {
            get
            {
                return _isbusy;
            }

            set
            {
                _isbusy = value;
            }
        }

        public void decode(Input _in)
        {
            _index = _in.readInt();
            _isbusy = _in.readBoolean();
        }
        public void encode(Output _out)
        {
            _out.writeInt(_index);
            _out.writeBoolean(_isbusy);
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
            return "Busying[ ]";
        }
    }
}
