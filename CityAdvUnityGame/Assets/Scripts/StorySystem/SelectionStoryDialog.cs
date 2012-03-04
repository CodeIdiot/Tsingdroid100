using UnityEngine;
using System.Collections;

public class SelectionStoryDialog : StoryDialog {
	
	private SelectionResult resultRef;
	private string[] options;
	
	public SelectionStoryDialog(NPCBehavior npc, string text, SelectionResult resultRef, string[] options)
		: base(npc, text, true) {
		this.resultRef = resultRef;
		this.options = options;
	}
	
	public override bool OnGUI ()
	{
		base.OnGUI ();
		
		//draw selection area
		
		float spaceHeight = 30;
		
		//measure size
		float width = 300;	//minWidth
		float height = 100;	//minHeight
		foreach (string option in options) {
			Vector2 size = GUI.skin.button.CalcSize(new GUIContent(option));
			if (size.x > width) {
				width = size.x;
			}
			if (size.y > height) {
				height = size.y;
			}
		}
		
		//maxHeight
		float maxHeight = (boxArea.yMin - spaceHeight) / options.Length - spaceHeight;
		/*if(height < maxHeight) {
			height = maxHeight;
		}*/
		
		float totalHeight = height * options.Length + spaceHeight * (options.Length + 1);		
		Vector2 leftUp = new Vector2((Screen.width - width) / 2, 
		                             (Screen.height - boxArea.height - totalHeight) / 2 + spaceHeight);
		if(leftUp.y < spaceHeight) {
			leftUp = new Vector2(leftUp.x, spaceHeight);
		}
		
		for (int i = 0; i < options.Length; i++) {
			if (GUI.Button(new Rect(leftUp.x, leftUp.y + (height + spaceHeight) * i, width, height),
			               options[i])) {
				resultRef.Index = i;
				resultRef.Title = options[i];
				return true;
			}
		}
		
		return false;
	}
}
