using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadInfo : MonoBehaviour
{

    public RawImage Avatar;
    public Image duanxian;
    public Image readyImage;
    public Text username;
    public GameObject Avatar0;
    public int id = -1;
    public bool Isoffline = false;
    // Use this for initialization
    public void ShowAvatar(com.guojin.mj.net.message.game.GameUserInfo GUIO)
    {
        Avatar.enabled = true;
        Avatar0.SetActive(false);
        if (!GUIO.online)
        {
            Method.diaoxianNum++;
            duanxian.gameObject.SetActive(true);
            Isoffline = true;
        }
        if (GUIO.isready && GUIO.locationIndex != 0)
        {
            readyImage.gameObject.SetActive(true);
        }
        if (GUIO.locationIndex == 0)
        {
            readyImage.gameObject.SetActive(true);
        }
        id = GUIO.userId;
        username.text = GUIO.userName;
        StartCoroutine(loadImage(GUIO.avatar));
    }

    public void showDistance(Texture ava, string username1)
    {
        Avatar.texture = ava;
        username.text = username1;

    }
    IEnumerator loadImage(string url)
    {
        if (url == null)
        {
            url = URLConst.DefaultUserHeadUrl;
        }
        //用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空
        url = url.Substring(0, url.Length - 1);

        url += "132";
        WWW www = new WWW(url);
        yield return www;
        Avatar.texture = www.texture;
    }
    public void ExitRoom()
    {
        //Avatar.texture = null;
        Avatar.enabled = false;
        username.text = "";
        id = -1;
        Avatar0.SetActive(true);
        readyImage.gameObject.SetActive(false);
        duanxian.gameObject.SetActive(false);
        Isoffline = false;
    }
    public void SetReady()
    {
        readyImage.gameObject.SetActive(true);
    }
    public void SetNoReady()
    {
        readyImage.gameObject.SetActive(false);
    }

    public void SetOffLine()
    {
        duanxian.gameObject.SetActive(true);
        Isoffline = true;
    }
    public void SetOnLine()
    {
        duanxian.gameObject.SetActive(false);
        Isoffline = false;
    }
}