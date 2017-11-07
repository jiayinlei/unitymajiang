using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MaJongOutInfo : MonoBehaviour {
     int Id;      
    public Image huase;
    public Image huier;
    public Image dise;
    public Image Fangxiang;
    public Text textzhang;
    public int Id1
    {
        get
        {
            return Id;
        }

        set
        {
            Id = value;
        }
    }

   public void ShowZhang(int zhang)
    {
        textzhang.text = zhang.ToString();
    }

    /// <summary>
    /// 创建牌
    /// </summary>
    /// <param name="id">牌的id</param>
    /// <param name="Huase">牌的花色</param>
    public void  CreatMaJongOutInfods(int id, Sprite Huase)//后期会有声音
    {      
        Id1 = id;
        huase.sprite = Huase;
    }
    public void IsHun(int id, Sprite Huase)
    {
        if (id == Method.huier)
        {
            huier.gameObject.SetActive(true);
        }
        Id1 = id;
        huase.sprite = Huase;
    }
    public void IsDuanMen(Sprite Huase)
    {
        huase.sprite = Huase;
    }
    public void ShowOnePai(int id, Sprite Huase, int fangxiang)
    {
        if (id == Method.huier)
        {
            huier.gameObject.SetActive(true);
        }
        Id1 = id;
        huase.sprite = Huase;
        if (fangxiang!=-1)
        {
            Fangxiang.gameObject.SetActive(true);
            Fangxiang.sprite = Method.fangxiang[fangxiang];
        }
    }
    /// <summary>
    /// 显示牌
    /// </summary>
    /// <param name="weizhi">0123 底色</param>
    /// <param name="id">牌的id</param>
    /// <param name="Huase">牌的花色</param>
    /// <param name="fangxiang">碰扛的玩家方向</param>
    /// <param name="show">是否需要显示方向</param>
    public void creatPai(int weizhi,int id,Sprite Huase,int fangxiang,bool show)
    {
        dise.sprite =Method.mjdise[Method.choiceHuaSe*5+weizhi];
        if (id == Method.huier)
        {
            huier.gameObject.SetActive(true);
        }
        Id1 = id;
        huase.sprite = Huase;
        if (show)
        {
            Fangxiang.gameObject.SetActive(true);
            Fangxiang.sprite = Method.fangxiang[fangxiang];
        }
    }
    private void OnDestroy()
    {
        Debug.Log("beigandian");
    }
    public void HoldDo()
    {
        StartCoroutine(doSomething());
    }
    IEnumerator doSomething()
    {
        yield return new WaitForSeconds(1f);

    }
}
