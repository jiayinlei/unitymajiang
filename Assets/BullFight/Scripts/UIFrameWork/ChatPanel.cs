using Assets.Scripts.BullFight.Data;
using Assets.Scripts.BullFight.Manager;
using com.guojin.dn.net.message;
using com.guojin.mj.net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatPanel : BasePanel {
    //bool showEmoji = false;
    // Use this for initialization
    private GameObject emojiPage;
    private GameObject shortCutPage;
    Image toggleImage1;
    Image toggleImage2;

    void Start () {
        emojiPage = transform.FindChild("EmojiPage").gameObject;
        shortCutPage = transform.FindChild("ShortCutPage").gameObject;
        transform.FindChild("CloseMask").GetComponent<Button>().onClick.AddListener(()=> {
            DNUIManager.Instance.PopPanel();

        });
        toggleImage1 = transform.FindChild("ToggleGroup/ToggleEmoji/Background").GetComponent<Image>();
        toggleImage2 = transform.FindChild("ToggleGroup/ToggleShortCut/Background").GetComponent<Image>();
        emojiPage.SetActive(true);
        shortCutPage.SetActive(false);
        toggleImage1.sprite = DynamicUIManager.Instance.BaseNameGetSprite("表情包_bule");
        toggleImage2.sprite = DynamicUIManager.Instance.BaseNameGetSprite("常用语_gray");
        transform.FindChild("ToggleGroup/ToggleEmoji").GetComponent<Toggle>().onValueChanged.AddListener((bool show)=> {
            if (show) {
                emojiPage.SetActive(true);
                toggleImage1.sprite = DynamicUIManager.Instance.BaseNameGetSprite("表情包_bule");
                toggleImage2.sprite = DynamicUIManager.Instance.BaseNameGetSprite("常用语_gray");
                shortCutPage.SetActive(false);
            } else {
                emojiPage.SetActive(false);
                shortCutPage.SetActive(true);
                toggleImage1.sprite = DynamicUIManager.Instance.BaseNameGetSprite("表情包_gray");
                toggleImage2.sprite = DynamicUIManager.Instance.BaseNameGetSprite("常用语_bule");
            }
        });
        transform.FindChild("ToggleGroup/ToggleShortCut").GetComponent<Toggle>().onValueChanged.AddListener((bool show) => {
            if (show) {
                emojiPage.SetActive(false);
                shortCutPage.SetActive(true);
                toggleImage1.sprite = DynamicUIManager.Instance.BaseNameGetSprite("表情包_gray");
                toggleImage2.sprite = DynamicUIManager.Instance.BaseNameGetSprite("常用语_bule");
            } else {
                emojiPage.SetActive(true);
                shortCutPage.SetActive(false);
                toggleImage1.sprite = DynamicUIManager.Instance.BaseNameGetSprite("表情包_bule");
                toggleImage2.sprite = DynamicUIManager.Instance.BaseNameGetSprite("常用语_gray");
            }
        });
        InitEmoji();
        InitShortCut();
    }
    private void InitEmoji() {
        int count = DNGlobalData.emojiCount;
        GameObject emojiModel = transform.FindChild("emojiModel").gameObject;
        Transform content = transform.FindChild("EmojiPage/Viewport/Content");
        for (int i = 0; i < count; i++) {
            GameObject emoji = Instantiate(emojiModel);
            emoji.transform.SetParent(content);
            emoji.transform.localScale = Vector3.one;
            if (emoji.GetComponent<Button>() == null) {
                emoji.AddComponent<Button>();
            }
            string emojiName = string.Format("{0:000}", i+1);
            emoji.GetComponent<Image>().sprite = DynamicUIManager.Instance.BaseNameGetSprite("emotion_" + emojiName);
            emoji.GetComponent<Button>().onClick.AddListener(()=> {
                DouniuChat chat = new DouniuChat();
                chat.setIndex(2);
                chat.setChatContent(emojiName);
                SocketMgr.GetInstance().Send(Net.instance.write(chat));
            });
            
        }
    }
        Button shortCutButton;
    private void InitShortCut() {
        Transform shortCut = transform.FindChild("ShortCutPage/Scroll View/Viewport/Content");
        int child = shortCut.childCount;
        List<Button> buttonList = new List<Button>();
        for (int i = 0; i < child; i++) {
            buttonList.Add(shortCut.GetChild(i).GetComponent<Button>());
            shortCutButton = shortCut.GetChild(i).GetComponent<Button>();
            //GameManager.instance.shortCutList.Add(shortCutTrans.text);
            //shortCutButton.onClick.AddListener(delegate () {
            //    ShortCutClick(i);
            //});
           // buttonList.Add(shortCutButton);
            StartCoroutine("Click",i+1);
            //int count = 0;
            //foreach (var a in buttonList) {
            //    a.onClick.AddListener(() => {
            //        DouniuChat chat = new DouniuChat();
            //        chat.setIndex(3);
            //        Debug.Log("i:" + count);
            //        chat.setChatContent(count.ToString());
            //        SocketMgr.GetInstance().Send(Net.instance.write(chat));
            //    });
            //    count++;
            //}
        }
    }
    //private void ShortCutClick(int i) {

    //}
    IEnumerator Click(int i) {
        shortCutButton.onClick.AddListener(delegate () {
            DouniuChat chat = new DouniuChat();
            chat.setIndex(3);
            Debug.Log("i:" + i);
            chat.setChatContent(i.ToString());
            SocketMgr.GetInstance().Send(Net.instance.write(chat));
            //ShortCutClick(i);
        });
        yield return new WaitForSeconds(0);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
