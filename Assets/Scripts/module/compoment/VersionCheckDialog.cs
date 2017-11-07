using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public delegate void VersionCheckDialogCallBack();

public class VersionCheckDialog : MonoBehaviour
{
    private VersionCheckDialogCallBack okCallBack = null;
    private VersionCheckDialogCallBack cancleCallBack = null;
    public Text content;

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
    // todo 
    public void SetOkCallBack(VersionCheckDialogCallBack okCB)
    {
        this.okCallBack = okCB;
    }
    public void OnClickOk()
    {

        if (this.okCallBack != null)
        {
            this.okCallBack();
        }
        //Destroy(this.gameObject);
    }
    public void OnClickCancle()
    {
        //Destroy(this.gameObject);
    }
}
