using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnAudioMgr : MonoBehaviour {
    public AudioClip ac;
    public void BtnVoice()
    {
        if (PlayerPrefs.GetFloat("AudioSet", 1) >= 0.1f)
        {
            AudioSource.PlayClipAtPoint(ac, Vector3.zero);
        }

    }
    void Start () {
        Button btn = this.GetComponent<Button>();
        if (btn)
        {
            btn.onClick.AddListener(delegate () {
                BtnVoice();
            });
        }
    }
	
}
