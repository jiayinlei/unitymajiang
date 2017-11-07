using com.guojin.core.io.message;
using com.guojin.core.io;
using System;
using System.Collections.Generic;

public class PdkUserPlaceMsg : Message
{
    public static int TYPE = 4;
    public static int ID = 5;
    protected int[] shouPais;    //手牌
    protected int[] outPais;     //出过的牌


    public void decode(Input _in)
    {
        shouPais = _in.readIntArray ();
        outPais = _in.readIntArray();
    }

    public void encode(Output _out)
    {
        _out.writeIntArray(shouPais);
        _out.writeIntArray(outPais);
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
        return "PdkUserPlaceMsg [shouPais=>" + shouPais + "  outPais=>" + outPais +", ]";
    }
}
