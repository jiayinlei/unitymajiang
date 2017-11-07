﻿
using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
namespace com.guojin.mj.net.message.game
{
    public class ServerError : Message
    {

        public static int TYPE = 1;
        public static int ID = 27;

        public ServerError()
        {

        }
        public void decode(Input _in)
        {
            
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
            return "ServerError [ ]";
        }     
    }
}


