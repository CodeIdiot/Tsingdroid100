using UnityEngine;
using System.Collections;

public class StoryDialog : CoroutineComponent {
	
	private const int GUI_DEPTH = 1;
	
	private const string buttonText = "";
	
	private NPCBehavior npc;
	private string text;
	//private Vector2 scrollViewVector = Vector2.zero;
	private float arrowOffset;
	private bool disableNext;	//if this class is inherited and the input should be processed by the child, set this to be true.
	
	//for inherited class
	protected Rect boxArea, sideArea, titleArea, imageArea, textArea, arrowArea;
	
	public StoryDialog(NPCBehavior npc, string text) : this(npc, text, false) {
		
	}
	
	public StoryDialog(NPCBehavior npc, string text, bool disableNext) {
		this.npc = npc;
		this.text = text;
		this.disableNext = disableNext;
	}
	
	/// <summary>
	/// Called by OnGUI. Return true when 'ok' is clicked.
	/// </summary>
	public override bool OnGUI ()
	{
		base.OnGUI();
		
		bool result = false;
		
		GUI.depth = GUI_DEPTH;
		GUI.skin = ReferencePool.Instance.StorySkin;
		
/*		//calc sizes
		//boxArea = new Rect(100, Screen.height - Screen.height * 0.4f, Screen.width - 200, Screen.height * 0.4f);
		//Rect buttonArea = new Rect(boxArea.xMax - 40, boxArea.yMax - 40, 40, 40);
		boxArea = new Rect((Screen.width - 1024) / 2, Screen.height - 226, 1024, 226);
		
		float titleHeight = GUI.skin.label.CalcHeight(new GUIContent(npc.Name), boxArea.height - GUI.skin.box.margin.vertical);
		float avatarSize = boxArea.height - GUI.skin.box.margin.vertical - GUI.skin.label.margin.vertical - titleHeight; 
		
		sideArea = new Rect(boxArea.xMax - GUI.skin.box.margin.right - avatarSize, 
		                           boxArea.yMin + GUI.skin.box.margin.top, 
		                           avatarSize, 
		                           boxArea.height - GUI.skin.box.margin.vertical);
		titleArea = new Rect(sideArea.xMin + GUI.skin.label.margin.left, 
		                          sideArea.yMin + GUI.skin.label.margin.top, 
		                          avatarSize - GUI.skin.label.margin.right, titleHeight);
		imageArea = new Rect(sideArea.xMin + GUI.skin.box.margin.left, 
		                          titleArea.yMax + GUI.skin.label.margin.bottom,
		                          avatarSize, avatarSize);*/
		//Vector2 buttonSize = GUI.skin.button.CalcSize(new GUIContent(buttonText));
		
		/*Rect scrollArea = new Rect(boxArea.xMin + GUI.skin.scrollView.margin.left, 
                           boxArea.yMin + GUI.skin.scrollView.margin.top,
                           boxArea.width - GUI.skin.box.margin.horizontal - GUI.skin.scrollView.margin.horizontal - sideArea.width, 
                           boxArea.height - GUI.skin.scrollView.margin.vertical);
		float textHeight = GUI.skin.box.CalcHeight(new GUIContent(text), 
		          				scrollArea.width - GUI.skin.verticalScrollbar.margin.horizontal - GUI.skin.verticalScrollbar.fixedWidth);
		*/
		/*Rect textArea = new Rect(scrollArea.xMin, 
		                         scrollArea.yMin, 
		                         scrollArea.width - GUI.skin.verticalScrollbar.fixedWidth - GUI.skin.verticalScrollbar.margin.horizontal, 
		                         textHeight);*/
/*		textArea = new Rect(boxArea.xMin + GUI.skin.scrollView.margin.left, 
                           boxArea.yMin + GUI.skin.scrollView.margin.top,
                           boxArea.width - GUI.skin.box.margin.horizontal - sideArea.width, 
                           boxArea.height - GUI.skin.box.margin.vertical);
		
		arrowArea = new Rect(textArea.xMax - 20,
		                          textArea.yMax - 23 + arrowOffset,
		                          20, 20);*/
		
		
		//absolute
		float top = Screen.height - 234;
		float left = (Screen.width - 1024) / 2.0f;
		boxArea = new Rect(left, top, 1024, 234);
		titleArea = new Rect(left + 240, top + 90, 508, 40);
		imageArea = new Rect(left + 748, top + 34, 200, 200);
		textArea = new Rect(left + 234, top + 131, 508, 103);
		arrowArea = new Rect(left + 722, top + 210 + arrowOffset, 20, 20);
		
		
		//draw
		//GUI.Box(boxArea, "");
		GUI.DrawTexture(boxArea, ReferencePool.Instance.DialogBackground);
		
		//scrollViewVector = GUI.BeginScrollView(scrollArea, scrollViewVector, textArea);
		GUI.Box(textArea, "\"" + text + "\"", GUI.skin.GetStyle("DialogTextBox"));
		//GUI.EndScrollView();
		
		GUI.Label(titleArea, "[" + npc.Name + "]");
		
		Texture2D tex = npc.Avatar == null ? ReferencePool.Instance.DefaultAvatar : npc.Avatar;
		GUI.DrawTexture(imageArea, tex, ScaleMode.ScaleToFit);
		
		if(!disableNext) {
			GUI.DrawTexture(arrowArea, ReferencePool.Instance.NextArrowIcon);
		}
		
		/*
		if(GUI.Button(buttonArea, buttonText)) {
			result = true;
		}*/
		
		if(!disableNext && InputManager.Controller.isClick(boxArea)) {
			result = true;
		}
		
		return result;
	}
	
	/// <summary>
	/// Called by OnUpdate
	/// </summary>
	public override void Update() {
		base.Update();
		if(!disableNext) {
			arrowOffset = Mathf.Sin(Time.time * 5) * 3;
		}
	}
}
