using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChoicePaoTimes : MonoBehaviour {
    
    Button[] BtnArr;
    Transform Center_Img;
    void Start () {     
        BtnArr = transform.GetComponentsInChildren<Button>();
        BtnArr[0].onClick.AddListener(delegate {
            Method.IPaoTimes = 1;      
            SendTimesToSever(102, Method.IPaoTimes);     
            Method.isChoicePao = true;
            Destroy(gameObject);
           
        });
        BtnArr[1].onClick.AddListener(delegate {
            Method.IPaoTimes = 2;
            SendTimesToSever(102, Method.IPaoTimes);     
            Method.isChoicePao = true;
            Destroy(gameObject);
        });
        BtnArr[2].onClick.AddListener(delegate {
            Method.IPaoTimes = 3;  
            SendTimesToSever(102, Method.IPaoTimes);     
            Method.isChoicePao = true;
            Destroy(gameObject);
        });
        BtnArr[3].onClick.AddListener(delegate {
            Method.IPaoTimes = 0;
            SendTimesToSever(102, Method.IPaoTimes);          
            Method.isChoicePao = true;
            Destroy(gameObject);
        });

    }	
	
    /// <summary>
    ///向服务器发送选择的要跑的次数 
    /// </summary>
    public void SendTimesToSever(int id,int times)
    {
        Debug.Log( id+"跑了"+Method.IPaoTimes+"次");
        com.guojin.mj.net.message.game.OperationDingPao dp = com.guojin.mj.net.message.game.OperationDingPao.create(id, times);
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(dp));
    }
}
