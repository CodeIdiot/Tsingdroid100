using UnityEngine;
using System.Collections;

public class ReferencePool : MonoBehaviour {
	
	public GUISkin EntryScreenSkin;
	public GUISkin StorySkin;
	public Texture2D DefaultAvatar;
	public Texture2D NextArrowIcon;
	public Texture2D DialogBackground;
	
	private static ReferencePool instance;
	public static ReferencePool Instance {
		get {return instance; }
	}
	
	// Use this for initialization
	void Start () {
		instance = this;
	}

}
