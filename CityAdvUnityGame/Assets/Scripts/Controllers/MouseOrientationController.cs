using UnityEngine;
using System.Collections;

public class MouseOrientationController : OrientationController
{

	private const float xSpeed = 250.0f;
	private const float ySpeed = 120.0f;

	private const float yMinLimit = -20f;
	private const float yMaxLimit = 80f;

	private float x = 0.0f;
	private float y = 0.0f;

	private Quaternion? rotation;
	private Vector3? eulerAngles;
	
	public MouseOrientationController(PlayerManager playerManager) : base(playerManager) {
	}
	
	public override void Update ()
	{
		//remote?
		if (RemoteSensorClient.Instance != null && RemoteSensorClient.Instance.OrientationEnabled) {
			Vector3 sensorAngles = RemoteSensorClient.Instance.CameraAngles;
			eulerAngles = new Vector3(-sensorAngles.x, -sensorAngles.y, 0);
			rotation = null;
		} else {
			if(!Input.GetMouseButton(0)) {
				rotation = null;
				eulerAngles = null;
				return;
			}
			
			x = playerManager.transform.eulerAngles.y;
			y = playerManager.transform.eulerAngles.x;
			
			x += Input.GetAxis ("Mouse X") * xSpeed * 0.02f;
			y -= Input.GetAxis ("Mouse Y") * ySpeed * 0.02f;
			
			//y = clampAngle (y, yMinLimit, yMaxLimit);
			
			rotation = Quaternion.Euler (y, x, 0);
		}
		base.Update ();
	}

	protected override Quaternion? Rotation 
	{
		get { return rotation; }
	}
	
	protected override Vector3? EulerAngles {
		get {
			return eulerAngles;
		}
	}

	private static float clampAngle (float angle, float min, float max)
	{
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp (angle, min, max);
	}
}
