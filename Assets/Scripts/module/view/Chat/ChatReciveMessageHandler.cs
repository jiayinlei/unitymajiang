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

/* ***********************************************
 * Describe:处理接收到的聊天信息
 * Author :  MuLongFei
 * Email: 424113771@qq.com
 * DATA: 2017/7/18 17:03:57 
 * FileName: ChatReciveMessageHandler 
 * Version: V1.0.1
 * ***********************************************/

namespace Assets.Scripts.module.view.Chat {
    class ChatReciveMessageHandler : Observer{

        private void Start() {
            initMsg();
        }

        protected override string[] GetMsgList() {
            return new string[] {
                MessageFactoryImpi.instance.getMessageString(1,25)
            };
        }

        public override void OnMsg(string msg, Message data) {
            PlayerReceiveChatInfo message = data as PlayerReceiveChatInfo;
            PlayerReceiveChatInfo resp = PlayerReceiveChatInfo.create(message.UserIndex,message.ReceiveChatInfo,message.Num);
            switch (resp.Num) {
                case 1:
                    //处理文本

                    break;
                case 2:
                    //处理表情
                    SetEmoji(resp.UserIndex,resp.ReceiveChatInfo);
                    break;
                case 3:
                    //处理语音
                    SetUsefulWord(resp.UserIndex,resp.ReceiveChatInfo);
                    break;
            }

            Debug.Log(data.toString());
        }

        private void SetEmoji(int index,string emojiID) {
            Transform target = transform.Find(("HeadPhoto" + index));
            GameObject emoji = new GameObject();
            emoji.name = "emoji";
            emoji.AddComponent<Image>().sprite = Resources.Load<Sprite>("UIPicture/Emoji/emotion_" + emojiID);

            emoji.transform.SetParent(target);
            emoji.transform.localScale = Vector3.one * 0.5f;
            
            if (target.localPosition.x > 0) {
                emoji.transform.localPosition = Vector3.zero + new Vector3(-25, -25, 0);
            } else {
                emoji.transform.localPosition = Vector3.zero + new Vector3(25, 25, 0);
            }

            emoji.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            emoji.GetComponent<Image>().DOFade(1, 0.5f);

            Destroy(emoji, 2.0f);
        }


        private void SetUsefulWord(int index, string wordID) {
            Transform target = transform.Find(("HeadPhoto" + index));
            
            //播放音效
            AudioSource voice = gameObject.AddComponent<AudioSource>();
            voice.clip = Resources.Load<AudioClip>("Music(UnUsed)/UsefulWord/" + wordID);
            voice.volume = 5;
            if (PlayerPrefs.GetFloat("AudioSet", 1f)>=0.1f)
            {
                voice.Play();
            }

            float t = voice.clip.length;
            Destroy(voice, t);

            //显示喇叭图标
            GameObject horn = new GameObject();
            horn.name = "horn";
            horn.AddComponent<Image>().sprite = Resources.Load<Sprite>("NewUIPicture/voice_icon");
            horn.transform.SetParent(target);

            if (target.localPosition.x > 0) {
                horn.transform.localPosition = Vector3.zero + new Vector3(-25, -25, 0);
                horn.transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
            } else {
                horn.transform.localPosition = Vector3.zero + new Vector3(25, 25, 0);
                horn.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }

            horn.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            horn.GetComponent<Image>().DOFade(1, 0.5f).SetLoops(-1,LoopType.Yoyo);
            Destroy(horn, t);
        }
    }
}
