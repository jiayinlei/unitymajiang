using UnityEngine;

namespace GameCore
{
    public enum UIState
    {
        UIState_None = 0,
        UIState_LoginPage,                  // 登陆
        UIState_GameHallPage,               //大厅
        UIState_LoadingPage,                //加载
        UIState_MJFightingPage,             //
        UIState_DNFightingPage,             //斗牛战斗
    }

    public class UiStateManager : EntityState
    {
        public UiStateManager(int stateid, EntityState parent, GameObject gameObject)
            : base(stateid, parent, gameObject)
        {
        }
        protected override void OnStateInit()
        {
            AddSubState(new UIState_LoginPage((int)UIState.UIState_LoginPage, this));
            AddSubState(new UIState_GameHallPage((int)UIState.UIState_GameHallPage,this));
            AddSubState(new UIState_LoadingPage((int)UIState.UIState_LoadingPage ,this));
            AddSubState(new UIState_MJFightingPage((int)UIState.UIState_MJFightingPage, this));
            AddSubState(new UIState_MJFightingPage((int)UIState.UIState_DNFightingPage, this));
            //------------------注册状态机---------------------------
            SetNextSubState((int)UIState.UIState_LoginPage);

            base.OnStateInit();
        }
        //-------------------------------------------------------------------------
        protected override void OnStateBegin(object[] command)
        {
        }
        //-------------------------------------------------------------------------
        protected override void OnStateEnd()
        {
        }
        //-------------------------------------------------------------------------
        protected override void OnSubStateChanged(object[] command)
        {
        }
    }
}
