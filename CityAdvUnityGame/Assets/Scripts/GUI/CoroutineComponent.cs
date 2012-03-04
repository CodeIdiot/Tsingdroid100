using UnityEngine;
using System.Collections;

public class CoroutineComponent : YieldInstruction {

	public CoroutineComponent() {
		
	}
	
	/// <summary>
	/// Called in OnGUI of a MonoBehavior. Return false if this component still hold the execution. Return true to resume the execution.
	/// </summary>
	/// <returns>
	/// A <see cref="System.Boolean"/>
	/// </returns>
	public virtual bool OnGUI() {
		return true;
	}
	
	
	public virtual void Update() {
		
	}
}
