using UnityEngine;
using System.Collections;

public class FortyOneTutorial : BaseTutorial {

	public FortyOneTutorial()
	{
		maxStep = 4;
	}
	
	public override void Step1 ()
	{
		GamePlay.pauseCollider.enabled = false;
		GamePlay.inventoryCollider.enabled = false;
		TemplateShowTutorial (new int[]{34}, StatementShadow.Off, StatementShadow.Off, 5f, StringConstants.GetTextTutorial(StringConstants.Level.FortyOne, 0), true, true);
	}
	
	public override void Step2 ()
	{
		ResetTutorial ();
//		GameData.manager.SetShadows (StatementShadow.Off, Colors.Empty);
		TemplateSelectTutorial (new int[]{31,32,33,34}, false, StatementShadow.Off, StatementShadow.Off,6f, StringConstants.GetTextTutorial(StringConstants.Level.FortyOne, 1));
	}
	
	public override void Step3()
	{
		TemplatePopupTutorial (true, StatementShadow.Off, StatementShadow.Off, 10, StringConstants.GetTextTutorial(StringConstants.Level.FortyOne, 2), new Vector2 (-3.4f, 8.5f), false);
	}

	public override void Step4()
	{
		TemplatePopupTutorial (true, StatementShadow.Off, StatementShadow.Off, 10, StringConstants.GetTextTutorial(StringConstants.Level.FortyOne, 3), new Vector2 (-3.4f, 8.5f), false);
	}
}
