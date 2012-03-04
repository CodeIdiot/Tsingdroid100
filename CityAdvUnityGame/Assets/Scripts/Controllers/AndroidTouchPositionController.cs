using UnityEngine;
using System.Collections;

public class AndroidTouchPositionController : PositionController {

	private const float speed = 2.0f;
	private Vector3? moveMotion;
	
	public AndroidTouchPositionController(PlayerManager playerManager)
		:base(playerManager)
	{
	}
	
	public override void Update ()
	{
		float dist = speed * Time.deltaTime;
		
		Rect touchArea = new Rect(Screen.width - 50,
		                          Screen.height - 50,
		                          50, 50);	//right up corner
		
		if(Input.touchCount == 0) {
			moveMotion = null;
		} else {
			Touch t = Input.touches[0];
			if (touchArea.Contains(t.position)) {
				moveMotion = playerManager.transform.rotation * Vector3.forward * dist;
			} else {
				moveMotion = null;
			}
		}
		
		if(moveMotion != null)
		{
			moveMotion = new Vector3(moveMotion.Value.x, 0, moveMotion.Value.z);
		}
		
		base.Update ();
	}
	
	protected override System.Nullable<Vector3> MoveMotion {
		get 
		{
			return moveMotion;
		}
	}
}
