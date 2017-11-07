using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public class textrawimage : MonoBehaviour {

    public GameObject obj;
    private void Start()
    {
        List<string> listArr = new List<string>();
        for (int i = 0; i < 10; i++)
        {            
           listArr.Add(i.ToString());            
        }
        string strArray = string.Join(",", listArr.ToArray());
        Debug.Log(strArray);


        string str = "2,4,3,1,5";
        int[] list = Array.ConvertAll<string, int>(str.Split(','), s => int.Parse(s));

        for (int i = 0; i < list.Length; i++)
        {
            Debug.Log(list[i]);
        }
        //Screen.orientation = ScreenOrientation.LandscapeLeft;
        //StartCoroutine(loads());
        //com.guojin.mj.net.message.login.JoinRoom jr = com.guojin.mj.net.message.login.JoinRoom.create(Method.GameRoomID);
        //SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(jr));
        //SceneManager.LoadScene("Horizontal");
        //Transform aprent = GameObject.Find("Canvas").transform;
        //GameObject temp = Instantiate(obj)as GameObject;
        //temp.transform.SetParent(aprent);
        //temp.name = "1";
        //temp.transform.SetSiblingIndex(aprent.childCount);
        //temp.transform.localScale = Vector3.one;
        //temp.transform.localPosition = Vector3.zero;
    }
    List<int> LIST = new List<int>();
    IEnumerator loads()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Horizontal");
    }
    public void show()
    {

        Destroy(obj);
    }
    public void onload()
    {
        SceneManager.LoadScene("TestScene");
    }
   public void btn()
    {
        //Color ca = new Color(80, 80, 80,255);
        //obj.GetComponent<Image>().color = new Color(80/255f, 80 / 255f, 80 / 255f, 1);
        //Screen.orientation = ScreenOrientation.Portrait;
        Debug.Log(GetDistance(34.8258372756, 113.5923700734, 34.8257712215, 113.5923968955));
        Debug.Log(Convert.ToDouble(GetDistance(34.8258372756, 113.5923700734, 34.8257712215, 113.5923968955)).ToString("0.00") + "米");
        Debug.Log(GetDistance(35.8258372756, 113.5923700734, 36.8257712215, 113.5923968955));
       Debug.Log( Convert.ToDouble(GetDistance(35.8258372756, 113.5923700734, 36.8257712215, 113.5923968955)).ToString("0.00")+"公里");//保留小数点后两位,结果为10000.00
        Method.GPSPosition0[0] = "0";
    }


    private const double EARTH_RADIUS = 6371010;

    /// <summary>
    /// 计算两点位置的距离，返回两点的距离，单位：米
    /// 该公式为GOOGLE提供，误差小于0.2米
    /// </summary>
    /// <param name="lng1">第一点经度</param>
    /// <param name="lat1">第一点纬度</param>        
    /// <param name="lng2">第二点经度</param>
    /// <param name="lat2">第二点纬度</param>
    /// 34.65 33.22  18.99 17.11
    /// <returns></returns>
    public static double GetDistance(double lng1, double lat1, double lng2, double lat2)
    {   
        double radLat1 = Rad(lat1);
        double radLng1 = Rad(lng1);
        double radLat2 = Rad(lat2);
        double radLng2 = Rad(lng2);
        double a = radLat1 - radLat2;
        double b = radLng1 - radLng2;
        double result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2))) * EARTH_RADIUS;

        Convert.ToDouble(result).ToString("0.00");//保留小数点后两位,结果为10000.00
        return result;
    }

    /// <summary>
    /// 经纬度转化成弧度
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    private static double Rad(double d)
    {
        return (double)d * Math.PI / 180d;
    }
    public void budsajn()
    {
        System.Random random = new System.Random();
        int i = random.Next(2) + 1;
        Debug.Log(i);
        Texture2D tex = Resources.Load<Texture2D>(string.Format("Atlas/BigSprite/lineShare{0}", i.ToString()));
        obj.GetComponent<RawImage>().texture = tex;
    }
   

}
