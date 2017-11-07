using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.flower
{
    //创建房间配置信息
    public class OptionEntryZJH : Message
    {
        public static int TYPE = 3;
        public static int ID = 2;

        public static OptionEntryZJH create(string key, string value)
        {
            OptionEntryZJH optionEntryZJH = new OptionEntryZJH();
            optionEntryZJH._key = key;
            optionEntryZJH._value = value;
            return optionEntryZJH;
        }
        protected string _key;
        protected string _value;

        public OptionEntryZJH()
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
        public string key
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
            return "OptionEntryZJH [" + ",key=" + _key + ",value=" + _value + ", ]";
        }
    }

}

