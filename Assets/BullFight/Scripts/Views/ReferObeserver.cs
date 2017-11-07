using System.Collections;
using System.Collections.Generic;
using com.guojin.core.io.message;
using UnityEngine;
using com.guojin.mj.net.message;
using UnityEngine.UI;
using com.guojin.dn.net.message;
using LitJson;
using System;
using Assets.Scripts.BullFight.Manager;
using Assets.Scripts.BullFight.Data;

public class ReferObeserver : Observer {
    GameObject referResource;
    GameObject userScoreResource;
    Transform referParent;
    private int[] positionArr;
    AudioSource au;
    AudioSource audioSource;
    AudioSource transSource;
    int recPlayBackCount;
    private void Awake() {
        DouniuRoomHistoryList history = new DouniuRoomHistoryList();
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(history));
    }
    private void Start() {
        initMsg();
        InitUI();
    }
    protected override string[] GetMsgList() {
        return new string[] {
            MessageFactoryImpi.instance.getMessageString(5, 49),
            MessageFactoryImpi.instance.getMessageString(5,22),
            MessageFactoryImpi.instance.getMessageString(5, 56),
        };
    }
    public override void OnMsg(string msg, Message data) {
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 49)) {
            DouniuRoomHistoryListRet history = data as DouniuRoomHistoryListRet;
            SetHistory(history);
        }
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 56)) {
            RecordChapterInfoRet rec = (RecordChapterInfoRet)data;
            recPlayBackCount = rec.List.Count;
            for (int i = 0; i < rec.List.Count; i++) {
                Debug.Log(rec.List[i].ChapterInfo);
                PlaybackModel rejson = LitJson.JsonMapper.ToObject<PlaybackModel>(rec.List[i].ChapterInfo);
                executeIntrust(rejson.gameChapterEnd);
            }
            //GameObject obj = TooL.loadPrefab(GameObject.Find("addNode"), "DNReferGamePanel");
            //GameObject obj2 = GameObject.Instantiate(Resources.Load<GameObject>("BullFightPrefab/Blur"));
        }
        if (msg == MessageFactoryImpi.instance.getMessageString(5, 22)) {
            chapterEnd = data as DouniuGameChapterEnd;
            GameManager.instance.chapterEndList.Add(chapterEnd);
            if ( GameManager.instance.chapterEndList.Count == recPlayBackCount) {
                downLocationIndex = chapterEnd.getCompareResultList()[downSpriteIndex].LocationIndex;
                StartCoroutine(DownSprite(chapterEnd.getCompareResultList()[downSpriteIndex].Avator));
                //for (int i = 0; i < chapterEnd.getCompareResultList().Count; i++) {
                //    downLocationIndex=
                //}
            }
        }
    }
    DouniuGameChapterEnd chapterEnd;
    int downLocationIndex=0;
    int downSpriteIndex=0;
    Texture2D tex2d;
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
            DNGlobalData.userImageDict.Add(downLocationIndex,sp);
            // manager.GetDictUserImage(addPlayerIndex).sprite = sp;
            //中途加入不能这样写。没有中途加入可用(好像又没有关系)
            if (DNGlobalData.userImageDict.Count == chapterEnd.getCompareResultList().Count) {

                //UnityEngine.SceneManagement.SceneManager.LoadScene("DNPlayBack");
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                Screen.orientation = ScreenOrientation.AutoRotation;
                Screen.autorotateToLandscapeLeft = true;
                Screen.autorotateToLandscapeRight = true;
                Screen.autorotateToPortrait = false;
                Screen.autorotateToPortraitUpsideDown = false;
                UIManager.ChangeUI(UIManager.PageState.DNReferGamePanel, (GameObject obj) => {
                    //obj.GetComponent<NetConnectPageEvent>().Init();
                    obj.GetComponent<DNReferGamePanel>().InformationSetting();
                    DNGlobalData.blur=Instantiate(Resources.Load<GameObject>("BullFightPrefab/Blur"));
                    try {
                        Destroy(transform.gameObject);
                        Destroy(GameObject.Find("DNMainPanelPage(Clone)"));
                    } catch (Exception ex) {
                        Debug.Log(ex.Message);
                    }
                });


            } else {
                downSpriteIndex++;
                downLocationIndex = chapterEnd.getCompareResultList()[downSpriteIndex].LocationIndex;
                StartCoroutine(DownSprite(chapterEnd.getCompareResultList()[downSpriteIndex].Avator));
            }
        }
    }
    public static Sprite CreateSprite(Texture2D texture, int width, int height) {
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0, 0));
        return sp;
    }
    public class PlaybackModel {
        public string gameChapterEnd;
    }
    public void executeIntrust(string intrust) {
        byte[] instuctBytes = hexStringToByte(intrust);
        SocketMgr.GetInstance().OnWebSocketUnityReceiveData(instuctBytes);
    }

    public static byte[] hexStringToByte(string hex) {
        int len = (hex.Length / 2);
        byte[] result = new byte[len];
        char[] achar = hex.ToCharArray();
        for (int i = 0; i < len; i++) {
            int pos = i * 2;
            result[i] = (byte)(toByte(achar[pos]) << 4 | toByte(achar[pos + 1]));
        }
        return result;
    }
    private static byte toByte(char c) {
        c = Char.ToUpper(c);
        byte b = (byte)"0123456789ABCDEF".IndexOf(c);
        return b;
    }
    void SetHistory(DouniuRoomHistoryListRet history) {
        //Debug.Log("历史");
        List<DouniuRoomHistory> list = history.getList();
        for (int i = 0; i < list.Count; i++) {
            GameObject refer = Instantiate(referResource);
            string roomid = list[i].getRoomCheckId();
            string time = list[i].getStartDate();

            refer.transform.SetParent(referParent);
            refer.transform.localScale = Vector3.one;
            Transform scoreParent = refer.transform.FindChild("UserScore");
            //print(list[i].getChapterNums());
            refer.transform.FindChild("ShareButton").GetComponent<Button>().onClick.AddListener(() => {
                //LoginAndShare.Controller.OnClickSharedDN(1);
            });
            refer.transform.FindChild("PlayBack").GetComponent<Button>().onClick.AddListener(() => {
                //加载战绩预设体
                //obj.GetComponent<DNGamePanelObserver>().enabled = false;
                //obj.GetComponent<DNGamePanel>().enabled = false;
                //obj.AddComponent<DNRecordGamePanel>();
                //发送5.54 接收5.56
                DNGlobalData.roomID = roomid;
                GameManager.instance.chapterEndList.Clear();
                RecordChapterInfo rec = RecordChapterInfo.Creat(roomid, time);
                SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(rec));

            });
            refer.transform.FindChild("RoomID").GetComponent<Text>().text = list[i].getRoomCheckId();
            refer.transform.FindChild("Time").GetComponent<Text>().text = list[i].getStartDate();
            //foreach (var a in list[i].getUserNames()) {
            //    Debug.Log("名字："+a);
            //}
            for (int j = 0; j < list[i].getUserNames().Length; j++) {
                GameObject score = Instantiate(userScoreResource);
                score.transform.SetParent(scoreParent);
                score.transform.localScale = Vector3.one;
                //print(list[i].getUserNames()[j]);
                //print(list[i].getScores()[j].ToString());
                score.transform.FindChild("UserName").GetComponent<Text>().text = list[i].getUserNames()[j];
                score.transform.FindChild("UserScore").GetComponent<Text>().text = list[i].getScores()[j].ToString();
            }
        }
    }
    private void InitUI() {
        userScoreResource = Resources.Load<GameObject>("BullFightPrefab/User");
        referResource = Resources.Load<GameObject>("BullFightPrefab/Refer");
        referParent = transform.FindChild("DOTween/Scroll View/Viewport/Content");
        transform.FindChild("DOTween/CloseButton").GetComponent<Button>().onClick.AddListener(() => {
            transform.PlayButtonVoice();
            Destroy(gameObject);
        });
    }
}
