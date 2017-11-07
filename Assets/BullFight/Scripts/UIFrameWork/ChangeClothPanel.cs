using Assets.Scripts.BullFight.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeClothPanel : BasePanel {

    ///public Sprite image1;
  //public Sprite image2;
    //public Sprite image3;
    private Image background;
    //public Button closeBtn;
    int a = 0;
    private Toggle toggle1;
    private Toggle toggle2;
    private Toggle toggle3;
    private void Start() {
        background = GameObject.Find("BackGround").GetComponent<Image>();
        toggle1 = transform.FindChild("group/Toggle1").GetComponent<Toggle>();
        toggle2 = transform.FindChild("group/Toggle2").GetComponent<Toggle>();
        toggle3 = transform.FindChild("group/Toggle3").GetComponent<Toggle>();
        switch (PlayerPrefs.GetInt("cloth")) {
            case 1:
                //background.sprite = DynamicUIManager.Instance.BaseNameGetSprite("bg_3");
                toggle1.isOn = true;
                break;
            case 2:
                //background.sprite = DynamicUIManager.Instance.BaseNameGetSprite("bg_2");
                toggle2.isOn = true;
                break;
            case 3:
                //background.sprite = DynamicUIManager.Instance.BaseNameGetSprite("bg_1");
                toggle3.isOn = true;
                break;
            default:
                //background.sprite = DynamicUIManager.Instance.BaseNameGetSprite("bg_3");
                toggle1.isOn = true;
                break;
        }
        
        transform.FindChild("CloseButton").GetComponent<Button>().onClick.AddListener(delegate {
            DNUIManager.Instance.PopPanel();
        });
        toggle1.onValueChanged.AddListener((bool show)=> {
            if (show) {
                background.sprite = DynamicUIManager.Instance.BaseNameGetSprite("bg_3");
                PlayerPrefs.SetInt("cloth", 1);
            }
        });
        toggle2.onValueChanged.AddListener((bool show) => {

            if (show) {
                background.sprite = DynamicUIManager.Instance.BaseNameGetSprite("bg_2");
                PlayerPrefs.SetInt("cloth", 2);
            }
        });
        toggle3.onValueChanged.AddListener((bool show) => {

            if (show) {
                background.sprite = DynamicUIManager.Instance.BaseNameGetSprite("bg_1");
                PlayerPrefs.SetInt("cloth", 3);
            }
        });
        //if (StartGameController.TogleNum == 0) {
        //    Back1.isOn = true;
        //    Back2.isOn = false;
        //    Back3.isOn = false;
        //} else if (StartGameController.TogleNum == 1) {
        //    Back1.isOn = false;
        //    Back2.isOn = true;
        //    Back3.isOn = false;
        //} else {
        //    Back1.isOn = false;
        //    Back2.isOn = false;
        //    Back3.isOn = true;
        //}



    }
    //public void OnvalueImage1() {
    //    StartGameController.TogleNum = 0;
    //    background.sprite = image1;
    //}
    //public void OnvalueImage2() {
    //    StartGameController.TogleNum = 1;
    //    background.sprite = image2;
    //}
    //public void OnvalueImage3() {
    //    StartGameController.TogleNum = 2;
    //    background.sprite = image3;
    //}

}
