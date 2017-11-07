using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RecordPageMgr : MonoBehaviour
{
    private static RecordPageMgr _instance = null;
    private RecordPageMgr() { }
    public static RecordPageMgr GetInstance() {
        if (_instance == null) {
            _instance = new RecordPageMgr();
        }
        return _instance;
    }

    private Transform volume1;
    private Transform volume2;
    private Transform volume3;
    private Text text;
    private float index = 15;

  
    // Use this for initialization
    void Start ()
	{
        if (SceneManager.GetActiveScene().name.Equals("GameHall"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);

        }
        else
        {
            volume1 = transform.GetChild(1).GetChild(1);
            volume2 = transform.GetChild(1).GetChild(2);
            volume3 = transform.GetChild(1).GetChild(3);
            text = transform.GetChild(1).GetChild(4).GetComponent<Text>();
            StartCoroutine(ShowVolumeChange());
            StartCoroutine(RecordTime());
        }

	}

    IEnumerator ShowVolumeChange()
    {
        for (int i = 0; i < 8; i++)
        {
            volume1.gameObject.SetActive(false);
            volume2.gameObject.SetActive(false);
            volume3.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            volume1.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            volume2.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            volume3.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator RecordTime()
    {
        for (int i = 0; i <= 15; i++)
        {
            text.text = "录音还剩" + index.ToString() + "秒";
            yield return new WaitForSeconds(1f);
            index--;
        }
    }

    void Update () {		
	}

}
