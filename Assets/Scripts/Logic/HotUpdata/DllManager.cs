using UnityEngine;  
using System.Collections;  
using System;  

public class DllManager:MonoBehaviour{  

	public static DllManager Instance;
	public System.Reflection.Assembly assembly;
	void Awake(){
		Instance = this;
	}

	public void InitAssembly(System.Reflection.Assembly assembly){
		this.assembly = assembly;
        AddCompotent(gameObject, "WndManager");
	}
		
	public Component AddCompotent(GameObject prefab, string compotentName){
		if (Application.platform == RuntimePlatform.Android) {
			if (assembly != null) {
				Type type = assembly.GetType (compotentName);
				if (prefab != null) {
					Debug.Log ("---------->add momo suc");
					prefab.AddComponent(type);
				}
				return null;
			}
		} else {
			Component com = prefab.GetComponent (compotentName);

			if(com == null){
				Type type = Type.GetType (compotentName); 
				prefab.AddComponent (type);
			}
			return com;
		}
		return null;
	}
}  