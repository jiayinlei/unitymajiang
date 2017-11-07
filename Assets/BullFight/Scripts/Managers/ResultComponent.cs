using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace com.guojin.dn.player {
    public class ResultComponent {
        GameObject player;
        Text name;
        Text id;
        Text score;
        Image userImage;
        Image win;
        Text winCount;
        Text loseCount;
        Text chapter;
        Text equalCount;

        public GameObject Player {
            get {
                return player;
            }

            set {
                player = value;
            }
        }
        

        public Text Name {
            get {
                return name;
            }

            set {
                name = value;
            }
        }

        public Text Id {
            get {
                return id;
            }

            set {
                id = value;
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

        public Image Win {
            get {
                return win;
            }

            set {
                win = value;
            }
        }

        public Text WinCount {
            get {
                return winCount;
            }

            set {
                winCount = value;
            }
        }

        public Text LoseCount {
            get {
                return loseCount;
            }

            set {
                loseCount = value;
            }
        }

        public Text Chapter {
            get {
                return chapter;
            }

            set {
                chapter = value;
            }
        }

        public Text EqualCount {
            get {
                return equalCount;
            }

            set {
                equalCount = value;
            }
        }
    }
}
