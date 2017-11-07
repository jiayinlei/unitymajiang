using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class PlayBack : MonoBehaviour
{

    private List<int> Playerlist0;
    private List<int> Playerlist1;
    private List<int> Playerlist2;
    private List<int> Playerlist3;
    static List<int>[] listArr;  
    static List<GameObject> obj = new List<GameObject>();
    static List<Transform> ParentShouPai = new List<Transform>();
    //Method.huier//初始化
    public Transform[] parentShouPai;
    public Transform[] ParentOutPai;
    public Transform[] ParentPeng;
    public Transform[] ParentRecive;
    public GameObject[] HeadPhoto0playback;
    public GameObject[] peng;
    public GameObject[] gang;//明扛
    public GameObject[] Angang;//明扛
    private void Start()
    {
        InitObj();
        Playerlist0 = new List<int>();
        Playerlist1 = new List<int>();
        Playerlist2 = new List<int>();
        Playerlist3 = new List<int>();
    }

    static int[] intarr;
    public  void SetPlayerPosition(int peoplenum)
    {
        if (peoplenum == 4)
        {
            intarr = new int[] { 0, 1, 2, 3 };            

        }
        else
        {
            intarr = new int[3] { 0, 1, 3 };
           
        }
    }

    void InitListArr(int people, com.guojin.mj.net.message.game.MajiangChapterMsg majiangChapterMsg)
    {
        if (people==3)
        {
            listArr = new List<int>[3];
        }
        else
        {
            listArr = new List<int>[4];

        }
        for (int i = 0; i < majiangChapterMsg.userPlace.Count; i++)
        {           
            for (int j = 0; j < majiangChapterMsg.userPlace[i].shouPai.Length; j++)
            {
                listArr[i].Add(majiangChapterMsg.userPlace[i].shouPai[j]);
            }
        }
    }

    void InitObj()
    {
        for (int i = 0; i < 4; i++)
        {
            obj.Add(Resources.Load<GameObject>("MjPrefab/OutPai" + i + "PlayBack"));
        }
    }
    /// <summary>
    /// 第一次显示手牌的方法
    /// </summary>
    /// <param name="arr"></param>
    /// <param name="list"></param>
    /// <param name="weizhi"></param>
    void ShowMaJangaShouPai(int[] arr, List<int> list, int weizhi)
    {

        for (int i = 0; i < arr.Length; i++)
        {
            list.Add(arr[i]);
        }
        list.Sort();
        for (int i = list.Count - 1; i >= 0; i--)
        {
            GameObject temp = Instantiate(obj[intarr[weizhi]]);
            temp.GetComponentInChildren<MaJongOutInfo>().ShowOnePai(list[i], Method.MjNameSp[list[i]], -1);
            temp.transform.SetParent(parentShouPai[intarr[weizhi]]);
            temp.transform.localPosition = Vector3.zero;
            temp.transform.localScale = Vector3.one;
        }

    }
    /// <summary>
    /// 碰扛后显示手牌
    /// </summary>
    /// <param name="arr"></param>
    /// <param name="list"></param>
    /// <param name="weizhi"></param>
    void PengOverMaJangaShouPai(List<int> list, int weizhi)
    {
        Method.DestoryAll(parentShouPai[intarr[weizhi]]);
        list.Sort();
        for (int i = list.Count - 1; i >= 0; i--)
        {
            GameObject temp = Instantiate(obj[intarr[weizhi]]);
            temp.GetComponentInChildren<MaJongOutInfo>().ShowOnePai(list[i], Method.MjNameSp[list[i]], -1);
            temp.transform.SetParent(parentShouPai[intarr[weizhi]]);
            temp.transform.localPosition = Vector3.zero;
            temp.transform.localScale = Vector3.one;
        }
    }

    /// <summary>
    /// 显示头像的方法
    /// </summary>
    /// <param name="GUIO"></param>
    void ShowHead(List<com.guojin.mj.net.message.game.GameUserInfo> GUIO)
    {
        if (GUIO.Count==3)
        {
            HeadPhoto0playback[2].gameObject.SetActive(false);
        }
        for (int i = 0; i < GUIO.Count; i++)
        {
            HeadPhoto0playback[intarr[i]].GetComponent<PlayerInfo>().CreatOnePlayer(GUIO[i].score, GUIO[i].userName, GUIO[i].userId, GUIO[i].avatar);
        }
        //JsonData jd = new JsonData();
        //JsonData dsa = JsonMapper.ToObject(jd.ToJson());
        //GUIO[0].userName = dsa[0]["dsad"].ToJson();
    }

    /// <summary>
    /// 出一张牌
    /// </summary>
    /// <param name="weizhi"></param>
    /// <param name="pai"></param>
    void OutPaiBehav(int weizhi, int pai,List<int> list)
    {

        Method.DestoryAll(parentShouPai[intarr[weizhi]]);
        Destroy(Method.go);
        list.Remove(pai);
        list.Sort();
        for (int i = list.Count - 1; i >= 0; i--)
        {
            GameObject temp = Instantiate(obj[intarr[weizhi]]);
            temp.GetComponentInChildren<MaJongOutInfo>().ShowOnePai(list[i], Method.MjNameSp[list[i]], -1);
            temp.transform.SetParent(parentShouPai[intarr[weizhi]]);
            temp.transform.localPosition = Vector3.zero;
            temp.transform.localScale = Vector3.one;
        }        
        GameObject temp1 = Instantiate(obj[intarr[weizhi]]);
        temp1.GetComponentInChildren<MaJongOutInfo>().ShowOnePai(pai, Method.MjNameSp[pai], -1);
        temp1.transform.SetParent(ParentOutPai[intarr[weizhi]]);
        temp1.transform.localPosition = Vector3.zero;
        temp1.transform.localScale = Vector3.one;
        Method.lastMajong = temp1;
    }

    /// <summary>
    /// 接收到一张牌
    /// </summary>
    /// <param name="weizhi"></param>
    /// <param name="pai"></param>
    void ReciveOneMaJang(int weizhi, int pai,List<int> list)
    {

        GameObject temp1 = Instantiate(obj[intarr[weizhi]]);
        temp1.GetComponentInChildren<MaJongOutInfo>().ShowOnePai(pai, Method.MjNameSp[pai], -1);
        temp1.transform.SetParent(ParentRecive[intarr[weizhi]]);
        temp1.transform.localPosition = Vector3.zero;
        temp1.transform.localScale = Vector3.one;
        list.Add(pai);
        Method.go = temp1;
    }

    /// <summary>
    /// 
    /// </summary> 
    /// <param name="type"> 0 碰 1大明扛 ，2暗杠， 3 小明杠</param>
    /// <param name="pai"></param>
    /// <param name="list"></param>
    void MaJangBeha(int type,int pai,List<int> list)
    {
        switch (type)
        {
            case 0:
                for (int i = 0; i < 2; i++)
                {
                    list.Remove(pai);
                }               
                break;
            case 1:
                for (int i = 0; i < 3; i++)
                {
                    list.Remove(pai);
                }
                break;
            case 2:
                for (int i = 0; i < 4; i++)
                {
                    list.Remove(pai);
                }
                break;
            case 3:                
                    list.Remove(pai);                
                break;
        }
    }
    /// <summary>
    /// 显示碰扛的玩家位置
    /// </summary>
    /// <param name="myindex"></param>
    /// <param name="otherindex"></param>
    /// <returns></returns>
    int returnFangxiang(int myindex,int otherindex)
    {
        int a = myindex - otherindex;
        if (a==-3||a==1)
        {
            //左
            return 2;
        }else if (a==-1||a==3)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    void PengBeha(int weizhi,int pai)
    {
        GameObject temp1 = Instantiate(peng[intarr[weizhi]]);       
        temp1.transform.SetParent(ParentPeng[intarr[weizhi]]);
        temp1.transform.localPosition = Vector3.zero;
        temp1.transform.localScale = Vector3.one;
        temp1.GetComponent<OpenAndClose>().Id = pai;
        MaJongOutInfo[] mjo = temp1.GetComponentsInChildren<MaJongOutInfo>();
        for (int i = 0; i < mjo.Length; i++)
        {
            if (i==1)
            {
                //显示麻将碰的谁的
            }
            else
            {
                mjo[i].ShowOnePai(pai, Method.MjNameSp[pai], -1);
            }
        }
        Destroy(Method.lastMajong);

       // JsonData js = new JsonData();
       //JsonData jd= JsonMapper.ToObject(js.ToJson());
       //  string dsa= jd[0]["jdsha"].ToJson();
    }
    /// <summary>
    /// 明扛
    /// </summary>
    /// <param name="weizhi"></param>
    /// <param name="pai"></param>//加上Destroy(Method.lastMajong); 
    void GangBeha(int weizhi, int pai,int selfindex,int otherindex)
    {
        GameObject temp1 = Instantiate(gang[intarr[weizhi]]);
        temp1.transform.SetParent(ParentPeng[intarr[weizhi]]);
        temp1.transform.localPosition = Vector3.zero;
        temp1.transform.localScale = Vector3.one;
        MaJongOutInfo[] mjo = temp1.GetComponentsInChildren<MaJongOutInfo>();
        for (int i = 0; i < mjo.Length; i++)
        {
            if (i == 3)
            {
                //显示麻将碰的谁的
                mjo[i].ShowOnePai(pai, Method.MjNameSp[pai], returnFangxiang(selfindex,otherindex));
            }
            else
            {
                mjo[i].ShowOnePai(pai, Method.MjNameSp[pai], -1);
            }
        }
    }
    void XiaomingGangBeha(int weizhi, int pai, int selfindex, int otherindex)
    {
        OpenAndClose[] opArr = ParentPeng[intarr[weizhi]].GetComponentsInChildren<OpenAndClose>();
        for (int i = 0; i < opArr.Length; i++)
        {
            if (opArr[i].Id == pai)
            {
                Destroy(opArr[i].gameObject);               
            }
        }
        Destroy(Method.go);
        GangBeha(weizhi,pai, selfindex, otherindex);
    }
    /// <summary>
    /// 暗杠
    /// </summary>
    /// <param name="weizhi"></param>
    /// <param name="pai"></param>
    void AnGangBeha(int weizhi, int pai)
    {
        GameObject temp1 = Instantiate(Angang[intarr[weizhi]]);
        temp1.transform.SetParent(ParentPeng[intarr[weizhi]]);
        temp1.transform.localPosition = Vector3.zero;
        temp1.transform.localScale = Vector3.one;
        temp1.GetComponentInChildren<MaJongOutInfo>().ShowOnePai(pai, Method.MjNameSp[pai], -1);
        
    }


    

}
