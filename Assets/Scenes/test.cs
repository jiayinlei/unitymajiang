using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class test : MonoBehaviour {
    void StartThread()

    {
        Thread athread = new Thread(new ThreadStart(goThread));

       // athread.IsBackground = false;//防止后台现成。相反需要后台线程就设为false
        athread.Start();
    }
    object lockd = new object();
    void goThread()
    {
        int index = 0;
        while (true)
        {
            lock (lockd)//防止其他线程访问当前线程使用的数据
            {
                Debug.Log("in thread" + index);
                index++;
                Thread.Sleep(1000);
                //if (index == 100)
                //{
                //    Thread.Sleep(10000);//   将当前线程挂起指定的时间 毫秒  时间结束后 继续执行下一步  和yield类似
                //}
                //else if (index == 200)
                //{
                //    break;//该函数执行完自动结束该线程
                //}
            }
        }
    }
        // Use this for initialization
    void Start ()
    {
        StartThread();
    }
    int  index = 0;
    float t = 0;
    // Update is called once per frame
    void Update ()
    {
        t += Time.deltaTime;
        if (t>3)
        {
            t = 0;
            Debug.Log("in Update" + index);
        }
        
    }
}
