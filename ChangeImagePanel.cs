using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImagePanel : MonoBehaviour {

    public Sprite image1;
    public Sprite image2;
    public Sprite image3;
    private Image background;
    public Button closeBtn;
    int a = 0;
    public Toggle Back1;
    public Toggle Back2;
    public Toggle Back3;
    private void Start()
    {
        background = GameObject.Find("ChoicePlayeWnd_Img").GetComponent<Image>();
        closeBtn.onClick.AddListener(delegate {
            VoiceManger.Instance.BtnVoice();
            Destroy(gameObject);
        });
        changTogle();
    }
    public void OnvalueImage1()
    {
        VoiceManger.Instance.BtnVoice();
        StartGameController.TogleNum =0;
        background.sprite = image1;   
    }
    public void OnvalueImage2()
    {
        VoiceManger.Instance.BtnVoice();
        VoiceManger.Instance.BtnVoice();
        StartGameController.TogleNum = 1;
        background.sprite = image2;     
    }
    public void OnvalueImage3()
    {
        VoiceManger.Instance.BtnVoice();
        StartGameController.TogleNum = 2;
        background.sprite = image3;     
    }
    public void OnvalueImagebtn1()
    {      
        StartGameController.TogleNum = 0;
        background.sprite = image1;
        changTogle();
    }
    public void OnvalueImagebtn2()
    {       
        StartGameController.TogleNum = 1;
        background.sprite = image2;
        changTogle();
    }
    public void OnvalueImagebtn3()
    {       
        StartGameController.TogleNum = 2;
        background.sprite = image3;
        changTogle();
    }
    public void changTogle()
    {
        VoiceManger.Instance.BtnVoice();
        if (StartGameController.TogleNum == 0)
        {
            Back1.isOn = true;
            Back2.isOn = false;
            Back3.isOn = false;
        }
        else if (StartGameController.TogleNum == 1)
        {
            Back1.isOn = false;
            Back2.isOn = true;
            Back3.isOn = false;
        }
        else
        {
            Back1.isOn = false;
            Back2.isOn = false;
            Back3.isOn = true;
        }
    }

}
