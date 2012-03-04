using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoroutineList {
	private List<IEnumerator> items;
	private int currentItemIndex;
	
	public CoroutineList() {
		items = new List<IEnumerator>();
		currentItemIndex = -1;
	}
	
	/// <summary>
	/// Move the pointer to the next coroutine item. Return false if no more item.
	/// </summary>
	/// <returns>
	/// A <see cref="System.Boolean"/>
	/// </returns>
	public bool MoveNext() {
		currentItemIndex++;
		return currentItemIndex < items.Count;
	}
	
	/// <summary>
	/// Get current yield instruction of current coroutine item
	/// </summary>
	public CoroutineComponent CurrentYieldInstruction {
		get {return items[currentItemIndex].Current as CoroutineComponent; }
	}
	
	/// <summary>
	/// Add a new coroutine item to the list, and execute it immediately.
	/// </summary>
	/// <param name="item">
	/// A <see cref="IEnumerator"/>
	/// </param>
	public void AddItem(IEnumerator item) {
		if(item == null) {
			return;
		}
		bool result = item.MoveNext();
		if(result) {
			items.Add(item);
		}
	}
	
	/// <summary>
	/// Remove current item from the list and abort its execution
	/// </summary>
	public void RemoveCurrentItem() {
		items.RemoveAt(currentItemIndex);
		currentItemIndex--;
	}
	
	/// <summary>
	/// Resume executing the current coroutine item. Return false if it reaches the end. When the item reaches its end, it is removed from the list, and the pointer will point to its previous element.
	/// </summary>
	public bool ResumeExec() {
		bool result = items[currentItemIndex].MoveNext();
		if(!result) {
			items.RemoveAt(currentItemIndex);
			currentItemIndex--;
			return false;
		}
		return true;
	}
	
	/// <summary>
	/// Move the pointer to head of the list. Call MoveNext to get first element.
	/// </summary>
	public void MoveToHead() {
		currentItemIndex = -1;
	}
	
	public bool IsEmtpy {
		get {return items.Count == 0; }
	}
}
