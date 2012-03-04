using UnityEngine;
using System.Collections;

public abstract class InputControllerBase {
	
	//"down"/"up" is state, "press"/"release" is action to change the two states.
	
	private bool prevCursorIsDown, cursorIsDown;
	private Vector2 prevCursorPosition, cursorPosition;
	private Vector2 prevCursorPressPosition;	//record the initial position when the cursor is pressed

	public InputControllerBase() {
		
	}
	
	public virtual void Start() {
	}
	
	public virtual void Update() {
	}
	
	public virtual void OnGUI() {
		prevCursorIsDown = cursorIsDown;
		prevCursorPosition = cursorPosition;
		
		getCursorState(out cursorPosition, out cursorIsDown);
		//Debug.Log("Cursor: " + cursorPosition.x.ToString() + ", " + cursorPosition.y.ToString() + ", " + cursorIsDown.ToString());
		
		if(!prevCursorIsDown && cursorIsDown) {
			prevCursorPressPosition = cursorPosition;
		}
	}
	
	/// <summary>
	/// get the cursor state.
	/// </summary>
	/// <param name="position">
	/// A <see cref="Vector2"/>. The cursor's position on the screen.
	/// </param>
	/// <param name="isDown">
	/// A <see cref="System.Boolean"/>. Whether the cursor is been pressing. (mouse click, finger on screen, ...)
	/// </param>
	/// <returns>
	/// A <see cref="System.Boolean"/>. Return false if no cursor is support
	/// </returns>
	protected virtual bool getCursorState(out Vector2 position, out bool isDown) {
		position = Vector2.zero;
		isDown = false;
		return false;
	}
	
	/*public bool isClick(GUIElement element, out Vector2 position) {
		return prevCursorIsDown && !cursorIsDown 
			&& element.HitTest(new Vector3(prevCursorPosition.x, prevCursorPosition.y, 0))
			&& element.HitTest(new Vector3(cursorPosition.x, cursorPosition.y, 0));
		element.
	}*/
	
	/// <summary>
	/// Return true if the user release the cursor
	/// </summary>
	/// <param name="position">
	/// A <see cref="Vector2"/>
	/// </param>
	/// <returns>
	/// A <see cref="System.Boolean"/>
	/// </returns>
	public bool isRelease() {
		Debug.Log("Check release: " + prevCursorIsDown.ToString() + ", " + cursorIsDown.ToString());
		return prevCursorIsDown && !cursorIsDown;
	}
	
	public bool isPress() {
		Debug.Log("Check press: " + prevCursorIsDown.ToString() + ", " + cursorIsDown.ToString());
		return !prevCursorIsDown && cursorIsDown;
	}
	
	/// <summary>
	/// Return whether the cursor is clicked (press and then release) inside the area. 
	/// </summary>
	/// <param name="area">
	/// A <see cref="Rect"/>
	/// </param>
	/// <returns>
	/// A <see cref="System.Boolean"/>
	/// </returns>
	public bool isClick(Rect guiArea) {
		Rect guiAreaMirroed = new Rect(guiArea.xMin, Screen.height - guiArea.yMax, guiArea.width, guiArea.height);
		return isPress()
			&& guiAreaMirroed.Contains(prevCursorPressPosition);
			/*&& guiAreaMirroed.Contains(prevCursorPosition);*/	//for finger, the current position will reset to zero.
	}
	
	/// <summary>
	/// Whether the cursor is over a collider
	/// </summary>
	/// <param name="collider">
	/// A <see cref="Collider"/>
	/// </param>
	/// <param name="distance">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <returns>
	/// A <see cref="System.Boolean"/>
	/// </returns>
	public bool isOver(Collider collider, float distance) {
		if(collider == null) {
			return false;
		}
		
		Ray ray;
		RaycastHit info;
		
		ray = Camera.main.ScreenPointToRay(prevCursorPressPosition);
		if(!Physics.Raycast(ray, out info, distance) || info.collider != collider) {
			Debug.Log("fail 1");
			return false;
		}
		
		/*ray = Camera.main.ScreenPointToRay(prevCursorPosition);
		if(!Physics.Raycast(ray, out info, distance) || info.collider != collider) {
			Debug.Log("fail 2");
			return false;
		}*/
		
		//Debug.Log("Over collider...");
		
		return true;
	}
	
	/// <summary>
	/// Whether the cursor is click on a collider
	/// </summary>
	/// <param name="collider">
	/// A <see cref="Collider"/>
	/// </param>
	/// <param name="distance">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <returns>
	/// A <see cref="System.Boolean"/>
	/// </returns>
	public bool isClickOn(Collider collider, float distance) {
		return isPress() && isOver(collider, distance);
	}
}
