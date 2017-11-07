using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DNMainPanel : BasePanel {
    private void Start() {
        //transform.FindChild("CreateRoomButton").GetComponent<Button>().onClick.AddListener(() => 
        //{
        //    DNUIManager.Instance.PushPanel(UIPanelType.DNCreateRoomPanel);
        //});
        //transform.FindChild("PlayAgainButton").GetComponent<Button>().onClick.AddListener(() => {
        //    //DNUIManager.Instance.PushPanel(UIPanelType.DNLoadingPanel);
        //    DNLoadingPanel.LoadSceneName="BullFight";
        //    SceneManager.LoadScene("LoadingScene");
        //});
        //transform.FindChild("JoinRoomButton").GetComponent<Button>().onClick.AddListener(() => {
        //    DNUIManager.Instance.PushPanel(UIPanelType.DNJoinRoomPanel);
        //});
        //transform.FindChild("RuleButton").GetComponent<Button>().onClick.AddListener(() => {
        //    DNUIManager.Instance.PushPanel(UIPanelType.DNRulePanel);
        //});
        //transform.FindChild("BackButton").GetComponent<Button>().onClick.AddListener(() => {
        //    //DNUIManager.Instance.PushPanel(UIPanelType.DNRulePanel);
        //    SceneManager.LoadScene("GameHall");
        //});
    }
    public override void OnExit() {
        gameObject.SetActive(true);
    }
}
