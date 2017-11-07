
using Assets.Scripts.BullFight.Manager;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.Scripts.BullFight.Data {
    public class Poker : MonoBehaviour {
        private int thisPosID;
        private bool outOver = false;
        private Transform trans;
        private int outNum;
        private float defaultY;
        GameManager inst = GameManager.instance;
        bool presses = false;
        //private int pokerValue;
        //private int pokerNumber;

        //public int Value {
        //    get {
        //        return pokerValue;
        //    }

        //    set {
        //        pokerValue = value;
        //    }
        //}

        //public int PokerNumber {
        //    get {
        //        return pokerNumber;
        //    }

        //    set {
        //        pokerNumber = value;
        //    }
        //}

        private void Start() {
            trans = transform;

            transform.GetComponent<Button>().onClick.AddListener(() => {
                transform.PlayButtonVoice();
                if (presses == false) {
                    if (inst.fillList.Count < 3) {
                        inst.FillCalText(transform);
                        //GameManager.outCount++;
                        presses = true;
                    }
                } else {
                    inst.DeleteClaText(transform);
                    presses = false;
                }
            });

        }
    }
}