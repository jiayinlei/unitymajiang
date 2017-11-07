using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour {
    protected virtual string[] GetMsgList() {
        return new string[] { };
    }
    public virtual void OnMsg(string msg, com.guojin.core.io.message.Message data) {

    }
    protected void initMsg() {
        string[] msgArr = this.GetMsgList();
        foreach (string msg in msgArr) {
            ObserverMgr.addEventListener(msg, this);
        }
    }
    void OnDisable() {
        //Debug.Log("removeEventListenerWithObserver");
        ObserverMgr.removeEventListenerWithObserver(this);
    }
}
