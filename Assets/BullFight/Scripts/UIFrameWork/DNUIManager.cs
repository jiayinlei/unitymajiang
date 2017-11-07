using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public enum UIPanelType {
    DNGamePanel,
    SettingPanel,
    DNRulePanel,
    DissolvePanel,
    VotePanel,
    CountDownPanel,
    GameRulePanel,
    ChatPanel,
    ChangeClothPanel,
    AudioSettingPanel,
    ResultPanel,
    CountDown,
    Begin,
    VoicePanel,
    PlayerInfoPanel,
    NoticePanel,
    ExitPanel,
    WaitRulePanel
}

public class DNUIManager {

    /// 
    /// 单例模式的核心
    /// 1，定义一个静态的对象 在外界访问 在内部构造
    /// 2，构造方法私有化

    private static DNUIManager _instance;

    public static DNUIManager Instance {
        get {
            if (_instance == null) {
                _instance = new DNUIManager();
            }
            return _instance;
        }
    }

    private static Transform canvasTransform;
    public static Transform CanvasTransform {
        get {
            if (canvasTransform == null) {
                canvasTransform = GameObject.Find("Canvas/addNode").transform;
            }
            return canvasTransform;
        }
    }
    //private static Dictionary<UIPanelType, string> panelPathDict;//存储所有面板Prefab的路径
    private Dictionary<UIPanelType, BasePanel> panelDict;//保存所有实例化面板的游戏物体身上的BasePanel组件
    private Stack<BasePanel> panelStack;

    private DNUIManager() {
    }
    public void ClearStack() {
        panelStack.Clear();
    }
    public void ClearPanelDict() {
        panelDict.Clear();
    }
    /// <summary>
    /// 把某个页面入栈，  把某个页面显示在界面上
    /// </summary>
    public void PushPanel(UIPanelType panelType) {
        if (panelStack == null)
            panelStack = new Stack<BasePanel>();

        //判断一下栈里面是否有页面
        if (panelStack.Count > 0) {
            BasePanel topPanel = panelStack.Peek();
            topPanel.OnPause();
        }

        BasePanel panel = GetPanel(panelType);
        panel.OnEnter();
        if (!panelStack.Contains(panel)) {
            panelStack.Push(panel);
        }
    }
    /// <summary>
    /// 出栈 ，把页面从界面上移除
    /// </summary>
    public void PopPanel() {
        if (panelStack == null)
            panelStack = new Stack<BasePanel>();

        if (panelStack.Count <= 0)
            return;

        //关闭栈顶页面的显示
        BasePanel topPanel = panelStack.Pop();
        topPanel.OnExit();
        //panelStack.
        if (panelStack.Count <= 0)
            return;
        BasePanel topPanel2 = panelStack.Peek();
        topPanel2.OnResume();

    }
    /// <summary>
    /// 出栈 ，把页面从界面上移除
    /// </summary>
    public void PopReferPanel(UIPanelType panel) {
        if (panelStack == null)
            panelStack = new Stack<BasePanel>();

        if (panelStack.Count <= 0)
            return;

        //关闭栈顶页面的显示
        BasePanel topPanel = panelStack.Peek();
        List<BasePanel> tempPanel = new List<BasePanel>();
        if (topPanel.name != panel.ToString() && panelStack.Count > 0) {
            tempPanel.Add(panelStack.Pop());
            topPanel = panelStack.Peek();
            if (topPanel.name != panel.ToString() && panelStack.Count > 0) {

                tempPanel.Add(panelStack.Pop());
            } else {
                panelStack.Pop();
                topPanel.OnExit();
            }
        } else {
            panelStack.Pop();
            topPanel.OnExit();
        }
        foreach (var a in tempPanel) {
            panelStack.Push(a);
        }

        //topPanel.OnExit();
        //panelStack.
        if (panelStack.Count <= 0)
            return;
        BasePanel topPanel2 = panelStack.Peek();
        topPanel2.OnResume();

    }

    /// <summary>
    /// 根据面板类型 得到实例化的面板
    /// </summary>
    /// <returns></returns>
    private BasePanel GetPanel(UIPanelType panelType) {
        if (panelDict == null) {
            panelDict = new Dictionary<UIPanelType, BasePanel>();
        }
        BasePanel panel;
        panelDict.TryGetValue(panelType,out panel);
        //Debug.Log(panelType);
        //Debug.Log(panel);
        if (panel==null) {
            //如果找不到，那么就找这个面板的prefab的路径，然后去根据prefab去实例化面板
            string path;
            path = "PanelPrefabs/" + panelType.ToString();
            //Debug.Log(path);
            //panelPathDict.TryGetValue(panelType, out path);
            GameObject instPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject;
            instPanel.transform.SetParent(CanvasTransform, false);
            instPanel.name = instPanel.name.Replace("(Clone)","");
            panelDict.Add(panelType, instPanel.GetComponent<BasePanel>());
            return instPanel.GetComponent<BasePanel>();
        } else {
            return panel;
        }

    }
    GameObject obj;
    /// <summary>
    /// 仅仅获取面板
    /// </summary>
    /// <param name="panelType"></param>
    /// <returns></returns>
    public GameObject JustGetPanel(UIPanelType panelType) {
        //Debug.Log(panelType);
        //Debug.Log(panel);
        try {
            if (obj == null || obj.name != panelType.ToString()) {
                //如果找不到，那么就找这个面板的prefab的路径，然后去根据prefab去实例化面板
                string path;
                path = "PanelPrefabs/" + panelType.ToString();
                //Debug.Log(path);
                //panelPathDict.TryGetValue(panelType, out path);
                GameObject instPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject;
                instPanel.transform.SetParent(CanvasTransform, false);
                instPanel.name = panelType.ToString();
                obj = instPanel;
                return obj;
            }
        } catch (Exception ex) {
            string path;
            path = "PanelPrefabs/" + panelType.ToString();
            //Debug.Log(path);
            //panelPathDict.TryGetValue(panelType, out path);
            GameObject instPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject;
            instPanel.transform.SetParent(CanvasTransform, false);
            instPanel.name = panelType.ToString();
            obj = instPanel;
            return obj;
        }
        obj.SetActive(true);
        return obj;
       

    }
}
