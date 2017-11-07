using System;
using com.guojin.mj.net.message.login;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.guojin.mj.net.message;
using UnityEngine.SceneManagement;
using com.guojin.core.io.message;
using com.guojin.mj.net.message.game;
using System.Collections.Generic;

public class GameResult : MonoBehaviour {

    public Text[] playerName;
    public Text[] playerID;
    public Text[] ziMo;
    public Text[] jiePiao;
    public Text[] dianPao;
    public Text[] anGang;
    public Text[] mingGang;
    public Text[] zongChengJi;
    public RawImage[] touxiang;
    public GameObject[] daYingJia;  

 
    /// <summary>
    /// 显示总结算界面
    /// </summary>
    /// <param name="srt"></param>
    public void GetPlayerResult(com.guojin.mj.net.message.game.StaticsResultRet srt)
    {
        if (Method.PeopleNum==3)
        {
            touxiang[3].gameObject.SetActive(false);
            ShowPlayerInfo(srt.locationIndex0, srt.zimo0, srt.jiepao0, srt.fangpao0, srt.angang0, srt.minggang0, srt.score0);
            ShowPlayerInfo(srt.locationIndex1, srt.zimo1, srt.jiepao1, srt.fangpao1, srt.angang1, srt.minggang1, srt.score1);
            ShowPlayerInfo(srt.locationIndex2, srt.zimo2, srt.jiepao2, srt.fangpao2, srt.angang2, srt.minggang2, srt.score2);
            for (int i = 0; i < 3; i++)
            {
                ShowNameAndId(PlayerInfo.playerIndex[i], PlayerInfo.playerName[i], PlayerInfo.playerID[i]);
            }
            if (srt.score0 >= srt.score1 && srt.score0 >= srt.score2 && srt.score0 >= srt.score3)
            {
                daYingJia[0].SetActive(true);
            }
            else if (srt.score1 >= srt.score0 && srt.score1 >= srt.score2 && srt.score1 >= srt.score3)
            {
                daYingJia[1].SetActive(true);
            }
            else if (srt.score2 >= srt.score0 && srt.score2 >= srt.score1 && srt.score2 >= srt.score3)
            {
                daYingJia[2].SetActive(true);
            }

        }
        else
        {
            ShowPlayerInfo(srt.locationIndex0, srt.zimo0, srt.jiepao0, srt.fangpao0, srt.angang0, srt.minggang0, srt.score0);
            ShowPlayerInfo(srt.locationIndex1, srt.zimo1, srt.jiepao1, srt.fangpao1, srt.angang1, srt.minggang1, srt.score1);
            ShowPlayerInfo(srt.locationIndex2, srt.zimo2, srt.jiepao2, srt.fangpao2, srt.angang2, srt.minggang2, srt.score2);
            ShowPlayerInfo(srt.locationIndex3, srt.zimo3, srt.jiepao3, srt.fangpao3, srt.angang3, srt.minggang3, srt.score3);
            for (int i = 0; i < PlayerInfo.playerIndex.Count; i++)
            {
                ShowNameAndId(PlayerInfo.playerIndex[i], PlayerInfo.playerName[i], PlayerInfo.playerID[i]);
            }
            if (srt.score0 >= srt.score1 && srt.score0 >= srt.score2 && srt.score0 >= srt.score3)
            {
                daYingJia[0].SetActive(true);
            }
            else if (srt.score1 >= srt.score0 && srt.score1 >= srt.score2 && srt.score1 >= srt.score3)
            {
                daYingJia[1].SetActive(true);
            }
            else if (srt.score2 >= srt.score0 && srt.score2 >= srt.score1 && srt.score2 >= srt.score3)
            {
                daYingJia[2].SetActive(true);
            }
            else if (srt.score3 >= srt.score0 && srt.score3 >= srt.score1 && srt.score3 >= srt.score2)
            {
                daYingJia[3].SetActive(true);
            }
        }       
    }
    public void ShowPlayerInfo(int i,int zimo,int jiepiao,int dianpao,int angang,int minggang,int score)
    {
        //StartCoroutine(loadimag(avater,touxiang[i]));
        touxiang[i].texture = GameObject.Find("HeadPhoto"+i).GetComponentInChildren<RawImage>().texture;
        ziMo[i].text = "自摸次数 ： " + zimo.ToString();
        jiePiao[i].text = "接炮次数 ： " + jiepiao.ToString();
        dianPao[i].text = "点炮次数 ： " + dianpao.ToString();
        anGang[i].text = "暗杠次数 ： " + angang.ToString();
        mingGang[i].text = "明杠次数 ： " + minggang.ToString();
        zongChengJi[i].text ="总成绩"+ score.ToString();
    }

    //IEnumerator loadimag(string url,RawImage ri)
    //{
    //    WWW www = new WWW(url);
    //    yield return www;
    //    ri.texture = www.texture;
    //}
    
    public void ShowNameAndId(int index,string name, int id)
    {
        playerName[index].text = name;
        playerID[index].text = id.ToString();
    }
    public void ReturnScenes()
    {
        //SocketMessageQueue.GetInstance().addMsg(GameGlobalMsg.DissolveRoom, null);
        Method.isChongLian = false;
        Method.LoadMainCity();
    }
    public void Close()
    {
        Destroy(gameObject);
    }
    
}
