using com.guojin.mj.net.message.login;
using GameCore;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameCore
{
    public class UIState_LoadingPage : EntityState
    {
        public static bool doNothing = false;
        public static bool isComeFromDN = false;
        public static bool isComeFromDNPlayeBack = false;
        public static bool toDNFightScene = false;


        public delegate void CallBack();
        public  CallBack callBack;
        UIManager.PageState pageState;
        private string toSceneName;
        public UIState_LoadingPage(int iStateId, EntityState eState) : base(iStateId, eState) { }
        protected override void OnStateBegin(object[] command)
        {
            if (doNothing)
            {
                doNothing = false;
            }
            bool isGameHall = false;
            string sceneName = SceneManager.GetActiveScene().name;
            if (sceneName.Equals("GameHall"))
            {
                //当前场景大厅场景
                isGameHall = true;
                pageState = UIManager.PageState.MjLoadingPage;
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                Screen.orientation = ScreenOrientation.AutoRotation;
                Screen.autorotateToLandscapeLeft = true;
                Screen.autorotateToLandscapeRight = true;
                Screen.autorotateToPortrait = false;
                Screen.autorotateToPortraitUpsideDown = false;
                switch (MainLogic.Controller.gameType)
                {
                    case GameType.NULL:
                        break;
                    case GameType.MJ:
                        Debug.Log("toSceneName = Horizontal");
                        toSceneName = "Horizontal";
                        callBack = delegate () { GoToMjFingtingPage();};
                        break;
                    case GameType.DN:
                        Debug.Log("toSceneName = BullFight");
                        toSceneName = "BullFight";
                        callBack = delegate () { GoToDNFightingPage();};
                        break;
                    default:
                        break;
                }
            }
            else if (sceneName.Equals("Login"))
            {
                //当前场景：登录场景
                pageState = UIManager.PageState.LoadingPage;
                UIState_GameHallPage.comeFromState = UIState_GameHallPage.ComeFromState.Login;
                toSceneName = "GameHall";
                callBack = delegate () { GoToGameHall(); };
            }
            else
            {
                //当前场景：战斗场景。需要设置
                Screen.orientation = ScreenOrientation.Portrait;
                pageState = UIManager.PageState.MjInSideLoadingPage;
                toSceneName = "GameHall";
                callBack = delegate () { GoToGameHall(); };
                switch (MainLogic.Controller.gameType)
                {
                    case GameType.NULL:
                        break;
                    case GameType.MJ:
                        UIState_GameHallPage.comeFromState = UIState_GameHallPage.ComeFromState.FromGameMjBack;
                        break;
                    case GameType.DN:
                        UIState_GameHallPage.comeFromState = UIState_GameHallPage.ComeFromState.FromDNFighting;
                        break;
                    default:
                        break;
                }
            }
            UIManager.ChangeUI(pageState, (GameObject obj) => {
                LoadingScenePageEvent.callBack += delegate () { callBack();};
                obj.GetComponent<LoadingScenePageEvent>().sceneName = toSceneName;
                obj.GetComponent<LoadingScenePageEvent>().InformationSetting();
            });      
        }

        public void GoToGameHall()
        {           
            GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_GameHallPage);
            GameObject.Find("StaticSource").GetComponent<MainLogic>().SetBgMusic();
            LoadingScenePageEvent.callBack = delegate { };
        }
        public void GoToDNFightingPage() {
            GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_DNFightingPage);
            LoadingScenePageEvent.callBack = delegate { };
        }
        public void GoToMjFingtingPage()
        {
            GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_MJFightingPage);
            GameObject.Find("StaticSource").GetComponent<MainLogic>().SetFightBgMusic ();
            LoadingScenePageEvent.callBack = delegate { };
        }
        protected override void OnStateEnd()
        {

        }

        protected override void OnUpdate()
        {

        }
        public override void OnFinishedMsg(string id)
        {

        }

    }
}
