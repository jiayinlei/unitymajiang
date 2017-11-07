using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* ***********************************************
 * Describe:录音显示界面
 * Author :  zhaozhongyang
 * Email: 1148312315@qq.com
 * DATA: 2017/7/28 16:00:21 
 * FileName: ChatPageEntry 
 * Version: V1.0.1
 * ***********************************************/
public class RecordPageEntry : MonoBehaviour ,IPointerDownHandler, IPointerUpHandler {

    private Transform root;
    private GameObject addNode;
    private Button Voice_Btn;
    private bool allowRecord = true;
    //private bool allowSendMsg = true;
    private bool isban = false;

    public bool canClick = false;
    public Image MicrophoneImg;
    private Transform ban;
    private AudioSource aud;
    private GameObject obj;
    bool isGameHall = false;
    private static RecordPageEntry instance = null;

    public static RecordPageEntry GetInstance()
    {
        return instance;
    }

    void Start ()
    {

         isGameHall = SceneManager.GetActiveScene().name.Equals("GameHall") ? true:false;
        
        instance = this;
        root = GameObject.Find("Canvas").transform;
        aud = GetComponent<AudioSource>();
        if (isGameHall)
        {
            Voice_Btn = this.GetComponent<Button>();
        }
        else
        {
            Voice_Btn = root.FindChild("ChoicePlayeWnd_Img/Voice_Btn").GetComponent<Button>();
        }

	    addNode = root.FindChild("addNode").gameObject;
        //Voice_Btn.transform.localScale *= 0.5F;
        //Voice_Btn.transform.localPosition += new Vector3(0, 0, 0);
        ban = Voice_Btn.gameObject.transform.FindChild("Ban");
#if UNITY_ANDROID && !UNITY_EDITOR
        LoginAndShare.Controller.CheckMicrophone();
#endif



    }


    void Update ()
	{
	    if (!isban)
	    {
            OnButtonHandled();
        }   
	}


   //0 关闭  1开启
    public void ShowBan( int controller)
    {
        switch (controller)
        {
            case 0:
                isban = false;
                if (ban)
                {
                    ban.gameObject.SetActive(false);
                }

                Voice_Btn.enabled = true;
                break;

            case 1:
                isban = true;
                if (ban)
                {
                    ban.gameObject.SetActive(true);
                }

                Voice_Btn.enabled = false;
                break;
        }
    }



    private float delay = 0.2f;
    private bool isDown = false;
    private float lastIsDownTime;


    private void OnButtonHandled()
    {
        if (isDown) {
            //if (Time.time - lastIsDownTime >= delay)
            //{
            //    Debug.Log("长按");
            //    lastIsDownTime = Time.time;
            for (int i = 0; i < Microphone.devices.Length; i++)
            {
                if (Microphone.IsRecording(Microphone.devices[i]))
                {
                    return;
                }
            }
            Micphone.GetInstance().StartRecord();
            isDown = false;
        }
    }
    bool isTruePointerDown = false;
    public void OnPointerDown(PointerEventData eventData)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (LoginAndShare.isMicrophoneOk)
        {
            if (aud.isPlaying)
            {
                isTruePointerDown = false;
                return;
            }
            isTruePointerDown = true;
            isDown = true;
            lastIsDownTime = Time.time;

            Debug.Log("ButtonDown");
        }
        else
        {

            LoginAndShare.Controller.ShowAndroidMsg("录音权限未开启");
        }
#else
        if (aud.isPlaying)
        {
            isTruePointerDown = false;
            return;
        }
        isTruePointerDown = true;
        isDown = true;
        lastIsDownTime = Time.time;

        Debug.Log("ButtonDown");
#endif
        
            
        
        

    }

    public void OnPointerUp(PointerEventData eventData) {
        if (aud.isPlaying)
        {
            return;
        }
        if (isTruePointerDown)
        {

            isTruePointerDown = false;
            
        }
        Micphone.GetInstance().StopRecord();

        isDown = false;
        //allowRecord = true;

    }


    public void showRecordPage()
    {
        //if (allowRecord)
        //{
             TooL.loadPrefab(root.FindChild("addNode").gameObject, "RecordPage").AddComponent<RecordPageMgr>();
        //    allowRecord = false;
        //}
        //else
        //{
        //    //提示
        //    if (GameObject.Find("MsgHint") == null)
        //    {
        //        GameObject hint = TooL.loadPrefab(root.FindChild("addNode").gameObject, "MsgHint");
        //        hint.transform.SetParent(root.FindChild("addNode"));
        //        hint.transform.localScale *= 0.5F;
        //        hint.transform.localPosition = new Vector3(0, -190, 0);
        //        Destroy(hint, 1.0f);
        //    }
        //}
    }

    public void DestoryRecordPage()
    {
        if (addNode.transform.childCount != 0)
        {
            if (addNode.transform.FindChild("RecordPage"))
            {
                Destroy(addNode.transform.FindChild("RecordPage").gameObject);
            }

        }     
    }
} 








