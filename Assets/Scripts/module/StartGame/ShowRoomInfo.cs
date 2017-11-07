using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowRoomInfo : MonoBehaviour
{

    public Text peopleNum;
    public Text juNum;
    public Text hun;
    public Text feng;
    public Text pao;
    public Text hupai;
    public Text qidui;
    public Text zhuangjia;
    public Text gangkai;
    public Text daipao;
    public Text roomid;


    public void ShowRulerInfo()
    {
        GetType().GetMethod(Method.MJType).Invoke(this, null);
    }
    //****************************************************************
    //麻将规则信息 如果增加了麻将的话 需要添加一个麻将的规则方法
    public void MaJongZhengZhou()
    {
        roomid.text = "房间号：" + Method.GameRoomID;
        peopleNum.text = PlayerData.peopleNum;
        juNum.text = PlayerData.juNum;
        hun.text = PlayerData.hun;
        feng.text = PlayerData.feng;
        pao.text = PlayerData.pao;
        hupai.text = PlayerData.hupai;
        qidui.text = PlayerData.qidui;
        zhuangjia.text = PlayerData.zhuangjia;
        gangkai.text = PlayerData.gangkai;
        daipao.text = PlayerData.daipao;
    }
    public void MaJongGuShi()
    {
        roomid.text ="房间号："+ Method.GameRoomID;
        peopleNum.text = PlayerData.peopleNum;
        juNum.text = PlayerData.juNum;
        hun.text = PlayerData.difenType;
        feng.text = PlayerData.feng;
        pao.text = PlayerData.pao;
        hupai.text = PlayerData.hupai;
        qidui.text = PlayerData.paozui;
        zhuangjia.text = PlayerData.zhuangjia;
        gangkai.text = PlayerData.duanmen;
        daipao.text = PlayerData.daipao;
    }
    //****************************************************************
    public void DestroyThis()
    {
        Destroy(gameObject);
    }

}
