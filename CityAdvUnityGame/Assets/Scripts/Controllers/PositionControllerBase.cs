using UnityEngine;
using System.Collections;

public abstract class PositionController {
	
	protected PlayerManager playerManager;
	protected CharacterController characterController;
	
	public PositionController(PlayerManager playerManager)
	{
		this.playerManager = playerManager;
		this.characterController = playerManager.GetComponent<CharacterController>();
	}
	
	/// <summary>
	/// Inherited class should override this function to update AbsolutePosision and MoveMotion, and then call its super.
	/// </summary>
	public virtual void Update()
	{
		if(AbsolutePosition.HasValue)
		{
			playerManager.transform.position = AbsolutePosition.Value;
		}
		
		if (SimpleMoveSpeed.HasValue)
		{
			characterController.SimpleMove(SimpleMoveSpeed.Value);
		}
		else if (MoveMotion.HasValue)
		{
			characterController.Move(MoveMotion.Value);
		}
	}
	
	public virtual void Start()
	{
	}
	
	public virtual void OnApplicationPause(bool pause)
	{
	}
	
	/// <summary>
	/// The target absolute position, or null if no immediate move. The caller should change transform immediately.
	/// </summary>
	/// <returns>
	/// A <see cref="System.Nullable<Vector3>"/>
	/// </returns>
	protected virtual Vector3? AbsolutePosition
	{
		get { return null; }
	}
	
	/// <summary>
	/// The player should move to the specified distance, or null if no graduate move. This field is ignored when MoveSpeed is not null. The caller should call CharacterController.move
	/// </summary>
	/// <returns>
	/// A <see cref="System.Nullable<Vector3>"/>
	/// </returns>
	protected virtual Vector3? MoveMotion
	{
		get {return null; }
	}
	
	/// <summary>
	/// The player should move in the specified speed.
	/// </summary>
	protected virtual Vector3? SimpleMoveSpeed
	{
		get {return null; }
	}
}
