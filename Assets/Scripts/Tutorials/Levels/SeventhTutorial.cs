using UnityEngine;
using System.Collections;

public class SeventhTutorial : BaseTutorial {

	public SeventhTutorial()
	{
		maxStep = 2;
	}
	
	public override void Step1 ()
	{
		GamePlay.pauseCollider.enabled = false;
		GamePlay.inventoryCollider.enabled = false;

		TemplateSelectTutorial (new int[]{7,13,17,11}, false, StatementShadow.Off, StatementShadow.Off, 14f, StringConstants.GetTextTutorial(StringConstants.Level.Seven, 0));
	}

	public override void Step2()
	{
		TemplatePopupTutorial (false, StatementShadow.Off, StatementShadow.Off, 10, StringConstants.GetTextTutorial(StringConstants.Level.Seven, 1), new Vector2(0,8.5f), false);
	}
	

}
