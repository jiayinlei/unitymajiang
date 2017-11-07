using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitPanel : BasePanel {

	// Use this for initialization
	void Start () {
        transform.FindChild("OKButton").GetComponent<Button>().onClick.AddListener(()=> {
            transform.PlayButtonVoice();
            Application.Quit();
            //if (Application.platform == RuntimePlatform.WindowsEditor) {
            //    Application
            //}
        });
        transform.FindChild("CancelButton").GetComponent<Button>().onClick.AddListener(() => {
            transform.PlayButtonVoice();
            DNGlobalData.show = true;
            Destroy(gameObject);
            //DNUIManager.Instance.PopPanel();
        });
        transform.FindChild("CloseBtn").GetComponent<Button>().onClick.AddListener(() => {
            transform.PlayButtonVoice();
            DNGlobalData.show = true;
            Destroy(gameObject);
        });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
