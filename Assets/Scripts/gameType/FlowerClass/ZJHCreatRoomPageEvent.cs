using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZJHCreatRoomPageEvent : EventManager
{
    public override void InformationSetting()
    {
        SetLable(this.BindingSource[0], GameData.GetInstance().playerData.name);
        SetLable(this.BindingSource[1], string.Format("ID:{0}", GameData.GetInstance().playerData.id.ToString()));
        SetLable(this.BindingSource[2], GameData.GetInstance().playerData.gold.ToString());
    }
    /// <summary>
    /// 点击返回按钮
    /// </summary>
    void ClosePage()
    {
        Destroy(this.gameObject);
        UIManager.ChangeUI(UIManager.PageState.GameHall, (GameObject obj) =>
        {
            obj.GetComponent<GameHallPageEvent>().InformationSetting();
        });
    }
    /// <summary>
    /// 点击创建房间按钮
    /// </summary>
    void CreateRoom()
    {
        Debug.Log("炸金花创建房间配置界面");
        GameObject obj = Resources.Load<GameObject>("ZJHPrefab/RoomSetZJH");
        TooL.clone(obj, GameObject.Find("addNode"));
    }
    /// <summary>
    /// 点击加入房间按钮
    /// </summary>
    void JionRoom()
    {
        Debug.Log("炸金花加入房间");
        GameObject obj = Resources.Load<GameObject>("ZJHPrefab/JoinRoomLayerZJH");
        TooL.clone(obj, GameObject.Find("addNode"));
    }
    /// <summary>
    /// 点击继续游戏按钮
    /// </summary>
    void ContinueGame()
    {

    }
    /// <summary>
    /// 点击规则按钮
    /// </summary>
    void ShoWRule()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
