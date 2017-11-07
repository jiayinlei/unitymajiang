using System.Collections;
using System.Collections.Generic;
using com.guojin.core.io.message;
using UnityEngine;
using com.guojin.mj.net.message;
using com.guojin.dn.net.message;
using Assets.Scripts.BullFight.Data;
using Assets.Scripts.BullFight.Managers;
using Assets.Scripts.BullFight.Manager;
using GameCore;
using UnityEngine.UI;
using com.guojin.mj.net;
using DG.Tweening;
using System.Threading;
using System;
using System.Net;
using UnityEngine.SceneManagement;

public class DNGamePanelObserver : Observer {
    
    private List<Vector3> localPosition = new List<Vector3>();
    private int[] positionArr;
    Text roomID, mode;
    GameManager manager;
    int changeIndex=-1;
    int voiceIndex;
    Thread t;
    Texture2D texture;
    Sprite sp;
    bool isOperation = false;

    private void Start() {
        manager = GameManager.instance;
        roomID = transform.FindChild("Table/RoomID").GetComponent<Text>();
        mode = transform.FindChild("Table/Mode").GetComponent<Text>();
        roomID.text = "房间号:" + DNGlobalData.roomID;
        initMsg();
    }
    protected override string[] GetMsgList() {
        return new string[] {
                  MessageFactoryImpi.instance.getMessageString(5,24),
                  MessageFactoryImpi.instance.getMessageString(5,9),
                  MessageFactoryImpi.instance.getMessageString(7,11),
                  MessageFactoryImpi.instance.getMessageString(5,25),
                  MessageFactoryImpi.instance.getMessageString(5,20),
                  MessageFactoryImpi.instance.getMessageString(5,13),
                  MessageFactoryImpi.instance.getMessageString(5,35),
                  MessageFactoryImpi.instance.getMessageString(5,11),
                  MessageFactoryImpi.instance.getMessageString(5,32),
                  MessageFactoryImpi.instance.getMessageString(5,22),
                  MessageFactoryImpi.instance.getMessageString(5,7),
                  MessageFactoryImpi.instance.getMessageString(5,42),
                  MessageFactoryImpi.instance.getMessageString(5,45),
                  MessageFactoryImpi.instance.getMessageString(5,23),
            MessageFactoryImpi.instance.getMessageString(5,51),
            MessageFactoryImpi.instance.getMessageString(5, 44),
            MessageFactoryImpi.instance.getMessageString(5, 53)
        };
    }

    public override void OnMsg(string msg, Message data) {
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 24)) {
            DouniuGameRoomInfoRet roomInfo = data as DouniuGameRoomInfoRet;
            SetSceneMessage(roomInfo);
            if (DNGlobalData.zhuangInfo != null) {
                SetZhuangInfo(DNGlobalData.zhuangInfo);
                DNGlobalData.zhuangInfo = null;
            }

        }
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 9)) {
            Debug.Log("有玩家准备了，index: ");
            DouniuStartGameRet ready = data as DouniuStartGameRet;
            ReadyRet(ready);
        }
        //if (msg == MessageFactoryImpi.instance.getMessageString(7, 11)) {
        //    com.guojin.mj.net.message.login.Notice notice = data as com.guojin.mj.net.message.login.Notice;

        //    Debug.Log("人数不足" + notice.reboot + "," + notice.key + "," + notice.type + "," + notice.args);
        //}
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 25)) {
            Debug.Log("有人加入---->");
            DouniuGameUserInfo resp = data as DouniuGameUserInfo;
            AddPlayer(resp);
        }
        //开始发牌
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 20)) {
            Debug.Log("发牌---->");
            DouniuChapterMsg resp = data as DouniuChapterMsg;
            StartCoroutine(WaitBegin(resp));
        }
        //回应5，12，通知所有人有人抢庄/下注，并且值是多少
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 13)) {
            Debug.Log("有人加注/抢庄");
            DouniuOperationRet operation = data as DouniuOperationRet;
            Debug.Log(operation);
            OperationRet(operation);
        }
        //通知客户端抢庄结果是什么
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 35)) {
            Debug.Log("抢庄成功");
            QiangZhuangRet qiang = data as QiangZhuangRet;
            SetZhuangInfo(qiang);
        }
        //通知客户端最后一张牌什么
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 11)) {
            DouniuFaPaiRet finalFaPai = data as DouniuFaPaiRet;
            NewFinallyPoker(finalFaPai);
            //List<DouniuOnePai> paiList = finalFaPai.OnePai;
            ////这个判断是为了确保最后一张牌有值，因为除了玩家自己的手牌，其他人的手牌都是空的
            //if (paiList[0].getPokerSuit() > 0) {
            //    FinallyPoker(paiList,finalFaPai.OnePai2);
            //}
        }
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 32)) {
            Debug.Log("同步时间");
        }
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 22)) {
            Debug.Log("比牌结果");
            DouniuGameChapterEnd chapterEnd = data as DouniuGameChapterEnd;
            //SetChapterEnd(chapterEnd);
            StartCoroutine(SetChapterEndCoroutine(chapterEnd));
        }
        //普通用户退出房间
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 7)) {
            ExitDouniuRoomResult exit = data as ExitDouniuRoomResult;
            IstZhuangExit(exit);

        }
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 42)) {
            DouniuDelRoomRet resp = data as DouniuDelRoomRet;
            VoteExit(resp);

        }
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 45)) {
            DouniuVoteRet resp = data as DouniuVoteRet;
            DelRoom(resp);

        }
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 23)) {
            //
            DouniuExitRoomRet resp = data as DouniuExitRoomRet;
            ExitRoom(resp);
        }
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 44)) {
            //有用户离线
            DouniuUserOffline resp = data as DouniuUserOffline;
            UserOffLine(resp);
        }
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 53)) {
            //抢手牌返回
            DouniuCasiPaiRet resp = data as DouniuCasiPaiRet;
            if (!DNGlobalData.paiCountDic.ContainsKey(resp.getIndex())) {
                DNGlobalData.paiCountDic.Add(resp.getIndex(), resp.getPaiLens());
            }
        }

        if (msg == MessageFactoryImpi.instance.getMessageString(5, 51)) {
            DouniuChatRet resp = data as DouniuChatRet;
            switch (resp.getIndex()) {
                case 1:
                    break;
                case 2:
                    SetEmoji(resp.getUserindex(), resp.getChatContent());
                    break;
                case 3:
                    SetShortCut(resp.getUserindex(), resp.getChatContent());
                    break;
                case 4:
                    voiceIndex = resp.getUserindex();
                    StartCoroutine(DownVoice(resp.getChatContent()));
                    break;
            }
        }
    }
    private void SetZhuangInfo(QiangZhuangRet qiang) {

        GameManager.instance.HideButton(UIButton.邀请);
        GameManager.instance.HideButton(UIButton.取消准备);

        if (DNGlobalData.mode == "1" || DNGlobalData.mode == "2") {
            if (changeIndex != -1) {
                manager.noticeBG.enabled = false;
                manager.noticeTextComp.text = "";
                if (positionArr == null) {
                    positionArr = manager.SetPlayerPosition(DNGlobalData.roomPlayerNumber, DNGlobalData.locationIndex);
                }
                DNGlobalData.changeUserName = manager.GetDictPlayerName(positionArr[qiang.LocationIndex]).text;
                if (DNGlobalData.currentUserName == DNGlobalData.changeUserName) {
                    manager.changeNoticeZhuang.SetActive(true);
                } else {
                    manager.changeNoticeOthers.SetActive(true);
                }
                StartCoroutine(WaitBegin1(qiang));
                changeIndex = -1;
            } else {
                manager.changeNoticeZhuang.SetActive(false);
                manager.changeNoticeOthers.SetActive(false);
                SetQiangZhuangPlayer(qiang);
            }
        }
    }
        bool planPlaying = false;
    bool changeImage=true;
    private void Update() {
        if (doneCount == DNGlobalData.roomPlayerNumber && changeImage) {
            if (positionArr==null) {
                positionArr = manager.SetPlayerPosition(DNGlobalData.roomPlayerNumber,DNGlobalData.locationIndex);
            }
            for (int i = 0; i < DNGlobalData.userImage.Count; i++) {
                manager.GetDictUserImage(i).sprite = DNGlobalData.userImage[i];
            }
            changeImage = false;
        }
    }
    IEnumerator WaitBegin(DouniuChapterMsg chapter) {
        if (DNGlobalData.lookPoker) {
            DOTweenDealPoker.SetNextList(4);
        } else {
            DOTweenDealPoker.SetNextList(5);
        }
        manager.noticeBG.enabled = false;
        manager.noticeTextComp.text = "";
        for (int i = 0; i < positionArr.Length; i++) {
            if (DNGlobalData.paiCountDic[i] == 2) {
                string type1 = (positionArr[i] + 1).ToString() + (DNGlobalData.paiCountDic[i] - 1).ToString();
                string type2 = (positionArr[i] + 1).ToString() + DNGlobalData.paiCountDic[i].ToString();
                string type = (positionArr[i] + 1).ToString() + DNGlobalData.paiCountDic[i].ToString();
                print(type1);
                print(type2);
                manager.BaseTypeDOTween(type);
                manager.BaseTypeDOTween(type1);
                manager.BaseTypeDOTween(type2);
            } else {
                string type = (positionArr[i] + 1).ToString() + (DNGlobalData.paiCountDic[i]+2).ToString();
                print(type);
                manager.BaseTypeDOTween(type);
            }
        }
        yield return new WaitForSeconds(0.8f);
        DOTweenDealPoker.EndDOTween();
        SetChpaterMessage(chapter);
    }
    
    IEnumerator WaitBegin1(QiangZhuangRet qiang) {

        yield return new WaitForSeconds(2f);
        SetQiangZhuangPlayer(qiang);
    }

    IEnumerator DownVoice(string path) {
        WWW www = new WWW(path);
        yield return www;
        if (www.isDone) {
            AudioClip clip = www.GetAudioClipCompressed(true, AudioType.OGGVORBIS);
            SetUserVoice(clip,voiceIndex);
        }
    }
    Texture2D tex2d;
    int doneCount;
    IEnumerator DownSprite(string path) {
        if (path == null) {
            int i = 0;
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
        WWW www = new WWW(path);
        yield return www;
        if (www.isDone) {
            tex2d = www.texture;
            Sprite sp = CreateSprite(tex2d, tex2d.width, tex2d.height);
            manager.GetDictUserImage(addPlayerIndex).sprite = sp;
        }
    }
    int index;
    IEnumerator DownSprites() {
        if (index < DNGlobalData.spritePathList.Count) {
            string path = DNGlobalData.spritePathList[index];
            if (path == null) {
                int i = DNGlobalData.locationIndex;
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
            } else {
            path = path.Substring(0, path.Length - 1);
            path += "132";
            }
            WWW www = new WWW(path);
            yield return www;
            if (www.isDone) {
                texture = www.texture;
                sp = CreateSprite(texture, texture.width, texture.height);
                manager.GetDictUserImage(index).sprite = sp;
                index++;
                StartCoroutine(DownSprites());
            }
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
    public static Sprite CreateSprite(Texture2D texture, int width, int height) {
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0, 0));
        return sp;
    }
    /// <summary>
    /// 5，24后设置
    /// </summary>
    /// <param name="gameRoominfo"></param>
    public void SetSceneMessage(DouniuGameRoomInfoRet gameRoominfo) {
        DNGlobalData.fangZhuUserID = gameRoominfo.CreateUserId;
        manager.chapterText.text = (gameRoominfo.LeftChapterNums) + "/" + gameRoominfo.ChapterMax + "局";
        DNGlobalData.currentChapter = gameRoominfo.LeftChapterNums;
        print(gameRoominfo.LeftChapterNums);
        DNGlobalData.maxChapter = gameRoominfo.ChapterMax;
        DNGlobalData.locationIndex = gameRoominfo.LocalIndex;
        DNGlobalData.changeMode = gameRoominfo.FangShi;
        switch (DNGlobalData.changeMode) {
            case "1":
                DNGlobalData.changeNiuTypeStr = "牛九";
                break;
            case "2":
                DNGlobalData.changeNiuTypeStr = "牛牛";
                break;
        }
        List<DouniuGameUserInfo> sceneUser = gameRoominfo.SceneUser;
        DNGlobalData.roomPlayerNumber = sceneUser.Count;
        positionArr = manager.SetPlayerPosition(DNGlobalData.roomPlayerNumber, DNGlobalData.locationIndex);
        print("数量："+positionArr.Length);
        DNGlobalData.mode = gameRoominfo.MoShi;
        if (gameRoominfo.MoShi == "1") {
            mode.text = "房主坐庄";
        } else if (gameRoominfo.MoShi == "2") {
            mode.text = "随机庄";
        } else {
            mode.text = "抢庄";
        }
        SetUserInfo(sceneUser);
        DNGlobalData.userIDList.Clear();
        DNGlobalData.userIDList.Add(gameRoominfo.CreateUserId);
        //foreach (var a in sceneUser) {
            
        //}
        if (!DNGlobalData.confirmChongLian) {
            switch (gameRoominfo.RoomState) {
                case "5":
                    manager.ShowButton(UIButton.继续);
                    break;
                default:
                    if (DNGlobalData.currentChapter > 1) {
                        manager.noticeBG.enabled = true;
                        manager.noticeTextComp.text = "对局正在进行，请稍等...";
                    }
                    break;
            }
        } else {
            Debug.Log("重连庄的位置：" + gameRoominfo.Chapter.getZhuangIndex());
            if (gameRoominfo.IsCheck == "1") {
                DNGlobalData.lookPoker = true;
            } else {
                DNGlobalData.lookPoker = false;
            }
            DNGlobalData.roomState = gameRoominfo.RoomState;
            manager.HideAllButton();
            manager.ShowButton(UIButton.聊天);
            manager.ShowButton(UIButton.设置);
            manager.ShowButton(UIButton.语音);
            print("房间状态:"+gameRoominfo.RoomState);
            QiangZhuangRet qiangRet = new QiangZhuangRet();
            switch (gameRoominfo.RoomState) {
                case "0":
                    break;
                case "1":
                    //等待选手牌，模拟收到抢庄成功消息；
                    qiangRet.LookPoker = DNGlobalData.lookPoker;
                    qiangRet.LocationIndex = gameRoominfo.Chapter.getZhuangIndex();
                    SetQiangZhuangPlayer(qiangRet);
                    if (isOperation) {
                        isOperation = false;
                        //StopAllCoroutines();
                        manager.UserIsOperation();
                        manager.noticeBG.enabled = true;
                        manager.noticeTextComp.text = "正在等待其他玩家选择手牌数量";
                    }
                    //if (DNGlobalData.lookPoker) {
                    //    //SetQiangZhuangPlayer(ret2);
                    //    SetChpaterMessage(gameRoominfo.Chapter);
                    //} else {
                    //    manager.noticeBG.enabled = true;
                    //    manager.noticeTextComp.text = "请下注";
                    //    manager.ShowAllJiaZhuButton();
                    //}
                    break;
                case "2":
                    //等待加注，模拟收到发牌消息；
                    qiangRet.LookPoker = DNGlobalData.lookPoker;
                    qiangRet.LocationIndex = gameRoominfo.Chapter.getZhuangIndex();
                    SetQiangZhuangPlayer(qiangRet);
                    if (DNGlobalData.lookPoker) {
                        SetChpaterMessage(gameRoominfo.Chapter);
                        manager.StopCountDown();
                       
                    } else {
                        if (DNGlobalData.locationIndex == gameRoominfo.Chapter.getZhuangIndex()) {
                            manager.noticeTextComp.text = "等待闲家下注...";
                            manager.noticeBG.enabled = true;
                        } else {
                            manager.ShowAllJiaZhuButton();
                            manager.noticeTextComp.text = "请下注...";
                            manager.noticeBG.enabled = true;
                        }
                    }
                    manager.HideAllRobPokerButton();
                    if (isOperation) {
                        isOperation = false;
                        manager.UserIsOperation();
                        manager.noticeBG.enabled = true;
                        manager.noticeTextComp.text = "正在等待其他玩家押注";
                    }
                    //StopAllCoroutines();

                    break;
                case "3":
                    //等待最后一张牌，模拟收到发牌消息；（多余）
                    qiangRet.LookPoker = DNGlobalData.lookPoker;
                    qiangRet.LocationIndex = gameRoominfo.Chapter.getZhuangIndex();
                    SetQiangZhuangPlayer(qiangRet);
                    SetChpaterMessage(gameRoominfo.Chapter);
                    manager.HideAllJiaZhuButton();
                    manager.HideAllRobPokerButton();
                    if (isOperation) {
                        isOperation = false;
                        manager.UserIsOperation();
                        manager.noticeBG.enabled = true;
                        manager.noticeTextComp.text = "正在等待其他玩家押注";
                    }
                    break;
                case "4":
                    //等待亮牌，模拟收到最后一张牌或者发牌消息
                    DNGlobalData.waitEnd = true;
                    qiangRet.LookPoker = DNGlobalData.lookPoker;
                    qiangRet.LocationIndex = gameRoominfo.Chapter.getZhuangIndex();
                    SetQiangZhuangPlayer(qiangRet);
                    SetChpaterMessage(gameRoominfo.Chapter);
                    for (int i = 0; i < manager.handPoker.Count; i++) {
                        if (manager.handPoker[i].GetComponent<Poker>() == null) {
                            manager.handPoker[i].gameObject.AddComponent<Poker>();
                        }
                        //else {
                        //    Destroy(manager.handPoker[i].GetComponent<Poker>());
                        //    manager.handPoker[i].gameObject.AddComponent<Poker>();
                        //}
                    }
                    for (int i = 0; i < manager.handPoker2.Count; i++) {
                        if (manager.handPoker2[i].GetComponent<Poker>() == null) {
                            manager.handPoker2[i].gameObject.AddComponent<Poker>();
                        }
                    }
                    DNGlobalData.countDownOpt = "End";
                    manager.calTexts.gameObject.SetActive(true);
                    //StopAllCoroutines();
                    manager.HideAllJiaZhuButton();
                    manager.HideAllRobPokerButton();

                    manager.noticeTextComp.text = "";
                    manager.noticeBG.enabled = false;
                    manager.ShowButton(UIButton.灰亮牌);
                    manager.ShowButton(UIButton.提示);
                    DNGlobalData.roomState = "0";
                    if (isOperation) {
                        isOperation = false;
                        manager.UserIsOperation();
                        manager.noticeBG.enabled = true;
                        manager.noticeTextComp.text = "正在等待其他玩家亮牌";
                    }
                    break;
                case "5":
                    //已经亮牌，模拟收到结果消息
                    print(gameRoominfo.ChapterEnd.getCompareResultList().Count);
                    Transform tran2 = manager.GetDictHandPoint1(0);
                    tran2.localPosition = new Vector3(tran2.localPosition.x, -243 + 40, 0);
                    tran2 = manager.GetDictHandPoint2(0);
                    tran2.localPosition = new Vector3(tran2.localPosition.x, -243 + 40, 0);
                    tran2 = manager.GetDictHandPoint3(0);
                    tran2.localPosition = new Vector3(tran2.localPosition.x, -243 + 40, 0);
                    manager.GetDictNiuType1(0).transform.DOLocalMoveY(-230, 0);
                    manager.GetDictNiuType2(0).transform.DOLocalMoveY(-230, 0);
                    manager.GetDictNiuType3(0).transform.DOLocalMoveY(-230, 0);
                    manager.GetDictWinText1(0).transform.DOLocalMoveY(-230, 0);
                    manager.GetDictWinText2(0).transform.DOLocalMoveY(-230, 0);
                    manager.GetDictWinText3(0).transform.DOLocalMoveY(-230, 0);
                    manager.HideAllJiaZhuButton();
                    manager.HideAllRobPokerButton();
                    if (isOperation) {
                        isOperation = false;
                        manager.UserIsOperation();
                        manager.noticeBG.enabled = true;
                        manager.noticeTextComp.text = "正在等待其他玩家继续游戏";
                    } else {
                        // manager.ShowButton(UIButton.继续);
                        manager.GetDictZhuang(gameRoominfo.Chapter.getZhuangIndex()).SetActive(true);
                        StartCoroutine(SetChapterEndCoroutine(gameRoominfo.ChapterEnd));
                    }
                    break;
            }
            DNGlobalData.confirmChongLian = false;
        }
    }
    IEnumerator  CallChapterEnd(DouniuGameChapterEnd chapterEnd) {
        yield return new WaitForSeconds(1);
        StartCoroutine(SetChapterEndCoroutine(chapterEnd));
    }
    //public void 

    private void SetUserInfo(List<DouniuGameUserInfo> userInfoList) {
        for (int i = 0; i < DNGlobalData.roomPlayerNumber; i++) {
            int realPosition = positionArr[userInfoList[i].getLocationIndex()];
            localPosition.Add(manager.GetDictPlayer(i).transform.localPosition);
        }
        for (int i = 0; i < userInfoList.Count; i++) {
            DNGlobalData.winCountList.Add("0");
            DNGlobalData.loseCountList.Add("0");
            DNGlobalData.equalCountList.Add("0");
            DNGlobalData.userChapterList.Add("0");
            DNGlobalData.userScoreList.Add("0");

            int realPosition = positionArr[userInfoList[i].getLocationIndex()];

            DNGlobalData.userNameList.Add(userInfoList[i].getUserName());
            manager.GetDictPlayer(realPosition).SetActive(true);
            DNGlobalData.userIDList.Add(userInfoList[i].getUserId());
            manager.GetDictPlayerName(realPosition).text = userInfoList[i].getUserName();
            manager.GetDictScoreName(realPosition).text = userInfoList[i].getScore().ToString();
            manager.GetDictPlayer(i).transform.localPosition = localPosition[realPosition];
            if (!userInfoList[i].getOnline()) {
                manager.GetDictOffLine(i).enabled = true;
            }

            if (realPosition == 0) {
                manager.GetDictPlayer(i).transform.FindChild("headMask").gameObject.SetActive(false);
                DNGlobalData.currentUserID = userInfoList[i].getUserId();
            }
            if (DNGlobalData.confirmChongLian) {
                if (userInfoList[i].HandPokerCount != 0) {
                    if (!DNGlobalData.paiCountDic.ContainsKey(userInfoList[i].getLocationIndex())) {
                        DNGlobalData.paiCountDic.Add(userInfoList[i].getLocationIndex(), userInfoList[i].HandPokerCount);
                    }
                    //else {
                    //    DNGlobalData.paiCountDic[userInfoList[i].getLocationIndex()] = userInfoList[i].HandPokerCount;
                    //}
                }
            }

            string path = userInfoList[i].getAvatar();

            DNGlobalData.spritePathList.Add(path);

            if (userInfoList[i].getLocationIndex() == DNGlobalData.locationIndex) {
                isOperation = userInfoList[i].IsOperation;
            }
        }
        StartCoroutine(DownSprites());
    }
    /// <summary>
    /// 发牌操作
    /// </summary>
    /// <param name="chapterMsg"></param>
    public void SetChpaterMessage(DouniuChapterMsg chapterMsg) {
        manager.ClearHandPoker();
        manager.HideAllRobPokerButton();
        manager.noticeBG.enabled = false;
        manager.noticeTextComp.text = "";
        DNGlobalData.currentChapter = chapterMsg.getChapterNums();
        for (int i = 0; i < 5; i++) {
            manager.GetDictReady(i).enabled = false;
        }
        List<DouniuUserPlaceMsg> chapterList = chapterMsg.getUserPlace();
        int count = chapterList[0].ShouPai.Count;
        int num4 = chapterList.Count;
        List<DouniuOnePai> paiList = new List<DouniuOnePai>();
        paiList = chapterList[0].ShouPai;
        DouniuOnePai pai = new DouniuOnePai();
        pai.setPokerSuit(-1);

        manager.final.Clear();
        manager.final2.Clear();

        for (int i = 0; i < chapterList.Count; i++) {
            int realPosition = positionArr[chapterList[i].LocationIndex];
            if (realPosition == 0) {
                if (manager.GetDictPaiCount(chapterList[i].LocationIndex) == 1) {

                    for (int j = 0; j < paiList.Count; j++) {
                        Transform poker = DealRealPoker(paiList[j], manager.GetDictHandPoint3(0), 1.5f);
                        manager.handPoker.Add(poker);
                    }
                    if (DNGlobalData.lookPoker && DNGlobalData.roomState != "4") {
                        Transform poker = DealVirtualPoker(manager.GetDictHandPoint3(0), 1.5f);
                        manager.final.Add(poker);
                    }

                } else if (manager.GetDictPaiCount(chapterList[i].LocationIndex) == 2) {

                    for (int j = 0; j < paiList.Count; j++) {
                        Transform poker = DealRealPoker(paiList[j], manager.GetDictHandPoint1(0), 1.5f);
                        manager.handPoker.Add(poker);
                    }
                    if (DNGlobalData.lookPoker && DNGlobalData.roomState != "4") {
                        Transform poker = DealVirtualPoker(manager.GetDictHandPoint1(0), 1.5f);
                        manager.final.Add(poker);
                    }

                    List<DouniuOnePai> paiList1 = chapterList[0].ShouPai2;
                    for (int j = 0; j < paiList1.Count; j++) {
                        Transform poker = DealRealPoker(paiList1[j], manager.GetDictHandPoint2(0), 1.5f);
                        manager.handPoker2.Add(poker);
                    }
                    if (DNGlobalData.lookPoker && DNGlobalData.roomState != "4") {
                        Transform poker = DealVirtualPoker(manager.GetDictHandPoint2(0), 1.5f);
                        manager.final2.Add(poker);
                    }

                }
            } else {
                if (manager.GetDictPaiCount(chapterList[i].LocationIndex) == 1) {
                    for (int k = 0; k < count; k++) {
                        DealVirtualPoker(manager.GetDictHandPoint3(realPosition), 1.3f);
                    }
                } else if (manager.GetDictPaiCount(chapterList[i].LocationIndex) == 2) {
                    for (int k = 0; k < count; k++) {
                        DealVirtualPoker(manager.GetDictHandPoint1(realPosition), 1);
                    }
                    for (int k = 0; k < count; k++) {
                        DealVirtualPoker(manager.GetDictHandPoint2(realPosition), 1);
                    }
                    //}
                }

            }
        }
        manager.DealDiffentMode();
        if (DNGlobalData.confirmChongLian) {
            manager.GetDictZhuang(chapterMsg.getZhuangIndex()).SetActive(true);

        } 
    }
    public Transform DealRealPoker(DouniuOnePai onePai, Transform parent ,float scale = 1,bool isBack=true) {

        Transform poker = manager.GetPoker(onePai);
        poker.SetParent(parent);
        poker.GetChild(0).GetComponent<Image>().enabled = isBack;
        poker.localScale = new Vector3(scale, scale, scale);
        if (poker.GetComponent<Poker>() != null) {
            Destroy(poker.GetComponent<Poker>());
        }
        return poker;
    }

    public Transform DealVirtualPoker(Transform parent, float scale = 1) {
        DouniuOnePai pai = new DouniuOnePai();
        pai.setPokerSuit(-1);

        Transform poker = GameObject.Instantiate<Transform>(manager.GetPoker(pai));
        poker.SetParent(parent);
        poker.localScale = new Vector3(scale, scale, scale);
        poker.name= "PokerModel";
        if (poker.GetComponent<Poker>() != null) {
            GameObject.Destroy(poker.GetComponent<Poker>());
        }
        DNGlobalData.tempBackPokerList.Add(poker.gameObject);
        return poker;
    }
    public void NewFinallyPoker(DouniuFaPaiRet finalDeal) {
        int reverseIndex = positionArr[finalDeal.Index];
        //try {
        //    foreach (var a in finalDeal.OnePai) {
        //        Debug.Log(a.toString());
        //    }
        //    foreach (var a in finalDeal.OnePai2) {
        //        Debug.Log(a.toString());
        //    }
        //} catch (Exception ex) {

        //}
        if (finalDeal.OnePai[0].getPokerSuit() < 0) {
            if (manager.GetDictPaiCount(finalDeal.Index) == 1) {

                DealVirtualPoker(manager.GetDictHandPoint3(reverseIndex), 1.3f);

            } else if (manager.GetDictPaiCount(finalDeal.Index) == 2) {
                DealVirtualPoker(manager.GetDictHandPoint1(reverseIndex));
                DealVirtualPoker(manager.GetDictHandPoint2(reverseIndex));
            }
        } else {
            manager.ShowButton(UIButton.灰亮牌);
            manager.ShowButton(UIButton.提示);
            manager.noticeBG.enabled = false;
            manager.noticeTextComp.text = "";
            DNGlobalData.waitEnd = true;
            manager.calTexts.gameObject.SetActive(true);

            foreach (var a in manager.final) {
                Destroy(a.gameObject);
            }
            foreach (var a in manager.final2) {
                Destroy(a.gameObject);
            }
            manager.final.Clear();
            manager.final2.Clear();

            if (manager.GetDictPaiCount(finalDeal.Index) == 1) {

                Transform poker = DealRealPoker(finalDeal.OnePai[0], manager.GetDictHandPoint3(0), 1.5f);
                manager.final.Add(poker);
                manager.DOTweenRotateShow(manager.final);
                manager.handPoker.Add(poker);

            } else if (manager.GetDictPaiCount(finalDeal.Index) == 2) {

                Transform poker = DealRealPoker(finalDeal.OnePai[0], manager.GetDictHandPoint1(0), 1.5f);
                manager.final.Add(poker);
                manager.DOTweenRotateShow(manager.final);
                manager.handPoker.Add(poker);

                Transform poker2 = DealRealPoker(finalDeal.OnePai2[0], manager.GetDictHandPoint2(0), 1.5f);
                manager.final2.Add(poker2);
                manager.DOTweenRotateShow(manager.final2);
                manager.handPoker2.Add(poker2);
            }
        }

        foreach (var a in manager.handPoker) {
            if (a.GetComponent<Poker>() == null) {
                a.gameObject.AddComponent<Poker>();
            }
            //Debug.Log("第一手牌："+a);
        }
        foreach (var a in manager.handPoker2) {
            if (a.GetComponent<Poker>() == null) {
                a.gameObject.AddComponent<Poker>();
            }
            //Debug.Log("第二手牌：" + a);
        }

    }


    /// <summary>
    /// 最后一张牌
    /// </summary>
    /// <param name="finalPoker"></param>
    /// <param name="index"></param>
    public void FinallyPoker(List<DouniuOnePai> finalPoker, List<DouniuOnePai> finalPoker2) {
        manager.ShowButton(UIButton.灰亮牌);
        manager.ShowButton(UIButton.提示);
        //manager.StartCountDown("End", 15);
        manager.noticeBG.enabled = false;
        manager.noticeTextComp.text = "";
        DNGlobalData.waitEnd = true;
        manager.calTexts.gameObject.SetActive(true);
        for (int i = 0; i < positionArr.Length; i++) {
            if (DNGlobalData.isStart) {
                if (i == positionArr.Length - 1) {
                    break;
                }
            }
            int reverseIndex = positionArr[i];
            List<Transform> final = new List<Transform>();
            List<Transform> final2 = new List<Transform>();
            if (positionArr[i] == 0) {

                if (manager.GetDictPaiCount(i) == 1) {

                    Transform poker = DealRealPoker(finalPoker[0], manager.GetDictHandPoint3(0), 1.5f);
                    Destroy(manager.handPoker[manager.handPoker.Count - 1].gameObject);
                    manager.handPoker.RemoveAt(manager.handPoker.Count - 1);
                    manager.handPoker.Add(poker);
                    final.Add(poker);
                    manager.DOTweenRotateShow(final);

                } else if (manager.GetDictPaiCount(i) == 2) {

                    Transform poker = DealRealPoker(finalPoker[0], manager.GetDictHandPoint1(0), 1.5f);
                    Destroy(manager.handPoker[manager.handPoker.Count - 1].gameObject);
                    manager.handPoker.RemoveAt(manager.handPoker.Count - 1);
                    manager.handPoker.Add(poker);
                    final.Add(poker);
                    manager.DOTweenRotateShow(final);

                    Transform poker2 = DealRealPoker(finalPoker2[0], manager.GetDictHandPoint2(0), 1.5f);
                    Destroy(manager.handPoker2[manager.handPoker2.Count - 1].gameObject);
                    manager.handPoker2.RemoveAt(manager.handPoker2.Count - 1);
                    manager.handPoker2.Add(poker2);
                    final2.Add(poker2);
                    manager.DOTweenRotateShow(final2);
                }

            } else {
                if (manager.GetDictPaiCount(i) == 1) {

                    DealVirtualPoker(manager.GetDictHandPoint3(reverseIndex), 1.3f);

                } else if (manager.GetDictPaiCount(i) == 2) {
                    DealVirtualPoker(manager.GetDictHandPoint1(reverseIndex));
                    DealVirtualPoker(manager.GetDictHandPoint2(reverseIndex));
                }

            }
        }
    }

    /// <summary>
    /// 比牌结果
    /// </summary>
    /// <param name="chapterEnd"></param>
    public void SetChapterEnd(DouniuGameChapterEnd chapterEnd) {
        manager.noticeBG.enabled = false;
        manager.noticeTextComp.text = "";
        DNGlobalData.userScoreList.Clear();
        DNGlobalData.userNameList.Clear();
        DNGlobalData.userChapterList.Clear();
        DNGlobalData.winCountList.Clear();
        DNGlobalData.loseCountList.Clear();
        DNGlobalData.equalCountList.Clear();

        manager.fillList.Clear();

        manager.HideButton(UIButton.灰亮牌);
        manager.HideButton(UIButton.提示);
        manager.HideButton(UIButton.亮牌);
        manager.HideButton(UIButton.灰提示);
        manager.calTexts.gameObject.SetActive(false);
        manager.GetDictDone2(positionArr[DNGlobalData.locationIndex]).enabled = false;
        DNGlobalData.waitEnd = false;
        List<DouniuCompareResult> list = chapterEnd.getCompareResultList();
        manager.ClearHandPoker();
        DouniuCompareResult result;
        int winNumber = 0;
        string str;
        manager.handPoker.Clear();
        manager.handPoker2.Clear();
        for (int i = 0; i < list.Count; i++) {
            int realPosition = positionArr[list[i].LocationIndex];
            try {
                result = list[i];
                NiuType paiTypeDict = DouniuPaiType.Instance.GetPaiTypeDict(list[i].PaiType);
                switch (DNGlobalData.changeMode) {
                    case "1":
                        if (paiTypeDict == NiuType.牛9) {
                            changeIndex = i;
                        }
                        break;
                    case "2":
                        if (paiTypeDict == NiuType.牛牛) {
                            changeIndex = i;
                        }
                        break;
                    default:
                        changeIndex = -1;
                        break;
                }
                manager.GetDictDone(i).enabled = false;
                if (realPosition == 0) {
                    List<DouniuOnePai> paiList = result.PaiList;
                    if (manager.GetDictPaiCount(list[i].LocationIndex) == 1) {

                        for (int j = 0; j < paiList.Count; j++) {
                            Transform poker = manager.GetPoker(paiList[j]);
                            poker.SetParent(manager.GetDictHandPoint3(realPosition));
                            poker.GetChild(0).GetComponent<Image>().enabled = false;
                            poker.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                            if (poker.GetComponent<Poker>() != null) {
                                GameObject.Destroy(poker.GetComponent<Poker>());
                            }
                            manager.handPoker.Add(poker);
                        }
                        manager.GetDictNiuType3(realPosition).SetActive(true);
                        manager.GetDictNiuType3(realPosition).transform.GetChild(0).GetComponent<Image>().sprite =
                        DynamicUIManager.Instance.BaseNiuTypeGetSprite(paiTypeDict);
                        manager.PlayTransformAudio("f0_nn" + result.PaiType);
                        str = list[i].WinCountNum;
                        int.TryParse(str, out winNumber);
                        manager.GetDictWinText3(realPosition).text = manager.GetNumberSprite(winNumber);

                    } else if (manager.GetDictPaiCount(list[i].LocationIndex) == 2) {
                       // Screen.orientation
                        for (int j = 0; j < paiList.Count; j++) {
                            Transform poker = manager.GetPoker(paiList[j]);
                            poker.SetParent(manager.GetDictHandPoint1(realPosition));
                            poker.GetChild(0).GetComponent<Image>().enabled = false;
                            poker.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                            if (poker.GetComponent<Poker>() != null) {
                                GameObject.Destroy(poker.GetComponent<Poker>());
                            }
                            manager.handPoker.Add(poker);
                        }
                        manager.GetDictNiuType1(realPosition).SetActive(true);
                        manager.GetDictNiuType1(realPosition).transform.GetChild(0).GetComponent<Image>().sprite =
                            DynamicUIManager.Instance.BaseNiuTypeGetSprite(paiTypeDict);
                        manager.PlayTransformAudio("f0_nn" + result.PaiType);
                        str = list[i].WinCountNum;
                        int.TryParse(str, out winNumber);
                        manager.GetDictWinText1(realPosition).text = manager.GetNumberSprite(winNumber);
                        NiuType paiTypeDict1 = DouniuPaiType.Instance.GetPaiTypeDict(list[i].PaiType2);
                        List<DouniuOnePai> paiList1 = list[i].PaiList2;
                        for (int j = 0; j < paiList1.Count; j++) {
                            Transform poker = manager.GetPoker(paiList1[j]);
                            poker.SetParent(manager.GetDictHandPoint2(realPosition));
                            poker.GetChild(0).GetComponent<Image>().enabled = false;
                            poker.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                            if (poker.GetComponent<Poker>() != null) {
                                GameObject.Destroy(poker.GetComponent<Poker>());
                            }
                            manager.handPoker2.Add(poker);
                        }
                        manager.GetDictNiuType2(realPosition).SetActive(true);
                        manager.GetDictNiuType2(realPosition).transform.GetChild(0).GetComponent<Image>().sprite =
                            DynamicUIManager.Instance.BaseNiuTypeGetSprite(paiTypeDict1);
                        manager.PlayTransformAudio("f0_nn" + result.PaiType2);
                        str = list[i].WinCountNum2;
                        int.TryParse(str, out winNumber);
                        manager.GetDictWinText2(realPosition).text = manager.GetNumberSprite(winNumber);
                    }
                } else {
                    List<DouniuOnePai> paiList = result.PaiList;
                    if (manager.GetDictPaiCount(list[i].LocationIndex) == 1) {

                        for (int m = 0; m < paiList.Count; m++) {
                            Transform transform2 = manager.GetPoker(paiList[m]);
                            transform2.SetParent(manager.GetDictHandPoint3(realPosition));
                            transform2.GetChild(0).GetComponent<Image>().enabled = false;
                            transform2.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                            if (transform2.GetComponent<Poker>() != null) {
                                GameObject.Destroy(transform2.GetComponent<Poker>());
                            }
                        }
                        manager.GetDictNiuType3(realPosition).SetActive(true);
                        manager.GetDictNiuType3(realPosition).transform.GetChild(0).GetComponent<Image>().sprite =
                            DynamicUIManager.Instance.BaseNiuTypeGetSprite(paiTypeDict);
                        str = list[i].WinCountNum;
                        int.TryParse(str, out winNumber);
                        manager.GetDictWinText3(realPosition).text = manager.GetNumberSprite(winNumber);

                    } else if (manager.GetDictPaiCount(list[i].LocationIndex) == 2) {

                        for (int m = 0; m < paiList.Count; m++) {
                            Transform transform2 = manager.GetPoker(paiList[m]);
                            transform2.SetParent(manager.GetDictHandPoint1(realPosition));
                            transform2.GetChild(0).GetComponent<Image>().enabled = false;
                            transform2.localScale = new Vector3(1, 1, 1);
                            if (transform2.GetComponent<Poker>() != null) {
                                GameObject.Destroy(transform2.GetComponent<Poker>());
                            }
                        }
                        manager.GetDictNiuType1(realPosition).SetActive(true);
                        manager.GetDictNiuType1(realPosition).transform.GetChild(0).GetComponent<Image>().sprite =
                            DynamicUIManager.Instance.BaseNiuTypeGetSprite(paiTypeDict);
                        str = list[i].WinCountNum;
                        int.TryParse(str, out winNumber);
                        manager.GetDictWinText1(realPosition).text = manager.GetNumberSprite(winNumber);

                        NiuType paiTypeDict1 = DouniuPaiType.Instance.GetPaiTypeDict(list[i].PaiType2);
                        List<DouniuOnePai> paiList1 = list[i].PaiList2;
                        for (int m = 0; m < paiList1.Count; m++) {
                            Transform transform2 = manager.GetPoker(paiList1[m]);
                            transform2.SetParent(manager.GetDictHandPoint2(realPosition));
                            transform2.GetChild(0).GetComponent<Image>().enabled = false;
                            transform2.localScale = new Vector3(1, 1, 1);
                            if (transform2.GetComponent<Poker>() != null) {
                                GameObject.Destroy(transform2.GetComponent<Poker>());
                            }
                        }
                        manager.GetDictNiuType2(realPosition).SetActive(true);
                        manager.GetDictNiuType2(realPosition).transform.GetChild(0).GetComponent<Image>().sprite =
                            DynamicUIManager.Instance.BaseNiuTypeGetSprite(paiTypeDict1);
                        str = list[i].WinCountNum2;
                        int.TryParse(str, out winNumber);
                        manager.GetDictWinText2(realPosition).text = manager.GetNumberSprite(winNumber);
                    }
                }
                manager.GetDictScoreName(realPosition).text = result.Scores.ToString();
                DNGlobalData.userNameList.Add(list[i].PlayName);
                DNGlobalData.userScoreList.Add(list[i].Scores);
                DNGlobalData.userChapterList.Add(list[i].PlaysNum);
                DNGlobalData.winCountList.Add(list[i].WinNum);
                DNGlobalData.loseCountList.Add(list[i].LoseNum);
                DNGlobalData.equalCountList.Add(list[i].EqualCount);
            } catch (Exception ex) {
                Debug.Log(ex.Message);
            }

        }
        manager.handPoker.Clear();
        manager.handPoker2.Clear();
        if (DNGlobalData.currentChapter >= DNGlobalData.maxChapter) {
            manager.noticeBG.enabled = true;
            manager.noticeTextComp.text = "对局已经结束，正在整理牌局...";
            manager.StartCountDown("ChapterEnd", 5);
        } else {
            manager.ShowButton(UIButton.继续);
        }
    }
    IEnumerator SetChapterEndCoroutine(DouniuGameChapterEnd chapterEnd) {

        manager.noticeBG.enabled = false;
        manager.noticeTextComp.text = "";

        manager.fillList.Clear();

        manager.HideButton(UIButton.灰亮牌);
        manager.HideButton(UIButton.提示);
        manager.HideButton(UIButton.亮牌);
        manager.HideButton(UIButton.灰提示);
        manager.calTexts.gameObject.SetActive(false);
        manager.GetDictDone2(positionArr[DNGlobalData.locationIndex]).enabled = false;
        DNGlobalData.waitEnd = false;
        List<DouniuCompareResult> list = chapterEnd.getCompareResultList();
        DouniuCompareResult result;
        manager.handPoker.Clear();
        manager.handPoker2.Clear();
        for (int i = 0; i < list.Count; i++) {
            int realPosition = positionArr[list[i].LocationIndex];
            result = list[i];
            NiuType paiTypeDict = DouniuPaiType.Instance.GetPaiTypeDict(list[i].PaiType);
            switch (DNGlobalData.changeMode) {
                case "1":
                    if (paiTypeDict == NiuType.牛9) {
                        changeIndex = i;
                    }
                    break;
                case "2":
                    if (paiTypeDict == NiuType.牛牛) {
                        changeIndex = i;
                    }
                    break;
                default:
                    changeIndex = -1;
                    break;
            }
            manager.GetDictDone(i).enabled = false;
            if (realPosition == 0) {
                List<DouniuOnePai> paiList = result.PaiList;
                if (manager.GetDictPaiCount(list[i].LocationIndex) == 1) {
                    manager.ClearHandPoker(0, 3);
                    for (int j = 0; j < paiList.Count; j++) {
                        Transform poker = manager.GetPoker(paiList[j]);
                        poker.SetParent(manager.GetDictHandPoint3(realPosition));
                        poker.GetChild(0).GetComponent<Image>().enabled = false;
                        poker.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                        if (poker.GetComponent<Poker>() != null) {
                            GameObject.Destroy(poker.GetComponent<Poker>());
                        }
                        manager.handPoker.Add(poker);
                    }
                    manager.GetDictNiuType3(realPosition).SetActive(true);
                    manager.GetDictNiuType3(realPosition).transform.GetChild(0).GetComponent<Image>().sprite =
                    DynamicUIManager.Instance.BaseNiuTypeGetSprite(paiTypeDict);
                    manager.PlayTransformAudio("f0_nn" + result.PaiType);

                } else if (manager.GetDictPaiCount(list[i].LocationIndex) == 2) {

                    try {
                        manager.ClearHandPoker(0, 1);
                        for (int j = 0; j < paiList.Count; j++) {
                            Transform poker = manager.GetPoker(paiList[j]);
                            poker.SetParent(manager.GetDictHandPoint1(realPosition));
                            poker.GetChild(0).GetComponent<Image>().enabled = false;
                            poker.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                            if (poker.GetComponent<Poker>() != null) {
                                GameObject.Destroy(poker.GetComponent<Poker>());
                            }
                            manager.handPoker.Add(poker);
                        }

                        manager.GetDictNiuType1(realPosition).SetActive(true);
                        manager.GetDictNiuType1(realPosition).transform.GetChild(0).GetComponent<Image>().sprite =
                            DynamicUIManager.Instance.BaseNiuTypeGetSprite(paiTypeDict);
                        manager.PlayTransformAudio("f0_nn" + result.PaiType);
                    } catch (Exception ex) {
                        Debug.Log(ex.Message);
                    }
                    if (!DNGlobalData.isChongLian) {
                        yield return new WaitForSeconds(1);
                    }
                    try {
                        manager.ClearHandPoker(0, 2);
                        NiuType paiTypeDict1 = DouniuPaiType.Instance.GetPaiTypeDict(list[i].PaiType2);
                        List<DouniuOnePai> paiList1 = list[i].PaiList2;
                        for (int j = 0; j < paiList1.Count; j++) {
                            Transform poker = manager.GetPoker(paiList1[j]);
                            poker.SetParent(manager.GetDictHandPoint2(realPosition));
                            poker.GetChild(0).GetComponent<Image>().enabled = false;
                            poker.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                            if (poker.GetComponent<Poker>() != null) {
                                GameObject.Destroy(poker.GetComponent<Poker>());
                            }
                            manager.handPoker2.Add(poker);
                        }
                        manager.GetDictNiuType2(realPosition).SetActive(true);
                        manager.GetDictNiuType2(realPosition).transform.GetChild(0).GetComponent<Image>().sprite =
                            DynamicUIManager.Instance.BaseNiuTypeGetSprite(paiTypeDict1);
                        manager.PlayTransformAudio("f0_nn" + result.PaiType2);    
                    } catch (Exception ex) {
                        Debug.Log(ex.Message);
                    }
                }
            } else {
                try {
                    List<DouniuOnePai> paiList = result.PaiList;
                    if (manager.GetDictPaiCount(list[i].LocationIndex) == 1) {
                        manager.ClearHandPoker(realPosition, 3);
                        for (int m = 0; m < paiList.Count; m++) {
                            Transform transform2 = manager.GetPoker(paiList[m]);
                            transform2.SetParent(manager.GetDictHandPoint3(realPosition));
                            transform2.GetChild(0).GetComponent<Image>().enabled = false;
                            transform2.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                            if (transform2.GetComponent<Poker>() != null) {
                                GameObject.Destroy(transform2.GetComponent<Poker>());
                            }
                        }
                        manager.GetDictNiuType3(realPosition).SetActive(true);
                        manager.GetDictNiuType3(realPosition).transform.GetChild(0).GetComponent<Image>().sprite =
                            DynamicUIManager.Instance.BaseNiuTypeGetSprite(paiTypeDict);

                    } else if (manager.GetDictPaiCount(list[i].LocationIndex) == 2) {

                        manager.ClearHandPoker(realPosition,1);
                        for (int m = 0; m < paiList.Count; m++) {
                            Transform transform2 = manager.GetPoker(paiList[m]);
                            transform2.SetParent(manager.GetDictHandPoint1(realPosition));
                            transform2.GetChild(0).GetComponent<Image>().enabled = false;
                            transform2.localScale = new Vector3(1, 1, 1);
                            if (transform2.GetComponent<Poker>() != null) {
                                GameObject.Destroy(transform2.GetComponent<Poker>());
                            }
                        }
                        manager.GetDictNiuType1(realPosition).SetActive(true);
                        manager.GetDictNiuType1(realPosition).transform.GetChild(0).GetComponent<Image>().sprite =
                            DynamicUIManager.Instance.BaseNiuTypeGetSprite(paiTypeDict);

                        manager.ClearHandPoker(realPosition, 2);
                        NiuType paiTypeDict1 = DouniuPaiType.Instance.GetPaiTypeDict(list[i].PaiType2);
                        List<DouniuOnePai> paiList1 = list[i].PaiList2;
                        for (int m = 0; m < paiList1.Count; m++) {
                            Transform transform2 = manager.GetPoker(paiList1[m]);
                            transform2.SetParent(manager.GetDictHandPoint2(realPosition));
                            transform2.GetChild(0).GetComponent<Image>().enabled = false;
                            transform2.localScale = new Vector3(1, 1, 1);
                            if (transform2.GetComponent<Poker>() != null) {
                                GameObject.Destroy(transform2.GetComponent<Poker>());
                            }
                        }
                        manager.GetDictNiuType2(realPosition).SetActive(true);
                        manager.GetDictNiuType2(realPosition).transform.GetChild(0).GetComponent<Image>().sprite =
                            DynamicUIManager.Instance.BaseNiuTypeGetSprite(paiTypeDict1);
                    }
                } catch (Exception ex) {
                    Debug.Log(ex.Message);
                }
            }
            manager.GetDictScoreName(realPosition).text = result.Scores.ToString();
            DNGlobalData.userNameList.Add(list[i].PlayName);
            DNGlobalData.userScoreList.Add(list[i].Scores);
            DNGlobalData.userChapterList.Add(list[i].PlaysNum);
            DNGlobalData.winCountList.Add(list[i].WinNum);
            DNGlobalData.loseCountList.Add(list[i].LoseNum);
            DNGlobalData.equalCountList.Add(list[i].EqualCount);

        }
        yield return new WaitForSeconds(0);
        SetChapterEndScore(chapterEnd);
    }
    DouniuGameChapterEnd tempChapterEnd;
    int resultLocalIndex = 0;
    float waitSecond = 1;
    //IEnumerator NewSetChapterEnd() {
    //    if (resultLocalIndex < manager.compareResultDict.Count) {
    //        int realPosition = positionArr[resultLocalIndex];
    //        DouniuCompareResult result = manager.compareResultDict[resultLocalIndex];
    //        NiuType paiTypeDict = DouniuPaiType.Instance.GetPaiTypeDict(result.PaiType);
    //        switch (DNGlobalData.changeMode) {
    //            case "1":
    //                if (paiTypeDict == NiuType.牛9) {
    //                    changeIndex = resultLocalIndex;
    //                }
    //                break;
    //            case "2":
    //                if (paiTypeDict == NiuType.牛牛) {
    //                    changeIndex = resultLocalIndex;
    //                }
    //                break;
    //            default:
    //                changeIndex = -1;
    //                break;
    //        }
    //        List<DouniuOnePai> paiList = result.PaiList;
    //        if (realPosition == 0) {
    //            if (manager.GetDictPaiCount(realPosition) == 1) {

    //                manager.ClearHandPoker(0, 3);

    //                for (int j = 0; j < paiList.Count; j++) {
    //                    Transform poker = manager.GetPoker(paiList[j]);//DealRealPoker(paiList[i], manager.GetDictHandPoint3(realPosition),1.5f,false);
    //                    poker.SetParent(manager.GetDictHandPoint3(0));
    //                    poker.GetChild(0).GetComponent<Image>().enabled = false;
    //                    poker.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    //                    //poker.localPosition=new Vector2(poker.localPosition.x,poker.localPosition.y+ 40);
    //                    if (poker.GetComponent<Poker>() != null) {
    //                        GameObject.Destroy(poker.GetComponent<Poker>());
    //                    }
    //                    manager.handPoker.Add(poker);
    //                }
    //                manager.GetDictNiuType3(0).SetActive(true);
    //                manager.GetDictNiuType3(0).transform.GetChild(0).GetComponent<Image>().sprite =
    //                DynamicUIManager.Instance.BaseNiuTypeGetSprite(paiTypeDict);
    //                manager.PlayTransformAudio("f0_nn" + result.PaiType);

    //            } else if (manager.GetDictPaiCount(realPosition) == 2) {

    //                manager.ClearHandPoker(0, 1);

    //                for (int j = 0; j < paiList.Count; j++) {
    //                    Transform poker = manager.GetPoker(paiList[j]);//DealRealPoker(paiList[i],);
    //                    poker.SetParent(manager.GetDictHandPoint1(0));
    //                    poker.GetChild(0).GetComponent<Image>().enabled = false;
    //                    poker.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    //                    //poker.localPosition = new Vector2(poker.localPosition.x, poker.localPosition.y + 40);
    //                    if (poker.GetComponent<Poker>() != null) {
    //                        GameObject.Destroy(poker.GetComponent<Poker>());
    //                    }
    //                    manager.handPoker.Add(poker);
    //                }
    //                manager.GetDictNiuType1(0).SetActive(true);
    //                manager.GetDictNiuType1(0).transform.GetChild(0).GetComponent<Image>().sprite =
    //                    DynamicUIManager.Instance.BaseNiuTypeGetSprite(paiTypeDict);
    //                manager.PlayTransformAudio("f0_nn" + result.PaiType);

    //                yield return new WaitForSeconds(waitSecond);

    //                //清理对应索引的手牌
    //                manager.ClearHandPoker(resultLocalIndex, 2);

    //                //manager.GetDictWinText1(realPosition).text = list[k].WinCountNum;
    //                NiuType paiTypeDict1 = DouniuPaiType.Instance.GetPaiTypeDict(result.PaiType2);
    //                List<DouniuOnePai> paiList1 = result.PaiList2;
    //                for (int j = 0; j < paiList1.Count; j++) {
    //                    Transform poker = manager.GetPoker(paiList1[j]);
    //                    poker.SetParent(manager.GetDictHandPoint2(0));
    //                    poker.GetChild(0).GetComponent<Image>().enabled = false;
    //                    poker.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    //                    //poker.localPosition = new Vector2(poker.localPosition.x, poker.localPosition.y + 40);
    //                    if (poker.GetComponent<Poker>() != null) {
    //                        GameObject.Destroy(poker.GetComponent<Poker>());
    //                    }
    //                    manager.handPoker2.Add(poker);
    //                }
    //                manager.GetDictNiuType2(0).SetActive(true);
    //                manager.GetDictNiuType2(0).transform.GetChild(0).GetComponent<Image>().sprite =
    //                    DynamicUIManager.Instance.BaseNiuTypeGetSprite(paiTypeDict1);
    //                manager.PlayTransformAudio("f0_nn" + result.PaiType2);
    //                //manager.GetDictWinText2(realPosition).text = list[k].WinCountNum2;
    //            }
    //        } else {
    //            //List<DouniuOnePai> paiList = result.PaiList;
    //            if (manager.GetDictPaiCount(realPosition) == 1) {

    //                manager.ClearHandPoker(realPosition, 3);

    //                for (int m = 0; m < paiList.Count; m++) {
    //                    Transform transform2 = manager.GetPoker(paiList[m]);
    //                    transform2.SetParent(manager.GetDictHandPoint3(realPosition));
    //                    transform2.GetChild(0).GetComponent<Image>().enabled = false;
    //                    transform2.localScale = new Vector3(1.3f, 1.3f, 1.3f);
    //                    if (transform2.GetComponent<Poker>() != null) {
    //                        GameObject.Destroy(transform2.GetComponent<Poker>());
    //                    }
    //                }
    //                manager.GetDictNiuType3(realPosition).SetActive(true);
    //                manager.GetDictNiuType3(realPosition).transform.GetChild(0).GetComponent<Image>().sprite =
    //                    DynamicUIManager.Instance.BaseNiuTypeGetSprite(paiTypeDict);
    //                //manager.PlayTransformAudio("f0_nn" + result.PaiType);

    //            } else if (manager.GetDictPaiCount(realPosition) == 2) {

    //                manager.ClearHandPoker(resultLocalIndex, 1);

    //                for (int m = 0; m < paiList.Count; m++) {
    //                    Transform transform2 = manager.GetPoker(paiList[m]);
    //                    transform2.SetParent(manager.GetDictHandPoint1(realPosition));
    //                    transform2.GetChild(0).GetComponent<Image>().enabled = false;
    //                    transform2.localScale = new Vector3(1, 1, 1);
    //                    if (transform2.GetComponent<Poker>() != null) {
    //                        GameObject.Destroy(transform2.GetComponent<Poker>());
    //                    }
    //                }
    //                manager.GetDictNiuType1(realPosition).SetActive(true);
    //                manager.GetDictNiuType1(realPosition).transform.GetChild(0).GetComponent<Image>().sprite =
    //                    DynamicUIManager.Instance.BaseNiuTypeGetSprite(paiTypeDict);
    //                //manager.PlayTransformAudio("f0_nn" + result.PaiType);

    //                //yield return new WaitForSeconds(waitSecond);

    //                //清理对应索引的手牌
    //                manager.ClearHandPoker(resultLocalIndex, 2);

    //                NiuType paiTypeDict1 = DouniuPaiType.Instance.GetPaiTypeDict(result.PaiType2);
    //                List<DouniuOnePai> paiList1 = result.PaiList2;
    //                for (int m = 0; m < paiList1.Count; m++) {
    //                    Transform transform2 = manager.GetPoker(paiList1[m]);
    //                    transform2.SetParent(manager.GetDictHandPoint2(realPosition));
    //                    transform2.GetChild(0).GetComponent<Image>().enabled = false;
    //                    transform2.localScale = new Vector3(1, 1, 1);
    //                    if (transform2.GetComponent<Poker>() != null) {
    //                        GameObject.Destroy(transform2.GetComponent<Poker>());
    //                    }
    //                }
    //                manager.GetDictNiuType2(realPosition).SetActive(true);
    //                manager.GetDictNiuType2(realPosition).transform.GetChild(0).GetComponent<Image>().sprite =
    //                    DynamicUIManager.Instance.BaseNiuTypeGetSprite(paiTypeDict1);
    //                //manager.PlayTransformAudio("f0_nn" + result.PaiType2);
    //            }
    //        }
    //        yield return new WaitForSeconds(waitSecond);
    //        resultLocalIndex++;
    //        StartCoroutine(NewSetChapterEnd());
    //    } else {
    //        SetChapterEndScore(tempChapterEnd);
    //    }
    //}
    public void SetChapterEndScore(DouniuGameChapterEnd chapterEnd) {

        DNGlobalData.userScoreList.Clear();
        DNGlobalData.userNameList.Clear();
        DNGlobalData.userChapterList.Clear();
        DNGlobalData.winCountList.Clear();
        DNGlobalData.loseCountList.Clear();
        DNGlobalData.equalCountList.Clear();

        List<DouniuCompareResult> list = chapterEnd.getCompareResultList();
        DouniuCompareResult result;
        int winNumber = 0;
        string str;
        for (int i = 0; i < list.Count; i++) {
            int realPosition = positionArr[list[i].LocationIndex];
            try {
                result = list[i];
                manager.GetDictDone(i).enabled = false;
                if (realPosition == 0) {
                    List<DouniuOnePai> paiList = result.PaiList;
                    if (manager.GetDictPaiCount(list[i].LocationIndex) == 1) {
                        str = list[i].WinCountNum;
                        int.TryParse(str, out winNumber);
                        manager.GetDictWinText3(realPosition).text = manager.GetNumberSprite(winNumber);

                    } else if (manager.GetDictPaiCount(list[i].LocationIndex) == 2) {
                        str = list[i].WinCountNum;
                        int.TryParse(str, out winNumber);
                        manager.GetDictWinText1(realPosition).text = manager.GetNumberSprite(winNumber);


                        str = list[i].WinCountNum2;
                        int.TryParse(str, out winNumber);
                        manager.GetDictWinText2(realPosition).text = manager.GetNumberSprite(winNumber);
                    }
                } else {
                    List<DouniuOnePai> paiList = result.PaiList;
                    if (manager.GetDictPaiCount(list[i].LocationIndex) == 1) {
                        str = list[i].WinCountNum;
                        int.TryParse(str, out winNumber);
                        manager.GetDictWinText3(realPosition).text = manager.GetNumberSprite(winNumber);

                    } else if (manager.GetDictPaiCount(list[i].LocationIndex) == 2) {
                        
                        str = list[i].WinCountNum;
                        int.TryParse(str, out winNumber);
                        manager.GetDictWinText1(realPosition).text = manager.GetNumberSprite(winNumber);
                        
                        str = list[i].WinCountNum2;
                        int.TryParse(str, out winNumber);
                        manager.GetDictWinText2(realPosition).text = manager.GetNumberSprite(winNumber);
                    }
                }
                manager.GetDictScoreName(realPosition).text = result.Scores.ToString();
                DNGlobalData.userNameList.Add(list[i].PlayName);
                DNGlobalData.userScoreList.Add(list[i].Scores);
                DNGlobalData.userChapterList.Add(list[i].PlaysNum);
                DNGlobalData.winCountList.Add(list[i].WinNum);
                DNGlobalData.loseCountList.Add(list[i].LoseNum);
                DNGlobalData.equalCountList.Add(list[i].EqualCount);
            } catch (Exception ex) {
                Debug.Log(ex.Message);
            }
        }
        manager.handPoker.Clear();
        manager.handPoker2.Clear();
        if (DNGlobalData.currentChapter >= DNGlobalData.maxChapter) {
            manager.noticeBG.enabled = true;
            manager.noticeTextComp.text = "对局已经结束，正在整理牌局...";
            manager.StartCountDown("ChapterEnd", 5);
        } else {
            if (!isOperation) {
                manager.ShowButton(UIButton.继续);
            } else {
                manager.HideButton(UIButton.继续);
            }
        }
    }

    /// <summary>
    /// 解散房间
    /// </summary>
    /// <param name="delRoom"></param>
    public void DelRoom(DouniuVoteRet delRoom) {
        if (DNGlobalData.currentChapter < DNGlobalData.maxChapter) {
            if (delRoom.Operation) {
                CountDownPanel.ShutDown = true;
                VoteCountDown.ShutDown = true;
                manager.CountDownOperation("ChapterEnd");
            }
            if(!delRoom.isResult()) {
                DNGlobalData.noticeText = "“" + delRoom.UserName + "”" + "拒绝解散房间，再玩一会儿吧";
                CountDownPanel.ShutDown = false;
                DNUIManager.Instance.PopPanel();
                DNUIManager.Instance.PushPanel(UIPanelType.NoticePanel);
            }
        }
    }
    public void IstZhuangExit(ExitDouniuRoomResult exit) {
        manager.ClearAllDictAndList();
        UIState_LoadingPage.isComeFromDN = true;
        Debug.Log("UIState_LoadingPage--DNgamePanel");
        GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_LoadingPage);
    }
    public void UserOffLine(DouniuUserOffline offLine) {
        manager.GetDictOffLine(offLine.getIndex()).enabled = true;
    }

    /// <summary>
    /// 通知用户有人退出需要隐藏
    /// </summary>
    /// <param name="exitResult"></param>
    public void ExitRoom(DouniuExitRoomRet exitResult) {
        Debug.Log("退出");
        for (int i = 0; i < DNGlobalData.userIDList.Count; i++) {
            if (exitResult.getUserID() == DNGlobalData.userIDList[i]) {
                manager.HidePlayer(i);
            }
        }
        readyCount--;
        if (readyCount == DNGlobalData.roomPlayerNumber - 1 && DNGlobalData.roomPlayerNumber >= 2) {
            if (DNGlobalData.currentUserID == DNGlobalData.fangZhuUserID) {
                manager.ShowButton(UIButton.开始);
            }
        } else {
            manager.HideButton(UIButton.开始);
        }
        DNGlobalData.roomPlayerNumber--;

        if (DNGlobalData.roomPlayerNumber <= 0) {
            Debug.Log("退出");
        }
    }

    /// <summary>
    /// 5，9后有人准备
    /// </summary>
    /// <param name="ready"></param>
    public void Ready(DouniuStartGameRet ready) {

    }

    /// <summary>
    /// 5,13后通知有人操作
    /// </summary>
    /// <param name="operation"></param>
    public void OperationRet(DouniuOperationRet operation) {
        string opt = operation.Opt;
        Debug.Log(opt);
        if (opt != null) {
            if (!(opt == "OPT_XIAZHU")) {
                if (opt == "OPT_QIPAI") {
                    manager.RetQiPaiOperation(operation);
                } else if (opt == "OPT_GENZHU") {
                    manager.RetGenZhuOperation(operation);
                } else if (opt == "OPT_QIANGZHUANG") {
                    manager.RetQiangZhuangOperation(operation);
                } else if (opt == "OPT_LIANGPAI") {
                    manager.RetLiangPaiOperation(operation);
                }
            } else {
                manager.RetXiaZhuOperation(operation);
            }
        }
    }
    /// <summary>
     /// 5，35 抢庄成功
     /// </summary>
     /// <param name="qiang"></param>
    public void SetQiangZhuangPlayer(QiangZhuangRet qiang) {
        manager.noticeBG.enabled = false;
        manager.noticeTextComp.text = "";
        manager.handPoker.Clear();
        manager.handPoker2.Clear();
        if (SceneManager.GetActiveScene().name == "BullFight") {
            Debug.Log("庄的位置：" + qiang.LocationIndex);
            DNGlobalData.chapterCount++;
            DNGlobalData.lookPoker = qiang.LookPoker;
            if (DNGlobalData.currentChapter == 0) {
                DNGlobalData.currentChapter = 1;
            }
            int playerIndex = qiang.LocationIndex;
            int num2 = 0;
            print(DNGlobalData.fangZhuUserID+"+"+DNGlobalData.currentUserID);
            positionArr = manager.SetPlayerPosition(DNGlobalData.roomPlayerNumber, DNGlobalData.locationIndex);
            manager.GetDictZhuang(playerIndex).SetActive(true);
            for (int i = 0; i < positionArr.Length; i++) {
                if (positionArr[i] == 0) {
                    if (qiang.LookPoker) {
                        if (!manager.GetDictZhuang(i).activeSelf) {
                            //manager.StartCountDown("RobHandPoker", 15);
                            manager.ShowAllRobPokerButton();
                        } else {
                            if (!DNGlobalData.confirmChongLian) {
                                DouniuCasiPai handCount = new DouniuCasiPai();
                                handCount.setPaiLens(1);
                                SocketMgr.GetInstance().Send(Net.instance.write(handCount));
                            } 
                            manager.noticeTextComp.text = "等待闲家选择手牌...";
                            manager.noticeBG.enabled = true;
                        }
                    } else {
                        if (!manager.GetDictZhuang(i).activeSelf) {
                            //manager.StartCountDown("RobHandPoker", 15);
                            manager.ShowAllRobPokerButton();
                        } else {
                            if (!DNGlobalData.confirmChongLian) {
                                DouniuCasiPai handCount = new DouniuCasiPai();
                                handCount.setPaiLens(1);
                                SocketMgr.GetInstance().Send(Net.instance.write(handCount));
                            } 
                            manager.noticeTextComp.text = "等待闲家选择手牌...";
                            manager.noticeBG.enabled = true;
                        }
                    }
                }
            }
            DNGlobalData.zhuangIndex = qiang.LocationIndex;
        }
    }

    /// <summary>
    /// 投票退出
    /// </summary>
    /// <param name="voteRet"></param>
    public void VoteExit(DouniuDelRoomRet voteRet) {
        DNGlobalData.delUserName = voteRet.getUserName();
        if (voteRet.getUserId() != DNGlobalData.currentUserID) {
            CountDownPanel.ShutDown = true;
            DNUIManager.Instance.PushPanel(UIPanelType.VotePanel);
        }
    }
    int addPlayerIndex;
    /// <summary>
    /// 有玩家加入
    /// </summary>
    /// <param name="userInfo"></param>
    public void AddPlayer(DouniuGameUserInfo userInfo) {
        positionArr = manager.SetPlayerPosition(DNGlobalData.roomPlayerNumber, DNGlobalData.locationIndex);
        addPlayerIndex = userInfo.getLocationIndex();
        int realPosition = positionArr[addPlayerIndex];
        manager.ShowPlayer(addPlayerIndex);
        manager.GetDictPlayerName(realPosition).text = userInfo.getUserName();
        manager.GetDictScoreName(realPosition).text = userInfo.getScore().ToString();
        manager.GetDictOffLine(addPlayerIndex).enabled = false;
        string path = userInfo.getAvatar();
        StartCoroutine(DownSprite(path));
    }

    int readyCount;
    public void ReadyRet(DouniuStartGameRet start) {
    }

    public void SetShortCut(int index, string shortCutIndexStr) {
        Image shortCut = manager.GetDictEmoji(positionArr[index]);
        shortCut.enabled = true;
        shortCut.sprite = DynamicUIManager.Instance.BaseNameGetSprite("trumpet");
        shortCut.DOFade(1, 0);
        Tweener te = shortCut.DOFade(0, 1);
        te.OnComplete(() => {
            Tweener tw = shortCut.DOFade(1, 0.5f);
            tw.OnComplete(() => {
                Tweener tr = shortCut.DOFade(0, 1);
                tr.OnComplete(() => {
                    Tweener tn = shortCut.DOFade(1, 0.5f);
                    tn.OnComplete(() => {
                        shortCut.DOFade(0, 1);
                    });
                });
            });
        });
        int.TryParse(shortCutIndexStr, out index);
        manager.PlayTransformAudio("fix_msg_" + index);
    }

    public void SetEmoji(int index, string emojiID) {
        Image emoji = manager.GetDictEmoji(positionArr[index]);
        emoji.enabled = true;
        emoji.sprite = DynamicUIManager.Instance.BaseNameGetSprite("emotion_" + emojiID);
        emoji.DOFade(0.95f, 0);
        Tweener tw= emoji.DOFade(1, 1f);
        tw.OnComplete(()=> {
            emoji.DOFade(0, 1f);
        });
    }

    public void SetUserVoice(AudioClip clip,int index) {
        Image voiceImg = manager.GetDictEmoji(positionArr[index]);
        voiceImg.enabled = true;
        voiceImg.sprite = DynamicUIManager.Instance.BaseNameGetSprite("trumpet");
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
        manager.PlayTransformAudioBaseAudio(clip);
    }
    
    public void RobHandPoker(DouniuCasiPaiRet robRet) {
        DNGlobalData.paiCountDic.Add(robRet.getIndex(), robRet.getPaiLens());
    }
    
}
