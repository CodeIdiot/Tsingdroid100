using UnityEngine;
using System.Collections;

public class KeyboardPositionController : PositionController {
	
	private const float speed = 2.0f;
	//private Vector3? speedVector;
	private Vector3? moveMotion;
	
	public KeyboardPositionController(PlayerManager playerManager)
		:base(playerManager)
	{
	}
	
	public override void Update ()
	{
		float dist = speed * Time.deltaTime;
		if(Input.GetKey(KeyCode.W))
		{
			moveMotion = playerManager.transform.rotation * Vector3.forward * dist;
		}
		else if(Input.GetKey(KeyCode.S))
		{
			moveMotion = playerManager.transform.rotation * Vector3.back * dist;
		}
		else
		{
			moveMotion = null;
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
	
//	protected override Vector3? SimpleMoveSpeed {
//		get 
//		{
//			return speedVector;
//		}
//	}
}
