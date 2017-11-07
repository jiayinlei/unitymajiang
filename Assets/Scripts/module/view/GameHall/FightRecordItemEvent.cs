using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class FightRecordItemEvent : EventManager  {
    public override void InformationSetting()
    {
        
    }
    public void ShareBtnClick()
    {
        //Debug.Log("fengxiang");
        //LoginAndShare.Controller.OnClickShared();

#if UNITY_ANDROID
        UIManager.ChangeUI(UIManager.PageState.SharePage, (GameObject obj) =>
        {

        });
#elif UNITY_IPHONE
        LoginAndShare.Controller.SharePngIos();
#endif
    }

    // Use this for initialization
    void Start () {
        this.Open();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void PlaybackBtnClick()
    {
        string roomNumStr = ((Text)this.BindingSource[2].GetComponent<Text>()).text;
        string chapterNumStr = ((Text)this.BindingSource[0].GetComponent<Text>()).text;
        Regex reg = new Regex("(?<num>\\d+)");
        Match match = reg.Match(roomNumStr);
        string roomNum = match.Groups["num"].Value;
        Match match2 = reg.Match(chapterNumStr);
        string chapterNum = match2.Groups["num"].Value;
        Debug.Log("房号：" + roomNum);
        Debug.Log("局数：" + chapterNum);
        ShowPlaybackList(Convert.ToInt32(roomNum), Convert.ToInt32(chapterNum));
    }

    /// <summary>
    /// 显示回放列表
    /// </summary>
    public void ShowPlaybackList(int roomNum, int chapterNum)
    {
        UIManager.ChangeUI(UIManager.PageState.PlaybackListPage, (GameObject obj) =>
        {
            obj.GetComponent<PlaybackListPageEvent>().roomNum = roomNum;
            obj.GetComponent<PlaybackListPageEvent>().chapterNum = chapterNum;
            Debug.Log("ShowPlaybackList Callback");
        });
    }
}
