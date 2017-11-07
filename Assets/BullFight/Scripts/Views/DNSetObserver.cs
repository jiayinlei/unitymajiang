using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.guojin.core.io.message;
using UnityEngine.UI;
using UnityEngine;
using com.guojin.mj.net.message.login;
using com.guojin.mj.net.message;
//using com.guojin.dn.net.message;
using com.guojin.mj.net;
using Assets.Scripts.BullFight.Data;
using com.guojin.dn.net.message;
using GameCore;
using UnityEngine.SceneManagement;
/* ***********************************************
* Describe:该类功能描述
* Author :  MuLongFei
* Email: 424113771@qq.com
* DATA: 2017/7/28 14:20:53 
* FileName: DNRoomSetPage 
* Version: V1.0.1
* ***********************************************/

namespace com.guojin.dn.net.view {
    class DNSetObserver : Observer {
        
        private string mode="1";
        private string jushu="10";
        private string changMode="1";
        private string handPoker="1";
        private string kingMode="1";
        private string isCheck="1";
        private string playerNumber = "2";

        private void Start() {
            //
            transform.Find("CreatRoom/juNum/Grid/1").GetComponent<Toggle>().onValueChanged.AddListener((bool isOn)=> {
                transform.PlayButtonVoice();
                if (isOn) {
                    jushu = "10";
                }
            });
            transform.Find("CreatRoom/juNum/Grid/2").GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => {
                transform.PlayButtonVoice();
                if (isOn) {
                    jushu = "20";
                }
            });
            transform.Find("CreatRoom/juNum/Grid/3").GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => {
                transform.PlayButtonVoice();
                if (isOn) {
                    jushu = "30";
                }
            });

            //
            transform.Find("CreatRoom/zhuangMode/Grid/1").GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => {
                transform.PlayButtonVoice();
                if (isOn) {
                    mode = "1";
                }
            });
            transform.Find("CreatRoom/zhuangMode/Grid/2").GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => {
                transform.PlayButtonVoice();
                if (isOn) {
                    mode = "2";
                }
            });
            //transform.Find("CreatRoom/zhuangMode/Grid/3").GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => {
            //    if (isOn) {
            //        mode = "3";
            //    }
            //});

            //
            transform.Find("CreatRoom/changeMode/Grid/1").GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => {
                transform.PlayButtonVoice();
                if (isOn) {
                    changMode = "1";
                }
            });
            transform.Find("CreatRoom/changeMode/Grid/2").GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => {
                transform.PlayButtonVoice();
                if (isOn) {
                    changMode = "2";
                }
            });
            transform.Find("CreatRoom/changeMode/Grid/3").GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => {
                transform.PlayButtonVoice();
                if (isOn) {
                    changMode = "3";
                }
            });

            //
            transform.Find("CreatRoom/yaZhu/Grid/1").GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => {
                transform.PlayButtonVoice();
                if (isOn) {
                    isCheck = "1";
                }
            });
            transform.Find("CreatRoom/yaZhu/Grid/2").GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => {
                transform.PlayButtonVoice();
                if (isOn) {
                    isCheck = "2";
                }
            });

            //
            transform.Find("CreatRoom/kingMode/Grid/1").GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => {
                transform.PlayButtonVoice();
                if (isOn) {
                    kingMode = "1";
                }
            });
            transform.Find("CreatRoom/kingMode/Grid/2").GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => {
                transform.PlayButtonVoice();
                if (isOn) {
                    kingMode = "2";
                }
            });
            transform.Find("CreatRoom/playerNumber/Grid/2").GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => {
                transform.PlayButtonVoice();
                if (isOn) {
                    playerNumber = "2";
                }
            });
            transform.Find("CreatRoom/playerNumber/Grid/3").GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => {
                transform.PlayButtonVoice();
                if (isOn) {
                    playerNumber = "3";
                }
            });
            transform.Find("CreatRoom/playerNumber/Grid/4").GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => {
                transform.PlayButtonVoice();
                if (isOn) {
                    playerNumber = "4";
                }
            });
            //transform.Find("CreatRoom/handPoker/one").GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => {
            //    if (isOn) {
            //        handPoker = "1";
            //    }
            //});
            transform.FindChild("CreatRoom/CreatroomBtn").GetComponent<Button>().onClick.AddListener(()=> {
                transform.PlayButtonVoice();
                List<OptionEntry> list = new List<OptionEntry>();
                list.Add(OptionEntry.create("jushu", jushu));//局数
                list.Add(OptionEntry.create("moShi", mode));//坐庄模式
                list.Add(OptionEntry.create("isCheck", isCheck));//是否看牌
                list.Add(OptionEntry.create("fangShi", changMode));//换庄方式
                list.Add(OptionEntry.create("playerNum", playerNumber));//玩家数量
                DNGlobalData.maxPlayerNumber = playerNumber;
                //list.Add(OptionEntry.create("fangShi", f));//看牌
                DouniuCreateRoom CR = new DouniuCreateRoom();
                CR.options = list;
                CR.profile = "dn";
                SocketMgr.GetInstance().Send(Net.instance.write(CR));
            });
            transform.FindChild("CreatRoom/CloseBtn").GetComponent<Button>().onClick.AddListener(()=> {
                transform.PlayButtonVoice();
                Destroy(gameObject);
            });
            //transform.FindChild("CreatRoom/BackButton").GetComponent<Button>().onClick.AddListener(() => {
            //    Destroy(gameObject);
            //});
            transform.FindChild("CreatRoom/Rule").GetComponent<Button>().onClick.AddListener(() => {
                transform.PlayButtonVoice();
                //Destroy(gameObject);
                GameObject obj=Instantiate(Resources.Load<GameObject>("BullFightPrefab/DNRulePanl"));
                obj.transform.SetParent(gameObject.transform);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;
            });

            initMsg();
        }
        
        protected override string[] GetMsgList() {
            return new string[] {
                  MessageFactoryImpi.instance.getMessageString(5,1),
                  MessageFactoryImpi.instance.getMessageString(7,7),
            };
        }
        bool isFirst = true;
        /// <summary>
        /// 接收到消息，比较传入的字符串和消息类型返回的字符串，决定进行哪种操作
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        public override void OnMsg(string msg, Message data) {
            //创建房间的结果解析（获取创建房间响应信息），result值为true，创建新房间，并接收房间ID；
            if (msg == MessageFactoryImpi.instance.getMessageString(5,1)) {
                DouniuCreateRoomRet resp = data as DouniuCreateRoomRet;
                if (resp.result) {
                    Debug.Log("创建房间成功 房间ID： " + resp.roomCheckId);
                    DNGlobalData.roomID=resp.roomCheckId;
                    DNGlobalData.isFirstCreate = true;
                    JoinRoom req = new JoinRoom();
                    req.roomCheckId = resp.roomCheckId;
                    SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(req));

                } else {
                    DNGlobalData.isFirstCreate = false;
                    Debug.Log("创建房间失败");
                }
            }
            //加入房间的结果解析（获取加入房间响应信息），result值为true，加入房间，并接收房间ID；
            if (msg == MessageFactoryImpi.instance.getMessageString(7,7)) {
                JoinRoomRet resp = data as JoinRoomRet;

                if (resp.result&&resp.Type==1) {
                    Debug.Log("加入房间成功");
                    Destroy(transform.parent.gameObject);
                    UIManager.ChangeUI(UIManager.PageState.DNWaitPanel, (GameObject obj) => {
                        // obj.GetComponent<DNCreatRoomPage>().InformationSetting();
                    });
                    if (isFirst){
                        DNGlobalData.isFirstCreate = true;
                    };
                } else {
                    Debug.Log("加入房间失败");
                }
            }
        }
    }
}
