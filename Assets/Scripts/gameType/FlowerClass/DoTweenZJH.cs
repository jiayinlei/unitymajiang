using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoTweenZJH : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnStep(int index)
    {
        PlayGameZJH.Instance.CopyShoupai(index);
    }
    public void OnComplete()
    {
        Destroy(this.gameObject);
    }
}
