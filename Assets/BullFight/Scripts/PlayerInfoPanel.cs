using Assets.Scripts.BullFight.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoPanel : MonoBehaviour {
    Image userImage;
    Text userName;
    Text userID;
    Text score;
	// Use this for initialization
	void Start () {
        transform.FindChild("CloseBg").GetComponent<Button>().onClick.AddListener(()=> {
            DNUIManager.Instance.PopPanel();
        });
        transform.FindChild("PopBg/CloseBtn").GetComponent<Button>().onClick.AddListener(() => {
            transform.PlayButtonVoice();
            DNUIManager.Instance.PopPanel();
        });

        userImage = transform.FindChild("PopBg/HeadBg/HeadImage").GetComponent<Image>();
        userName = transform.FindChild("PopBg/HeadBg/NameText/Name").GetComponent<Text>();
        userID = transform.FindChild("PopBg/HeadBg/IDText/ID").GetComponent<Text>();
        score = transform.FindChild("PopBg/HeadBg/Score/Score").GetComponent<Text>();
    }
    private void OnEnable() {
        userImage.sprite = DNGlobalData.clickUserImage;
        userName.text = DNGlobalData.clickUserName;
        userID.text = DNGlobalData.clickUserID;
        score.text = DNGlobalData.clickUserSocre;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
