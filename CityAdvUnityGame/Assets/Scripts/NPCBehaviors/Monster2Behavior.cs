using UnityEngine;
using System.Collections;

public class Monster2Behavior : NPCBehavior {

	protected override IEnumerator OnNearing (bool isPlayerHeading, bool isNpcHeading)
	{
		yield return ShowDialog("RAWR!!!!!!!!!!!!! (He is apparently in great agony)");
	}
}
