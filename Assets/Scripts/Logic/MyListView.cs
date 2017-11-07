using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

[System.Serializable]
public abstract class MyListView :MonoBehaviour
{
    /// <summary>
    /// 当前Index
    /// </summary>
    public int Index { get; set; }
    /// <summary>
    /// 最大index
    /// </summary>
    public int MaxIndex { get; set; }
    /// <summary>
    /// 是否缓存页面
    /// </summary>
    public bool isCacheIterms = false;
    /// <summary>
    /// 列表项itermState
    /// </summary>
    public string iterm;
    /// <summary>
    /// 一次显示最大个数  默认为10
    /// </summary>
    public int MaxCount = 10;
    /// <summary>
    /// iterm高度
    /// </summary>
    public int Height;
    /// <summary>
    /// 列表uipanel对象
    /// </summary>
    /// <summary>
    /// item集合
    /// </summary>
    public List<GameObject> ItermsObj { get; set; }
    /// <summary>
    /// 当前源
    /// </summary>
    public IList Source { get; set; }
    /// <summary>
    /// 初始化列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Grid"></param>
    /// <param name="list"></param>
    /// <param name="name"></param>
    public abstract void InitContect<T>(TableViewSource tableViewSource, T t);
    /// <summary>
    /// 初始化函数
    /// </summary>
    public abstract void Init();
    /// <summary>
    /// 清理函数
    /// </summary>
    public abstract void Clear(TableViewSource tableViewSource);
    /// <summary>
    /// 页面切换
    /// </summary>
    /// <param name="index"></param>
    public abstract void PageChange(int newIndex);
    /// <summary>
    /// 列表初始化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    public void Init<T>(List<T> list, int index = 0)
    {
        this.Source = list;
        this.ItermsObj = new List<GameObject>();
        StartCoroutine(Delay(list, index));
        this.Index = 0;
        this.MaxIndex = (int)Math.Ceiling((list.Count * 1.0f) / (this.MaxCount * 1.0f));
    }

    public void UpdateList<T>(List<T> list)
    {
        this.Source = list;
        int max = this.ItermsObj.Count;
        for (int i = 0; i < max; i++)
        {
            if (this.ItermsObj[i].activeInHierarchy == true)
            {
                TableViewSource tableViewSource = this.ItermsObj[i].GetComponent<TableViewSource>();
                int index = tableViewSource.Index;
                this.InitContect(tableViewSource, list[index]);
            }
        }
    }

    /// <summary>
    /// 延迟3帧
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    IEnumerator Delay<T>(List<T> list, int index)
    {
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(this.BeginInit(list, index));
    }
    /// <summary>
    /// 开始初始化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="pageNum"></param>
    /// <returns></returns>
    IEnumerator BeginInit<T>(List<T> list, int pageNum)
    {
        GameObject temp = null;

        //截取列表页面
        List<T> pageList = list.Skip(pageNum * this.MaxCount).Take(this.MaxCount).ToList();
        ///创建预制件模板
        //if (this.isCacheIterms && UIPageController.ItermDic.ContainsKey(this.iterm))
        //{
        //    ///使用缓存
        //}
        //else
        //{
        //    temp = Resources.Load(string.Format("Perfab/Items/{0}", this.iterm)) as GameObject;
        //}
        //yield return new WaitForFixedUpdate();

        //this.uiPanel = this.gameObject.GetComponent<UIPanel>();
        //float beginPos = this.gameObject.transform.localPosition. / 2.0f;
        ///float width = this.uiPanel.width;
        //this.GetMaxChangeValue(list);

        float beginPos = 0f;
        for (int i = 0; i < this.MaxCount; i++)
        {
            if (i >= pageList.Count)
            {
                break;
            }
            else
            {
                GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>(string.Format("{0}/{1}", "prefab", this .iterm )));
                this.ItermsObj.Add(obj);
                obj.transform.parent = this.gameObject.transform;
                obj.transform.localPosition = new Vector3(0, (beginPos - this.Height * (i + 0.5f)), 0);
                obj.transform.localScale = new Vector3(1, 1, 1);
                //obj.GetComponent<UIWidget>().width = (int)width;
                yield return new WaitForFixedUpdate();
                TableViewSource tableViewSource = obj.GetComponent<TableViewSource>();
                tableViewSource.Index = i;
                this.InitContect(tableViewSource, list[i]);
                yield return new WaitForFixedUpdate();
            }
        }
        temp = null;
        yield return new WaitForFixedUpdate();
        //this.ResetPosition();
        OnLoadFinshed();
    }

    /// <summary>
    /// 列表加载完成
    /// </summary>
    public virtual void OnLoadFinshed()
    {

    }





    /// <summary>
    /// 延迟解锁
    /// </summary>
    /// <returns></returns>
    IEnumerator DelayLock(float timne)
    {
        this.isStory = true;
        yield return new WaitForSeconds(timne);
        this.isStory = false;
    }

    /// <summary>
    /// 更新列表
    /// </summary>
    /// <param name="index"></param>
    void UpdateList(int index, bool isRight)
    {
        StartCoroutine(DelayUpdate(index, isRight));
    }
    IEnumerator DelayUpdate(int index, bool isRight)
    {
        int beginNum = this.MaxCount * index;
        int max = this.Source.Count - beginNum >= this.MaxCount ? this.MaxCount + beginNum : this.Source.Count;

        for (int i = beginNum, j = 0; i < this.MaxCount + beginNum; i++, j++)
        {
            if (i >= max)
            {
                this.ItermsObj[j].SetActive(false);
            }
            else
            {
                this.ItermsObj[j].SetActive(true);
            }
        }

        int itermMax = this.ItermsObj.Count;
        yield return new WaitForSeconds(0.2f);


        for (int i = beginNum, j = 0; i < this.MaxCount + beginNum; i++, j++)
        {
            if (i >= max)
            {

            }
            else
            {
                TableViewSource tableViewSource = this.ItermsObj[j].GetComponent<TableViewSource>();
                tableViewSource.Index = i;
                this.InitContect(tableViewSource, this.Source[i]);
                yield return new WaitForFixedUpdate();
            }
        }
    }


    private bool isPress { get; set; }
    private bool isStory = false;
    /// <summary>
    /// 按下状态
    /// </summary>
    //public override void Press(bool pressed)
    //{
    //    if (this.isStory)
    //    {
    //        return;
    //    }
    //    base.Press(pressed);
    //}
    ///// <summary>
    ///// OnDrag
    ///// </summary>
    ///// <param name="delta"></param>
    //public override void Drag(Vector2 delta)
    //{

    //    if (this.isStory)
    //    {
    //        return;
    //    }
    //    base.Drag(delta);
    //}
    ///// <summary>
    ///// 滚轮事件
    ///// </summary>
    ///// <param name="delta"></param>
    //public override void Scroll(float delta)
    //{

    //    if (this.isStory)
    //    {
    //        return;
    //    }
    //    base.Scroll(delta);
    //}

    /// <summary>
    /// 清理列表函数
    /// </summary>
    public void Clear()
    {
        if (ItermsObj == null)
        {
            return;
        }
        if (this.isCacheIterms)
        {
            //UIPageController.ItermGetBack(this.iterm);
            ItermsObj.ForEach((p) =>
            {
                this.Clear(p.GetComponent<TableViewSource>());
            });
            ItermsObj.Clear();
            ItermsObj = null;

        }
        else
        {
            ItermsObj.ForEach((p) =>
            {
                Destroy(p);
                p = null;
            });
            ItermsObj.Clear();
            ItermsObj = null;
        }
    }

}
