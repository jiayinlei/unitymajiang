using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPageEvent : EventManager {
    public int getRoomCard;
    public override void InformationSetting()
    {
        SetLable(this.BindingSource[0],string .Format ("房卡：{0}", getRoomCard.ToString()) );
    }
    void GetRoomBtnClick()
    {
        DestroyImmediate(this .gameObject );
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
