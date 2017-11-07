using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreSetPageEvent : EventManager
{
    public override void InformationSetting()
    {
        
    }
    void ClosePage()
    {
        Destroy(this.gameObject);
    }
    void PhoneCallBtnClick()
    {
        TooL.showTips("确认拨打电话？", () => LoginAndShare.Controller.CallPhone());
        //LoginAndShare.Controller.CallPhone();
    }
    void PlayerReport()
    {
        UIManager.ChangeUI(UIManager.PageState.ReportPage , (GameObject obj) =>
        {
            obj.GetComponent<ReportPageEvent >().InformationSetting();
        });
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
