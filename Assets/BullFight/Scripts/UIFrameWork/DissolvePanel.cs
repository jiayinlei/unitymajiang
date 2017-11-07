using Assets.Scripts.BullFight.Data;
using com.guojin.dn.net.message;
using com.guojin.mj.net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DissolvePanel : BasePanel {

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
        CountDownPanel.ShutDown = true;
        //VoteCountDown.ShutDown = true;
        countDown.SetActive(false);
        sureButton.onClick.AddListener(() => {
            transform.PlayButtonVoice();
            text.text = "正在等待其他人投票";
            cancelButton.gameObject.SetActive(false);
            sureButton.gameObject.SetActive(false);
            DNGlobalData.countDownOpt = "ExitRoom";
            VoteCountDown.ClockNum = 30;
            VoteCountDown.ShutDown = false;
            countDown.SetActive(true);
            DouniuDelRoom del = new DouniuDelRoom();
            //del.setUserId(DNGlobalData.fangZhuUserID);
            SocketMgr.GetInstance().Send(Net.instance.write(del));
        });
        cancelButton.onClick.AddListener(() => {
            transform.PlayButtonVoice();
            //StartCoroutine("ClosePanel");
            DNUIManager.Instance.PopPanel();
            //text.text = "您已拒绝退出";
        });
        transform.FindChild("ExitButton").GetComponent<Button>().onClick.AddListener(() => {
            transform.PlayButtonVoice();
            //StopCoroutine("ClosePanel");
            DNUIManager.Instance.PopPanel();
            //VoteCountDown.ShutDown = true;
           // CountDownPanel.ShutDown = false;
        });
    }
    public override void OnEnter() {
        base.OnEnter();
        cancelButton.gameObject.SetActive(true);
        sureButton.gameObject.SetActive(true);
        CountDownPanel.ShutDown = true;
    }
    // Update is called once per frame
    //void Update () {

    IEnumerator ClosePanel() {
        yield return new WaitForSeconds(2);
        DNUIManager.Instance.PopPanel();
        VoteCountDown.ShutDown = true;
        CountDownPanel.ShutDown = false;
    }
    //}
}
