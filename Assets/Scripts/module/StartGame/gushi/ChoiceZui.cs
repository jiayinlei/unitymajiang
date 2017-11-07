using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceZui : MonoBehaviour {

    public int num;
    bool isChoice=true;
    public Image choiceImage; 
    public bool canGet;
    public void OnClickChoice()
    {
        if (isChoice)
        {

            isChoice = false;
            choiceImage.gameObject.SetActive(true);
            canGet = true;
            Debug.Log(canGet);
        }
        else
        {
            isChoice = true;
            choiceImage.gameObject.SetActive(false);
            canGet = false;
            Debug.Log(canGet);
        }
    }
	
    public void CreateOne(int num1)
    {
        num = num1;       
    }

    public void ShowZui(int zuiID)
    {
        num = zuiID;
        choiceImage.sprite = Resources.Load<Sprite>("NewUIPicture/gushi/"+zuiID);
    }
}
