using com.guojin.core.io.message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.guojin.core.io;
using System;

public class JoinPdkRoomRet : Message
{
    public static int TYPE = 4;
    public static int ID = 7;
    protected bool result;

    public void decode(com.guojin.core.io.Input _in)
    {
        result = _in .readBoolean();
    }

    public void encode(Output _out)
    {
        _out.writeBoolean(result);
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
        return "JoinPdkRoomRet [result=>" + result + ", ]";
    }
}
