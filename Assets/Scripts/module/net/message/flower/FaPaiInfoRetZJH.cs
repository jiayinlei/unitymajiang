using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
using System.Collections.Generic;

namespace com.guojin.mj.net.message.flower
{
    /// <summary>
    /// 炸金花发牌消息  返回
    /// </summary>
    public class FaPaiInfoRetZJH : Message
    {
        public static int TYPE = 3;
        public static int ID = 8;

        public static FaPaiInfoRetZJH create(int playerNum, int[] index, int zhuangIndex)
        {
            FaPaiInfoRetZJH ChapterMsg = new FaPaiInfoRetZJH();
            ChapterMsg._playerNum = playerNum;
            ChapterMsg._index = index;
            ChapterMsg._zhuangIndex = zhuangIndex;
            return ChapterMsg;
        }
        //剩余玩家数
        protected int _playerNum;
        //剩余玩家位置
        protected int[] _index;
        //庄index 0 东 1南 2西 3北 逆时针顺序
        protected int _zhuangIndex;

        public FaPaiInfoRetZJH()
        {

        }
        public void decode(Input _in)
        {
            _zhuangIndex = _in.readInt();
            _playerNum = _in.readInt();
            _index = _in.readIntArray();
        }

        public void encode(Output _out)
        {
            _out.writeInt(_zhuangIndex);
            _out.writeInt(_playerNum);
            _out.writeIntArray(_index);
        }

        public int PlayerNum
        {
            get
            {
                return _playerNum;
            }

            set
            {
                _playerNum = value;
            }
        }
        public int[] Index
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
        public int ZhuangIndex
        {
            get
            {
                return _zhuangIndex;
            }

            set
            {
                _zhuangIndex = value;
            }
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
            return "FaPaiInfoRetZJH [playerNum=" + _playerNum + ",index=" + _index + ",zhuangIndex=" + _zhuangIndex + ", ]";
        }
    }
}
