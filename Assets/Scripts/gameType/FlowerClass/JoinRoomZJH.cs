using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using com.guojin.mj.net.message;
using UnityEngine.SceneManagement;
public class JoinRoomZJH : Observer
{
    public Text roomNum;
    void Start()
    {
        this.initMsg();
        this.roomNum.text = "";
    }
    private void UpdateInputRoomNum(int num)
    {
        if (this.roomNum.text.Length < 6)
        {
            this.roomNum.text += num.ToString();

            if (this.roomNum.text.Length == 6)
            {
                string roomID = this.roomNum.text;
                com.guojin.mj.net.message.flower.JoinRoomInfoZJH jr = com.guojin.mj.net.message.flower.JoinRoomInfoZJH.create(roomID);
                SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(jr));
            }
        }
        else
        {
            Debug.Log("[JoinRoomLayer] 房间号已经输入6位");
        }
    }
    public void OnClickBackBtn()
    {
        this.roomNum.text = "";
        Destroy(this.gameObject);
    }
    public void Get0()
    {
        this.UpdateInputRoomNum(0);
    }
    public void Get1()
    {
        this.UpdateInputRoomNum(1);
    }
    public void Get2()
    {
        this.UpdateInputRoomNum(2);
    }
    public void Get3()
    {
        this.UpdateInputRoomNum(3);
    }
    public void Get4()
    {
        this.UpdateInputRoomNum(4);
    }
    public void Get5()
    {
        this.UpdateInputRoomNum(5);
    }
    public void Get6()
    {
        this.UpdateInputRoomNum(6);
    }
    public void Get7()
    {
        this.UpdateInputRoomNum(7);
    }
    public void Get8()
    {
        this.UpdateInputRoomNum(8);
    }
    public void Get9()
    {
        this.UpdateInputRoomNum(9);
    }
    public void Backspace()//退格
    {
        if (roomNum.text.Length > 0)
        {
            roomNum.text = roomNum.text.Substring(0, roomNum.text.Length - 1);
        }
    }
    public void Clear()//清除
    {
        roomNum.text = "";
    }
}

