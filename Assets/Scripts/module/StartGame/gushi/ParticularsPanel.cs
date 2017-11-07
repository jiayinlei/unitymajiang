using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class ParticularsPanel : MonoBehaviour
{

    public GameObject SelfInfo;
    public GameObject Player3;
    public GameObject PlayerParent;
    Text[] textArr;
    RuleLayer[] textArr1;
    private void Start()
    {
        //textArr = SelfInfo.GetComponentsInChildren<Text>();
        //textArr1 = PlayerParent.GetComponentsInChildren<RuleLayer>();       
    }
    private void Awake()
    {
        //textArr = SelfInfo.GetComponentsInChildren<Text>();
        //textArr1 = PlayerParent.GetComponentsInChildren<RuleLayer>();
    }
    //public void ShowInfoSoce(string nameself,string getScore,string zongfen)
    //{
    //    textArr[0].text = nameself;
    //    textArr[1].text = getScore;
    //    textArr[2].text ="总分："+ zongfen;
    //}
    public void DestroyThis()
    {
        Destroy(gameObject);
    }

    public void ShowInfoSoce(com.guojin.mj.net.message.game.RecordDetailsRet SQPT)
    {
        textArr = SelfInfo.GetComponentsInChildren<Text>();
        textArr1 = PlayerParent.GetComponentsInChildren<RuleLayer>();
        if (Method.PeopleNum == 3)
        {
            Player3.SetActive(false);
        }
        StringBuilder getScore = new StringBuilder();
        //textArr[0].text = nameself;
        //textArr[1].text = getScore;
        //textArr[2].text = "总分：" + zongfen;
        if (SQPT.isZiMo)
        {
            getScore.Append("胡牌类型：自模胡；");
        }
        else
        {
            getScore.Append("胡牌类型：点炮胡；");
        }
        if (SQPT.isZhuangJiaDi)
        {
            getScore.Append("庄家加底；");
        }
        else
        {
            getScore.Append("庄家不加底；");
        }
        if (SQPT.daMingGang!=null)
        {
            for (int i = 0; i < SQPT.daMingGang.Length; i++)
            {
                getScore.Append("明杠 " + SQPT.daMingGang[i] + "；");
            }
        }
        if (SQPT.xiaoMingGang !=null)
        {
            for (int i = 0; i < SQPT.xiaoMingGang.Length; i++)
            {
                getScore.Append("补杠 " + SQPT.xiaoMingGang[i] + "；");
            }
        }
        if (SQPT.anGang != null)
        {
            for (int i = 0; i < SQPT.anGang.Length; i++)
            {
                getScore.Append("暗杠 " + SQPT.anGang[i] + "；");
            }
        }
        if (SQPT.huFen != null || SQPT.huFen != "")
        {
            getScore.Append("胡分 " + SQPT.huFen + "；");
        }
        if (SQPT.content != ""&& SQPT.content != null)
        {
            //for (int i = 0; i < SQPT.zui.Length; i++)
            //{
            //    getScore.Append(SQPT.zui[i] + " ");
            //}
            getScore.Append( SQPT.content+"；");
        }
        if (SQPT.zuiscore != null && SQPT.zuiscore != "")
        {
            getScore.Append("总嘴得分：" + SQPT.zuiscore + "。");
        }




        textArr[1].text = getScore.ToString();

        List<com.guojin.mj.net.message.game.GameFanResult> GFList = new List<com.guojin.mj.net.message.game.GameFanResult>();
        for (int i = 0; i < Method.END.fanResults.Count; i++)
        {
            GFList.Add(Method.END.fanResults[i]);
        }
        textArr[0].text ="玩家："+ GFList[Method.Index].userName;
        if (GFList[Method.Index].score > 0)
        {
            textArr[2].text = "总分：+" + GFList[Method.Index].score.ToString();
        }
        else
        {
            textArr[2].text = "总分：" + GFList[Method.Index].score.ToString();
        }
        GFList.RemoveAt(Method.Index);
        for (int i = 0; i < GFList.Count; i++)
        {
            textArr1[i].ShowParticulars(GFList[i]);
        }

    }
}
