using com.guojin.mj.net.message;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using com.guojin.mj.net.message.game;
using System;

/* ***********************************************
 * Describe:聊天界面逻辑
 * Author :  MuLongFei
 * Email: 424113771@qq.com
 * DATA: 2017/7/18 16:15:54 
 * FileName: ChatPageManager 
 * Version: V1.0.1
 * ***********************************************/

namespace Assets.Scripts.module.view.Chat {

    class OnChatItemClickListener : MonoBehaviour,IPointerClickHandler {
        public string ID = "";
        public int msgType = 0;
        public void OnPointerClick(PointerEventData eventData) {
            Debug.Log("click emoji id :" + ID);
            PlayerSendChatInfo req = PlayerSendChatInfo.create(ID,msgType);
            SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(req));

            Destroy(gameObject.transform.root.FindChild("addNode/ChatPage").gameObject);

        }
    }

    class ChatPageManager :MonoBehaviour{
        private Button closeBtn;
        private Toggle emojiTl, usefulWordTl;
        private GameObject emojiPage, usefulWordPage;

        private void Start() {
            //初始化UI
            InitUI();

            //初始化表情
            InitNormalEmoji();

            //初始化常用语
            InitUsefulWord();
            
        }

        private void OnSwitchToggle(bool isOn) {
            emojiPage.SetActive(emojiTl.isOn);
            usefulWordPage.SetActive(usefulWordTl.isOn);

            emojiTl.transform.FindChild("Background").GetComponent<Image>().color = emojiTl.isOn ? new Color(1, 1, 1, 1) : new Color(0, 0, 0, 0.5f);
            usefulWordTl.transform.FindChild("Background").GetComponent<Image>().color = usefulWordTl.isOn ? new Color(1, 1, 1, 1) : new Color(0, 0, 0, 0.5f);
        }
        

        private void InitUI() {
            closeBtn = transform.FindChild("maskLayer").GetComponent<Button>();
            closeBtn.onClick.AddListener(OnCloseBtnClicked);
            emojiTl = transform.FindChild("Bg/ToggleGroup/ToggleEmoji").GetComponent<Toggle>();
            usefulWordTl = transform.FindChild("Bg/ToggleGroup/ToggleUsefulWord").GetComponent<Toggle>();
            emojiTl.onValueChanged.AddListener(OnSwitchToggle);
            usefulWordTl.onValueChanged.AddListener(OnSwitchToggle);

            emojiPage = transform.FindChild("Normal_Emojis").gameObject;
            usefulWordPage = transform.FindChild("UsefulWord").gameObject;
            usefulWordPage.SetActive(false);
            usefulWordTl.transform.FindChild("Background").GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
        }

        private void InitNormalEmoji() {
            for (int i = 1; i <=55; i++) {
                string path = "";
                if (i < 10) {
                    path = "00" + i;
                } else {
                    path = "0" + i;
                }

                GameObject emoji = Instantiate(transform.FindChild("emojiModel")).gameObject;
                emoji.GetComponent<Image>().sprite = Resources.Load<Sprite>("UIPicture/Emoji/emotion_" + path);
                emoji.transform.SetParent(transform.FindChild("Normal_Emojis/Viewport/Content"));
                emoji.transform.localPosition = Vector3.zero;
                emoji.transform.localScale = Vector3.one;
                emoji.name = path;
                emoji.AddComponent<OnChatItemClickListener>().ID = emoji.name;
                emoji.GetComponent<OnChatItemClickListener>().msgType = 2;
            }
        }

        private void InitUsefulWord() {
            Transform content = usefulWordPage.transform.FindChild("Scroll View/Viewport/Content");
            foreach (Transform item in content) {
                item.gameObject.AddComponent<OnChatItemClickListener>().ID = item.name;
                item.gameObject.GetComponent<OnChatItemClickListener>().msgType = 3;
            }
        }

        private void OnCloseBtnClicked() {
            Destroy(gameObject);
        }
    }
}
