using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPageEvent : EventManager {
    private string[] stringArr = { ". ", ". .", ". . ." };
    public override void InformationSetting()
    {
        StartCoroutine(PlayText(0));
    }
    IEnumerator PlayText(int index)
    {
        SetLable(this.BindingSource[0], stringArr[index]);
        yield return new WaitForSeconds(0.5f);
        index++;
        if (index >= stringArr.Length) index = 0;
        StartCoroutine(PlayText(index));
    }
}
