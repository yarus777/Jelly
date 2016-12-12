using UnityEngine;
using System.Collections;

public class ThirtyOneTutorial : BaseTutorial {

	public ThirtyOneTutorial()
	{
		maxStep = 3;
	}

	public override void Step1 ()
	{
		GamePlay.pauseCollider.enabled = false;
		GamePlay.inventoryCollider.enabled = false;
		TemplateSelectTutorial (new int[]{20,21,22,23,24}, false, StatementShadow.Off, StatementShadow.Off,6, StringConstants.GetTextTutorial(StringConstants.Level.ThirtyOne, 0));
	}
	
	public override void Step2 ()
	{
		ResetTutorial ();
		TemplateSelectTutorial (new int[]{10,11,12,13,14}, false, StatementShadow.Off, StatementShadow.Off,12, StringConstants.GetTextTutorial(StringConstants.Level.ThirtyOne, 1));
	}
	
	public override void Step3()
	{
		TemplatePopupTutorial (true, StatementShadow.Off, StatementShadow.Off, 10, StringConstants.GetTextTutorial(StringConstants.Level.ThirtyOne, 2), new Vector2 (-3.4f, 8.5f), false);
	}
}
