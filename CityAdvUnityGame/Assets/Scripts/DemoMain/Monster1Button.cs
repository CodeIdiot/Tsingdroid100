using UnityEngine;
using System.Collections;

public class Monster1Button : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		Collider collider = GetComponent<Collider>();
		if (InputManager.Controller.isClickOn(collider, 500)) {
			Debug.Log("Click on Boss!");
			
			Application.LoadLevel(2);
		}
	}
}
