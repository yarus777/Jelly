using UnityEngine;
using System.Collections;

public class SecondTutorial: BaseTutorial {

	public SecondTutorial()
	{
		maxStep = 2;
	}
	
	public override void Step1()
	{
		GamePlay.pauseCollider.enabled = false;
		GamePlay.inventoryCollider.enabled = false;

		TemplatePopupTutorial (false, StatementShadow.Off, StatementShadow.Off, 10, StringConstants.GetTextTutorial(StringConstants.Level.Second,0), new Vector2(0,8.5f), true);

		GamePlay.gameUI.nameLimit.color = new Color(GamePlay.gameUI.nameLimit.color.r,
		                                            GamePlay.gameUI.nameLimit.color.g,
		                                            GamePlay.gameUI.nameLimit.color.b,
		                                            1f);
		GamePlay.gameUI.countLimit.color = new Color(GamePlay.gameUI.countLimit.color.r,
		                                             GamePlay.gameUI.countLimit.color.g,
		                                             GamePlay.gameUI.countLimit.color.b,
		                                             1f);

		foreach(SpriteRenderer spriteRenderer in GamePlay.gameUI.stars.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.color = new Color(1f,1f,1f,1f);
		}

//		GamePlay.gameUI.panels[1].color = new Color(1f,1f,1f,1f);
	}

	public override void Step2()
	{
		TemplatePopupTutorial (true, StatementShadow.Off, StatementShadow.Off, 10, StringConstants.GetTextTutorial(StringConstants.Level.Second,1), new Vector2(0,8.5f), false);
       /* foreach (SpriteRenderer spriteRenderer in GamePlay.gameUI.stars.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }*/
    }
}
