using UnityEngine;
using System.Collections;

/// <summary>
/// Reference wrapper to store the result of the selection story dialog
/// </summary>
public class SelectionResult {
	private int index = -1;
	private string title = null;
	
	public int Index { 
		get { return index; } 
		set { index = value; }
	}
	
	public string Title { 
		get { return title; } 
		set { title = value; }
	}
}
