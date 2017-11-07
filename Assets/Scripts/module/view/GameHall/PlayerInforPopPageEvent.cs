using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInforPopPageEvent : EventManager {
    public override void InformationSetting()
    {
        SetLable(this.BindingSource[1], GameData.GetInstance().playerData.name);
        SetLable(this.BindingSource[2], GameData.GetInstance().playerData.id.ToString());
        SetLable(this.BindingSource[3], GameData.GetInstance().playerData.gold.ToString());
        int playTimes = GameData.GetInstance().playerData.playMjTimes + GameData.GetInstance().playerData.playPokerTimes;
        SetLable(this.BindingSource[4], playTimes.ToString () );
        int winTimes = GameData.GetInstance().playerData.mjWinTimes  + GameData.GetInstance().playerData.winPokerTimes ;
        string IP = Network.player.ipAddress;
        if (GameData.GetInstance().playerData.place == null || GameData.GetInstance().playerData.place.Equals("")
            ||GameData.GetInstance().playerData.place .Equals("null"))
        {
            SetLable(this.BindingSource[6], "未知");
        }
        else
        {
            SetLable(this.BindingSource[6], GameData.GetInstance().playerData.place.ToString());
        }
        Debug.Log("地理位置1" + GameData.GetInstance().playerData.place);
#if UNITY_ANDROID

        //Login

#elif UNITY_IPHONE

#endif
        //playTimes = 5;
        //winTimes = 3;

        if (winTimes == 0)
        {
            SetLable(this.BindingSource[5], "0 %");

        }
        else
        {
            float temp = (float )winTimes / playTimes;
            int j = (int)(temp * 100);
            SetLable(this.BindingSource[5], string .Format ("{0} %",j.ToString()));
        }
        string url = GameData.GetInstance().playerData.avatar;
        
        if (GameHallPageEvent.texture2D!=null )
        {
            this.BindingSource[0].GetComponent<RawImage>().texture = GameHallPageEvent.texture2D;
        }
        else if ( url != null )
        {
            

            url = url.Substring(0, url.Length - 1);
            url += "132";
            if (url != null)
            {
                StartCoroutine(DownloadImage(url));
            }
        }

        

    }
    private IEnumerator DownloadImage(string url)
    {
        WWW www = new WWW(url);
        yield return www;
        this.BindingSource[0].GetComponent<RawImage>().texture = www.texture;
    }
    void ClosePage() {
        transform.PlayButtonVoice();
        Destroy(this.gameObject);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
