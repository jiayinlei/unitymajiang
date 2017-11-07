using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingPanel : BasePanel {

    //public RectTransform setting;//下拉箭头的动画
    ////public RectTransform Parent;//按键的父对象
    //public Button changeImage;//换肤按钮
    //public Button ruler;//规则按钮
    //public Button dissolveRoom;//解散房间按钮
    //public Transform closeButton;
    //public static GameObject settingobj;
    //public static GameObject ruleOBJ;
    private void Start() {

        transform.FindChild("Parent").DOLocalMoveX(500, 0.3f);
        transform.GetComponent<Image>().DOFade(1, 0.5f);
        //closeButton = transform.FindChild("Parent/CloseButton").transform;
        //closeButton.DORotate(new Vector3(0, 0, 359), 0.3f);
        transform.FindChild("Parent/CloseButton").GetComponent<Button>().onClick.AddListener(() => {
            transform.PlayButtonVoice();
            Tweener tw=transform.FindChild("Parent").DOLocalMoveX(800, 0.3f);
            transform.GetComponent<Image>().DOFade(0, 0.3f);
            //closeButton.DORotate(new Vector3(0, 0, 0), 0.3f);
            tw.OnComplete(()=> {
                DNUIManager.Instance.PopPanel();
            });
        });

        transform.FindChild("CloseMask").GetComponent<Button>().onClick.AddListener(() => {
            transform.PlayButtonVoice();
            Tweener tw = transform.FindChild("Parent").DOLocalMoveX(800, 0.3f);
            transform.GetComponent<Image>().DOFade(0, 0.3f);
           // closeButton.DORotate(new Vector3(0, 0, -359), 0.3f);
            tw.OnComplete(() => {
                DNUIManager.Instance.PopPanel();
            });

        });
        transform.FindChild("Parent/SetButton").GetComponent<Button>().onClick.AddListener(() => {
            transform.PlayButtonVoice();
            //DNUIManager.Instance.PushPanel(UIPanelType.AudioSettingPanel);

            string path;
            path = "PanelPrefabs/" + "MainSetting";
            //Debug.Log(path);
            //panelPathDict.TryGetValue(panelType, out path);
            GameObject instPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject;
            instPanel.transform.SetParent(transform, false);
            instPanel.transform.localScale = Vector3.one;
            instPanel.name = instPanel.name.Replace("(Clone)", "");

        });
        //exitButton = transform.FindChild("GameButtons/ExitButton").GetComponent<Button>();
        transform.FindChild("Parent/dissolveRoomBTN").GetComponent<Button>().onClick.AddListener(() => {
            transform.PlayButtonVoice();
            DNUIManager.Instance.PushPanel(UIPanelType.DissolvePanel);
        });
        transform.FindChild("Parent/ChangeButton").GetComponent<Button>().onClick.AddListener(() => {
            transform.PlayButtonVoice();
            DNUIManager.Instance.PushPanel(UIPanelType.ChangeClothPanel);

        });
        transform.FindChild("Parent/RuleButton").GetComponent<Button>().onClick.AddListener(() => {
            transform.PlayButtonVoice();
            DNUIManager.Instance.PushPanel(UIPanelType.GameRulePanel);

        });
    }
    private void OnEnable() {
        transform.FindChild("Parent").DOLocalMoveX(500, 0.3f);
        transform.GetComponent<Image>().DOFade(1,0.5f);
        //closeButton.DOLocalRotate(new Vector3(0, 0, 359), 0.3f);
    }

}
