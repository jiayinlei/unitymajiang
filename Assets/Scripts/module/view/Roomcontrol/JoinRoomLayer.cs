using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using com.guojin.mj.net.message;
using UnityEngine.SceneManagement;
public class JoinRoomLayer : Observer {   
    public Text[] textgroup;
    public static int index=0;
    protected override string[] GetMsgList() {
        return new string[] {
            MessageFactoryImpi.instance.getMessageString(7, 23),// 创建房间返回
             MessageFactoryImpi.instance.getMessageString(7, 7),// 创建房间返回
             //MessageFactoryImpi.instance.getMessageString(7, 11),// 创建房间返回
        };
    }
    public override void OnMsg(string msg, com.guojin.core.io.message.Message data) {
        if (msg == MessageFactoryImpi.instance.getMessageString(7, 7))
        {
            com.guojin.mj.net.message.login.JoinRoomRet jrt= (com.guojin.mj.net.message.login.JoinRoomRet) data;
            if (!jrt.result)
            {
                for (int i = 0; i < textgroup.Length; i++)
                {
                    textgroup[i].text = "";
                }
                index = 0;
                roomid = "";
                //GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/JoinRoomDef"));
                //temp.transform.SetParent(gameObject.transform);
                //temp.transform.localPosition = Vector3.zero;
                //temp.transform.localScale = Vector3.one;
                //temp.GetComponentInChildren<RuleLayer>().ShowText("加入房间失败！");
                //Destroy(transform.parent.gameObject);
            }
            else
            {
                com.guojin.mj.net.message.game.GameJoinRoom Gjr = new com.guojin.mj.net.message.game.GameJoinRoom();
                SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(Gjr));
            }
        }
        if (msg == MessageFactoryImpi.instance.getMessageString(7, 23)) {
            //this.TesrGo();
            // com.guojin.mj.net.message.login.JoinRoomRet gameData = (com.guojin.mj.net.message.login.JoinRoomRet)data;
            //if (gameData.result) {// 创建房间成功

            //    SceneManager.LoadScene("Horizontal");
            //} 
            //GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_LoadingPage);
            //GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/WaitStartGame"));
            //temp.transform.SetParent(GameObject.Find("addNode").transform);
            //temp.transform.localScale = Vector3.one;
            //temp.transform.localPosition = Vector3.zero;
            //Method.readyGame = temp;
            Destroy(transform.parent.gameObject);

        }
        //else
        //{// 创建房间失败
        //    Debug.Log("创建房间失败...");
        //    TooL.showTips("创建房间失败", new TipsDialogCallBack(this.OnClickTipsDialogOKBtn));
        //}
        //ReciveNotice(msg,data);
    }


    //void ReciveNotice(string msg, com.guojin.core.io.message.Message data)
    //{
    //    if (msg == MessageFactoryImpi.instance.getMessageString(7, 11))
    //    {
    //        com.guojin.mj.net.message.login.Notice notice = (com.guojin.mj.net.message.login.Notice)data;
    //        if (PlayerData.dictionary.ContainsKey(notice.key))
    //        {
    //            GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/JoinRoomDef"));
    //            temp.transform.SetParent(gameObject.transform.parent);
    //            temp.transform.localPosition = Vector3.zero;
    //            temp.transform.localScale = Vector3.one;
    //            temp.GetComponentInChildren<RuleLayer>().ShowText(PlayerData.dictionary[notice.key]);
    //        }
    //    }
    //}

    void Reciveroomret(string msg, com.guojin.core.io.message.Message message)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(7, 7))
        {
            com.guojin.mj.net.message.login.JoinRoomRet jrt = (com.guojin.mj.net.message.login.JoinRoomRet)message;
            if (!jrt.result)
            {
                //GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/Notice"));
                //temp.transform.SetParent(gameObject.transform);
                //temp.transform.localPosition = Vector3.zero;
                //temp.transform.localScale = Vector3.one;
                //temp.GetComponentInChildren<RuleLayer>().ShowText("加入房间失败");
                Method.isChongLian = false;
                StartCoroutine(ReturnGameHall());
                //Destroy(transform.parent.gameObject);
            }
            else
            {
                com.guojin.mj.net.message.game.GameJoinRoom Gjr = new com.guojin.mj.net.message.game.GameJoinRoom();
                SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(Gjr));
            }
        }
    }
    IEnumerator ReturnGameHall()
    {
        yield return new WaitForSeconds(2);
        //GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_LoadingPage);
        Method.LoadMainCity();
    }
    private void TesrGo() {
        com.guojin.mj.net.message.game.GameJoinRoom Gjr = new com.guojin.mj.net.message.game.GameJoinRoom(); //com.guojin.mj.net.message.game.GameJoinRoom//= com.guojin.mj.net.message.game.GameJoinRoom.create(null);
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(Gjr));
    }
    private void OnClickTipsDialogOKBtn() {

    }
    void Start() {
        this.initMsg();       
        index = 0;
        roomid = "";
    }



    private void SendJoinRoomMsg() {

    }
    static string roomid;
    private void UpdateInputRoomNum(int num) {
        LoginAndShare.Controller.BtnVoice();
        textgroup[index].text = num.ToString();       
        index++;
        if (textgroup[5].text!=""&&textgroup[5].text!=null)
        {
            for (int i = 0; i < textgroup.Length; i++)
            {
                roomid += textgroup[i].text;
            }
            Method.joinRoomID = roomid;
            SendMessageMgr.JoinRoom(roomid);
        }
    }

    // 退格键
    public void OnClickBackBtn() {
        for (int i = 0; i < textgroup.Length; i++)
        {
            textgroup[i].text = "";
        }
        textgroup[index].text = "";
        index--;
        LoginAndShare.Controller.BtnVoice();
        //this.roomNum.text = "";
        Destroy(transform.parent.gameObject);
    }


    public void Get0() {
        this.UpdateInputRoomNum(0);
    }
    public void Get1() {
        this.UpdateInputRoomNum(1);
    }
    public void Get2() {
        this.UpdateInputRoomNum(2);
    }
    public void Get3() {
        this.UpdateInputRoomNum(3);
    }
    public void Get4() {
        this.UpdateInputRoomNum(4);
    }
    public void Get5() {
        this.UpdateInputRoomNum(5);
    }
    public void Get6() {
        this.UpdateInputRoomNum(6);
    }
    public void Get7() {
        this.UpdateInputRoomNum(7);
    }
    public void Get8() {
        this.UpdateInputRoomNum(8);
    }
    public void Get9() {
        this.UpdateInputRoomNum(9);
    }
    public void Backspace()//退格
    {
        LoginAndShare.Controller.BtnVoice();
        //if (roomNum.text.Length > 0) {
        //    roomNum.text = roomNum.text.Substring(0, roomNum.text.Length - 1);
        //}
        if (index>0)
        {
            index--;
            textgroup[index].text = "";
          
        }
        
    }
    public void Clear()//清除
    {
        LoginAndShare.Controller.BtnVoice();
        for (int i = 0; i < textgroup.Length; i++)
        {
            textgroup[i].text = "";
        }
        index = 0;
        roomid = "";      
    }
}
