using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingPanel : BasePanel {
    bool isMusicOn = true;
    bool isAudioOn = true;
    Image musicImage;
    Image audioImage;
    AudioSource AS;
	// Use this for initialization
	void Start () {
        AS = GameObject.Find("StaticSource").GetComponent<AudioSource>();
        musicImage = transform.FindChild("yinyue/kaiguan").GetComponent<Image>();
        audioImage = transform.FindChild("yinxiao/kaiguan").GetComponent<Image>();
        musicImage.GetComponent<Button>().onClick.AddListener(()=> {
            SetGameMusicBtnClick();

        });
        audioImage.GetComponent<Button>().onClick.AddListener(() => {
            SetAudioBtnClick();
        });
        transform.FindChild("CloseButton").GetComponent<Button>().onClick.AddListener(delegate {
            DNUIManager.Instance.PopPanel();
        });
        if (PlayerPrefs.GetInt("MusicSet") == 0) {
            isMusicOn = false;
            musicImage.sprite = Resources.Load<Sprite>("Pictures/Close");
        } else  {
            isMusicOn = true;
            musicImage.sprite = Resources.Load<Sprite>("Pictures/Open");
        }
        if (PlayerPrefs.GetInt("AudioSet") == 0) {
            isAudioOn = false;
            audioImage.sprite = Resources.Load<Sprite>("Pictures/Close");
        } else {
            isAudioOn = true;
            audioImage.sprite = Resources.Load<Sprite>("Pictures/Open");
        }
    }

    void SetGameMusicBtnClick() {
        //Debug.Log("按下："+isMusicOn);
        if (isMusicOn) {
            musicImage.sprite = Resources.Load<Sprite>("Pictures/Close");
            isMusicOn = false;
            AS.volume = 0f;
            PlayerPrefs.SetInt("MusicSet", 0);
            PlayerPrefs.Save();
        } else {
            musicImage.sprite = Resources.Load<Sprite>("Pictures/Open");
            isMusicOn = true;
            AS.volume = 1f;
            PlayerPrefs.SetInt("MusicSet", 1);
            PlayerPrefs.Save();
        }
    }
    void SetAudioBtnClick() {
        if (isAudioOn) {
            audioImage.sprite = Resources.Load<Sprite>("Pictures/Close");
            isAudioOn = false;
            PlayerPrefs.SetInt("AudioSet", 0);
            PlayerPrefs.Save();
        } else {
            audioImage.sprite = Resources.Load<Sprite>("Pictures/Open");
            isAudioOn = true;
            PlayerPrefs.SetInt("AudioSet", 1);
            PlayerPrefs.Save();
        }
    }
}
