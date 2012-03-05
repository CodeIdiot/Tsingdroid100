using UnityEngine;
using System.Collections;

public class Monster1Behavior : NPCBehavior {

	protected override IEnumerator OnNearing (bool isPlayerHeading, bool isNpcHeading)
	{
		yield return ShowDialog("I used to be just a sculpture, all my dream was to be appreciated in the museum.");
		yield return ShowDialog("But a monster controlled me and changed me.");
		yield return ShowDialog("I never meant to disturb other's life, please rescue me.");
		yield return ShowDialog("If you want to save your campus, you need to remove the spell on us and defeat the monster.");
		yield return ShowDialog("The monster now possesses your new building as its form.");
		yield return ShowDialog("It's the one on the west close to the main street. ");
		yield return ShowDialog("Such a pity that it used to be a beautiful building and was meant for the celebration.");
	}
}
