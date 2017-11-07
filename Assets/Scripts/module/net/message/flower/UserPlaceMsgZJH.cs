using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.flower
{
    /// <summary>
    /// 炸金花玩家的信息
    /// </summary>
    public class UserPlaceMsgZJH : Message
    {
        public static int TYPE = 5;
        public static int ID = 1;
        public static UserPlaceMsgZJH create(int[] shouPai, bool isKan, bool isQi, bool isZiDongGen, int money)
        {
            UserPlaceMsgZJH userPlaceMsg = new UserPlaceMsgZJH();
            userPlaceMsg._shouPai = shouPai;
            userPlaceMsg._isKan = isKan;
            userPlaceMsg._isQi = isQi;
            userPlaceMsg._isZiDongGen = isZiDongGen;
            userPlaceMsg._money = money;
            return userPlaceMsg;
        }
        //玩家持有筹码
        protected int _money;
        //玩家手牌
        protected int[] _shouPai;
        //玩家是否看牌
        protected bool _isKan;
        //玩家是否弃牌
        protected bool _isQi;
        //玩家是否自动跟注
        protected bool _isZiDongGen;

        public int[] ShouPai
        {
            get
            {
                return _shouPai;
            }

            set
            {
                _shouPai = value;
            }
        }

        public bool IsKan
        {
            get
            {
                return _isKan;
            }

            set
            {
                _isKan = value;
            }
        }

        public bool IsQi
        {
            get
            {
                return _isQi;
            }

            set
            {
                _isQi = value;
            }
        }

        public bool IsZiDongGen
        {
            get
            {
                return _isZiDongGen;
            }

            set
            {
                _isZiDongGen = value;
            }
        }

        public int Money
        {
            get
            {
                return _money;
            }

            set
            {
                _money = value;
            }
        }

      
        public void decode(Input _in)
        {
            _shouPai = _in.readIntArray();
            _isKan = _in.readBoolean();
            _isQi = _in.readBoolean();
            _isZiDongGen = _in.readBoolean();
            _money = _in.readInt();
        }

        public void encode(Output _out)
        {
            _out.writeIntArray(_shouPai);
            _out.writeBoolean(_isKan);
            _out.writeBoolean(_isQi);
            _out.writeBoolean(_isZiDongGen);
            _out.writeInt(_money);
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
            return "UserPlaceMsgZJH [shoupai=" + _shouPai + ",isKan=" + _isKan + ",isQi=" + _isQi + ",isZiDongGen=" + _isZiDongGen + ",money=" + _money + ", ]";
        }
    }
}