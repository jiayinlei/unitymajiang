using UnityEngine;
using System.Collections;

public class Momo : MonoBehaviour {

	protected virtual void Awake(){
		InitPanelCompotent ();
		InitPanelData ();
	}

	protected virtual void InitPanelCompotent(){
		Debug.Log ("moo InitPanelCompotent");
	}

	protected virtual void InitPanelData(){
		Debug.Log ("moo InitPanelData");
	}

}
