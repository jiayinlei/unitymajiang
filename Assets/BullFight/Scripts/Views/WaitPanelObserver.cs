using System.Collections;
using System.Collections.Generic;
using com.guojin.core.io.message;
using UnityEngine;
using com.guojin.mj.net.message;
using com.guojin.dn.net.message;
using Assets.Scripts.BullFight.Data;
using Assets.Scripts.BullFight.Manager;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Assets.Scripts.BullFight.View;
using GameCore;
using System.Threading;
using System.Net;

public class WaitPanelObserver : Observer {
    GameManager manager;
    int[] positionArr;
    List<Vector3> localPosition;
    WaitPanel waitPanel;
    GameObject chatInfo;
    GameObject chatContent;
    int voiceIndex;
    AudioSource tranAudio;
    Thread t;
    Texture2D texture;
    Sprite sp;
    private GridLayoutGroup playersParent;
    private List<DouniuChatRet> cliplist = new List<DouniuChatRet>();
    //List<GameObject> players = new List<GameObject>();
    // Use this for initialization
    private void Awake() {
        GameManager.instance.ClearAllDictAndList();
    }
    void Start() {
        //manager = GameManager.instance;
        waitPanel = transform.GetComponent<WaitPanel>();
        chatContent = transform.FindChild("ChatPart/Scroll View/Viewport/Content_0").gameObject;
        chatInfo = Resources.Load<GameObject>("BullFightPrefab/ChatInfo");
        tranAudio = transform.GetComponent<AudioSource>();
        playersParent = transform.FindChild("Players").GetComponent<GridLayoutGroup>();
        //for (int i = 0; i < 5; i++) {
        //    players.Add(transform.FindChild("Players/HeadPhoto" + i).gameObject);
        //    players[i].SetActive(false);
        //}
        //InitUI();
        initMsg();
        //DownSprite(null);
    }
    protected override string[] GetMsgList() {
        return new string[] {
            MessageFactoryImpi.instance.getMessageString(5,24),
            MessageFactoryImpi.instance.getMessageString(5, 25),
            MessageFactoryImpi.instance.getMessageString(5, 23),
            MessageFactoryImpi.instance.getMessageString(5, 9),
            MessageFactoryImpi.instance.getMessageString(5, 51),
            MessageFactoryImpi.instance.getMessageString(5, 44),
            MessageFactoryImpi.instance.getMessageString(5, 45),
            MessageFactoryImpi.instance.getMessageString(5, 7),
            MessageFactoryImpi.instance.getMessageString(5, 35),
            MessageFactoryImpi.instance.getMessageString(5, 53)

        };
    }
    public override void OnMsg(string msg, Message data) {
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 24)) {
            DouniuGameRoomInfoRet roomInfo = data as DouniuGameRoomInfoRet;
            SetSceneMessage(roomInfo);
        }
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 23)) {
            Debug.Log("有人退出---->");
            DouniuExitRoomRet resp = data as DouniuExitRoomRet;
            ExitRoom(resp);
        }
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 25)) {
            Debug.Log("有人加入---->");
            DouniuGameUserInfo resp = data as DouniuGameUserInfo;
            AddPlayer(resp);
        }
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 9)) {
            DouniuStartGameRet ready = data as DouniuStartGameRet;
            Debug.Log("有玩家准备了，index: " + ready.Index);
            ReadyRet(ready);
        }
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 44)) {
            DouniuUserOffline resp = data as DouniuUserOffline;
            UserOffLine(resp);
        }
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 7)) {
            ExitDouniuRoomResult resp = data as ExitDouniuRoomResult;
            //UserOffLine(resp);
            ExitWaitRoom(resp);
        }
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 45)) {
            //DouniuVoteRet resp = data as DouniuVoteRet;
            DelRoom();
        }
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 51)) {
            DouniuChatRet resp = data as DouniuChatRet;
            switch (resp.getIndex()) {
                case 1:
                    if (DNGlobalData.locationIndex != resp.getUserindex()) {
                        GameObject obj = Instantiate(chatInfo);
                        obj.transform.SetParent(chatContent.transform);
                        obj.transform.localScale = Vector3.one;
                        obj.transform.FindChild("UserName").GetComponent<Text>().text = waitPanel.GetDictUserName(resp.getUserindex()).text + "：";
                        obj.transform.FindChild("ChatMessage").GetComponent<Text>().text = resp.getChatContent();
                    }
                    break;
                case 4:
                    voiceIndex = resp.getUserindex();
                    StartCoroutine(DownAddress(resp.getChatContent()));
                    //cliplist.Add(resp);
                    break;
            }
        }
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 35)) {
            QiangZhuangRet qiang = data as QiangZhuangRet;
            DNGlobalData.zhuangInfo = qiang;
        }
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 53)) {
            //抢手牌
            DouniuCasiPaiRet resp = data as DouniuCasiPaiRet;
            if (!DNGlobalData.paiCountDic.ContainsKey(resp.getIndex())) {
                DNGlobalData.paiCountDic.Add(resp.getIndex(), resp.getPaiLens());
            }
        }

    }
    Texture2D tex2d;
    int doneCount;
    int voiceDoneCount;
    bool changeImage=true;
    int index;
    IEnumerator DownSprite() {
        string path = DNGlobalData.spritePathList[index];
        if (path == null) {
            //int i = Random.Range(0, 2);
            int i = DNGlobalData.locationIndex;
            switch (i) {
                case 0:
                    path = "http://touxiang.qqzhi.com/uploads/2012-11/1111012929751.jpg";
                    break;
                case 1:
                    path = "http://touxiang.qqzhi.com/uploads/2012-11/1111021857439.jpg";
                    break;
            }
        }
        WWW www = new WWW(path);
        yield return www;
        if (www.isDone) {
            texture = www.texture;
            sp = CreateSprite(texture, texture.width, texture.height);
            print("玩家头像索引：" + index);
            waitPanel.GetDictUserImage(index).sprite = sp;
            waitPanel.StartLoading(index, false);
            index++;
            if (index < DNGlobalData.spritePathList.Count) {
                waitPanel.StartLoading(index, true);
                StartCoroutine(DownSprite());
            }
            if (!string.IsNullOrEmpty(www.error)) {
                waitPanel.GetDictUserImage(index).sprite = DynamicUIManager.Instance.BaseNameGetSprite("无法查看");
            }
        }
    }
    IEnumerator DownAddress(string path) {
        //int index = cliplist[0].getIndex();
        //string path = cliplist[0].getChatContent();
        WWW www = new WWW(path);
        yield return www;
        if (www.isDone) {
            tranAudio.clip = www.GetAudioClipCompressed(true, AudioType.OGGVORBIS);
            tranAudio.volume = 1;
            tranAudio.Play();
            SetUserVoice(voiceIndex);
        }
        //cliplist.Clear();
        //planPlaying = false;
    }
    public void ExitWaitRoom(ExitDouniuRoomResult exit) {
        DNGlobalData.spriteDict.Clear();
        DNGlobalData.userIDList.Clear();
        DNGlobalData.spritePathList.Clear();
        readyCount = 0;
        //DNGlobalData.userNameList.Clear();
        Destroy(gameObject);
        UIManager.ChangeUI(UIManager.PageState.DNMainPanelPage, (GameObject obj) => {
            obj.GetComponent<DNCreatRoomPage>().InformationSetting();
        });
        //UIState_LoadingPage.isComeFromDN = true;
        //GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_LoadingPage);
    }
    public void SetUserVoice(int index) {
        Image voiceImg = waitPanel.GetDictVoice(index);
        voiceImg.enabled = true;
        //voiceImg.sprite = DynamicUIManager.Instance.BaseNameGetSprite("trumpet");
        voiceImg.DOFade(1, 0);
        Tweener te = voiceImg.DOFade(0, 1);
        te.OnComplete(() => {
            Tweener tw = voiceImg.DOFade(1, 0.5f);
            tw.OnComplete(() => {
                Tweener tr = voiceImg.DOFade(0, 1);
                tr.OnComplete(() => {
                    Tweener tn = voiceImg.DOFade(1, 0.5f);
                    tn.OnComplete(() => {
                        voiceImg.DOFade(0, 1);
                    });
                });
            });
        });
        tranAudio.volume = 1.0f;
        tranAudio.Play();


    }
    private void Update() {
        if (doneCount == DNGlobalData.roomPlayerNumber&&changeImage) {
            //print("done");
            for (int i = 0; i < DNGlobalData.userImage.Count; i++) {
                waitPanel.GetDictUserImage(i).sprite=DNGlobalData.userImage[i];
            }
            changeImage = false;
        }
        //if(tranAudio)
    }
    public static Sprite CreateSprite(Texture2D texture, int width, int height) {
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0, 0));
        return sp;
    }
    public void SetSceneMessage(DouniuGameRoomInfoRet gameRoominfo) {
        //if (!DNGlobalData.isChongLian) {
        //if (DNGlobalData.isChongLian) {
        //    DNGlobalData.confirmChongLian = true;
        //    //DNGlobalData.isChongLian = true;
        //    //if (gameRoominfo.RoomState != null) {
        //    //    //Destroy(gameObject);
        //    //    SceneManager.LoadScene("BullFight");
        //    //}
        //    //Destroy(gameObject);
        //    return;
        List<DouniuGameUserInfo> infoList = gameRoominfo.SceneUser;
        DNGlobalData.locationIndex = gameRoominfo.LocalIndex;
        DNGlobalData.currentUserID = infoList[DNGlobalData.locationIndex].getUserId();
        DNGlobalData.fangZhuUserID = gameRoominfo.CreateUserId;
        Debug.Log("房间状态："+gameRoominfo.RoomState);
        if (!DNGlobalData.isChongLian) {
            if (DNGlobalData.currentUserID == DNGlobalData.fangZhuUserID) {
                waitPanel.HideButton(UIButton.准备);
                waitPanel.ShowButton(UIButton.灰色开始);
                //waitPanel.noticeText.text = "2人即可开始游戏";
            } else {
                waitPanel.ShowButton(UIButton.准备);
            }
            if (gameRoominfo.Start) {

                DNGlobalData.isStart = true;
                DNGlobalData.noticeText = "游戏已经开始不能加入房间";
                GameObject temp = Instantiate(Resources.Load<GameObject>("BullFightPrefab/GameHallNoticePanel"));
                temp.transform.SetParent(gameObject.transform);
                temp.transform.localPosition = Vector3.zero;
                temp.transform.localScale = Vector3.one;
                StartCoroutine(DestroyGameObject());
                return;
            }
        } else if (gameRoominfo.RoomState != null) {
            SceneManager.LoadScene("BullFight");
            return;
        } else if (DNGlobalData.isChongLian) {
            if (DNGlobalData.currentUserID != DNGlobalData.fangZhuUserID) {
                foreach (var a in infoList) {
                    if (a.getLocationIndex() == gameRoominfo.LocalIndex) {
                        if (a.IsReady) {
                            waitPanel.ShowButton(UIButton.取消准备);
                            waitPanel.GetDictReady(a.getLocationIndex()).enabled = true;
                        } else {
                            waitPanel.ShowButton(UIButton.准备);

                        }
                    }
                }
            }
            //if (infoList[DNGlobalData.locationIndex].IsReady) {
            //} else {
            //}
        } 
        //else if (DNGlobalData.isChongLian) {
        //}

        DNGlobalData.isChongLian = false;
        DNGlobalData.confirmChongLian = false;

        DNGlobalData.maxChapter = gameRoominfo.ChapterMax;
        DNGlobalData.changeMode = gameRoominfo.FangShi;
        DNGlobalData.mode = gameRoominfo.MoShi;
        if (gameRoominfo.IsCheck == "1") {
            DNGlobalData.lookPoker = true;
        } else {
            DNGlobalData.lookPoker = false;
        }
        if (DNGlobalData.isFirstCreate) {
            DNGlobalData.roomPlayerNumber = 1;
            DNGlobalData.isFirstCreate = false;
        }
        int maxPlayerNumber;
        DNGlobalData.roomPlayerNumber = infoList.Count;
        DNGlobalData.maxPlayerNumber = gameRoominfo.UserNum.ToString();

        int.TryParse(DNGlobalData.maxPlayerNumber, out maxPlayerNumber);
        //if()
        for (int i = maxPlayerNumber; i < waitPanel.playerDict.Count; i++) {
            waitPanel.GetDictPlayer(i).SetActive(false);
        }
        //switch (maxPlayerNumber) {
        //    case 2:
        //        playersParent.spacing = new Vector2(50, 0);
        //        break;
        //    case 3:
        //        playersParent.spacing = new Vector2(40, 0);
        //        break;
        //    case 4:
        //        playersParent.spacing = new Vector2(30, 0);
        //        break;
        //}
        //此处位置映射可能出错
        //positionArr = manager.SetPlayerPosition(DNGlobalData.roomPlayerNumber, DNGlobalData.locationIndex);
        //for (int j = 0; j < positionArr.Length; j++) {
        //    if (positionArr[j] == 0) {
        //        DNGlobalData.currentUserID = infoList[j].getUserId();
        //    }
        //}
        DNGlobalData.spritePathList.Clear();
        //texture = new Texture2D(100, 100);
        for (int i = 0; i < infoList.Count; i++) {
            //if (DNGlobalData.currentUserID == infoList[i].getUserId() && DNGlobalData.isChongLian) {
            //    if (infoList[i].IsReady) {
            //        waitPanel.ShowButton(UIButton.取消准备);
            //    }
            //}
            if (infoList[i].IsReady) {
                //print("ssdsda:" + i + "+" + infoList[i].IsReady);
                readyCount++;   
                waitPanel.GetDictReady(infoList[i].getLocationIndex()).enabled = true;
            }
            if (!infoList[i].getOnline()) {
                waitPanel.GetDictOffLine(i).enabled = true;
            }
            DNGlobalData.userIDList.Add(infoList[i].getUserId());
            changeImage = true;
            waitPanel.GetDictUserImage(infoList[i].getLocationIndex()).enabled = true;
            waitPanel.GetDictUserName(infoList[i].getLocationIndex()).text = infoList[i].getUserName();
            string path = infoList[infoList[i].getLocationIndex()].getAvatar();
            DNGlobalData.spritePathList.Add(path);
        }
        waitPanel.GetDictReady(0).enabled = true;
        //print("ss:"+readyCount);
        if (readyCount == DNGlobalData.roomPlayerNumber - 1 && DNGlobalData.roomPlayerNumber >= 2) {
            if (DNGlobalData.currentUserID == DNGlobalData.fangZhuUserID) {
                waitPanel.ShowButton(UIButton.开始);
                waitPanel.HideButton(UIButton.灰色开始);
            }
        }
        waitPanel.StartLoading(index, true);
        StartCoroutine(DownSprite());
            if (DNGlobalData.currentUserID != DNGlobalData.fangZhuUserID) {
            transform.FindChild("DissolutionButton").GetComponent<Image>().sprite = DynamicUIManager.Instance.BaseNameGetSprite("退出房间_btn_bule");
        }
    }
    byte[] ThreadDown(string path, int i) {
        if (string.IsNullOrEmpty(path)) {
            switch (i) {
                case 0:
                    path = "http://touxiang.qqzhi.com/uploads/2012-11/1111012929751.jpg";
                    break;
                case 1:
                    path = "http://touxiang.qqzhi.com/uploads/2012-11/1111021857439.jpg";
                    break;
                default:
                    path = "http://touxiang.qqzhi.com/uploads/2012-11/1111021857439.jpg";
                    break;
            }
        }
        WebClient client = new WebClient();
        byte[] result = client.DownloadData(path);
        return result;
    }
    IEnumerator DestroyGameObject() {
        yield return new WaitForSeconds(1);
        Destroy(transform.gameObject);
    }
    int addDownIndex;
    private void AddPlayer(DouniuGameUserInfo addUser) {
        string path = addUser.getAvatar();
        DNGlobalData.userIDList.Add(addUser.getUserId());
        DNGlobalData.roomPlayerNumber = addUser.PlayerNumber;
        //manager.gameManagerObj.GetComponent<GameRoot>().StartCoroutineMethod(path);
        addDownIndex = addUser.getLocationIndex();
        waitPanel.StartLoading(addDownIndex, true);
        StartCoroutine(DownUserImage(path));
        waitPanel.GetDictUserImage(addUser.getLocationIndex()).enabled=true;
        waitPanel.GetDictUserName(addUser.getLocationIndex()).text = addUser.getUserName();
        waitPanel.GetDictReady(addUser.getLocationIndex()).enabled = false;
        waitPanel.GetDictOffLine(addUser.getLocationIndex()).enabled = false;
        waitPanel.GetDictReady(0).enabled = true;
        if (addUser.IsReady) {
            waitPanel.GetDictReady(addUser.getLocationIndex()).enabled = true;
        }
        //if (readyCount == DNGlobalData.roomPlayerNumber-1) {
        //    SceneManager.LoadScene("BullFight");
        //}

    }
    IEnumerator DownUserImage(string path) {
        if (path == null) {
            //int i = Random.Range(0, 2);
            int i = DNGlobalData.locationIndex;
            switch (i) {
                case 0:
                    path = "http://touxiang.qqzhi.com/uploads/2012-11/1111012929751.jpg";
                    break;
                case 1:
                    path = "http://touxiang.qqzhi.com/uploads/2012-11/1111021857439.jpg";
                    break;
            }
        }
        WWW www = new WWW(path);
        yield return www;
        if (www.isDone) {
            texture = www.texture;
            sp = CreateSprite(texture, texture.width, texture.height);
            //print("玩家头像索引：" + index);
            waitPanel.GetDictUserImage(addDownIndex).sprite = sp;
            waitPanel.StartLoading(addDownIndex, false);

        }
        if (!string.IsNullOrEmpty(www.error)) {
            waitPanel.GetDictUserImage(addDownIndex).sprite = DynamicUIManager.Instance.BaseNameGetSprite("无法查看");
        }
    }
    public void UserOffLine(DouniuUserOffline offLine) {
        waitPanel.GetDictOffLine(offLine.getIndex()).enabled = true;
        //readyCount--;
        //if (readyCount == DNGlobalData.roomPlayerNumber - 1 && DNGlobalData.roomPlayerNumber >= 2) {
        //    if (DNGlobalData.currentUserID == DNGlobalData.fangZhuUserID) {
        //        waitPanel.ShowButton(UIButton.开始);
        //        waitPanel.HideButton(UIButton.灰色开始);
        //    } else {
        //    }
        //} else {
        //    waitPanel.HideButton(UIButton.开始);
        //    waitPanel.ShowButton(UIButton.灰色开始);
        //}
    }
    public void DelRoom() {
        DNGlobalData.spriteDict.Clear();
        DNGlobalData.userIDList.Clear();
        DNGlobalData.spritePathList.Clear();
        readyCount = 0;
        //DNGlobalData.userNameList.Clear();
        //if (!DNGlobalData.isChongLian) {
        //Invoke("ShowDelRoomLater", 1);
        //TooL.showNetErroTips("房间已经被解散", () => {
        //});
        StartCoroutine("ShowDelRoomLater");
        UIManager.ChangeUI(UIManager.PageState.DNMainPanelPage, (GameObject obj) => {
                obj.GetComponent<DNCreatRoomPage>().InformationSetting();
            });
        //} else {

        //}
    }
    IEnumerator ShowDelRoomLater() {
       // Debug.Log("aa");
        yield return new WaitForSeconds(0.2f);
        //Debug.Log("bb");
        Destroy(gameObject);
        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/JoinRoomDef"));
        GameObject addnode = GameObject.Find("addNode");
        temp.transform.SetParent(addnode.transform);
        temp.transform.localPosition = Vector3.zero;
        temp.transform.localScale = Vector3.one;
        temp.GetComponentInChildren<RuleLayer>().ShowText("房间已经被解散");
        //TooL.showNetErroTips("房间已经被解散", () => {
        //});
    }
    public void GoToGameHall() {

        GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_GameHallPage);
        GameObject.Find("StaticSource").GetComponent<MainLogic>().SetBgMusic();
        //GameObject.Find("StaticSource").GetComponent<AudioSource>().enabled = true;
    }
    public void ExitRoom(DouniuExitRoomRet exitResult) {
        Debug.Log("退出");
        //DNGlobalData.spritePathList.Clear();
        //int exitIndex = 0;
        for (int i = 0; i < DNGlobalData.userIDList.Count; i++) {
            if (exitResult.getUserID() == DNGlobalData.userIDList[i]) {
                waitPanel.GetDictUserImage(i).enabled=false;
                waitPanel.GetDictReady(i).enabled = false;
                waitPanel.GetDictUserName(i).text = "";
                waitPanel.GetDictVoice(i).enabled = false;
            }
        }
        //readyCount--;
        if (readyCount == DNGlobalData.roomPlayerNumber - 1 && DNGlobalData.roomPlayerNumber >= 2) {
            if (DNGlobalData.currentUserID == DNGlobalData.fangZhuUserID) {
                waitPanel.ShowButton(UIButton.开始);
                waitPanel.HideButton(UIButton.灰色开始);
            }
        } else {
            waitPanel.HideButton(UIButton.开始);
            waitPanel.ShowButton(UIButton.灰色开始);
        }
        DNGlobalData.roomPlayerNumber--;
    }
    int readyCount;
    public void ReadyRet(DouniuStartGameRet start) {
        if (start.IsReady) {
            waitPanel.GetDictReady(start.Index).enabled = true;
            readyCount++;
        } else {
            waitPanel.GetDictReady(start.Index).enabled = false;
            readyCount--;
        }
        if (start.Index==0) {
            //manager.ClearAllDictAndList();
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            Screen.orientation = ScreenOrientation.AutoRotation;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
            Screen.autorotateToPortrait = false;
            Screen.autorotateToPortraitUpsideDown = false;

            DNGlobalData.spriteDict.Clear();
            DNGlobalData.userIDList.Clear();
            DNGlobalData.spritePathList.Clear();
            //DNGlobalData.userNameList.Clear();
            UIState_LoadingPage.toDNFightScene = true;
            Debug.Log("UIState_LoadingPage----WaitPanelObserver");
            GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_LoadingPage);
            //SceneManager.LoadScene("BullFight");
        }
        print("准备次数："+readyCount);
        if (readyCount == DNGlobalData.roomPlayerNumber - 1 && DNGlobalData.roomPlayerNumber >= 2) {
            if (DNGlobalData.currentUserID == DNGlobalData.fangZhuUserID) {
                waitPanel.ShowButton(UIButton.开始);
                waitPanel.HideButton(UIButton.灰色开始);
            } else {
            }
        } else {
            waitPanel.HideButton(UIButton.开始);
            waitPanel.ShowButton(UIButton.灰色开始);
        }
    }
}
