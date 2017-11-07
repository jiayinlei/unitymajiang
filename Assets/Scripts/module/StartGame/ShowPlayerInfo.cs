using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerInfo : MonoBehaviour {

    public RawImage touxiang;
    public Transform penggangP;
    public Transform shoupaiP;
    public Image zhuang;
    public Text username;
    public Image typewin;
    public Text Soc;
    public Text userid;

    public void ShowShouPai(com.guojin.mj.net.message.game.GameFanResult GF,int weizhi)
    {
        touxiang.texture = GameObject.Find("HeadPhoto"+weizhi).GetComponentInChildren<RawImage>().texture;
        if (weizhi==Method.zhaungweizhi)
        {
            zhuang.gameObject.SetActive(true);
        }
        username.text = GF.userName;
        Soc.text = (GF.score).ToString();
        if (GF.peng!=null)
        {
            for (int i = 0; i < GF.peng.Length; i++)
            {
                CloneToParent("pengjieshu", penggangP,GF.peng[i]);
            }
        }
        if (GF.anGang!=null)
        {
            for (int i = 0; i < GF.anGang.Length; i++)
            {
                CloneToParent("AnGangMajongjieshu", penggangP,GF.anGang[i]);
            }
        }
        if (GF.daMingGang!=null)
        {
            for (int i = 0; i < GF.daMingGang.Length; i++)
            {
                CloneToParent("GangMajongjieshu", penggangP, GF.daMingGang[i]);
            }
        }
        if (GF.xiaoMingGang != null)
        {
            for (int i = 0; i < GF.xiaoMingGang.Length; i++)
            {
                CloneToParent("GangMajongjieshu", penggangP, GF.xiaoMingGang[i]);
            }
        }
        List<int> intlist = new List<int>();
        for (int i = 0; i < GF.shouPai.Length; i++)
        {
            intlist.Add(GF.shouPai[i]);
            Debug.Log("+++++++++++++" + GF.shouPai[i]);
        }
        Debug.Log("+++++++++++++" + GF.shouPai.Length);       
        intlist.Sort();
        for (int i =0; i < intlist.Count; i++)
        {
            GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/OutPaijieshu"));
            temp.transform.SetParent(shoupaiP);
            temp.transform.localScale = Vector3.one;
            temp.transform.localPosition = Vector3.zero;
            temp.GetComponentInChildren<MaJongOutInfo>().IsHun(intlist[i],Method.MjNameSp[intlist[i]]);
        }
    }
    public void CloneToParent(string name,Transform parent ,int id)
    {
        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/"+name));
        temp.transform.SetParent(parent);
        temp.transform.localScale = Vector3.one;
        temp.transform.localPosition = Vector3.zero;     
        if (name!= "AnGangMajongjieshu")
        {
            MaJongOutInfo[] mjArr = temp.GetComponentsInChildren<MaJongOutInfo>();
            for (int i = 0; i < mjArr.Length; i++)
            {
                mjArr[i].IsHun(id, Method.MjNameSp[id]);
            }
        }
        //todo jia 2017/7/26 显示暗杠
        else
        {
            MaJongOutInfo mjArr = temp.GetComponentInChildren<MaJongOutInfo>();
            mjArr.IsHun(id, Method.MjNameSp[id]);
        }//over   
            
    }
    public void CreatGameRoomEnd(int weizhi, string soc,bool isfirst)
    {
        GameObject temp = GameObject.Find("HeadPhoto" + weizhi);
        touxiang.texture = temp.GetComponentInChildren<RawImage>().texture;
        username.text = temp.GetComponent<PlayerInfo>().userName.text;
        userid.text ="ID:"+ temp.GetComponent<PlayerInfo>().userID.text;

        if (int.Parse(soc) >= 0)
        {
            Soc.color = Color.red;
        }
        else
        {
            Soc.color = Color.green;
        }
        Soc.text = soc;
        if (isfirst)
        {
            zhuang.gameObject.SetActive(true);
        }
    }

}
