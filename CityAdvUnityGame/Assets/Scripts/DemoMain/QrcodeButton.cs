using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class QrcodeButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		Collider collider = GetComponent<Collider>();
		if (InputManager.Controller.isClickOn(collider, 500)) {
			Debug.Log("Click on qr!");
			
#if UNITY_ANDROID
			AndroidJavaObject activity = PluginsHelper.GetUnityActivity();
			activity.Call("startQrScan");
#endif
		}
	}
	
	public void OnScanResult(string content) {
		Debug.Log("Content: " + content);
		
		Regex regex = new Regex("geo:.*\\?q=([0-9]+)\\s.*");
		Match match = regex.Match(content);
		
		int level = 0;
		if(match != null) {
			string idStr = match.Groups[1].Value;
			int id = int.Parse(idStr);
			
			switch(id) {
			case 47:
				level = 1;
				break;
				
			case 39:
				level = 2;
				break;
				
			case 29:
				level = 3;
				break;
			}
		}
		
		if (level != 0) {
			Application.LoadLevel(level);
		}
	}
}
