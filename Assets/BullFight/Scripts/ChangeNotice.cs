using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeNotice : MonoBehaviour {
    //Image niuImage;
    Text niuText;
    private void Awake() {

        niuText = transform.FindChild("NiuText").GetComponent<Text>();

    }

    private void OnEnable() {
        //niuImage.sprite = DNGlobalData.changeNiuType;
        niuText.text = DNGlobalData.changeNiuTypeStr;
        Invoke("CloseGameobject",2);
    }
    void CloseGameobject() {
        CancelInvoke("CloseGameobject");
        gameObject.SetActive(false);
    }
}
