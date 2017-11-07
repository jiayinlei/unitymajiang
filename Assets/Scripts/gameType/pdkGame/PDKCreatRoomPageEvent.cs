using com.guojin.core.io.message;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PDKCreatRoomPageEvent : EventManager
{
    public override void InformationSetting()
    {
        SetLable(this.BindingSource[0], GameData.GetInstance().playerData.name);
        SetLable(this.BindingSource[1], string.Format("ID:{0}", GameData.GetInstance().playerData.id.ToString()));
        SetLable(this.BindingSource[2], GameData.GetInstance().playerData.gold.ToString());
    }
    void CreatRoomBtnClick()
    {
        List<com.guojin.mj.net.message.login.OptionEntry> OptionList = new List<com.guojin.mj.net.message.login.OptionEntry>();
        OptionList.Add(com.guojin.mj.net.message.login.OptionEntry.create("chapterMax", "3"));//局数 

        com.guojin.mj.net.message.login.CreatePdkRoom createPdkRoom = com.guojin.mj.net.message.login.CreatePdkRoom.create(OptionList);
        SocketClient.GetInstance().Send(createPdkRoom, 4, 0, (Message msg, int type, int id) =>
        {
            com.guojin.mj.net.message.login.CreatePdkRoom_Ret Ret = (com.guojin.mj.net.message.login.CreatePdkRoom_Ret)msg;
            if (Ret.result == true)
            {
                Debug.Log("room id=>>" + Ret.roomCheckId);
            }


            //GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_LoadingPage);

        });
    }
    void PlayAgain()
    {

    }
    void JoinRoomBtnClick()
    {

    }
    void ClosePage()
    {
        Destroy(this.gameObject);
        UIManager.ChangeUI(UIManager.PageState.GameHall, (GameObject obj) =>
        {
            obj.GetComponent<GameHallPageEvent>().InformationSetting();
        });
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
