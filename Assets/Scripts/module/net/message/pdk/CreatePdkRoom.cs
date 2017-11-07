
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
using System.Collections.Generic;
namespace com.guojin.mj.net.message.login
{
    //创建房间
    public class CreatePdkRoom : Message
    {

        public static int TYPE = 4;
        public static int ID = 0;

        public static CreatePdkRoom create(List<OptionEntry> options)
        {
            CreatePdkRoom createRoom = new CreatePdkRoom();
            createRoom._options = options;
            return createRoom;
        }
        protected List<OptionEntry> _options;

        public CreatePdkRoom()
        {

        }
        public void decode(Input _in)
        {
            var optionsLen = _in.readInt();

            if (optionsLen == -1)
            {
                _options = null;
            }
            else
            {

                for (int optionsI = 0; optionsI < optionsLen; optionsI++)
                {
                    OptionEntry optionsItem = new OptionEntry();

                    optionsItem.decode(_in);
                    _options[optionsI] = optionsItem;

                }
            }
        }

        public void encode(Output _out)
        {
            if (options == null)
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
            return "CreateRoom [profile="  + ",options=" + _options + ", ]";
        }
    }
}

