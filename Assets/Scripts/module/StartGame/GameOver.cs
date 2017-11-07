using System;
using com.guojin.mj.net.message.login;
using UnityEngine;
using UnityEngine.UI;
using com.guojin.mj.net.message;
using UnityEngine.SceneManagement;
using com.guojin.core.io.message;
using com.guojin.mj.net.message.game;
using System.Collections.Generic;

public class GameOver : MonoBehaviour {

    private static GameOver instance;
    public Text m_successOrFailure;
    public Image m_successType;//胜利方式
    public Text[] m_playerName;
    public Text[] m_playerGrade;
    public GameObject[] m_playerParents;
    public Sprite[] sprite;

    void Awake()
    {

    }
    void Start()
    {

    }
    public static GameOver Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameOver();
            }
            return instance;
        }

    }
    /// <summary>
    /// 显示游戏结算界面，显示本局游戏信息
    /// </summary>
    /// <param name="End"></param>
    public void GameEnd(com.guojin.mj.net.message.game.GameChapterEnd End)
    {
        if (End.huPaiIndex == Method.Index)
        {
            GetSuccessOrFailure("胜利");
        }
        else
        {
            if (End.huPaiIndex == -1)
            {
                GetSuccessOrFailure("流局");
                ShowSuccessType(null);
            }
            else if(End.fangPaoIndex == -1)
            {
                GetSuccessOrFailure("失败");
            }
            else if (Method.intArr[End.fangPaoIndex] != 0)
            {
                GetSuccessOrFailure("平局");
            }
            else
            {
                GetSuccessOrFailure("失败");
            }
        }
        if(End.huPaiIndex != -1)
        {
            GetPlayerNameAndGrade(End.fanResults[End.huPaiIndex].userName, End.fanResults[End.huPaiIndex].score, m_playerName[0], m_playerGrade[0]);
            ShowShouPai(m_playerParents[0], End.fanResults[End.huPaiIndex].shouPai, End.fanResults[End.huPaiIndex].anGang, End.fanResults[End.huPaiIndex].xiaoMingGang, End.fanResults[End.huPaiIndex].daMingGang, End.fanResults[End.huPaiIndex].peng, End.fanResults[End.huPaiIndex].chi);
            End.fanResults.Remove(End.fanResults[End.huPaiIndex]);
            for (int i = 0; i < End.fanResults.Count; i++)
            {
                GetPlayerNameAndGrade(End.fanResults[i].userName, End.fanResults[i].score, m_playerName[i + 1], m_playerGrade[i + 1]);
                ShowShouPai(m_playerParents[i + 1], End.fanResults[i].shouPai, End.fanResults[i].anGang, End.fanResults[i].xiaoMingGang, End.fanResults[i].daMingGang, End.fanResults[i].peng, End.fanResults[i].chi);

            }
            if (End.fangPaoIndex == -1)
            {
                ShowSuccessType(sprite[0]);
            }
            else
            {
                ShowSuccessType(sprite[1]);
            }
        }
        else//流局
        {
            Destroy(m_successType.gameObject);
            for (int i = 0; i < End.fanResults.Count; i++)
            {

                GetPlayerNameAndGrade(End.fanResults[i].userName, End.fanResults[i].score, m_playerName[i], m_playerGrade[i]);
                ShowShouPai(m_playerParents[i], End.fanResults[i].shouPai, End.fanResults[i].anGang, End.fanResults[i].xiaoMingGang, End.fanResults[i].daMingGang, End.fanResults[i].peng, End.fanResults[i].chi);

            }

        }

    }
    /// <summary>
    /// 显示玩家牌
    /// </summary>
    /// <param name="shoupai"></param>
    private void ShowShouPai(GameObject playerPaiParents,int[] shouPai, int[] anGang, int[] xiaoMingGang, int[] daMingGang, int[] peng, int[] chi)
    {
        if (anGang != null)
        {
            for (int i = 0; i < anGang.Length; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    GameObject maJiang = TooL.loadPrefab(playerPaiParents, "OutPai0");
                    maJiang.transform.SetParent(playerPaiParents.transform);
                    maJiang.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                    MaJongOutInfo mo = maJiang.GetComponent<MaJongOutInfo>();
                    mo.IsHun(anGang[i], Method.MjNameSp[anGang[i]]);
                }
            }
        }
        if (xiaoMingGang != null)
        {
            for (int i = 0; i < xiaoMingGang.Length; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    GameObject maJiang = TooL.loadPrefab(playerPaiParents, "OutPai0");
                    maJiang.transform.SetParent(playerPaiParents.transform);
                    maJiang.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                    MaJongOutInfo mo = maJiang.GetComponent<MaJongOutInfo>();
                    mo.IsHun(xiaoMingGang[i], Method.MjNameSp[xiaoMingGang[i]]);
                }
               
            }
        }
        if (daMingGang != null)
        {
            for (int i = 0; i < daMingGang.Length; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    GameObject maJiang = TooL.loadPrefab(playerPaiParents, "OutPai0");
                    maJiang.transform.SetParent(playerPaiParents.transform);
                    maJiang.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                    MaJongOutInfo mo = maJiang.GetComponent<MaJongOutInfo>();
                    mo.IsHun(daMingGang[i], Method.MjNameSp[daMingGang[i]]);
                }
               
            }
        }
        if (peng != null)
        {
            for (int i = 0; i < peng.Length; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    GameObject maJiang = TooL.loadPrefab(playerPaiParents, "OutPai0");
                    maJiang.transform.SetParent(playerPaiParents.transform);
                    maJiang.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                    MaJongOutInfo mo = maJiang.GetComponent<MaJongOutInfo>();
                    mo.IsHun(peng[i], Method.MjNameSp[peng[i]]);
                }
            }
        }
        if (chi != null)
        {
            for (int i = 0; i < chi.Length; i++)
            {
                GameObject maJiang = TooL.loadPrefab(playerPaiParents, "OutPai0");
                maJiang.transform.SetParent(playerPaiParents.transform);
                maJiang.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                MaJongOutInfo mo = maJiang.GetComponent<MaJongOutInfo>();
                mo.IsHun(chi[i], Method.MjNameSp[chi[i]]);
            }
        }
        if(shouPai != null)
        {
            Array.Sort(shouPai);
            for (int i = 0; i < shouPai.Length; i++)
            {
                GameObject maJiang = TooL.loadPrefab(playerPaiParents, "OutPai0");
                maJiang.transform.SetParent(playerPaiParents.transform);
                maJiang.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                MaJongOutInfo mo = maJiang.GetComponent<MaJongOutInfo>();
                mo.IsHun(shouPai[i], Method.MjNameSp[shouPai[i]]);
            }
        }
    }

    /// <summary>
    /// 判断胜利、平局、失败、流局
    /// </summary>
    /// <param name="s"></param>
    public void GetSuccessOrFailure(string s)
    {
        m_successOrFailure.text = s;
    }
    public void ShowSuccessType(Sprite type)
    {
        m_successType.sprite = type;
    }
    /// <summary>
    /// 玩家的名字、分数
    /// </summary>
    public void GetPlayerNameAndGrade(string name,int grade,Text nameText,Text gradeText)
    {
        //Debug.Log(name + ":" + grade.ToString());
        nameText.text = name;
        gradeText.text = grade.ToString();
    }  
    /// <summary>
    /// （游戏结算）点击确认或关闭按钮执行
    /// </summary>
    public void SureOrClose()
    {
        Destroy(gameObject);
    }
}
