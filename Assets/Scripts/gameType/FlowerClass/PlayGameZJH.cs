using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayGameZJH : MonoBehaviour
{
    public static PlayGameZJH Instance;
    public GameObject[] headParent;
    public GameObject[] shouPaiParent;
    public GameObject meHandle;//自己操作时的按钮界面（未出局）
    public GameObject otherHandle;//他人操作时的按钮界面（未出局）
    public GameObject NoBi;
    public GameObject Bi;
    private GameObject zhuang;
    private Button Kan;
    void Awake()
    {
        Instance = this;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
    }
    void Start()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        DestoryAllChildren();
        for (int i = 0; i < 5; i++)
        {
            TooL.destroyAllChildren(GameObject.Find("PlayerParent" + i));
        }
        MainSceneZJH.ReciveMessage(MainSceneZJH.GRI);
    }
    /// <summary>
    /// 克隆庄
    /// </summary>
    /// <param name="index"></param>
    public void ShowZhuang(int index)
    {
        zhuang = Instantiate(Resources.Load<GameObject>("MjPrefab/zhuang"));
        zhuang.transform.SetParent(GameObject.Find("HeadPhotoZJH" + index).transform);//克隆庄
        zhuang.transform.localPosition = new Vector3(22, 22, 0);
    }
    /// <summary>
    /// 清理游戏场景
    /// </summary>
    public void DestoryAllChildren()
    {
        for (int i = 0; i < 5; i++)
        {
            TooL.destroyAllChildren(GameObject.Find("CardParent" + i));
            TooL.destroyAllChildren(GameObject.Find("PlayerType" + i));
        }
        if (zhuang != null)
        {
            Destroy(zhuang);
        }
    }
    /// <summary>
    /// 显示弃牌图标
    /// </summary>
    /// <param name="index"></param>
    public void ShowQi(int index)
    {
        TooL.destroyAllChildren(GameObject.Find("PlayerType" + index));
        TooL.destroyAllChildren(GameObject.Find("CardParent" + index));
        GameObject obj = Instantiate(Resources.Load<GameObject>("ZJHPrefab/Qi"));
        obj.transform.SetParent(GameObject.Find("PlayerType" + index).transform);
        obj.transform.localPosition = new Vector3(0, 0, 0);
    }
    /// <summary>
    /// 克隆头像
    /// </summary>
    /// <param name="index"></param>
    /// <param name="HeadPrefab"></param>
    public void ShowHead(int index, GameObject HeadPrefab)
    {
        HeadPrefab.transform.SetParent(GameObject.Find("PlayerParent" + index).transform);
        HeadPrefab.transform.localScale = Vector3.one;
    }
    /// <summary>
    /// 展示手牌
    /// </summary>
    /// <param name="index"></param>
    public void ShowShoupai(int index, int[] shouPai)
    {
        if (MainSceneZJH.intArr[index] == 0)
        {
            MainSceneZJH.isKanPai = true;
            TooL.destroyAllChildren(GameObject.Find("CardParent" + index));
            //克隆手牌
            for (int i = 0; i < shouPai.Length; i++)
            {
                GameObject temp = Instantiate(Resources.Load<GameObject>("ZJHPrefab/Card"));
                temp.transform.SetParent(GameObject.Find("CardParent" + index).transform);
                temp.GetComponent<Image>().sprite = GetSprite(shouPai[i]);
                temp.transform.localScale = Vector3.one;
            }
        }
        else
        {
            //克隆已看牌图标
            GameObject temp1 = Instantiate(Resources.Load<GameObject>("ZJHPrefab/LookCard"));
            temp1.transform.SetParent(GameObject.Find("PlayerType" + index).transform);
            temp1.transform.localScale = Vector3.one;
            temp1.transform.localPosition = Vector3.zero;
        }
    }
    /// <summary>
    /// 克隆手牌/背面
    /// </summary>
    public void CopyShoupai(int index)
    {
        GameObject temp = Instantiate(Resources.Load<GameObject>("ZJHPrefab/BackCard"));
        temp.transform.SetParent(GameObject.Find("CardParent" + index).transform);
        temp.transform.localScale = Vector3.one;
    }
    /// <summary>
    /// 获取卡牌图片
    /// </summary>
    /// <param name="Index"></param>
    /// <returns></returns>
    public Sprite GetSprite(int Index)
    {
        return Resources.Load<Sprite>("ZJHPicture/" + Index);
    }
    void Update()
    {
        NoBi.SetActive(MainSceneZJH.isBiPai);
        Bi.SetActive(!MainSceneZJH.isBiPai);
        if (MainSceneZJH.isStart)
        {
            if (!MainSceneZJH.isOut)
            {
                if (MainSceneZJH.isQiPai)
                {
                    meHandle.SetActive(false);
                    otherHandle.SetActive(false);
                }
                else
                {
                    meHandle.SetActive(MainSceneZJH.isMine);
                    otherHandle.SetActive(!MainSceneZJH.isMine);
                }
            }
            else
            {
                meHandle.SetActive(false);
                otherHandle.SetActive(false);
            }
        }
        else
        {
            meHandle.SetActive(false);
            otherHandle.SetActive(false);
        }

    }
}
