using System;

using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.game
{
    //一局麻将的信息
    public class UserPlaceMsg : Message
    {
        public static int TYPE = 1;
        public static int ID = 20;

        public static  UserPlaceMsg create(int[] shouPai,int shouPaiLen, int[] anGang, int[] xiaoMingGang, int[] daMingGang, int[] peng , int[] chi, int[] outPai,int xuanpaocount,int[] chiIndex, int[] pengIndex, int[] daMingGangIndex, int[] xiaoMingGangIndex)
        {
            UserPlaceMsg userPlaceMsg = new UserPlaceMsg();
            userPlaceMsg._shouPai = shouPai;
            userPlaceMsg._shouPaiLen = shouPaiLen;
            userPlaceMsg._anGang = anGang;
            userPlaceMsg._xiaoMingGang = xiaoMingGang;
            userPlaceMsg._daMingGang = daMingGang;
            userPlaceMsg._peng = peng;
            userPlaceMsg._chi = chi;
            userPlaceMsg._outPai = outPai;
            userPlaceMsg._xuanpaocount = xuanpaocount;
            userPlaceMsg._chiIndex = chiIndex;
            userPlaceMsg._pengIndex = pengIndex;
            userPlaceMsg._daMingGangIndex = daMingGangIndex;
            userPlaceMsg._xiaoMingGangIndex = xiaoMingGangIndex;
            return userPlaceMsg;
        }
        protected int[] _shouPai;
        //别人的信息只显示手牌的数量
        protected int _shouPaiLen;
        //已经显示的暗杠，如果是自己的则全部显示，不是自己的且如果还不能显示，那么传递-1
        protected int[] _anGang;
        protected int[] _xiaoMingGang;
        protected int[] _daMingGang;
        protected int[] _peng;
        //三个一组
        protected int[] _chi;
        //打出的牌
        protected int[] _outPai;
        protected int _xuanpaocount;
        private int[] _chiIndex;     //暂时未用此属性，但有定义，也需解码
        private int[] _pengIndex;    //  和上面的peng[]一一对应
        private int[] _daMingGangIndex;    //和上面的daMingGang[]一一对应
        private int[] _xiaoMingGangIndex;    //和上面的xiaoMingGang[]一一对应
        public  int queIndex;//123
        public  string zuiIndex;
        public UserPlaceMsg()
        {

        }
        public void decode(Input _in)
        {
            _shouPai = _in.readIntArray();
            _shouPaiLen = _in.readInt();
            _anGang = _in.readIntArray();
            _xiaoMingGang = _in.readIntArray();
            _daMingGang = _in.readIntArray();
            _peng = _in.readIntArray();
            _chi = _in.readIntArray();
            _outPai = _in.readIntArray();
            _xuanpaocount = _in.readInt();
            _chiIndex = _in.readIntArray();
            _pengIndex = _in.readIntArray();
            _daMingGangIndex = _in.readIntArray();
            _xiaoMingGangIndex = _in.readIntArray();
            if (Method.MJType== "MaJongGuShi")
            {
                queIndex = _in.readInt();
                zuiIndex = _in.readString();
            }
        }

        public void encode(Output _out)
        {
            _out.writeIntArray(shouPai);
            _out.writeInt(shouPaiLen);
            _out.writeIntArray(anGang);
            _out.writeIntArray(xiaoMingGang);
            _out.writeIntArray(daMingGang);
            _out.writeIntArray(peng);
            _out.writeIntArray(chi);
            _out.writeIntArray(outPai);
            _out.writeInt(xuanpaocount);
            _out.writeIntArray(chiIndex);
            _out.writeIntArray(pengIndex);
            _out.writeIntArray(daMingGangIndex);
            _out.writeIntArray(xiaoMingGangIndex);
        }
        public int[] chiIndex
        {
            get { return _chiIndex; }
            set { _chiIndex = value; }
        }

        public int[] pengIndex
        {
            get { return _pengIndex; }
            set { _pengIndex = value; }
        }
        public int[] daMingGangIndex
        {
            get { return _daMingGangIndex; }
            set { _daMingGangIndex = value; }
        }
        public int[] xiaoMingGangIndex
        {
            get { return _xiaoMingGangIndex; }
            set { _xiaoMingGangIndex = value; }
        }
        public int xuanpaocount
        {
            get { return _xuanpaocount; }
            set { _xuanpaocount = value; }
        }
        public int[] shouPai
        {
            get { return _shouPai; }
            set { _shouPai = value; }
        }
        public  int shouPaiLen
        {
            get { return _shouPaiLen; }
            set { _shouPaiLen = value; }
        }
        public int[] anGang
        {
            get { return _anGang; }
            set { _anGang = value; }
        }
        public int[] xiaoMingGang
        {
            get { return _xiaoMingGang; }
            set { _xiaoMingGang = value; }
        }
        public int[] daMingGang
        {
            get { return _daMingGang; }
            set { _daMingGang = value; }
        }
        public int[] peng
        {
            get { return _peng; }
            set { _peng = value; }
        }
        public int[] chi
        {
            get { return _chi; }
            set { _chi = value; }
        }
        public int[] outPai
        {
            get { return _outPai; }
            set { _outPai = value; }
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
            return "UserPlaceMsg [shouPai=" + _shouPai + ",shouPaiLen=" + _shouPaiLen + ",anGang=" + _anGang + ",xiaoMingGang=" + _xiaoMingGang + ",daMingGang=" + _daMingGang + ",peng=" + _peng + ",chi=" + _chi + ",outPai=" + _outPai + ",xuanpaocount=" + _xuanpaocount + ", ]";
        }
    }
}