using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using DG.Tweening;

public class OpenAndClose : MonoBehaviour
{
    Image image;
    int id;

    //private void Start()
    //{
    //    ShowDingQue();
    //}
    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }


    public void CreatShow(Sprite sp)
    {
        image = GetComponent<Image>();
        image.sprite = sp;
        //StartCoroutine(WaitClose());
    }
    public void destorySelf()
    {
        Destroy(gameObject);
    }
    public void PENG()
    {
        VoiceManger.Instance.LoadMusiceByName(Method.PGH);
    }

    public void SendMessagePaoZuiToSever()
    {
        Toggle[] choiceArr = GetComponentsInChildren<Toggle>();

        // ChoiceZui[] choiceArr = GetComponentsInChildren<ChoiceZui>();
        List<string> listArr = new List<string>();      
        for (int i = 0; i < choiceArr.Length; i++)
        {
            if (choiceArr[i].isOn)
            {
                listArr.Add((i+1).ToString());
                Debug.Log(i+1);
            }
        }
        com.guojin.mj.net.message.game.SelectZuiType SZ;
        if (listArr.Count==0)
        {
            SZ = com.guojin.mj.net.message.game.SelectZuiType.Creat("");
        }
        else
        {
            SZ = com.guojin.mj.net.message.game.SelectZuiType.Creat(Method.ReturnString(listArr));
        }
      
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(SZ));
        Destroy(gameObject);
    }
    public void SendCanleToSever()
    {    
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(com.guojin.mj.net.message.game.SelectZuiType.Creat("")));
        Destroy(gameObject);
    }
    /// <summary>
    /// 显示定缺
    /// </summary>
    /// <param name="arr"></param>
    public void ShowDingQue(int[]arr)
    {
        Button[] imageArr = GetComponentsInChildren<Button>();
        Method.duanmenindex = arr[0];
        Method.DuanMan = arr[0];
        Method.CloneChild();
        for (int i = 0; i < arr.Length; i++)
        {
            imageArr[i].gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("NewUIPicture/gushi/duanmen" + arr[i]);          
        }
        imageArr[0].onClick.AddListener(delegate {
            //发送定缺
            Method.duanmenindex = arr[0];
            Method.DuanMan = arr[0];
            Method.CloneChild();
            dotweenColler(imageArr,0);
        });
        imageArr[1].onClick.AddListener(delegate {
            //发送定缺
            Method.duanmenindex = arr[1];
            Method.DuanMan = arr[1];
            Method.CloneChild();
            dotweenColler(imageArr, 1);
        });
        imageArr[2].onClick.AddListener(delegate {
            //发送定缺
            Method.duanmenindex = arr[2];
            Method.DuanMan = arr[2];
            Method.CloneChild();
            dotweenColler(imageArr, 2);
        });
    }

    void dotweenColler(Button[] imageArr,int num)
    {
        for (int i = 0; i < imageArr.Length; i++)
        {
            if (num==i)
            {
                imageArr[i].GetComponent<DOTweenAnimation>().DOPlay();
            }
            else
            {
                imageArr[i].GetComponent<DOTweenAnimation>().DORewind();
            }
        }
    }
    private Image[]state;
    private Sprite[] statesprite=new Sprite[5];
    //选嘴
    public void InitState()
    {
        state = GetComponentsInChildren<Image>();
        for (int i = 0; i < 5; i++)
        {
            statesprite[i] = Resources.Load<Sprite>("NewUIPicture/gushi/state"+i);
        }
        if (Method.PeopleNum==3)
        {
            state[1].gameObject.SetActive(false);
        }
        for (int i = 0; i < state.Length; i++)
        {
            state[i].GetComponent<RectTransform>().sizeDelta = new Vector2(194, 57);
            state[i].sprite = statesprite[0];
        }
    }
    /// <summary>
    /// 用于选跑时断线的情况
    /// </summary>
    public void InitShowPao()
    {
        state = GetComponentsInChildren<Image>();
        for (int i = 0; i < 5; i++)
        {
            statesprite[i] = Resources.Load<Sprite>("NewUIPicture/gushi/state" + i);
        }
        if (Method.PeopleNum == 3)
        {
            state[1].gameObject.SetActive(false);
        }
        for (int i = 0; i < state.Length; i++)
        {
            state[i].GetComponent<RectTransform>().sizeDelta = new Vector2(147, 57);
            state[i].sprite = statesprite[1];
        }
    }
    public void ShowPao()
    {
        if (Method.PeopleNum == 3)
        {
            state[1].gameObject.SetActive(false);
        }
        else
        {
            state[1].gameObject.SetActive(true);
        }
        for (int i = 0; i < state.Length; i++)
        {
            if (i!=1)
            {
                state[i].gameObject.SetActive(true);
            }          
            state[i].GetComponent<RectTransform>().sizeDelta = new Vector2(147, 57);
            state[i].sprite = statesprite[1];
        }        

    }
    public void ShowWeiZhiDingZui(int weizhi)
    {
        state[Method.intArr[weizhi]-1].GetComponent<RectTransform>().sizeDelta = new Vector2(194,57); 
        state[Method.intArr[weizhi]-1].sprite = statesprite[0];
    }
    public void ShowWeiZhiDingZuiAlready(int weizhi)
    {        
        state[Method.intArr[weizhi]-1].gameObject.SetActive(false);
    }  

    public void ShowWeiZhiDingPaoAlready(int weizhi)
    {     
        state[Method.intArr[weizhi]-1].sprite = statesprite[2];
    }
    public void SetFalseAll()
    {
        for (int i = 0; i < state.Length; i++)
        {
            state[i].gameObject.SetActive(false);
        }
    }
    public void ShowQueMen()
    {
        state = GetComponentsInChildren<Image>();
        for (int i = 0; i < 5; i++)
        {
            statesprite[i] = Resources.Load<Sprite>("NewUIPicture/gushi/state" + i);
        }
        if (Method.PeopleNum == 3)
        {
            state[1].gameObject.SetActive(false);
        }
        for (int i = 0; i < state.Length; i++)
        {
            state[i].GetComponent<RectTransform>().sizeDelta = new Vector2(147, 57);
            state[i].sprite = statesprite[3];
        }
    }
    public void ShowWeiZhiDingQue(int weizhi)
    {    
        state[Method.intArr[weizhi]].GetComponent<RectTransform>().sizeDelta = new Vector2(147, 57);
        state[Method.intArr[weizhi]].sprite = statesprite[3];
    }
    public void ShowWeiZhiDingQueAlready(int weizhi)
    {     
        state[Method.intArr[weizhi]-1].sprite = statesprite[4];
    }
}

