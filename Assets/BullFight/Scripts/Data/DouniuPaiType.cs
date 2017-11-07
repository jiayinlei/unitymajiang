
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.BullFight.Data {
    public enum NiuType {
		没牛,
        牛1,
        牛2,
        牛3,
        牛4,
        牛5,
        牛6,
        牛7,
        牛8,
        牛9,
        牛牛,
        炸弹,
        五花牛,
        五小牛
    }
    public class DouniuPaiType {
        private static DouniuPaiType instance;

        static Dictionary<int, NiuType> typeDict = new Dictionary<int, NiuType>();

        public static DouniuPaiType Instance {
            get {
                if (instance == null) {
                    instance = new DouniuPaiType();
                }
                return instance;
            }
        }
        private DouniuPaiType(){
            AddTyptDict();
        }
        private void AddTyptDict() {
            typeDict.Add(0, NiuType.没牛);
            typeDict.Add(1,NiuType.牛1);
            typeDict.Add(2, NiuType.牛2);
            typeDict.Add(3, NiuType.牛3);
            typeDict.Add(4, NiuType.牛4);
            typeDict.Add(5, NiuType.牛5);
            typeDict.Add(6, NiuType.牛6);
            typeDict.Add(7, NiuType.牛7);
            typeDict.Add(8, NiuType.牛8);
            typeDict.Add(9, NiuType.牛9);
            typeDict.Add(10, NiuType.牛牛);
            typeDict.Add(11, NiuType.炸弹);
            typeDict.Add(12, NiuType.五花牛);
            typeDict.Add(13, NiuType.五小牛);
        }
        public NiuType GetPaiTypeDict(int typeIndex) {
            NiuType type;
            //Debug.Log("这个数："+typeIndex);
            typeDict.TryGetValue(typeIndex, out type);

            //Debug.Log("这个类型：" + type);
            return type;
        }
    }
}