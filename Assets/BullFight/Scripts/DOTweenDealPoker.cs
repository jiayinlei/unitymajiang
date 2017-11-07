using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class DOTweenDealPoker : MonoBehaviour {
    static List<GameObject> pokerList = new List<GameObject>();
    GameObject poker;
    GameObject trans;
    Transform parent;
   static List<int> nextList = new List<int>();
   static List<int> handPoker = new List<int>();
    static int index;
    static int pokerCount;
    float doTweenDuration = 0.2f;
    float waitTime = 0.1f;
    int spacing = 32;
    int playerSpacing = 97;
    int pokerNum = 5;
    static int max;

    public static bool out11;
    public static bool out12;
    public static bool out21;
    public static bool out22;
    public static bool out31;
    public static bool out32;
    public static bool out41;
    public static bool out42;
    public static bool out51;
    public static bool out52;

    public static bool out13;
    public static bool out23;
    public static bool out33;
    public static bool out43;
    public static bool out53;

    void Start() {
        max = 50;
        pokerCount = 4;
        index = 0;
        parent = GameObject.Find("Canvas/addNode").transform;
        poker = Resources.Load<GameObject>("BullFightPrefab/PokerModel");
        for (int i = 0; i < max; i++) {
            trans = Instantiate(poker);
            trans.transform.SetParent(parent);
            trans.transform.localScale = new Vector3(0.72f, 0.72f);
            trans.transform.localPosition = new Vector3(0, 72, 0);
            trans.name = trans.name.Replace("(Clone)", "");
            trans.GetComponent<Image>().enabled=false;
            pokerList.Add(trans);
        }
        List<GameObject> game = new List<GameObject>();
        foreach (var a in pokerList) {
            if (a != null) {
                game.Add(a);
            }
            //print(a);
        }
        pokerList = game;
        int addCount = 0;
        for (int i = 0; ; i++) {
            if (i < pokerCount) {
                nextList.Add(i);
            } else {
                i = -1;
            }
            if (addCount > max) {
                break;
            }
            addCount++;
        }
        //out13 = true;
        //out23 = true;
        //out33 = true;
        //out43 = true;
        //out53 = true;
        //out11 = true;
        //out12 = true;
        //out21 = true;
        //out22 = true;
        //out31 = true;
        //out32 = true;
        //out41 = true;
        //out42 = true;
        //out51 = true;
        //out52 = true;
    }
    public static void EndDOTween() {
        foreach (var a in pokerList) {
            a.GetComponent<Image>().enabled = false;
            a.transform.localPosition = new Vector3(0, 72, 0);
            a.transform.localScale = new Vector3(0.72f, 0.72f);
        }
        index = 0;
    }
    public static void SetNextList(int pokerCount) {
        nextList.Clear();
        if (DOTweenDealPoker.pokerCount == 4 && pokerCount == 5) {
            index++;
        }
        DOTweenDealPoker.pokerCount = pokerCount;
        int addCount = 0;
        for (int i = 0; ; i++) {
            if (i < pokerCount) {
                nextList.Add(i);
            } else {
                i = -1;
            }
            if (addCount > max) {
                break;
            }
            addCount++;
        }
    }

    private void Update() {
        if (out11) {
            OutPoker11(pokerCount);
            out11 = false;
        }
        if (out12) {
            OutPoker12(pokerCount);
            out12 = false;
        }
        if (out21) {
            OutPoker21(pokerCount);
            out21 = false;
        }
        if (out22) {
            OutPoker22(pokerCount);
            out22 = false;
        }
        if (out31) {
            OutPoker31(pokerCount);
            out31 = false;
        }
        if (out32) {
            OutPoker32(pokerCount);
            out32 = false;
        }
        if (out41) {
            OutPoker41(pokerCount);
            out41 = false;
        }
        if (out42) {
            OutPoker42(pokerCount);
            out42 = false;
        }
        if (out51) {
            OutPoker51(pokerCount);
            out51 = false;
        }
        if (out52) {
            OutPoker52(pokerCount);
            out52 = false;
        }
        if (out13) {
            OutPoker13(pokerCount);
            out13 = false;
        }
        if (out23) {
            OutPoker23(pokerCount);
            out23 = false;
        }
        if (out33) {
            OutPoker33(pokerCount);
            out33 = false;
        }
        if (out43) {
            OutPoker43(pokerCount);
            out43 = false;
        }
        if (out53) {
            OutPoker53(pokerCount);
            out53 = false;
        }
    }
    void OutPoker11(int num) {
        for (int i = 0; i < num; i++) {
            //print(pokerList[index]);
            pokerList[index].GetComponent<Image>().enabled = true;
            StartCoroutine(DOTween11(index));
            index++;
        }
    }
    void OutPoker12(int num) {
        for (int i = 0; i < num; i++) {
            pokerList[index].GetComponent<Image>().enabled = true;
            StartCoroutine(DOTween12(index));
            index++;
        }
    }
    void OutPoker21( int num) {
        for (int i = 0; i < num; i++) {
            pokerList[index].GetComponent<Image>().enabled = true;
            StartCoroutine(DOTween21(index));
            index++;
        }
    }
    void OutPoker22( int num) {
        for (int i = 0; i < num; i++) {
            pokerList[index].GetComponent<Image>().enabled = true;
            StartCoroutine(DOTween22(index));
            index++;
        }
    }
    void OutPoker31(int num) {
        for (int i = 0; i < num; i++) {
            pokerList[index].GetComponent<Image>().enabled = true;
            StartCoroutine(DOTween31(index));
            index++;
        }
    }
    void OutPoker32( int num) {
        for (int i = 0; i < num; i++) {
            pokerList[index].GetComponent<Image>().enabled = true;
            StartCoroutine(DOTween32(index));
            index++;
        }
    }
    void OutPoker41( int num) {
        for (int i = 0; i < num; i++) {
            pokerList[index].GetComponent<Image>().enabled = true;
            StartCoroutine(DOTween41(index));
            index++;
        }
    }
    void OutPoker42(int num) {
        for (int i = 0; i < num; i++) {
            pokerList[index].GetComponent<Image>().enabled = true;
            StartCoroutine(DOTween42(index));
            index++;
        }
    }
    void OutPoker51(int num) {
        for (int i = 0; i < num; i++) {
        pokerList[index].GetComponent<Image>().enabled = true;
            StartCoroutine(DOTween51(index));
            index++;
        }
    }
    void OutPoker52( int num) {
        for (int i = 0; i < num; i++) {
        pokerList[index].GetComponent<Image>().enabled = true;
            StartCoroutine(DOTween52(index));
            index++;
        }
    }
    void OutPoker13(int num) {
        for (int i = 0; i < num; i++) {
            pokerList[index].GetComponent<Image>().enabled = true;
            StartCoroutine(DOTween13(index));
            index++;
        }
    }
    void OutPoker23(int num) {
        for (int i = 0; i < num; i++) {
            pokerList[index].GetComponent<Image>().enabled = true;
            StartCoroutine(DOTween23(index));
            index++;
        }
    }
    void OutPoker33(int num) {
        for (int i = 0; i < num; i++) {
            pokerList[index].GetComponent<Image>().enabled = true;
            StartCoroutine(DOTween33(index));
            index++;
        }
    }
    void OutPoker43(int num) {
        for (int i = 0; i < num; i++) {
            pokerList[index].GetComponent<Image>().enabled = true;
            StartCoroutine(DOTween43(index));
            index++;
        }
    }
    void OutPoker53(int num) {
        for (int i = 0; i < num; i++) {
            pokerList[index].GetComponent<Image>().enabled = true;
            StartCoroutine(DOTween53(index));
            index++;
        }
    }

    IEnumerator DOTween11(int index) {
        yield return new WaitForSeconds(waitTime * nextList[index]);
        pokerList[index].transform.DOLocalMove(new Vector3(-375 + nextList[index] * playerSpacing, -228, 0), doTweenDuration, true);
        pokerList[index].transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f),doTweenDuration);
    }
    IEnumerator DOTween12(int index) {
        yield return new WaitForSeconds(waitTime * nextList[index]);
        //pokerList[index].gameObject.SetActive(true);
        pokerList[index].transform.DOLocalMove(new Vector3(85 + nextList[index] * playerSpacing, -228, 0), doTweenDuration, true);
        pokerList[index].transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), doTweenDuration);
    }
    IEnumerator DOTween21(int index) {
        yield return new WaitForSeconds(waitTime * nextList[index]);
        //pokerList[index].gameObject.SetActive(true);
        pokerList[index].transform.DOLocalMove(new Vector3(-477 + nextList[index] * spacing, -10, 0), doTweenDuration, true);
    }
    IEnumerator DOTween22(int index) {
        yield return new WaitForSeconds(waitTime * nextList[index]);
       // pokerList[index].gameObject.SetActive(true);
        pokerList[index].transform.DOLocalMove(new Vector3(-263 + nextList[index] * spacing, -10, 0), doTweenDuration, true);
    }
    IEnumerator DOTween31(int index) {
        yield return new WaitForSeconds(waitTime * nextList[index]);
        //pokerList[index].gameObject.SetActive(true);
        pokerList[index].transform.DOLocalMove(new Vector3(-479 + nextList[index] * spacing, 161, 0), doTweenDuration, true);
    }
    IEnumerator DOTween32(int index) {
        yield return new WaitForSeconds(waitTime * nextList[index]);
       // pokerList[index].gameObject.SetActive(true);
        pokerList[index].transform.DOLocalMove(new Vector3(-265 + nextList[index] * spacing, 161, 0), doTweenDuration, true);
    }
    IEnumerator DOTween41(int index) {
        yield return new WaitForSeconds(waitTime * nextList[index]);
        //pokerList[index].gameObject.SetActive(true);
        pokerList[index].transform.DOLocalMove(new Vector3(130 + nextList[index] * spacing, 161, 0), doTweenDuration, true);
    }
    IEnumerator DOTween42(int index) {
        yield return new WaitForSeconds(waitTime * nextList[index]);
       // pokerList[index].gameObject.SetActive(true);
        pokerList[index].transform.DOLocalMove(new Vector3(343 + nextList[index] * spacing, 161, 0), doTweenDuration, true);
    }
    IEnumerator DOTween51(int index) {
        yield return new WaitForSeconds(waitTime * nextList[index]);
        //pokerList[index].gameObject.SetActive(true);
        pokerList[index].transform.DOLocalMove(new Vector3(130 + nextList[index] * spacing, -10, 0), doTweenDuration, true);
    }
    IEnumerator DOTween52(int index) {
        yield return new WaitForSeconds(waitTime * nextList[index]);
        //pokerList[index].gameObject.SetActive(true);
        pokerList[index].transform.DOLocalMove(new Vector3(343 + nextList[index] * spacing, -10, 0), doTweenDuration, true);
    }

    IEnumerator DOTween13(int index) {
        yield return new WaitForSeconds(waitTime * nextList[index]);
        //pokerList[index].gameObject.SetActive(true);
        pokerList[index].transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), doTweenDuration);
        pokerList[index].transform.DOLocalMove(new Vector3(-214 + nextList[index] * 114, -228, 0), doTweenDuration, true);
    }
    IEnumerator DOTween23(int index) {
        yield return new WaitForSeconds(waitTime * nextList[index]);
        //pokerList[index].gameObject.SetActive(true);
        pokerList[index].transform.DOScale(new Vector3(0.95f, 0.95f, 0.95f), doTweenDuration);
        pokerList[index].transform.DOLocalMove(new Vector3(-415 + nextList[index] * 43, -1.5f, 0), doTweenDuration, true);
    }
    IEnumerator DOTween33(int index) {
        yield return new WaitForSeconds(waitTime * nextList[index]);
        //pokerList[index].gameObject.SetActive(true);
        pokerList[index].transform.DOScale(new Vector3(0.95f, 0.95f, 0.95f), doTweenDuration);
        pokerList[index].transform.DOLocalMove(new Vector3(-415 + nextList[index] * 56, 143, 0), doTweenDuration, true);
    }
    IEnumerator DOTween43(int index) {
        yield return new WaitForSeconds(waitTime * nextList[index]);
        //pokerList[index].gameObject.SetActive(true);
        pokerList[index].transform.DOScale(new Vector3(0.95f, 0.95f, 0.95f), doTweenDuration);
        pokerList[index].transform.DOLocalMove(new Vector3(218 + nextList[index] * 56, 143, 0), doTweenDuration, true);
    }
    IEnumerator DOTween53(int index) {
        yield return new WaitForSeconds(waitTime * nextList[index]);
        //pokerList[index].gameObject.SetActive(true);
        pokerList[index].transform.DOScale(new Vector3(0.95f, 0.95f, 0.95f), doTweenDuration);
        pokerList[index].transform.DOLocalMove(new Vector3(126 + nextList[index] * 56, -0, 0), doTweenDuration, true);
    }
}

