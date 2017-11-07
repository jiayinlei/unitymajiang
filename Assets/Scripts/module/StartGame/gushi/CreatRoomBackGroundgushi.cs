using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.guojin.mj.net.message;
using com.guojin.mj.net.message.login;

public class CreatRoomBackGroundgushi : Observer
{

    private string playCount = "4";// 牌局  1
    private string pao = "4";// 跑 1
    private string paozui = "false";// 跑嘴 1
    private string hupai = "0";// 胡牌  1
    private string daifengpai = "false";// 带风  1
    private string duanmen = "false";// 断门 1
    private string dui7 = "false";// 对7
    private string zhuangjia = "false";// 庄家 1
    private string gangkai = "false";// 杠开
    string peopelNum = "4";//人数
    private string difen = "1";// 底分 1
    protected override string[] GetMsgList()
    {
        return new string[] {
                  MessageFactoryImpi.instance.getMessageString(7,1),
                  MessageFactoryImpi.instance.getMessageString(7,7),
                  MessageFactoryImpi.instance.getMessageString(7,11),
                  //MessageFactoryImpi.instance.getMessageString(1,37),
        };
    }
    //4: 4局(2房卡)
    //8: 8局(4房卡) 
    //16: 16局(8房卡)
    public void OnClickPlayCount4()
    {
        LoginAndShare.Controller.BtnVoice();
        this.playCount = "4";
    }
    public void OnClickPlayCount8()
    {
        LoginAndShare.Controller.BtnVoice();
        this.playCount = "8";
    }
    public void OnClickPlayCount16()
    {
        LoginAndShare.Controller.BtnVoice();
        this.playCount = "16";
    }

    //胡牌类型
    public void OnClickHuPai1()
    {
        LoginAndShare.Controller.BtnVoice();
        this.hupai = "0";
    }
    public void OnClickHuPai2()
    {
        LoginAndShare.Controller.BtnVoice();
        this.hupai = "1";
    }
    public void OnClickHuPaizimo()
    {
        LoginAndShare.Controller.BtnVoice();
        this.hupai = "2";
    }



    //底分
    public void OnClickdifen1()
    {
        LoginAndShare.Controller.BtnVoice();
        this.difen = "1";
    }
    public void OnClickdifen2()
    {
        LoginAndShare.Controller.BtnVoice();
        this.difen = "2";
    }
    public void OnClickdifen3()
    {
        LoginAndShare.Controller.BtnVoice();
        this.difen = "3";
    }
    public void OnClickdifen4()
    {
        LoginAndShare.Controller.BtnVoice();
        this.difen = "4";
    }
    public void OnClickdifen5()
    {
        LoginAndShare.Controller.BtnVoice();
        this.difen = "5";
    }


    //选跑 4: 4局选跑 1:每局选跑
    // 跑1
    public void OnClickPao1()
    {
        LoginAndShare.Controller.BtnVoice();
        this.pao = "1";
    }
    // 跑4
    public void OnClickPao4()
    {
        LoginAndShare.Controller.BtnVoice();
        this.pao = "4";
    }
    //玩家人数 三人 四人
    public void OnClickPeople4()
    {
        LoginAndShare.Controller.BtnVoice();
        peopelNum = "4";
    }
    // 跑4
    public void OnClickPeople3()
    {
        LoginAndShare.Controller.BtnVoice();
        peopelNum = "3";
    }

    //跑嘴
    int ispaozui = 0;
    public void OnClickPaoZuiYes()
    {
        LoginAndShare.Controller.BtnVoice();
        if (ispaozui == 0)
        {
            ispaozui = 1;
            paozui = "true";

        }
        else
        {
            ispaozui = 0;
            paozui = "false";
        }
        Debug.Log(paozui);
    }
    public void OnClickHunNo()
    {
        LoginAndShare.Controller.BtnVoice();
        this.paozui = "false";
    }
    //   false自摸胡, true点炮胡
    // 点炮胡
    public void OnClickDianPaoHu()
    {
        LoginAndShare.Controller.BtnVoice();
        this.hupai = "true";
    }
    // 自模糊
    public void OnClickZiMoHu()
    {
        LoginAndShare.Controller.BtnVoice();
        this.hupai = "false";

    }
    // 带字牌
    int isdaifeng = 0;
    public void OnClickDaiFengPaiYes()
    {   
        LoginAndShare.Controller.BtnVoice();
        if (isdaifeng == 0)
        {
            isdaifeng = 1;
            daifengpai = "true";

        }
        else
        {
            isdaifeng = 0;
            daifengpai = "false";
        }
        Debug.Log(daifengpai);

    }
    // 不带字牌
    public void OnClickDaiZiPaiNo()
    {
        LoginAndShare.Controller.BtnVoice();
        this.daifengpai = "false";

    }
    // 断门
    int isduanmen = 0;
    public void OnClickDuanMenYes()
    {
        LoginAndShare.Controller.BtnVoice();
        if (isduanmen == 0)
        {
            isduanmen = 1;
            duanmen = "true";

        }
        else
        {
            isduanmen = 0;
            duanmen = "false";
        }
        Debug.Log(duanmen);
    }
    public void OnClickDaiGangPaoNo()
    {
        LoginAndShare.Controller.BtnVoice();
        this.duanmen = "false";
    }
    // 七对杠
    public void OnClickDui7Yes()
    {
        LoginAndShare.Controller.BtnVoice();
        this.dui7 = "true";
    }
    public void OnClickDui7No()
    {
        LoginAndShare.Controller.BtnVoice();
        this.dui7 = "false";
    }
    // 庄家
    int iszhuangjia = 0;
    public void OnClickZhuangJiaYes()
    {
        LoginAndShare.Controller.BtnVoice();
        if (iszhuangjia == 0)
        {
            iszhuangjia = 1;
            zhuangjia = "true";

        }
        else
        {
            iszhuangjia = 0;
            zhuangjia = "false";
        }
        Debug.Log(zhuangjia);

    }
    public void OnClickZhuangJiaNo()
    {
        LoginAndShare.Controller.BtnVoice();
        this.zhuangjia = "false";
    }
    // 杠开
    int isgangkai = 0;
    public void OnClickGangaKaiYes()
    {
        LoginAndShare.Controller.BtnVoice();
        if (isgangkai == 0)
        {
            isgangkai = 1;
            gangkai = "true";

        }
        else
        {
            isgangkai = 0;
            gangkai = "false";
        }
        Debug.Log(gangkai);
    }
    public void OnClickGangKaiNo()
    {
        LoginAndShare.Controller.BtnVoice();
        this.gangkai = "false";
    }
    int qidui = 0;
    public void onvalueChangeDui7()
    {
        LoginAndShare.Controller.BtnVoice();
        if (qidui == 0)
        {
            qidui = 1;
            dui7 = "true";

        }
        else
        {
            qidui = 0;
            dui7 = "false";
        }
    }
    public void OnValueChange(int a, string T, string F, string valuename)
    {
        LoginAndShare.Controller.BtnVoice();
        if (a == 1)
        {
            a = 0;
            valuename = T;
        }
        else
        {
            a = 1;
            valuename = F;
        }
    }

    public override void OnMsg(string msg, com.guojin.core.io.message.Message data)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(7, 1))
        {// 创建房间返回
            CreateRoomRet netData = (CreateRoomRet)data;
            if (netData.result)
            {
                com.guojin.mj.net.message.login.JoinRoom JR = com.guojin.mj.net.message.login.JoinRoom.create(netData.roomCheckId);
                SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(JR));
            }           
        }
        if (msg == MessageFactoryImpi.instance.getMessageString(7, 7))
        {
            JoinRoomRet netData = (JoinRoomRet)data;
            if (netData.result)
            {
                com.guojin.mj.net.message.game.GameJoinRoom GJM = new com.guojin.mj.net.message.game.GameJoinRoom();
                SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(GJM));
                //LoadWaitStart();
                Destroy(transform.parent.parent.gameObject);

            }

        }

        //if (msg == MessageFactoryImpi.instance.getMessageString(1, 37))
        //{
        //    com.guojin.mj.net.message.game.GameRoomInfoGuShi gr = (com.guojin.mj.net.message.game.GameRoomInfoGuShi)data;
        //    Debug.Log("创建房间成功！！");
        //}
        //if (msg == MessageFactoryImpi.instance.getMessageString(1, 7))
        //{
        //    com.guojin.mj.net.message.game.GameRoomInfo GJM = (com.guojin.mj.net.message.game.GameRoomInfo)data;
        //    Method.GRI = GJM;
        //    Method.isCreater = true;
        //    Method.maJongJuNum = GJM.leftChapterNums.ToString();
        //    GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_LoadingPage);
        //    //SceneManager.LoadScene("Horizontal");

        //}
        ReciveNotice(msg, data);
    }
    //void LoadWaitStart()
    //{
    //    GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/WaitStartGame"));
    //    temp.transform.SetParent(GameObject.Find("addNode").transform);
    //    temp.transform.localScale = Vector3.one;
    //    temp.transform.localPosition = Vector3.zero;
    //    Method.readyGame = temp;
    //}

    void Start()
    {
        this.initMsg();
    }
    void ReciveNotice(string msg, com.guojin.core.io.message.Message data)
    {
        if (msg == MessageFactoryImpi.instance.getMessageString(7, 11))
        {
            com.guojin.mj.net.message.login.Notice notice = (com.guojin.mj.net.message.login.Notice)data;
            if (PlayerData.dictionary.ContainsKey(notice.key))
            {
                GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/JoinRoomDef"));
                temp.transform.SetParent(gameObject.transform.parent);
                temp.transform.localPosition = Vector3.zero;
                temp.transform.localScale = Vector3.one;
                temp.GetComponentInChildren<RuleLayer>().ShowText(PlayerData.dictionary[notice.key]);
            }
        }
    }
    public void sendCreatRoomInfo()
    {
        // playCount peopelNum  hupai difen pao duanmen paozui daifengpai zhuangjia
        List<com.guojin.mj.net.message.login.OptionEntry> OptionList = new List<com.guojin.mj.net.message.login.OptionEntry>();
        OptionList.Add(com.guojin.mj.net.message.login.OptionEntry.create("chapterMax", this.playCount));//局数  
        OptionList.Add(com.guojin.mj.net.message.login.OptionEntry.create("user_num", peopelNum));//人数       待改正
        OptionList.Add(com.guojin.mj.net.message.login.OptionEntry.create("IS_FANG_PAO", this.hupai));// false自摸胡, true点炮胡   待改正
        OptionList.Add(com.guojin.mj.net.message.login.OptionEntry.create("IS_BUDAIFENG", this.daifengpai));// 带风
        OptionList.Add(com.guojin.mj.net.message.login.OptionEntry.create("QUE_YI_MEN", this.duanmen));// 断门
        OptionList.Add(com.guojin.mj.net.message.login.OptionEntry.create("XUAN_PAO_COUNT", this.pao));// 选跑
        OptionList.Add(com.guojin.mj.net.message.login.OptionEntry.create("DI_FEN", this.difen));// 底分
        OptionList.Add(com.guojin.mj.net.message.login.OptionEntry.create("IS_ZHUANG_JIA_DI", this.zhuangjia));// 庄家跑底     
        OptionList.Add(com.guojin.mj.net.message.login.OptionEntry.create("IS_PAO_ZUI", this.paozui));// 跑嘴      
        OptionList.Add(com.guojin.mj.net.message.login.OptionEntry.create("MA_JIANG_TYPE", "gushi"));//麻将类型    
        Debug.Log(pao+"++++++++++++++++");
        
        com.guojin.mj.net.message.login.CreateRoom CR = com.guojin.mj.net.message.login.CreateRoom.create("mj", OptionList);
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(CR));
    }
    // 点击创建房间
    bool isDianji = true;
    public void OnClickCreateRoomBtn()
    {
        //Method.CloneMask(transform.Find("background/CreatroomBtn"));
        if (isDianji)
        {
            isDianji = false;            
            sendCreatRoomInfo();
            LoginAndShare.Controller.BtnVoice();
            StartCoroutine(ChangeIsDianji());
        }        
    }
    IEnumerator ChangeIsDianji()
    {
        yield return new WaitForSeconds(1.5f);
        isDianji = true;
    }
 

    public void OnClickRulerBtn()
    {
        LoginAndShare.Controller.BtnVoice();
        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/RulePanlgishi"));
        temp.transform.SetParent(transform.parent.parent);
        temp.transform.localPosition = Vector3.zero;
        temp.transform.localScale = Vector3.one;
    }
    public void OnClickClose()
    {
        Destroy(transform.parent.parent.gameObject);
    }
}

