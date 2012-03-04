using UnityEngine;
using System.Collections;

public class Monster1Behavior : NPCBehavior {

	protected override IEnumerator OnNearing (bool isPlayerHeading, bool isNpcHeading)
	{
		yield return ShowDialog("我本是一个普通的雕塑，呆在美术馆里；");
		yield return ShowDialog("不料却被西面那只不变异怪物控制了，让我不得安宁；");
		yield return ShowDialog("并不是我想要来给这个校园捣乱的呀！");
		yield return ShowDialog("如果要拯救这里，你要打败所有被控制的雕塑，让它们恢复原状；");
		yield return ShowDialog("然后干掉那只变异怪；它就在西边不远处，两条主干道的交点；");
		yield return ShowDialog("太让人伤心了，那本来是一座漂亮的建筑，还来不及让人观赏，就被......");
	}
}
