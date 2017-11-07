using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.game
{
    public class AllDingPao : Message
    {
        public static int TYPE = 1;
        public static int ID = 31;

        public static AllDingPao create(int[] userDingPaos)
        {
            AllDingPao gameChapterEnd = new AllDingPao();
            gameChapterEnd._userDingPaos = userDingPaos;
            return gameChapterEnd;
        }    
        protected int[] _userDingPaos;

        public void decode(Input _in)
        {
            _userDingPaos = _in.readIntArray();
        }


        public void encode(Output _out)
        {
            _out.writeIntArray(userDingPaos);

        }
        public int[] userDingPaos
        {
            get { return _userDingPaos; }
            set { _userDingPaos = value; }
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
            return "AllDingPao [userDingPaos=" + _userDingPaos + ", ]";
        }

    }
}
