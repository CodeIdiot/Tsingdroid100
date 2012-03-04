using UnityEngine;
using System.Collections.Generic;

public class ChatTextContainer : MonoBehaviour {
	
	private const int GUI_DEPTH = 99;
	private static ChatTextContainer instance;
	public static ChatTextContainer Instance {
		get {return instance; }
	}
	
	private List<LineItem> lines;
	
	public void show(string name, string text, Color color) {
		LineItem item = new LineItem(name, text, color, Time.time);
		lines.Add(item);
	}
	
	// Use this for initialization
	void Start () {
		instance = this;
		lines = new List<LineItem>();
	}
	
	void OnGUI() {
		GUI.depth = GUI_DEPTH;
		GUI.skin = ReferencePool.Instance.StorySkin;
		GUIStyle textStyle = GUI.skin.GetStyle("DialogTextBox");
		
		float left = 20;
		float bottom = Screen.height - 20;	//bottom of the last line
		float maxHeight = Screen.height * 0.5f;
		float width = Screen.width * 0.6f;
		float stayTime = 5.0f;
		float fadeTime = 1.0f;
		
		for(int i = lines.Count - 1; i >= 0; i--) {
			LineItem item = lines[i];
			float elapsed = Time.time - item.time;
			
			//too much text
			if(bottom < maxHeight) {
				lines.RemoveRange(0, i + 1);
				break;
			}
			
			//time out
			if (elapsed > stayTime + fadeTime) {
				lines.RemoveAt(i);
				continue;
			}
			
			//fading
			float alpha;
			if (elapsed < stayTime) {
				alpha = 1.0f;
			} else {
				alpha = 1.0f - (elapsed - stayTime) / fadeTime;
			}
			
			//position
			GUIContent content = new GUIContent(((item.name != null && item.name.Length > 0) ? item.name + ": " : "") + item.text);
			float height = textStyle.CalcHeight(content, width);
			Rect lineArea = new Rect(left, bottom - height, width, height);
			
			//draw
			Color color = new Color(item.color.r, item.color.g, item.color.b, alpha);
			Color colorBak = GUI.contentColor;
			GUI.contentColor = color;
			GUI.Box(lineArea, content, textStyle);
			GUI.contentColor = colorBak;
			
			bottom -= height;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private struct LineItem {
		public string name;
		public string text;
		public Color color;
		public float time;
		
		public LineItem(string name, string text, Color color, float time) {
			this.name = name;
			this.text = text;
			this.color = color;
			this.time = time;
		}
	}
}
