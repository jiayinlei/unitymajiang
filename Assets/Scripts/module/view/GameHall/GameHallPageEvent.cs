using Assets.Scripts.BullFight.View;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHallPageEvent : EventManager
{
    public static Texture2D texture2D;
    string url;
    public WebMediator WebMediator;
    public static bool isFirstLogin = true;
    public override void InformationSetting()
    {
        this.ChangeBtnSize(0);
        SetLable(this.BindingSource[0], GameData.GetInstance().playerData.name);
        SetLable(this.BindingSource[1], string.Format("ID:{0}", GameData.GetInstance().playerData.id.ToString()));
       // SetLable(this.BindingSource[8],  GameData.GetInstance().playerData.place);
        //SetLable(this.BindingSource[5], GameData.GetInstance().playerData.gold.ToString());
        //SetLable(this.BindingSource[11], GameData.GetInstance().playerData.SystemMessages.radio);
        //float bg_f   = this.BindingSource[12].GetComponent<RectTransform>().rect.width;
        //float text_f = this.BindingSource[11].GetComponent<RectTransform>().rect.width;
        //float f = bg_f / 2 + text_f;
        //this.BindingSource[11].GetComponent<>
        //this.BindingSource[10].GetComponent<RawImage>().

#if WX_SDK
        url = GameData.GetInstance().playerData.avatar;
        if (texture2D != null)
        {
            this.BindingSource[2].GetComponent<RawImage>().texture = texture2D;
            SetActive(BindingSource[7], false);
        }
        else if (url != null)
        {
            //用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空
            url = url.Substring(0, url.Length - 1);
            url += "132";
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
        if (isFirstLogin)
        {
            isFirstLogin = false;
            // UIManager.ChangeUI(UIManager.PageState.GameHallNoticePage, (GameObject obj) =>{});


        }
#else
        url = URLConst.DefaultUserHeadUrl; 
#endif

        PlayerPrefs.SetInt("isGameHall", 1);
        PlayerPrefs.Save();
    }
    public void ResetGoldNum()
    {
        //SetLable(this.BindingSource[5], GameData.GetInstance().playerData.gold.ToString());
        
    }
    void DNHall() {
        transform.PlayButtonVoice();
        ClosePage();
        UIManager.ChangeUI(UIManager.PageState.DNMainPanelPage, (GameObject obj) => {
            obj.GetComponent<DNCreatRoomPage>().InformationSetting();
        });
        //SceneManager.LoadScene("DNGameHall");
        //clubPage.GetComponent<ClubPageManager>().callBack += GameHallBtn;
    }
    void SisBtnClick()
    {
        UIManager.ChangeUI(UIManager.PageState.SysNoticePage, (GameObject obj) =>
        {
            obj.GetComponent<SysNoticePageEvent>().InformationSetting();
        });
    }
    void AppBtnClick()
    {
#if WX_SDK
       LoginAndShare.Controller.GetAgency();
#else

#endif

    }
    void NameApproveBtnClick()
    {
        int identitycardState = PlayerPrefs.GetInt("identitycard", 0);
        if (identitycardState == 0)
        {
            UIManager.ChangeUI(UIManager.PageState.NameApprovePopupPage, (GameObject obj) =>
            {
                obj.GetComponent<NameApprovePopupPageEvent>().InformationSetting();
            });
        }
        else
        {
            UIManager.ChangeUI(UIManager.PageState.NoticePage, (GameObject obj) =>
            {
                obj.GetComponent<NoticePageEvent>().InformationSetting();
                obj.GetComponent<NoticePageEvent>().SetHintMessage("已进行过身份验证");
            });
        }
    }
    void GameMusicSetBtnClick()
    {
        GameObject gameObject = GameObject.Find("addNode");
        TooL.loadPrefab(gameObject, "GameSetPage");
    }
    void shareBtnCliack()
    {
#if WX_SDK
        UIManager.ChangeUI(UIManager.PageState.SharePage, (GameObject obj) =>
        {
            obj.GetComponent<SharePageEvent>().hasRoomID = false;
        });
#else
         //LoginAndShare.Controller.sharePng();
#endif

    }


    private IEnumerator DownloadImage(string url)
    {
        WWW www = new WWW(url);
        yield return www;
        texture2D = www.texture;
        this.BindingSource[2].GetComponent<RawImage>().texture = texture2D;
        SetActive(BindingSource[7], false);
    }
    void MJBtnClick()
    {
        ClosePage();
        UIManager.ChangeUI(UIManager.PageState.MJCreatRoomPage, (GameObject obj) =>
        {
            obj.GetComponent<MJCreatRoomPageEvent>().InformationSetting();
        });
    }
    void GoldenFlowerBtnClick()
    {
        //SceneManager.LoadScene("PokerFlower");
        ////ClosePage();
        ////UIManager.ChangeUI(UIManager.PageState.MJCreatRoomPage, (GameObject obj) =>
        ////{
        ////    obj.GetComponent<MJCreatRoomPageEvent>().InformationSetting();
        ////});
        UIManager.ChangeUI(UIManager.PageState.NoticePage, (GameObject obj) =>
        {
            obj.GetComponent<NoticePageEvent>().InformationSetting();
        });
    }
    void RunFastBtnClick()
    {
        //ClosePage();
        //UIManager.ChangeUI(UIManager.PageState.PDKCreateRoomPage, (GameObject obj) =>
        //{
        //    obj.GetComponent<PDKCreatRoomPageEvent>().InformationSetting();
        //});
        UIManager.ChangeUI(UIManager.PageState.NoticePage, (GameObject obj) =>
        {
            obj.GetComponent<NoticePageEvent>().InformationSetting();
            obj.GetComponent<NoticePageEvent>().SetHintMessage("暂未开放");
        });

    }
    public void GameHallBtn()
    {
        this.ChangeBtnSize(0);
    }

    void ClubBtn()
    {
        this.ChangeBtnSize(1);
        GameObject clubPage = TooL.loadPrefab(GameObject.Find("Canvas/addNode"), "ClubPage");
        clubPage.GetComponent<ClubPageManager>().callBack += GameHallBtn;
    }


    public  void ChangeBtnSize(int btnNum)
    {
        for (int i = 0; i < 2; i++)
        {
            if (btnNum == 0)
            {
                SetActive(this.BindingSource[3], true);
                SetActive(this.BindingSource[4], false);
                SetActive(this.BindingSource[6], true);
                SetActive(this.BindingSource[5], false);
            }
            else
            {
                SetActive(this.BindingSource[6], false);
                SetActive(this.BindingSource[5], true);
                SetActive(this.BindingSource[3], false);
                SetActive(this.BindingSource[4], true);
            }
        }
    }

    void ClosePage()
    {
        Destroy(this.gameObject);
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
    void MoreSetClick()
    {
        UIManager.ChangeUI(UIManager.PageState.MoreSetPage, (GameObject obj) =>
        {
            obj.GetComponent<MoreSetPageEvent>().InformationSetting();
        });
    }


}
