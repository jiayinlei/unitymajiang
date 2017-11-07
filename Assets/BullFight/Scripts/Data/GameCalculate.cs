using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/* ***********************************************
 * Describe:扑克牌算法计算
 * Author :  MuLongFei
 * Email: 424113771@qq.com
 * DATA: 2017/7/26 15:24:11 
 * FileName: GameCalculate 
 * Version: V1.0.1
 * ***********************************************/

namespace Assets.Scripts.BullFight.Data {
    class GameCalculate {
        //private static List<PokerCardBean> pokers = new List<PokerCardBean>();

        //internal static List<PokerCardBean> Pokers {
        //    get {
        //        return pokers;
        //    }

        //    set {
        //        pokers = value;
        //    }
        //}

        /// <summary>
        /// 计算并设置玩家牌形
        /// </summary>
        /// <returns></returns>
        public static PokerCardType CalculatePlayerCardType(BullFightPlayerData playerData) {
            //排序
            List<PokerCardBean> pokers = playerData.Pokers;
            pokers.Sort();

            //炸弹
            for (int i = 0; i < 2; i++) {
                PokerCardBean poker1 = pokers[i];
                PokerCardBean poker2 = pokers[i + 1];
                PokerCardBean poker3 = pokers[i + 2];
                PokerCardBean poker4 = pokers[i + 3];

                if (poker1.CardNumber == poker2.CardNumber && poker1.CardNumber == poker3.CardNumber && poker1.CardNumber == poker4.CardNumber) {
                    playerData.PokerType = PokerCardType.BOMB;
                    playerData.BombNumber = poker1.CardNumber;
                    return PokerCardType.BOMB;
                }
            }

            //五小牛
            int num1 = pokers[0].CardNumber;
            int num2 = pokers[1].CardNumber;
            int num3 = pokers[2].CardNumber;
            int num4 = pokers[3].CardNumber;
            int num5 = pokers[4].CardNumber;

            if ((num1 < 5 && num2 < 5 && num3 < 5 && num4 < 5 && num5 < 5) && (num1+num2+num3+num4+num5 <= 10)) {
                playerData.PokerType = PokerCardType.XN5;
                return PokerCardType.XN5;
            }

            //五花牛
            if (num1 >= 11 && num2 >= 11 && num3 >= 11 && num4 >= 11 && num5 >= 11) {
                playerData.PokerType = PokerCardType.HN5;
                return PokerCardType.HN5;
            }

            //四花牛
            for (int i = 0; i < pokers.Count; i++) {
                if (pokers[0].CardNumber == 10 && pokers[1].CardNumber >= 11 && pokers[2].CardNumber >= 11 && pokers[3].CardNumber >= 11 && pokers[4].CardNumber >= 11) {
                    playerData.PokerType = PokerCardType.HN4;
                    return PokerCardType.HN4;
                }
            }

            //牛牛 牛几 无牛
            for (int i = 0; i < pokers.Count - 2; i++) {
                PokerCardBean poker1 = pokers[i];
                for (int j = i + 1; j < pokers.Count - 1; j++) {
                    PokerCardBean poker2 = pokers[j];
                    for (int k = j + 1; k < pokers.Count; k++) {
                        PokerCardBean poker3 = pokers[k];

                        int sum = poker1.CardNumber2() + poker2.CardNumber2() + poker3.CardNumber2();
                        
                        if (sum >= 10 && sum % 10 == 0) {
                            //有牛
                            int niuNum = (pokers[0].CardNumber2() + pokers[1].CardNumber2() + pokers[2].CardNumber2() + pokers[3].CardNumber2() + pokers[4].CardNumber2()) % 10;

                            if (niuNum == 0) {
                                //牛牛
                                playerData.PokerType = PokerCardType.NN;
                                return PokerCardType.NN;
                            } else {
                                //牛几
                                playerData.PokerType = GetPokerType(niuNum);
                                return GetPokerType(niuNum);
                            }
                        }
                    }
                }
            }
            //无牛 
            playerData.PokerType = PokerCardType.NONE;
            return PokerCardType.NONE;
        }

        private static PokerCardType GetPokerType(int num) {
            switch (num) {
                case 1:
                    return PokerCardType.N1;
                case 2:
                    return PokerCardType.N2;
                case 3:
                    return PokerCardType.N3;
                case 4:
                    return PokerCardType.N4;
                case 5:
                    return PokerCardType.N5;
                case 6:
                    return PokerCardType.N6;
                case 7:
                    return PokerCardType.N7;
                case 8:
                    return PokerCardType.N8;
                case 9:
                    return PokerCardType.N9;
                default:
                    return PokerCardType.NONE;
            }
        }


        /// <summary>
        /// 庄家和闲家比较牌形
        /// </summary>
        /// <param name="zhuang"></param>
        /// <param name="xian"></param>
        public static BullFightPlayerData ComparerCardType(BullFightPlayerData zhuang, BullFightPlayerData xian) {
            //Debug.Log(string.Format("庄家：{0}，闲家{1}", zhuang.PokerType, xian.PokerType));
            
            if (zhuang.PokerType > xian.PokerType) {
                //庄家赢
                return zhuang;
            } else if(zhuang.PokerType == xian.PokerType) {
                //都无牛
                if (zhuang.PokerType == PokerCardType.NONE) {
                    //无牛比较
                    //1.比较最大的一张牌
                    PokerCardBean zhuangPoker = zhuang.GetMaxCard();
                    PokerCardBean xianPoker = xian.GetMaxCard();

                    if (zhuangPoker.CardNumber > xianPoker.CardNumber) {
                        //庄家赢
                        return zhuang;
                    } else if(zhuangPoker.CardNumber == xianPoker.CardNumber){
                        //2.比较花色
                        if (zhuangPoker.CardKind > xianPoker.CardKind) {
                            //庄家赢
                            return zhuang;
                        } else {
                            //闲家赢
                            return xian;
                        }
                    } else {
                        //闲家赢 
                        return xian;
                    }
                }

                //都是炸弹
                if (zhuang.PokerType == PokerCardType.BOMB) {
                    if (zhuang.BombNumber > xian.BombNumber) {
                        //庄家赢
                        return zhuang;
                    } else {
                        //闲家赢
                        return xian;
                    }
                }

                //都是五花牛
                if (zhuang.PokerType == PokerCardType.HN5) {
                    if (zhuang.CardMaxNumber > xian.CardMaxNumber) {
                        //庄家赢
                        return zhuang;
                    } else if(zhuang.CardMaxNumber == xian.CardMaxNumber){
                        //比较花色
                        if (zhuang.MaxNumberKinds > xian.MaxNumberKinds) {
                            //庄家赢
                            return zhuang;
                        } else {
                            //闲家赢
                            return xian;
                        }
                    } else {
                        //闲家赢
                        return xian;
                    }
                }

                string str = zhuang.PokerType.ToString();
                string[] types = new string[] { "N1", "N2", "N3", "N4", "N5", "N6", "N7", "N8", "N9", "NN", "HN4", "XN5" };

                foreach (string item in types) {
                    if (str.Contains(item)) {
                        PokerCardBean zhuangPoker = zhuang.GetMaxCard();
                        PokerCardBean xianPoker = xian.GetMaxCard();

                        if (zhuangPoker.CardNumber > xianPoker.CardNumber) {
                            //庄家赢
                            return zhuang;
                        } else if (zhuangPoker.CardNumber == xianPoker.CardNumber) {
                            //2.比较花色
                            if (zhuangPoker.CardKind > xianPoker.CardKind) {
                                //庄家赢
                                return zhuang;
                            } else {
                                //闲家赢
                                return xian;
                            }
                        } else {
                            //闲家赢 
                            return xian;
                        }

                    }
                }
            } else {
                //闲家赢
                return xian;
            }

            return null;
        }
    }
}
