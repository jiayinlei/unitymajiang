using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReferEvent : EventManager
{
    public override void InformationSetting()
    {

    }
    public void CloseNoticePage()
    {
        DestroyImmediate(this.gameObject);
    }
    // Use this for initialization
    void Start () {
        transform.FindChild("DOTween/CloseButton").GetComponent<Button>().onClick.AddListener(() => {
            Destroy(gameObject);
        });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
