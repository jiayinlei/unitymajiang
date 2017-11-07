using com.guojin.core.io.message;
using com.guojin.mj.net.message.club;
using com.guojin.mj.net;
using UnityEngine;
using com.guojin.mj.net.message;
using UnityEngine.UI;
/* ***********************************************
 * Describe:俱乐部逻辑
 * Author :  MuLongFei
 * Email: 424113771@qq.com
 * DATA: 2017/7/15 8:55:56 
 * FileName: ClubPageManager 
 * Version: V1.0.1
 * Version: V1.0.2 提示界面clubHintPage取消
 * ***********************************************/

class ClubPageManager : Observer {
    
    public static ClubPageManager instance;

    private Button clubAddPage_closeBtn,clubAddPage_SureBtn, clubInfoPage_CloseBtn; //clubHintPage_CloseBtn, clubHintPage_SureBtn, clubHintPage_CancleBtn
    private InputField clubAddPage_ClubIputID;
    private Text clubInfoPage_TitleText, clubInfoPage_ClubInfoText, clubInfoPage_ClubNoticeText,ClubNoticeText, ClubINameText, ClubOwnerText
        ,WXNumText, peopleNumText;//clubHintPage_ClubInfoText
    private Transform gameHallPage;

    private GameObject clubAddPage;
    //private GameObject clubHintPage;
    private GameObject clubInfoPage;

    private ClubInfo tempClubInfo;

    private int idLength = 2;

    public delegate void CallBack();
    public CallBack callBack;


    private void Awake() {
        instance = this;
    }

    public void Start() {
        initMsg();

        //初始化UI
        gameHallPage = GameObject.Find("Canvas/addNode/NewGameHall(Clone)").transform;
        clubAddPage = transform.FindChild("ClubAddPage").gameObject;
        clubInfoPage = transform.FindChild("ClubInfoPage").gameObject;

        clubAddPage_closeBtn = transform.FindChild("ClubAddPage/CloseBtn").GetComponent<Button>();
        clubAddPage_SureBtn = transform.FindChild("ClubAddPage/Bg/SureBtn").GetComponent<Button>();
        clubAddPage_ClubIputID = transform.FindChild("ClubAddPage/Bg/InputField").GetComponent<InputField>();
        clubAddPage_ClubIputID.shouldHideMobileInput = true;

        clubInfoPage_CloseBtn = transform.FindChild("ClubInfoPage/Bg/CloseBtn").GetComponent<Button>();
        clubInfoPage_TitleText = transform.FindChild("ClubInfoPage/Bg/TopImage/ClubCreator").GetComponent<Text>();
        ClubNoticeText = transform.FindChild("ClubInfoPage/Bg/ClubNoticeText").GetComponent<Text>();
        ClubINameText = transform.FindChild("ClubInfoPage/Bg/ClubINameText").GetComponent<Text>();
        ClubOwnerText = transform.FindChild("ClubInfoPage/Bg/ClubOwnerText").GetComponent<Text>();
        WXNumText = transform.FindChild("ClubInfoPage/Bg/WXNumText").GetComponent<Text>();
        peopleNumText = transform.FindChild("ClubInfoPage/Bg/peopleNumText").GetComponent<Text>();
        //clubInfoPage_ClubNoticeText = transform.FindChild("ClubInfoPage/Bg/ClubNoticeText").GetComponent<Text>();

        //注册事件
        clubAddPage_closeBtn.onClick.AddListener(OnCloseBtnClicked);
        clubAddPage_SureBtn.onClick.AddListener(SendJoinClubReq);
        clubInfoPage_CloseBtn.onClick.AddListener(OnCloseBtnClicked);

        //clubHintPage = transform.FindChild("ClubHintPage").gameObject;peopleNumText
        //clubHintPage_CloseBtn = transform.FindChild("ClubHintPage/Bg/CloseBtn").GetComponent<Button>();
        //clubHintPage_SureBtn = transform.FindChild("ClubHintPage/SureBtn").GetComponent<Button>();
        //clubHintPage_CancleBtn = transform.FindChild("ClubHintPage/CancleBtn").GetComponent<Button>();
        //clubHintPage_ClubInfoText = transform.FindChild("ClubHintPage/Bg/ClubInfo").GetComponent<Text>();
        //clubHintPage_CancleBtn.onClick.AddListener(BackToAddClubPage);
        //clubHintPage_SureBtn.onClick.AddListener(ShowClubInfoPage);
        //clubHintPage_CloseBtn.onClick.AddListener(OnCloseBtnClicked);

        ShowUI();
    }

    private void ShowClubInfoPage() {
        ShowPanal("ClubInfoPage");
        //确定加入俱乐部
        if (tempClubInfo != null) {
            ClubInformationCache.instance.HasInClub = true;
            ClubInformationCache.instance.ClubId = tempClubInfo.ClubId;
            ClubInformationCache.instance.CreateUserName = tempClubInfo.CreateUserName;
            ClubInformationCache.instance.Notice = tempClubInfo.Notice;
            ClubInformationCache.instance.WxNo = tempClubInfo.WxNo;
        }

        SetClubInfo();

    }

    private void BackToAddClubPage() {
        ShowPanal("ClubAddPage");
    }

    private void SendJoinClubReq() {
        if (clubAddPage_ClubIputID.text.Length >= idLength) {
            ////todo 向服务器发送加入俱乐部请求
            SendMessageMgr.JoinClub(clubAddPage_ClubIputID.text);
            Debug.Log(ClubStaticField.DEBUG_SEND_JOIN_CLUB_REQ);

        } else {
            //弹窗提示输入有误
            PopWnd(ClubStaticField.ERROR_INPUT); 
        }
    }

    private void OnCloseBtnClicked() {
        callBack.Invoke();
        Destroy(gameObject);
    }

    protected override string[] GetMsgList() {
        return new string[] {
                MessageFactoryImpi.instance.getMessageString(7,34),
                MessageFactoryImpi.instance.getMessageString(7,36)
        };
    }

    public override void OnMsg(string msg, Message data) {
        Debug.Log(string.Format(ClubStaticField.CLUB_REVEIVE_MSG,data.getMessageType(),data.getMessageId()));
        string tempMsg = data.getMessageType() + "-" + data.getMessageId();
        
        if (tempMsg == "7-34") {
            JoinClubRet resp = data as JoinClubRet;
            if (!resp.Result) {
                switch (resp.ReasonType) {
                    case -1:
                        //服务器错误
                        PopWnd(ClubStaticField.ERROR_UNKNOW);
                        break;
                    case 0:
                        //没有此俱乐部
                        if (GameObject.Find("Canvas/addNode/NoticePage(Clone)") == null) {
                            PopWnd(ClubStaticField.CLUB_NOT_FOUND);
                        }
                        break;
                }
            }
        }

        if (tempMsg == "7-36") {
            ClubInfo resp = data as ClubInfo;
            tempClubInfo = resp;

            ShowClubInfoPage();

        }
    }

    private void ShowPanal(string name) {
        clubInfoPage.SetActive(name == clubInfoPage.name);
        clubAddPage.SetActive(name == clubAddPage.name);
        //clubHintPage.SetActive(name == clubHintPage.name);
    }

    private void SetClubInfo() {
        clubInfoPage_TitleText.text = ClubInformationCache.instance.CreateUserName + ClubStaticField.CLUB_CREATOR;
        ClubNoticeText.text = ClubStaticField.CLUB_INFO;
        ClubINameText.text = "俱乐部名称："+ ClubInformationCache.instance.ClubId;
        ClubOwnerText.text = "俱乐部主人："+ ClubInformationCache.instance.CreateUserName;
        WXNumText.text = "微信号："+ ClubInformationCache.instance.WxNo;
        peopleNumText.text = "";
        //clubInfoPage_ClubInfoText.text = string.Format(
        //    ClubStaticField.CLUB_INFO, 
        //    ClubInformationCache.instance.ClubId, 
        //    ClubInformationCache.instance.CreateUserName, 
        //    ClubInformationCache.instance.WxNo);
        if (ClubInformationCache.instance.Notice != null) {
            ClubNoticeText.text = ClubInformationCache.instance.Notice;
        } else {
            ClubNoticeText.text = ClubStaticField.CLUB_NO_NOTICE;
        }
    }

    //首次点击俱乐部显示
    private void ShowUI() {
        if (ClubInformationCache.instance.HasInClub) {
            //显示俱乐部信息界面
            ShowPanal("ClubInfoPage");
            //设置信息
            SetClubInfo();
        } else {
            //显示加入俱乐部界面
            ShowPanal("ClubAddPage");
        }
    }

    /// <summary>
    /// 弹窗
    /// </summary>
    /// <param name="message"></param>
    private void PopWnd(string message) {
        UIManager.ChangeUI(UIManager.PageState.NoticePage, (GameObject obj) => {
            obj.GetComponent<NoticePageEvent>().InformationSetting();
            obj.GetComponent<NoticePageEvent>().SetHintMessage(message);
        });
    }
}
