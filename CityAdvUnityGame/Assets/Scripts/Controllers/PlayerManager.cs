using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
	
	private PositionController positionController;
	private OrientationController orientationController;
	
	// Use this for initialization
	void Start () {
		
		if (Application.platform == RuntimePlatform.Android)
		{
			orientationController = new AndroidAccelOrientationManager(this);
			Debug.Log("Using android orientation");
		}
		else
		{
			Debug.Log("Using PC orientation");
			orientationController = new MouseOrientationController(this);
			positionController = new KeyboardPositionController(this);
		}
	
		
		if(orientationController != null)
		{
			orientationController.Start();
		}
		
		if(positionController != null)
		{
			positionController.Start();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(positionController != null)
		{
			positionController.Update();
		}
		
		if(orientationController != null)
		{
			orientationController.Update();
		}
	}
	
	void OnApplicationPause(bool pause)
	{
		if(orientationController != null)
		{
			orientationController.OnApplicationPause(pause);
		}
		
		if(positionController != null)
		{
			positionController.OnApplicationPause(pause);
		}
	}
}
