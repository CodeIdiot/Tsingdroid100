using UnityEngine;
using System.Collections;

public class TesterBehavior : NPCBehavior {
	protected override IEnumerator OnNearing (bool isPlayerHeading, bool isNpcHeading)
	{
		if(!GameData.GetBool("clickedTester")) {
			ShowChatText("I'm a sculpture. Touch me.");
		} else {
			ShowChatText("Hi，nice to meet you again! You like " + GameData.GetString("food"));
		}
		return null;
	}
	
	protected override IEnumerator OnLeaving (bool isPlayerHeading, bool isNpcHeading)
	{
		if(!GameData.GetBool("clickedTester")) {
			ShowChatText("Don't leave me!");
		} else {
			ShowChatText("I've remembered what you like. Bye!");
		}
		return null;
	}
	
	protected override IEnumerator OnClick ()
	{
		SelectionResult result = new SelectionResult();
		yield return ShowSelectionDialog("What do you like?", result, "Gamers", "Programmers", "Artists");
		GameData.SetBool("clickedTester", true);
		GameData.SetString("food", result.Title);
		yield return ShowDialog("You said you like " + result.Title + "?");
	}
}
