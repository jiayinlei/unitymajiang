using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/* ***********************************************
 * Describe:该类功能描述
 * Author :  MuLongFei
 * Email: 424113771@qq.com
 * DATA: 2017/8/2 14:22:56 
 * FileName: PokerFactory 
 * Version: V1.0.1
 * ***********************************************/

namespace Assets.Scripts.BullFight.Data {
    class PokerFactory {
        public static List<string> Creat(int[][] data) {
            List<string> pai = new List<string>();

            foreach (var item in data) {
                int pokerNumber = item[2];
                PokerCardKinds kinds = (PokerCardKinds)item[1];

                switch (kinds) {
                    case PokerCardKinds.DEFAULT:
                        break;
                    case PokerCardKinds.DIAMONDS:
                        pai.Add("fp_" + pokerNumber);
                        break;
                    case PokerCardKinds.CLUBS:
                        pai.Add("mh_" + pokerNumber);
                        break;
                    case PokerCardKinds.HEARTS:
                        pai.Add("hx_" + pokerNumber);
                        break;
                    case PokerCardKinds.SPADES:
                        pai.Add("ht_" + pokerNumber);
                        break;
                    default:
                        break;
                }
            }

            return pai;
        }
    }
}
