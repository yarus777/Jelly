using UnityEngine;
using System.Collections;

public class FiftiOneTutorial : BaseTutorial {

	public FiftiOneTutorial()
	{
		maxStep = 5;
	}
	
	public override void Step1 ()
	{
		GamePlay.pauseCollider.enabled = false;
		GamePlay.inventoryCollider.enabled = false;

		TemplateShowTutorial (new int[]{20}, StatementShadow.Off, StatementShadow.Off, 10f, StringConstants.GetTextTutorial(StringConstants.Level.FiftiOne, 0), true, true);
	}
	
	public override void Step2 ()
	{
		ResetTutorial ();
		TemplateShowTutorial (new int[]{20}, StatementShadow.Off, StatementShadow.Off, 10f, StringConstants.GetTextTutorial(StringConstants.Level.FiftiOne, 1), true, true);
	}
	
	public override void Step3()
	{
		ResetTutorial ();
		TemplateSelectTutorial (new int[]{25,26,27}, false, StatementShadow.Off, StatementShadow.Off, 14, StringConstants.GetTextTutorial(StringConstants.Level.FiftiOne, 2));
	}

	public override void Step4()
	{
		TemplatePopupTutorial (true, StatementShadow.Off, StatementShadow.Off, 10, StringConstants.GetTextTutorial(StringConstants.Level.FiftiOne, 3), new Vector2(0,8.5f), false);
	}

	public override void Step5()
	{
		TemplatePopupTutorial (true, StatementShadow.Off, StatementShadow.Off, 10, StringConstants.GetTextTutorial(StringConstants.Level.FiftiOne, 4), new Vector2(0,8.5f), false);
	}
}
