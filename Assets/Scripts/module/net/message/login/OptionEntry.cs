
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.login
{
    public class OptionEntry : Message
    {
        public static int TYPE = 7;
        public static int ID = 12;

        public static OptionEntry create(string   key ,string value )
        {
            OptionEntry optionEntry = new OptionEntry();
            optionEntry._key = key;
            optionEntry._value = value;
            return optionEntry;
        }
        protected string   _key;//键，操作的类型
        protected string _value;//值，操作类型的具体值

        public OptionEntry()
        {

        }
        public void decode(Input _in)
        {
            _key = _in.readString();
            _value = _in.readString();
        }

        public void encode(Output _out)
        {
            _out.writeString(key);
			_out.writeString(value);
        }
        public string   key
        {
            get { return _key; }
            set { _key = value; }
        }
        public string value
        {
            get { return _value; }
            set { _value = value; }
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
            return "OptionEntry [key=" + _key + ",value=" + _value + ", ]";
        }
    }
}