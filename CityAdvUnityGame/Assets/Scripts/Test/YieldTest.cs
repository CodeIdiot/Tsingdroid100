using UnityEngine;
using System.Collections;

public class YieldTest : MonoBehaviour {
	
	IEnumerator r;
	
	void Start() {
		Debug.Log("Start");
		r = test();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnGUI() {
		if(GUI.Button(new Rect(0,0,100,100), "Test")) {
			Debug.Log("Before next: " + (r.Current == null ? "null" : r.Current.ToString()));
			r.MoveNext();
			Debug.Log("After next: " + (r.Current == null ? "null" : r.Current.ToString()));
		}
	}
	
	IEnumerator test() {
		Debug.Log("before 0");
		yield return 0;
		Debug.Log("before 1");
		yield return 1;
		Debug.Log("before 2");
		yield return 2;
		Debug.Log("after 2");
	}
}
