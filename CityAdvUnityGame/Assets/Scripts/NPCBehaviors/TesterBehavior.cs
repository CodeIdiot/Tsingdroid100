using UnityEngine;
using System.Collections;

public class TesterBehavior : NPCBehavior {
	protected override IEnumerator OnNearing (bool isPlayerHeading, bool isNpcHeading)
	{
		if(!GameData.GetBool("clickedTester")) {
			ShowChatText("我是雕塑, 点我一下");
		} else {
			ShowChatText("Hi，又见面了。你爱吃" + GameData.GetString("food"));
		}
		return null;
	}
	
	protected override IEnumerator OnLeaving (bool isPlayerHeading, bool isNpcHeading)
	{
		if(!GameData.GetBool("clickedTester")) {
			ShowChatText("555不要走不要走");
		} else {
			ShowChatText("我记住你爱吃什么了，88！");
		}
		return null;
	}
	
	protected override IEnumerator OnClick ()
	{
		SelectionResult result = new SelectionResult();
		yield return ShowSelectionDialog("今晚吃什么？", result, "俄式红菜汤、法式烤蜗牛、意式奶油焗面", "白开水、馒头、咸菜", "Skywalker");
		GameData.SetBool("clickedTester", true);
		GameData.SetString("food", result.Title);
		yield return ShowDialog("你好！据说你要吃" + result.Title + "？");
		yield return ShowDialog("长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长");
	}
}
