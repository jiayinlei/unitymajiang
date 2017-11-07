using Assets.Scripts.BullFight.Data;
using Assets.Scripts.BullFight.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoteCountDown : MonoBehaviour {
    private static VoteCountDown instance;
    private Text clockText;
    private static bool shutDown;
    private static int clockNum;
    //private static string clockOperation = "";

    public static VoteCountDown Instance {
        get {
            if (instance == null) {
                instance = new VoteCountDown();
            }
            return instance;
        }
    }

    public static int ClockNum {
        get {
            return clockNum;
        }

        set {
            clockNum = value;
        }
    }

    public static bool ShutDown {
        get {
            return shutDown;
        }

        set {
            shutDown = value;
        }
    }
    void Awake() {
        clockText = transform.FindChild("Clock").GetChild(0).GetComponent<Text>();
        ShutDown = false;
    }
    float time;
    void Update() {
        if (!ShutDown) {
            time += Time.deltaTime;
        } else {
            gameObject.SetActive(false);
        }
        if (time > 1 && !ShutDown) {
            ClockNum--;
            if (ClockNum <= 0) {
                ClockNum = 10;
                gameObject.SetActive(false);
                GameManager.instance.CountDownOperation(DNGlobalData.countDownOpt);
            }
            time = 0;
            //print("-1");
        }
        if (ClockNum >= 0) {
            clockText.text = ClockNum.ToString();
        }
    }
    //private void OnDestroy() {
    //    CancelInvoke("ClockDown");
    //}
}
