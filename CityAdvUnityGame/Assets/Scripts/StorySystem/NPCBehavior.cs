using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class NPCBehavior : MonoBehaviour {
	
	/// <summary>
	/// Name of the NPC
	/// </summary>
	public string Name = "";
	
	/// <summary>
	/// Description of the NPC
	/// </summary>
	public string Description = "";
	
	/// <summary>
	/// Avatar of the NPC
	/// </summary>
	public Texture2D Avatar;	
	
	/// <summary>
	/// When <player, NPC> < NearingRadius, OnNearing is invoked for once.
	/// </summary>
	public float NearingRadius = 2.0f;
	
	/// <summary>
	/// When <player, NPC> > LeavingRadius, OnLeaving is invoked for once.
	/// </summary>
	public float LeavingRadius = 4.0f;
	
	private PlayerManager playerManager;
	private bool isNearing, isLeaving, isPlayerHeading, isNpcHeading;
	private float prevDistSquared;
	private CoroutineList coroutineList;
	
	// Use this for initialization
	protected void Start () {
		playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
		
		isNearing = isLeaving = isPlayerHeading = isNpcHeading = false;
		prevDistSquared = float.MaxValue;
		
		coroutineList = new CoroutineList();
	}
	
	// Update is called once per frame
	protected void Update () {
		//calculate distance and (TODO)heading
		Vector3 vec = playerManager.transform.position - transform.position;
		float distSquared = vec.sqrMagnitude;
		
		float nearingRadiusSquared = NearingRadius * NearingRadius;
		float leavingRadiusSquared = LeavingRadius * LeavingRadius;
		
		if (prevDistSquared >= nearingRadiusSquared && distSquared < nearingRadiusSquared) {
			isNearing = true;
			Debug.Log("Nearing");
			coroutineList.AddItem(OnNearing(isPlayerHeading, isNpcHeading));
		} else {
			isNearing = false;
		}
		
		if (prevDistSquared <= leavingRadiusSquared && distSquared > leavingRadiusSquared) {
			isLeaving = true;
			Debug.Log("Leaving");
			coroutineList.AddItem(OnLeaving(isPlayerHeading, isNpcHeading));
			//TODO: close dialogs
		} else {
			isLeaving = false;
		}
		
		prevDistSquared = distSquared;
		
		//call update in coroutinelist
		coroutineList.MoveToHead();
		while(coroutineList.MoveNext()) {
			CoroutineComponent inst = coroutineList.CurrentYieldInstruction;
			inst.Update();
		}
	}
	
	protected void OnGUI() {
		coroutineList.MoveToHead();
		while(coroutineList.MoveNext()) {	//draw each coroutine GUI elements
			CoroutineComponent inst = coroutineList.CurrentYieldInstruction;
			//check distance
			if(prevDistSquared >= NearingRadius * NearingRadius) {
				coroutineList.RemoveCurrentItem();	//TODO: remote communication?
			} else {
				if(inst.OnGUI()) {
					coroutineList.ResumeExec();
				}
			}
		}
		
		//hit test
		if (coroutineList.IsEmtpy 
		    && InputManager.Controller.isClickOn(collider, NearingRadius)) {
			coroutineList.AddItem(OnClick());
		}
	}
	
	/// <summary>
	/// Show a dialog to display story lines.
	/// </summary>
	/// <param name="text">
	/// A <see cref="System.String"/>
	/// </param>
	/// <returns>
	/// A <see cref="StoryDialog"/>
	/// </returns>
	protected StoryDialog ShowDialog(string text) {
		StoryDialog dialog = new StoryDialog(this, text);
		return dialog;
	}
	
	/// <summary>
	/// Show a dialog along with several options for the user to select. 
	/// </summary>
	/// <param name="text">
	/// A <see cref="System.String"/>, the text to display. Typically it is a question.
	/// </param>
	/// <param name="resultRef">
	/// A <see cref="SelectionResult"/>, the reference to store the result. It MUST NOT be null. The caller should call new SelectionResult() first. 
	/// </param>
	/// <param name="options">
	/// A <see cref="System.String[]"/> 
	/// </param>
	/// <returns>
	/// A <see cref="SelectionStoryDialog"/>
	/// </returns>
	protected SelectionStoryDialog ShowSelectionDialog(string text, SelectionResult resultRef, params string[] options) {
		SelectionStoryDialog dialog = new SelectionStoryDialog(this, text, resultRef, options);
		return dialog;
	}
	
	protected void ShowChatText(string text, Color color) {
		ChatTextContainer.Instance.show(this.Name, text, color);
	}
	
	protected void ShowChatText(string text) {
		ChatTextContainer.Instance.show(this.Name, text, Color.blue);
	}
	
	protected virtual IEnumerator OnNearing(bool isPlayerHeading, bool isNpcHeading) {
		return null;
	}
	
	protected virtual IEnumerator OnLeaving(bool isPlayerHeading, bool isNpcHeading) {
		return null;
	}
	
	protected virtual IEnumerator OnClick() {
		return null;
	}
}
