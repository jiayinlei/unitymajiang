using com.guojin.core.io;
using com.guojin.core.io.message;

namespace com.guojin.mj.net.message.login
{


    /**
     * ping
     * 
     */
    public class Question : Message
    {
        public static int TYPE = 7;
        public static int ID = 37;

        public static Question create(string content)
        {
            Question question = new Question();
            question._content = content;
            return question;
        }

        protected string _content;

        public Question()
        {
        }

        public void decode(Input _in)
        {
            _content = _in.readString();
        }

        public void encode(Output _out)
        {
            _out.writeString(_content);
        }


        public string content
        {
            get { return _content; }
            set { _content = value; }
        }

        public string toString()
        {
            return "Question [content=" + _content + ", ]";
        }

        public int getMessageType()
        {
            return TYPE;
        }

        public int getMessageId()
        {
            return ID;
        }
    }
}