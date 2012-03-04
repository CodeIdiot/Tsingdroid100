using UnityEngine;
using System.Collections;

public class NavButton: MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		Collider collider = GetComponent<Collider>();
		if (InputManager.Controller.isClickOn(collider, 500)) {
			Debug.Log("Click on nav!");
			
#if UNITY_ANDROID
		AndroidJavaObject activity = PluginsHelper.GetUnityActivity();
		activity.Call("startNav");
#endif
		}
	}
}
