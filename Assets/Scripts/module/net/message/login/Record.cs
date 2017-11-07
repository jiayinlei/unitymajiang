using com.guojin.core.io.message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.guojin.core.io;
using System;
namespace com.guojin.mj.net.message.login
{
    public class Record : Message
    {
        public static int TYPE = 7;
        public static int ID = 35;
        private int _majiangNum; //麻将总局数
        private int _majiangWinCount;    //麻将赢的次数
        private int _pokerNum;    //扑克总局数，暂时未用，可不用处理，但须解码
        private int _pokerWinCount;    //扑克赢的总局数，暂时未用，可不用处理，但须解码
        public int majiangNum
        {
            get
            {
                return _majiangNum;
            }

            set
            {
                _majiangNum = value;
            }
        }
        public int majiangWinCount
        {
            get
            {
                return _majiangWinCount;
            }

            set
            {
                _majiangWinCount = value;
            }
        }
        public int pokerNum
        {
            get
            {
                return _pokerNum;
            }

            set
            {
                _pokerNum = value;
            }
        }
        public int pokerWinCount
        {
            get
            {
                return _pokerWinCount;
            }

            set
            {
                _pokerWinCount = value;
            }
        }
        public void decode(com.guojin.core.io.Input _in)
        {
            this._majiangNum = _in.readInt();
            this._majiangWinCount = _in.readInt();
            this._pokerNum = _in.readInt();
            this._pokerWinCount = _in.readInt();
        }

        public void encode(Output _out)
        {
            _out.writeInt(_majiangNum);
            _out.writeInt(_majiangWinCount);
            _out.writeInt(_pokerNum);
            _out.writeInt(_pokerWinCount);
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
            return "Record [Record=" + ", ]";
        }




    }
}
