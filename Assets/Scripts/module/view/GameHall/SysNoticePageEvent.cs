using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SysNoticePageEvent : EventManager  {
    public override void InformationSetting()
    {
        
    }
    void CloseBtnClick()
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
