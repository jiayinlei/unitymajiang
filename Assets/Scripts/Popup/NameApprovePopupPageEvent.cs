using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameApprovePopupPageEvent : EventManager
{
    public override void InformationSetting()
    {

    }
    void CloseBtnClick() {
        transform.PlayButtonVoice();
        DestroyImmediate(this.gameObject);
    }
    void SureBtnClick() {
        transform.PlayButtonVoice();
        string strName = this.BindingSource[0].GetComponent<InputField>().text;
        if (strName.Equals(""))
        {
            TooL.showTips("请输入您真实的名字!", () => { });
            return;
        }
        string strID   = this.BindingSource[1].GetComponent<InputField>().text;
        if (strID.Length!=18 && strID.Length != 15)
        {
            TooL.showTips("请输入您真实的身份证号!", () => { });
            return;
        }
        bool result = CheckIDCard(strID);
        //string strBirth = strID.Substring(6,8);
        if (result)
        {
            //发送给服务器保存
            Debug.Log("发送给服务器保存");
            PlayerPrefs.SetInt("identitycard", 1);
            PlayerPrefs.Save();
            UIManager.ChangeUI(UIManager.PageState.NoticePage, (GameObject obj) =>
            {
                obj.GetComponent<NoticePageEvent>().InformationSetting();
                obj.GetComponent<NoticePageEvent>().SetHintMessage("身份验证成功");
            });
            this.CloseBtnClick();
        }
        else
        {
            TooL.showTips("请输入您真实的身份证号!", () => { });
        }
    }
    /// <summary>  
    /// 验证身份证合理性  
    /// </summary>  
    /// <param name="Id"></param>  
    /// <returns></returns>  
    public bool CheckIDCard(string idNumber)
    {
        if (idNumber.Length == 18)
        {
            bool check = CheckIDCard18(idNumber);
            return check;
        }
        else if (idNumber.Length == 15)
        {
            bool check = CheckIDCard15(idNumber);
            return check;
        }
        else
        {
            return false;
        }
    }


    /// <summary>  
    /// 18位身份证号码验证  
    /// </summary>  
    private bool CheckIDCard18(string idNumber)
    {
        long n = 0;
        if (long.TryParse(idNumber.Remove(17), out n) == false
            || n < Math.Pow(10, 16) || long.TryParse(idNumber.Replace('x', '0').Replace('X', '0'), out n) == false)
        {
            return false;//数字验证  
        }
        string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
        if (address.IndexOf(idNumber.Remove(2)) == -1)
        {
            return false;//省份验证  
        }
        string birth = idNumber.Substring(6, 8).Insert(6, "-").Insert(4, "-");
        DateTime time = new DateTime();
        if (DateTime.TryParse(birth, out time) == false)
        {
            return false;//生日验证  
        }
        string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
        string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
        char[] Ai = idNumber.Remove(17).ToCharArray();
        int sum = 0;
        for (int i = 0; i < 17; i++)
        {
            sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
        }
        int y = -1;
        Math.DivRem(sum, 11, out y);
        if (arrVarifyCode[y] != idNumber.Substring(17, 1).ToLower())
        {
            return false;//校验码验证  
        }
        return true;//符合GB11643-1999标准  
    }


    /// <summary>  
    /// 16位身份证号码验证  
    /// </summary>  
    private bool CheckIDCard15(string idNumber)
    {
        long n = 0;
        if (long.TryParse(idNumber, out n) == false || n < Math.Pow(10, 14))
        {
            return false;//数字验证  
        }
        string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
        if (address.IndexOf(idNumber.Remove(2)) == -1)
        {
            return false;//省份验证  
        }
        string birth = idNumber.Substring(6, 6).Insert(4, "-").Insert(2, "-");
        DateTime time = new DateTime();
        if (DateTime.TryParse(birth, out time) == false)
        {
            return false;//生日验证  
        }
        return true;
    }
    // Use this for initialization
    void Start()
    {
        Open();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
