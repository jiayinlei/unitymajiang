using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using System.Linq;

[System.Serializable]
public abstract class EventManager : MonoBehaviour
{

    /// <summary>
    /// 弹窗内容显示区域
    /// </summary>
    public GameObject PopupContext;

    /// <summary>
    /// 绑定源数据
    /// </summary>
    public List<GameObject> BindingSource;


    /// <summary>
    /// 界面模板
    /// </summary>
    public EventManager templateEventManager;

    /// <summary>
    /// 普通按钮点击事件
    /// </summary>
    public List<Data> EventList;

    /// <summary>
    /// checkbox事件
    /// </summary>
    public List<Data> EventListChenkBox;

    /// <summary>
    /// 垃圾gameObj
    /// </summary>
    public List<GameObject> RubbishObj { get; set; }

    /// <summary>
    /// 更具名字获取id
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    //public GameObject GetGameObjectByName(string name)
    //{
    //    GameObject obj = GameObject.Find(name) as GameObject;
    //    if (obj == null)
    //    {
    //        obj = GameObject.Find(string.Format("{0}(Clone)", name)) as GameObject;
    //    }
    //    if (obj == null)
    //    {
    //        return null;
    //    }
    //    this.RubbishObj.Add(obj);
    //    return obj;
    //}

    public static void CloseChild(GameObject obj)
    {
        //ListView.GetChild(obj).ForEach((p) => p.SetActive(false));
    }
    public static void SetLable(GameObject obj, string str)
    {
        try
        {
            Text uiLabel_0 = obj.GetComponent<Text>();
            if (string.IsNullOrEmpty(str))
            {
                uiLabel_0.text = string.Empty;
            }
            else
            {
                uiLabel_0.text = TooL.strGetNewline(str);
                
            }
            uiLabel_0 = null;
        }
        catch (System.Exception ex)
        {
            Debug.LogError(obj.name);
            throw ex;
        }

    }
    public static void SetSlider(GameObject obj, float num)
    {
        Slider uiSlider = obj.GetComponent<Slider>();
        uiSlider.value = num;
        uiSlider = null;
    }
    public static void SetSlider(GameObject obj, float num_0, float num_1)
    {
        Slider[] uiSlider = obj.GetComponents<Slider>();
        uiSlider[0].value = num_0;
        uiSlider[1].value = num_1;
        uiSlider = null;
    }
    /// <summary>
    /// (type==0,MJ)(type==1,ZJH)(type==2,PDK)
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="str"></param>
    /// <param name="type"></param>
    public static void SetImage(GameObject obj, string str,int imageType)
    {
        try
        {
            Image image = obj.GetComponent<Image>();
            string imagePath = TooL.GetImagePath(str, imageType);
            image.sprite = Resources.Load<Sprite>(imagePath);
            image = null;
        }
        catch (System.Exception)
        {
            Debug.LogError(obj.name + "->SetImage出错");
        }

    }
    public static void MakeImageSizeFit(GameObject obj)
    {
        try
        {
            Image image = obj.GetComponent<Image>();
            image.raycastTarget  = true ;
        }
        catch (System.Exception)
        {
            Debug.LogError(obj.name + "->MakeImageSizeFit出错");
        }

    }
    public static void SetActive(GameObject obj, bool isActive)
    {
        if (obj != null) {
            obj.SetActive(isActive);
        }
    }
    public static void SetBtnDisable(GameObject obj, bool isActive)
    {
        obj.GetComponent<Button>().enabled = isActive;
    }
    public static void SetBtnImg(GameObject obj, string str,int imageType)
    {
        Image image = obj.GetComponent<Image>();
        if (image == null)
        {
            Debug.LogError("image=>" + obj.name);
            return;
        }
        string imagePath = TooL.GetImagePath(str, imageType);
        image.sprite = Resources.Load<Sprite>(imagePath);
        image = null;

        Button button = obj.GetComponent<Button>();   
        if (button == null)
        {
            Debug.LogError("没有找到uiButton=>" + obj.name);
            return;
        } else 
        {
            if (button.transition == Selectable.Transition.SpriteSwap) 
            {
                button.transition = Selectable.Transition.SpriteSwap;
                SpriteState tmpSpriteState = new SpriteState();
                tmpSpriteState.highlightedSprite = Resources.Load<Sprite>(imagePath);
            }
        }
        button = null;
    }
    public static void SetColor(GameObject obj, Color color)
    {
        Image image = obj.GetComponent<Image>();
        if (image == null) {
            Debug.LogError("image=>" + obj.name);
            return;
        }
        obj.GetComponent<Image>().color = color;
    }
    public static void SetColor(GameObject obj, float r, float g, float b, float a)
    {
        Image image = obj.GetComponent<Image>();
        if (image == null) {
            Debug.LogError("image=>" + obj.name);
            return;
        }
        obj.GetComponent<Image>().color = new Color(r/255f, g/255f, b/255f, a/255f);
    }
    public static void SetGrey(GameObject obj)
    {
        Image image = obj.GetComponent<Image>();
        if (image == null) {
            Debug.LogError("image=>" + obj.name);
            return;
        }
        image.color = new Color(0, 255, 255, 255);
    }
    public static void CancelGrey(GameObject obj)
    {
        Image image = obj.GetComponent<Image>();
        if (image == null) {
            Debug.LogError("image=>" + obj.name);
            return;
        }
        image.color = new Color(255, 255, 255, 255);
    }
    public static void SetWhite(GameObject obj)
    {
        CancelGrey(obj);
    }

    public static void SetToggleValue(GameObject obj, bool value)
    {
        Toggle toggle = obj.GetComponent<Toggle>();
        if (toggle == null)
        {
            Debug.LogError("找不到组件");
        }
        toggle.isOn = value;
    }
    public static void SetBtnClick(GameObject obj, string MethodName, MonoBehaviour target)
    {
        Button button = obj.GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("UIButton按钮事件绑定出错");
            return;
        }
        button.onClick.RemoveAllListeners();

        button.onClick.AddListener(delegate () {
            target.Invoke(MethodName,0);
        });
        button = null;
    }


    /// <summary>
    /// 初始化函数
    /// </summary>

    ///不使用
    public void Start()
    {

    }
    /// <summary>
    /// 不适用
    /// </summary>
    public void OnDestroy()
    {

    }

    /// <summary>
    /// 开启
    /// </summary>
    public void Open()
    {
        #region 清理绑定事件

        this.EventList.ForEach((p) =>
        {
            Button btn = p.gameObject.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.RemoveAllListeners();
            }
            btn = null;
        });
        this.EventListChenkBox.ForEach((p) =>
        {
            Toggle uIToggle = p.gameObject.GetComponent<Toggle>();
            if (uIToggle != null)
            {
                uIToggle.onValueChanged.RemoveAllListeners();
            }
            uIToggle = null;
        });
        #endregion

       
        //绑定事件
        this.EventList.ForEach((p) =>
        {
            try
            {
                Button btn = p.gameObject.GetComponent<Button>();
                btn.onClick.AddListener(delegate() {
                    this.Invoke(p.Target,0);
                });
                //按钮声音未处理
                btn = null;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

        });
        this.EventListChenkBox.ForEach((p) =>
        {
            Toggle uIToggle = p.gameObject.GetComponent<Toggle>();
            //uIToggle.onValueChanged.AddListener(new EventDelegate(this, p.Target));
            uIToggle = null;
            //p.gameObject.AddComponent<UIPlaySound>().audioClip = Resources.Load(string.Format("Sound/{0}", this.CheckBoxName)) as AudioClip;
        });
        
    }

    public void ClearEvent()
    {
        #region 清理绑定事件

        this.EventList.ForEach((p) =>
        {
            Button btn = p.gameObject.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.RemoveAllListeners();
            }
            //UIPlaySound uiPlaySound = p.gameObject.GetComponent<UIPlaySound>();
            //if (uiPlaySound != null)
            //{
            //    Destroy(uiPlaySound);
            //}
            btn = null;
        });
        //this.EventListUITweener.ForEach((p) =>
        //{
        //    UITweener uITweener = p.gameObject.GetComponent<UITweener>();
        //    if (uITweener != null)
        //    {
        //        if (uITweener.onFinished != null)
        //        {
        //            uITweener.onFinished.Clear();
        //        }
        //        if (uITweener.mTemp != null)
        //        {
        //            uITweener.mTemp.Clear();
        //        }
        //    }
        //    uITweener = null;
        //});
        this.EventListChenkBox.ForEach((p) =>
        {
            Toggle uIToggle = p.gameObject.GetComponent<Toggle>();
            if (uIToggle != null)
            {
                uIToggle.onValueChanged.RemoveAllListeners();
            }
            uIToggle = null;
            //UIPlaySound uiPlaySound = p.gameObject.GetComponent<UIPlaySound>();
            //if (uiPlaySound != null)
            //{
            //    Destroy(uiPlaySound);
            //}
        });
        #endregion
    }
    public abstract void InformationSetting();

    [System.Serializable]
    public class Data
    {
        public GameObject gameObject;
        public string Target;
    }
}
