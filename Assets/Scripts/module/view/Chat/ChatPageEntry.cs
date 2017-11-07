using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/* ***********************************************
 * Describe:聊天界面入口
 * Author :  MuLongFei
 * Email: 424113771@qq.com
 * DATA: 2017/7/18 16:00:21 
 * FileName: ChatPageEntry 
 * Version: V1.0.1
 * ***********************************************/

namespace Assets.Scripts.module.view.Chat {
    class ChatPageEntry : MonoBehaviour{

        private Transform root;
        private Button chatBtn;

        private const float msgInterval = 3.0f;
        private float lastClickTime = 0;
        private float nowClickTime = 0;
        private bool allowSendMsg = true;


        private void Start() {
            root = GameObject.Find("Canvas").transform;
            chatBtn = root.FindChild("ChoicePlayeWnd_Img/Chat_Btn").GetComponent<Button>();
            //chatBtn.transform.localScale *= 0.5F;
            chatBtn.transform.localPosition = new Vector3(601,-257, 0); 
            chatBtn.onClick.AddListener(OnChatBtnClicked);
        }

        private void OnChatBtnClicked() {
            //TooL.loadPrefab(root.FindChild("addNode").gameObject, "ChatPage").AddComponent<ChatPageManager>();

            if (allowSendMsg)
            {
                TooL.loadPrefab(root.FindChild("addNode").gameObject, "ChatPage").AddComponent<ChatPageManager>();
                allowSendMsg = false;
            }
            else
            {
                //提示
                if (GameObject.Find("MsgHint") == null)
                {
                    GameObject hint = TooL.loadPrefab(root.FindChild("addNode").gameObject, "MsgHint");
                    hint.transform.SetParent(root.FindChild("addNode"));
                    hint.transform.localScale *= 0.8F;
                    hint.transform.localPosition = new Vector3(0, -120, 0);
                    Destroy(hint, 1.0f);
                }
            }
        }

        private void Update()
        {
            lastClickTime += Time.deltaTime;
            if (lastClickTime - msgInterval >= 0)
            {
                allowSendMsg = true;
                lastClickTime = 0;
            }

        }
    }
}
