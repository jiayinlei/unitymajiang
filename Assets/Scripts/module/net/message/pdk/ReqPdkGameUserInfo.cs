using com.guojin.core.io.message;
using com.guojin.core.io;
using System;

public class ReqPdkGameUserInfo  : Message
{
    public static int TYPE = 4;
    public static int ID = 3;

    protected String _headUrl; //头像
    protected String _nickName;//昵称
    protected int _selfIndex;    //当前位置  逆时针 0，1，2.。。
    protected int _socre;    //积分
    protected int _sex; // 0为男，1为女
    protected bool  _online; //是否在线
    protected int _userId; //用户I
    protected String _ip;// 用户当前Ip
    /**
     * 用户经纬度
     */
    private String _longitude;
    private String _latitude;
    public string headUrl
    {
        get
        {
            return _headUrl;
        }
        set
        {
            _headUrl = value;
        }
    }
    public string nickName
    {
        get
        {
            return _nickName;
        }
        set
        {
            _nickName = value;
        }
    }
    public int selfIndex
    {
        get
        {
            return _selfIndex;
        }
        set
        {
            _selfIndex = value;
        }
    }
    public int socre
    {
        get
        {
            return _socre;
        }
        set
        {
            _socre = value;
        }
    }
    public int sex
    {
        get
        {
            return _sex;
        }
        set
        {
            _sex = value;
        }
    }
    public bool online
    {
        get
        {
            return _online;
        }
        set
        {
            _online = value;
        }
    }
    public int userId
    {
        get
        {
            return _userId;
        }
        set
        {
            _userId = value;
        }
    }
    
    public string ip
    {
        get
        {
            return _ip;
        }
        set
        {
            _ip = value;
        }
    }
    public string longitude
    {
        get
        {
            return _longitude;
        }
        set
        {
            _longitude = value;
        }
    }
    
    public string latitude
    {
        get
        {
            return _latitude;
        }
        set
        {
            _latitude = value;
        }
    }
    public void decode(com.guojin.core.io.Input _in)
    {
        _headUrl = _in.readString();
        _nickName = _in.readString();
        _selfIndex = _in.readInt();
        _socre = _in.readInt();
        _sex = _in.readInt();
        _online = _in.readBoolean();
        _userId = _in.readInt();
        _ip = _in.readString();
        _longitude = _in.readString();
        _latitude = _in.readString();
    }

    public void encode(Output _out)
    {
        _out.writeString(_headUrl);
        _out.writeString(_nickName);
        _out.writeInt(_selfIndex);
        _out.writeInt(_socre);
        _out.writeInt(_sex);
        _out.writeBoolean(_online);
        _out.writeInt(_userId);
        _out.writeString(_ip);
        _out.writeString(_longitude);
        _out.writeString(_latitude);

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
        return "ReqPdkGameUserInfo [ " + "  _headUrl=>" + _headUrl + "  _nickName=>" + _nickName + "  _selfIndex=>" + _selfIndex + "  _socre=>"+ _socre
           + "  _sex=>"+ _sex + "  _online=>"+ _online+ "  _userId=>"+ _userId + "  _ip=>"+ _ip+ "  _longitude=>"+ _longitude+ "  _latitude=>"+ _latitude+ "]";
    }

    // Use this for initialization

}
