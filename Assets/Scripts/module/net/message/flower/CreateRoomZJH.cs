using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
using System.Collections.Generic;
namespace com.guojin.mj.net.message.flower
{
    //炸金花创建房间信息  发送
    public class CreateRoomZJH : Message
    {
        public static int TYPE = 3;
        public static int ID = 0;

        public static CreateRoomZJH create(List<OptionEntryZJH> options)
        {
            CreateRoomZJH createRoomZJH = new CreateRoomZJH();
            createRoomZJH._options = options;
            return createRoomZJH;
        }

        protected string _profile;
        protected List<OptionEntryZJH> _options = new List<OptionEntryZJH>();

        public CreateRoomZJH()
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
                    OptionEntryZJH optionsItem = new OptionEntryZJH();

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
        public string profile
        {
            get { return _profile; }
            set { _profile = value; }
        }
        public List<OptionEntryZJH> options
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
            return "CreateRoomZJH [" + ",options =" + _options + ", ]";
        }
    }
}