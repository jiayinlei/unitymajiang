using UnityEngine;  
using System.Collections;  
using System;  

public class AssemblyLoader : MonoBehaviour {

	private  string DLL_URL;

	System.Reflection.Assembly assembly;

	void Awake(){
        DLL_URL = "file:///" + Application.dataPath + "/Plugins/ExtDll.assetbundle";

        StartCoroutine(loadDllScript()); 
	}
	private IEnumerator loadDllScript()  
	{  
		WWW www = new WWW(DLL_URL);  
		yield return www;  
		AssetBundle bundle = www.assetBundle;  
		TextAsset asset = bundle.LoadAsset("ExtDll",typeof(TextAsset)) as TextAsset;  
		assembly = System.Reflection.Assembly.Load(asset.bytes);  
		yield return null;

		if (assembly != null) {
			Type type = assembly.GetType ("DllManager");
			Component com = gameObject.AddComponent(type);
			System.Reflection.MethodInfo methodInfo = type.GetMethod ("InitAssembly");
			object[] obj = new object[]{assembly};
			methodInfo.Invoke (com, obj); //反射调用DllManager 
			Debug.Log ("dll load suc");
		}
	}  
}
