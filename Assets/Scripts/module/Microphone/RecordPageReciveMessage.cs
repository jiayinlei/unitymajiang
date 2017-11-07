using com.guojin.mj.net.message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.guojin.core.io.message;
using UnityEngine;
using com.guojin.mj.net.message.game;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;
/* ***********************************************
* Describe: 接收服务器消息
* Author : 赵中阳
* Email: 1148312315@qq.com
* DATA: 2017/7/29 16:00:21 
* FileName: RecordPageReciveMessage
* Version: V1.0.1
* ***********************************************/
public class RecordPageReciveMessage : Observer
{

    private static RecordPageReciveMessage instance = null;

    public static RecordPageReciveMessage GetInstance()
    {
        return instance;
    }

    private  AudioSource aud;
    private AudioClip downLoadClip;
    public  bool planPlaying = false;
    private List<PlayerReceiveChatInfo> cliplist = new List<PlayerReceiveChatInfo>();
    WaitStartGamePageEvent we;

    private void Awake()
    {
        instance = this;
    }


    private void Start() {
        initMsg();
        aud = GetComponent<AudioSource>();
        if (GameObject.Find("WaitStartGame(Clone)"))
        {
            we = GameObject.Find("WaitStartGame(Clone)").GetComponent<WaitStartGamePageEvent>();
        }
       
    }

    protected override string[] GetMsgList() {
        return new string[] {
                MessageFactoryImpi.instance.getMessageString(1,25)
            };
    }

    public override void OnMsg(string msg, Message data)
    {
        Debug.Log("接收服务器发送的地址消息");
        PlayerReceiveChatInfo message = data as PlayerReceiveChatInfo;

        switch (message.Num) {
            case 1:
                //处理文本
                    string content = GameObject.Find("WaitStartGame(Clone)").GetComponent<WaitStartGame>().TouXiang[message.UserIndex].GetComponent<HeadInfo>().username.text;
                    we.chatItemList.Add(we.CreatItemData(content, message.ReceiveChatInfo));
                    if (we.chatItemList.Count >= 31)
                    {
                        we.chatItemList.RemoveAt(0);
                    }
                    we.cleanItem();
                    we.initView();
                
                break;
            case 2:
                //处理表情
                break;
            case 3:
                //处理语音
                break;
            case 4:
                cliplist.Add(message);
                break;
        }
        Debug.Log(data.toString());
    }

    private void Update() {

        if (!aud.isPlaying && cliplist.Count != 0 && planPlaying == false)
        {
            Next();
        }
    }

    private void Next()
    {
        planPlaying = true;
        StartCoroutine(DownAddress());
    }

    IEnumerator DownAddress()
    {
        RecordPageEntry.GetInstance().ShowBan(1);
         int index  =  cliplist[0].UserIndex;
        string path = cliplist[0].ReceiveChatInfo;
        WWW www = new WWW(path);
        yield return www;        
        downLoadClip = www.GetAudioClipCompressed(true,AudioType.OGGVORBIS);
        cliplist.RemoveAt(0);
        PlayRecord(index, downLoadClip);
    }

    IEnumerator ShowBan(float  t)
    {
        yield return new WaitForSeconds(t);
        RecordPageEntry.GetInstance().ShowBan(0);
    }


    private void PlayRecord(int index, AudioClip clip)
    {
        Debug.Log(index);
        Debug.Log(clip.length);
        Transform target = GameObject.Find(("HeadPhoto" + index)).transform;
        aud.clip = clip;
        aud.volume = 0.9f;
        aud.Play();
        planPlaying = false;
        float t = aud.clip.length;
        if (cliplist.Count == 0)
        {
            StartCoroutine(ShowBan(t));
        }

        //显示喇叭图标
        GameObject horn = new GameObject();
        horn.name = "horn";
        if (SceneManager.GetActiveScene().name.Equals("GameHall"))
        {
            horn.AddComponent<Image>().sprite = PPTextureManage.getInstance().LoadAtlasSprite("Atlas/CreatRoom_1", "voiceNotice");
            horn.transform.SetParent(target);

  
            
                horn.transform.localPosition = Vector3.zero ;
                horn.transform.localScale = new Vector3(1f, 1f, 1f);
            

            horn.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            horn.GetComponent<Image>().DOFade(1, 0.5f).SetLoops(-1, LoopType.Yoyo);
            Destroy(horn, t);
        }
        else
        {
            horn.AddComponent<Image>().sprite = Resources.Load<Sprite>("NewUIPicture/voice_icon");
            horn.transform.SetParent(target);

            if (target.localPosition.x > 0)
            {
                horn.transform.localPosition = Vector3.zero + new Vector3(-25, -25, 0);
                horn.transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
            }
            else
            {
                horn.transform.localPosition = Vector3.zero + new Vector3(25, 25, 0);
                horn.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }

            horn.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            horn.GetComponent<Image>().DOFade(1, 0.5f).SetLoops(-1, LoopType.Yoyo);
            Destroy(horn, t);
        }



    }
}
