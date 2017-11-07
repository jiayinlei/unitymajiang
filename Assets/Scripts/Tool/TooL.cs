using UnityEngine;
using System.Collections;
using System;

public class TooL {

    private static TooL instance = null;
    public static TooL Instance {
        get {
            if (instance == null) {
                instance = new TooL();
            }
            return instance;
        }
    }

    // 显示一个对话框
    public static void showTips(string content, TipsDialogCallBack okCB, TipsDialogCallBack cancelCB = null,string sureStr = "确定",string cancelStr ="取消") {
        GameObject root = GameObject.Find("addNode");
        if (root) {
            GameObject tips = TooL.loadPrefab(root, "TipsDialog");
            TipsDialog script = tips.GetComponent<TipsDialog>();
            if (script) {
                script.SetContent(content);
                script.SetOkCallBack(okCB);
                if (cancelCB!=null)
                {
                    script.SetCancleCallBack(cancelCB);
                }
                script.SetBtnContent(sureStr, cancelStr);
            }
        } else {
            Debug.Log("[Tool] 没有发现Canvas节点 ...");
        }
    }
    public static void showNetErroTips(string content, TipsDialogCallBack okCB)
    {
        GameObject root = GameObject.Find("addNode");
        if (root)
        {
            GameObject tips = TooL.loadPrefab(root, "NetErroTipsDialog");
            TipsDialog script = tips.GetComponent<TipsDialog>();
            if (script)
            {
                script.SetContent(content);
                script.SetOkCallBack(okCB);
            }
        }
        else
        {
            Debug.Log("[Tool] 没有发现Canvas节点 ...");
        }
    }
    public static void VersionCheckTips(string content, VersionCheckDialogCallBack okCB)
    {
        GameObject root = GameObject.Find("addNode");
        if (root)
        {
            GameObject tips = TooL.loadPrefab(root, "VersionCheckDialog");
            VersionCheckDialog script = tips.GetComponent<VersionCheckDialog>();
            if (script)
            {
                script.SetContent(content);
                script.SetOkCallBack(okCB);
            }
        }
        else
        {
            Debug.Log("[Tool] 没有发现Canvas节点 ...");
        }
    }
    /// Resources加载, prefab 必须在Resource文件夹下
    public static GameObject loadPrefab(GameObject parentNode, string prefabUrl) {
        GameObject clone = GameObject.Instantiate(Resources.Load("Prefab/" + prefabUrl) as GameObject) as GameObject;
        clone.transform.SetParent(parentNode.transform,false);
        clone.name = prefabUrl;
        clone.transform.localPosition = new Vector3(0, 0, 0);
        clone.transform.localRotation = parentNode.transform.rotation;
        clone.transform.localScale = Vector3.one;
        return clone;
    }
    public static GameObject clone(GameObject prefab, GameObject parentNode) {
        GameObject clone = GameObject.Instantiate(prefab) as GameObject;
        clone.name = prefab.name;
        clone.transform.SetParent(parentNode.transform);
        clone.transform.localPosition = new Vector3(0, 0, 0);
        clone.transform.localRotation = parentNode.transform.rotation;
        clone.transform.localScale = parentNode.transform.localScale;
        return clone;
    }
    public static void destroyAllChildren(GameObject node) {
        if (node!=null)
        {
            for (int i = 0; i < node.transform.childCount; i++)
            {
                GameObject.Destroy(node.transform.GetChild(i).gameObject);
            }
        }
       
    }


    /// Resources加载
    public void cloning(GameObject GO, string name, Vector3 ves) {
        GameObject clone = GameObject.Instantiate(Resources.Load("Prefab/" + name) as GameObject) as GameObject;
        clone.transform.SetParent(GO.transform);
        clone.name = name;
        clone.transform.localPosition = ves;
        clone.transform.localRotation = GO.transform.rotation;
        clone.transform.localScale = GO.transform.localScale;
        return;
    }

    public void cloning(string name, Vector3 ves) {
        GameObject GO = GameObject.Find("addNode");
        GameObject clone = GameObject.Instantiate(Resources.Load("Prefab/" + name) as GameObject) as GameObject;
        clone.transform.SetParent(GO.transform);
        clone.name = name;
        clone.transform.localPosition = ves;
        clone.transform.localRotation = GO.transform.rotation;
        clone.transform.localScale = GO.transform.localScale;
        return;
    }

    public void cloning(string name) {
        GameObject GO = GameObject.Find("addNode");
        GameObject clone = GameObject.Instantiate(Resources.Load(name) as GameObject) as GameObject;
       clone.transform.SetParent(GO.transform);
        clone.name = name;
        clone.transform.localPosition = new Vector3(0, 0, 0);
        clone.transform.localRotation = GO.transform.rotation;
        clone.transform.localScale = GO.transform.localScale;
        return;
    }
    public static string strGetNewline(string str)
    {
        return str.Replace(@"\\n", "\n");
    }
    public static string GetImagePath(string imageName, int imageType)
    {
        string imagePath = "";
        switch (imageType)
        {
            case 0:
                imagePath = string.Format("MJSprite/{0}", imageName);
                return imagePath;
            case 1:
                imagePath = string.Format("ZJHSprite/{0}", imageName);
                return imagePath;
            case 2:
                imagePath = string.Format("PDKSprite/{0}", imageName);
                return imagePath;
            default:
                throw new Exception("Image 类型出错");
        }
    }
    /// <summary>
    /// 文字颜色
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static string NameWithColor(Color color)
    {
        switch (color)
        {
            case Color.GRAY:
                return "[999999]{0}[-]";
            case Color.WHITE:
                return "[ffffff]{0}[-]";
            case Color.BLUE:
                return "[00ccff]{0}[-]";
            case Color.PURPLE:
                return "[cc00cc]{0}[-]";
            case Color.Yellow:
                return "[ffff00]{0}[-]";
            default:
                throw new Exception("文字颜色出错");
        }
    }
    public enum Color
    {
        None = 0,
        GRAY,
        WHITE,
        BLUE,
        PURPLE,
        Yellow,
        Max
    }
    public static void ClearMemory()
    {
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }
    //---聊天功能未开发，屏蔽字暂不处理，需求未提出,屏蔽字库未提供---------------------------
    public static bool IsMaskWord(ref string strValue)
    {
        return true ;
    }
    /// <summary>
    /// 功能开关未开发
    /// </summary>
    /// <returns><c>true</c>, if use condition check was moudled, <c>false</c> otherwise.</returns>
    public static bool MoudleUseConditionCheck(int iMoudleType)
    { 
        return true;
    }

}

