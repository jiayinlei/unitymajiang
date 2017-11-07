using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerInfo : MonoBehaviour {

    private GameObject gameOverParent;
    public RawImage Avatar;
    public Text goldNum;
    public Text userName;
    public Text userID;
    public GameObject joinRoom;
    public GameObject OffLine;
    public Sprite hold;
    private string userNames;
    private string avatar;
    private int sex;
    private int gold;
    private int score;
    private int locationIndex;

    private int userId;
    private bool online;
    private string ip;
    public static bool isCopy;
    public static List<int> playerIndex = new List<int>();
    public static List<string> playerName = new List<string>();
    public static List<int> playerID = new List<int>();
    void Awake()
    {
        isCopy = true;
        gameOverParent = GameObject.Find("GameOverParent");
    }

    public void showBusyType()
    {
        if (!OffLine.activeSelf)
        {
            OffLine.SetActive(true);
            OffLine.GetComponent<Image>().sprite = Resources.Load<Sprite>("UIPicture/Playerstat/busying");
        }
    }
    public void CreatPlayerInfo(int socre,string name,int id,string avatar,bool isOnline)
    {
        goldNum.text = socre.ToString();
        userName.text = name;
        userID.text = id.ToString();
        if (!isOnline)
        {
            OffLine.SetActive(true);
            OffLine.GetComponent<Image>().sprite = Resources.Load<Sprite>("UIPicture/Playerstat/KWX_head_offline");
            Method.diaoxianNum++;
        }
        
        StartCoroutine(ImageDownload(avatar));
    }
    public void CreatOnePlayer(int socre, string name, int id, string avatar)
    {
        goldNum.text = socre.ToString();
        userName.text = name;
        userID.text = id.ToString();     
        StartCoroutine(ImageDownload(avatar));
    }
    IEnumerator ImageDownload(string url)
    {
        if (url == null) {
            url = URLConst.DefaultUserHeadUrl;
        }
        WWW www = new WWW(url);
        yield return www;
        Avatar.texture = www.texture;
    }
    public void CreatUserInfo(string name)
    {
        userName.text = "玩家：" + name;
    }
    public void CreatUserInfo1(string name)
    {
        userName.text = "玩家：" + name +"请求解散房间"+"，30秒未取消，默认同意！";
    }
    public void GetPlayerInfo(com.guojin.mj.net.message.game.GameUserInfo GUIO)
    {
        userNames = GUIO.userName;
        avatar = GUIO.avatar;
        sex = GUIO.sex;
        gold = GUIO.gold;
        score = GUIO.score;
        locationIndex = GUIO.locationIndex;
        userId = GUIO.userId;
        online = GUIO.online;
        ip = GUIO.ip;
        playerIndex.Add(GUIO.locationIndex);
        playerName.Add(GUIO.userName);
        playerID.Add(GUIO.userId);
    }
    public void OnClick()
    {
        if(gameOverParent == null)
        {
            Debug.Log("gameOverParent");
        }
        if(isCopy ==true)
        {
            ShowTouXiangInfo();
        }
    }      
    
    void ShowTouXiangInfo()
    {
       
        switch (Method.MJType)
        {           
            case "MaJongZhengZhou":
                GameObject obj = TooL.loadPrefab(gameOverParent, "PlayerInfo");
                PlayerHeadOnClick poc = obj.GetComponent<PlayerHeadOnClick>();
                List<string> distance = Method.showDistanceInfo(locationIndex);
                List<string> name = Method.ReturnName(locationIndex);               
                poc.GetInfo(userNames, sex, userId, ip, Avatar.texture, distance, name);
                isCopy = false;
                break;
            case "MaJongGuShi":
                GameObject obj1 = TooL.loadPrefab(gameOverParent, "PlayerInfoGuShi");
                PlayerHeadOnClick poc1 = obj1.GetComponent<PlayerHeadOnClick>();
                if (PlayerData.PaoZuiDictionary.ContainsKey(locationIndex))
                {
                    poc1.GetInfoGuShi(userNames, sex, userId, Avatar.texture, locationIndex, Method.ReturnIntArr(PlayerData.PaoZuiDictionary[locationIndex]));
                }
                else
                {
                    poc1.GetInfoGuShi(userNames, sex, userId, Avatar.texture, locationIndex, Method.ReturnIntArr(""));
                }
               
                isCopy = false;
                break;
        }
    }                                    
    public void UpdateScore(int socre)         
    {                                          
        goldNum.text = socre.ToString();       
    }                                          
    public void ShowOffLine()                  
    {                                          
        OffLine.SetActive(true);
        OffLine.GetComponent<Image>().sprite = Resources.Load<Sprite>("UIPicture/Playerstat/KWX_head_offline");
    }                                          
    public void HiddenOffLine()                
    {                                          
        OffLine.SetActive(false);              
    }                                          
   public void showHold()
    {
        joinRoom.SetActive(true);
    }  
    public void changeState()
    {
        joinRoom.GetComponent<RectTransform>().sizeDelta = new Vector2(76,25);
        joinRoom.GetComponent<Image>().sprite = hold;
    }
    public void setFalseHold()
    {
        joinRoom.SetActive(false);
    }
    public void showplayerinfo(string username, string soc)
    {
        userName.text = username;
        goldNum.text = soc;
    }
    public void showZhuang()
    {
        OffLine.SetActive(true);
    }
    public void NoshowZhuang()
    {
        OffLine.SetActive(false);
    }
    public void setweizhi()
    {
        goldNum.gameObject.transform.localPosition = new Vector3(92,-22,0);
        userName.gameObject.transform.localPosition = new Vector3(108,14,0);
    }
    public void HideHead()
    {
        //Color col = transform.GetComponent<Image>().color;
        //transform.GetComponent<Image>().color = new Color(col.r,col.g,col.b,0);
        hideObj(gameObject);
        //hideObj(Avatar.gameObject);
        Color col = Avatar.GetComponent<RawImage>().color;
        Avatar.GetComponent<RawImage>().color = new Color(col.r, col.g, col.b, 0);
        // hideObj(goldNum.gameObject);

    }
    public void hideObj(GameObject obj)
    {
        Color col = obj.GetComponent<Image>().color;
        obj.GetComponent<Image>().color = new Color(col.r, col.g, col.b, 0);
    }
    public GameObject objzui;
    public void ShowZui()
    {
        objzui.SetActive(true);    
    }
    public void SetFalseZui()
    {
        objzui.SetActive(false);
    }
}                                                                     
