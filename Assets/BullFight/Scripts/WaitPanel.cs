using Assets.Scripts.BullFight.Data;
using com.guojin.dn.net.message;
using com.guojin.mj.net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using DG.Tweening;
using Assets.Scripts.BullFight.Manager;

public class WaitScnePlayer {
    GameObject player;
    Image ready;
    Image userImage;
    Image voice;
    Image offLine;
    Text userName;
    Transform loding;
    bool canLoding;

    public GameObject Player {
        get {
            return player;
        }

        set {
            player = value;
        }
    }

    public Image Ready {
        get {
            return ready;
        }

        set {
            ready = value;
        }
    }

    public Image UserImage {
        get {
            return userImage;
        }

        set {
            userImage = value;
        }
    }

    public Image Voice {
        get {
            return voice;
        }

        set {
            voice = value;
        }
    }

    public Image OffLine {
        get {
            return offLine;
        }

        set {
            offLine = value;
        }
    }

    public Text UserName {
        get {
            return userName;
        }

        set {
            userName = value;
        }
    }

    public Transform Loding {
        get {
            return loding;
        }

        set {
            loding = value;
        }
    }

    public bool CanLoding {
        get {
            return canLoding;
        }

        set {
            canLoding = value;
        }
    }
}
public class WaitPanel : EventManager {
    private GameObject voicePanel,inputField, pushVoice,chatInfoSelf, chatContent,rulePanel;
    private Button startButton, cancelButton, microPhoneButton, keyBordButton,sendButton,readyButton,inviteButton,cantStartButton;
    [HideInInspector]
    //public Text noticeText;
    public Dictionary<int, WaitScnePlayer> playerDict = new Dictionary<int, WaitScnePlayer>();
    public Dictionary<UIButton, Button> buttonDict = new Dictionary<UIButton, Button>();
    [HideInInspector]
    public int readyCount;
    public WaitScnePlayer InitPlayerDict(string playerPath) {
        WaitScnePlayer waitPlayer = new WaitScnePlayer();
        //Transform player = 
        waitPlayer.Player= transform.FindChild(playerPath).gameObject;
        waitPlayer.Ready = waitPlayer.Player.transform.FindChild("Ready").GetComponent<Image>();
        waitPlayer.OffLine = waitPlayer.Player.transform.FindChild("OffLine").GetComponent<Image>();
        waitPlayer.Voice = waitPlayer.Player.transform.FindChild("Voice").GetComponent<Image>();
        waitPlayer.UserName = waitPlayer.Player.transform.FindChild("UserName").GetComponent<Text>();
        waitPlayer.UserImage = waitPlayer.Player.transform.FindChild("Mask/UserImage").GetComponent<Image>();
        waitPlayer.Loding = waitPlayer.Player.transform.FindChild("Mask/Loding");
        return waitPlayer;
    }
    void Awake() {
        MainLogic.Controller.gameType = GameType.DN;
        chatContent = transform.FindChild("ChatPart/Scroll View/Viewport/Content_0").gameObject;
        //chatInfoSelf = Resources.Load<GameObject>("BullFightPrefab/ChatInfoSelf");
        chatInfoSelf = Resources.Load<GameObject>("BullFightPrefab/ChatInfo");
        //chatInfoSelf = Resources.Load<GameObject>("BullFightPrefab/ChatInfo");
        inputField = transform.FindChild("ChatPart/TextChat/InputField").gameObject;
        startButton = transform.FindChild("StartButton").GetComponent<Button>();
        cantStartButton = transform.FindChild("CantStartButton").GetComponent<Button>();
        readyButton = transform.FindChild("ReadyButton").GetComponent<Button>();
        cancelButton = transform.FindChild("CancelButton").GetComponent<Button>();
        inviteButton = transform.FindChild("InviteButton").GetComponent<Button>();
        microPhoneButton = transform.FindChild("ChatPart/VoiceChat/MicroPhoneButton").GetComponent<Button>();
        keyBordButton = transform.FindChild("ChatPart/TextChat/KeyBordButton").GetComponent<Button>();
        sendButton = transform.FindChild("ChatPart/SendButton").GetComponent<Button>();
        //noticeText = transform.FindChild("NoticeText").GetComponent<Text>();
        buttonDict.Add(UIButton.准备, readyButton);
        buttonDict.Add(UIButton.邀请, inviteButton);
        buttonDict.Add(UIButton.开始, startButton);
        buttonDict.Add(UIButton.取消准备, cancelButton);
        buttonDict.Add(UIButton.灰色开始, cantStartButton);
        pushVoice = transform.FindChild("ChatPart/VoiceChat/PushButton").gameObject;
        transform.FindChild("RoomID").GetComponent<Text>().text = DNGlobalData.roomID;
        playerDict.Add(0, InitPlayerDict("Players/Player1"));
        playerDict.Add(1, InitPlayerDict("Players/Player2"));
        playerDict.Add(2, InitPlayerDict("Players/Player3"));
        playerDict.Add(3, InitPlayerDict("Players/Player4"));
        //playerDict.Add(4, InitPlayerDict("Players/Player5"));
        for (int i = 0; i < playerDict.Count; i++) {
            
            GetDictReady(i).enabled = false;
           // GetDictUserImage(i).enabled = false;
            GetDictVoice(i).enabled = false;
            GetDictOffLine(i).enabled = false;
            GetDictLoding(i).gameObject.SetActive(false);
            GetDictUserName(i).text = "";
        }
        GetDictReady(0).enabled = true;
        //noticeText.text = "";
        inputField.SetActive(true);
        startButton.gameObject.SetActive(false);
        HideButton(UIButton.准备);
        HideButton(UIButton.取消准备);
        HideButton(UIButton.开始);
        ShowButton(UIButton.邀请);
        ShowButton(UIButton.灰色开始);
        microPhoneButton.gameObject.SetActive(false);
        pushVoice.SetActive(false);

    }
    public void ShowButton(UIButton button) {
        buttonDict[button].gameObject.SetActive(true);
    }
    public void HideButton(UIButton button) {
        buttonDict[button].gameObject.SetActive(false);
    }
    float val;
    private int loadIndex;
    public void StartLoading(int index,bool start) {
        loadIndex = index;
        SetDictCanLoding(loadIndex, start);
        if (start) {
            GetDictLoding(loadIndex).gameObject.SetActive(true);
        } else {
            GetDictLoding(loadIndex).gameObject.SetActive(false);
        }
    }
    private void Update() {
        if (GetDictCanLoding(loadIndex)) {
            GetDictLoding(loadIndex).localRotation = Quaternion.Euler(new Vector3(0, 0, val));
            val -= Time.deltaTime * 300;
        }
        //if (GetDictCanLoding(0)) {
        //    GetDictLoding(0).localRotation = Quaternion.Euler(new Vector3(0, 0, val));
        //    val -= Time.deltaTime * 300;
        //}
        //if (Input.GetKeyDown(KeyCode.Space)) {
        //    if (GetDictCanLoding(0)) {
        //        SetDictCanLoding(0, false);
        //    } else {
        //        SetDictCanLoding(0, true);
        //        LodingDoTween(0);
        //    }
        //}
    }
    private void Start() {

        if (SocketMgr.GetInstance() != null) {
            DouniuGameRoomInfo msg = new DouniuGameRoomInfo();
            SocketMgr.GetInstance().Send(Net.instance.write(msg));
        }
        transform.FindChild("DissolutionButton").GetComponent<Button>().onClick.AddListener(() => {
            transform.PlayButtonVoice();
            if (DNGlobalData.fangZhuUserID == DNGlobalData.currentUserID) {
                DouniuDelRoom delRoom = new DouniuDelRoom();
                SocketMgr.GetInstance().Send(Net.instance.write(delRoom));
                Destroy(rulePanel);
            } else {
                if (DNGlobalData.currentPlayerIsReady) {
                    DouniuStartGame start = new DouniuStartGame();
                    start.IsReady = false;
                    DNGlobalData.currentPlayerIsReady = false;
                    SocketMgr.GetInstance().Send(Net.instance.write(start));
                    //HideButton(UIButton.取消准备);
                    //if (DNGlobalData.currentUserID != DNGlobalData.fangZhuUserID) {
                    //    ShowButton(UIButton.准备);
                    //}
                }
                ExitDouniuRoom exit = new ExitDouniuRoom();
                exit.RoomID = DNGlobalData.roomID;
                SocketMgr.GetInstance().Send(Net.instance.write(exit));

            }
        });
        transform.FindChild("RuleBtn").GetComponent<Button>().onClick.AddListener(() => {
            transform.PlayButtonVoice();
            if (rulePanel == null) {
                rulePanel = Instantiate(Resources.Load<GameObject>("BullFightPrefab/WaitRulePanelPortrait"));
                rulePanel.transform.SetParent(GameObject.Find("Canvas/addNode").transform);
                rulePanel.transform.localScale = Vector3.one;
                rulePanel.transform.localPosition = Vector3.zero;
                rulePanel.SetActive(false);
            }
            rulePanel.SetActive(true);
        });
        transform.FindChild("InviteButton").GetComponent<Button>().onClick.AddListener(() => {
            transform.PlayButtonVoice();
           // LoginAndShare.Controller.OnClickShareDouniuInfo(0);
        });
        startButton.onClick.AddListener(() => {
            transform.PlayButtonVoice();
            DouniuStartGame start = new DouniuStartGame();
            start.IsReady = true;
            //start.ReadyCount = DNGlobalData.roomPlayerNumber;
            SocketMgr.GetInstance().Send(Net.instance.write(start));
             //
            HideButton(UIButton.开始);
            ShowButton(UIButton.灰色开始);
        });
        cancelButton.onClick.AddListener(() => {
            transform.PlayButtonVoice();
            DouniuStartGame start = new DouniuStartGame();
            start.IsReady = false;
            //int co = readyCount;
            //co--;
            //start.ReadyCount = co;
            DNGlobalData.currentPlayerIsReady = false;
            SocketMgr.GetInstance().Send(Net.instance.write(start));
            HideButton(UIButton.取消准备);
            if (DNGlobalData.currentUserID != DNGlobalData.fangZhuUserID) {
                ShowButton(UIButton.准备);
            }
            //readyCount--;
            //if (readyCount == DNGlobalData.roomPlayerNumber - 1 && DNGlobalData.roomPlayerNumber >= 2) {
            //    if (DNGlobalData.currentUserID == DNGlobalData.fangZhuUserID) {
            //        ShowButton(UIButton.开始);
            //    }
            //} else {
            //    HideButton(UIButton.开始);
            //}
        });
        readyButton.onClick.AddListener(() => {
            transform.PlayButtonVoice();
            DouniuStartGame start = new DouniuStartGame();
            start.IsReady = true;
            //int co = readyCount;
            //co++;
            //start.ReadyCount = co;
            DNGlobalData.currentPlayerIsReady = true;
            SocketMgr.GetInstance().Send(Net.instance.write(start));
            HideButton(UIButton.准备);
            //readyCount++;
            if (DNGlobalData.currentUserID != DNGlobalData.fangZhuUserID) {
                ShowButton(UIButton.取消准备);
            }
            //if (readyCount == DNGlobalData.roomPlayerNumber - 1 && DNGlobalData.roomPlayerNumber >= 2) {
            //    if (DNGlobalData.currentUserID == DNGlobalData.fangZhuUserID) {
            //        ShowButton(UIButton.开始);
            //    }
            //} else {
            //    HideButton(UIButton.开始);
            //}
        });
        sendButton.onClick.AddListener(() => {
            transform.PlayButtonVoice();
            string str = inputField.GetComponent<InputField>().text;
            GameObject obj = Instantiate(chatInfoSelf);
            obj.transform.SetParent(chatContent.transform);
            obj.transform.localScale = Vector3.one;
            //obj.transform.FindChild("UserName").GetComponent<Text>().text = "："+GetDictUserName(DNGlobalData.locationIndex).text;
            obj.transform.FindChild("UserName").GetComponent<Text>().text =   GetDictUserName(DNGlobalData.locationIndex).text+"：";
            obj.transform.FindChild("ChatMessage").GetComponent<Text>().text = str;
            inputField.GetComponent<InputField>().text = "";
            DouniuChat chat = new DouniuChat();
            chat.setChatContent(str);
            chat.setIndex(1);
            SocketMgr.GetInstance().Send(Net.instance.write(chat));
        });
        microPhoneButton.onClick.AddListener(() => {
            transform.PlayButtonVoice();
            microPhoneButton.gameObject.SetActive(false);
            pushVoice.SetActive(false);
            keyBordButton.gameObject.SetActive(true);
            inputField.SetActive(true);
            sendButton.gameObject.SetActive(true);
        });
        keyBordButton.onClick.AddListener(() => {
            transform.PlayButtonVoice();
            microPhoneButton.gameObject.SetActive(true);
            pushVoice.SetActive(true);
            sendButton.gameObject.SetActive(false);
            keyBordButton.gameObject.SetActive(false);
            inputField.SetActive(false);

        });
    }
    //bool canLoad = false;
    //private void Update() {
    //    if (canLoad) {

    //    }
    //}
    public GameObject GetDictPlayer(int playerIndex) {
        WaitScnePlayer waitPlayer;
        if (playerDict.TryGetValue(playerIndex, out waitPlayer)) {
            return waitPlayer.Player;
        }
        return null;
    }
    public bool GetDictCanLoding(int playerIndex) {
        WaitScnePlayer waitPlayer;
        if (playerDict.TryGetValue(playerIndex, out waitPlayer)) {
            return waitPlayer.CanLoding;
        }
        return false;
    }
    public void SetDictCanLoding(int playerIndex,bool value) {
        WaitScnePlayer waitPlayer;
        if (playerDict.TryGetValue(playerIndex, out waitPlayer)) {
            waitPlayer.CanLoding=value;
        }
    }
    public Image GetDictReady(int playerIndex) {
        WaitScnePlayer waitPlayer;
        if (playerDict.TryGetValue(playerIndex, out waitPlayer)) {
            return waitPlayer.Ready;
        }
        return null;
    }
    public Image GetDictUserImage(int playerIndex) {
        WaitScnePlayer waitPlayer;
        if (playerDict.TryGetValue(playerIndex, out waitPlayer)) {
            return waitPlayer.UserImage;
        }
        return null;
    }
    public Transform GetDictLoding(int playerIndex) {
        WaitScnePlayer waitPlayer;
        if (playerDict.TryGetValue(playerIndex, out waitPlayer)) {
            return waitPlayer.Loding;
        }
        return null;
    }
    public Image GetDictOffLine(int playerIndex) {
        WaitScnePlayer waitPlayer;
        if (playerDict.TryGetValue(playerIndex, out waitPlayer)) {
            return waitPlayer.OffLine;
        }
        return null;
    }
    public Image GetDictVoice(int playerIndex) {
        WaitScnePlayer waitPlayer;
        if (playerDict.TryGetValue(playerIndex, out waitPlayer)) {
            return waitPlayer.Voice;
        }
        return null;
    }
    public Text GetDictUserName(int playerIndex) {
        WaitScnePlayer waitPlayer;
        if (playerDict.TryGetValue(playerIndex, out waitPlayer)) {
            return waitPlayer.UserName;
        }
        return null;
    }

    public override void InformationSetting() {
        throw new NotImplementedException();
    }
}
