using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingManger : MonoBehaviour
{

    public RectTransform setting;//下拉箭头的动画
    public RectTransform Parent;//按键的父对象
    public Button changeImage;//换肤按钮
    public Button ruler;//规则按钮
    public Button dissolveRoom;//解散房间按钮
    public Button setBtn;//设置按钮
    public Button RelLoad;
    public static GameObject settingobj;
    //public static GameObject ruleOBJ;
    private void Start()
    {

        setting.GetComponent<Button>().onClick.AddListener(delegate
        {
            VoiceManger.Instance.BtnVoice();
            Parent.DOLocalMoveX(813, 1);
            setting.DOLocalRotate(new Vector3(0, 0, -90), 1);
            StartCoroutine(DestoryGameObj());

        });
        ruler.onClick.AddListener(delegate
        {
            VoiceManger.Instance.BtnVoice();
            if (settingobj == null)
            {
                settingobj = CloneOne("GameRoomInfo");
                settingobj.GetComponent<ShowRoomInfo>().ShowRulerInfo();
            }
        });

        dissolveRoom.onClick.AddListener(delegate
        {
            VoiceManger.Instance.BtnVoice();
            if (settingobj == null)
            {
                GameObject temp = CloneOne("DissolutionRoomPanel");
                temp.GetComponent<RuleLayer>().ShowContent();
                Button[] btnArr = temp.GetComponentsInChildren<Button>();
                btnArr[0].onClick.AddListener(delegate
                {
                    VoiceManger.Instance.BtnVoice();
                    com.guojin.mj.net.message.game.VoteDelStart VDS = new com.guojin.mj.net.message.game.VoteDelStart();
                    SocketMgr.GetInstance().Send(com.guojin.mj.net.Net.instance.write(VDS));
                    Destroy(temp);

                });
                btnArr[1].onClick.AddListener(delegate
                {
                    VoiceManger.Instance.BtnVoice();
                    Destroy(temp);

                });
                btnArr[2].onClick.AddListener(delegate
                {
                    VoiceManger.Instance.BtnVoice();
                    Destroy(temp);

                });
            }


        });
        changeImage.onClick.AddListener(delegate
        {
            VoiceManger.Instance.BtnVoice();
            if (settingobj == null)
            {
                settingobj = CloneOne("ChangeImagePanel");
            }


        });

        setBtn.onClick.AddListener(delegate
        {
            VoiceManger.Instance.BtnVoice();
            if (settingobj == null)
            {
                settingobj = CloneOne("SettingPanle");
                settingobj.GetComponent<GameSetPageEvent>().InformationSetting();
            }

        });

        if (GameObject.Find("StaticSource").GetComponent<PlaybackLayer>().inPlayback)
        {
            dissolveRoom.gameObject.SetActive(false);
        }

        RelLoad.onClick.AddListener(delegate
        {
            Method.OutMaJong(30);
        });
    }

    IEnumerator DestoryGameObj()
    {
        yield return new WaitForSeconds(0.7f);
        Method.SettingBTN.SetActive(true);
        Destroy(gameObject);
    }

    public GameObject CloneOne(string name)
    {
        GameObject temp = Instantiate(Resources.Load<GameObject>("MjPrefab/" + name));
        temp.transform.SetParent(GameObject.Find("ChoicePlayeWnd_Img").transform);
        temp.transform.localPosition = Vector3.zero;
        temp.transform.localScale = Vector3.one;
        return temp;
    }


}
