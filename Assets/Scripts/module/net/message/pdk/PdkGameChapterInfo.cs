using com.guojin.core.io.message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.guojin.core.io;
using System;

public class PdkGameChapterInfo : Message
{
    public static int TYPE = 4;
    public static int ID = 4;
    private int fistChupaiUserIndex;//第一个出牌人的位置
    private int currentChupaiUserIndex; //当前出牌人位置
                                        //    private int current;//  当场出牌人倒计时
    private List<PdkUserPlaceMsg> userPlaces;  //下标代表对应位置，PdkUserPlacemsg 为相应位置的一些信息


    public void decode(com.guojin.core.io.Input _in)
    {
        throw new NotImplementedException();
    }

    public void encode(Output _out)
    {
        throw new NotImplementedException();
    }

    public int getMessageId()
    {
        throw new NotImplementedException();
    }

    public int getMessageType()
    {
        throw new NotImplementedException();
    }

    public string toString()
    {
        throw new NotImplementedException();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
