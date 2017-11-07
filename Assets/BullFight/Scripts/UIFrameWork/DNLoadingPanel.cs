using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DNLoadingPanel : MonoBehaviour {
    private static DNLoadingPanel instance;

    private uint _nowprocess;
    public static DNLoadingPanel Instance {
        get {
            if (instance == null) {
                instance = new DNLoadingPanel();
            }
            return instance;
        }
    }


    private static string loadSceneName;
    public static string LoadSceneName {
        get {
            return loadSceneName;
        }

        set {
            loadSceneName = value;
        }
    }
    private Slider slider;
    AsyncOperation asy;
    // Use this for initialization
    void Start () {
        DNUIManager.Instance.ClearStack();
        DNUIManager.Instance.ClearPanelDict();
        slider = transform.FindChild("Slider").GetComponent<Slider>();
        StartCoroutine(LoadIEnume());
        //switch (LoadSceneName) {
        //    case "GameHall":
        //        UIManager.ChangeUI(UIManager.PageState.GameHall, (GameObject obj) =>
        //        {
        //            obj.GetComponent<NoticePageEvent>().InformationSetting();
        //        });
        //        break;
        //}
	}
    IEnumerator LoadIEnume() {

        asy= SceneManager.LoadSceneAsync(LoadSceneName);
        asy.allowSceneActivation = false;

        yield return asy;
        
    }
	// Update is called once per frame
	void Update () {
        uint toProcess;
        //Debug.Log(asy.progress * 100);
        if (asy.progress < 0.9f)//坑爹的progress，最多到0.9f
        {
            toProcess = (uint)(asy.progress * 100);
        } else {
            toProcess = 100;
        }

        if (_nowprocess < toProcess) {
            _nowprocess++;
        }

        slider.value = _nowprocess / 100f;

        if (_nowprocess == 100)//async.isDone应该是在场景被激活时才为true
        {
            asy.allowSceneActivation = true;
        }
        //slider.value = asy.progress;
        //if (slider.value >= 0.8f) {

        //    asy.allowSceneActivation = true;
        //}
    }
}
