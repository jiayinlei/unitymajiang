using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Begin : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Tweener tw= transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 1);
        tw.OnComplete(()=> {
           Tweener te=  transform.DOScale(Vector3.zero, 0.5f);
            te.OnComplete(()=> {
                transform.localScale = Vector3.one;
                Destroy(gameObject);
            });
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
