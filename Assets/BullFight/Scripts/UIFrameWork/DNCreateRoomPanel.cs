using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.guojin.dn.net.message;
using com.guojin.mj.net;
using com.guojin.mj.net.message.login;

public class DNCreateRoomPanel : BasePanel {
    private void Start() {
        transform.FindChild("DoTween/CloseButton").GetComponent<Button>().onClick.AddListener(()=> {
            transform.PlayButtonVoice();
            DNUIManager.Instance.PopPanel();

        });
        transform.FindChild("DoTween/CreateRoomButton").GetComponent<Button>().onClick.AddListener(() => {
            transform.PlayButtonVoice();

            List<OptionEntry> list = new List<OptionEntry>();
            list.Add(OptionEntry.create("jushu", "10"));//局数
            list.Add(OptionEntry.create("mode", "fzzz"));//坐庄模式
            DouniuCreateRoom createRoom = DouniuCreateRoom.create("dn", list);
            SocketMgr.GetInstance().Send(Net.instance.write(createRoom));

        });
    }
    public override void OnEnter() {
        base.OnEnter();

        //transform.FindChild("DoTween").GetComponent<DOTweenAnimation>().enabled = false;
        //transform.FindChild("DoTween").GetComponent<DOTweenAnimation>().enabled = true;
    }
}
