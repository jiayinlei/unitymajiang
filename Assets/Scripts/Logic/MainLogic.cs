/* ============================================================================
** Author	: 1130282072@qq.com
** Comments	: 游戏主逻辑，状态机入口，游戏初始化
** ----------------------------------------------------------------------------
** History	:	DATE			DESC                    
**				2017-7-2		Create by sunfei         
** ============================================================================
*/
using GameCore;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLogic : MonoBehaviour
{


    public GameCore.UiStateManager uiStateManager { get; set; }
    private static MainLogic _Controller = null;

    public static MainLogic Controller
    {
        get
        {
            if (_Controller == null)
            {
                _Controller = GameObject.Find("StaticSource").GetComponent<MainLogic>();
            }
            return _Controller;
        }
    }
    public GameType gameType = GameType.NULL;

    public void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        ////限制屏幕保护时间
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
#if UNITY_IPHONE
        Application.targetFrameRate = 60;
#else
        Application.targetFrameRate = 30;
#endif
        ////声音初始化设置，画质设置在此处
        System.Random random = new System.Random();
        bgFightMusicIndex = random.Next(2) + 1;
        ////注册界面
        UIManager.PageInit();
        Screen.orientation = ScreenOrientation.Portrait;
#if UNITY_EDITOR

#else
            Debug.Log("获取地理位置");
            LoginAndShare.Controller.GetPlaceInfo();
#endif
    }
    void Start()
    {
        //注册状态机
        this.uiStateManager = new GameCore.UiStateManager((int)GameCore.UIState.UIState_None
                                                            , null
                                                            , this.gameObject);
        //SocketMgr.GetInstance().InitNet();
        uiStateManager.Init();
    }
    AudioSource AS;
    int bgFightMusicIndex;
    public void SetBgMusic()
    {
        AS = GameObject.Find("StaticSource").GetComponent<AudioSource>();
        AS.clip = (AudioClip)Resources.Load("Music(UnUsed)/SoundEffect_Common/backmusic1", typeof(AudioClip));
        AS.volume = PlayerPrefs.GetFloat("MusicSet", 0.33f);
        PlayerPrefs.SetFloat("MusicSet", AS.volume);
        PlayerPrefs.Save();
        AS.Play();
    }
    public void SetFightBgMusic()
    {
        AS = GameObject.Find("StaticSource").GetComponent<AudioSource>();
        AS.clip = (AudioClip)Resources.Load(string.Format("Music(UnUsed)/SoundEffect_Common/bgFight0{0}", bgFightMusicIndex.ToString()), typeof(AudioClip));
        AS.volume = PlayerPrefs.GetFloat("MusicSet", 0.33f);
        PlayerPrefs.SetFloat("MusicSet", AS.volume);
        PlayerPrefs.Save();
        AS.Play();
    }
    public void ChangeState(int iStateId)
    {
        this.uiStateManager.SetNextSubState(iStateId);
    }
    void Update()
    {
        OnButtonHandled();
        uiStateManager.Update();
    }

    private bool isDown = false;
    private bool isUp = false;
    private void OnButtonHandled()
    {
        if (this.uiStateManager.NextSubStateID == (int)UIState.UIState_None ||
           this.uiStateManager.CurrentSubStateID() == (int)UIState.UIState_MJFightingPage || this.uiStateManager.CurrentSubStateID() == (int)UIState.UIState_LoadingPage)//棋牌战斗内未处理
            return;
        if (SceneManager.GetActiveScene().name.Equals("Horizontal"))
        {
            return;
        }
        if (SceneManager.GetActiveScene().name.Equals("BullFight")) {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isDown = true;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            isUp = true;
        }
        //TODO : sunfei 退出游戏处理 后期请将此功能使用android原生开发
        if (isDown && isUp)//
        {
            Debug.LogError("OnButtonHandled");
            GameObject tipsDialog = GameObject.Find("Canvas/addNode/TipsDialog");
            if (tipsDialog)
            {
                DestroyImmediate(tipsDialog);
            }
            else
            {
                TooL.showTips("确认退出游戏？", () => Application.Quit());
            }
            isDown = false;
            isUp = false;

        }
    }
}
public enum GameType
{
    NULL,
    MJ,
    DN,
}