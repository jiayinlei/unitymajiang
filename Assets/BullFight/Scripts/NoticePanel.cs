using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticePanel : BasePanel {

	// Use this for initialization
	void Start () {
	}
    public override void OnEnter() {
        base.OnEnter();
        transform.FindChild("SureButton").GetComponent<Button>().onClick.AddListener(() => {
            transform.PlayButtonVoice();
            DNUIManager.Instance.PopPanel();
            CountDownPanel.ShutDown = false;
        });
        transform.FindChild("CloseButton").GetComponent<Button>().onClick.AddListener(() => {
            transform.PlayButtonVoice();
            CountDownPanel.ShutDown = false;
            DNUIManager.Instance.PopPanel();
        });
        transform.FindChild("Text").GetComponent<Text>().text = DNGlobalData.noticeText;
    }
}
