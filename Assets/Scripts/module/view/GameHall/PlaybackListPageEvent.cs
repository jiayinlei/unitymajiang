using com.guojin.mj.net.message;
using com.guojin.mj.net.message.login;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class PlaybackListPageEvent : EventManager {
    void Start () {
        Debug.Log("PlaybackListPageEvent Start");
        Debug.Log("roomNum:" + roomNum);
        Debug.Log("chapterNum:" + chapterNum);
        //GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().init();
        for (int i = 0; i < chapterNum; i++)
        {
            //GameObject.Find("Btn" + i).SetActive(true);
            GameObject.Find("Btn" + i).transform.Find("Text").GetComponent<Text>().text = Convert.ToString(i + 1) + "/" + chapterNum.ToString();
            Button btn = GameObject.Find("Btn" + i).GetComponent<Button>();
            GameObject obj = GameObject.Find("Btn" + i);
            btn.onClick.AddListener(delegate () {
                this.playbackBtnClick(obj);
            });

        }

        for (int i = chapterNum; i < 16; i++)
        {
            GameObject.Find("Btn" + i).SetActive(false);
        }
    }
  

    void playbackBtnClick(GameObject sender)
    {
        Debug.Log("playbackBtnClick");
        Regex reg = new Regex("(?<num>\\d+)");
        Match match = reg.Match(sender.name);
        string chapterIndex = match.Groups["num"].Value;
        Debug.Log(chapterIndex);
        Playback playback = Playback.create(roomNum.ToString(), (Int32.Parse(chapterIndex) + 1).ToString());
       
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(playback));
        //Method.isCreater = false;
        //GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().playbackTest();
    }

    public int roomNum = 0;
    public int chapterNum = 0;

    void CloseBtnClick()
    {
        Destroy(this.gameObject);
    }

    public override void InformationSetting()
    {
        throw new NotImplementedException();
    }
}
