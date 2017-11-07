using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerInfoZJH : MonoBehaviour {
    public Image Avatar;//头像
    public Text userScore;//分数
    public Text userName;//名字
    public string userNames;//玩家名字
    public string avatar;//玩家头像
    public int index;//位置
    public int id;//ID
    public string ip;//IP
    public int score;//分数
   
	void Start () {
	
	}
	
	void Update () {
     
    }
    public void GetInfo(string name, string scores, int Id, string IP)
    {
        userNames = name;
        score = int.Parse(scores);
        id = Id;
        ip = IP;
        userName.text = userNames;
        userScore.text = score.ToString();
    }
    public void ChangedScore(int scores)
    {
        score += scores;
        userScore.text = score.ToString();
    }
    public int GetIndex()
    {
        return index;
    }
    public void OnClickBiPai()
    {
        if(MainSceneZJH.isBiPai && MainSceneZJH.Index != index)
        {
            com.guojin.mj.net.message.flower.CompareCardInfoZJH cci = com.guojin.mj.net.message.flower.CompareCardInfoZJH.Create(index);
            SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(cci));
        }
        else
        {
            //点击头像显示玩家信息
        }
    }
}
