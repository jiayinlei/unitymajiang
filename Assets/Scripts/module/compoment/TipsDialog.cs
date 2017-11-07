using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public delegate void TipsDialogCallBack();

public class TipsDialog : MonoBehaviour
{
    private TipsDialogCallBack okCallBack = null;
    private TipsDialogCallBack cancleCallBack = null;
    public Text content;
    public Text sure;
    public Text cancel;

    // Use this for initialization
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }
    public void SetContent(string str)
    {
        this.content.text = str;
    }
    public void SetBtnContent(string surestr,string cancelstr)
    {
        this.sure.text = surestr;
        this.cancel.text = cancelstr;
    }
    public void SetOkCallBack(TipsDialogCallBack okCB)
    {
        this.okCallBack = okCB;
    }
    public void SetCancleCallBack(TipsDialogCallBack cancleCB)
    {
        this.cancleCallBack = cancleCB;
    }
    public void OnClickOk()
    {

        Application.Quit();
        //EditorApplication.isPlaying = false;
        if (this.okCallBack != null)
        {
            this.okCallBack();
        }
        Destroy(this.gameObject);
    }
    public void OnClickCancle()
    {
        if (this.cancleCallBack !=null)
        {
            this.cancleCallBack();
        }
        Destroy(this.gameObject);
    }
}
