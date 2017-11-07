using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class UIManager :MonoBehaviour
{
    #region 第一版：未抽取主prefab通用模板，界面缓存机制待完善,uiDepth未处理，注册模式未精简


    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new UIManager();
            return _instance;
        }
    }
    /// <summary>
    /// 页面字典
    /// </summary>
    public static Dictionary<PageState, MyPageData.Data> UiDic { get; set; }
    /// <summary>
    /// 显示层级
    /// </summary>
    public static Dictionary<PageDepth, GameObject> UiPageDepth { get; set; }
    /// <summary>
    /// 注册界面
    /// </summary>
    public static void PageInit()
    {
        MyPageData _MyPageData = new MyPageData();
        _MyPageData.MyPageDataList = new List<MyPageData.Data>();
        //------------------在下面注册界面---------------------------------------
        _MyPageData.MyPageDataList.Add(new MyPageData.Data() {pageState = PageState.login,FoldName = "Prefab",PerfabName = "LoginPage"});
        _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.GameHall, FoldName = "Prefab", PerfabName = "NewGameHall" });
        _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.LoadingPage , FoldName = "Prefab", PerfabName = "LoadingPage" });
        _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.MJCreatRoomPage, FoldName = "MjPrefab", PerfabName = "MJCreatRoomPage" });
        _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.ZJHCreateRoomPage, FoldName = "ZJHPrefab", PerfabName = "ZJHCreatRoomPage" });
        _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.NetConnectPage, FoldName = "Prefab", PerfabName = "NetConnectPage" });
        _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.PDKCreateRoomPage, FoldName = "Prefab", PerfabName = "PDKCreatRoomPage" });
        _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.PlayerInforPopPage, FoldName = "Prefab", PerfabName = "PlayerInforPopPage" });
        _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.BuyRoomCardPage, FoldName = "Prefab", PerfabName = "BuyRoomCardPage" });
        _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.NoticePage, FoldName = "Prefab", PerfabName = "NoticePage" });
        _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.MoreSetPage, FoldName = "Prefab", PerfabName = "MoreSetPage" });
        _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.ReportPage , FoldName = "Prefab", PerfabName = "ReportPage" });
        _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.MjLoadingPage, FoldName = "Prefab", PerfabName = "MJLoadingPage" });
        _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.ClubAddPage, FoldName = "Prefab", PerfabName = "ClubAddPage" });
        _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.SysNoticePage, FoldName = "Prefab", PerfabName = "SysNoticePage" });
        _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.RewardPage, FoldName = "Prefab", PerfabName = "RewardPage" });
        _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.ResultPage, FoldName = "Prefab", PerfabName = "FightRecordPage" });
        _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.VersionCheckDialog, FoldName = "Prefab", PerfabName = "VersionCheckDialog" });
        _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.SharePage, FoldName = "Prefab", PerfabName = "SharePage" });
        _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.PlaybackListPage, FoldName = "Prefab", PerfabName = "PlaybackListPage" });
        _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.GameHallNoticePage, FoldName = "Prefab", PerfabName = "GameHallNoticePage" });
        _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.NameApprovePopupPage, FoldName = "Prefab/Popup", PerfabName = "NameApprovePopupPage" });
        _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.GameSetPage, FoldName = "Prefab/Popup", PerfabName = "GameSetPage" });
        _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.TempPage, FoldName = "Prefab", PerfabName = "TempPage" });
        _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.MjInSideLoadingPage, FoldName = "Prefab", PerfabName = "MjInSideLoadingPage" });
        _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.ShareQRcodePage, FoldName = "Prefab", PerfabName = "ShareQRcodePage" });
        _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.DNMainPanelPage, FoldName = "BullFightPrefab", PerfabName = "DNMainPanelPage" });
        _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.DNWaitPanel, FoldName = "BullFightPrefab", PerfabName = "WaitPanel" });
        _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.DNReferPanel, FoldName = "BullFightPrefab", PerfabName = "ReferPanel" });
         _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.DNNetConnectPage, FoldName = "BullFightPrefab", PerfabName = "DNNetConnectPage" });
         _MyPageData.MyPageDataList.Add(new MyPageData.Data() { pageState = PageState.DNReferGamePanel, FoldName = "BullFightPrefab", PerfabName = "DNReferGamePanel" });
        //------------------在上面注册界面---------------------------------------
        UiDic = _MyPageData.MyPageDataList.ToDictionary((p) => p.pageState, (q) => q);
        Debug.Log("加载ui配置表完成");
    }


    /// <summary>
    /// 页面切换
    /// </summary>
    /// <param name="state">切换目标</param>
    /// <param name="callBack">切换回调</param>
    public delegate void CallBack(GameObject child);
    public static void ChangeUI(PageState state, CallBack callBack = null, float delayTime = -1)
    {
        //LoginAndShare.Controller.StartCoroutine( ChangeUiByIEnumerator(state,callBack,delayTime));
        ChangeUiByIEnumerator(state, callBack, delayTime);
    }
    public static void ChangeUiByIEnumerator(PageState state, CallBack callBack = null, float delayTime = -1)
    {

        //if (delayTime != -1)
        //{
        //    yield return new WaitForSeconds(delayTime);
        //}
        //WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        ///切换目标对象
        //MyPageData.Data data = UiDic[state];
        //GameObject father = null;
        //father = GameObject.Find("addNode");
        //GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>(string.Format("{0}/{1}", data.FoldName, data.PerfabName)));
        //try
        //{
        //    EventManager eventManager = obj.GetComponent<EventManager>();
        //    obj.GetComponent<EventManager>().templateEventManager = eventManager;
        //    obj.GetComponent<EventManager>().templateEventManager.Open();
        //}
        //catch (System.Exception ex)
        //{
        //    Debug.LogError(obj.name);
        //    throw ex;
        //}
        //yield return waitForEndOfFrame;
        /////坐标位置 拉伸适应
        //SetAnchorPoint(obj, father);
        //yield return waitForEndOfFrame;
        //UpdateUiInfor(callBack, obj);

        //callBack = null;
        MyPageData.Data data = UiDic[state];
        GameObject father = null;
        father = GameObject.Find("addNode");
        GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>(string.Format("{0}/{1}", data.FoldName, data.PerfabName)));
        try
        {
            EventManager eventManager = obj.GetComponent<EventManager>();
            obj.GetComponent<EventManager>().templateEventManager = eventManager;
            obj.GetComponent<EventManager>().templateEventManager.Open();
        }
        catch (System.Exception ex)
        {
            Debug.LogError(obj.name);
            throw ex;
        }

        ///坐标位置 拉伸适应
        SetAnchorPoint(obj, father);
        UpdateUiInfor(callBack, obj);

        callBack = null;
    }
    //显示对话框
    public void ShowDialog(string msg, DialogType type = DialogType.None)
    {

    }
    public static void UpdateUiInfor(CallBack callBack, GameObject obj)
    {
        //yield return new WaitForEndOfFrame();
        try
        {
            if (callBack != null)
            {
                callBack(obj);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            callBack = null;
        }
    }
    /// <summary>
    /// 初始化新生成目标的锚点与缩放
    /// </summary>
    /// <param name="obj">目标</param>
    /// <param name="father">参照对象</param>
    public static void SetAnchorPoint(GameObject prefab, GameObject parentNode)
    {
        prefab.transform.SetParent(parentNode.transform,false);
        prefab.transform.localPosition = new Vector3(0, 0, 0);
        prefab.transform.localRotation = parentNode.transform.rotation;
        prefab.transform.localScale = parentNode.transform.localScale;
    }

    
    /// <summary>
    /// 关闭页面统一接口
    /// </summary>
    /// <param name="page"></param>
    public static void ClosePage(PageState page)
    {
        //if (UiCache.Contains(page))
        //{
        //    UiDic[page].ClosePage();
        //    UiCache.Remove(page);
        //}
    }
    #endregion
    /// <summary>
    /// 页面
    /// </summary>
    public enum PageState
    {
        /// <summary>
        /// 空
        /// </summary>
        None,

        /// <summary>
        /// 登陆
        /// </summary>
        login,

        /// <summary>
        /// 大厅
        /// </summary>
        GameHall,
        /// <summary>
        /// 加载界面
        /// </summary>
        LoadingPage,
        /// <summary>
        /// 麻将创建房间
        /// </summary>
        MJCreatRoomPage,
        /// <summary>
        /// 炸金花创建房间
        /// </summary>
        ZJHCreateRoomPage,
        /// <summary>
        /// 跑得快创建房间
        /// </summary>
        PDKCreateRoomPage,
        /// <summary>
        ///菊花
        /// </summary>
        NetConnectPage,
        /// <summary>
        /// 玩家信息弹窗
        /// </summary>
        PlayerInforPopPage,
        /// <summary>
        /// 购买房卡
        /// </summary>
        BuyRoomCardPage,
        /// <summary>
        /// 提示
        /// </summary>
        NoticePage,
        /// <summary>
        /// 更多设置
        /// </summary>
        MoreSetPage,
        /// <summary>
        /// 提交报告
        /// </summary>
        ReportPage,
        MjLoadingPage,

        ClubAddPage,//加入俱乐部
        ClubHintPage,//加入俱乐部提示
        ClubInfoPage,//俱乐部信息

        SysNoticePage,
        RewardPage,
        ResultPage,
        VersionCheckDialog,
        SharePage,
        PlaybackListPage,
        GameHallNoticePage,
        NameApprovePopupPage,
        GameSetPage,
        TempPage,
        MjInSideLoadingPage,
        ShareQRcodePage,
        DNMainPanelPage,
        DNWaitPanel,
        DNReferPanel,
        DNNetConnectPage,
        DNReferGamePanel,
        //----------在上面添加-----------------------------
        /// 最大值 不允许超过该值 枚举最大值
        /// </summary>
        ///</summary>
        Max,
    }
    /// <summary>
    /// 页面显示层级关系
    /// </summary>
    public enum PageDepth
    {
        None = 0,
        SystemPage = 8,
        Max
    }
    /// <summary>
    /// 对话框类型
    /// </summary>
    public enum DialogType
    {
        None = 0,
    }
    public static List<GameObject> GetChild(GameObject obj)
    {
        List<GameObject> list = new List<GameObject>();
        int i = 0;
        while (i < obj.transform.childCount)
        {
            Transform parent = obj.transform.GetChild(i);
            list.Add(parent.gameObject);
            i++;
        }
        return list;
    }
}
public class MyPageData
{
    public List<Data> MyPageDataList
    {
        get;
        set;
    }

    public class Data
    {
        public UIManager.PageState pageState { get; set; }
        private UIManager.PageDepth PageDepth { get; set; }
        public string FoldName { get; set; }
        public string PerfabName { get; set; }
    }

}
