using UnityEngine;
using System.Collections;

public class AndroidInputController : InputControllerBase {

	protected override bool getCursorState (out Vector2 position, out bool isDown)
	{
		if(Input.touchCount == 0) {
			position = Vector2.zero;
			isDown = false;
			return false;
		} else {
			Touch touch = Input.GetTouch(0);
			position = touch.position;
			isDown = true;
			return true;
		}
	}
}
