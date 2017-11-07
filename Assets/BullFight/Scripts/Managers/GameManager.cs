using Assets.Scripts.BullFight.Data;
using com.guojin.core.utils;
using com.guojin.dn.net.message;
using com.guojin.dn.player;
using com.guojin.mj.net;
using GameCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.BullFight.Manager {
    public class GameManager : Singleton<GameManager> {
        //[HideInInspector]
        //public  Text noticeTextComp;
        [HideInInspector]
        public static int outCount = 0;
        private int num1, num2, num3, fillIndex, playerIndex;
        public GameObject bgPoker, doubleAndRob, gameManagerObj, player1, player5, player3, player4, player2, pokerModel, pokers, countDown, changeNoticeZhuang, changeNoticeOthers;

        public Transform calTexts, poker1, poker2, poker3, poker4, poker5, root, sceneObjects;
        private AudioSource audioSource, transSource;
        public Text chapterText, fill1, fill2, fill3, fillResult, mode, roomID;
        private Button chatBtn, exitButton, meiniuBtn, noticeButton, rob1, rob2, rob3, rob4, setBtn,
            showPokerButton, yazhu1, yazhu2, yazhu3, yazhu4, youniuBtn, yqBtn, yuyinBtn, zhunbeiBtn;
        public Dictionary<int, PlayerMessageComponent> playerDict = new Dictionary<int, PlayerMessageComponent>();
        public Dictionary<string, GameObject> pokerDict = new Dictionary<string, GameObject>();
        public Dictionary<UIButton, Button> uiButtonDict = new Dictionary<UIButton, Button>();
        // public Dictionary<int, int> paiCountDic = new Dictionary<int, int>();
        public Dictionary<int, DouniuCompareResult> compareResultDict = new Dictionary<int, DouniuCompareResult>();
        public List<GameObject> pokerFPList;
        public List<GameObject> pokerHTList;
        public List<GameObject> pokerHXList;
        public List<GameObject> pokerMHList;
        public List<int> robPokerCountList = new List<int>();
        //public  List<int> userIDList = new List<int>();
        public List<Vector3> localPostion = new List<Vector3>();
        public List<Transform> fillList = new List<Transform>();
        public List<Transform> handPoker = new List<Transform>();
        public List<Transform> handPoker2 = new List<Transform>();
        public List<Transform> final = new List<Transform>();
        public List<Transform> final2 = new List<Transform>();
        public List<DouniuGameChapterEnd> chapterEndList = new List<DouniuGameChapterEnd>();
        private string[] fill = new string[] { "", "", "", "" };
        public int[] positionArr;
        public Text noticeTextComp;
        public Image noticeBG;
        public GameManager() {
        }
        public void InitOperation() {
            Debug.Log("初始化GameManager");
            //gameManagerObj = GameObject.Find("GameManager");
            string sceneName = SceneManager.GetActiveScene().name;
            if (sceneName == "DNPlayBack" || sceneName == "Login" || sceneName == "GameHall") {
                Debug.Log(GameObject.Find("Canvas"));
                Debug.Log(GameObject.Find("Canvas/addNode"));
                root = GameObject.Find("Canvas/addNode/DNReferGamePanel(Clone)").transform;

            } else {
                root = GameObject.Find("Canvas/addNode/DNGamePanel").transform;
                transSource = GameObject.Find("GameManager").GetComponent<AudioSource>();
            }
            sceneObjects = root.FindChild("Players/SceneObjects");
            calTexts = root.transform.FindChild("CalTexts");
            calTexts.gameObject.SetActive(false);
            fill1 = calTexts.FindChild("Fill1/Fill").GetComponent<Text>();
            fill2 = calTexts.FindChild("Fill2/Fill").GetComponent<Text>();
            fill3 = calTexts.FindChild("Fill3/Fill").GetComponent<Text>();
            fillResult = calTexts.FindChild("FillResult/Fill").GetComponent<Text>();
            chapterText = root.transform.FindChild("Table/Chapter").GetComponent<Text>();
            noticeBG = root.transform.FindChild("Table/NoticeBG").GetComponent<Image>();
            noticeTextComp = noticeBG.transform.FindChild("NoticeText").GetComponent<Text>();
            //countDown = GameObject.FindGameObjectWithTag("CountDown");
            //countDown= LoadAndInstanceObject( "PanelPrefabs/CountDown");

            noticeTextComp.text = "";
            noticeBG.enabled = false;

            countDown = GameObject.Instantiate(Resources.Load<GameObject>("PanelPrefabs/CountDown"));
            countDown.transform.SetParent(root);
            countDown.transform.localScale = Vector3.one;
            countDown.transform.localPosition = new Vector3(0, 150, 0);
            countDown.name = countDown.name.Replace("(Clone)", "");
            countDown.SetActive(false);

            changeNoticeZhuang = LoadAndInstanceObject("PanelPrefabs/ChangeNoticeZhuang");
            changeNoticeOthers = LoadAndInstanceObject("PanelPrefabs/ChangeNoticeOthers");

            pokers = root.FindChild("Pokers").gameObject;
            pokerModel = root.FindChild("PokerModel").gameObject;
            pokerModel.transform.GetChild(0).GetComponent<Image>().enabled = true;
            bgPoker = GameObject.Instantiate<GameObject>(pokerModel);
            bgPoker.transform.SetParent(pokers.transform);
            bgPoker.transform.localPosition = new Vector3(1500, 0, 0);
            bgPoker.name = "PokerModel";

            playerDict.Add(0, InitPlayerDictOperation("Players/PlayerInfoSelf", "Player1"));
            playerDict.Add(1, InitPlayerDictOperation("Players/PlayerInfoVL", "Player2"));
            playerDict.Add(2, InitPlayerDictOperation("Players/PlayerInfoHL", "Player3"));
            playerDict.Add(3, InitPlayerDictOperation("Players/PlayerInfoHM", "Player4"));
            playerDict.Add(4, InitPlayerDictOperation("Players/PlayerInfoHR", "Player5"));


            poker1 = GetDictHandPoint1(0);
            poker2 = GetDictHandPoint1(1);
            poker3 = GetDictHandPoint1(2);
            poker4 = GetDictHandPoint1(3);
            poker5 = GetDictHandPoint1(4);
            player1 = GetDictPlayer(0);
            player2 = GetDictPlayer(1);
            player3 = GetDictPlayer(2);
            player4 = GetDictPlayer(3);
            player5 = GetDictPlayer(4);

            InitPokeList();
            for (int i = 0; i < 5; i++) {
                GetDictScoreName(i).text = "";
                GetDictPlayerName(i).text = "";
                GetDictOffLine(i).enabled = false;
            }
            HideAllNeedUI();
            HideAllPlayer();
            HideAllButton();

        }

        public void ClearAllDictAndList() {
            Debug.Log("清理所有的手牌和列表");
            try {
                //foreach (var a in pokerFPList) {
                //    GameObject.Destroy(a);
                //}
                //foreach (var a in pokerHTList) {
                //    GameObject.Destroy(a);
                //}
                //foreach (var a in pokerHXList) {
                //    GameObject.Destroy(a);
                //}

                //foreach (var a in pokerMHList) {
                //    GameObject.Destroy(a);
                //}
                //foreach (var a in DNGlobalData.tempBackPokerList) {
                //    GameObject.Destroy(a);
                //}
                DNGlobalData.tempBackPokerList.Clear();
                pokerFPList.Clear();
                pokerHTList.Clear();
                pokerHXList.Clear();
                pokerMHList.Clear();
                pokerDict.Clear();
                uiButtonDict.Clear();
                playerDict.Clear();
                localPostion.Clear();
                compareResultDict.Clear();
                DNGlobalData.paiCountDic.Clear();
                ResultPanel.playerComDict.Clear();
                Debug.Log("Clear");
                DNGlobalData.userIDList.Clear();
                DNGlobalData.userImage.Clear();
                DNGlobalData.spriteDict.Clear();
                DNGlobalData.userNameList.Clear();
                DNGlobalData.userScoreList.Clear();
                DNGlobalData.spritePathList.Clear();
                //DNGlobalData.userScoreList.Clear();
                //DNGlobalData.userNameList.Clear();
                DNGlobalData.userChapterList.Clear();
                DNGlobalData.winCountList.Clear();
                DNGlobalData.loseCountList.Clear();
                DNGlobalData.equalCountList.Clear();
                //DynamicUIManager.Instance.dySpriteDic.Clear();
                //DynamicAudioManager.Instance.audioClip.Clear();
                DNUIManager.Instance.ClearPanelDict();
                DNUIManager.Instance.ClearStack();
                //DNGlobalData.paiCountDic.Clear();
            } catch (Exception ex) { Debug.Log(ex.Message); }

        }
        private PlayerMessageComponent InitPlayerDictOperation(string playerPath, string scenePlayerPath) {
            //Debug.Log("初始化桌面上的玩家UI");
            PlayerMessageComponent playerMessageComponent = new PlayerMessageComponent();
            Transform scenePlayer = sceneObjects.FindChild(scenePlayerPath);
            playerMessageComponent.Player = root.FindChild(playerPath).gameObject;

            playerMessageComponent.PlayerPoint = scenePlayer.FindChild("PlayerPoint");
            playerMessageComponent.PlayerHandPoker1 = scenePlayer.FindChild("HandPokerPoint1").transform;
            playerMessageComponent.PlayerHandPoker2 = scenePlayer.FindChild("HandPokerPoint2").transform;
            playerMessageComponent.PlayerHandPoker3 = scenePlayer.FindChild("HandPokerPoint3").transform;
            playerMessageComponent.NiuType1 = scenePlayer.FindChild("NiuType1").gameObject;
            playerMessageComponent.NiuType2 = scenePlayer.FindChild("NiuType2").gameObject;
            playerMessageComponent.NiuType3 = scenePlayer.FindChild("NiuType3").gameObject;
            playerMessageComponent.Done = scenePlayer.FindChild("Done").GetComponent<Image>();
            playerMessageComponent.Done2 = scenePlayer.FindChild("Done2").GetComponent<Image>();
            playerMessageComponent.Win1 = scenePlayer.FindChild("Win1").GetComponent<Text>();
            playerMessageComponent.Win2 = scenePlayer.FindChild("Win2").GetComponent<Text>();
            playerMessageComponent.Win3 = scenePlayer.FindChild("Win3").GetComponent<Text>();

            playerMessageComponent.Name = scenePlayer.FindChild("nicheng").GetComponent<Text>();
            playerMessageComponent.Score = scenePlayer.FindChild("score").GetComponent<Text>();
            playerMessageComponent.Zhu = scenePlayer.FindChild("zhu").GetComponent<Image>();
            playerMessageComponent.Emoji = scenePlayer.FindChild("emoji").GetComponent<Image>();
            playerMessageComponent.Ready = scenePlayer.FindChild("zb").GetComponent<Image>();

            playerMessageComponent.UserImage = playerMessageComponent.Player.transform.FindChild("headMask/headImage").GetComponent<Image>();
            playerMessageComponent.Zhuang = playerMessageComponent.Player.transform.FindChild("zhuang").gameObject;
            playerMessageComponent.OffLine = playerMessageComponent.Player.transform.FindChild("headMask/offLine").GetComponent<Image>();
            return playerMessageComponent;
        }


        public void ClearChapterInfo() {
            Debug.Log("点击继续游戏清理回合信息");
            ClearHandPoker();
            robPokerCountList.Clear();
            compareResultDict.Clear();
            DNGlobalData.paiCountDic.Clear();
            HideAllNeedUI();
            fill1.text = "";
            fill2.text = "";
            fill3.text = "";
            fillResult.text = "";
            fillIndex = 0;
            outCount = 0;
            //DNGlobalData.currentChapter++;
            chapterText.text = (DNGlobalData.currentChapter + 1) + "/" + DNGlobalData.maxChapter + "局";
            //noticeTextComp.transform.parent.gameObject.SetActive(true);
        }
        public void StartCountDown(string operation, int countTime) {
            Debug.Log("开始倒计时：operation："+operation+" 秒数："+countTime);
            DNGlobalData.countDownOpt = operation;
            //noticeTextComp.text = "请开牌";
            CountDownPanel.ClockNum = countTime;
            CountDownPanel.ShutDown = false;
            countDown.SetActive(true);
        }
        public void StopCountDown() {
            Debug.Log("停止倒计时的运行并重置");
            DNGlobalData.countDownOpt = "";
            //noticeTextComp.text = "请开牌";
            CountDownPanel.ClockNum = 0;
            CountDownPanel.ShutDown = true;
            countDown.SetActive(false);
        }

        public void CountDownOperation(string opt) {
            Debug.Log("倒计时结束或者直接点击调用");
            //CountDownPanel.ShutDown = false;
            switch (opt) {
                case "ChapterEnd":
                    DNUIManager.Instance.PopPanel();
                    DNUIManager.Instance.PushPanel(UIPanelType.ResultPanel);
                    //CountDownPanel.ShutDown = true;
                    break;
                case "NextChapter":
                    NextChapterOperation();
                    break;
                case "YaZhu":
                    DNGlobalData.yazhuNumber = 1;
                    YaZhuOperation();
                    break;
                case "Rob":
                    DNGlobalData.robNumber = 1;
                    RobOperation();
                    break;
                case "NowYaZhu":
                    YaZhuOperation();
                    break;
                case "NowRob":
                    RobOperation();
                    break;
                case "RobHandPoker":
                    DNGlobalData.handPokerNum = 1;
                    RobHandPokerOperation();
                    break;
                case "NowRobHandPoker":
                    RobHandPokerOperation();
                    break;
                case "End":
                    EndOperation();
                    //RobOperation();
                    break;
                case "SureVote":
                    VoteOperation(true);
                    //RobOperation();
                    break;
                case "CancelVote":
                    VoteOperation(false);
                    //RobOperation();
                    break;
                case "ExitRoom":
                    ClearAllDictAndList();
                    UIState_LoadingPage.isComeFromDN = true;

                    GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_LoadingPage);
                    //RobOperation();
                    break;
                case "":
                    break;

            }
        }
        bool firstYaZhu = true;
        int zhuNum;
        public void VoteOperation(bool bo) {
            Debug.Log("点击投票退出房间后的操作，倒计时和点击通用");
            DouniuVote vote = new DouniuVote();
            vote.setResult(bo);
            vote.setUserId(DNGlobalData.currentUserID);
            SocketMgr.GetInstance().Send(Net.instance.write(vote));
        }
        public void NextChapterOperation() {
            Debug.Log("点击继续游戏后的操作，倒计时和点击通用");
            DouniuStartGame msg = new DouniuStartGame();
            msg.IsReady = true;
            SocketMgr.GetInstance().Send(Net.instance.write(msg));
            HideButton(UIButton.继续);
            GetDictNiuType1(0).transform.DOLocalMoveY(-270, 0.2f);
            GetDictNiuType2(0).transform.DOLocalMoveY(-270, 0.2f);
            GetDictNiuType3(0).transform.DOLocalMoveY(-270, 0.2f);
            GetDictWinText1(0).transform.DOLocalMoveY(-270, 0.2f);
            GetDictWinText2(0).transform.DOLocalMoveY(-270, 0.2f);
            GetDictWinText3(0).transform.DOLocalMoveY(-270, 0.2f);
            Transform tran1 = GetDictHandPoint1(0);
            tran1.localPosition = new Vector3(tran1.localPosition.x, tran1.localPosition.y - 40, 0);
            tran1 = GetDictHandPoint2(0);
            tran1.localPosition = new Vector3(tran1.localPosition.x, tran1.localPosition.y - 40, 0);
            tran1 = GetDictHandPoint3(0);
            tran1.localPosition = new Vector3(tran1.localPosition.x, tran1.localPosition.y - 40, 0);
            ClearChapterInfo();
        }
        public void EndOperation() {
            Debug.Log("回合结束操作，倒计时和点击通用");
            DouniuOperation resp = new DouniuOperation();
            resp.Opt = "OPT_LIANGPAI";
            resp.ZhuNum = 1;
            SocketMgr.GetInstance().Send(Net.instance.write(resp));
            HideAllJiaZhuButton();
            HideAllRobPokerButton();
            foreach (var a in handPoker) {
                if (a.GetComponent<Poker>() != null) {
                    GameObject.Destroy(a.GetComponent<Poker>());
                }
            }
            foreach (var a in handPoker2) {
                if (a.GetComponent<Poker>() != null) {
                    GameObject.Destroy(a.GetComponent<Poker>());
                }
            }
            foreach (var a in handPoker) {
                a.DOLocalMoveY(7, 0.2f);
            }
            foreach (var a in handPoker2) {
                a.DOLocalMoveY(7, 0.2f);
            }
            Transform tran1 = GetDictHandPoint1(0);
            tran1.localPosition = new Vector3(tran1.localPosition.x, -243 + 40, 0);
            tran1 = GetDictHandPoint2(0);
            tran1.localPosition = new Vector3(tran1.localPosition.x, -243 + 40, 0);
            tran1 = GetDictHandPoint3(0);
            tran1.localPosition = new Vector3(tran1.localPosition.x, -243 + 40, 0);
            GetDictNiuType1(0).transform.DOLocalMoveY(-230, 0.2f);
            GetDictNiuType2(0).transform.DOLocalMoveY(-230, 0.2f);
            GetDictNiuType3(0).transform.DOLocalMoveY(-230, 0.2f);
            GetDictWinText1(0).transform.DOLocalMoveY(-230, 0.2f);
            GetDictWinText2(0).transform.DOLocalMoveY(-230, 0.2f);
            GetDictWinText3(0).transform.DOLocalMoveY(-230, 0.2f);
            //HideButton(UIButton.有牛);
            HideButton(UIButton.灰亮牌);
            HideButton(UIButton.提示);
            HideButton(UIButton.亮牌);
            HideButton(UIButton.灰提示);
        }
        public void YaZhuOperation() {
            Debug.Log("加注操作，倒计时和点击通用");
            HideAllJiaZhuButton();
            HideAllRobPokerButton();
            HideButton(UIButton.灰亮牌);
            HideButton(UIButton.提示);
            HideButton(UIButton.亮牌);
            HideButton(UIButton.灰提示);
            if (!DNGlobalData.lookPoker) {
                // manager.HideAllRobPokerButton();
                //ShowAllJiaZhuButton();
                noticeBG.enabled = true;
                noticeTextComp.text = "正在等待其他玩家下注...";
            }
            DouniuOperation yaZhu = new DouniuOperation();
            yaZhu.ZhuNum = DNGlobalData.yazhuNumber;
            yaZhu.ZhuNum2 = 0;
            yaZhu.Opt = "OPT_XIAZHU";
            SocketMgr.GetInstance().Send(Net.instance.write(yaZhu));
        }

        public void RobOperation() {
            //noticeTextComp.text = "";
            DouniuOperation rob = new DouniuOperation();
            rob.ZhuNum = DNGlobalData.robNumber;
            rob.Opt = "OPT_QIANGZHUANG";
            SocketMgr.GetInstance().Send(Net.instance.write(rob));
            HideAllRobButton();
        }
        public void RobHandPokerOperation() {
            //noticeTextComp.text = "";
            DouniuCasiPai rob = new DouniuCasiPai();
            rob.setPaiLens(DNGlobalData.handPokerNum);
            SocketMgr.GetInstance().Send(Net.instance.write(rob));
            HideAllRobPokerButton();
            if (!DNGlobalData.lookPoker) {
                // manager.HideAllRobPokerButton();
                ShowAllJiaZhuButton();
                noticeBG.enabled = true;
                noticeTextComp.text = "请下注";
            }
            //noticeBG.enabled = true;
            //noticeTextComp.text = "正在等待其他人选择手牌";
        }

        GameObject LoadAndInstanceObject(string path, bool active = false) {
            GameObject valueObject = GameObject.Instantiate(Resources.Load<GameObject>(path));
            valueObject.transform.SetParent(root);
            valueObject.transform.localScale = Vector3.one;
            valueObject.transform.localPosition = new Vector3(0, 67, 0);
            valueObject.name = valueObject.name.Replace("(Clone)", "");
            valueObject.SetActive(false);
            return valueObject;
        }
        private void UserImageClick(int i) {
            Debug.Log("daj:" + i);
            Image playerImg = GetDictUserImage(i);
            DNGlobalData.clickUserImage = playerImg.sprite;
            DNGlobalData.clickUserName = GetDictPlayerName(i).text;
            //DNGlobalData.clickUserSocre = GetDictWinText(i).text;
        }
        public int GetDictPaiCount(int index) {
            Debug.Log("获取手牌字典中相应索引的手牌数量  index："+index+"value:"+DNGlobalData.paiCountDic[index]);
            int count;
            DNGlobalData.paiCountDic.TryGetValue(index, out count);
            return count;
        }
        public void BaseTypeDOTween(string type) {
            Debug.Log("根据类型播放发牌动画");
            switch (type) {
                case "11":
                    DOTweenDealPoker.out11 = true;
                    break;
                case "12":
                    DOTweenDealPoker.out12 = true;
                    break;
                case "21":
                    DOTweenDealPoker.out21 = true;
                    break;
                case "22":
                    DOTweenDealPoker.out22 = true;
                    break;
                case "31":
                    DOTweenDealPoker.out31 = true;
                    break;
                case "32":
                    DOTweenDealPoker.out32 = true;
                    break;
                case "41":
                    DOTweenDealPoker.out41 = true;
                    break;
                case "42":
                    DOTweenDealPoker.out42 = true;
                    break;
                case "51":
                    DOTweenDealPoker.out51 = true;
                    break;
                case "52":
                    DOTweenDealPoker.out52 = true;
                    break;
                case "13":
                    DOTweenDealPoker.out13 = true;
                    break;
                case "23":
                    DOTweenDealPoker.out23 = true;
                    break;
                case "33":
                    DOTweenDealPoker.out33 = true;
                    break;
                case "43":
                    DOTweenDealPoker.out43 = true;
                    break;
                case "53":
                    DOTweenDealPoker.out53 = true;
                    break;
            }
        }
        private void InitPokeList() {
            Debug.Log("初始化所有的扑克牌");
            string str;
            pokerFPList = new List<GameObject>();
            for (int i = 0; i < 13; i++) {
                str = "fp_" + (i + 1);
                GameObject item = GameObject.Instantiate<GameObject>(pokerModel);
                item.GetComponent<Image>().sprite = DynamicUIManager.Instance.BaseNameGetSprite(str);
                item.transform.SetParent(pokers.transform);
                item.transform.localPosition = new Vector3(1500, 0, 0);
                item.name = (i + 1).ToString();
                pokerFPList.Add(item);
            }
            pokerHTList = new List<GameObject>();
            for (int j = 0; j < 13; j++) {
                str = "ht_" + (j + 1);
                GameObject obj3 = GameObject.Instantiate<GameObject>(pokerModel);
                obj3.GetComponent<Image>().sprite = DynamicUIManager.Instance.BaseNameGetSprite(str);
                obj3.transform.SetParent(pokers.transform);
                obj3.transform.localPosition = new Vector3(1500, 0, 0);
                obj3.name = (j + 1).ToString();
                pokerHTList.Add(obj3);
            }
            pokerHXList = new List<GameObject>();
            for (int k = 0; k < 13; k++) {
                str = "hx_" + (k + 1);
                GameObject obj4 = GameObject.Instantiate<GameObject>(pokerModel);
                obj4.GetComponent<Image>().sprite = DynamicUIManager.Instance.BaseNameGetSprite(str);
                obj4.transform.SetParent(pokers.transform);
                obj4.transform.localPosition = new Vector3(1500, 0, 0);
                obj4.name = (k + 1).ToString();
                pokerHXList.Add(obj4);
            }
            pokerMHList = new List<GameObject>();
            for (int m = 0; m < 13; m++) {
                str = "mh_" + (m + 1);
                GameObject obj5 = GameObject.Instantiate<GameObject>(pokerModel);
                obj5.GetComponent<Image>().sprite = DynamicUIManager.Instance.BaseNameGetSprite(str);
                obj5.transform.SetParent(pokers.transform);
                obj5.transform.localPosition = new Vector3(1500, 0, 0);
                obj5.name = (m + 1).ToString();
                //obj5.AddComponent<Poker>();
                //Poker poker = obj5.GetComponent<Poker>();
                //poker.PokerNumber = m + 1;
                //if (m >= 10) {
                //    poker.Value = 10;
                //} else {
                //    poker.Value = m + 1;
                //}
                //Debug.Log(poker.PokerNumber);
                //Debug.Log(poker.Value);
                pokerMHList.Add(obj5);
            }
        }
        public void ClearAllPoker() {
            Debug.Log("战绩回放时清理手牌");
            for (int i = 0; i < DNGlobalData.roomPlayerNumber; i++) {
                GetDictHandPoint1(i).DetachChildren();
                GetDictHandPoint2(i).DetachChildren();
                GetDictHandPoint3(i).DetachChildren();
            }
        }
        public void ClearHandPoker() {
            Debug.Log("游戏界面清理手牌");
            Transform poker;
            int roomPlayerNumber = DNGlobalData.roomPlayerNumber;
            for (int i = 0; i < roomPlayerNumber; i++) {
                int reverseIndex = i;
                for (int j = 0; j < GetDictHandPoint1(reverseIndex).childCount; j++) {
                    poker = GetDictHandPoint1(reverseIndex).GetChild(j);
                    poker.SetParent(pokers.transform);
                    poker.localPosition = new Vector3(1500, 0, 0);
                    if (poker.name == "PokerModel") {
                        GameObject.Destroy(poker.gameObject);
                    }
                }
                for (int j = 0; j < GetDictHandPoint2(reverseIndex).childCount; j++) {
                    poker = GetDictHandPoint2(reverseIndex).GetChild(j);
                    poker.SetParent(pokers.transform);
                    poker.localPosition = new Vector3(1500, 0, 0);
                    if (poker.name == "PokerModel") {
                        GameObject.Destroy(poker.gameObject);
                    }
                }
                for (int j = 0; j < GetDictHandPoint3(reverseIndex).childCount; j++) {
                    poker = GetDictHandPoint3(reverseIndex).GetChild(j);
                    poker.SetParent(pokers.transform);
                    poker.localPosition = new Vector3(1500, 0, 0);
                    if (poker.name == "PokerModel") {
                        GameObject.Destroy(poker.gameObject);
                    }
                }
                GetDictHandPoint1(i).DetachChildren();
                GetDictHandPoint2(i).DetachChildren();
                GetDictHandPoint3(i).DetachChildren();
            }
        }
        /// <summary>
        /// parent 有三个值1，2，3分别代表GetDictHandPoint1，GetDictHandPoint2，GetDictHandPoint3
        /// </summary>
        /// <param name="index"></param>
        /// <param name="parent"></param>
        public void ClearHandPoker(int index, int parent) {
            //GetDictHandPoint1(index);
            Debug.Log("根据手牌索引和父物体索引清理手牌 ");
            Transform poker;
            switch (parent) {
                case 1:
                    for (int j = 0; j < GetDictHandPoint1(index).childCount; j++) {
                        poker = GetDictHandPoint1(index).GetChild(j);
                        poker.localPosition = new Vector3(1000, 0, 0);
                        poker.SetParent(pokers.transform);
                        if (poker.name == "PokerModel") {
                            GameObject.Destroy(poker.gameObject);
                        }
                    }
                    GetDictHandPoint1(index).DetachChildren();
                    break;
                case 2:
                    for (int j = 0; j < GetDictHandPoint2(index).childCount; j++) {
                        poker = GetDictHandPoint2(index).GetChild(j);
                        poker.localPosition = new Vector3(1000, 0, 0);
                        poker.SetParent(pokers.transform);
                        if (poker.name == "PokerModel") {
                            GameObject.Destroy(poker.gameObject);
                        }
                    }
                    GetDictHandPoint2(index).DetachChildren();
                    break;
                case 3:
                    for (int j = 0; j < GetDictHandPoint3(index).childCount; j++) {
                        poker = GetDictHandPoint3(index).GetChild(j);
                        poker.localPosition = new Vector3(1000, 0, 0);
                        poker.SetParent(pokers.transform);
                        if (poker.name == "PokerModel") {
                            GameObject.Destroy(poker.gameObject);
                        }
                    }
                    GetDictHandPoint3(index).DetachChildren();
                    break;
            }
        }

        public void DealDiffentMode() {
            Debug.Log("不同规则的发牌后的后续操作");
            if (DNGlobalData.lookPoker) {
                DOTweenRotateShow(handPoker);
                if (handPoker2 != null || handPoker2.Count == 0) {
                    DOTweenRotateShow(handPoker2);
                }
                if (!GetDictZhuang(DNGlobalData.locationIndex).activeSelf && !DNGlobalData.isStart) {
                    noticeBG.enabled = false;
                    noticeTextComp.text = "";
                    ShowAllJiaZhuButton();
                    //StartCountDown("YaZhu", 15);
                } else {
                    noticeBG.enabled = true;
                    noticeTextComp.text = "等待闲家下注...";
                }
            } else {
                DOTweenRotateShow(handPoker);
                if (handPoker2 != null || handPoker2.Count == 0) {
                    DOTweenRotateShow(handPoker2);
                }
                foreach (var a in handPoker) {
                    if (a.GetComponent<Poker>() == null) {
                        a.gameObject.AddComponent<Poker>();
                    }
                    //Debug.Log("第一手牌：" + a);
                }
                foreach (var a in handPoker2) {
                    if (a.GetComponent<Poker>() == null) {
                        a.gameObject.AddComponent<Poker>();
                    }
                    //Debug.Log("第二手牌：" + a);
                }
                DNGlobalData.waitEnd = true;
                calTexts.gameObject.SetActive(true);
                HideButton(UIButton.灰提示);
                HideButton(UIButton.亮牌);
                ShowButton(UIButton.灰亮牌);
                ShowButton(UIButton.提示);
                //StartCountDown("End", 15);
            }
        }
        public void NetClose() {
            ClearAllDictAndList();

        }
        public void RetGenZhuOperation(DouniuOperationRet operation) {
            Debug.Log("跟注");
        }

        public void RetLiangPaiOperation(DouniuOperationRet operation) {
            Debug.Log("亮牌/比牌/有牛操作");
            if (DNGlobalData.locationIndex != operation.Index) {
                //noticeBG.enabled = true;
                //noticeTextComp.text = "正在等待其他人亮牌";
            }
        }

        public void RetQiangZhuangOperation(DouniuOperationRet operation) {
            Debug.Log("抢庄操作");
            //positionArr = SetPlayerPosition(DNGlobalData.roomPlayerNumber, DNGlobalData.locationIndex);
            Image dictZhu = GetDictZhu(positionArr[operation.Index]);
            dictZhu.enabled = true;
            //for (int i = 0; i < positionArr.Length; i++) {
            //    if (positionArr[i] == 0) {
            //        dictZhu = GetDictZhu(operation.Index);
            //        dictZhu.enabled = true;
            //    }
            //}
            switch (operation.ZhuNum) {
                case 1:
                    dictZhu.sprite = DynamicUIManager.Instance.BaseNameGetSprite("white_rob1");
                    PlayTransformAudio("buqiang");
                    break;
                case 2:
                    dictZhu.sprite = DynamicUIManager.Instance.BaseNameGetSprite("white_rob2");
                    PlayTransformAudio("rob1");
                    break;
                case 3:
                    dictZhu.sprite = DynamicUIManager.Instance.BaseNameGetSprite("white_rob3");
                    PlayTransformAudio("rob1");
                    break;
                case 4:
                    dictZhu.sprite = DynamicUIManager.Instance.BaseNameGetSprite("white_rob4");
                    PlayTransformAudio("rob1");
                    break;
            }
        }

        public void PlayTransformAudio(string audioName) {
            Debug.Log("播放音效：" + audioName );
            transSource.clip = DynamicAudioManager.Instance.BaseNameGetAudio(audioName);
            transSource.volume = PlayerPrefs.GetFloat("AudioSet",1); 
            //transSource.volume = 1.0f;
            transSource.Play();
        }

        public void PlayTransformAudioBaseAudio(AudioClip audio) {
            Debug.Log("播放音效："+audio+"  "+audio.name);
            transSource.clip = audio;
            transSource.volume = PlayerPrefs.GetFloat("AudioSet",1);
            //transSource.volume = 1.0f;
            transSource.Play();
        }
        public void RetQiPaiOperation(DouniuOperationRet operation) {
            Debug.Log("弃牌");
        }

        public void RetXiaZhuOperation(DouniuOperationRet operation) {
            Debug.Log("加注操作:" + operation.Index);

            if (!DNGlobalData.lookPoker && operation.Index != DNGlobalData.locationIndex) {
                if (DNGlobalData.currentUserID == DNGlobalData.fangZhuUserID) {
                    noticeBG.gameObject.SetActive(true);
                    noticeTextComp.text = "等待闲家下注...";
                } else {
                    noticeBG.gameObject.SetActive(true);
                    noticeTextComp.text = "正在等待其他玩家下注...";

                }
            }
            if (!DNGlobalData.isStart) {
                positionArr = SetPlayerPosition(DNGlobalData.roomPlayerNumber, DNGlobalData.locationIndex);
                Image dictZhu = GetDictZhu(positionArr[operation.Index]);
                dictZhu.enabled = true;
                switch (operation.ZhuNum) {
                    case 1:
                        dictZhu.sprite = DynamicUIManager.Instance.BaseNameGetSprite("yellow_1");
                        break;

                    case 2:
                        dictZhu.sprite = DynamicUIManager.Instance.BaseNameGetSprite("yellow_2");
                        break;

                    case 3:
                        dictZhu.sprite = DynamicUIManager.Instance.BaseNameGetSprite("yellow_3");
                        break;

                    case 4:
                        dictZhu.sprite = DynamicUIManager.Instance.BaseNameGetSprite("yellow_4");
                        break;
                }
            }
        }

        public void UserIsOperation() {
            HideAllJiaZhuButton();
            HideAllRobPokerButton();
            HideButton(UIButton.亮牌);
            HideButton(UIButton.灰亮牌);
            HideButton(UIButton.提示);
            HideButton(UIButton.灰提示);
        }
        int num;
        string numStr;
        List<Sprite> spriteList = new List<Sprite>();
        public List<Sprite> WinTextToSprite(string text) {
            spriteList.Clear();
            int.TryParse(text, out num);
            Sprite operatorSprite;
            if (num < 0) {
                operatorSprite = DynamicUIManager.Instance.BaseNameGetSprite("-");
            } else {
                operatorSprite = DynamicUIManager.Instance.BaseNameGetSprite("+");
            }
            num = Mathf.Abs(num);
            numStr = num.ToString();
            //GetNumberSprite(numStr[])
            Sprite unitSprite;


            spriteList.Add(operatorSprite);
            return spriteList;
            //manager.GetNumberSprite()
        }

        public string GetNumberSprite(int num) {
            // Sprite sprite;
            string str;
            if (num >= 0) {
                str = num.ToString();
                return str;
            } else {
                string tempStr = num.ToString();
                tempStr = tempStr.Replace("0", "A");
                tempStr = tempStr.Replace("1", "B");
                tempStr = tempStr.Replace("2", "C");
                tempStr = tempStr.Replace("3", "D");
                tempStr = tempStr.Replace("4", "E");
                tempStr = tempStr.Replace("5", "F");
                tempStr = tempStr.Replace("6", "G");
                tempStr = tempStr.Replace("7", "H");
                tempStr = tempStr.Replace("8", "I");
                tempStr = tempStr.Replace("9", "J");
                str = tempStr;
                return str;
            }
        }

        public void DOTweenRotateHide(List<Transform> pokerList) {

            foreach (var poker in pokerList) {
                poker.DOLocalMoveY(7f, 0);
                Tweener tw = poker.DOLocalRotate(new Vector3(0, 90, 0), 0.5f);
                tw.OnComplete(() => {
                    poker.GetChild(0).GetComponent<Image>().enabled = true;
                    Tweener te = poker.DOLocalRotate(new Vector3(0, 180, 0), 0.5f);
                    te.OnComplete(() => {
                        poker.localRotation = Quaternion.Euler(Vector3.zero);
                    });
                });
            }
        }
        public void DOTweenRotateShow(List<Transform> pokerList) {
            foreach (var poker in pokerList) {
                float posX = poker.localScale.x;
                Tweener tw = poker.DOScaleX(0, 0.5f);
                tw.OnComplete(() => {
                    poker.GetChild(0).GetComponent<Image>().enabled = false;
                    Tweener te = poker.DOScaleX(posX, 0.5f);
                    te.OnComplete(() => {
                        poker.localRotation = Quaternion.Euler(Vector3.zero);
                    });
                });
            }
        }
        public void DonePoker() {
            //foreach (var a in handPoker) {
            //    a.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            //AddPokerDOTween(a);
            //}
            //foreach (var a in handPoker2) {
            //    a.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            //    //AddPokerDOTween(a);
            //}
        }

        public void AddPokerDOTween(Transform tran) {
            Tweener tw = tran.DOLocalRotate(new Vector3(0, 90, 0), 0.5f);
            tw.OnComplete(() => {
                tran.GetChild(0).GetComponent<Image>().enabled = true;
                Tweener te = tran.DOLocalRotate(new Vector3(0, 180, 0), 0.5f);
                te.OnComplete(() => {
                    tran.localRotation = Quaternion.Euler(Vector3.zero);
                });
            });
        }

        public int[] SetPlayerPosition(int peopleNum, int currentPlayreIndex) {
            Debug.Log("重新为位置索引的数组赋值");
            int[] numArray;
            //if (peopleNum == 6) {
            //    numArray = new int[6];
            //    int[] numArray2 = new int[] { 0, 1, 2, 3, 4, 5 };
            //    int[] numArray3 = new int[] { 5, 0, 1, 2, 3, 4 };
            //    int[] numArray4 = new int[] { 4, 5, 0, 1, 2, 3 };
            //    int[] numArray5 = new int[] { 3, 4, 5, 0, 1, 2 };
            //    int[] numArray6 = new int[] { 2, 3, 4, 5, 0, 1 };
            //    int[] numArray7 = new int[] { 1, 2, 3, 4, 5, 0 };
            //    switch (currentPlayreIndex) {
            //        case 0:
            //            return numArray2;

            //        case 1:
            //            return numArray3;

            //        case 2:
            //            return numArray4;

            //        case 3:
            //            return numArray5;

            //        case 4:
            //            return numArray6;

            //        case 5:
            //            return numArray7;
            //    }
            //    return numArray;
            //}
            if (peopleNum == 5) {
                numArray = new int[5];
                int[] numArray8 = new int[] { 0, 1, 2, 3, 4 };
                int[] numArray9 = new int[] { 1, 0, 2, 3, 4 };
                int[] numArray10 = new int[] { 1, 2, 0, 3, 4 };
                int[] numArray11 = new int[] { 1, 2, 3, 0, 4 };
                int[] numArray12 = new int[] { 1, 2, 3, 4, 0 };
                switch (currentPlayreIndex) {
                    case 0:
                        return numArray8;

                    case 1:
                        return numArray9;

                    case 2:
                        return numArray10;

                    case 3:
                        return numArray11;

                    case 4:
                        return numArray12;
                }
                return numArray;
            }
            if (peopleNum == 4) {
                numArray = new int[4];
                int[] numArray13 = new int[] { 0, 1, 2, 3 };
                int[] numArray14 = new int[] { 1, 0, 2, 3 };
                int[] numArray15 = new int[] { 1, 2, 0, 3 };
                int[] numArray16 = new int[] { 1, 2, 3, 0 };
                switch (currentPlayreIndex) {
                    case 0:
                        return numArray13;

                    case 1:
                        return numArray14;

                    case 2:
                        return numArray15;

                    case 3:
                        return numArray16;
                }
                return numArray;
            }
            if (peopleNum == 3) {
                numArray = new int[3];
                int[] numArray13 = new int[] { 0, 1, 2 };
                //int[] numArray14 = new int[] {2, 0, 1};
                int[] numArray14 = new int[] { 1, 0, 2 };
                int[] numArray15 = new int[] { 1, 2, 0 };
                switch (currentPlayreIndex) {
                    case 0:
                        return numArray13;

                    case 1:
                        return numArray14;

                    case 2:
                        return numArray15;
                }
                return numArray;
            }
            if (peopleNum == 2) {
                numArray = new int[2];
                int[] numArray24 = new int[2];
                numArray24[1] = 1;
                int[] numArray20 = numArray24;
                int[] numArray25 = new int[2];
                numArray25[0] = 1;
                int[] numArray21 = numArray25;
                if (currentPlayreIndex != 0) {
                    if (currentPlayreIndex == 1) {
                        return numArray21;
                    }
                    return numArray;
                }
                return numArray20;
            }
            return new int[1];
        }

        public Transform GetDictHandPoint1(int playerIndex) {
            PlayerMessageComponent component;
            if (playerDict.TryGetValue(playerIndex, out component)) {
                return component.PlayerHandPoker1;
            }
            return null;
        }

        public Transform GetDictHandPoint2(int playerIndex) {
            PlayerMessageComponent component;
            if (playerDict.TryGetValue(playerIndex, out component)) {
                return component.PlayerHandPoker2;
            }
            return null;
        }
        public Transform GetDictHandPoint3(int playerIndex) {
            PlayerMessageComponent component;
            if (playerDict.TryGetValue(playerIndex, out component)) {
                return component.PlayerHandPoker3;
            }
            return null;
        }

        public GameObject GetDictNiuType1(int playerIndex) {
            PlayerMessageComponent component;
            if (playerDict.TryGetValue(playerIndex, out component)) {
                return component.NiuType1;
            }
            return null;
        }

        public GameObject GetDictNiuType2(int playerIndex) {
            PlayerMessageComponent component;
            if (playerDict.TryGetValue(playerIndex, out component)) {
                return component.NiuType2;
            }
            return null;
        }
        public GameObject GetDictNiuType3(int playerIndex) {
            PlayerMessageComponent component;
            if (playerDict.TryGetValue(playerIndex, out component)) {
                return component.NiuType3;
            }
            return null;
        }

        public GameObject GetDictPlayer(int playerIndex) {
            PlayerMessageComponent component;
            if (playerDict.TryGetValue(playerIndex, out component)) {
                return component.Player;
            }
            return null;
        }

        public Text GetDictPlayerName(int playerIndex) {
            PlayerMessageComponent component;
            if (playerDict.TryGetValue(playerIndex, out component)) {
                return component.Name;
            }
            return null;
        }

        public Text GetDictScoreName(int playerIndex) {
            PlayerMessageComponent component;
            if (playerDict.TryGetValue(playerIndex, out component)) {
                return component.Score;
            }
            return null;
        }

        public Button GetDictUIButton(UIButton button) {
            Button button2;
            if (uiButtonDict.TryGetValue(button, out button2)) {
                return button2;
            }
            return null;
        }

        public Image GetDictUserImage(int playerIndex) {
            PlayerMessageComponent component;
            if (playerDict.TryGetValue(playerIndex, out component)) {
                return component.UserImage;
            }
            return null;
        }
        public Image GetDictEmoji(int playerIndex) {
            PlayerMessageComponent component;
            if (playerDict.TryGetValue(playerIndex, out component)) {
                return component.Emoji;
            }
            return null;
        }
        public Image GetDictOffLine(int playerIndex) {
            PlayerMessageComponent component;
            if (playerDict.TryGetValue(playerIndex, out component)) {
                return component.OffLine;
            }
            return null;
        }

        public Text GetDictWinText1(int playerIndex) {
            PlayerMessageComponent component;
            if (playerDict.TryGetValue(playerIndex, out component)) {
                return component.Win1;
            }
            return null;
        }
        public Text GetDictWinText2(int playerIndex) {
            PlayerMessageComponent component;
            if (playerDict.TryGetValue(playerIndex, out component)) {
                return component.Win2;
            }
            return null;
        }
        public Text GetDictWinText3(int playerIndex) {
            PlayerMessageComponent component;
            if (playerDict.TryGetValue(playerIndex, out component)) {
                return component.Win3;
            }
            return null;
        }

        public Image GetDictZhu(int playerIndex) {
            PlayerMessageComponent component;
            if (playerDict.TryGetValue(playerIndex, out component)) {
                return component.Zhu;
            }
            return null;
        }
        public Image GetDictReady(int playerIndex) {
            PlayerMessageComponent component;
            if (playerDict.TryGetValue(playerIndex, out component)) {
                return component.Ready;
            }
            return null;
        }
        public Image GetDictDone(int playerIndex) {
            PlayerMessageComponent component;
            if (playerDict.TryGetValue(playerIndex, out component)) {
                return component.Done;
            }
            return null;
        }
        public Image GetDictDone2(int playerIndex) {
            PlayerMessageComponent component;
            if (playerDict.TryGetValue(playerIndex, out component)) {
                return component.Done2;
            }
            return null;
        }

        public GameObject GetDictZhuang(int playerIndex) {
            PlayerMessageComponent component;
            if (playerDict.TryGetValue(playerIndex, out component)) {
                return component.Zhuang;
            }
            return null;
        }

        public Transform GetPoker(DouniuOnePai pai) {
            int num = pai.getPokerNum();
            int num2 = pai.getPokerSuit();
            int num3 = pai.getPokerValue();
            GameObject bgPoker = null;
            switch ((num2 + 1)) {
                case 0:
                    bgPoker = this.bgPoker;
                    break;

                case 2:
                    bgPoker = pokerFPList[num - 1];
                    break;

                case 3:
                    bgPoker = pokerMHList[num - 1];
                    break;

                case 4:
                    bgPoker = pokerHTList[num - 1];
                    break;

                case 5:
                    bgPoker = pokerHXList[num - 1];
                    break;
            }
            return bgPoker.transform;
        }

        public void ShowAllJiaZhuButton() {
            Debug.Log("显示所有的加注按钮");
            GetDictUIButton(UIButton.加3注).gameObject.SetActive(true);
            GetDictUIButton(UIButton.加5注).gameObject.SetActive(true);
            GetDictUIButton(UIButton.加8注).gameObject.SetActive(true);
            GetDictUIButton(UIButton.加10注).gameObject.SetActive(true);
            //noticeTextComp.text = "等待玩家下注";
        }

        public void ShowAllRobButton() {
            GetDictUIButton(UIButton.抢1).gameObject.SetActive(true);
            GetDictUIButton(UIButton.抢2).gameObject.SetActive(true);
            GetDictUIButton(UIButton.抢3).gameObject.SetActive(true);
            GetDictUIButton(UIButton.抢4).gameObject.SetActive(true);
            //noticeTextComp.text = "等待玩家抢庄";
        }

        public void ShowButton(UIButton button) {
           // Debug.Log("显示按钮："+button.ToString());
            GetDictUIButton(button).gameObject.SetActive(true);
        }

        public void ShowPlayer(int playerIndex) {
            Debug.Log("根据索引显示玩家");
            switch (playerIndex) {
                case 0:
                    player1.SetActive(true);
                    break;

                case 1:
                    player2.SetActive(true);
                    break;

                case 2:
                    player3.SetActive(true);
                    break;

                case 3:
                    player4.SetActive(true);
                    break;

                case 4:
                    player5.SetActive(true);
                    break;

                    //case 5:
                    //    player6.SetActive(true);
                    //    break;
            }
        }

        public void HideAllButton() {
            Debug.Log("隐藏所有的按钮");
            IEnumerator enumerator = Enum.GetValues(typeof(UIButton)).GetEnumerator();
            try {
                while (enumerator.MoveNext()) {
                    UIButton current = (UIButton)enumerator.Current;
                    HideButton(current);
                }
            } finally {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null) {
                    disposable.Dispose();
                }
            }
        }

        public void HideAllJiaZhuButton() {
            Debug.Log("隐藏所有加注按钮");
            GetDictUIButton(UIButton.加3注).gameObject.SetActive(false);
            GetDictUIButton(UIButton.加5注).gameObject.SetActive(false);
            GetDictUIButton(UIButton.加8注).gameObject.SetActive(false);
            GetDictUIButton(UIButton.加10注).gameObject.SetActive(false);
            //noticeTextComp.text = "";
        }
        public void HideAllRobPokerButton() {
            Debug.Log("隐藏所有抢手牌按钮");
            GetDictUIButton(UIButton.一手).gameObject.SetActive(false);
            GetDictUIButton(UIButton.两手).gameObject.SetActive(false);
            //GetDictUIButton(UIButton.三手).gameObject.SetActive(false);
        }
        public void ShowAllRobPokerButton() {
            Debug.Log("显示所有抢手牌按钮");
            GetDictUIButton(UIButton.一手).gameObject.SetActive(true);
            GetDictUIButton(UIButton.两手).gameObject.SetActive(true);
            //GetDictUIButton(UIButton.三手).gameObject.SetActive(true);
        }
        public void HideAllNeedUI() {
            Debug.Log("隐藏所有需要的UI");
            for (int i = 0; i < 5; i++) {
                GetDictZhuang(i).SetActive(false);
                GetDictZhu(i).enabled = false;
                GetDictWinText1(i).text = "";
                GetDictWinText2(i).text = "";
                GetDictWinText3(i).text = "";
                //GetDictScoreName(i).text = "0";
                GetDictNiuType1(i).SetActive(false);
                GetDictNiuType2(i).SetActive(false);
                GetDictNiuType3(i).SetActive(false);
                GetDictEmoji(i).enabled = false;
                GetDictDone(i).enabled = false;
                GetDictDone2(i).enabled = false;
                GetDictReady(i).enabled = false;
            }
        }

        public void HideAllPlayer() {
            Debug.Log("隐藏所有的玩家");
            for (int i = 0; i < 5; i++) {
                HidePlayer(i);
            }
        }

        public void HideAllRobButton() {
            Debug.Log("隐藏所有的抢注");
            GetDictUIButton(UIButton.抢1).gameObject.SetActive(false);
            GetDictUIButton(UIButton.抢2).gameObject.SetActive(false);
            GetDictUIButton(UIButton.抢3).gameObject.SetActive(false);
            GetDictUIButton(UIButton.抢4).gameObject.SetActive(false);
            //noticeTextComp.text = "";
        }

        public void HideButton(UIButton button) {

            //Debug.Log("隐藏："+button.ToString()+"按钮");
            Button btn = GetDictUIButton(button);
            if (btn != null) {
                GetDictUIButton(button).gameObject.SetActive(false);
            }
        }
        void OutPokerDOTween() {
            Debug.Log("有牛是播放出牌动画（三张牌出去）");
            fill[0] = "";
            fill[1] = "";
            fill[2] = "";
            fill[3] = "";
            int num = 0;
            //Debug.Log("点击牌的数量："+fillList.Count);
            
            if (fillList.Count == 0) {
                for (int i = 0; i < outList.Count; i++) {
                    outList[i].DOLocalMoveY(40, 0.2f);

                    // int value;
                    int.TryParse(outList[i].name, out value);
                    if (value >= 11) {
                        value = 10;
                    }
                    num += value;
                    fill[i] = value.ToString();
                }
            } else {
                for (int i = 0; i < fillList.Count; i++) {
                    fillList[i].DOLocalMoveY(40, 0.2f);

                    // Debug.Log("点击的牌：" + fillList[i]);
                    // int value;
                    int.TryParse(fillList[i].name, out value);
                    if (value >= 11) {
                        value = 10;
                    }
                    num += value;
                    fill[i] = value.ToString();
                }
            }
            //for (int i = 0; i < outList.Count; i++) {
            //    //outList[i].DOLocalMoveY(40, 0.2f);

            //    Debug.Log("点击的牌2：" + outList[i]);
            //}
            //for (int i = 0; i < outList.Count; i++) {
            //    outList[i].DOLocalMoveY(40, 0.2f);

            //   // int value;
            //    int.TryParse(outList[i].name, out value);
            //    if (value >= 11) {
            //        value = 10;
            //    }
            //    num += value;
            //    fill[i] = value.ToString();
            //}
            fill[3] = num.ToString();
            fill1.text = fill[0];
            fill2.text = fill[1];
            fill3.text = fill[2];
            fillResult.text = fill[3];
        }

        public List<Transform> outList = new List<Transform>();
        public List<Transform> remainList = new List<Transform>();
        public NiuType CalNiuType(List<Transform> handPokerList, bool doTween = true) {
            Debug.Log("计算牛的类型");
            int num1;
            int num2;
            NiuType niuType = 0;
            //foreach (var a in handPokerList) {
            //    Debug.Log("扑克【："+a+"值："+a.GetComponent<Poker>().Value+"名字："+a.name);
            //}
            NiuType niu = ConfirmNiuType(handPokerList);
            if (niu != 0) {
                niuType = niu;
            }
            niu = ConfirmBomb(handPokerList);
            if (niu != 0) {
                niuType = niu;
            }

            niu = ConfirmMaxNiu(handPokerList);
            if (niu != 0) {
                niuType = niu;
            }
            niu = ConfirmMinNiu(handPokerList);
            if (niu != 0) {
                niuType = niu;
            }
            //if (niu == 0&& outList == null) {
            //    niuType = NiuType.没牛;
            //}
            if (doTween) {
                if ((int)niuType > 0 && (int)niuType <= 9) {
                    OutPokerDOTween();
                } else {
                    fill[0] = "";
                    fill[1] = "";
                    fill[2] = "";
                    fill[3] = "";
                    //fill[3] = num.ToString();
                    fill1.text = fill[0];
                    fill2.text = fill[1];
                    fill3.text = fill[2];
                    fillResult.text = fill[3];
                }
            }
            Debug.Log("计算出的牛的类型："+niuType);
            return niuType;
        }
        private NiuType ConfirmBomb(List<Transform> handPokerList) {
            NiuType niuType;
            for (int i = 0; i < handPokerList.Count; i++) {
                for (int j = i + 1; j < handPokerList.Count; j++) {
                    for (int k = j + 1; k < handPokerList.Count; k++) {
                        for (int l = k + 1; l < handPokerList.Count; l++) {
                            if (handPokerList[i].name == handPokerList[j].name && handPokerList[j].name == handPokerList[k].name && handPokerList[k].name == handPokerList[l].name) {
                                outList.Clear();
                                remainList.Clear();
                                //outList.Add(handPokerList[i]);
                                //outList.Add(handPokerList[j]);
                                //outList.Add(handPokerList[k]);
                                //outList.Add(handPokerList[l]);
                                //handPokerList[i].DOLocalMoveY(40,0.2f);
                                //handPokerList[j].DOLocalMoveY(40, 0.2f);
                                //handPokerList[k].DOLocalMoveY(40, 0.2f);
                                //handPokerList[l].DOLocalMoveY(40, 0.2f);
                                niuType = NiuType.炸弹;
                                return niuType;
                            }
                        }
                    }
                }
            }
            return 0;
        }
        private NiuType ConfirmMaxNiu(List<Transform> handPokerList) {
            int count = 0;
            int tempValue=0;
            for (int i = 0; i < handPokerList.Count; i++) {
                int.TryParse(handPokerList[i].name, out tempValue);
                if ( tempValue>= 11) {
                    count++;
                }
                //if (handPokerList[i].GetComponent<Poker>().PokerNumber >= 11) {
                //    count++;
                //}
            }
            if (count == 5) {
                outList.Clear();
                remainList.Clear();
                return NiuType.五花牛;
            }
            return 0;
        }
        private NiuType ConfirmMinNiu(List<Transform> handPokerList) {
            //NiuType niuType;
            int count = 0;
            int tempValue = 0;
            int[] val = new int[handPokerList.Count];
            for (int i = 0; i < handPokerList.Count; i++) {
                //int value = handPokerList[i].GetComponent<Poker>().PokerNumber;
                int.TryParse(handPokerList[i].name, out tempValue);
                if (tempValue <= 4) {
                    count++;
                    val[i] = tempValue;
                }
            }
            if (count == 5) {
                int num = 0;
                for (int i = 0; i < val.Length; i++) {
                    num += val[i];
                }
                if (num == 10) {
                    outList.Clear();
                    remainList.Clear();
                    return NiuType.五小牛;
                }
            }
            return 0;
        }

        private NiuType ConfirmNiuType(List<Transform> handPokerList) {
            int pokerNum1;
            int pokerNum2;
            int pokerNum3;
            NiuType niuType = 0;
            outList.Clear();
            remainList.Clear();
            for (int i = 0; i < handPokerList.Count; i++) {
                for (int j = i + 1; j < handPokerList.Count; j++) {
                    for (int k = j + 1; k < handPokerList.Count; k++) {
                        int.TryParse(handPokerList[i].name, out pokerNum1);
                        int.TryParse(handPokerList[j].name, out pokerNum2);
                        int.TryParse(handPokerList[k].name, out pokerNum3);
                        if (pokerNum1 >= 11) {
                            pokerNum1 = 10;
                        }
                        if (pokerNum2 >= 11) {
                            pokerNum2 = 10;
                        }
                        if (pokerNum3 >= 11) {
                            pokerNum3 = 10;
                        }

                        if ((pokerNum1 + pokerNum2 + pokerNum3) % 10 == 0) {
                            //List<Transform> tran = new List<Transform>();
                            outList.Add(handPokerList[i]);
                            outList.Add(handPokerList[j]);
                            outList.Add(handPokerList[k]);

                            foreach (var a in handPokerList) {
                                remainList.Add(a);
                            }
                            foreach (var a in outList) {
                                remainList.Remove(a);
                            }
                            int.TryParse(remainList[0].name, out num1);
                            int.TryParse(remainList[1].name, out num2);
                            if (num1 >= 11) {
                                num1 = 10;
                            }
                            if (num2 >= 11) {
                                num2 = 10;
                            }
                            //num1 = remainList[0].GetComponent<Poker>().Value;
                            //num2 = remainList[1].GetComponent<Poker>().Value;
                            int typeNum = (num1 + num2) % 10;
                            switch (typeNum) {
                                case 0:
                                    niuType = NiuType.牛牛;
                                    break;
                                case 1:
                                    niuType = NiuType.牛1;
                                    break;
                                case 2:
                                    niuType = NiuType.牛2;
                                    break;
                                case 3:
                                    niuType = NiuType.牛3;
                                    break;
                                case 4:
                                    niuType = NiuType.牛4;
                                    break;
                                case 5:
                                    niuType = NiuType.牛5;
                                    break;
                                case 6:
                                    niuType = NiuType.牛6;
                                    break;
                                case 7:
                                    niuType = NiuType.牛7;
                                    break;
                                case 8:
                                    niuType = NiuType.牛8;
                                    break;
                                case 9:
                                    niuType = NiuType.牛9;
                                    break;
                            }
                            return niuType;
                        }
                        //return tran;

                    }
                }
            }
            return 0;
        }
        public void HidePlayer(int playerIndex) {
            //Debug.Log("根据索引隐藏玩家");
            switch (playerIndex) {
                case 0:
                    player1.SetActive(false);
                    break;

                case 1:
                    player2.SetActive(false);
                    break;

                case 2:
                    player3.SetActive(false);
                    break;

                case 3:
                    player4.SetActive(false);
                    break;

                case 4:
                    player5.SetActive(false);
                    break;

                    //case 5:
                    //    player6.SetActive(false);
                    //    break;
            }
        }
        int value;
        public void DeleteClaText(Transform pokerTrans) {
            //Debug();
            pokerTrans.DOLocalMoveY(7f, 0.2f);
            fillList.Remove(pokerTrans);
            //outCount--;
            fill[0] = "";
            fill[1] = "";
            fill[2] = "";
            fill[3] = "";
            int tempNum = 0;
            for (int i = 0; i < fillList.Count; i++) {
                int.TryParse(fillList[i].name, out value);
                if (value >= 11) {
                    value = 10;
                }
                tempNum += value;
                fill[i] = value.ToString();
            }
            fill[3] = tempNum.ToString();
            if (fillList.Count == 0) {
                fill[3] = "";
            }
            fill1.text = fill[0];
            fill2.text = fill[1];
            fill3.text = fill[2];
            fillResult.text = fill[3];
        }
        Transform lastParent;
        List<Transform> tempFillList = new List<Transform>();
        public void FillCalText(Transform pokerTrans) {
            //Poker poker = pokerTrans.GetComponent<Poker>();
            fill[0] = "";
            fill[1] = "";
            fill[2] = "";
            fill[3] = "";
            if (fillList.Count >= 2) {
                pokerTrans.DOLocalMoveY(40, 0.2f);
                fillList.Add(pokerTrans);
                tempFillList.Add(pokerTrans);
                //int.TryParse(fillList[0].name, out num1);
                //int.TryParse(fillList[1].name, out num2);
                //int.TryParse(fillList[2].name, out num3);
                //int.TryParse(fill[0], out num1);
                //int.TryParse(fill[1], out num2);
                //int.TryParse(fill[2], out num3);
                int tempNum = 0;
                for (int i = 0; i < fillList.Count; i++) {
                    int.TryParse(fillList[i].name, out value);
                    if (value >= 11) {
                        value = 10;
                    }
                    fill[i] = value.ToString();
                    tempNum += value;
                }
                fill[3] = tempNum.ToString();
                //if (DNGlobalData.waitEnd) {
                //    DNGlobalData.hintDouble = true;
                //    //
                //}
                if (tempNum % 10 == 0 && lastParent == pokerTrans.parent) {
                    //Debug.Log();
                    if (lastParent.name == "HandPokerPoint1") {
                        GetDictNiuType1(0).SetActive(true);
                        GetDictNiuType1(0).transform.GetChild(0).GetComponent<Image>().sprite =
                            DynamicUIManager.Instance.BaseNiuTypeGetSprite(CalNiuType(handPoker));
                        foreach (var a in handPoker) {
                            if (a.GetComponent<Poker>() != null) {
                                GameObject.Destroy(a.GetComponent<Poker>());
                            }
                        }

                        //if (DNGlobalData.doubleIsDone) {

                        //    HideButton(UIButton.灰亮牌);
                        //    HideButton(UIButton.提示);
                        //    ShowButton(UIButton.灰提示);
                        //    ShowButton(UIButton.亮牌);
                        //    DNGlobalData.doubleIsDone = false;
                        //}
                        DNGlobalData.hintDouble = true;
                        DNGlobalData.hintSingle = false;
                        DNGlobalData.confirmOutSingle = true;
                    } else if (lastParent.name == "HandPokerPoint2") {
                        GetDictNiuType2(0).SetActive(true);
                        GetDictNiuType2(0).transform.GetChild(0).GetComponent<Image>().sprite =
                            DynamicUIManager.Instance.BaseNiuTypeGetSprite(CalNiuType(handPoker2));
                        foreach (var a in handPoker2) {
                            if (a.GetComponent<Poker>() != null) {
                                GameObject.Destroy(a.GetComponent<Poker>());
                            }
                        }
                        DNGlobalData.hintDouble = false;
                        DNGlobalData.hintSingle = true;
                        DNGlobalData.confirmOutDouble = true;
                        //if (DNGlobalData.hintDouble) {
                        //    HideButton(UIButton.灰亮牌);
                        //    HideButton(UIButton.提示);
                        //    ShowButton(UIButton.灰提示);
                        //    ShowButton(UIButton.亮牌);
                        //} 
                        //else {
                        //    DNGlobalData.doubleIsDone = true;
                        //}
                    } else if (lastParent.name == "HandPokerPoint3") {
                        //DNGlobalData.confirmOutDouble = true;
                        if (handPoker2.Count != 0) {
                            handPoker2.Clear();
                        }
                        GetDictNiuType3(0).SetActive(true);
                        GetDictNiuType3(0).transform.GetChild(0).GetComponent<Image>().sprite =
                            DynamicUIManager.Instance.BaseNiuTypeGetSprite(CalNiuType(handPoker));
                        foreach (var a in handPoker) {
                            if (a.GetComponent<Poker>() != null) {
                                GameObject.Destroy(a.GetComponent<Poker>());
                            }
                        }
                        HideButton(UIButton.灰亮牌);
                        HideButton(UIButton.提示);
                        ShowButton(UIButton.灰提示);
                        ShowButton(UIButton.亮牌);
                        //DNGlobalData.confirmOutSingle = true;
                        DNGlobalData.hintDouble = false;
                        DNGlobalData.hintSingle = true;
                    }
                    if (DNGlobalData.confirmOutDouble && DNGlobalData.confirmOutSingle) {
                        HideButton(UIButton.灰亮牌);
                        HideButton(UIButton.提示);
                        ShowButton(UIButton.灰提示);
                        ShowButton(UIButton.亮牌);
                        DNGlobalData.confirmOutSingle = false;
                        DNGlobalData.confirmOutDouble = false;
                        DNGlobalData.waitEnd = false;
                        //DNGlobalData.confirmOutSingle = true;
                        DNGlobalData.hintDouble = false;
                        DNGlobalData.hintSingle = true;
                    }
                    fillList.Clear();
                }
            } else if(fillList.Count<3){
                pokerTrans.DOLocalMoveY(40, 0.2f);
                fillList.Add(pokerTrans);
                tempFillList.Add(pokerTrans);
                int tempNum = 0;
                for (int i = 0; i < fillList.Count; i++) {
                    int.TryParse(fillList[i].name, out value);
                    if (value >= 11) {
                        value = 10;
                    }
                    tempNum += value;
                    fill[i] = value.ToString();
                }
                fill[3] = tempNum.ToString();
            }
            fill1.text = fill[0];
            fill2.text = fill[1];
            fill3.text = fill[2];
            fillResult.text = fill[3];
            if (lastParent != null && lastParent != pokerTrans.parent) {
                for (int i = 0; i < fill.Length; i++) {
                    fill[i] = "";
                }
                if (lastParent.name == "HandPokerPoint2") {
                    foreach (var a in handPoker2) {
                        a.DOLocalMoveY(7f, 0.2f);
                    }
                } else {
                    foreach (var a in handPoker) {
                        a.DOLocalMoveY(7f, 0.2f);
                    }
                }
                fill[0] = pokerTrans.name;
                //foreach (var a in fillList) {
                //    a.DOLocalMoveY(7f, 0.2f);
                //}
                fillList.Clear();
                //outCount = 0;
                pokerTrans.DOLocalMoveY(40, 0.2f);
                fillList.Add(pokerTrans);
                fill1.text = fill[0];
                fill2.text = fill[1];
                fill3.text = fill[2];
                fillResult.text = fill[3];
            }
            lastParent = pokerTrans.parent;
        }
    }
}
