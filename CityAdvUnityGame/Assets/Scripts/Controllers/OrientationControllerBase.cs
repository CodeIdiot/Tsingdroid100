using UnityEngine;
using System.Collections;

public abstract class OrientationController {
	
	protected PlayerManager playerManager;
	
	public OrientationController(PlayerManager playerManager)
	{
		this.playerManager = playerManager;
	}
	
	/// <summary>
	/// Inherited class should override this function to update LocalEulerAngles and Rotation, and then call its super.
	/// </summary>
	public virtual void Update ()
	{
		if (Rotation.HasValue)
		{
			playerManager.transform.rotation = Rotation.Value;
		} 
		else if (EulerAngles.HasValue)
		{
			playerManager.transform.eulerAngles = EulerAngles.Value;
		}
	}
	
	public virtual void Start()
	{
	}
	
	public virtual void OnApplicationPause(bool pause)
	{
	}
	
	/// <summary>
	/// This field is ignored if Rotation is not null
	/// </summary>
	protected virtual Vector3? EulerAngles
	{
		get {return null;}
	}
	
	protected virtual Quaternion? Rotation 
	{
		get {return null;}
	}
}
