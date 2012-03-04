using UnityEngine;
using System.Collections;

public class PcInputController : InputControllerBase {
	protected override bool getCursorState (out Vector2 position, out bool isDown)
	{
		position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		isDown = Input.GetMouseButton(0);
		return true;
	}
}
