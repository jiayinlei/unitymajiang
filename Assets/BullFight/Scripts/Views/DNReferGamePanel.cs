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
using Assets.Scripts.BullFight.View;

public class DNReferGamePanel : EventManager
{
    private int[] positionArr;
    //AudioSource au;
    AudioSource audioSource;
    //AudioSource transSource;
    Text roomID, mode,chapter;
    GameManager manager;
    int changeIndex = -1;
    int voiceIndex;

    //战绩回放索引
    int chapterEndIndex=0;
    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        
        //transSource = GameObject.Find("GameManager").GetComponent<AudioSource>();
        //for(int i=0;i<DNGlobalData.)

        //chapterEndIndex++;
       // au = GameObject.Find("GameManager").GetComponent<AudioSource>();
        transform.FindChild("Back").GetComponent<Button>().onClick.AddListener(()=> {
            Screen.orientation = ScreenOrientation.Portrait;
            //Screen.autorotateToLandscapeLeft = false;
            //Screen.autorotateToLandscapeRight = false;
            //Screen.autorotateToPortrait = true;
            //Screen.autorotateToPortraitUpsideDown = true;
            manager.chapterEndList.Clear();
            manager.ClearAllDictAndList();
            DNGlobalData.userImageDict.Clear();
            Destroy(gameObject);
            Destroy(DNGlobalData.blur);
            UIManager.ChangeUI(UIManager.PageState.DNMainPanelPage, (GameObject obj) => {
                obj.GetComponent<DNCreatRoomPage>().InformationSetting();
                UIManager.ChangeUI(UIManager.PageState.DNReferPanel, (GameObject obj1) => {

                });
            });
                // Destroy(gameObject);
                // SceneManager.LoadScene("GameHall");
                //Screen.orientation = ScreenOrientation.Portrait;
                // UIState_LoadingPage.isComeFromDNPlayeBack = true;
                //GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_LoadingPage);
            });
        transform.FindChild("Right").GetComponent<Button>().onClick.AddListener(() => {
            if (chapterEndIndex < manager.chapterEndList.Count-1) {
                //manager.ClearHandPoker();
                manager.ClearAllPoker();
                manager.HideAllNeedUI(); 
                DNGlobalData.paiCountDic.Clear();
                chapterEndIndex++;
                print("++" + chapterEndIndex);
                SetChapterEnd(manager.chapterEndList[manager.chapterEndList.Count-1-chapterEndIndex]);
            }
        });
        transform.FindChild("Left").GetComponent<Button>().onClick.AddListener(() => {
            if (chapterEndIndex > 0) {
                manager.ClearAllPoker();
                DNGlobalData.paiCountDic.Clear();
                manager.HideAllNeedUI();
                chapterEndIndex--;
                print("--" + chapterEndIndex);
                SetChapterEnd(manager.chapterEndList[manager.chapterEndList.Count - 1 - chapterEndIndex]);
            }
        });



    }


    public void SetChapterEnd(DouniuGameChapterEnd chapterEnd) {

        manager.fillList.Clear();
        manager.GetDictZhuang(chapterEnd.getZhuangIndex()).SetActive(true);
        DNGlobalData.waitEnd = false;
        List<DouniuCompareResult> list = chapterEnd.getCompareResultList();
        foreach (var a in list) {
            if (DNGlobalData.currentUserName == a.PlayName) {
                DNGlobalData.locationIndex = a.LocationIndex;
            }
            if (a.PaiList2 == null) {
                DNGlobalData.paiCountDic.Add(a.LocationIndex, 1);
            } else {
                DNGlobalData.paiCountDic.Add(a.LocationIndex, 2);
            }
        }
        DNGlobalData.maxChapter = list[0].MaxChapter;
       // DNGlobalData.currentChapter = chapterEndIndex;
        chapter.text = (chapterEndIndex + 1) + "/" + DNGlobalData.maxChapter + "局";
        DNGlobalData.roomPlayerNumber = list.Count;
        positionArr = manager.SetPlayerPosition(DNGlobalData.roomPlayerNumber, DNGlobalData.locationIndex);
        //manager.ClearHandPoker();
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
            manager.GetDictPlayer(realPosition).SetActive(true);
            manager.GetDictUserImage(realPosition).sprite = DNGlobalData.userImageDict[realPosition];
            manager.GetDictPlayerName(realPosition).text = list[i].PlayName;
            // manager.GetDictDone(i).enabled = false;
            //int reverseIndex = realPosition;
            if (realPosition == 0) {
                manager.GetDictPlayer(realPosition).transform.FindChild("headMask").gameObject.SetActive(false);
                List<DouniuOnePai> paiList = result.PaiList;
                if (manager.GetDictPaiCount(list[i].LocationIndex) == 1) {
                    for (int j = 0; j < paiList.Count; j++) {
                        Transform poker = manager.GetPoker(paiList[j]);//DealRealPoker(paiList[i], manager.GetDictHandPoint3(realPosition),1.5f,false);
                        poker.SetParent(manager.GetDictHandPoint3(realPosition));
                        poker.GetChild(0).GetComponent<Image>().enabled = false;
                        poker.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                        //poker.localPosition=new Vector2(poker.localPosition.x,poker.localPosition.y+ 40);
                        if (poker.GetComponent<Poker>() != null) {
                            GameObject.Destroy(poker.GetComponent<Poker>());
                        }
                    }
                    manager.GetDictNiuType3(realPosition).SetActive(true);
                    manager.GetDictNiuType3(realPosition).transform.GetChild(0).GetComponent<Image>().sprite =
                    DynamicUIManager.Instance.BaseNiuTypeGetSprite(paiTypeDict);
                    //manager.PlayTransformAudio("f0_nn" + result.PaiType);

                } else if (manager.GetDictPaiCount(list[i].LocationIndex) == 2) {

                    try {
                        for (int j = 0; j < paiList.Count; j++) {
                            Transform poker = manager.GetPoker(paiList[j]);//DealRealPoker(paiList[i],);
                            poker.SetParent(manager.GetDictHandPoint1(realPosition));
                            poker.GetChild(0).GetComponent<Image>().enabled = false;
                            poker.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                            //poker.localPosition = new Vector2(poker.localPosition.x, poker.localPosition.y + 40);
                            if (poker.GetComponent<Poker>() != null) {
                                GameObject.Destroy(poker.GetComponent<Poker>());
                            }
                            manager.handPoker.Add(poker);
                        }

                        manager.GetDictNiuType1(realPosition).SetActive(true);
                        manager.GetDictNiuType1(realPosition).transform.GetChild(0).GetComponent<Image>().sprite =
                            DynamicUIManager.Instance.BaseNiuTypeGetSprite(paiTypeDict);
                        //manager.PlayTransformAudio("f0_nn" + result.PaiType);
                    } catch (Exception ex) {
                        Debug.Log(ex.Message);
                    }
                    try {
                        //manager.GetDictWinText1(realPosition).text = list[k].WinCountNum;
                        NiuType paiTypeDict1 = DouniuPaiType.Instance.GetPaiTypeDict(list[i].PaiType2);
                        List<DouniuOnePai> paiList1 = list[i].PaiList2;
                        for (int j = 0; j < paiList1.Count; j++) {
                            Transform poker = manager.GetPoker(paiList1[j]);
                            poker.SetParent(manager.GetDictHandPoint2(realPosition));
                            poker.GetChild(0).GetComponent<Image>().enabled = false;
                            poker.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                            //poker.localPosition = new Vector2(poker.localPosition.x, poker.localPosition.y + 40);
                            if (poker.GetComponent<Poker>() != null) {
                                GameObject.Destroy(poker.GetComponent<Poker>());
                            }
                        }
                        manager.GetDictNiuType2(realPosition).SetActive(true);
                        manager.GetDictNiuType2(realPosition).transform.GetChild(0).GetComponent<Image>().sprite =
                            DynamicUIManager.Instance.BaseNiuTypeGetSprite(paiTypeDict1);
                        //manager.PlayTransformAudio("f0_nn" + result.PaiType2);
                        //manager.GetDictWinText2(realPosition).text = list[k].WinCountNum2;           
                    } catch (Exception ex) {
                        Debug.Log(ex.Message);
                    }
                }
            } else {
                try {
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
                        //manager.GetDictWinText1(reverseIndex).text = list[k].WinCountNum;
                        
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
        SetChapterEndScore(chapterEnd);
    }
    public void SetChapterEndScore(DouniuGameChapterEnd chapterEnd) {

        DNGlobalData.userScoreList.Clear();
        DNGlobalData.userNameList.Clear();
        DNGlobalData.userChapterList.Clear();
        DNGlobalData.winCountList.Clear();
        DNGlobalData.loseCountList.Clear();
        DNGlobalData.equalCountList.Clear();

        List<DouniuCompareResult> list = chapterEnd.getCompareResultList();
        //manager.ClearHandPoker();
        DouniuCompareResult result;
        int winNumber = 0;
        string str;
        //manager.handPoker.Clear();
        //manager.handPoker2.Clear();
        for (int i = 0; i < list.Count; i++) {
            int realPosition = positionArr[list[i].LocationIndex];
            try {
                result = list[i];
                manager.GetDictDone(i).enabled = false;
                //int reverseIndex = realPosition;
                if (realPosition == 0) {
                    List<DouniuOnePai> paiList = result.PaiList;
                    if (manager.GetDictPaiCount(list[i].LocationIndex) == 1) {

                        //manager.PlayTransformAudio("f0_nn" + result.PaiType);
                        str = list[i].WinCountNum;
                        int.TryParse(str, out winNumber);
                        manager.GetDictWinText3(realPosition).text = manager.GetNumberSprite(winNumber);

                    } else if (manager.GetDictPaiCount(list[i].LocationIndex) == 2) {
                        //manager.PlayTransformAudio("f0_nn" + result.PaiType);
                        str = list[i].WinCountNum;
                        int.TryParse(str, out winNumber);
                        manager.GetDictWinText1(realPosition).text = manager.GetNumberSprite(winNumber);


                        str = list[i].WinCountNum2;
                        int.TryParse(str, out winNumber);
                        manager.GetDictWinText2(realPosition).text = manager.GetNumberSprite(winNumber);
                        //manager.GetDictWinText2(realPosition).text = list[k].WinCountNum2;
                    }
                } else {
                    List<DouniuOnePai> paiList = result.PaiList;
                    if (manager.GetDictPaiCount(list[i].LocationIndex) == 1) {

                        //manager.GetDictWinText3(reverseIndex).text = list[k].WinCountNum;
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
    }

    public override void InformationSetting() {
        manager = GameManager.instance;
        manager.InitOperation();
        GameObject staticSource = GameObject.Find("StaticSource");
        roomID = transform.FindChild("Table/RoomID").GetComponent<Text>();
        mode = transform.FindChild("Table/Mode").GetComponent<Text>();
        chapter = transform.FindChild("Table/Chapter").GetComponent<Text>();
        //发送5.54 接收5.56
        if (staticSource != null) {
            audioSource = staticSource.GetComponent<AudioSource>();
            SetChapterEnd(manager.chapterEndList[manager.chapterEndList.Count - 1 - chapterEndIndex]);
        }
        roomID.text = "房间号:" + DNGlobalData.roomID;
        //throw new NotImplementedException();
    }
}
