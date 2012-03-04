using UnityEngine;
using System.Collections;

/// <summary>
/// The manager for processing user's input
/// </summary>
public class InputManager : MonoBehaviour {
	
	private static InputManager instance;
	
	private InputControllerBase inputController;
	
	// Use this for initialization
	void Start () {
		instance = this;
		
		if(Application.platform == RuntimePlatform.Android) {
			inputController = new AndroidInputController();
			Debug.Log("Using Android input");
		} else {
			inputController = new PcInputController();
			Debug.Log("Using PC input");
		}
		
		if(inputController != null) {
			inputController.Start();
		}
	}
	
	void OnGUI() {
		if(inputController != null) {
			inputController.OnGUI();
		}
	}
	
	void Update() {
		if(inputController != null) {
			inputController.Update();
		}
	}
	
	public static InputControllerBase Controller {
		get {
			return instance.inputController;
		}
	}
	
}