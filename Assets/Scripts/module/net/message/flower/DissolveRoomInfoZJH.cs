using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.flower
{
    /// <summary>
    /// 炸金花发起解散房间，发送
    /// </summary>
    public class DissolveRoomInfoZJH : Message
    {
        public static int TYPE = 3;
        public static int ID = 24;
        public static DissolveRoomInfoZJH Create(int userIndex, int roomId)
        {
            DissolveRoomInfoZJH Pgof = new DissolveRoomInfoZJH();
            Pgof._userIndex = userIndex;
            Pgof._roomId = roomId;
            return Pgof;
        }
        //发起者
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
        public DissolveRoomInfoZJH()
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
            return "DissolveRoomInfoZJH [ " + "roomId" + _roomId + "userIndex" + _userIndex + ",]";
        }
    }
}
