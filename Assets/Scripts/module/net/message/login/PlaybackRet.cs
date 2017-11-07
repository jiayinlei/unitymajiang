
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.login
{
    public class PlaybackRet : Message
    {
        public static int TYPE = 7;
        public static int ID = 39;

        public static PlaybackRet create(string recordDetail)
        {
            PlaybackRet playbackRet = new PlaybackRet();
            playbackRet._recordDetail = recordDetail;
            return playbackRet;
        }

        public string _recordDetail;

        public PlaybackRet()
        {

        }
        public void decode(Input _in)
        {
            _recordDetail = _in.readString();
        }

        public void encode(Output _out)
        {
            _out.writeString(recordDetail);
        }

        public string recordDetail
        {
            get { return _recordDetail; }
            set { _recordDetail = value; }
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
            return "PlaybackRet [recordDetail=" + _recordDetail + ", ]";
        }
    }
}