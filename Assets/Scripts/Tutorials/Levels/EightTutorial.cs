using UnityEngine;
using System.Collections;

public class EightTutorial : BaseTutorial {

	public EightTutorial()
	{
		maxStep = 1;
	}
	
	public override void Step1 ()
	{
		GamePlay.pauseCollider.enabled = false;
		GamePlay.inventoryCollider.enabled = false;
		TemplateShowTutorial (new int[]{12,22}, StatementShadow.Off, StatementShadow.Off, 12.5f, StringConstants.GetTextTutorial(StringConstants.Level.Eight, 0), true, true);
	}

}
