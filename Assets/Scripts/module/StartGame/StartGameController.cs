using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class StartGameController : MonoBehaviour
{

    public GameObject ExitBtnOBJ;
    public static int TogleNum = 0;
    public GameObject GameInfo;//房间信息父对象
    public Text info;//牌局信息
    public Text info1;//牌局信息
    public Text paiju;//牌的局数信息
    public Text infodise;
    public Text info1dise;
    public Text RoomIDdise;
    public GameObject voiceBtn;
    public GameObject plane;


    GameObject CloneGameobj;//要克隆的对象
    Transform parent;//克隆对象的父物体
    Transform parentFa;//发牌的父对象
    Text timeText;
    int[] intArr = { 1, 5, 3, 10, 4, 6, 8, 2, 6, 6, 25, 30, 15, 32 };
    int[] intarr2 = { 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6 };
    GameObject childd;
    void Awake()
    {
        Method.CanAddMessage = false;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        AwakeAfter();
    }
    void Start()
    {    
        TogleNum = 0;
        //over
        Method.isMine = false;
        Method.isPeng = false;
        Method.isHor = true; 
        Method.State = (int)State.gameStart;
        SetBtnFalse();
        Method.touxiangL.Clear();
        Method.recivePaiId = -1;
        Method.huier = -1;
        Method.zhaungweizhi = -1;


        Method.huier = -1;
        Method.recivePaiId = -1;
        Method.zhaungweizhi = -1;


        if (!Method.isChongLian && !GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback)
        {
            Method.isFirJu = true;         
            Method.isCreater = false;
            
            Method.ReciveMessageVertical();           
            sendReadyMess();
        }
        else
        {
            Method.isReciveReady = true;
         
            if (GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback)
            {              
                MaJong.MaJongManage.Instance().MaJong.ChongLian(Method.message);
                Method.tranList[1].GetComponent<GridLayoutGroup>().spacing = new Vector2(-2.77f, 0);
                Method.tranList[4].GetComponent<GridLayoutGroup>().spacing = new Vector2(0, 0);
                Method.tranList[4].localPosition = new Vector3(535, 255, 0);
                Method.tranList[7].GetComponent<GridLayoutGroup>().spacing = new Vector2(-1f, 0);
                Method.tranList[7].localPosition = new Vector3(-222, 357.5f, 0);
                Method.tranList[10].GetComponent<GridLayoutGroup>().spacing = new Vector2(0, 0);
                Method.tranList[10].localPosition = new Vector3(-533, -132, 0);
           
                Method.tranList[13].localPosition = new Vector3(-273, 345, 0);
                Method.tranList[14].localPosition = new Vector3(-500, -150, 0);
            }
            else
            {
                if (Method.isChongLian)
                {                
                    Method.isChongLian = false;
                    destroyGameOBJ();
                    MaJong.MaJongManage.Instance().MaJong.ChongLian(Method.message);  
                    sendReadyMess();
                    if (Method.messagelist.Count!=0)
                    {
                        for (int i = 0; i < Method.messagelist.Count; i++)
                        {
                            SocketMessageQueue.GetInstance().addMsg(Method.messagelist[i]);
                        }
                    }                  
                }

            }
        }
    }

    float num = 0;     
    void sendReadyMess()
    {
        Debug.Log("发送了一次");
        com.guojin.mj.net.message.game.JoinRoomReady JR = new com.guojin.mj.net.message.game.JoinRoomReady();
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(JR));
    }
    void initData()
    {
        Method.recivePaiId = -1;
        Method.huier = -1;
        Method.zhaungweizhi = -1;
    }
    void SetBtnFalse()
    {
        if (GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback)
        {
            voiceBtn.SetActive(false);
        }
    }

    void AwakeAfter()
    {
        InitUI();
        InitParentTransf();
        AddClick();
        Method.username0 = "";
        Method.username1 = "";
        Method.username2 = "";
        Method.username3 = "";
    }
    public void ShowGameRoomInfo(string roomid)
    {
        info.text = PlayerData.peopletype + "/" + PlayerData.xuanpaotype;
        info1.text = PlayerData.hupaitype + "/" + PlayerData.huntype;
        info1dise.text = PlayerData.hupaitype + "/" + PlayerData.huntype;
        paiju.text = "/" + PlayerData.jutype + "局";
        infodise.text = PlayerData.peopletype + "/" + PlayerData.xuanpaotype;
        RoomIDdise.text = "房号：" + roomid;

    }
    public void showJUnum()
    {
        paiju.text = "/" + PlayerData.jutype + "局";
    }
    public void destroyGameOBJ()
    {
        if (GameInfo != null)
        {
            Destroy(GameInfo);
        }

    }

 
    public void SendStartGame()
    {
        com.guojin.mj.net.message.game.GameChapterStart cr = new com.guojin.mj.net.message.game.GameChapterStart();
        SocketMgr.GetInstance().webSocket.Send(com.guojin.mj.net.Net.instance.write(cr));
    }
   

    static GameObject SET;
    public void AddClick()
    {
        Method.SettingBTN = ExitBtnOBJ;
        ExitBtnOBJ.GetComponent<Button>().onClick.AddListener(delegate
        {
            VoiceManger.Instance.BtnVoice();
            if (SET == null)
            {
                GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/setting"));
                temp.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
                temp.transform.localPosition = /*Vector3.zero*/new Vector3(0, 360, 0);
                temp.transform.localScale = Vector3.one;
                SET = temp;
            }
            Method.SettingBTN.SetActive(false);

        });
    }
    public void CloneSingleBtn(Transform parent, string name)
    {
        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/PGHSinglebtn"));
        temp.transform.SetParent(parent);
        temp.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("MJSprite/MjName/" + name);
        temp.transform.localScale = Vector3.one;
        temp.transform.localPosition = Vector3.zero;
    }


    public static int numID;
    Transform panel;
    /// <summary>
    /// 自动插入排序
    /// </summary>
    public void Voluntarily(int id, GameObject go)
    {
        MJInfo[] tranChild = parent.GetComponentsInChildren<MJInfo>();
        for (int i = 0; i < tranChild.Length; i++)
        {
            //tranChild[i].localPosition = new Vector3(i*-47,0,0);   
            if (id >= tranChild[0].Id1)
            {
                go.transform.SetParent(parent);
                go.transform.localPosition = new Vector3(0, 0, 0);
                go.transform.localScale = Vector3.one;

                return;
            }
            else if (id < tranChild[i].Id1)
            {
                go.transform.SetParent(parent);
                go.transform.localPosition = new Vector3(i * -47, 0, 0);
                go.transform.localScale = Vector3.one;
                return;
            }
            else if (id > tranChild[tranChild.Length - 1].Id1)
            {
                go.transform.SetParent(parent);
                go.transform.localPosition = new Vector3((tranChild.Length - 1) * -47, 0, 0);
                go.transform.localScale = Vector3.one;
                return;
            }
        }
    }

    public void InitUI()
    {
        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/PanleManger"));
        temp.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
        temp.name = "PanleManger";
        temp.transform.localScale = Vector3.one;
        temp.transform.localPosition = Vector3.zero;
        parent = GameObject.Find("MahjongContent0").transform;
        parentFa = GameObject.Find("ReceiveContent0").transform;
        CloneGameobj = Resources.Load<GameObject>("MjPrefab/MaJiang");
        Method.OutPai0 = GameObject.Find("CHUMahjongContent0").transform;
        Method.parent = parent;
        Method.goParent = parentFa;
        Method.clon = CloneGameobj;
        Method.tranList.Clear();
        Method.tranList.Add(GameObject.Find("PengMajong0").transform);
        Method.tranList.Add(GameObject.Find("MahjongContent0").transform);
        Method.tranList.Add(GameObject.Find("CHUMahjongContent0").transform);
        Method.tranList.Add(GameObject.Find("PengMajong1").transform);
        Method.tranList.Add(GameObject.Find("MahjongContent1").transform);
        Method.tranList.Add(GameObject.Find("CHUMahjongContent1").transform);
        Method.tranList.Add(GameObject.Find("PengMajong2").transform);
        Method.tranList.Add(GameObject.Find("MahjongContent2").transform);
        Method.tranList.Add(GameObject.Find("CHUMahjongContent2").transform);
        Method.tranList.Add(GameObject.Find("PengMajong3").transform);
        Method.tranList.Add(GameObject.Find("MahjongContent3").transform);
        Method.tranList.Add(GameObject.Find("CHUMahjongContent3").transform);
        Method.tranList.Add(GameObject.Find("ReceiveContent1").transform);
        Method.tranList.Add(GameObject.Find("ReceiveContent2").transform);
        Method.tranList.Add(GameObject.Find("ReceiveContent3").transform);
        Method.setFalse();

    }
    public void InitParentTransf()
    {
        Method.objList.Add(Resources.Load<GameObject>("MjPrefab/MaJiang"));
        Method.objList.Add(Resources.Load<GameObject>("MjPrefab/OutPai0"));
        Method.objList.Add(Resources.Load<GameObject>("MjPrefab/MaJiangRight"));
        Method.objList.Add(Resources.Load<GameObject>("MjPrefab/OutPai1"));
        Method.objList.Add(Resources.Load<GameObject>("MjPrefab/MaJiangUP"));
        Method.objList.Add(Resources.Load<GameObject>("MjPrefab/OutPai2"));
        Method.objList.Add(Resources.Load<GameObject>("MjPrefab/MaJiangLeft"));
        Method.objList.Add(Resources.Load<GameObject>("MjPrefab/OutPai3"));
        Method.MjNameSp = Method.LoadSprite();
        // todo jia 2017/07/26
        Method.mjdise = Method.LoadDiSe();
        Method.fangxiang = Method.loadFangxiang();// over

        Method.LoadPrefabs("peng");
        Method.LoadPrefabs("GangMajong");
        Method.LoadPrefabs("AnGangMajong");

    }    
}
