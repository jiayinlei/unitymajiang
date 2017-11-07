using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.flower
{
    /// <summary>
    /// 炸金花操作玩家信息，返回
    /// </summary>
    public class PlayerHandleRetZJH : Message
    {
        public static int TYPE = 3; 
        public static int ID = 33;
        public PlayerHandleRetZJH Create(int index,int zhu)
        {
            PlayerHandleRetZJH lcf = new PlayerHandleRetZJH();
            lcf._index = index;
            lcf._zhu = zhu;
            return lcf;
        }
        /// <summary>
        /// 玩家位置
        /// </summary>
        protected int _index;
        //当前的注数
        protected int _zhu;
        public int Index
        {
            get
            {
                return _index;
            }
            set
            {
                _index = value;
            }
        }
        public int Zhu
        {
            get
            {
                return _zhu;
            }

            set
            {
                _zhu = value;
            }
        }



        public PlayerHandleRetZJH()
        {

        }
        public void decode(Input _in)
        {
            _index = _in.readInt();
            _zhu = _in.readInt();
        }

        public void encode(Output _out)
        {
            _out.writeInt(_index);
            _out.writeInt(_zhu);
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
            return "PlayerHandleRetZJH [ " + "index" + _index + "zhu" + _zhu + ",]";
        }
    }
}
