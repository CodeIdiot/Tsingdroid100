using UnityEngine;
using System.Collections;

public class SkyboxController : MonoBehaviour {
	
	public Vector3 speed;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(speed.x * Time.deltaTime, speed.y * Time.deltaTime, speed.z * Time.deltaTime);
	}
}
