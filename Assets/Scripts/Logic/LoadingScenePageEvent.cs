using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LoadingScenePageEvent: EventManager 
{
    //public Slider processBar;
    public delegate void CallBack();
    public static CallBack callBack;
    private AsyncOperation async;
    public string sceneName { get; set; }
    private int nowProcess;
    private string[] stringArr = { "", ".", "..", "..." };
    private string str;
    void Start()
    {
        GameObject staticSource = GameObject.Find("StaticSource");  
        if (staticSource.GetComponent<MainLogic>().uiStateManager.GetOldStateID() == (int)GameCore.UIState.UIState_LoginPage)
        {
            str = "资源加载中，不消耗流量"; 
        }
        else
        {
            str = "加载中";
        }
        StartCoroutine(PlayText(0));
    }
    IEnumerator PlayText(int index)
    {
        SetLable (this .BindingSource [0], str + stringArr[index]);
        yield return new WaitForSeconds(0.1f);
        index++;
        if (index >= stringArr.Length) index = 0;
        StartCoroutine(PlayText(index));
    }
    /// <summary>  
    /// 加载完场景后就会跳转  
    /// </summary>  
    /// <returns></returns>  
    IEnumerator loadScene()
    {
        SceneManager.sceneLoaded += delegate { callBack(); };
        async = SceneManager.LoadSceneAsync(sceneName);
        Debug.Log("开始加载场景...");
        async.allowSceneActivation = false;
        yield return async;
    }
    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        throw new NotImplementedException();
    }
    void Update()
    {
        if (async == null)
        {
            return;
        }
        if (async.progress < 0.9f)
        {
            return;
        }
        else
        {
            async.allowSceneActivation = true;
        }    
    }

    public override void InformationSetting()
    {
        StartCoroutine(loadScene());
    }
}