using Assets.Scripts.BullFight.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using com.guojin.core.utils;
public enum SpriteName
{
    
}
namespace Assets.Scripts.BullFight.Manager {
    public class DynamicUIManager {
        public Dictionary<string, Sprite> dySpriteDic = new Dictionary<string, Sprite>();
        private object[] sprites;
        private static DynamicUIManager instance;

        public static DynamicUIManager Instance {
            get {
                if (instance == null) {
                    instance = new DynamicUIManager();
                }
                return instance;
            }
        }
        
        /// <summary>
        /// 初始化动态加载的图片（会导致多个打包的Texture中有名字相同的图片，逻辑中不予添加重复字典。所以会使用其他图集的图片导致DrawCall增加一到两个）
        /// </summary>
        public void InitDynamicUIManager() {
            Sprite sp;
            sprites = Resources.LoadAll<Sprite>("poker");
            for (int i = 0; i < sprites.Length; i++) {
                sp = sprites[i] as Sprite;
                if (!dySpriteDic.ContainsKey(sp.name)) {
                    dySpriteDic.Add(sp.name, sp);
                } else {
                    Debug.Log("dySpriteDic中已经存在：" + sp.name + "这个Key了");
                }
            }

            sprites = Resources.LoadAll<Sprite>("UIPicture/Emoji");
            DNGlobalData.emojiCount = sprites.Length;
            for (int i = 0; i < sprites.Length; i++) {
                sp = sprites[i] as Sprite;
                if (!dySpriteDic.ContainsKey(sp.name)) {
                    dySpriteDic.Add(sp.name, sp);
                } else {
                    Debug.Log("dySpriteDic中已经存在：" + sp.name + "这个Key了");
                }
            }

            sprites = Resources.LoadAll<Sprite>("Pictures/AddNum");
            foreach (var s in sprites) {
                sp = s as Sprite;
                if (!dySpriteDic.ContainsKey(sp.name)) {
                    dySpriteDic.Add(sp.name, sp);
                } else {
                    Debug.Log("dySpriteDic中已经存在：" + sp.name + "这个Key了");
                }
            }

            sprites = Resources.LoadAll<Sprite>("Pictures/WinPictures");
            foreach (var s in sprites) {
                sp = s as Sprite;
                if (!dySpriteDic.ContainsKey(sp.name)) {
                    dySpriteDic.Add(sp.name, sp);
                } else {
                    Debug.Log("dySpriteDic中已经存在：" + sp.name + "这个Key了");
                }
            }
            sprites = Resources.LoadAll<Sprite>("Pictures/Images");
            foreach (var s in sprites) {
                sp = s as Sprite;
                if (!dySpriteDic.ContainsKey(sp.name)) {
                    dySpriteDic.Add(sp.name, sp);
                } else {
                    Debug.Log("dySpriteDic中已经存在：" + sp.name + "这个Key了");
                }
            }

            //sp = Resources.Load<Sprite>("Pictures/trumpet");
           // dySpriteDic.Add(sp.name, sp);
            //sp = Resources.Load<Sprite>("Pictures/Done");
            //dySpriteDic.Add(sp.name, sp);
        }
        /// <summary>
        /// 根据枚举名字获取精灵
        /// </summary>
        /// <returns></returns>
        public Sprite BasePageNameGetSprite(SpriteName spriteName) {
            string name = spriteName.ToString();
            Sprite sp = null;
            if (dySpriteDic.TryGetValue(name, out sp)) {
                return sp;
            } else {
                return null;
            }
        }
        public Sprite BaseNiuTypeGetSprite(NiuType niuType) {
            string name = niuType.ToString();
            Sprite sp = null;
            if (dySpriteDic.TryGetValue(name, out sp)) {
                return sp;
            } else {
                return null;
            }
        }
        /// <summary>
        /// 根据名字获取精灵
        /// </summary>
        /// <returns></returns>
        public Sprite BaseNameGetSprite(string name) {
            Sprite sp = null;
            if (dySpriteDic.TryGetValue(name, out sp)) {
                return sp;
            } else {
                return null;
            }
        }
    }
}