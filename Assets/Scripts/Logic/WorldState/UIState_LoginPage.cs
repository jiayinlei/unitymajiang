using GameCore;
using System.IO;
using System.Text;
using UnityEngine;

namespace GameCore
{
    public class UIState_LoginPage : EntityState
    {
        public static bool doNothing = false;

        public UIState_LoginPage(int iStateId, EntityState eState) : base(iStateId, eState) { }
        protected override void OnStateBegin(object[] command)
        {

            if (doNothing)
            {
                doNothing = false;
                return;
            }
            //todo:fei
            try
            {
                string strHttpPostBack =  HttpPostData(URLConst.VersionURL, "version=" + "1.0.5");
                VersionCheckCallBack(strHttpPostBack);
            }
            catch (System.Exception)
            {

                //throw;
                UIManager.ChangeUI(UIManager.PageState.login, (GameObject obj) =>
                {
                    obj.GetComponent<LoginPageEvent>().InformationSetting();
                });
            }
            
        }
        /// <summary>
        /// sresult 格式1.0.5.0_1_2,   1.0.5是版本号，0_1_2是功能开关
        /// </summary>
        /// <param name="sresult"></param>
        void VersionCheckCallBack( string sresult)
        {
            Debug.Log("服务器返回版本" + sresult);
            string Sever_big_0 = sresult.Split('.')[0];
            string Sever_big_1 = sresult.Split('.')[1];
            string Sever_small_0 = sresult.Split('.')[2];
#if UNITY_IPHONE
            if (sresult.Split('.').Length==4)
            {
                string functionNum = sresult.Split('.')[3];
                for (int i = 0; i < functionNum.Split('_').Length; i++)
                {
                    LoginAndShare.Controller.functionNum.Add(int.Parse(functionNum.Split('_')[i]));
                }
                Debug.Log("从服务器获取的功能开关" + functionNum);
            }
#endif
            string Local_big_0 = DefaultKeyConst.version.Split('.')[0];
            string Local_big_1 = DefaultKeyConst.version.Split('.')[1];
            string Local_small_0 = DefaultKeyConst.version.Split('.')[2];

            string SeverBigVersionNum = Sever_big_0 + Sever_big_1;
            string LocalBigVersionNum = Local_big_0 + Local_big_1;
            if (int.Parse(SeverBigVersionNum) > int.Parse(LocalBigVersionNum))
            {
                TooL.VersionCheckTips("当前版本过旧！请下载新版本！", () =>
                {
                    PlayerPrefs.DeleteAll();
                    LoginAndShare.Controller.GetNewVersion();
                });
            }
            else if (int.Parse(SeverBigVersionNum) == int.Parse(LocalBigVersionNum) && int.Parse(Sever_small_0)>int.Parse(Local_small_0))
            {
                string oldSmallVersion = PlayerPrefs.GetString("oldSmallVersion","");
                if (oldSmallVersion.Equals("") || int.Parse(Sever_small_0) > int.Parse(oldSmallVersion))
                {
                    TooL.showTips("当前版本过旧！请下载新版本！", () =>
                    {
                        PlayerPrefs.DeleteAll();
                        LoginAndShare.Controller.GetNewVersion();
                    }, () => {
                        PlayerPrefs.SetString("oldSmallVersion", Sever_small_0);
                        PlayerPrefs.Save();
                        UIManager.ChangeUI(UIManager.PageState.login, (GameObject obj) =>
                        {
                            obj.GetComponent<LoginPageEvent>().InformationSetting();
                        });
                    },"更新","忽略");
                }
                else
                {
                    UIManager.ChangeUI(UIManager.PageState.login, (GameObject obj) =>
                    {
                        obj.GetComponent<LoginPageEvent>().InformationSetting();
                    });
                }           
            }
            else
            {
                UIManager.ChangeUI(UIManager.PageState.login, (GameObject obj) =>
                {
                    obj.GetComponent<LoginPageEvent>().InformationSetting();
                });
            }



        }

        public  string HttpPostData(string url, string param)
        {
            var result = string.Empty;
            //注意提交的编码 这边是需要改变的 这边默认的是Default：系统当前编码
            byte[] postData = Encoding.UTF8.GetBytes(param);

            // 设置提交的相关参数 
            System.Net.HttpWebRequest request = System.Net.WebRequest.Create(url) as System.Net.HttpWebRequest;
            Encoding myEncoding = Encoding.UTF8;
            request.Method = "POST";
            request.KeepAlive = false;
            request.AllowAutoRedirect = true;
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR  3.0.04506.648; .NET CLR 3.5.21022; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)";
            request.ContentLength = postData.Length;
            request.Timeout = 3000;

            // 提交请求数据 
            System.IO.Stream outputStream = request.GetRequestStream();
            outputStream.Write(postData, 0, postData.Length);
            outputStream.Close();

            System.Net.HttpWebResponse response;
            Stream responseStream;
            StreamReader reader;
            string srcString;
            response = request.GetResponse() as System.Net.HttpWebResponse;
            responseStream = response.GetResponseStream();
            reader = new System.IO.StreamReader(responseStream, Encoding.GetEncoding("UTF-8"));
            srcString = reader.ReadToEnd();
            result = srcString;   //返回值赋值
            reader.Close();
            Debug.Log("获取HttpPostData服务器返回Version" + result);
            return result;
        }


        //-------------------------------------------------------------------------
        protected override void OnStateEnd()
        {

        }
        //-------------------------------------------------------------------------
        protected override void OnUpdate()
        {

        }
        public override void OnFinishedMsg(string id)
        {

            Debug.Log("UIState_LoginPage.OnFinishedMsg");
        }

    }
}
