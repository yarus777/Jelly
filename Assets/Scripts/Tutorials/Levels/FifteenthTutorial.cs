using UnityEngine;
using System.Collections;

public class FifteenthTutorial : BaseTutorial {

	public FifteenthTutorial()
	{
		maxStep = 3;
	}
	
	public override void Step1 ()
	{
		GamePlay.pauseCollider.enabled = false;
		GamePlay.inventoryCollider.enabled = false;
		TemplateShowTutorial (new int[]{16,17,18}, StatementShadow.Off, StatementShadow.Off, 14f, StringConstants.GetTextTutorial(StringConstants.Level.Fifteen, 0), false, true);
	}
	
	public override void Step2 ()
	{
		ResetTutorial ();
		TemplateSelectTutorial (new int[]{21,22,23}, false, StatementShadow.Off, StatementShadow.Off, 14, StringConstants.GetTextTutorial(StringConstants.Level.Fifteen, 1));
		TemplateShowTutorial (new int[]{16,17,18, 21,22,23}, StatementShadow.Off, StatementShadow.Off, 12f, StringConstants.GetTextTutorial(StringConstants.Level.Fifteen, 0), false, false);
	}

	public override void Step3()
	{
		TemplatePopupTutorial (true, StatementShadow.Off, StatementShadow.Off, 10, StringConstants.GetTextTutorial(StringConstants.Level.Fifteen, 2), new Vector2(0,8.5f), false);
	}
}
