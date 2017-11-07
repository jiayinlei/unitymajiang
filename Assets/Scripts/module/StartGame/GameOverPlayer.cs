using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameOverPlayer : MonoBehaviour {

    public Image titile;
    public Sprite windianpao;
    public Sprite lose;
    public Sprite winzimu;
    public GridLayoutGroup GG;
    public Transform ParentAll;
    public GameObject particulars;//固始麻将查看详情按钮

    List<int> listInt ;  
    public void ShowInfo(com.guojin.mj.net.message.game.GameChapterEnd GCD)
    {
        setGrid();
        if (GCD.huPaiIndex != -1)
        {
            if (GCD.fangPaoIndex != -1)
            {
                //点炮
                showDianPao(GCD);
            }
            else
            {
                //自摸
                showZiMo(GCD);
            }
        }
        else
        {
            //流局
            showLiuJu(GCD);
        }
    }
    public void setGrid()
    {
        if (Method.PeopleNum == 3)
        {
            GG.cellSize = new Vector2(944, 150);
        }
        else
        {
            GG.cellSize = new Vector2(944, 115);
        }
        if (Method.MJType== "MaJongGuShi" && !GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback)
        {
            particulars.SetActive(true);
            particulars.GetComponent<Button>().onClick.AddListener(delegate {
                com.guojin.mj.net.message.game.RecordDetails RD = new com.guojin.mj.net.message.game.RecordDetails();
                SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(RD));
                //ShowParticulars();
            });
        }
    }
    public void Initlist()
    {
        listInt = new List<int>();
        if (Method.PeopleNum==3)
        {
            listInt.Add(0);
            listInt.Add(1);
            listInt.Add(2);
        }
        else
        {
            listInt.Add(0);
            listInt.Add(1);
            listInt.Add(2);
            listInt.Add(3);
        }        

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="type">0为自模糊1为点炮胡2为点炮</param>
    public void CloneImage(Transform parent,int type)
    {

        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/winType"));
        temp.transform.SetParent(parent);
        temp.transform.localScale = Vector3.one;
        temp.transform.localPosition = new Vector3(416,0,0);
        if (type==0)
        {
            temp.GetComponent<Image>().sprite = winzimu;
        }
        else if(type==1)
        {
            temp.GetComponent<Image>().sprite = windianpao;
        }
        else
        {
            temp.GetComponent<Image>().sprite = lose;
        }
    }
    public void showDianPao(com.guojin.mj.net.message.game.GameChapterEnd GCD)
    {
        Initlist();
        if (GCD.huPaiIndex==Method.Index)
        {
            //titile.text = "胜利";
            titile.sprite= Resources.Load<Sprite>("Atlas/jieshuan");

        }
        else if (GCD.fangPaoIndex==Method.Index)
        {
            //titile.text = "失败";
            titile.sprite = Resources.Load<Sprite>("Atlas/jieshuan");
        }
        else
        {
            //titile.text = "平局";
            titile.sprite = Resources.Load<Sprite>("Atlas/jieshuan");
        }
        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/player"));
        temp.transform.SetParent(ParentAll);
        temp.transform.localScale = Vector3.one;
        temp.transform.localPosition = Vector3.zero;
        temp.GetComponent<ShowPlayerInfo>().ShowShouPai(GCD.fanResults[GCD.huPaiIndex],GCD.huPaiIndex);
        CloneImage(temp.transform,1);
        listInt.Remove(GCD.huPaiIndex);
        for (int i = 0; i < listInt.Count; i++)
        {
            GameObject temp1 = Instantiate(Resources.Load<GameObject>("MjPrefab/player"));
            temp1.transform.SetParent(ParentAll);
            temp1.transform.localScale = Vector3.one;
            temp1.transform.localPosition = Vector3.zero;
            temp1.GetComponent<ShowPlayerInfo>().ShowShouPai(GCD.fanResults[listInt[i]],listInt[i]);
            if (listInt[i]== GCD.fangPaoIndex)
            {
                CloneImage(temp1.transform, 2);
            }                     
        }
    }
    public void  showZiMo(com.guojin.mj.net.message.game.GameChapterEnd GCD)
    {
        Initlist();
        if (GCD.huPaiIndex == Method.Index)
        {
            titile.sprite = Resources.Load<Sprite>("Atlas/jieshuan");
        }
        else
        {
            titile.sprite = Resources.Load<Sprite>("Atlas/jieshuan");
        }
        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/player"));
        temp.transform.SetParent(ParentAll);
        temp.transform.localScale = Vector3.one;
        temp.transform.localPosition = Vector3.zero;
        temp.GetComponent<ShowPlayerInfo>().ShowShouPai(GCD.fanResults[GCD.huPaiIndex], GCD.huPaiIndex);
        CloneImage(temp.transform, 0);
        listInt.Remove(GCD.huPaiIndex);
        for (int i = 0; i < listInt.Count; i++)
        {
            GameObject temp1 = Instantiate(Resources.Load<GameObject>("MjPrefab/player"));
            temp1.transform.SetParent(ParentAll);
            temp1.transform.localScale = Vector3.one;
            temp1.transform.localPosition = Vector3.zero;
            temp1.GetComponent<ShowPlayerInfo>().ShowShouPai(GCD.fanResults[listInt[i]], listInt[i]);
        }
    }
    public void showLiuJu(com.guojin.mj.net.message.game.GameChapterEnd GCD)
    {
        Initlist();
        titile.sprite = Resources.Load<Sprite>("Atlas/jieshuan");
        for (int i = 0; i < GCD.fanResults.Count; i++)
        {
            GameObject temp1 = Instantiate(Resources.Load<GameObject>("MjPrefab/player"));
            temp1.transform.SetParent(ParentAll);
            temp1.transform.localScale = Vector3.one;
            temp1.transform.localPosition = Vector3.zero;
            temp1.GetComponent<ShowPlayerInfo>().ShowShouPai(GCD.fanResults[i], i);
        }
    }
    public void ClosePanle()
    {
        if (GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback)
        {
            //GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().resetPlayback();
            Method.LoadMainCity();
        } else
        {
            Destroy(gameObject);
        }
    }

    public void ShowParticulars()
    {
        GameObject temp1 = Instantiate(Resources.Load<GameObject>("MjPrefab/ParticularsPanel"));
        temp1.transform.SetParent(transform);
        temp1.transform.localPosition=Vector3.zero;
        temp1.transform.localScale = Vector3.one;
        //temp1.GetComponentInChildren<ParticularsPanel>().ShowInfoSoce();
    }
}
	
