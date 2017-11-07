using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using com.guojin.mj.net.message;
using com.guojin.mj.net.message.login;
using UnityEngine.SceneManagement;

public class CreateRoomLayer : Observer
{
    private string playCount = "4";// 牌局
    private string pao = "4";// 跑
    private string hun = "false";// 混
    private string hupai = "true";// 胡牌
    private string daizipai = "false";// 带字跑
    private string daigangpao = "false";// 带杠跑
    private string dui7 = "false";// 对7
    private string zhuangjia = "false";// 庄家
    private string gangkai = "false";// 杠开
    string peopelNum = "4";//人数
    public Image zhenghzou;
    public Image xinxiang;
    public Image kaifeng;
    public Sprite Zhengzhou1;
    public Sprite Zhengzhou2;
    public Sprite XinXiang1;
    public Sprite XinXiang2;
    public Sprite KaiFeng1;
    public Sprite KaiFeng2;
    //Button[] arr;//获取创建房间界面的 Button 
    // Toggle[] TG;//接收创建房间界面的toggle
    private GameObject OKButton_;
    private GameObject CancleButton_;
      //protected override string[] GetMsgList()
    //{
    //    return new string[] {
    //              MessageFactoryImpi.instance.getMessageString(7,1),
    //              MessageFactoryImpi.instance.getMessageString(7,7),
    //              MessageFactoryImpi.instance.getMessageString(1,7),
    //              MessageFactoryImpi.instance.getMessageString(7,11),
    //    };
    //}
    ////4: 4局(2房卡)
    ////8: 8局(4房卡) 
    ////16: 16局(8房卡)
    //public void OnClickPlayCount4()
    //{
    //    this.playCount = "4";
    //}
    //public void OnClickPlayCount8()
    //{
    //    this.playCount = "8";
    //}
    //public void OnClickPlayCount16()
    //{
    //    this.playCount = "16";
    //}

    ////选跑 4: 4局选跑 1:每局选跑
    //// 跑1
    //public void OnClickPao1()
    //{
    //    this.pao = "1";
    //}
    //// 跑4
    //public void OnClickPao4()
    //{
    //    this.pao = "4";
    //}
    ////玩家人数 三人 四人
    //public void OnClickPeople4()
    //{
    //    peopelNum = "4";
    //}
    //// 跑4
    //public void OnClickPeople3()
    //{
    //    peopelNum = "3";
    //}

    //int ishun = 0;
    //public void OnClickHunYes()
    //{      
    //    if (ishun == 0)
    //    {
    //        ishun = 1;
    //        hun = "true";

    //    }
    //    else
    //    {
    //        ishun = 0;
    //        hun = "false";
    //    }
    //    Debug.Log(hun);
    //}
    //public void OnClickHunNo()
    //{
    //    this.hun = "false";
    //}
    ////   false自摸胡, true点炮胡
    //// 点炮胡
    //public void OnClickDianPaoHu()
    //{
    //    this.hupai = "true";
    //}
    //// 自模糊
    //public void OnClickZiMoHu()
    //{
    //    this.hupai = "false";
    //}
    //// 带字牌
    //int isdaizipai = 0;
    //public void OnClickDaiZiPaiYes()
    //{       
    //    if (isdaizipai == 0)
    //    {
    //        isdaizipai = 1;
    //        daizipai = "true";

    //    }
    //    else
    //    {
    //        isdaizipai = 0;
    //        daizipai = "false";
    //    }
    //    Debug.Log(daizipai);
    //}
    //// 不带字牌
    //public void OnClickDaiZiPaiNo()
    //{
    //    this.daizipai = "false";
    //}
    //// 带杠跑
    //int isdaigangpao = 0;
    //public void OnClickDaiGangPaoYes()
    //{
    //    if (isdaigangpao == 0)
    //    {
    //        isdaigangpao = 1;
    //        daigangpao = "true";

    //    }
    //    else
    //    {
    //        isdaigangpao = 0;
    //        daigangpao = "false";
    //    }
    //    Debug.Log(daigangpao);      
    //}
    //public void OnClickDaiGangPaoNo()
    //{
    //    this.daigangpao = "false";
    //}
    //// 七对杠
    //public void OnClickDui7Yes()
    //{
    //    this.dui7 = "true";
    //}
    //public void OnClickDui7No()
    //{
    //    this.dui7 = "false";
    //}
    //// 庄家
    //int iszhuangjia = 0;
    //public void OnClickZhuangJiaYes()
    //{
    //    if (iszhuangjia == 0)
    //    {
    //        iszhuangjia = 1;
    //        zhuangjia = "true";

    //    }
    //    else
    //    {
    //        iszhuangjia = 0;
    //        zhuangjia = "false";
    //    }
    //    Debug.Log(zhuangjia);       

    //}
    //public void OnClickZhuangJiaNo()
    //{
    //    this.zhuangjia = "false";
    //}
    //// 杠开
    //int isgangkai = 0;
    //public void OnClickGangaKaiYes()
    //{
    //    if (isgangkai == 0)
    //    {
    //        isgangkai = 1;
    //        gangkai = "true";

    //    }
    //    else
    //    {
    //        isgangkai = 0;
    //        gangkai = "false";
    //    }
    //    Debug.Log(gangkai);        
    //}
    //public void OnClickGangKaiNo()
    //{
    //    this.gangkai = "false";
    //}
    //int qidui = 0;
    //public void onvalueChangeDui7()
    //{
    //    if (qidui == 0)
    //    {
    //        qidui = 1;
    //        dui7 = "true";

    //    }
    //    else
    //    {
    //        qidui = 0;
    //        dui7 = "false";
    //    }
    //    Debug.Log(dui7);
    //}
    //public void OnValueChange(int a,string T,string F,string valuename)
    //{
    //    if (a == 1)
    //    {
    //        a = 0;
    //        valuename = T;            
    //    }
    //    else
    //    {
    //        a = 1;
    //        valuename = F;            
    //    }
    //}

    //public override void OnMsg(string msg, com.guojin.core.io.message.Message data)
    //{
    //    if (msg == MessageFactoryImpi.instance.getMessageString(7, 1))
    //    {// 创建房间返回
    //        CreateRoomRet netData = (CreateRoomRet)data;
    //        if (netData.result)
    //        {
    //            com.guojin.mj.net.message.login.JoinRoom JR = com.guojin.mj.net.message.login.JoinRoom.create(netData.roomCheckId);
    //            SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(JR));
    //        }
    //        else
    //        {
    //            //GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/JoinRoomDef"));
    //            //temp.transform.SetParent(gameObject.transform.parent);
    //            //temp.transform.localPosition = Vector3.zero;
    //            //temp.transform.localScale = Vector3.one;
    //            //temp.GetComponentInChildren<RuleLayer>().ShowText("创建房间失败！！");
    //        }
    //    }
    //    if (msg == MessageFactoryImpi.instance.getMessageString(7, 7))
    //    {
    //        JoinRoomRet netData = (JoinRoomRet)data;
    //        if (netData.result)
    //        {
    //            com.guojin.mj.net.message.game.GameJoinRoom GJM = new com.guojin.mj.net.message.game.GameJoinRoom();
    //            SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(GJM));
    //        }

    //    }
    //    if (msg == MessageFactoryImpi.instance.getMessageString(1, 7))
    //    {
    //        com.guojin.mj.net.message.game.GameRoomInfo GJM = (com.guojin.mj.net.message.game.GameRoomInfo)data;
    //        Method.GRI = GJM;
    //        Method.isCreater = true;
    //        Method.maJongJuNum = GJM.leftChapterNums.ToString();
    //        GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_LoadingPage);
    //        //SceneManager.LoadScene("Horizontal");

    //    }
    //    ReciveNotice(msg,data);
    //}


    //void Start()
    //{
    //    this.initMsg();
    //}
    //void ReciveNotice(string msg, com.guojin.core.io.message.Message data)
    //{
    //    if (msg== MessageFactoryImpi.instance.getMessageString(7,11))
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
    //public void sendCreatRoomInfo()
    //{
    //    List<com.guojin.mj.net.message.login.OptionEntry> OptionList = new List<com.guojin.mj.net.message.login.OptionEntry>();
    //    OptionList.Add(com.guojin.mj.net.message.login.OptionEntry.create("chapterMax", this.playCount));//局数  
    //    OptionList.Add(com.guojin.mj.net.message.login.OptionEntry.create("XUAN_PAO_COUNT", this.pao));//选跑 4: 4局选跑 1:每局选跑 
    //    OptionList.Add(com.guojin.mj.net.message.login.OptionEntry.create("IS_FANG_PAO", this.hupai));// false自摸胡, true点炮胡
    //    OptionList.Add(com.guojin.mj.net.message.login.OptionEntry.create("IS_HUIER", this.hun));// 是否带混儿 true带, false 不带 
    //    OptionList.Add(com.guojin.mj.net.message.login.OptionEntry.create("IS_GANG_DAI_PAO", this.daigangpao));// 杠带跑
    //    OptionList.Add(com.guojin.mj.net.message.login.OptionEntry.create("IS_DAI_ZI_PAI", this.daizipai));// 带字牌
    //    OptionList.Add(com.guojin.mj.net.message.login.OptionEntry.create("IS_QI_DUI_FAN_BEI", this.dui7));//七对翻倍
    //    OptionList.Add(com.guojin.mj.net.message.login.OptionEntry.create("IS_ZHUANG_JIA_DI", this.zhuangjia));//庄家加倍
    //    OptionList.Add(com.guojin.mj.net.message.login.OptionEntry.create("IS_GANG_KAI_FAN_BEI", this.gangkai));//杠开翻倍
    //    OptionList.Add(com.guojin.mj.net.message.login.OptionEntry.create("user_num", peopelNum));//用户个数
    //    com.guojin.mj.net.message.login.CreateRoom CR = com.guojin.mj.net.message.login.CreateRoom.create("mj", OptionList);
    //    SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(CR));
    //}
    //// 点击创建房间
    //public void OnClickCreateRoomBtn()
    //{
    //    sendCreatRoomInfo();
    //}
    // 点击取消
    public void OnClickCancleBtn()
    {
        LoginAndShare.Controller.BtnVoice();
        Destroy(transform.parent.gameObject);
    }
    public void SetSizeS(Image image)
    {       
        image.rectTransform.sizeDelta=new Vector2(169, 63);
      
    }
    public void SetSizeM(Image image)
    {
        image.rectTransform.sizeDelta = new Vector2(187, 63);

    }
    //int num = 0;
    public void BtnZhengZhou()
    {
        LoginAndShare.Controller.BtnVoice();
        zhenghzou.sprite = PPTextureManage.getInstance().LoadAtlasSprite("Atlas/CreatRoom", "hover_btn");
        xinxiang.sprite = PPTextureManage.getInstance().LoadAtlasSprite("Atlas/CreatRoom", "xinxiangmj");
        kaifeng.sprite = PPTextureManage.getInstance().LoadAtlasSprite("Atlas/CreatRoom", "kaifengmj");
        //SetSizeM(zhenghzou);
        //SetSizeS(xinxiang);
        //SetSizeS(kaifeng);
        //if (num!=0)
        //{
        //    Destroy(GameObject.Find("background"));
        //    CloneBackGround("backgroundzhengzhou");
        //    num = 0;
        //}       
    }
    public void BtnXinXiang()
    {
        zhenghzou.sprite = PPTextureManage.getInstance().LoadAtlasSprite("Atlas/CreatRoom", "zzmj");
        xinxiang.sprite = PPTextureManage.getInstance().LoadAtlasSprite("Atlas/CreatRoom", "xinxiangbg");
        kaifeng.sprite = PPTextureManage.getInstance().LoadAtlasSprite("Atlas/CreatRoom", "kaifengmj");
        //SetSizeM(xinxiang);
        //SetSizeS(zhenghzou);
        //SetSizeS(kaifeng);
        //if (num!=1)
        //{
        //    Destroy(GameObject.Find("background"));
        //    CloneBackGround("backgroundxinxiang");
        //    num = 1;
        //}     
    }
    public void BtnKaiFeng()
    {
        zhenghzou.sprite = PPTextureManage.getInstance().LoadAtlasSprite("Atlas/CreatRoom", "zzmj");
        xinxiang.sprite = PPTextureManage.getInstance().LoadAtlasSprite("Atlas/CreatRoom", "xinxiangmj");
        kaifeng.sprite = PPTextureManage.getInstance().LoadAtlasSprite("Atlas/CreatRoom", "kaifengmj_2");
        //SetSizeM(kaifeng);
        //SetSizeS(zhenghzou);
        //SetSizeS(xinxiang);
        //if (num != 2)
        //{
        //    Destroy(GameObject.Find("background"));
        //    CloneBackGround("backgroundkaifeng");
        //    num = 2;
        //}

    }

    public void CloneBackGround(string name)
    {
        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/"+name));
        temp.transform.SetParent(transform);
        temp.transform.SetSiblingIndex(0);
        temp.name = "background";
        temp.transform.localScale = Vector3.one;
        temp.transform.localPosition = new Vector3(84,-16,0);
    }
}
