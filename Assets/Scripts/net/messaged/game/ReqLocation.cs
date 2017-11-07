using com.guojin.core.io.message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.guojin.core.io;
using System;

public class ReqLocation : Message
{
    public static int TYPE = 1;
    public static int ID = 35;
    public int locationIndex;
    public string lontitude ;  //经度
    public string latitude ;  //纬度

    public static ReqLocation Creat(int _locationIndex, string _lontitude, string _latitude)
    {
        ReqLocation reqLocation = new ReqLocation();
        reqLocation.locationIndex = _locationIndex;
        reqLocation.lontitude = _lontitude;
        reqLocation.latitude = _latitude;
        return reqLocation;
    }
    public void decode(com.guojin.core.io.Input _in)
    {
        locationIndex = _in.readInt();
        lontitude =_in.readString();
        latitude =_in.readString();
    }

    public void encode(Output _out)
    {
        _out.writeInt(locationIndex);
        _out.writeString(lontitude);
        _out.writeString(latitude);
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
        return "ReqLocation [lontitude=" + lontitude + ",latitude=" + latitude + ",locationIndex" + locationIndex+ ", ]";
    }

}
