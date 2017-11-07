
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.login
{
    public class Playback : Message
    {
        public static int TYPE = 7;
        public static int ID = 38;

        public static Playback create(string roomId, string chapterIndex)
        {
            Playback playback = new Playback();
            playback._roomId = roomId;
            playback._chapterIndex = chapterIndex;
            return playback;
        }

        public string _roomId;
        public string _chapterIndex;

        public Playback()
        {

        }
        public void decode(Input _in)
        {
            _roomId = _in.readString();
            _chapterIndex = _in.readString();
        }

        public void encode(Output _out)
        {
            _out.writeString(roomId);
            _out.writeString(chapterIndex);
        }

        public string roomId
        {
            get { return _roomId; }
            set { _roomId = value; }
        }
        public string chapterIndex
        {
            get { return _chapterIndex; }
            set { _chapterIndex = value; }
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
            return "Playback [roomId=" + _roomId + ", chapterIndex=" + _chapterIndex + ", ]";
        }
    }
}