using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerHeadOnClick : MonoBehaviour {

    public Text playerName;
    public Text playerSex;
    public Text playerId;
    public Text playerIp;
    public Text [] distance;
    public RawImage avt;
    public GameObject[] zui;

    public void GetInfo(string name, int sex, int userId, string ip, Texture av,List<string> ListStr, List<string> listname)
    {
        playerName.text = "玩家姓名："+name;
        if(sex == 0 || sex == 1)
        {
            playerSex.text = (sex == 0) ? "性别：女" : "性别：男";
        }
        else
        {
            playerSex.text = "性别：隐藏";
        }
        playerId.text = "ID：" + userId.ToString();
        playerIp.text = "玩家IP：" + ip.ToString();
        avt.texture = av;
        for (int i = 0; i < ListStr.Count; i++)
        {       
            if ((ListStr[i] == null||ListStr[i] == "")&&(listname[i]!=""))
            {               
                distance[i].text = "距离玩家" + listname[i] + "：未知";
            }
            else if ((ListStr[i]!=null|| ListStr[i] != "")&& listname[i] != "")
            {
                distance[i].text = "距离玩家" + listname[i] + "：" + ListStr[i];
            }            
        }
    }
   
    public void GetInfoGuShi(string name, int sex, int userId, Texture av,int index,int[]arr)
    {
        playerName.text = "玩家姓名：" + name;
        if (sex == 0 || sex == 1)
        {
            playerSex.text = (sex == 0) ? "性别：女" : "性别：男";
        }
        else
        {
            playerSex.text = "性别：隐藏";
        }
        playerId.text = "ID：" + userId.ToString();
        avt.texture = av;
        if (arr[0]!=-1)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                zui[i].SetActive(true);
                zui[i].GetComponent<ChoiceZui>().ShowZui(arr[i]);
            }
        }
       
    }
    public void OnClosePanel()
    {
        PlayerInfo.isCopy = true;
        Destroy(gameObject);
    }
}
