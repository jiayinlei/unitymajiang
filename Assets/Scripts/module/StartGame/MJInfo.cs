using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class MJInfo : MonoBehaviour, IPointerExitHandler, IPointerDownHandler
{

    int Id;
    int Weizhi;
    public Image dise;
    public Image huase;
    public Image huier;
    int num = 0;
    public int Id1 { get { return Id; } set { Id = value; } }
    public int Weizhi1 { get { return Weizhi; } set { Weizhi = value; } }
    public void CreatMJInfo(int id, int weizhi, Sprite Huase)
    {
        if (id == Method.huier)
        {
            huier.gameObject.SetActive(true);
        }
        Id1 = id;
        Weizhi1 = weizhi;
        huase.sprite = Huase;

        if (Method.MJType == "MaJongGuShi" && !Method.isPlayerBack)
        {
            CreatMJInfoGuShi(id, 140);
        }

    }


    public void CreatMJInfoGuShi(int id, int num)
    {
        //bool isHaveduanmen=false;

        //for (int i = 0; i < Method.intlist.Count; i++)
        //{
        //    if (!Method.OutPaiGuShi(Method.intlist[i]))
        //    {
        //        isHaveduanmen = true;
        //    }
        //}
        //if (!Method.OutPaiGuShi(id))
        //{
        //    isHaveduanmen = true;
        //}

        //if (isHaveduanmen)
        //{
        //    switch (Method.DuanMan)
        //    {
        //        case 1:
        //            if (id > 8)
        //            {
        //                dise.color = new Color(num / 255f, num / 255f, num / 255f, 1);
        //            }
        //            break;
        //        case 2:
        //            if (id < 9 || id > 17)
        //            {
        //                dise.color = new Color(num / 255f, num / 255f, num / 255f, 1);
        //            }
        //            break;
        //        case 3:
        //            if (id < 18 || id > 26)
        //            {
        //                dise.color = new Color(num / 255f, num / 255f, num / 255f, 1);
        //            }
        //            break;

        //    }
        //}

        switch (Method.DuanMan)
        {
            case 1:
                if (id >= 0 && id <= 8)
                {
                    dise.color = new Color(num / 255f, num / 255f, num / 255f, 1);
                }
                break;
            case 2:
                if (id >= 9 && id <= 17)
                {
                    dise.color = new Color(num / 255f, num / 255f, num / 255f, 1);
                }
                break;
            case 3:
                if (id >= 18 && id <= 26)
                {
                    dise.color = new Color(num / 255f, num / 255f, num / 255f, 1);
                }
                break;

        }

    }
    bool IsOut;
    static GameObject go;
    bool isOne;
    public void showdipai()
    {
        dise.sprite = Resources.Load<Sprite>("MJSprite/MJDipai/6");
        huase.gameObject.SetActive(false);
        huier.gameObject.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter.name == "ImageName" && IsOut)
        {
            go = eventData.pointerEnter.transform.parent.gameObject;
            IsOut = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback)
        {
            return;
        }

        Debug.Log("Down");
        num++;
        if (num == 1)
        {
            VoiceManger.Instance.BtnVoice();
            transform.localPosition += new Vector3(0, 40, 0);
            if (go != null)
            {
                go.GetComponent<MJInfo>().num = 0;
                go.transform.localPosition -= new Vector3(0, 40, 0);
            }
            go = gameObject;
        }
        if (num == 2)
        {
            if (Method.MJType == "MaJongGuShi" && Method.OutPaiGuShi(Id1))
            {
                Outpai();
            }
            else if(Method.MJType== "MaJongZhengZhou")
            {
                Outpai();
            }          
            else
            {
                transform.localPosition -= new Vector3(0, 40, 0);
                num = 0;
                go = null;
            }
            VoiceManger.Instance.BtnVoice();
        }
    }
    void Outpai()
    {
        if (Method.isMine)
        {
            if (Method.isFirstOutPai)
            {
                int id = Id1;
                Debug.Log("断的门是"+ Method.duanmenindex);
                Method.CloneDuanmen(Method.Index, Resources.Load<Sprite>("NewUIPicture/gushi/duan"+Method.duanmenindex)); 
                GameObject CloneGameobj = Instantiate(Method.objList[1]);
                CloneGameobj.GetComponent<MaJongOutInfo>().IsHun(id, Method.MjNameSp[id]);
                CloneGameobj.transform.SetParent(Method.tranList[2]);
                CloneGameobj.transform.localPosition = Vector3.zero;
                CloneGameobj.transform.localScale = Vector3.one;
                if (Method.Dingque!=null)
                {
                    Destroy(Method.Dingque);
                }
                //VoiceManger.Instance.LoadMusiceByName(id);
                //Method.lastMajong = CloneGameobj;
                //Method.SetGuangBiao(CloneGameobj, 1);
                Method.isMine = false;
                //go = null;
                //Method.intlist.Add(Method.goId);
                Method.intlist.Remove(id);
                //Method.goId = 100;
                //Destroy(Method.go);
                //向服务器发送出牌的指令
               // Method.OutMaJong(id);
                com.guojin.mj.net.message.game.SelectQuePaiType SQP = com.guojin.mj.net.message.game.SelectQuePaiType.Creat(id,Method.duanmenindex);
                SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(SQP));
                Method.CloneChild();
                Method.isFirstOutPai = false;
                return;
            }

            if (Method.isPeng)
            {
                int id = Id1;
                GameObject CloneGameobj = Instantiate(Method.objList[1]);
                CloneGameobj.GetComponent<MaJongOutInfo>().IsHun(id, Method.MjNameSp[id]);
                CloneGameobj.transform.SetParent(Method.tranList[2]);
                CloneGameobj.transform.localPosition = Vector3.zero;
                CloneGameobj.transform.localScale = Vector3.one;
                VoiceManger.Instance.LoadMusiceByName(id);
                Method.lastMajong = CloneGameobj;
                go = null;
                Method.intlist.Remove(id);
                Method.SetGuangBiao(CloneGameobj, 1);
                // 向服务器发送出牌的指令
                Method.PengOverOutMaJong(id);
                Method.isMine = false;
                Method.isPeng = false;
                Destroy(transform.parent.gameObject);
            }
            else
            {
                int id = Id1;
                GameObject CloneGameobj = Instantiate(Method.objList[1]);
                CloneGameobj.GetComponent<MaJongOutInfo>().IsHun(id, Method.MjNameSp[id]);
                CloneGameobj.transform.SetParent(Method.tranList[2]);
                CloneGameobj.transform.localPosition = Vector3.zero;
                CloneGameobj.transform.localScale = Vector3.one;
                VoiceManger.Instance.LoadMusiceByName(id);
                Method.lastMajong = CloneGameobj;
                Method.SetGuangBiao(CloneGameobj, 1);
                Method.isMine = false;
                go = null;
                Method.intlist.Add(Method.goId);
                Method.intlist.Remove(id);
                Method.goId = 100;
                Destroy(Method.go);
                //向服务器发送出牌的指令
                Method.OutMaJong(id);
                Method.CloneChild();
            }
        }
        else
        {
            transform.localPosition -= new Vector3(0, 40, 0);
            num = 0;
            go = null;
        }
    }
}
enum BoardState
{
    手牌,
    发牌,
    出牌
}