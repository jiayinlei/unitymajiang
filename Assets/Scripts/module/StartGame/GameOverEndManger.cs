using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverEndManger : MonoBehaviour {

    public GridLayoutGroup GG;
    public Text timeSystem;//系统时间
    public Text roomid;
    public Text junum;
    public Text shijian;
    public List<int> score;
    public Transform parent;
    public void InitGrid()
    {
        if (Method.PeopleNum == 3)
        {
            GG.spacing = new Vector2(10, 60);

        }
        else
        {
            GG.spacing = new Vector2(10, 10);
        }
    }
    void showtext(int minutime)
    {
        timeSystem.text = System.DateTime.Now.ToString();
        roomid.text = "房号："+Method.GameRoomID;
        junum.text = "局数："+PlayerData.jutype;

        Debug.Log("++++++++++++++时间"+minutime+"++++++++++++++++");
        if (minutime>=60)
        {
            int hour = minutime / 60;
            int minu = minutime - hour * 60;
            shijian.text = hour + "小时" + minu + "分钟";
        }
        else
        {
            shijian.text = minutime + "分钟";
        }
        
    }

    public void ShowEndInfo(com.guojin.mj.net.message.game.StaticsResultRet srt)
    {
        InitGrid();
        score = new List<int>();     
        if (Method.PeopleNum == 3)
        {
            score.Add(srt.score0);
            score.Add(srt.score1);
            score.Add(srt.score2);           
        }
        else
        {
            score.Add(srt.score0);
            score.Add(srt.score1);
            score.Add(srt.score2);
            score.Add(srt.score3);
        }
        int a = GetMaxNum(score);      
        showtext(srt.minutime);
        for (int i = 0; i < score.Count; i++)
        {
            GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/touxiangend"));
            temp.name = "touxiangend" + i.ToString();
            Debug.Log(score[i]);
            temp.transform.SetParent(parent);
            temp.transform.localScale = Vector3.one;
            temp.transform.localPosition = Vector3.zero;          
            if (score[i]== a)
            {
                temp.GetComponent<ShowPlayerInfo>().CreatGameRoomEnd(i, score[i].ToString(),true);
            }
            else
            {
                temp.GetComponent<ShowPlayerInfo>().CreatGameRoomEnd(i, score[i].ToString(), false);
            }          

        }

    }
    public void ShareBTN()
    {
        LoginAndShare.Controller.sharePng();

    }
    public int GetMaxNum(List<int> list)
    {
        int a = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (a< list[i])
            {
                a = list[i];
            }
        }
        return a;
    }
    public void OverBTN()
    {
        Method.isChongLian = false;
        Method.LoadMainCity();
    }
    public void CloseBTN()
    {
        Destroy(gameObject);
    }

}
