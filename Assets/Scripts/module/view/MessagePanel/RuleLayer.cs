using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Reflection;

public class RuleLayer : MonoBehaviour
{
    public Text text;//要显示的文字
    public Text texttype;//显示是什么麻将

    public Image zhenghzou;
    public Image xinxiang;
    public Image kaifeng;
    public Sprite Zhengzhou1;
    public Sprite Zhengzhou2;
    public Sprite XinXiang1;
    public Sprite XinXiang2;
    public Sprite KaiFeng1;
    public Sprite KaiFeng2;
    public void Setting_onClick()
    {
        LoginAndShare.Controller.BtnVoice();
        //Debug.Log("aaaa");
        Destroy(transform.parent.gameObject);
    }
    public void CloseThis()
    {
        StartCoroutine(waitone());
    }
    IEnumerator waitone()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(transform.parent.gameObject);
    }
    public void CloseThis1()
    {
        StartCoroutine(waitone());
    }
    IEnumerator waitone1()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    public void ShowText(string content)
    {
        text.text = content;
    }
    public void CreatBtn(Sprite sp)
    {
        GetComponent<Image>().sprite = sp;
    }
    public void ShowContent()
    {
        if (Method.isFirJu)
        {
            text.text = "解散房间不扣除房卡,是否确定解散?";
        }
        else
        {
            text.text = "房卡已扣除,是否确认发起解散请求?";
        }     
    }
    public void Close()
    {
        LoginAndShare.Controller.BtnVoice();
        Destroy(gameObject);
    } 
    // 点击取消
    public void OnClickCancleBtn()
    {
        LoginAndShare.Controller.BtnVoice();

        Destroy(transform.parent.gameObject);
    }

    public void ShowRuler()
    {
       this.GetType().GetMethod(Method.MJType).Invoke(this,null);      
    }

    //****************************************************************
    //麻将规则信息 如果增加了麻将的话 需要添加一个麻将的规则方法
    public void MaJongZhengZhou()
    {
        text.text ="\u3000\u3000"+ PlayerData.paijutype + ","+PlayerData.peopletype+","+PlayerData.xuanpaotype+","+PlayerData.hupaitype+","+PlayerData.zhuangjiatype+","+PlayerData.daipaotype+","+PlayerData.fengtype+","+PlayerData.huntype+","+PlayerData.qiduitype+","+PlayerData.gangkaitype;
        texttype.text = "郑州麻将";
    }
    public void MaJongGuShi()
    {
        text.text = "\u3000\u3000" + PlayerData.paijutype + ","+PlayerData.peopletype+"," + PlayerData.hupaitype + "," + PlayerData.fengtype +","+PlayerData.difenType+ "," + PlayerData.xuanpaotype + "," + PlayerData.duanmenType + "," + PlayerData.paozuiType + "," + PlayerData.zhuangjiatype + "！";
        texttype.text = "固始麻将";
    }

    //****************************************************************

    public void SetSizeS(Image image)
    {
        image.rectTransform.sizeDelta = new Vector2(169, 63);

    }
    public void SetSizeM(Image image)
    {
        image.rectTransform.sizeDelta = new Vector2(187, 63);

    }
    public void BtnZhengZhou()
    {
        LoginAndShare.Controller.BtnVoice();
        zhenghzou.sprite = Zhengzhou1;
        xinxiang.sprite = XinXiang2;
        kaifeng.sprite = KaiFeng2;
        SetSizeM(zhenghzou);
        SetSizeS(xinxiang);
        SetSizeS(kaifeng);
    }
    public void BtnXinXiang()
    {
        zhenghzou.sprite = Zhengzhou2;
        xinxiang.sprite = XinXiang1;
        kaifeng.sprite = KaiFeng2;
        SetSizeM(xinxiang);
        SetSizeS(zhenghzou);
        SetSizeS(kaifeng);
    }
    public void BtnKaiFeng()
    {
        zhenghzou.sprite = Zhengzhou2;
        xinxiang.sprite = XinXiang2;
        kaifeng.sprite = KaiFeng1;
        SetSizeM(kaifeng);
        SetSizeS(zhenghzou);
        SetSizeS(xinxiang);
    }


    public void ShowParticulars(com.guojin.mj.net.message.game.GameFanResult GF)
    {
        text.text = GF.userName;
        if (GF.score>0)
        {
            texttype.text ="得分 +"+ GF.score;
        }
        else
        {
            texttype.text = "得分 " + GF.score;
        }        
    }
}
