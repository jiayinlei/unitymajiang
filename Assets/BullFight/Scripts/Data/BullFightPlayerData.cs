using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/* ***********************************************
 * Describe:斗牛---玩家数据
 * Author :  MuLongFei
 * Email: 424113771@qq.com
 * DATA: 2017/7/27 14:06:21 
 * FileName: BullFightPlayerData 
 * Version: V1.0.1 
 * ***********************************************/

namespace Assets.Scripts.BullFight.Data {
    class BullFightPlayerData {
        private string name;
        private long gold;
        private int uid;
        private List<PokerCardBean> pokers = new List<PokerCardBean>();
        private bool isZhuangJia;//是否庄家 
        private PokerCardType pokerType;//牌形数据
        private int cardMaxNumber;//最大一张牌
        private PokerCardKinds maxNumberKinds;//最大一张牌的花色
        private int bombNumber;//炸弹点数 



        public string Name {
            get {
                return name;
            }

            set {
                name = value;
            }
        }

        public long Gold {
            get {
                return gold;
            }

            set {
                gold = value;
            }
        }

        public int Uid {
            get {
                return uid;
            }

            set {
                uid = value;
            }
        }

        internal List<PokerCardBean> Pokers {
            get {
                return pokers;
            }

            set {
                pokers = value;
            }
        }

        public bool IsZhuangJia {
            get {
                return isZhuangJia;
            }

            set {
                isZhuangJia = value;
            }
        }

        internal PokerCardType PokerType {
            get {
                return pokerType;
            }

            set {
                pokerType = value;
            }
        }

        public int CardMaxNumber {
            get {
                return cardMaxNumber;
            }

            set {
                cardMaxNumber = GetMaxCard().CardNumber;
            }
        }

        internal PokerCardKinds MaxNumberKinds {
            get {
                return maxNumberKinds;
            }

            set {
                maxNumberKinds = GetMaxCard().CardKind;
            }
        }

        public int BombNumber {
            get {
                return bombNumber;
            }

            set {
                bombNumber = value;
            }
        }

        public BullFightPlayerData(string name, long gold, int uid) {
            this.Name = name;
            this.Gold = gold;
            this.Uid = uid;
        }

        public PokerCardBean GetMaxCard() {
            pokers.Sort();
            return pokers[pokers.Count - 1];
        }
    }
}
