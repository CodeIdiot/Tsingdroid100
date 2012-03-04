using UnityEngine;
using System.Collections;
//using UnityEditor;

//[ExecuteInEditMode]
public class MainCameraManager : MonoBehaviour {
	
	public Transform player;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//just follow the player
		transform.eulerAngles = player.eulerAngles;
		transform.position = player.position;
		
		//Debug.Log(transform.eulerAngles.ToString());
	}
}
