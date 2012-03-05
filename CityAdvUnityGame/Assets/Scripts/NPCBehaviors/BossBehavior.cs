using UnityEngine;
using System.Collections;

public class BossBehavior : NPCBehavior {

	protected override IEnumerator OnClick ()
	{
		yield return ShowDialog("I must say I appreciate your courage to reach and face me, but now what can you do to stop me?");
		SelectionResult result = new SelectionResult();
		yield return ShowSelectionDialog("Shall I tell you what is real horror?", result,"Fight now","Maybe later");
		
		if (result.Index == 0) {
			yield return ShowDialog("You will regret your choice, kid.");
			
			yield return ShowDialog("....My combat system is not done yet, so I'll let you go this time, but mind my words, you will not be so lucky forever!");
			ShowChatText("Please hit return to exit");
		} else {
			yield return ShowDialog("I know you kids are weak, run your ass off!");
		}
	}
	
	protected override IEnumerator OnNearing (bool isPlayerHeading, bool isNpcHeading)
	{
		ShowChatText("So you are the kid who has been generating troubles? (Hint: Tab on the monster)");
		return null;
	}
}
