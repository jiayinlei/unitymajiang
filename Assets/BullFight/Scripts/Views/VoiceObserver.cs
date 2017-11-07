using System.Collections;
using System.Collections.Generic;
using com.guojin.core.io.message;
using UnityEngine;

public class VoiceObserver : Observer {
    private void Start() {
        initMsg();
    }
    protected override string[] GetMsgList() {
        return new string[] {


        };
    }
    public override void OnMsg(string msg, Message data) {
        base.OnMsg(msg, data);
    }
}
