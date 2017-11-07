using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDistance : MonoBehaviour {
    [Header("头像")]
    public GameObject[] head;
    [Header("距离文本")]
    public Text[] distanceT;

    List<GameObject> list = new List<GameObject>();
    public void showHeadDistance(GameObject[]touxiang,List<string> distance)
    {
        int num = 0;
        List<int> listint = new List<int>();
        for (int i = 0; i < touxiang.Length ; i++)
        {
            if (touxiang[i].GetComponent<HeadInfo>().id!=Method.userID)
            {
                list.Add(touxiang[i]);
            }
        }

        for (int i = 0; i < list.Count; i++)
        {
            HeadInfo hi = list[i].GetComponent<HeadInfo>();
            if (hi.id!=-1)
            {
                num++;
                listint.Add(i);
                   head[i].GetComponent<HeadInfo>().showDistance(hi.Avatar.texture, hi.username.text);
                //distanceT[i].text = distance[i];
            } 
        }
        if (num==2)
        {
            if (listint[0]==0&& listint[1]==1)
            {
                distanceT[0].text = distance[0];
            }
            else if(listint[0] == 0 && listint[1] == 2)
            {
                distanceT[1].text = distance[1];
            }
            else if (listint[0] == 1 && listint[1] == 2)
            {
                distanceT[2].text = distance[2];
            }

        }
        else if(num==3)
        {
            for (int i = 0; i < distance.Count; i++)
            {
                distanceT[i].text = distance[i];
            }
        }
        
    }
    public void destoryGameobj()
    {
        Destroy(gameObject);
    }
}
