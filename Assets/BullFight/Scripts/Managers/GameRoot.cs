using Assets.Scripts.BullFight.Data;
using Assets.Scripts.BullFight.Manager;
using com.guojin.dn.net.message;
using com.guojin.mj.net;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRoot : MonoBehaviour {
    // Use this for initialization
    //public Transform poker1;
    //public Transform poker2;
    //public Transform poker3;
    //public Transform poker4;
    //public Transform poker5;
    void Awake() {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        DNUIManager.Instance.PushPanel(UIPanelType.DNGamePanel);
    }
    //public void Click() {
    //}
    //private void Start() {
    //    poker1.GetComponent<Poker>().PokerNumber = 2;
    //    poker2.GetComponent<Poker>().PokerNumber = 3;
    //    poker3.GetComponent<Poker>().PokerNumber = 5;
    //    poker4.GetComponent<Poker>().PokerNumber = 2;
    //    poker5.GetComponent<Poker>().PokerNumber = 1;
    //    poker1.GetComponent<Poker>().Value = 2;
    //    poker2.GetComponent<Poker>().Value = 3;
    //    poker3.GetComponent<Poker>().Value = 5;
    //    poker4.GetComponent<Poker>().Value = 2;
    //    poker5.GetComponent<Poker>().Value = 1;
    //    GameManager.instance.handPoker.Add(poker1);
    //    GameManager.instance.handPoker.Add(poker2);
    //    GameManager.instance.handPoker.Add(poker3);
    //    GameManager.instance.handPoker.Add(poker4);
    //    GameManager.instance.handPoker.Add(poker5);
    //    GameObject.Find("aa").GetComponent<Button>().onClick.AddListener(()=> {
    //        GameManager.instance.CalAutoOut();
    //    });
    //}
}
