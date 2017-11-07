using com.guojin.core.io.message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.guojin.core.io;
using System;

public class JoinPdkRoom : Message {

    public static int TYPE = 4;
    public static int ID = 6;

    protected String room_no;
    public static JoinPdkRoom create(string roomNum)
    {
        JoinPdkRoom joinRoom = new JoinPdkRoom();
        joinRoom.room_no = roomNum;
        return joinRoom;
    }
    public void decode(com.guojin.core.io.Input _in)
    {
        room_no = _in.readString();
    }

    public void encode(Output _out)
    {
        _out.writeString(room_no);
    }

    public int getMessageId()
    {
        return ID; ;
    }

    public int getMessageType()
    {
        return TYPE;
    }

    public string toString()
    {
        return "AckJoinPdkRoom [room_no=>" + room_no + ", ]";
    }


}
