using UnityEngine;
using System.Collections;
using com.guojin.mj.net.message;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class TestZJH : MonoBehaviour {

    int a = 0;
    public void CopyShoupai(int index)
    {
        GameObject temp = Instantiate(Resources.Load<GameObject>("ZJHPrefab/BackCard"));
        temp.transform.SetParent(GameObject.Find("CardParent" + index).transform);
    }
    void Awake()
    {
        
        for (int i = 0; i < 4; i++)
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("ZJHPrefab/GrantCard" + i));
            obj.transform.SetParent(GameObject.Find("Canvas").transform);
            obj.transform.localPosition = new Vector3(0, 0, 0);
        }
      

    }
    public void jia()
    {
        a++;
        Debug.Log(a);
        CopyShoupai(0);
    }
    //   void Awake()
    //   {
    //       DontDestroyOnLoad(this);
    //   }
    //   public void OnMsg(string msg, com.guojin.core.io.message.Message data)
    //   {
    //       if (msg == MessageFactoryImpi.instance.getMessageString(7, 10))
    //       {// 登录返回来
    //           SceneManager.LoadScene("GameHall");
    //       }
    //       if (msg == GameGlobalMsg.HallOnClickZZMJ)
    //       {//点击郑州麻将
    //           SceneManager.LoadScene("Friends");
    //       }
    //       //if (msg == GameGlobalMsg.HallOnClickZJH)
    //       //{//点击炸金花
    //       //    SceneManager.LoadScene("PokerFlower");
    //       //}
    //       if (msg == MessageFactoryImpi.instance.getMessageString(3, 1))
    //       {// 炸金花创建房间返回
    //           Debug.Log("收到【3】【1】");
    //           com.guojin.mj.net.message.flower.CreateRoomRetZJH Jiz = (com.guojin.mj.net.message.flower.CreateRoomRetZJH)data;
    //           if (Jiz.result)
    //           {
    //               //发送加入房间消息
    //               com.guojin.mj.net.message.flower.JoinRoomInfoZJH ji = com.guojin.mj.net.message.flower.JoinRoomInfoZJH.create(Jiz.roomCheckId);
    //               Debug.Log("发送【3】【3】");
    //               SocketMgr.GetInstance().webSocket.Send(com.guojin.mj.net.Net.instance.write(ji));
    //           }
    //       }
    //       if (msg == MessageFactoryImpi.instance.getMessageString(3, 6))
    //       {// 炸金花同步房间信息
    //           Debug.Log("收到【3】【6】");
    //           MainSceneZJH.GRI = (com.guojin.mj.net.message.flower.GameRoomInfoZJH)data;
    //           SceneManager.LoadScene("PokerFlowerGame");
    //           MainSceneZJH.ReciveMessage(MainSceneZJH.GRI);
    //       }
    //       if (msg == MessageFactoryImpi.instance.getMessageString(3, 4))
    //       {// 炸金花加入房间返回
    //           Debug.Log("收到【3】【4】");
    //           com.guojin.mj.net.message.flower.JoinRoomInfoRetZJH Jiz = (com.guojin.mj.net.message.flower.JoinRoomInfoRetZJH)data;
    //           if (Jiz.result)
    //           {
    //               //向服务器发送玩家准备就绪消息
    //               com.guojin.mj.net.message.flower.ReadyZJH rrz = new com.guojin.mj.net.message.flower.ReadyZJH();
    //               Debug.Log("发送【3】【5】");
    //               SocketMgr.GetInstance().webSocket.Send(com.guojin.mj.net.Net.instance.write(rrz));
    //           }
    //           else
    //           {
    //               //弹出进入房间失败面板
    //           }
    //       }
    //       if (msg == MessageFactoryImpi.instance.getMessageString(3, 32))
    //       {
    //           Debug.Log("收到【3】【22】");
    //           com.guojin.mj.net.message.flower.GameUserInfoZJH GUIO = (com.guojin.mj.net.message.flower.GameUserInfoZJH)data;
    //           MainSceneZJH.StartShowHeadImage(GUIO);
    //           if (MainSceneZJH.allPeopleNum == 4)
    //           {
    //               if (GUIO.locationIndex == 3)
    //               {
    //                   MainSceneZJH.peopleNum = 4;
    //                   //发送开始游戏消息
    //                   Destroy(MainSceneZJH.InviteFriends);
    //                   MainSceneZJH.SendMessageStartGame();
    //               }
    //           }
    //       }
    //       if (msg == MessageFactoryImpi.instance.getMessageString(3, 8))
    //       {
    //           Debug.Log("收到【3】【8】");
    //           com.guojin.mj.net.message.flower.FaPaiInfoRetZJH fpif = (com.guojin.mj.net.message.flower.FaPaiInfoRetZJH)data;
    //           PlayGameZJH.Instance.DestoryAllChildren();
    //           MainSceneZJH.isStart = true;
    //           MainSceneZJH.chaperNum++;
    //           GameObject bt = GameObject.Find("GameStart");//寻找开始游戏按钮如果有，删除
    //           if (bt != null)
    //               Destroy(bt);
    //           for (int i = 0; i < fpif.Index.Length; i++)
    //           {
    //               if (MainSceneZJH.intArr[fpif.Index[i]] == 0)
    //               {
    //                   MainSceneZJH.isKanPai = false;
    //                   MainSceneZJH.isQiPai = false;
    //               }
    //               PlayGameZJH.Instance.CopyShoupai(fpif.Index[i]);
    //           }
    //           if(MainSceneZJH.intArr[fpif.ZhuangIndex] == 0)
    //           {
    //               MainSceneZJH.isMine = true;
    //           }
    //           GameObject temp1 = Instantiate(Resources.Load<GameObject>("ZJHPrefab/zhuangZJH"));
    //           temp1.transform.SetParent(GameObject.Find("HeadPhotoZJH" + fpif.ZhuangIndex).transform);//克隆庄
    //           temp1.GetComponent<RectTransform>().localPosition = new Vector3(22, 22, 0);
    //       }
    //   }    

    //    void Start () {

    // }

    //// Update is called once per frame
    //void Update () {
    //       if (Input.GetKeyDown(KeyCode.A))
    //       {
    //           //创建房间返回
    //           string msg = MessageFactoryImpi.instance.getMessageString(3, 1);
    //           com.guojin.mj.net.message.flower.CreateRoomRetZJH Jiz = com.guojin.mj.net.message.flower.CreateRoomRetZJH.create(true,"147258");
    //           OnMsg(msg, Jiz);
    //       }
    //       if (Input.GetKeyDown(KeyCode.B))
    //       {
    //           //加入房间返回
    //           string msg = MessageFactoryImpi.instance.getMessageString(3, 4);
    //           com.guojin.mj.net.message.flower.JoinRoomInfoRetZJH Jiz = com.guojin.mj.net.message.flower.JoinRoomInfoRetZJH.create(true);
    //           OnMsg(msg, Jiz);
    //       }
    //       //if (Input.GetKeyDown(KeyCode.C))
    //       //{
    //       //    //同步房间信息返回
    //       //    string msg = MessageFactoryImpi.instance.getMessageString(3, 6);
    //       //    List<com.guojin.mj.net.message.flower.GameUserInfoZJH> user = new List<com.guojin.mj.net.message.flower.GameUserInfoZJH>();
    //       //    user.Add(com.guojin.mj.net.message.flower.GameUserInfoZJH.create("张一", "123", 1, 10, 500, 0, 123456, true, "123456"));
    //       //    //user.Add(com.guojin.mj.net.message.flower.GameUserInfoZJH.create("张三", "123", 1, 10, 500, 2, 123415, true, "451236", "1", "1", "1", "1"));
    //       //    //user.Add(com.guojin.mj.net.message.flower.GameUserInfoZJH.create("张四", "123", 1, 10, 500, 3, 145698, true, "123465", "1", "1", "1", "1"));
    //       //    com.guojin.mj.net.message.flower.GameRoomInfoZJH Jiz = com.guojin.mj.net.message.flower.GameRoomInfoZJH.create(user,false,"147258",20,10,2000,false,4);
    //       //    OnMsg(msg, Jiz);
    //       //}
    //       //if (Input.GetKeyDown(KeyCode.D))
    //       //{
    //       //    a++;
    //       //    string msg = MessageFactoryImpi.instance.getMessageString(3, 32);
    //       //    com.guojin.mj.net.message.flower.GameUserInfoZJH Jiz = com.guojin.mj.net.message.flower.GameUserInfoZJH.create("张二", "123", 1, 10, 500, a, 159100, true, "456123");
    //       //    OnMsg(msg, Jiz);
    //       //}
    //       if (Input.GetKeyDown(KeyCode.E))
    //       {
    //           string msg = MessageFactoryImpi.instance.getMessageString(3, 8);
    //           com.guojin.mj.net.message.flower.FaPaiInfoRetZJH Jiz = com.guojin.mj.net.message.flower.FaPaiInfoRetZJH.create(4,new int[]{0,1,2,3},0);
    //           OnMsg(msg, Jiz);
    //       }
    //   }
}
