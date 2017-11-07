
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.login
{
    //通知
    public class Notice : Message
    {
        public static int TYPE = 7;
        public static int ID = 11;

        public static Notice create(string key ,string[] args ,int type ,bool reboot)
        {
            Notice notice = new Notice();
            notice._key = key;
            notice._args = args;
            notice._type = type;
            notice._reboot = reboot;
            return notice;
        }

        //语言文件的key,或者内容字符串
        protected string _key;
        protected string[] _args;

        //0 横屏实时通知，1悬停错误提示
        protected int _type;

        //是否需要重新启动游戏
        protected bool _reboot;

        public Notice()
        {

        }
        public void decode(Input _in)
        {
            _key = _in.readString();
            _args = _in.readStringArray();
            _type = _in.readInt();
            _reboot = _in.readBoolean();
        }

        public void encode(Output _out)
        {
            _out.writeString(key);
            _out.writeStringArray(args);
            _out.writeInt(type);
            _out.writeBoolean(reboot);
        }

        public string key
        {
            get { return _key; }
            set { _key = value; }
        }
        public string [] args
        {
            get { return _args; }
            set { _args = value; }
        }
        public int type
        {
            get { return _type; }
            set { _type = value; }
        }
        public bool reboot
        {
            get { return _reboot; }
            set { _reboot = value; }
        }

        public int getMessageId()
        {
            //throw new NotImplementedException();
            return ID;

        }

        public int getMessageType()
        {
            //throw new NotImplementedException();
            return TYPE;
        }

        public string toString()
        {
            //throw new NotImplementedException();
            return "Notice [key=" + _key + ",args=" + _args + ",type=" + _type + ",reboot=" + _reboot + ", ]";
        }
    }
}