using UnityEngine;
using UnityEngine.UI;

public class ChatInfo : MonoBehaviour {

    private Text chatInfo;
    public InputField inputText;
    public Button sendBtn;
    private GameObject showPhraseorface;
    private float time;
    private GameObject[] intUserIndex;
    public GameObject imagePrefab;
    // Use this for initialization
    void Start () {
        sendBtn.onClick.AddListener(ClickSendMessageBtn);
        intUserIndex = GameObject.FindGameObjectsWithTag("chatcontent");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userIndex">玩家的位置</param>
    /// <param name="chatContent">聊天内容</param>
    /// <param name="num">发送的类型1.文字 2.表情 3.语音</param>
    public void ShowChatContent(int userIndex,string chatContent,int num)
    {
        showPhraseorface = intUserIndex[Method.intArr[userIndex]].gameObject;
        chatInfo = showPhraseorface.AddComponent<Text>();
        TooL.destroyAllChildren(this.showPhraseorface);
        chatInfo.text = "";
        Destroy(chatInfo, 3);
        if (num == 1)
        {
            chatInfo.text = chatContent;
        }
        else if (num == 2)
        {
            int index = int.Parse(chatContent);
            GameObject obj = TooL.clone(imagePrefab, showPhraseorface);
            Image image = obj.GetComponent<Image>();
            image.sprite = ExpressionPanel.expressionSprite[int.Parse(chatContent)];
            Destroy(obj, 3);
        }
          else if (num == 3)
        {

        }
    }
    void ClickSendMessageBtn()
    {
        com.guojin.mj.net.message.game.PlayerSendChatInfo PSCI = com.guojin.mj.net.message.game.PlayerSendChatInfo.create(inputText.text, 1);
        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(PSCI));
        Destroy(GameObject.Find("MessagePanel"));
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Return) && inputText.text != string.Empty)
        {
            ClickSendMessageBtn();
        }
       //if (chatInfo.text!=string.Empty)
       // {
       //     time += Time.deltaTime;
       //     if (time>=3.0f)
       //     {
       //         chatInfo.text = "";
       //         time = 0;
       //     }
       // }
    }
}
