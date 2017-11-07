using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.flower
{
    /// <summary>
    /// 炸金花解散房间，返回
    /// </summary>
    public class DissolveRoomInfoRetZJH : Message
    {
        public static int TYPE = 3;
        public static int ID = 25;
        public static DissolveRoomInfoRetZJH Create(int userIndex, int roomId)
        {
            DissolveRoomInfoRetZJH Pgof = new DissolveRoomInfoRetZJH();
            Pgof._userIndex = userIndex;
            Pgof._roomId = roomId;
            return Pgof;
        }
        protected int _userIndex;
        /// <summary>
        /// 房间号
        /// </summary>
        protected int _roomId;

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
        public int UserIndex
        {
            get
            {
                return _userIndex;
            }
            set
            {
                _userIndex = value;
            }
        }
        public DissolveRoomInfoRetZJH()
        {

        }
        public void decode(Input _in)
        {
            _roomId = _in.readInt();
            _userIndex = _in.readInt();
        }

        public void encode(Output _out)
        {
            _out.writeInt(_roomId);
            _out.writeInt(_userIndex);
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
            return "DissolveRoomInfoRetZJH [ " + "roomId" + _roomId + "userIndex" + _userIndex + ",]";
        }
    }
}

