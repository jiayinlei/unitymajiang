using com.guojin.dn.net.message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.guojin.dn.player;
using Assets.Scripts.BullFight.Data;
using Assets.Scripts.BullFight.Manager;
using GameCore;

public class ResultPanel : BasePanel {
    public static Dictionary<int, ResultComponent> playerComDict = new Dictionary<int, ResultComponent>();
    // Use this for initialization
   
        int max;
        int maxIndex;
    //public override void OnEnter() {
    //    base.OnEnter();
    void Start() {
        //transform.FindChild("ShareButton").GetComponent<Button>().onClick.AddListener(() => {

        //});
        transform.FindChild("EndButton").GetComponent<Button>().onClick.AddListener(() => {
            //DNUIManager.Instance.PopPanel();
            GameManager.instance.ClearAllDictAndList();
            UIState_GameHallPage.comeFromState = UIState_GameHallPage.ComeFromState.FromDNFighting;
            Debug.Log("UIState_LoadingPage--ResultPanel");
            GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_LoadingPage);
            //DouniuBack douniuBack = new DouniuBack();
            //SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(douniuBack));
        });
        transform.FindChild("RoomID").GetComponent<Text>().text = "房号："+ DNGlobalData.roomID;
        //transform.FindChild("CurrentChapter").GetComponent<Text>().text ="局数："+ DNGlobalData.currentChapter;
        transform.FindChild("GameTime").GetComponent<Text>().text =System.DateTime.Now.ToString();
        AddUser(0, "User1");
        AddUser(1, "User2");
        AddUser(2, "User3");
        AddUser(3, "User4");
        AddUser(4, "User5");
        //AddUser(5, "User6");
        for (int i = 0; i < 5; i++) {
            GetDictPlayer(i).SetActive(false);
        }
        //DNGlobalData.roomPlayerNumber = 2;
        //DNGlobalData.userNameList.Add("a");
        //DNGlobalData.userNameList.Add("b");
        try {
            for (int i = 0; i < DNGlobalData.userNameList.Count; i++) {
                GetName(i).text = DNGlobalData.userNameList[i];
                //GetID(i).text ="ID:"+ DNGlobalData.userID[i].ToString();
                GetScore(i).text = "分数:" + DNGlobalData.userScoreList[i].ToString();
                //GetUserImage(i).sprite = DNGlobalData.userImage[i];
                GetChapter(i).text = DNGlobalData.userChapterList[i] + "/" + DNGlobalData.maxChapter;
                GetWinCount(i).text = DNGlobalData.winCountList[i];
                GetLoseCount(i).text = DNGlobalData.loseCountList[i];
                GetEqualCount(i).text = DNGlobalData.equalCountList[i];
                GetDictPlayer(i).SetActive(true);
                //GetWin(i).enabled=false;
            }
        } catch (System.Exception ex) {
            print(ex.Message);
        }
        //List<string> scoreSort = new List<string>();
        //List<int> score = new List<int>();
        //List<int> newScore = new List<int>();
        //scoreSort = DNGlobalData.userScoreList;
        //int num;
        //foreach (var a in scoreSort) {
        //    int.TryParse(a, out num);
        //    score.Add(num);
        //    newScore.Add(num);
        //}
        ////newScore = score;
        //for (int i = 0; i < score.Count; i++) {
        //    for (int j = i; j < score.Count; j++) {
        //        if (score[i] < score[j]) {
        //            int temp = score[i];
        //            score[i] = score[j];
        //            score[j] = temp;
        //        }
        //    }
        //}
        //max = score[0];
        //scoreSort.Clear();
        //foreach (var a in score) {
        //    //print(a);
        //    scoreSort.Add(a.ToString());
        //}
        ////foreach (var a in newScore) {
        ////    print("new:"+a);
        ////    //scoreSort.Add(a.ToString());
        ////}
        //for (int i = 0; i < newScore.Count; i++) {
        //    if (max == newScore[i]) {
        //        maxIndex = i;
        //    }
        //}


        //GetWin(maxIndex).enabled = true;
        //GetWin(maxIndex).sprite = DynamicUIManager.Instance.BaseNameGetSprite("Win");
        //score.Sort();


    }
    private void AddUser(int index, string user) {
        ResultComponent result = new ResultComponent();
        result.Player = transform.FindChild("Users/" + user).gameObject;
        //result.UserImage = transform.FindChild("Users/" + user + "/Image").GetComponent<Image>();
        result.Score = transform.FindChild("Users/" + user + "/Score").GetComponent<Text>();
        //result.Win = transform.FindChild("Users/" + user + "/FinallyWin").GetComponent<Image>();
        result.Name = transform.FindChild("Users/" + user + "/UserName").GetComponent<Text>();
        //result.Id= transform.FindChild("Users/" + user + "/UserID").GetComponent<Text>();
        result.Chapter = transform.FindChild("Users/" + user + "/Chapter").GetComponent<Text>();
        result.WinCount = transform.FindChild("Users/" + user + "/WinCount").GetComponent<Text>();
        result.LoseCount = transform.FindChild("Users/" + user + "/LoseCount").GetComponent<Text>();
        result.EqualCount = transform.FindChild("Users/" + user + "/EqualCount").GetComponent<Text>();
        playerComDict.Add(index, result);
        
    }
    //private void OnDisable() {
    //    //scoreSort
    //}
    private GameObject GetDictPlayer(int playerIndex) {
        GameObject tempObj;
        ResultComponent player;
        if (playerComDict.TryGetValue(playerIndex, out player)) {
            tempObj = player.Player;
            return tempObj;
        } else {
            return null;
        }
    }
    private Text GetID(int playerIndex) {
        Text tempObj;
        ResultComponent player;
        if (playerComDict.TryGetValue(playerIndex, out player)) {
            tempObj = player.Id;
            return tempObj;
        } else {
            return null;
        }
    }
    private Text GetChapter(int playerIndex) {
        Text tempObj;
        ResultComponent player;
        if (playerComDict.TryGetValue(playerIndex, out player)) {
            tempObj = player.Chapter;
            return tempObj;
        } else {
            return null;
        }
    }
    private Text GetWinCount(int playerIndex) {
        Text tempObj;
        ResultComponent player;
        if (playerComDict.TryGetValue(playerIndex, out player)) {
            tempObj = player.WinCount;
            return tempObj;
        } else {
            return null;
        }
    }
    private Text GetLoseCount(int playerIndex) {
        Text tempObj;
        ResultComponent player;
        if (playerComDict.TryGetValue(playerIndex, out player)) {
            tempObj = player.LoseCount;
            return tempObj;
        } else {
            return null;
        }
    }
    private Text GetEqualCount(int playerIndex) {
        Text tempObj;
        ResultComponent player;
        if (playerComDict.TryGetValue(playerIndex, out player)) {
            tempObj = player.EqualCount;
            return tempObj;
        } else {
            return null;
        }
    }
    private Text GetName(int playerIndex) {
        Text tempObj;
        ResultComponent player;
        if (playerComDict.TryGetValue(playerIndex, out player)) {
            tempObj = player.Name;
            return tempObj;
        } else {
            return null;
        }
    }
    private Image GetUserImage(int playerIndex) {
        Image tempObj;
        ResultComponent player;
        if (playerComDict.TryGetValue(playerIndex, out player)) {
            tempObj = player.UserImage;
            return tempObj;
        } else {
            return null;
        }
    }
    private Text GetScore(int playerIndex) {
        Text tempObj;
        ResultComponent player;
        if (playerComDict.TryGetValue(playerIndex, out player)) {
            tempObj = player.Score;
            return tempObj;
        } else {
            return null;
        }
    }
    private Image GetWin(int playerIndex) {
        Image tempObj;
        ResultComponent player;
        if (playerComDict.TryGetValue(playerIndex, out player)) {
            tempObj = player.Win;
            return tempObj;
        } else {
            return null;
        }
    }

}
