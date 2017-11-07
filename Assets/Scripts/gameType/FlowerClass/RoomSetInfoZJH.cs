using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class RoomSetInfoZJH : MonoBehaviour {

    public Text m_roomId;//房间号
    public Text m_beginMoney;//初始金币
    public Text m_roomJuNum;//第几局
    public Text m_roomDanZhu;//单注
    public Text m_roomRule;//规则
    public static Text m_roomAllZhu;//总注

    public static RoomSetInfoZJH Instance;
    public static string roomId;
    public static int beginMoney;
    public static int chapterNums;
    public static int chapterMax;
    public static int danZhu;
    public static bool roomRule;
    public static int allMoney;//所以玩家压得总注


    public void GetRoomInfo(string _roomId,int _beginMoney,int _danZhu,bool _rule)
    {
        roomId = _roomId;
        beginMoney = _beginMoney;
        danZhu = _danZhu;
        roomRule = _rule;
        m_roomId.text = "房间号：" + roomId;
        m_roomDanZhu.text = "单注：" + danZhu.ToString();
        if (roomRule)
        {
            m_roomRule.text = "赢家坐庄";
        }
        else
        {
            m_roomRule.text = "轮流坐庄";
        }


    }
    void Awake () {
        Instance = this;
        m_roomAllZhu = transform.Find("Text").GetComponent<Text>();
	}
	public void GetAllMoney(int zhu)
    {
        allMoney += zhu;
        m_roomAllZhu.text = allMoney.ToString();
    }
    public void Clear()
    {
        allMoney = 0;
        m_roomAllZhu.text = allMoney.ToString();
    }
    public void GetChapter(int num,int max)
    {
        chapterNums = num;
        chapterMax = max;
        m_roomJuNum.text = "第" + chapterNums.ToString() + "/" + chapterMax.ToString() + "局";
    }
    void Update () {
        m_beginMoney.text = DateTime.Now.ToString("HH:mm:ss");
    }
}
