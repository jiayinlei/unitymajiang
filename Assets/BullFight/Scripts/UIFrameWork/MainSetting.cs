using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSetting : EventManager {

    bool isMusicOn = true;
    bool isAudioOn = true;

    float fMusic = 1f;
    float fAudio = 1f;
    bool isLanguageOn = true;
    AudioSource AS;
    ///// <summary>
    ///// index==0,背景音
    ///// index==1,音效
    ///// </summary>
    //AudioSource[] audios;
    public override void InformationSetting() {
        fMusic = PlayerPrefs.GetFloat("MusicSet", 1f);
        this.BindingSource[0].GetComponent<Slider>().value = fMusic;
        fAudio = PlayerPrefs.GetFloat("AudioSet", 1f);
        //AudioSource.PlayClipAtPoint()
        this.BindingSource[1].GetComponent<Slider>().value = fAudio;
    }
    public void vc_0() {
        fMusic = this.BindingSource[0].GetComponent<Slider>().value;
        if (AS) {
            AS.volume = fMusic;
        }

        PlayerPrefs.SetFloat("MusicSet", fMusic);
        PlayerPrefs.Save();
    }
    public void vc_1() {
        fAudio = this.BindingSource[1].GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("AudioSet", fAudio);
        PlayerPrefs.Save();
    }
    static public bool IsMusicOn {
        get {
            return PlayerPrefs.GetFloat("MusicSet", 1) != 0f;
        }
    }

    static public bool IsAudioOn {
        get {
            return PlayerPrefs.GetFloat("AudioSet", 1) != 0f;
        }
    }

    void Start() {
        //audios = GameObject.Find("StaticSource").GetComponents<AudioSource>();
        AS = GameObject.Find("StaticSource").GetComponent<AudioSource>();
        this.Open();
        InformationSetting();
    }
    void GameMusicBtnDelClick() {
        if (fMusic <= 0.1f) {
            this.BindingSource[0].GetComponent<Slider>().value = 0f;
            fMusic = 0f;
            AS.volume = 0f;

        } else {
            fMusic -= 0.1f;
            this.BindingSource[0].GetComponent<Slider>().value = fMusic;
            AS.volume = fMusic;
        }
        PlayerPrefs.SetFloat("MusicSet", fMusic);
        PlayerPrefs.Save();
    }

    void GameMusicBtnAddClick() {
        if (fMusic >= 0.9f) {
            this.BindingSource[0].GetComponent<Slider>().value = 1f;
            fMusic = 1f;
            AS.volume = 1f;
        } else {
            fMusic += 0.1f;
            this.BindingSource[0].GetComponent<Slider>().value = fMusic;
            AS.volume = fMusic;
        }
        PlayerPrefs.SetFloat("MusicSet", fMusic);
        PlayerPrefs.Save();
    }
    void GameAudioBtnDelClick() {
        if (fAudio <= 0.1f) {
            this.BindingSource[1].GetComponent<Slider>().value = 0f;
            fAudio = 0f;
        } else {
            fAudio -= 0.1f;
            this.BindingSource[1].GetComponent<Slider>().value = fAudio;
        }
        PlayerPrefs.SetFloat("AudioSet", fAudio);
        PlayerPrefs.Save();
    }

    void GameAudioBtnAddClick() {
        if (fAudio >= 0.9f) {
            this.BindingSource[1].GetComponent<Slider>().value = 1f;
            fAudio = 1f;
        } else {
            fAudio += 0.1f;
            this.BindingSource[1].GetComponent<Slider>().value = fAudio;
        }
        PlayerPrefs.SetFloat("AudioSet", fAudio);
        PlayerPrefs.Save();
    }
    void OpenGps() {
#if UNITY_ANDROID && !UNITY_EDITOR

        LoginAndShare.Controller.GoToProcessView();

#elif UNITY_IPHONE

#endif

    }
    //void SetGameMusicBtnClick()
    //{
    //    if (isMusicOn)
    //    {
    //        this.BindingSource[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("NewUIPicture/guan");
    //        isMusicOn = false;
    //        AS.volume = 0f;
    //        PlayerPrefs.SetInt("MusicSet", 0);
    //        PlayerPrefs.Save();
    //    }
    //    else
    //    {
    //        this.BindingSource[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("NewUIPicture/oppen");
    //        isMusicOn = true;
    //        AS.volume = 1f;
    //        PlayerPrefs.SetInt("MusicSet", 1);
    //        PlayerPrefs.Save();
    //    }
    //}
    //void SetAudioBtnClick()
    //{
    //    if (isAudioOn)
    //    {
    //        this.BindingSource[1].GetComponent<Image>().sprite = Resources.Load<Sprite>("NewUIPicture/guan");
    //        isAudioOn = false;
    //        PlayerPrefs.SetInt("AudioSet", 0);
    //        PlayerPrefs.Save();
    //    }
    //    else
    //    {
    //        this.BindingSource[1].GetComponent<Image>().sprite = Resources.Load<Sprite>("NewUIPicture/oppen");
    //        isAudioOn = true;
    //        PlayerPrefs.SetInt("AudioSet", 1);
    //        PlayerPrefs.Save();
    //    }
    //}
    //void SetlanguageBtnClick() {
    //    if (isLanguageOn) {
    //        this.BindingSource[2].GetComponent<Image>().sprite = PPTextureManage.getInstance().LoadAtlasSprite("Atlas/GameHall", "Close");
    //        isLanguageOn = false;
    //        PlayerPrefs.SetInt("LanguageSet", 0);
    //        PlayerPrefs.Save();
    //    } else {
    //        this.BindingSource[2].GetComponent<Image>().sprite = PPTextureManage.getInstance().LoadAtlasSprite("Atlas/GameHall", "open");
    //        isLanguageOn = true;
    //        PlayerPrefs.SetInt("LanguageSet", 1);
    //        PlayerPrefs.Save();
    //    }
    //}
    void ClosePage() {
        Destroy(this.gameObject);
    }
    // Update is called once per frame

}
