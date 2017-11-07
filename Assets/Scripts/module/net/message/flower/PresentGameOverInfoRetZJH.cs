using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.flower
{
    /// <summary>
    /// 炸金花当前局结果，返回
    /// </summary>
    public class PresentGameOverInfoRetZJH : Message
    {
        public static int TYPE = 3;
        public static int ID = 18;
        public PresentGameOverInfoRetZJH Create(int roomId, int winIndex, int winId, int winScore, string winName)
        {
            PresentGameOverInfoRetZJH Pgof = new PresentGameOverInfoRetZJH();
            Pgof._roomId = roomId;
            Pgof._winIndex = winIndex;
            Pgof._winId = winId;
            Pgof._winName = winName;
            Pgof._winScore = winScore;
            return Pgof;
        }
        /// <summary>
        /// 房间号
        /// </summary>
        protected int _roomId;
        /// <summary>
        /// 本局胜者位置
        /// </summary>
        protected int _winIndex;
        /// <summary>
        /// 本局胜者Id
        /// </summary>
        protected int _winId;
        /// <summary>
        /// 胜者名字
        /// </summary>
        protected string _winName;
        /// <summary>
        /// 胜者获得的金钱
        /// </summary>
        protected int _winScore;
        public PresentGameOverInfoRetZJH()
        {

        }
        public int RoomId
        {
            get
            {
                return _roomId;
            }
            set
            {
                _roomId = value;
            }
        }
        public int WinIndex
        {
            get
            {
                return _winIndex;
            }
            set
            {
                _winIndex = value;
            }
        }
        public int WinId
        {
            get
            {
                return _winId;
            }
            set
            {
                _winId = value;
            }
        }
        public int WinScore
        {
            get
            {
                return _winScore;
            }
            set
            {
                _winScore = value;
            }
        }
        public string WinName
        {
            get
            {
                return _winName;
            }
            set
            {
                _winName = value;
            }
        }

        public void decode(Input _in)
        {
            _roomId = _in.readInt();
            _winIndex = _in.readInt();
            _winId = _in.readInt();
            _winName = _in.readString();
            _winScore = _in.readInt();

        }

        public void encode(Output _out)
        {
            _out.writeInt(_roomId);
            _out.writeInt(_winIndex);
            _out.writeInt(_winId);
            _out.writeInt(_winScore);
            _out.writeString(_winName);
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
            return "PresentGameOverInfoRetZJH [ " + "roomId" + _roomId + "winIndex" + _winIndex + "winId" + _winId + "winScore" + _winScore + "winName" + _winName + ",]";
        }
    }
}
