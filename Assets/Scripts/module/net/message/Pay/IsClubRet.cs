using System.Collections;
using System.Collections.Generic;
using com.guojin.core.io;
using com.guojin.core.io.message;
using com.guojin.mj.net.message.login;


public class IsClubRet : Message
{

    public static int TYPE = 7;
        public static int ID = 43;

        public IsClubRet()
        {
        }

        public List<payPrice> List
        {
            get { return _list; }
            set { _list = value; }
        }

        public bool Isclub
        {
            get { return _isclub; }
            set { _isclub = value; }
        }


        private List<payPrice> _list;
        private bool  _isclub;


        public void decode(Input _in)
        {

        _isclub = _in.readBoolean();
        GameData.GetInstance().playerData.isClub = _isclub;
        

        int len = _in.readInt();
            if (len >0)
            {
                Method.listPrice = new List<payPrice>();
                for (int i = 0; i < len; i++)
                {
                    payPrice pp = new payPrice();
                    pp.decode(_in);
                   
                    Method.listPrice.Add(pp);
                }    
            }
        } 

        public void encode(Output _out)
        {

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
            return "WeiXinShow [ ]";
        }
    }

