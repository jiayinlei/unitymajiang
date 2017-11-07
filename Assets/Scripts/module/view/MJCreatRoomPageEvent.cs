using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MJCreatRoomPageEvent : EventManager
{ 

    public void Click232323()
    {
        GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().Playback1();
        //string ss = "0514f4125849414f5f4d494e475f47414e47040d050d06";
        //string sss = "0514f4125849414f5f4d494e475f47414e47060b050b04";
        //GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().executeIntrust(sss);

    }
    public  Texture2D texture2D;
    string url;
    public override void InformationSetting()
    {
        MainLogic.Controller.gameType = GameType.MJ;
        SetLable(this.BindingSource[0], GameData.GetInstance().playerData.name);
        SetLable(this.BindingSource[1], string.Format("ID:{0}", GameData.GetInstance().playerData.id.ToString()));
        SetLable(this.BindingSource[2], GameData.GetInstance().playerData.gold.ToString());

        if (GameHallPageEvent.texture2D != null)
        {
            SetActive(BindingSource[7], false);
            SetActive(BindingSource[6], true);
            this.BindingSource[6].GetComponent<RawImage>().texture = GameHallPageEvent.texture2D;
        }
        else
        {
            url = GameData.GetInstance().playerData.avatar;

            if (url != null)
            {
                url = url.Substring(0, url.Length - 1);
                url += "64";
                if (url != null)
                {
                    StartCoroutine(DownloadImage(url));

                }
                SetActive(BindingSource[7], false);
            }
            else
            {
                SetActive(BindingSource[7], true);
            }

        }
        PlayerPrefs.SetInt("isGameHall",0);
        PlayerPrefs.Save();
    }
    void CloseSocket()
    {
        SocketMgr.GetInstance().OnWebScoketclose();
    }
    private IEnumerator DownloadImage(string url)
    {
        WWW www = new WWW(url);
        yield return www;
        texture2D = www.texture;
        this.BindingSource[6].GetComponent<RawImage>().texture = texture2D;
    }
    void PlayerInforBtnClick()
    {
        UIManager.ChangeUI(UIManager.PageState.PlayerInforPopPage, (GameObject obj) =>
        {
            obj.GetComponent<PlayerInforPopPageEvent>().InformationSetting();
        });
    }
    void BuyRoomCardBtnClick()
    {
        UIManager.ChangeUI(UIManager.PageState.BuyRoomCardPage, (GameObject obj) =>
        {
            obj.GetComponent<BuyRoomCardPageEvent>().InformationSetting();
        });
    }
    void ResultBtn()
    {
        UIManager.ChangeUI(UIManager.PageState.ResultPage, (GameObject obj) =>
        {
            obj.GetComponent<ResultPageEvent>().InformationSetting();
            SendMessageMgr.GetRoomHistory();
        });

    }
    void ShareBtnClick()
    {
#if UNITY_ANDROID 

        //StartCoroutine(GetCapture_version2());
        UIManager.ChangeUI(UIManager.PageState.ShareQRcodePage, (GameObject obj) =>
        {
            

        });
#elif UNITY_IPHONE
        LoginAndShare.Controller.isPngShare = true;
        LoginAndShare.Controller.SharePngIos();
#endif

    }

    public void ResetGoldNum()
    {
        SetLable(this.BindingSource[2], GameData.GetInstance().playerData.gold.ToString());
    }
    void ClosePage()
    {
        Destroy(this .gameObject);
        UIManager.ChangeUI(UIManager.PageState.GameHall, (GameObject obj) =>
        {
            obj.GetComponent<GameHallPageEvent>().InformationSetting();
        });
    }
    void SetBtnClick()
    {
                UIManager.ChangeUI(UIManager.PageState.GameSetPage, (GameObject obj) =>
                {
                    obj.GetComponent<GameSetPageEvent>().InformationSetting();
                });
    }
    void CreatRoom()
    {
        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/CreatRoomPanl"));
        temp.transform.SetParent(gameObject.transform);
        temp.transform.localPosition = Vector3.zero;
        temp.transform.localScale = Vector3.one;
    }
    void CreatRoomGuShi()
    {
        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/CreatRoomPanlgushi"));
        temp.transform.SetParent(gameObject.transform);
        temp.transform.localPosition = Vector3.zero;
        temp.transform.localScale = Vector3.one;
    }

    void OtherCreatRoom()
    {
        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/NotOpen"));
        temp.transform.SetParent(gameObject.transform);
        temp.transform.localPosition = Vector3.zero;
        temp.transform.localScale = Vector3.one;     
    }
    // Use this for initialization
    void JoinRoomBtn()
    {
        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/JoinRoomPanl"));
        temp.transform.SetParent(gameObject.transform);
        temp.transform.localPosition = Vector3.zero;
        temp.transform.localScale = Vector3.one;
    }
    void RuleBtn()
    {
        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/RulePanl"));
        temp.transform.SetParent(gameObject.transform);
        temp.transform.localPosition = Vector3.zero;
        temp.transform.localScale = Vector3.one;
    }
    void ContinueBtn()
    {
        Debug.Log("UIState_LoadingPage--MJCreatRoomPageEvent--ContinueBtn");
        GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_LoadingPage);
        
    }
}
