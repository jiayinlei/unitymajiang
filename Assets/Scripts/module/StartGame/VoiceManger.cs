using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VoiceManger : MonoBehaviour
{
    private static VoiceManger instance;
    
    public static VoiceManger Instance { get { return instance; } }

    public GameObject Guangbiao { get { return guangbiao; } }
   

    AudioClip[] acArrFangYan = new AudioClip[37];
    AudioClip[] acArrPuTong = new AudioClip[37];
    GameObject guangbiao;
    GameObject positionstar;
    public int timeNum;
    Transform cam;
    void Awake()
    {
        instance = this;
        acArrFangYan = Method.LoadVoice();
        acArrPuTong = Method.LoadPuTongVoice();
        cam = GameObject.Find("Camera").transform;
        guangbiao = Instantiate(Resources.Load<GameObject>("MjPrefab/guangbiao"));
       
    }
    public void OnLoadGuangBiao()
    {
        guangbiao = Instantiate(Resources.Load<GameObject>("MjPrefab/guangbiao"));
    }

    public void LoadMusiceByName(int id)
    {
        if (PlayerPrefs.GetFloat("AudioSet",1)>=0.1)
        {
            AudioClip ac;
            if (PlayerPrefs.GetInt("LanguageSet", 1) ==1)
            {
                Method.isPutong = false;
            }
            else
            {
                Method.isPutong = true;
            }
            
            if (Method.isPutong)
            {
                ac = acArrPuTong[id];
            }
            else
            {
                ac = acArrFangYan[id];
            }
            AudioSource.PlayClipAtPoint(ac, cam.position, PlayerPrefs.GetFloat("AudioSet", 1));
        } 
    }
    public AudioClip ac;
    public void BtnVoice()
    {
        if (PlayerPrefs.GetFloat("AudioSet", 1) >= 0.1f)
        {   
            AudioSource.PlayClipAtPoint(ac, cam.position);
        }
    }
    /// <summary>
    /// 听牌的操作
    /// </summary>
    /// <param name="intarr"></param>
    /// <param name="weizhi"></param>
    public void TingMaJong(List<int> intarr)
    {
        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/TingMaJong"));
        temp.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
        temp.transform.localScale = Vector3.one;
        temp.transform.localPosition = new Vector3(-66, -360, 0);
        Method.TingMaJong = temp;
        if (intarr.Count>=9)
        {
            for (int i = 0; i < 9; i++)
            {
                GameObject temp1 = Instantiate(Resources.Load<GameObject>("MjPrefab/Tingpai"));
                temp1.transform.SetParent(temp.transform);
                temp1.GetComponent<MaJongOutInfo>().IsHun(intarr[i], Method.MjNameSp[intarr[i]]);
                temp1.GetComponent<MaJongOutInfo>().ShowZhang(Method.ShowSurplusMaJong(intarr[i]));
                temp1.transform.localScale = Vector3.one;
                temp1.transform.localPosition = Vector3.zero;
                Debug.Log("听的牌"+ intarr[i]+"剩余"+Method.ShowSurplusMaJong(intarr[i])+"张");
            }
        }
        else
        {
            for (int i = 0; i < intarr.Count; i++)
            {
                GameObject temp1 = Instantiate(Resources.Load<GameObject>("MjPrefab/Tingpai"));
                temp1.transform.SetParent(temp.transform);
                temp1.GetComponent<MaJongOutInfo>().IsHun(intarr[i], Method.MjNameSp[intarr[i]]);
                temp1.GetComponent<MaJongOutInfo>().ShowZhang(Method.ShowSurplusMaJong(intarr[i]));
                temp1.transform.localScale = Vector3.one;
                temp1.transform.localPosition = Vector3.zero;
                Debug.Log("听的牌" + intarr[i] + "剩余" + Method.ShowSurplusMaJong(intarr[i]) + "张");
            }
        }
      
        
    }

}
