using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPageEvent : EventManager
{
    public static ResultPageEvent Instance;
    public ResultPageEvent()
    {
        Instance = this;
    }

    public override void InformationSetting()
    {
    }
    /// <summary>
    /// 点击麻将
    /// </summary>
    void MJBtn()
    {
        com.guojin.mj.net.message.login.RoomHistoryList rhl = new com.guojin.mj.net.message.login.RoomHistoryList();
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(rhl));
        Debug.Log("点击麻将");
    }
    /// <summary>
    /// 点击炸金花
    /// </summary>
    void ZJHBtn()
    {
       
        Debug.Log("点击炸金花");

    }
    /// <summary>
    /// 点击跑得快
    /// </summary>
    void PDKBtn()
    {
        Debug.Log("点击跑得快");

    }
    /// <summary>
    /// 点击分享
    /// </summary>
    void ShareBtn()
    {

    }

    void Start()
    {
        
    }
    void ClosePage()
    {
        if (GameObject.Find("Canvas/addNode/NewGameHall(Clone)"))
        {
            GameObject.Find("Canvas/addNode/NewGameHall(Clone)").GetComponent<GameHallPageEvent>().ChangeBtnSize(0);
        }
        Destroy(this.gameObject);

    }
    /// <summary>
    /// 显示房间总战绩信息
    /// </summary>
    public void ShowResultRoom(com.guojin.mj.net.message.login.RoomHistoryListRet rhl)
    {
        for (int i = 0; i < rhl.list.Count; i++)
        {
            GameObject obj = TooL.clone(Resources.Load<GameObject>("Prefab/FightRecordItem"), this.BindingSource[0]);
            for (int j = 0; j < rhl.list[i].userNames.Length; j++)
            {
                SetLable(obj.GetComponent<FightRecordItemEvent>().BindingSource[0], "郑州麻将(" + rhl.list[i].chapterNums + ")局");
                SetLable(obj.GetComponent<FightRecordItemEvent>().BindingSource[1], rhl.list[i].startDate);
                SetLable(obj.GetComponent<FightRecordItemEvent>().BindingSource[2],string .Format ("{0}",rhl.list[i].roomCheckId) );
                if(rhl.list[i].userNames[j]!=null)
                    if (!rhl.list[i].userNames[j].Equals("")) { 

                        SetLable(obj.GetComponent<FightRecordItemEvent>().BindingSource[3 + j], rhl.list[i].userNames[j]);
                        SetLable(obj.GetComponent<FightRecordItemEvent>().BindingSource[7 + j], rhl.list[i].scores[j].ToString ());
                    }
            }
            //TooL.clone(Resources.Load<GameObject>("Result/UserInfo"), obj.transform.FindChild("UserInfoPanel").gameObject);
        }
    }
}
