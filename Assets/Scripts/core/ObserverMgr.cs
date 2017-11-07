using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverMgr
{
    private static Dictionary<string, List<Observer>> dict = new Dictionary<string, List<Observer>>();

    public delegate void OnMsg(string msg, com.guojin.core.io.message.Message data);
    private static Dictionary<string, List<OnMsg>> dataDict = new Dictionary<string, List<OnMsg>>();
    // 添加监听
    public static void addEventListener(string msg, Observer ob)
    {
        // todo说 重复消息,重复订阅对象不给予订阅
        bool b = dict.ContainsKey(msg);
        if (b == false)
        {
            dict.Add(msg, new List<Observer>());
        }

        List<Observer> item = dict[msg];
        item.Add(ob);
    }
    // 添加监听(不继承自MonoBehaviour)
    public static void addEventListenerWithDelegate(string msg, OnMsg dele) {
        bool b = dataDict.ContainsKey(msg);
        if (b == false) {
            dataDict.Add(msg, new List<OnMsg>());
        }

        List<OnMsg> item = dataDict[msg];
        item.Add(dele);
    }
    public static void CleanDataEventListener() {
        dataDict.Clear();
    }
    // 移除监听
    public static void removeEventListener(string msg, Observer ob)
    {
        // todo 未实现       
    }
    // 移除该订阅者订阅的所有消息
    public static void removeEventListenerWithObserver(Observer ob)
    {

        foreach (List<Observer> obList in ObserverMgr.dict.Values)
        {
            List<Observer> dealObservreList = new List<Observer>();
            // 查找要删除的observer
            foreach (Observer obListItem in obList)
            {
                if (obListItem == ob)
                {
                    dealObservreList.Add(obListItem);

                }
            }
            // 删除observer
            foreach (Observer dealObserver in dealObservreList)
            {
                obList.Remove(dealObserver);
            }

        }
    }

    // 派发事件
    public static void DispatchMsg(string msg, com.guojin.core.io.message.Message data)
    {
        // 向GameData里面派发数据
        if (dataDict.ContainsKey(msg)) {
            List<OnMsg> cbItem = dataDict[msg];
            for (int i = 0; i < cbItem.Count; i++)
            {
                cbItem[i](msg, data);
            }
            //foreach(OnMsg cb in cbItem) {
            //    cb(msg, data);
            //}
        }

        // 向view里面派发数据
        bool b = dict.ContainsKey(msg);
        if (b)
        {
            List<Observer> item = dict[msg];
            for (int i = 0; i < item.Count; i++)
            {
                item[i].OnMsg(msg, data);
            }
            //foreach (Observer ob in item)
            //{
            //    ob.OnMsg(msg, data);
            //}
        } else
        {
            Debug.Log("消息列表中不存在这个消息: " + msg);
        }
    }


}
