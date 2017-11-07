using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.BullFight.Data;
using com.guojin.mj.net.message;
using com.guojin.core.io.message;
using com.guojin.mj.net;
using com.guojin.dn.net.message;
using UnityEngine.SceneManagement;
using com.guojin.mj.net.message.login;
using System.Collections;

namespace com.guojin.dn.net.view {
    class DNJoinRoomPage : Observer{
        private string roomID = "";
        private Button btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9, btn10, btn11,btnClose,input1, input2, input3, input4, input5, input6;
        private Text noticeText;
        private List<Text> inputTextList = new List<Text>();
        private List<int> inputNumberList = new List<int>();
        private Transform inputTextTrans;
       // private Transform inputNumberTrans;

        private void Start() {
            InitUI();
            initMsg();
            //inputNumberList.Add(2);
            //inputNumberList.Add(2);
            SetTextNumber(inputNumberList, inputTextList);
        }

        protected override string[] GetMsgList() {
            return new string[] {
                MessageFactoryImpi.instance.getMessageString(7, 7),
            };
        }
        /// <summary>
        /// 接收到消息，比较传入的字符串和消息类型返回的字符串，决定进行哪种操作
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        public override void OnMsg(string msg, Message data) {
            if (msg == MessageFactoryImpi.instance.getMessageString(7, 7)) {
                JoinRoomRet resp = data as JoinRoomRet;
                if (resp.result&&resp.Type==1) {
                    Debug.Log("加入房间成功");
                    DNGlobalData.isFirstCreate = false;
                    DNGlobalData.roomID = roomStr;
                    SetTextNumber(inputNumberList,inputTextList);
                    UIManager.ChangeUI(UIManager.PageState.DNWaitPanel, (GameObject obj) => {
                        // obj.GetComponent<DNCreatRoomPage>().InformationSetting();
                    });
                    
                    Destroy(transform.parent.gameObject);
                }
            }
            if (msg == MessageFactoryImpi.instance.getMessageString(5, 24)) {
                DouniuGameRoomInfoRet resp = data as DouniuGameRoomInfoRet;
                DNGlobalData.gameInfo = resp;
            }
        }
       // int[] inputNum = new int[6];
        private void SetTextNumber(List<int> inputNumber,List<Text> showText ) {
            for (int i = 0; i < showText.Count; i++) {
                if (i < inputNumber.Count) {
                    showText[i].text = inputNumber[i].ToString();
                    if (i == showText.Count - 1) {
                        roomStr = "";
                        foreach (var a in inputNumber) {
                            roomStr += a.ToString();
                           // Debug.Log(roomStr);
                        }
                        //print(roomStr);
                        JoinRoom req = new JoinRoom();
                        req.roomCheckId = roomStr;
                        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(req));
                        StartCoroutine(InvokeClear());
                    }
                } else {
                    showText[i].text = null;
                }
            }
        }
        IEnumerator InvokeClear() {
            yield return new WaitForSeconds(0.7f);
            ClearTextNumber(inputNumberList, inputTextList);
        }
        private void ClearTextNumber(List<int> inputNumber, List<Text> showText) {
            inputNumber.Clear();
            for (int i = 0; i < showText.Count; i++) {
                showText[i].text = null;
            }
        }
        private void BackTextNumber(List<int> inputNumber,List<Text> showText) {
            if (inputNumber.Count > 0) {
                inputNumber.RemoveAt(inputNumber.Count - 1);
                SetTextNumber(inputNumber, showText);
            }
        }
        //Dictionary<strin,Button> inputButton = new List<Button>();
        /// <summary>
        /// 初始化UI
        /// </summary>
        private void InitUI() {
            inputTextTrans = transform.FindChild("InputText");
            //int childCount= inputTextTrans.childCount;
            for (int i = 0; i < inputTextTrans.childCount; i++) {
                inputTextList.Add(inputTextTrans.GetChild(i).GetChild(0).GetComponent<Text>());
            }

            //inputNumberTrans = transform.FindChild("Numbers");
            //for (int i = 0; i < inputNumberTrans.childCount; i++) {
            //    inputButton.Add(inputNumberTrans.GetChild(i).GetComponent<Button>());
            //}

            // = transform.FindChild("InputText/Text1").GetComponent<Text>();
            //roomText.text = "";
            btn0 = transform.FindChild("Numbers/BtnNum0").GetComponent<Button>();
            btn1 = transform.FindChild("Numbers/BtnNum1").GetComponent<Button>();
            btn2 = transform.FindChild("Numbers/BtnNum2").GetComponent<Button>();
            btn3 = transform.FindChild("Numbers/BtnNum3").GetComponent<Button>();
            btn4 = transform.FindChild("Numbers/BtnNum4").GetComponent<Button>();
            btn5 = transform.FindChild("Numbers/BtnNum5").GetComponent<Button>();
            btn6 = transform.FindChild("Numbers/BtnNum6").GetComponent<Button>();
            btn7 = transform.FindChild("Numbers/BtnNum7").GetComponent<Button>();
            btn8 = transform.FindChild("Numbers/BtnNum8").GetComponent<Button>();
            btn9 = transform.FindChild("Numbers/BtnNum9").GetComponent<Button>();
            btn10 = transform.FindChild("Numbers/BtnNum10").GetComponent<Button>();
            btn11 = transform.FindChild("Numbers/BtnNum11").GetComponent<Button>();
            btnClose = transform.FindChild("CloseBtn").GetComponent<Button>();
            noticeText = transform.FindChild("Notice").GetComponent<Text>();
            noticeText.text = "";
            btn0.onClick.AddListener(() => {UpDateRoomID(0);});
            btn1.onClick.AddListener(() => {UpDateRoomID(1);});
            btn2.onClick.AddListener(() => {UpDateRoomID(2);});
            btn3.onClick.AddListener(() => {UpDateRoomID(3);});
            btn4.onClick.AddListener(() => {UpDateRoomID(4);});
            btn5.onClick.AddListener(() => {UpDateRoomID(5);});
            btn6.onClick.AddListener(() => {UpDateRoomID(6);});
            btn7.onClick.AddListener(() => {UpDateRoomID(7);});
            btn8.onClick.AddListener(() => {UpDateRoomID(8);});
            btn9.onClick.AddListener(() => {UpDateRoomID(9);});

            btn10.onClick.AddListener(() => {
                transform.PlayButtonVoice();
                ClearTextNumber(inputNumberList,inputTextList);
                //roomText.text = "";
            });

            btn11.onClick.AddListener(() => {
                transform.PlayButtonVoice();
                BackTextNumber(inputNumberList, inputTextList);
            });

            btnClose.onClick.AddListener(()=> {
                transform.PlayButtonVoice();
                Destroy(transform.parent.gameObject);
            });
        }
        private void ClearNoticeText() {
            noticeText.text = "";
        }
        string roomStr;
        /// <summary>
        /// 输入加入房间的房间号
        /// </summary>
        /// <param name="num"></param>
        private void UpDateRoomID(int num) {

            transform.PlayButtonVoice();
            inputNumberList.Add(num);
            SetTextNumber(inputNumberList, inputTextList);
            //if (roomText.text.Length < 6) {
            //    roomText.text += num;

            //    if (roomText.text.Length == 6) {
            //        roomStr = roomText.text;
            //        print("房间号："+ roomStr);
            //        JoinRoom req = new JoinRoom();
            //        req.roomCheckId = roomStr;
            //        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(req));

            //        //DouniuJoinRoom req = DouniuJoinRoom.create(roomStr);
            //        ////向服务器发送消息，
            //        //SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(req));
            //        //DNGlobalData.isFirstCreate = false;
            //        //GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_LoadingPage);

            //    }
            //}
        }
    }
}
