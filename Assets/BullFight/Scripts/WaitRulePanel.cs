using Assets.Scripts.BullFight.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitRulePanel : BasePanel {
    Button  closeMask;
    Text playerNumber;
    private void Awake() {

        playerNumber = transform.FindChild("Rule/Number").GetComponent<Text>();
    }
    // Use this for initialization
    void Start () {
		
        //gameObject.SetActive(false);
        closeMask= transform.FindChild("CloseMask").GetComponent<Button>();
        closeMask.onClick.AddListener(() => {
            transform.PlayButtonVoice();
            gameObject.SetActive(false);
        });
        transform.FindChild("Rule/Chapter").GetComponent<Text>().text = DNGlobalData.maxChapter+"局";
        //transform.FindChild("Rule/HandPoker").GetComponent<Text>().text = DNGlobalData. + "手牌";
        switch (DNGlobalData.mode) {
            case "1":
                transform.FindChild("Rule/Mode").GetComponent<Text>().text = "房主坐庄";
                break;
            case "2":
                transform.FindChild("Rule/Mode").GetComponent<Text>().text = "随机坐庄";
                break;
        }
        Text fangShi = transform.FindChild("Rule/FangShi").GetComponent<Text>();
        print(DNGlobalData.changeMode);
        switch (DNGlobalData.changeMode) {
            case "1":
                fangShi.text = "牛九换庄";
                break;
            case "2":
                fangShi.text = "牛牛换庄";
                break;
            case "3":
                fangShi.text = "不换庄";
                break;
            default:
                fangShi.text = "牛九换庄";
                break;

        }
        //transform.FindChild("Rule/Special").GetComponent<Text>().text = DNGlobalData.maxChapter ;
        if (DNGlobalData.lookPoker) {
            transform.FindChild("Rule/Look").GetComponent<Text>().text = "看牌";
        } else {
            transform.FindChild("Rule/Look").GetComponent<Text>().text = "不看牌";

        }
        //transform.FindChild("Rule/KingChange").GetComponent<Text>().text = DNGlobalData.maxChapter + "局";
    }

    private void OnEnable() {
       playerNumber.text = DNGlobalData.roomPlayerNumber + "人";
    }
}
