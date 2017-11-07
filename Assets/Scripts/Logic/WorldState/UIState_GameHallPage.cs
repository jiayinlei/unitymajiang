using Assets.Scripts.BullFight.View;
using com.guojin.mj.net.message.login;
using GameCore;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameCore
{
    public class UIState_GameHallPage : EntityState
    {
        public static bool doNothing = true;
        public static ComeFromState comeFromState = ComeFromState.Login;

        public enum ComeFromState
        {
            Null,
            Login,
            FromMjFighting,//麻将战斗
            FromGameMjBack,//麻将回放
            FromH5,        //H5跳转房间
            FromDNFighting,
            FromDNPlayBack,
            Max,

        }
        public UIState_GameHallPage(int iStateId, EntityState eState) : base(iStateId, eState) { }
        protected override void OnStateBegin(object[] command)
        {
            if (doNothing)
            {
                if (comeFromState==ComeFromState.FromH5 || Method.isChongLian)
                {
                    UIManager.ChangeUI(UIManager.PageState.TempPage, (GameObject obj) =>
                    {
                        obj.GetComponent<TempPageEvent>().InformationSetting();
                    });
                }
                if (!GameData.GetInstance().playerData.longitude.Equals(""))
                {
                    //发送第一次登录后的经纬度
                    AckLocation ack = AckLocation.Creat(GameData.GetInstance().playerData.longitude, GameData.GetInstance().playerData.latitude);
                    SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(ack));
                }
                doNothing = false;

            }
            switch (comeFromState)
            {
                case ComeFromState.Null:
                    break;
                case ComeFromState.Login:
                    if (Method.isChongLian)
                    {
                        com.guojin.mj.net.message.login.JoinRoom jr = com.guojin.mj.net.message.login.JoinRoom.create(Method.GameRoomID);
                        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(jr));
                    }
                    else
                    {
                        int _int = PlayerPrefs.GetInt("isGameHall", 1);
                        if (_int == 1)
                        {
                            UIManager.ChangeUI(UIManager.PageState.GameHall, (GameObject obj) =>
                            {
                                obj.GetComponent<GameHallPageEvent>().InformationSetting();
                            });
                        }
                        else
                        {
                            if (!SceneManager.GetActiveScene().name.Equals("Horizontal"))
                            {
                                UIManager.ChangeUI(UIManager.PageState.MJCreatRoomPage, (GameObject obj) =>
                                {
                                    obj.GetComponent<MJCreatRoomPageEvent>().InformationSetting();

                                });
                            }
                        }
                    }
                    break;
                case ComeFromState.FromMjFighting:
                    UIManager.ChangeUI(UIManager.PageState.MJCreatRoomPage, (GameObject obj) =>
                    {
                        obj.GetComponent<MJCreatRoomPageEvent>().InformationSetting();
                    });
                    break;
                case ComeFromState.FromGameMjBack:
                    UIManager.ChangeUI(UIManager.PageState.MJCreatRoomPage, (GameObject obj) =>
                    {
                        obj.GetComponent<MJCreatRoomPageEvent>().InformationSetting();
                    });
                    break;
                case ComeFromState.FromH5:
                    if (Method.GameRoomID != null)
                    {
                        LoginAndShare.Controller.ShowAndroidMsg("您未退出之前的房间，无法进入新的房间！");
                        com.guojin.mj.net.message.login.JoinRoom jr = com.guojin.mj.net.message.login.JoinRoom.create(Method.GameRoomID);
                        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(jr));
                    }
                    else
                    {
                        com.guojin.mj.net.message.login.JoinRoom jr = com.guojin.mj.net.message.login.JoinRoom.create(LoginAndShare.Controller.h5RoomId);
                        SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(jr));
                    }
                    break;
                case ComeFromState.FromDNFighting:
                    Screen.orientation = ScreenOrientation.Portrait;
                    UIManager.ChangeUI(UIManager.PageState.DNMainPanelPage, (GameObject obj) => {
                        obj.GetComponent<DNCreatRoomPage>().InformationSetting();
                    });
                    break;
                case ComeFromState.FromDNPlayBack:
                    Screen.orientation = ScreenOrientation.Portrait;
                    UIManager.ChangeUI(UIManager.PageState.DNMainPanelPage, (GameObject obj) => {
                        obj.GetComponent<DNCreatRoomPage>().InformationSetting();
                    });
                    UIManager.ChangeUI(UIManager.PageState.DNReferPanel, (GameObject obj) => {
                    });
                    break;
                case ComeFromState.Max:
                    break;
                default:
                    break;
            }
            comeFromState = ComeFromState.Null;
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
