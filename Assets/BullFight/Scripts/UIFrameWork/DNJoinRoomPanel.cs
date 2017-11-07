using com.guojin.dn.net.message;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DNJoinRoomPanel : BasePanel {

    private Text roomText;
    private Text noticeText;
    private Button btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9, btn10, btn11, btnClose;
    // Use this for initialization
    void Start () {
        InitUI();
    }

    public override void OnEnter() {
        base.OnEnter();

        //transform.FindChild("DoTween").GetComponent<DOTweenAnimation>().enabled = false;
        //transform.FindChild("DoTween").gameObject.SetActive(false);
        //transform.FindChild("DoTween").gameObject.SetActive(true);
    }
    void InitUI() {
        btn0 = transform.FindChild("DoTween/BtnNum0").GetComponent<Button>();
        btn1 = transform.FindChild("DoTween/BtnNum1").GetComponent<Button>();
        btn2 = transform.FindChild("DoTween/BtnNum2").GetComponent<Button>();
        btn3 = transform.FindChild("DoTween/BtnNum3").GetComponent<Button>();
        btn4 = transform.FindChild("DoTween/BtnNum4").GetComponent<Button>();
        btn5 = transform.FindChild("DoTween/BtnNum5").GetComponent<Button>();
        btn6 = transform.FindChild("DoTween/BtnNum6").GetComponent<Button>();
        btn7 = transform.FindChild("DoTween/BtnNum7").GetComponent<Button>();
        btn8 = transform.FindChild("DoTween/BtnNum8").GetComponent<Button>();
        btn9 = transform.FindChild("DoTween/BtnNum9").GetComponent<Button>();
        btn10 = transform.FindChild("DoTween/BtnNum10").GetComponent<Button>();
        btn11 = transform.FindChild("DoTween/BtnNum11").GetComponent<Button>();
        noticeText = transform.FindChild("DoTween/Notice").GetComponent<Text>();
        btnClose = transform.FindChild("DoTween/CloseButton").GetComponent<Button>();
        roomText = transform.FindChild("DoTween/RoomTextBG/Text").GetComponent<Text>();
        noticeText.text = "";
        btn0.onClick.AddListener(() => {
            UpDateRoomID(0);
        });
        btn1.onClick.AddListener(() => {
            UpDateRoomID(1);
        });
        btn2.onClick.AddListener(() => {
            UpDateRoomID(2);
        });
        btn3.onClick.AddListener(() => {
            UpDateRoomID(3);
        });
        btn4.onClick.AddListener(() => {
            UpDateRoomID(4);
        });
        btn5.onClick.AddListener(() => {
            UpDateRoomID(5);
        });
        btn6.onClick.AddListener(() => {
            UpDateRoomID(6);
        });
        btn7.onClick.AddListener(() => {
            UpDateRoomID(7);
        });
        btn8.onClick.AddListener(() => {
            UpDateRoomID(8);
        });
        btn9.onClick.AddListener(() => {
            UpDateRoomID(9);
        });
        btn10.onClick.AddListener(() => {
            roomText.text = "";
        });

        btn11.onClick.AddListener(() => {
            if (roomText.text.Length > 0) {
                roomText.text = roomText.text.Substring(0, roomText.text.Length - 1);
            }
        });
        btnClose.onClick.AddListener(() => {
            DNUIManager.Instance.PopPanel();
        });
    }

    /// <summary>
    /// 输入并更新ID显示
    /// </summary>
    /// <param name="num"></param>
    private void UpDateRoomID(int num) {

        if (roomText.text.Length < 6) {
            roomText.text += num;

            if (roomText.text.Length == 6) {
                string roomID = roomText.text;
                print("房间号：" + roomID);
                DouniuJoinRoom req = DouniuJoinRoom.create(roomID);
                SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(req));

            }
        }

    }
}
