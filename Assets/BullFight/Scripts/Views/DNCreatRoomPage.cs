using com.guojin.dn.net.message;
using com.guojin.mj.net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
/* ***********************************************
 * Describe:该类功能描述
 * Author :  MuLongFei
 * Email: 424113771@qq.com
 * DATA: 2017/7/28 11:21:14 
 * FileName: DNCreatRoomPage 
 * Version: V1.0.1
 * ***********************************************/

namespace Assets.Scripts.BullFight.View {
   public class DNCreatRoomPage : EventManager {
        private Text playerName, playerID, roomCard;
        private Button creatRoomBtn, continuGameBtn, joinRoomBtn,addRoomCardBtn,ruleBtn,backBtn,headBtn;
        private RawImage headImage;

        private void Awake() {

        }
         void Start() {
            //if (DNGlobalData.isChongLian) {
            //    Destroy(gameObject);
            //    UIManager.ChangeUI(UIManager.PageState.DNWaitPanel, (GameObject obj1) => {

            //    });
            //}
        }
        private void InitUI() {

            playerName = transform.FindChild("PlayerInfo/NameText").GetComponent<Text>();
            playerID = transform.FindChild("PlayerInfo/IDText").GetComponent<Text>();
            roomCard = transform.FindChild("PlayerInfo/RoomCards/RoomCardsText").GetComponent<Text>();
            headImage = transform.FindChild("PlayerInfo/headImageMask/HeadImage").GetComponent<RawImage>();

            //creatRoomBtn = transform.FindChild("CreateRoomButton").GetComponent<Button>();
            ////continuGameBtn = transform.FindChild("BtnList/PlayAgainBtn").GetComponent<Button>();
            //joinRoomBtn = transform.FindChild("JoinRoomButton").GetComponent<Button>();
            //addRoomCardBtn = transform.FindChild("PlayerInfo/RoomCards/AddRoomCardBtn").GetComponent<Button>();
            //ruleBtn = transform.FindChild("RuleButton").GetComponent<Button>();
            //backBtn = transform.FindChild("BackButton").GetComponent<Button>();
            //headBtn = transform.FindChild("HeadImage").GetComponent<Button>();

            //creatRoomBtn.onClick.AddListener(OnCreatRoomButtonClicked);
            ////continuGameBtn.onClick.AddListener(OnContinuGameBtnClicked);
            //joinRoomBtn.onClick.AddListener(OnJoinRoomBtn);
            //addRoomCardBtn.onClick.AddListener(OnAddRoomCardBtn);
            //ruleBtn.onClick.AddListener(OnRuleBtn);
            //backBtn.onClick.AddListener(OnBackBtn);
            //headBtn.onClick.AddListener(OnHeadBtn);

            InitPlayerInfo();

        }

        private void InitPlayerInfo() {
            playerName.text = GameData.GetInstance().playerData.name;
            playerID.text = GameData.GetInstance().playerData.id.ToString();
            roomCard.text = GameData.GetInstance().playerData.gold.ToString();

            if (GameHallPageEvent.texture2D != null) {
                headImage.texture = GameHallPageEvent.texture2D;
                headImage.gameObject.SetActive(true);
            } else {
                string url = GameData.GetInstance().playerData.avatar;

                if (url != null) {
                    url = url.Substring(0, url.Length - 1);
                    url += "64";
                    if (url != null) {
                        StartCoroutine(DownloadImage(url));
                    }
                }
            }
        }

        private IEnumerator DownloadImage(string url) {
            WWW www = new WWW(url);
            yield return www;
            headImage.texture = www.texture;
        }


        private void OnHeadBtn() {
            transform.PlayButtonVoice();
            UIManager.ChangeUI(UIManager.PageState.PlayerInforPopPage, (GameObject obj) => {
                obj.GetComponent<PlayerInforPopPageEvent>().InformationSetting();
            });
        }

        private void OnSetButton() {

            transform.PlayButtonVoice();
            //Instantiate(Resources.Load<GameObject>("BullFightPrefab/AudioSettingPanel"));
            GameObject temp = Instantiate(Resources.Load<GameObject>("PanelPrefabs/MainSetting"));
            temp.transform.SetParent(gameObject.transform);
            temp.transform.localPosition = Vector3.zero;
            temp.transform.localScale = Vector3.one;
        }
        private void OnBackBtn() {

            transform.PlayButtonVoice();
            Destroy(this.gameObject);
            UIManager.ChangeUI(UIManager.PageState.GameHall, (GameObject obj) =>
            {
                obj.GetComponent<GameHallPageEvent>().InformationSetting();
            });
        }
       public void OnReferButton() {
            transform.PlayButtonVoice();
            UIManager.ChangeUI(UIManager.PageState.DNReferPanel, (GameObject obj1) => {

            });
            //GameObject temp = Instantiate(Resources.Load<GameObject>("BullFightPrefab/ReferPanel"));
            //temp.transform.SetParent(gameObject.transform);
            //temp.transform.localPosition = Vector3.zero;
            //temp.transform.localScale = Vector3.one;
        }

        private void OnRuleBtn() {
            transform.PlayButtonVoice();
            DNGlobalData.noticeText = "暂未开放，敬请期待";
            GameObject temp = Instantiate(Resources.Load<GameObject>("BullFightPrefab/GameHallNoticePanel"));
            temp.transform.SetParent(gameObject.transform);
            temp.transform.localPosition = Vector3.zero;
            temp.transform.localScale = Vector3.one;
        }

        private void OnAddRoomCardBtn() {
            transform.PlayButtonVoice();
            UIManager.ChangeUI(UIManager.PageState.BuyRoomCardPage, (GameObject obj) =>
            {
                obj.GetComponent<BuyRoomCardPageEvent>().InformationSetting();
            });
        }

        private void OnJoinRoomBtn() {
            transform.PlayButtonVoice();
            GameObject temp = Instantiate(Resources.Load<GameObject>("BullFightPrefab/DNJoinRoomPanl"));
            temp.transform.SetParent(gameObject.transform);
            temp.transform.localPosition = Vector3.zero;
            temp.transform.localScale = Vector3.one;
        }

        private void OnShare() {
            transform.PlayButtonVoice();
            //LoginAndShare.addShare = true;
            //LoginAndShare.Controller.OnClickSharedDN(1);
            //TooL.showTips   
            //TODO: 张航 分享     
        }

        private void OnCreatRoomButtonClicked() {
            transform.PlayButtonVoice();
            GameObject temp = Instantiate(Resources.Load<GameObject>("BullFightPrefab/DNCreatRoomPanl"));
            temp.transform.SetParent(gameObject.transform);
            temp.transform.localPosition = Vector3.zero;
            temp.transform.localScale = Vector3.one;
        }

        public override void InformationSetting() {
            MainLogic.Controller.gameType = GameType.DN;
            InitUI();
            
        }
    }
}
