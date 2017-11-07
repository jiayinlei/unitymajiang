using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticePageEvent : EventManager
{
    private Text hintText;

    public override void InformationSetting()
    {
        hintText = gameObject.transform.FindChild("PopBg/Text").GetComponent<Text>();
    }

    public void SetHintMessage(string message) {
        hintText.text = message;
    }

    void ClosePage()
    {
        Destroy(this.gameObject);
    }
}
