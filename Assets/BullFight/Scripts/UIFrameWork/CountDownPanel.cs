using Assets.Scripts.BullFight.Data;
using Assets.Scripts.BullFight.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownPanel : MonoBehaviour {
    private static CountDownPanel instance;
    private Text clockText;
    private static bool shutDown;
    private static int clockNum;
    //private static string clockOperation = "";

    public static CountDownPanel Instance {
        get {
            if (instance == null) {
                instance = new CountDownPanel();
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

    //public void SetOpearation(string opt,int clockNum) {
    //    clockOperation = opt;
    //    CountDownPanel.clockNum = clockNum;

    //}

    // Use this for initialization
    void Awake () {
        clockText= transform.FindChild("Clock").GetChild(0).GetComponent<Text>();
        ShutDown = false;
        //InvokeRepeating("ClockDown", 0, 1);
        //InvokeRepeating("ClockDown", 0, 1);
    }
    //public override void OnEnter() {
    //    base.OnEnter();
    //}
    //private void ClockDown() {
    //    ClockNum-=1;
    //    if (ClockNum <= 0) {
    //        CancelInvoke("ClockDown");
    //        //GameManager.instance.OutClockOpt(out clockOperation);
    //        GameManager.instance.CountDownOperation(DNGlobalData.countDownOpt);
    //        Destroy(gameObject);
    //    }
    //}
    float time;
	// Update is called once per frame
	void Update () {
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
