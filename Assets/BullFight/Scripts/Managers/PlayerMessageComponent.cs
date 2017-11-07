using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace com.guojin.dn.player {
    public class PlayerMessageComponent {
        GameObject player;
        /// <summary>
        /// 两副牌第一副
        /// </summary>
        Transform playerHandPoker;
        /// <summary>
        /// 两副牌第二副
        /// </summary>
        Transform playerHandPoker1;
        /// <summary>
        /// 一副牌
        /// </summary>
        Transform playerHandPoker2;
        Text name;
        Text score;
        Image userImage;
        GameObject zhuang;
        Image zhu;
        Text win;
        Text win1;
        Text win2;
        Transform playerPoint;
        GameObject niuType;
        GameObject niuType1;
        GameObject niuType2;
        Image emoji;
        Image done;
        Image done2;
        Image ready;
        Image offLine;

        public Text Name {
            get {
                return name;
            }

            set {
                name = value;
            }
        }

        public Text Score {
            get {
                return score;
            }

            set {
                score = value;
            }
        }

        public Image UserImage {
            get {
                return userImage;
            }

            set {
                userImage = value;
            }
        }

        public GameObject Player {
            get {
                return player;
            }

            set {
                player = value;
            }
        }

        public Transform PlayerHandPoker1 {
            get {
                return playerHandPoker;
            }

            set {
                playerHandPoker = value;
            }
        }

        public GameObject Zhuang {
            get {
                return zhuang;
            }

            set {
                zhuang = value;
            }
        }

        public Image Zhu {
            get {
                return zhu;
            }

            set {
                zhu = value;
            }
        }

        public Text Win1 {
            get {
                return win;
            }

            set {
                win = value;
            }
        }

        public Transform PlayerPoint {
            get {
                return playerPoint;
            }

            set {
                playerPoint = value;
            }
        }

        public GameObject NiuType1 {
            get {
                return niuType;
            }

            set {
                niuType = value;
            }
        }

        public Image Emoji {
            get {
                return emoji;
            }

            set {
                emoji = value;
            }
        }

        public Image Done {
            get {
                return done;
            }

            set {
                done = value;
            }
        }

        public Image Ready {
            get {
                return ready;
            }

            set {
                ready = value;
            }
        }

        public Image OffLine {
            get {
                return offLine;
            }

            set {
                offLine = value;
            }
        }

        public GameObject NiuType2 {
            get {
                return niuType1;
            }

            set {
                niuType1 = value;
            }
        }

        public Transform PlayerHandPoker2 {
            get {
                return playerHandPoker1;
            }

            set {
                playerHandPoker1 = value;
            }
        }

        public Text Win2 {
            get {
                return win1;
            }

            set {
                win1 = value;
            }
        }

        public Text Win3 {
            get {
                return win2;
            }

            set {
                win2 = value;
            }
        }

        public GameObject NiuType3 {
            get {
                return niuType2;
            }

            set {
                niuType2 = value;
            }
        }

        public Transform PlayerHandPoker3 {
            get {
                return playerHandPoker2;
            }

            set {
                playerHandPoker2 = value;
            }
        }

        public Image Done2 {
            get {
                return done2;
            }

            set {
                done2 = value;
            }
        }
    }
}
