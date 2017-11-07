using UnityEngine;

using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainSceneZJH : MonoBehaviour 
{
    public static int allPeopleNum = 5;//游戏总人数
    public static int peopleNum;//游戏剩余人数
    public static int Index;//玩家座的位置 东0 西2 南1 北3
    public static int[] intArr = new int[4];
    public static com.guojin.mj.net.message.flower.GameRoomInfoZJH GRI;
    public static string RoomID;//房间id
    public static bool isKanPai;//是否看牌
    public static bool isQiPai;//是否弃牌
    public static bool isMine;//是否该自己操作
    public static bool isOut;//是否出局
    public static bool isBiPai;//是否比牌
    public static bool isStart;//是否开始游戏
    public static bool isAdd;//是否加注
    public static int chaperNum;//当前局数
    public static int chaper;//总局数
    public static int LunNum;//轮数
    public static int addZhu;//当前的注数
    public static GameObject zhuang;//克隆出来的庄
    public static bool isUpdate;
    public static GameObject InviteFriends;//邀请好友按钮
    public static Text daojishi;
    public static Sprite[] MjNameSp;//图片精灵
    public static int timeNum = 20;//操作计时
    public static GameObject positionStar;//显示到哪位玩家出牌   
    public static int[] userIndex = new int[] { 0, 1, 2, 3 };// 东0 西2 南1 北3

    private float time;
    /// <summary>
    /// 点击看牌按钮
    /// </summary>
    public void OnClickKanPai()
    {
        //向服务器发送看牌信息
        if(!isKanPai)
        {
            com.guojin.mj.net.message.flower.LookCardInfoZJH lc = com.guojin.mj.net.message.flower.LookCardInfoZJH.Create(0);
            SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(lc));
        }
        else
        {
            Debug.Log("已看牌");
        }
        GameObject temp = GameObject.Find("JiaZhu");
        if(temp != null)
        {
            isAdd = false;
            Destroy(temp);
        }
    }
    /// <summary>
    /// 点击弃牌按钮
    /// </summary>
    public void OnClickQiPai()
    {
        //向服务器发送弃牌信息
        com.guojin.mj.net.message.flower.LookCardInfoZJH guc = com.guojin.mj.net.message.flower.LookCardInfoZJH.Create(1);
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(guc));
        GameObject temp = GameObject.Find("JiaZhu");
        if (temp != null)
        {
            isAdd = false;
            Destroy(temp);
        }
    }
    /// <summary>
    /// 点击跟注按钮
    /// </summary>
    public void OnClickGenZhu()
    {
        //向服务器发送跟注信息
        com.guojin.mj.net.message.flower.LookCardInfoZJH fmi = com.guojin.mj.net.message.flower.LookCardInfoZJH.Create(2);
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(fmi));
        GameObject temp = GameObject.Find("JiaZhu");
        if (temp != null)
        {
            isAdd = false;
            Destroy(temp);
        }
    }

    /// <summary>
    /// 点击比牌、取消按钮
    /// </summary>
    public void OnClickBiPai()
    {
        //点击其他玩家头像选择比牌对象，比完不亮牌
        isBiPai = !isBiPai;
        GameObject temp = GameObject.Find("JiaZhu");
        if (temp != null)
        {
            isAdd = false;
            Destroy(temp);
        }
    }
    /// <summary>
    /// 点击加注按钮
    /// </summary>
    public void OnClickAddMoney()
    {
        //弹出四个不同注的按钮
        if(!isAdd)
        {
            isAdd = true;
            GameObject obj = TooL.clone(Resources.Load<GameObject>("ZJHPrefab/JiaZhu"), GameObject.Find("Canvas"));
            obj.transform.localPosition = new Vector3(310, -280, 0);
            obj.transform.localScale = Vector3.one;
            Button[] btn = obj.GetComponentsInChildren<Button>();
            for (int i = 0; i < btn.Length; i++)
            {
                if (addZhu >= RoomSetInfoZJH.danZhu * (i + 2))
                {
                    btn[i].interactable = false;
                }
                Text btnText = btn[i].GetComponentInChildren<Text>();
                if (MainSceneZJH.isKanPai)
                {
                    btnText.text = RoomSetInfoZJH.danZhu * 2 * (i + 2) + "";
                }
                else
                {
                    btnText.text = RoomSetInfoZJH.danZhu * (i + 2) + "";
                }
                btn[i].onClick.AddListener(delegate
                {
                    AddMoney(int.Parse(btnText.text));
                    Debug.Log(int.Parse(btnText.text));
                    isAdd = false;
                    Destroy(obj);
                });
            }
  
        }
        else
        {
            isAdd = false;
            Destroy(GameObject.Find("JiaZhu"));
        }
    }
    /// <summary>
    /// 加注
    /// </summary>
    public void AddMoney(int zhu)
    {
        com.guojin.mj.net.message.flower.AddMoneyInfoZJH ami = com.guojin.mj.net.message.flower.AddMoneyInfoZJH.Create(zhu);
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(ami));
    }
    public static void ShowInviteFriends()
    {
        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/InviteFriendsBtn"));
        temp.transform.SetParent(GameObject.Find("BtnInfoZJH").transform);
        temp.name = "InviteFriendsBtn";
        temp.transform.localScale = Vector3.one;
        temp.transform.localPosition = Vector3.zero;
        InviteFriends = temp;
    }
    /// <summary>
    /// 点击解散按钮
    /// </summary>
    public void OnClickDissolve()
    {
        GameObject temp = TooL.clone(Resources.Load<GameObject>("ZJHPrefab/DissolutionRoomZJH"), GameObject.Find("Canvas"));
        Button[] btnArr = temp.GetComponentsInChildren<Button>();
        btnArr[0].onClick.AddListener(delegate
        {
            com.guojin.mj.net.message.flower.DissolveRoomInfoZJH DSR = com.guojin.mj.net.message.flower.DissolveRoomInfoZJH.Create(MainSceneZJH.Index,int.Parse(RoomSetInfoZJH.roomId));
            SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(DSR));
        });
        btnArr[2].onClick.AddListener(delegate {
            Destroy(temp);
        });
    }
    public static void ReciveMessage(com.guojin.mj.net.message.flower.GameRoomInfoZJH GTI)
    {
        com.guojin.mj.net.message.flower.GameUserInfoZJH GUIO = GTI.sceneUser[GTI.sceneUser.Count - 1];
        Index = GUIO.locationIndex;//确定玩家位置
        SetPlayerPosition();
        chaperNum = GTI.ChapterNums;
        chaper = GTI.ChapterMax;
        for (int i = 0; i < GTI.sceneUser.Count; i++)
        {
            StartShowHeadImage(GTI.sceneUser[i]);
        }
        if (allPeopleNum == 4)
        {
            if (GTI.sceneUser.Count == 4)
            {
                //发送开始游戏消息
                SendMessageStartGame();
            }
            else
            {
                ShowInviteFriends();
            }
        }
        RoomSetInfoZJH.Instance.GetRoomInfo(GTI.roomId, GTI.BeginMoney, GTI.DanZhu, GTI.Zhuangjia);//显示房间信息
        RoomSetInfoZJH.Instance.GetChapter(chaperNum, chaper);
    }
    public static void SetPlayerPosition()
    {
        if (allPeopleNum == 5)
        {
            int[] ARR0 = { 0, 1, 2, 3 ,4};
            int[] ARR1 = { 4, 0, 1, 2 ,3};
            int[] ARR2 = { 3, 4, 0, 1 ,2};
            int[] ARR3 = { 2, 3, 4, 0 ,1};
            int[] ARR4 = { 1, 2, 3, 4, 0};
            switch (Index)
            {
                case 0:
                    intArr = ARR0;
                    break;
                case 1:
                    intArr = ARR1;
                    break;
                case 2:
                    intArr = ARR2;
                    break;
                case 3:
                    intArr = ARR3;
                    break;
                case 4:
                    intArr = ARR4;
                    break;
            }
        }
    }
    /// <summary>
    /// 开局 克隆显示头像 
    /// </summary>
    public static void StartShowHeadImage(com.guojin.mj.net.message.flower.GameUserInfoZJH GUIO)
    {
        GameObject temp = Instantiate(Resources.Load<GameObject>("ZJHPrefab/HeadPhotoZJH"));
        temp.name = "HeadPhotoZJH" + GUIO.locationIndex;
        PlayGameZJH.Instance.ShowHead(intArr[GUIO.locationIndex], temp);
        temp.GetComponent<PlayerInfoZJH>().GetInfo(GUIO.userName, GUIO.score, GUIO.userId,GUIO.ip);
    }
    public static AudioClip[] LoadVoice()
    {
        AudioClip[] AcArr = new AudioClip[38];
        for (int i = 0; i < AcArr.Length; i++)
        {
            AudioClip sp = Resources.Load<AudioClip>("VoiceMan/ZZ" + i);
            AcArr[i] = sp;

        }
        return AcArr;
    }
    /// <summary>
    /// 发送开始消息到服务器
    /// </summary>
    public static void SendMessageStartGame()
    {
        com.guojin.mj.net.message.flower.GameStartInfoZJH cr = new com.guojin.mj.net.message.flower.GameStartInfoZJH();
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(cr));
    }
    public static GameObject CloneSingleBtn(Transform parent, string name)
    {
        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/PGHSinglebtn"));
        temp.transform.SetParent(parent);
        temp.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("MJSprite/MjName/" + name);
        temp.transform.localScale = Vector3.one;
        temp.transform.localPosition = Vector3.zero;
        return temp;
    }
    /// <summary>
    /// 克隆加注，跟，弃牌，比牌到前端 待改
    /// </summary>
    /// <param name="PrefabName"></param>
    /// <param name="SpriteName"></param>
    /// <param name="V3"></param>

    public static void CloneShow(string SpriteName, int weizhi)
    {
        Vector3 V3;
        GameObject temp1 = Instantiate(Resources.Load<GameObject>("MjPrefab/PGH"));
        temp1.GetComponent<OpenAndClose>().CreatShow(Resources.Load<Sprite>("MJSprite/MjName/" + SpriteName));
        temp1.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
        temp1.transform.localScale = Vector3.one;
        switch (intArr[weizhi])
        {
            case 0:
                V3 = new Vector3(203, -90, 0);
                temp1.transform.localPosition = V3;
                break;
            case 1:
                V3 = new Vector3(276, 43, 0);
                temp1.transform.localPosition = V3;
                break;
            case 2:
                V3 = new Vector3(-157, 126, 0);
                temp1.transform.localPosition = V3;
                break;
            default:
                V3 = new Vector3(-296, 43, 0);
                temp1.transform.localPosition = V3;
                break;
        }
    }
    public static void SetPositionStar(int weizhi)
    {
        if (positionStar == null)
        {
            positionStar = Instantiate(Resources.Load<GameObject>("MjPrefab/PositionStar"));
            positionStar.transform.SetParent(GameObject.Find("Center_Img").transform);
            positionStar.transform.localPosition = Vector3.zero;
            positionStar.transform.localScale = Vector3.one;
            positionStar.transform.Rotate(new Vector3(0, 0, 90 * intArr[weizhi] + 90));
        }
        else
        {
            Destroy(positionStar);
            positionStar = Instantiate(Resources.Load<GameObject>("MjPrefab/PositionStar"));
            positionStar.transform.SetParent(GameObject.Find("Center_Img").transform);
            positionStar.transform.localPosition = Vector3.zero;
            positionStar.transform.localScale = Vector3.one;
            positionStar.transform.Rotate(new Vector3(0, 0, 90 * intArr[weizhi] + 90));
        }


    }
    void Update()
    {
        //if(isStart)
        //{
        //    time  += Time.deltaTime;
        //    if (timeNum >= 1)
        //    {
        //        timeNum--;
        //        time = 0;

        //    }
        //    if (isMine && timeNum <= 0)
        //    {
        //        OnClickQiPai();
        //        timeNum = 20;
        //    }
        //}
        if (Input.GetKeyDown(KeyCode.A))
        {
            SendMessageStartGame();
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            com.guojin.mj.net.message.flower.CompareCardInfoZJH cci = com.guojin.mj.net.message.flower.CompareCardInfoZJH.Create(2);
            SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(cci));
        }
    }
}
