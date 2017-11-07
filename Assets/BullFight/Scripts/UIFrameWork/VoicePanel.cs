using com.guojin.dn.net.message;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VoicePanel : BasePanel{
    private GameObject line1;
    private GameObject line2;
    private GameObject line3;
    private float time;
    private int moveType;
    // Use this for initialization
    void Start () {
        line1 = transform.FindChild("Line1").gameObject;
        line2 = transform.FindChild("Line2").gameObject;
        line3 = transform.FindChild("Line3").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time > 0.5f) {
            moveType++;
            if (moveType > 3) {
                moveType = 0;
            }
            time = 0;
        }
        switch (moveType) {
            case 0:
                line1.SetActive(false);
                line2.SetActive(false);
                line3.SetActive(false);
                break;
            case 1:
                line1.SetActive(true);
                line2.SetActive(false);
                line3.SetActive(false);
                break;
            case 2:
                line1.SetActive(true);
                line2.SetActive(true);
                line3.SetActive(false);
                break;
            case 3:
                line1.SetActive(true);
                line2.SetActive(true);
                line3.SetActive(true);
                break;
        }

    }
    private void OnEnable() {
    }
    private void OnDisable() {
    }

}
