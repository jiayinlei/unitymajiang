using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Expression : MonoBehaviour {
    private  int index;
    public void InitView(Sprite spr,int num)
    {
        Image image = this.GetComponent<Image>();
        image.sprite = spr;
        index = num;
    }
    public void OnClickBtn()
    {
        Destroy(GameObject.Find("MessagePanel"));
        com.guojin.mj.net.message.game.PlayerSendChatInfo PSCI = com.guojin.mj.net.message.game.PlayerSendChatInfo.create(index.ToString(), 2);
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(PSCI));
      
    }
}
