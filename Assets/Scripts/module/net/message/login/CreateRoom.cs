
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
using System.Collections.Generic;
namespace com.guojin.mj.net.message.login
{
    //创建房间
    public class CreateRoom : Message
    {

        public static int TYPE = 7;
        public static int ID = 0;

        public  static CreateRoom create(string profile,List<OptionEntry> options )
        {
            CreateRoom createRoom = new CreateRoom();
            createRoom._profile = profile;
            createRoom._options = options;
            return createRoom;
        }

        protected string _profile;
        protected List<OptionEntry> _options;

        public CreateRoom()
        {

        }
        public void decode(Input _in)
        {
            _profile = _in.readString();
            var  optionsLen = _in.readInt();

            if (optionsLen==-1)
            {
                _options = null;
            }
            else
            {
              
                for (int optionsI = 0; optionsI < optionsLen; optionsI++)
                {
                    OptionEntry optionsItem = new OptionEntry();

                    optionsItem.decode(_in);
                    _options[optionsI] =optionsItem;

                }
            }
        }

        public void encode(Output _out)
        {
            _out.writeString(profile);

            if (options==null)
            {
                _out.writeInt(-1);
            }
            else
            {
                int optionsLen = options.Count;
                _out.writeInt(optionsLen);
                for (int optionsI = 0; optionsI < optionsLen; optionsI++)
                {
                    options[optionsI].encode(_out);
                }
            }
        }
        public string profile
        {
            get { return _profile; }
            set { _profile = value; }
        }
        public List<OptionEntry> options
        {
            get { return _options; }
            set { _options = value; }
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
            return "CreateRoom [profile=" + _profile + ",options=" + _options + ", ]";
        }
    }
}

