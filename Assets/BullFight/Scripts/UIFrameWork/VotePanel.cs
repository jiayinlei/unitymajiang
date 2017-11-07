using com.guojin.dn.net.message;
using com.guojin.mj.net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.BullFight.Data;
using Assets.Scripts.BullFight.Manager;
using GameCore;

public class VotePanel : BasePanel {
    Button sureButton;
    Button cancelButton;
    GameObject countDown;
    Text text;
    // Use this for initialization
    void Awake () {
        sureButton = transform.FindChild("SureButton").GetComponent<Button>();
        cancelButton = transform.FindChild("CancelButton").GetComponent<Button>();
        text = transform.FindChild("Text").GetComponent<Text>();
        countDown = transform.FindChild("VoteCountDown").gameObject;
        sureButton.onClick.AddListener(()=> {
            //DNUIManager.Instance.ClearPanelDict();
            //DNUIManager.Instance.ClearStack();
            GameManager.instance.CountDownOperation("SureVote");
            VoteCountDown.ShutDown = true;
            countDown.SetActive(false);
            //DNUIManager.Instance.PopPanel();
            //GameManager.instance.ClearAllDictAndList();
            //UIState_LoadingPage.isComeFromDN = true;
            //GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_LoadingPage);
            text.text = "正在等待其他人投票";
            cancelButton.gameObject.SetActive(false);
            sureButton.gameObject.SetActive(false);
        });
        cancelButton.onClick.AddListener(() => {
            GameManager.instance.CountDownOperation("CancelVote");
            VoteCountDown.ShutDown = true;
            //CountDownPanel.ShutDown = false;
            countDown.SetActive(false);
            text.text = "您已拒绝退出";
            cancelButton.gameObject.SetActive(false);
            sureButton.gameObject.SetActive(false);
            StartCoroutine("ClosePanel");
        });
        transform.FindChild("ExitButton").GetComponent<Button>().onClick.AddListener(() => {
            //GameManager.instance.CountDownOperation("CancelVote");
           // VoteCountDown.ShutDown = true;
            //CountDownPanel.ShutDown = false;
           // countDown.SetActive(false);
            //StopCoroutine("ClosePanel");
            DNUIManager.Instance.PopPanel();
        });
    }
    public override void OnEnter() {
        base.OnEnter();
        text.text = DNGlobalData.delUserName + "请求解散房间";
        countDown.SetActive(true);
        DNGlobalData.countDownOpt = "SureVote";
        CountDownPanel.ShutDown = true;
        VoteCountDown.ShutDown = false;
        VoteCountDown.ClockNum = 30;
        cancelButton.gameObject.SetActive(true);
        sureButton.gameObject.SetActive(true);
    }
    // Update is called once per frame
    //void Update () {

    //}
    IEnumerator ClosePanel() {
        yield return new WaitForSeconds(2);
        DNUIManager.Instance.PopPanel();
    }
}
