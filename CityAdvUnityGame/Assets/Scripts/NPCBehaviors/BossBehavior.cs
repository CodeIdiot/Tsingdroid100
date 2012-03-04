using UnityEngine;
using System.Collections;

public class BossBehavior : NPCBehavior {

	protected override IEnumerator OnClick ()
	{
		yield return ShowDialog("说我搞乱了你们的校园？那来打败我好了，看看你有多大能耐。");
		SelectionResult result = new SelectionResult();
		yield return ShowSelectionDialog("让你尝尝我的厉害！（请点击上面的选择按钮）", result,"打败它","还是先算了");
		
		if (result.Index == 0) {
			yield return ShowDialog("你会后悔的，接招吧！！");
			yield return ShowDialog("............");
			yield return ShowDialog("......");
			yield return ShowDialog("......什么？开发人员表示战斗系统还没做好？！好吧，咱们也打不成了，你还是回去睡觉吧。");
			ShowChatText("开发人员表示最好回到那个奇怪的空间，为什么？我怎么会知道！总之按返回键吧。");
		} else {
			yield return ShowDialog("哈哈哈，还没打就逃了？");
		}
	}
	
	protected override IEnumerator OnNearing (bool isPlayerHeading, bool isNpcHeading)
	{
		ShowChatText("你就是那个到处捣乱的小鬼吗？（提示：点击那只怪物）");
		return null;
	}
}
