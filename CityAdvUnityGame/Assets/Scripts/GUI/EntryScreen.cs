using UnityEngine;
using System.Collections;

public class EntryScreen : MonoBehaviour {
	
	void OnGUI() {
		
		GUI.skin = ReferencePool.Instance.EntryScreenSkin;
		
		GUILayout.BeginArea(new Rect(Screen.width / 2 - 160, Screen.height / 2 - 60, 320, 120));
		GUILayout.BeginVertical();
		
		if(GUILayout.Button("Play")) {
			PlayerPrefs.SetInt("demo", 0);
			Application.LoadLevel(1);
		}
		
		if(GUILayout.Button("Demo")) {
			PlayerPrefs.SetInt("demo", 1);
			Application.LoadLevel(1);
		}
		 
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.Escape)) {
			Application.Quit();
		}
	}
}
