using UnityEngine;
using System.Collections;

public class Monster2Behavior : NPCBehavior {

	protected override IEnumerator OnNearing (bool isPlayerHeading, bool isNpcHeading)
	{
		yield return ShowDialog("嗷嗷嗷嗷嗷嗷～～～～～");
	}
}
