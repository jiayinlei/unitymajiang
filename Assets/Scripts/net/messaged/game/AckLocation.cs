using com.guojin.core.io.message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.guojin.core.io;
using System;
namespace com.guojin.mj.net.message.login
{
public class AckLocation : Message
{
    public static int TYPE = 7;
    public static int ID   = 41;

    public string lontitude;  //经度
    public string latitude;  //纬度
    public static AckLocation Creat(string _lontitude, string _latitude)
    {
        AckLocation ackLocation = new AckLocation();
        ackLocation.lontitude = _lontitude;
        ackLocation.latitude = _latitude;
        return ackLocation;
    }
    public void decode(com.guojin.core.io.Input _in)
    {

    }

    public void encode(Output _out)
    {
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
        return "AckLocation [lontitude=" + lontitude + ",latitude=" + latitude + ", ]";
    }
}
}
