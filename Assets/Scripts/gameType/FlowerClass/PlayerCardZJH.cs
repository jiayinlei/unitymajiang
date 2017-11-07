using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCardZJH : MonoBehaviour {

    private static PlayerCardZJH instance;
    //public static List<CardTypeZJH> cardArr = new List<CardTypeZJH>();

    public static PlayerCardZJH Instance
    {
        get
        {
            return new PlayerCardZJH();
        }
    }

    void Start () {
	
	}
	
	void Update () {
	
	}
}
