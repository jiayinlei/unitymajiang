using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/* ***********************************************
 * Describe:扑克牌模型
 * Author :  MuLongFei
 * Email: 424113771@qq.com
 * DATA: 2017/7/26 14:56:13 
 * FileName: PokerCardBean 
 * Version: V1.0.1
 * ***********************************************/

namespace Assets.Scripts.BullFight.Data {
    class PokerCardBean : IComparable<PokerCardBean> {

        private int cardNumber;//牌的大小
        private PokerCardKinds cardKind;//花色
        private int actualNumber;//实际大小

        public PokerCardBean(int cardNumber, PokerCardKinds cardKind) {
            this.CardNumber = cardNumber;
            this.CardKind = cardKind;
        }

        public PokerCardBean(int cardNumber, PokerCardKinds cardKind, int actualNumber) {
            this.cardNumber = cardNumber;
            this.cardKind = cardKind;
            this.actualNumber = actualNumber;
        }

        public int CardNumber {
            get {
                return cardNumber;
            }

            set {
                cardNumber = value;
            }
        }

        public int CardNumber2() {
            if (cardNumber >= 11) {
                return 10;
            } else {
                return cardNumber;
            }
        }

        public PokerCardKinds CardKind {
            get {
                return cardKind;
            }

            set {
                cardKind = value;
            }
        }

        public int CompareTo(PokerCardBean other) {
            return this.CardNumber.CompareTo(other.CardNumber);
        }
    }
}
