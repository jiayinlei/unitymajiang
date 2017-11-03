using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CenterController : MonoBehaviour
{

    public Text updateTime;
    int timeNum = 0;
    float times = 0;
    bool ishuifang=false;
    private void Start()
    {
        if (GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback)
        {
            ishuifang = true;
            updateTime.text = "回放";
        }
    }




    void FixedUpdate()
    {

        if (Method.isUpdate)
        {
            Debug.Log("isUpdate");

            timeNum = 16;
            Method.isUpdate = false;
        }
        times += Time.deltaTime;
        if (times >= 1 && timeNum > 0&& !ishuifang)
        {
            timeNum--;
            times = 0;
            updateTime.text = timeNum.ToString();

            //托管的处理
            if (timeNum == 0)
            {
                Method.TuoGuan();
            }
        }        
        if (Method.OutPai0 != null)
        {
            if (Method.OutPai0.childCount > 24)
            {
                Method.OutPai0.localPosition = new Vector3(0, -62, 0);
            }
            else
            {
                Method.OutPai0.localPosition = new Vector3(0, -97, 0);
            }
        }
    }
}
