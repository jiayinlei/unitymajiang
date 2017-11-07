
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.login
{
    public class Pay : Message
    {
        public static int TYPE = 7;
        public static int ID = 13;

        public static Pay create(int gold)
        {
            Pay pay = new Pay();
            pay._gold = gold;
            return pay;
        }

        public int _gold;

        public Pay()
        {

        }
        public void decode(Input _in)
        {
            _gold = _in.readInt();
        }

        public void encode(Output _out)
        {
           _out.writeInt(gold);
        }

        public int gold
        {
            get { return _gold; }
            set { _gold = value; }
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
            return "Pay [gold=" + _gold + ", ]";
        }
    }
}