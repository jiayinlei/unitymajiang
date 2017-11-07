using GameCore;
using UnityEngine;

namespace GameCore
{
    public class UIState_MJFightingPage : EntityState
    {
        public static bool doNothing = false;

        public UIState_MJFightingPage(int iStateId, EntityState eState) : base(iStateId, eState) { }
        protected override void OnStateBegin(object[] command)
        {

            if (doNothing)
            {
                doNothing = false;
                return;
            }
            //GameObject staticSource = GameObject.Find("StaticSource");
            //if (staticSource != null)
            //{
            //    if (staticSource.GetComponent<MainLogic>().uiStateManager.GetOldStateID() == (int)GameCore.UIState.UIState_LoginPage)
            //    {
            //        UIManager.ChangeUI(UIManager.PageState.LoadingPage, (GameObject obj) =>
            //        {
            //            GameObject loginPage = GameObject.Find("LoginPage(Clone)");
            //            if (loginPage)
            //            {
            //                Destroy(loginPage);
            //            }
            //            obj.GetComponent<LoadingScenePageEvent>().callBack += delegate () { GoToGameHall(); };
            //            obj.GetComponent<LoadingScenePageEvent>().sceneName = "GameHall";
            //            obj.GetComponent<LoadingScenePageEvent>().InformationSetting();

            //        });
            //    }

            //}



        }
        public void GoToGameHall()
        {
            GameObject.Find("StaticSource").GetComponent<MainLogic>().ChangeState((int)GameCore.UIState.UIState_GameHallPage);
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

        }

    }
}
