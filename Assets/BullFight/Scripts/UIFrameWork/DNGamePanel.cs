using Assets.Scripts.BullFight.Data;
using Assets.Scripts.BullFight.Manager;
using com.guojin.dn.net.message;
using com.guojin.dn.player;
using com.guojin.mj.net;
using DG.Tweening;
using GameCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum UIButton {
    准备,
    加3注,
    加5注,
    加8注,
    加10注,
    聊天,
    设置,
    邀请,
    语音,
    //有牛,
    //没牛,
    提示,
    //退出,
    亮牌,
    抢1,
    抢2,
    抢3,
    抢4,
    一手,
    两手,
    继续,
    取消准备,
    开始,
    小邀请,
    灰亮牌,
    灰提示,
    灰色开始
    //三手
}
public class DNGamePanel : BasePanel {

    
    private Button zhunbeiBtn, yqBtn, setBtn, chatBtn, yuyinBtn, youniuBtn, 
        meiniuBtn,closeSettingButton, yazhu1, yazhu2, yazhu3, yazhu4, rob1, rob2, 
        rob3, rob4, exitButton, noticeButton, robPoker1,
        robPoker2,robPoker3,continueButton,cancelReady,startButton,littleInvite,grayNiu,grayTip;
    private GameObject player1, player2, player3, player4, player5, player6, doubleAndRob, pokerModel, pokers, bgPoker;
    private Text roomID, chapter, mode, noticeTextComp, fill1, fill2, fill3, fillResult;
    private Transform poker1, poker2, poker3, poker4, poker5, poker6, root, calTexts;
    private GameManager manager;
    private Image background;
    NiuType niuType1;
    NiuType niuType2;
    NiuType niuType3;
    private void Awake() {
        //
    }
    private void Start() {
        manager = GameManager.instance;
        InitUIButtonAndAction();
        if (SocketMgr.GetInstance() != null) {
            DouniuGameRoomInfo msg = new DouniuGameRoomInfo();
            SocketMgr.GetInstance().Send(Net.instance.write(msg));
        } else {
            DynamicUIManager.Instance.InitDynamicUIManager();
        }
        background = GameObject.Find("BackGround").GetComponent<Image>();
        switch (PlayerPrefs.GetInt("cloth")) {
            case 1:
                background.sprite = DynamicUIManager.Instance.BaseNameGetSprite("bg_3");
                //toggle1.isOn = true;
                break;
            case 2:
                background.sprite = DynamicUIManager.Instance.BaseNameGetSprite("bg_2");
                // toggle2.isOn = true;
                break;
            case 3:
                background.sprite = DynamicUIManager.Instance.BaseNameGetSprite("bg_1");
                //toggle3.isOn = true;
                break;
            default:
                background.sprite = DynamicUIManager.Instance.BaseNameGetSprite("bg_3");
                // toggle1.isOn = true;
                break;
        }
        manager.InitOperation();
        manager.ShowButton(UIButton.设置);
        manager.ShowButton(UIButton.聊天);
        manager.ShowButton(UIButton.语音);
        //manager.ShowButton(UIButton.退出);
        DNGlobalData.isFirstIn = false;
    }
    GameObject instPanel;
    private void Update() {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            if (DNGlobalData.show) {

                //DNUIManager.Instance.PushPanel(UIPanelType.ExitPanel);
                instPanel=DNUIManager.Instance.JustGetPanel(UIPanelType.ExitPanel);
                //string path;
                //path = "PanelPrefabs/ExitPanel";
                ////Debug.Log(path);
                ////panelPathDict.TryGetValue(panelType, out path);
                //instPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject;
                //instPanel.transform.SetParent(GameObject, false);
                //instPanel.name = instPanel.name.Replace("(Clone)", "");
                DNGlobalData.show = false;
            } else {
                Destroy(instPanel);
                //DNUIManager.Instance.PopPanel();
                DNGlobalData.show = true;
            }
        }
    }

    public override void OnExit() {
        gameObject.SetActive(true);
    }
    private void InitUIButtonAndAction() {
        doubleAndRob = transform.FindChild("DoubleAndRob").gameObject;

        root = GameObject.Find("Canvas/addNode/DNGamePanel").transform;
        zhunbeiBtn = transform.FindChild("GameButtons/zbBtn").GetComponent<Button>();
        noticeTextComp = root.transform.FindChild("Table/NoticeBG/NoticeText").GetComponent<Text>();
        yqBtn = transform.FindChild("GameButtons/yqBtn").GetComponent<Button>();
        littleInvite = transform.FindChild("GameButtons/LittleInvite").GetComponent<Button>();
        setBtn = transform.FindChild("GameButtons/setBtn").GetComponent<Button>();
        chatBtn = transform.FindChild("GameButtons/chatBtn").GetComponent<Button>();
        yuyinBtn =transform.FindChild("GameButtons/yuyinBtn").GetComponent<Button>();
        youniuBtn = transform.FindChild("GameButtons/youniu").GetComponent<Button>();
        meiniuBtn =transform.FindChild("GameButtons/meiniu").GetComponent<Button>();
        grayNiu = transform.FindChild("GameButtons/grayNiu").GetComponent<Button>();
        grayTip = transform.FindChild("GameButtons/grayTip").GetComponent<Button>();
        yazhu1 = doubleAndRob.transform.FindChild("1").GetComponent<Button>();
        yazhu2 = doubleAndRob.transform.FindChild("2").GetComponent<Button>();
        yazhu3 = doubleAndRob.transform.FindChild("3").GetComponent<Button>();
        yazhu4 = doubleAndRob.transform.FindChild("4").GetComponent<Button>();
        calTexts = root.FindChild("CalTexts");

        zhunbeiBtn.onClick.AddListener(() => {
            zhunbeiBtn.PlayButtonVoice();
            if (zhunbeiBtn.gameObject.activeSelf) {
                noticeTextComp.text = "";
                DouniuStartGame req = new DouniuStartGame();
                req.IsReady = true;
                SocketMgr.GetInstance().Send(Net.instance.write(req));
                GameManager.instance.ShowButton(UIButton.取消准备);
                if (DNGlobalData.roomPlayerNumber < 5) {
                    GameManager.instance.ShowButton(UIButton.邀请);
                }
            }
            GameManager.instance.HideButton(UIButton.准备);
        });
        yqBtn.onClick.AddListener(() => {
            yqBtn.PlayButtonVoice();

        });
        littleInvite.onClick.AddListener(() => {

        });
        setBtn.onClick.AddListener(() => {
            setBtn.PlayButtonVoice();
            DNUIManager.Instance.PushPanel(UIPanelType.SettingPanel);
        });
        chatBtn.onClick.AddListener(() => {
            chatBtn.PlayButtonVoice();
            DNUIManager.Instance.PushPanel(UIPanelType.ChatPanel);
        });
        youniuBtn.onClick.AddListener(() => {
            youniuBtn.PlayButtonVoice();
            manager.CountDownOperation("End");
            //manager.DOTweenOperation();
            manager.HideButton(UIButton.灰亮牌);
            manager.HideButton(UIButton.提示);
            manager.HideButton(UIButton.灰提示);
            manager.HideButton(UIButton.两手);
            manager.noticeBG.enabled = true;
            manager.noticeTextComp.text = "正在等待其他玩家亮牌";
            //foreach (var a in manager.handPoker2) {
            //    a.DOLocalMoveY(40, 0.2f);
            //}
            //manager.countDown.SetActive(false);
            CountDownPanel.ShutDown = true;
        });
        //bool confirmOutSingle = false;
        //bool confirmOutDouble = false;
        meiniuBtn.onClick.AddListener(() => {
            meiniuBtn.PlayButtonVoice();
            if (DNGlobalData.waitEnd) {
                if (manager.handPoker2 == null || manager.handPoker2.Count == 0) {
                    niuType3 = manager.CalNiuType(manager.handPoker);
                    foreach (var a in manager.handPoker) {
                        Poker tempPoker = a.GetComponent<Poker>();
                        if (tempPoker != null) {
                            Destroy(tempPoker);
                        }
                    }
                    StartCoroutine(WaitShowPoker3(0));
                    manager.HideButton(UIButton.灰亮牌);
                    manager.HideButton(UIButton.提示);
                    manager.ShowButton(UIButton.灰提示);
                    manager.ShowButton(UIButton.亮牌);
                    DNGlobalData.waitEnd = false;
                    DNGlobalData.hintDouble = false;
                    DNGlobalData.hintSingle = true;
                    DNGlobalData.confirmOutSingle = true;
                } else {
                    //if (DNGlobalData.hintSingle) {
                    //    niuType1 = manager.CalNiuType(manager.handPoker);
                    //    StartCoroutine(WaitShowPoker1(0));
                    //    DNGlobalData.hintDouble = true;
                    //    DNGlobalData.hintSingle = false;
                    //    confirmOutSingle = true;
                    //}
                    if (DNGlobalData.hintDouble) {
                        niuType2 = manager.CalNiuType(manager.handPoker2);
                        StartCoroutine(WaitShowPoker2(0));
                        foreach (var a in manager.handPoker2) {
                            Poker tempPoker = a.GetComponent<Poker>();
                            if (tempPoker != null) {
                                Destroy(tempPoker);
                            }
                        }
                        DNGlobalData.hintSingle = true;
                        DNGlobalData.hintDouble = false;
                        DNGlobalData.confirmOutDouble = true;
                    } else if (DNGlobalData.hintSingle || !DNGlobalData.hintDouble) {
                        niuType1 = manager.CalNiuType(manager.handPoker);
                        StartCoroutine(WaitShowPoker1(0));
                        foreach (var a in manager.handPoker) {
                            Poker tempPoker = a.GetComponent<Poker>();
                            if (tempPoker != null) {
                                Destroy(tempPoker);
                            }
                        }
                        DNGlobalData.hintDouble = true;
                        DNGlobalData.hintSingle = false;
                        DNGlobalData.confirmOutSingle = true;
                    }
                    if (DNGlobalData.confirmOutDouble && DNGlobalData.confirmOutSingle) {
                        DNGlobalData.confirmOutSingle = false;
                        DNGlobalData.confirmOutDouble = false;
                        DNGlobalData.hintSingle = true;
                        DNGlobalData.hintDouble = false;
                        DNGlobalData.waitEnd = false;
                        manager.HideButton(UIButton.灰亮牌);
                        manager.HideButton(UIButton.提示);
                        manager.ShowButton(UIButton.灰提示);
                        manager.ShowButton(UIButton.亮牌);
                    }
                    //if (DNGlobalData.hintDouble) {
                    //    niuType2 = manager.CalNiuType(manager.handPoker2);
                    //    StartCoroutine(WaitShowPoker2(2));
                    //    DNGlobalData.hintDouble = false;
                    //    DNGlobalData.hintSingle = true;
                    //    DNGlobalData.waitEnd = false;
                    //    manager.HideButton(UIButton.灰亮牌);
                    //    manager.HideButton(UIButton.提示);
                    //    manager.ShowButton(UIButton.灰提示);
                    //    manager.ShowButton(UIButton.亮牌);
                    //} else if(DNGlobalData.hintSingle|| !DNGlobalData.hintDouble) {
                    //    niuType1 = manager.CalNiuType(manager.handPoker);
                    //    StartCoroutine(WaitShowPoker1(2));
                    //    DNGlobalData.hintDouble = true;
                    //    DNGlobalData.hintSingle = false;
                    //   // firstOutHint = false;
                    //}
                }
            }
        });
        yazhu1.onClick.AddListener(() => {
            yazhu1.PlayButtonVoice();
            Debug.Log("闲家押注：" + 1);
            DNGlobalData.yazhuNumber = 1;
            //GameManager.noticeTextComp.text = "请等待其他玩家押注";
            //GameManager.instance.HideAllJiaZhuButton();
            GameManager.instance.CountDownOperation("NowYaZhu");
            manager.noticeBG.enabled = true;
            manager.noticeTextComp.text = "正在等待其他玩家押注";
            //manager.countDown.SetActive(false);
            CountDownPanel.ShutDown = true;
            GameManager.instance.HideAllJiaZhuButton();
        });
        yazhu2.onClick.AddListener(() => {
            yazhu2.PlayButtonVoice();
            Debug.Log("闲家押注：" + 2);
            DNGlobalData.yazhuNumber = 2;
            //GameManager.noticeTextComp.text = "请等待其他玩家押注";
            GameManager.instance.CountDownOperation("NowYaZhu");
            manager.noticeBG.enabled = true;
            manager.noticeTextComp.text = "正在等待其他玩家押注";
            //manager.countDown.SetActive(false);
            CountDownPanel.ShutDown = true;
            GameManager.instance.HideAllJiaZhuButton();
            //GameManager.instance.HideAllJiaZhuButton();
        });
        yazhu3.onClick.AddListener(() => {
            yazhu3.PlayButtonVoice();
            Debug.Log("闲家押注：" + 3);
            DNGlobalData.yazhuNumber = 3;
           // GameManager.noticeTextComp.text = "请等待其他玩家押注";
            GameManager.instance.CountDownOperation("NowYaZhu");
            manager.noticeBG.enabled = true;
            manager.noticeTextComp.text = "正在等待其他玩家押注";
            //manager.countDown.SetActive(false);
            CountDownPanel.ShutDown = true;
            GameManager.instance.HideAllJiaZhuButton();
        });
        yazhu4.onClick.AddListener(() => {
            yazhu4.PlayButtonVoice();
            Debug.Log("闲家押注：" + 4);
            DNGlobalData.yazhuNumber = 4;
            //GameManager.noticeTextComp.text = "请等待其他玩家押注";
            GameManager.instance.CountDownOperation("NowYaZhu");
            manager.noticeBG.enabled = true;
            manager.noticeTextComp.text = "正在等待其他玩家押注";
            // manager.countDown.SetActive(false);
            CountDownPanel.ShutDown = true;
            GameManager.instance.HideAllJiaZhuButton();
        });

        //rob1 = doubleAndRob.transform.FindChild("Rob1").GetComponent<Button>();
        //rob1.onClick.AddListener(() => {
        //    DNGlobalData.robNumber = 1;
        //    //GameManager.noticeTextComp.text = "请等待其他玩家押注";
        //    GameManager.instance.CountDownOperation("NowRob");
        //    CountDownPanel.Instance.ShutDown = true;
        //});
        //rob2 = doubleAndRob.transform.FindChild("Rob2").GetComponent<Button>();
        //rob2.onClick.AddListener(() => {
        //    DNGlobalData.robNumber = 2;
        //    //GameManager.noticeTextComp.text = "请等待其他玩家押注";
        //    GameManager.instance.CountDownOperation("NowRob");
        //    CountDownPanel.Instance.ShutDown = true;
        //});
        //rob3 = doubleAndRob.transform.FindChild("Rob3").GetComponent<Button>();
        //rob3.onClick.AddListener(() => {
        //    DNGlobalData.robNumber = 3;
        //    //GameManager.noticeTextComp.text = "请等待其他玩家押注";
        //    GameManager.instance.CountDownOperation("NowRob");
        //    CountDownPanel.Instance.ShutDown = true;
        //});
        //rob4 = doubleAndRob.transform.FindChild("Rob4").GetComponent<Button>();
        //rob4.onClick.AddListener(() => {
        //    DNGlobalData.robNumber = 4;
        //   // GameManager.noticeTextComp.text = "请等待其他玩家押注";
        //    GameManager.instance.CountDownOperation("NowRob");
        //    CountDownPanel.Instance.ShutDown = true;
        //});
        robPoker1 = doubleAndRob.transform.FindChild("OnePokers").GetComponent<Button>();
        robPoker1.onClick.AddListener(()=> {
            robPoker1.PlayButtonVoice();
            DNGlobalData.handPokerNum = 1;
            //DouniuCasiPai pokerCount = new DouniuCasiPai();
            //pokerCount.setPaiLens(1);
            //SocketMgr.GetInstance().Send(Net.instance.write(pokerCount));
            CountDownPanel.ShutDown = true;
            manager.noticeBG.enabled = true;
            manager.noticeTextComp.text = "正在等待其他玩家选择手牌数量";
            GameManager.instance.CountDownOperation("NowRobHandPoker");
            //manager.HideAllRobPokerButton();
        });
        robPoker2 = doubleAndRob.transform.FindChild("TwoPokers").GetComponent<Button>();
        robPoker2.onClick.AddListener(()=> {
            robPoker2.PlayButtonVoice();
            DNGlobalData.handPokerNum = 2;
            //DouniuCasiPai pokerCount = new DouniuCasiPai();
            //pokerCount.setPaiLens(2);
            //SocketMgr.GetInstance().Send(Net.instance.write(pokerCount));
            CountDownPanel.ShutDown = true;
            manager.noticeBG.enabled = true;
            manager.noticeTextComp.text = "正在等待其他玩家选择手牌数量";
            GameManager.instance.CountDownOperation("NowRobHandPoker");
            //manager.HideAllRobPokerButton();
        });
        startButton = transform.FindChild("GameButtons/StartButton").GetComponent<Button>();
        startButton.onClick.AddListener(() => {
            startButton.PlayButtonVoice();
            noticeTextComp.text = "";
            DouniuStartGame req = new DouniuStartGame();
            req.IsReady = true;
            SocketMgr.GetInstance().Send(Net.instance.write(req));
            //GameManager.noticeTextComp.text = "请等待其他玩家准备";
            GameManager.instance.HideButton(UIButton.开始);
            //GameManager.instance.HideButton(UIButton.取消准备);
            //if (DNGlobalData.roomPlayerNumber < 5) {
            //    GameManager.instance.ShowButton(UIButton.小邀请);
            //}
        });
        continueButton = transform.FindChild("GameButtons/ContinueButton").GetComponent<Button>();
        continueButton.onClick.AddListener(()=> {
            continueButton.PlayButtonVoice();
            noticeTextComp.text = "";
            manager.CountDownOperation("NextChapter");
            manager.noticeBG.enabled = true;
            manager.noticeTextComp.text = "正在等待其他玩家继续游戏";
            CountDownPanel.ShutDown = true;
            //DouniuStartGame req = new DouniuStartGame();
            //req.IsReady = true;
            //SocketMgr.GetInstance().Send(Net.instance.write(req));
            //GameManager.noticeTextComp.text = "请等待其他玩家准备";
            //GameManager.instance.HideButton(UIButton.取消准备);
            //if (DNGlobalData.roomPlayerNumber < 5) {
            //    GameManager.instance.ShowButton(UIButton.邀请);
            //}
        });
        cancelReady = transform.FindChild("GameButtons/CancelReady").GetComponent<Button>();
        cancelReady.onClick.AddListener(() => {
            cancelReady.PlayButtonVoice();
            noticeTextComp.text = "";
            DouniuStartGame req = new DouniuStartGame();
            req.IsReady = false;
            SocketMgr.GetInstance().Send(Net.instance.write(req));
            //GameManager.noticeTextComp.text = "请等待其他玩家准备";
            GameManager.instance.HideButton(UIButton.取消准备);
            if (DNGlobalData.roomPlayerNumber < 5) {
                GameManager.instance.ShowButton(UIButton.邀请);
            }
            if (DNGlobalData.currentChapter >= 1) {
                GameManager.instance.ShowButton(UIButton.继续);
            } else {
                GameManager.instance.ShowButton(UIButton.准备);
            }
        });

        manager. uiButtonDict.Add(UIButton.准备, zhunbeiBtn);
        manager.uiButtonDict.Add(UIButton.加3注, yazhu1);
        manager.uiButtonDict.Add(UIButton.加5注, yazhu2);
        manager.uiButtonDict.Add(UIButton.加8注, yazhu3);
        manager.uiButtonDict.Add(UIButton.加10注, yazhu4);
        manager.uiButtonDict.Add(UIButton.聊天, chatBtn);
        manager.uiButtonDict.Add(UIButton.设置, setBtn);
        manager.uiButtonDict.Add(UIButton.邀请, yqBtn);
        manager.uiButtonDict.Add(UIButton.语音, yuyinBtn);
        //manager.uiButtonDict.Add(UIButton.有牛, youniuBtn);
        manager.uiButtonDict.Add(UIButton.提示, meiniuBtn);
        manager.uiButtonDict.Add(UIButton.抢1, rob1);
        manager.uiButtonDict.Add(UIButton.抢2, rob2);
        manager.uiButtonDict.Add(UIButton.抢3, rob3);
        manager.uiButtonDict.Add(UIButton.抢4, rob4);
        manager.uiButtonDict.Add(UIButton.一手, robPoker1);
        manager.uiButtonDict.Add(UIButton.两手, robPoker2);
        //GameManager.uiButtonDict.Add(UIButton.三手, robPoker3);
        //manager.uiButtonDict.Add(UIButton.退出, exitButton);
        manager.uiButtonDict.Add(UIButton.亮牌, youniuBtn);
        manager.uiButtonDict.Add(UIButton.继续, continueButton);
        manager.uiButtonDict.Add(UIButton.取消准备, cancelReady);
        manager.uiButtonDict.Add(UIButton.小邀请, littleInvite);
        manager.uiButtonDict.Add(UIButton.开始, startButton);
        manager.uiButtonDict.Add(UIButton.灰亮牌, grayNiu);
        manager.uiButtonDict.Add(UIButton.灰提示, grayTip);
    }
    IEnumerator WaitShowPoker3(float time) {
        manager.GetDictNiuType3(0).SetActive(true);
        manager.GetDictNiuType3(0).transform.GetChild(0).GetComponent<Image>().sprite =
DynamicUIManager.Instance.BaseNiuTypeGetSprite(niuType3);
        //manager.GetDictNiuType3(0).transform.GetChild(0).DOPunchScale(new Vector3(0.3f, 0, 0), 0.5f);
        yield return new WaitForSeconds(time);
        //manager.DOTweenRotateHide(manager.handPoker);
        //manager.DOTweenRotateShow(manager.handPoker2);
        //manager.GetDictDone(0).enabled = true;
    }
    IEnumerator WaitShowPoker1(float time) {
        manager.GetDictNiuType1(0).SetActive(true);
        manager.GetDictNiuType1(0).transform.GetChild(0).GetComponent<Image>().sprite =
DynamicUIManager.Instance.BaseNiuTypeGetSprite(niuType1);
        //manager.GetDictNiuType1(0).transform.GetChild(0).DOPunchScale(new Vector3(0.3f, 0, 0), 0.5f);
        yield return new WaitForSeconds(time);
        //manager.DOTweenRotateHide(manager.handPoker);
        //manager.DOTweenRotateShow(manager.handPoker2);
        //manager.GetDictDone(0).enabled = true;
    }
    IEnumerator WaitShowPoker2(float time) {
        manager.GetDictNiuType2(0).SetActive(true);
        manager.GetDictNiuType2(0).transform.GetChild(0).GetComponent<Image>().sprite =
DynamicUIManager.Instance.BaseNiuTypeGetSprite(niuType2);
        //transform.DOPunchScale(new Vector3(0.3f, 0, 0), 0.8f);
        //manager.GetDictNiuType2(0).transform.GetChild(0).DOPunchScale(new Vector3(0.3f, 0, 0), 0.5f);
        yield return new WaitForSeconds(time);
        //manager.DOTweenRotateHide(manager.handPoker2);
        //manager.DOTweenRotateShow(manager.handPoker2);
        //manager.GetDictDone2(0).enabled = true;
    }
}
