using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForbiddenExitPanel : BasePanel {

	// Use this for initialization
	void Start () {

        transform.FindChild("SureButton").GetComponent<Button>().onClick.AddListener(delegate {
            DNUIManager.Instance.PopPanel();
        });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
