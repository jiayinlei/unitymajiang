using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetAudio : EventManager {

    bool isMusicOn = true;
    bool isAudioOn = true;
    bool isLanguageOn = true;
    AudioSource AS;
    public override void InformationSetting() {
        if (PlayerPrefs.GetInt("MusicSet", 1) == 1) {
            isMusicOn = true;
            //image.sprite = Resources.Load<Sprite>(imagePath);
            this.BindingSource[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("NewUIPicture/oppen");
        } else {
            isMusicOn = false;
            this.BindingSource[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("NewUIPicture/guan");
        }
        if (PlayerPrefs.GetInt("AudioSet", 1) == 1) {
            isAudioOn = true;
            this.BindingSource[1].GetComponent<Image>().sprite = Resources.Load<Sprite>("NewUIPicture/oppen");
        } else {
            isAudioOn = false;
            this.BindingSource[1].GetComponent<Image>().sprite = Resources.Load<Sprite>("NewUIPicture/guan");
        }
        if (PlayerPrefs.GetInt("LanguageSet", 1) == 1) {
            isLanguageOn = true;
            this.BindingSource[2].GetComponent<Image>().sprite = Resources.Load<Sprite>("NewUIPicture/oppen");
        } else {
            isLanguageOn = false;
            this.BindingSource[2].GetComponent<Image>().sprite = Resources.Load<Sprite>("NewUIPicture/guan");
        }
    }
    //    static public bool IsMusicOn {
    //        get {
    //            return PlayerPrefs.GetInt("MusicSet", 1) == 1;
    //        }
    //    }

    //    static public bool IsAudioOn {
    //        get {
    //            return PlayerPrefs.GetInt("AudioSet", 1) == 1;
    //        }
    //    }

    //    // Use this for initialization
    //    void Start() {

    //        //AS = GameObject.Find("StaticSource").GetComponent<AudioSource>();
    //        this.Open();
    //        this.InformationSetting();
    //    }
    //    void SetGameMusicBtnClick() {
    //        if (isMusicOn) {
    //            this.BindingSource[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("NewUIPicture/guan");
    //            isMusicOn = false;
    //            AS.volume = 0f;
    //            PlayerPrefs.SetInt("MusicSet", 0);
    //            PlayerPrefs.Save();
    //        } else {
    //            this.BindingSource[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("NewUIPicture/oppen");
    //            isMusicOn = true;
    //            AS.volume = 1f;
    //            PlayerPrefs.SetInt("MusicSet", 1);
    //            PlayerPrefs.Save();
    //        }
    //    }
    //    void SetAudioBtnClick() {
    //        if (isAudioOn) {
    //            this.BindingSource[1].GetComponent<Image>().sprite = Resources.Load<Sprite>("NewUIPicture/guan");
    //            isAudioOn = false;
    //            PlayerPrefs.SetInt("AudioSet", 0);
    //            PlayerPrefs.Save();
    //        } else {
    //            this.BindingSource[1].GetComponent<Image>().sprite = Resources.Load<Sprite>("NewUIPicture/oppen");
    //            isAudioOn = true;
    //            PlayerPrefs.SetInt("AudioSet", 1);
    //            PlayerPrefs.Save();
    //        }
    //    }
    //    void SetlanguageBtnClick() {
    //        if (isAudioOn) {
    //            this.BindingSource[2].GetComponent<Image>().sprite = Resources.Load<Sprite>("NewUIPicture/guan");
    //            isAudioOn = false;
    //            PlayerPrefs.SetInt("LanguageSet", 0);
    //            PlayerPrefs.Save();
    //        } else {
    //            this.BindingSource[2].GetComponent<Image>().sprite = Resources.Load<Sprite>("NewUIPicture/oppen");
    //            isAudioOn = true;
    //            PlayerPrefs.SetInt("LanguageSet", 1);
    //            PlayerPrefs.Save();
    //        }
    //    }
   public void ClosePage() {
    Destroy(this.gameObject);
}
    //    // Update is called once per frame

}
