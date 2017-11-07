using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ChangeNoticeOthers : MonoBehaviour {
    Text userName;
    GameObject tra;
   // Image niuImage;
    Text niuText;
    private void Awake() {
        userName = transform.FindChild("UserName").GetComponent<Text>();
        niuText = transform.FindChild("NiuText").GetComponent<Text>();
        transform.FindChild("Text").GetComponent<Text>().text="，需 要 庄 喽 ！";
        tra = gameObject;
    }
    private void OnEnable() {
        userName.GetComponent<Text>().text = "“" + DNGlobalData.changeUserName + " ” 拿 到";
        //niuImage.sprite = DNGlobalData.changeNiuType;
        niuText.text = DNGlobalData.changeNiuTypeStr;
        Invoke("CloseGameobject", 2);
    }
    void CloseGameobject() {
        CancelInvoke("CloseGameobject");
        gameObject.SetActive(false);
    }
}
