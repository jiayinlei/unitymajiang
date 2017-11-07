using com.guojin.mj.net.message;
using com.guojin.mj.net.message.game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitStartGamePageEvent : EventManager
{
    public List<ItemData> chatItemList;
    public ItemData CreatItemData(string name,string chatContent)
    {
        ItemData it = new ItemData() { playerName = name, chatContent = chatContent };

        return it;

    }
    public override void InformationSetting()
    {
        MainLogic.Controller.gameType = GameType.MJ;
        SetActive(BindingSource[0], true);
        SetActive(BindingSource[1], false);
        SetActive(BindingSource[2], false);
        SetActive(BindingSource[3], true);
        //initEvent();
        chatItemList = new List<ItemData>();
    }
    void closeYuyin()
    {
        RocordWebSocket.webSocket.Close();
    }
    public void initEvent()
    {
        string[] msgList = this.GetMsgList();
        foreach (string msg in msgList)
        {
            ObserverMgr.addEventListenerWithDelegate(msg, new ObserverMgr.OnMsg(this.OnMsg));
        }

    }
    protected string[] GetMsgList()
    {
        return new string[] {
             MessageFactoryImpi.instance.getMessageString(1,25),// 聊天消息返回
  
        };
    }
    public void OnMsg(string msg, com.guojin.core.io.message.Message data)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(1, 25))
        {
            Debug.Log("收到聊天信息");
            PlayerReceiveChatInfo chatInfo = (PlayerReceiveChatInfo)data;
            if (chatInfo.Num == 1)
            {
                string content = GameObject.Find("WaitStartGame(Clone)").GetComponent<WaitStartGame>().TouXiang[chatInfo.UserIndex].GetComponent<HeadInfo>().username.text;
                chatItemList.Add(new ItemData() { playerName = content, chatContent = chatInfo.ReceiveChatInfo });
                if (chatItemList.Count >= 31)
                {
                    chatItemList.RemoveAt(0);
                }
                cleanItem();
                initView();
            }


            Debug.Log(chatInfo.ReceiveChatInfo);
        }
    }
   public  void cleanItem()
    {
        TooL.destroyAllChildren(this.BindingSource[4]);
    }
    public void initView()
    {
        //todo : sunfei
        GameObject tr = GameObject.Find("Content_0");
        for (int i = 0; i < chatItemList.Count; i++)
        {
            GameObject obj = TooL.clone(Resources.Load<GameObject>("Prefab/Item/ChatItem"), tr);
            SetLable(obj.GetComponent<ChatItemEvent>().BindingSource[0], chatItemList[i].playerName);
            SetLable(obj.GetComponent<ChatItemEvent>().BindingSource[1], ": " + chatItemList[i].chatContent);
        }

        //tr.transform.localPosition = new Vector3(-10f,-164f,0f);
    }
    void MicImageClick()
    {
        SetActive(BindingSource[0], false);
        SetActive(BindingSource[1], true);
        SetActive(BindingSource[2], true);
        SetActive(BindingSource[3], false);
    }
    void KeyBordBtnClick()
    {
        SetActive(BindingSource[0], true);
        SetActive(BindingSource[1], false);
        SetActive(BindingSource[2], false);
        SetActive(BindingSource[3], true);
    }
    void SendBtnClick()
    {
        string strTemp = this.BindingSource[3].GetComponent<InputField>().text;
        if (!strTemp.Equals(""))
        {
            PlayerSendChatInfo req = PlayerSendChatInfo.create(strTemp, 1);
            SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(req));
            this.BindingSource[3].GetComponent<InputField>().text = "";
        }

    }
    // Use this for initialization
    WaitStartGame ws;
    void Start()
    {
        Open();
        InformationSetting();
        ws = GameObject.Find("WaitStartGame(Clone)").GetComponent<WaitStartGame>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public class ItemData
    {
        public string playerName;
        public string chatContent;
    }
}
