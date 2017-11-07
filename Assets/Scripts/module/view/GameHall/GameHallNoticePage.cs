using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHallNoticePage:MonoBehaviour {
    void CloseBtnClick()
    {
        print("click");
    }
    // Use this for initialization
    void Start () {
        transform.FindChild("DOTween/SureButton").GetComponent<Button>().onClick.AddListener(()=>{

            transform.PlayButtonVoice();
            Destroy(gameObject);
        });
        transform.FindChild("DOTween/NoticeText").GetComponent<Text>().text = DNGlobalData.noticeText;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
