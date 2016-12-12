using UnityEngine;
using System.Collections;

public class TeenTutorial : BaseTutorial {

	public TeenTutorial()
	{
		maxStep = 1;
	}
	
	public override void Step1 ()
	{
		GamePlay.pauseCollider.enabled = false;
		GamePlay.inventoryCollider.enabled = false;
		
		TemplateSelectTutorial (new int[]{38,32,25,19,14,15,22,28,33,39}, true, StatementShadow.Off, StatementShadow.Off, 15f, StringConstants.GetTextTutorial(StringConstants.Level.Teen, 0));
	}

    /*public override void Step2 ()
    {
        ResetTutorial ();
        TemplateShowTutorial (new int[]{27}, StatementShadow.Off, StatementShadow.Off, 12f, StringConstants.GetTextTutorial(StringConstants.Level.Teen, 1), true, true);
        CanSelected (3, 4);
    }
	
    public override void Step3 ()
    {
        ResetTutorial ();
        GamePlay.notDeleteObject = true;
        Properties property = GameData.manager.ReturnObjectOfIJPos (3, 4);
        Debug.Log (property.gameObject);
        GamePlay.SelectedObj (property);


        TemplateShowTutorial (new int[]{27}, StatementShadow.Off, StatementShadow.Off, 4f, StringConstants.GetTextTutorial(StringConstants.Level.Teen, 2), true, true);
		
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
            spriteRenderer.color = new Color(1f,1f,1f,0f);
        }
		
//		GamePlay.gameUI.panels[1].color = new Color(1f,1f,1f,1f);
        CanSelected (3, 4);
    }
	
    public override void Step4 ()
    {
        ResetTutorial ();
        GamePlay.notDeleteObject = true;
        Properties property = GameData.manager.ReturnObjectOfIJPos (4, 4);

        Debug.Log (property.gameObject);
        GamePlay.SelectedObj (property);

        TemplateShowTutorial (new int[]{27,28}, StatementShadow.Off, StatementShadow.Off, 4f, StringConstants.GetTextTutorial(StringConstants.Level.Teen, 3), false, true);
        CanSelected (3, 4);
        CanSelected (4, 4);
    }
	
	public override void Step5 ()
	{
		GamePlay.notDeleteObject = false;
		ResetTutorial ();
		TemplatePopupTutorial (true, StatementShadow.Off, StatementShadow.Off, 10, StringConstants.GetTextTutorial(StringConstants.Level.Teen, 4), new Vector2(3.4f,8.5f), false);
	}

	public override void Step6 ()
	{
		GamePlay.notDeleteObject = false;
		ResetTutorial ();
		TemplatePopupTutorial (true, StatementShadow.Off, StatementShadow.Off, 10, StringConstants.GetTextTutorial(StringConstants.Level.Teen, 5), new Vector2(3.4f,8.5f), false);
	}

	public override void Step7 ()
	{
		ResetTutorial ();
		TemplatePopupTutorial (true, StatementShadow.Off, StatementShadow.Off, 10, StringConstants.GetTextTutorial(StringConstants.Level.Teen, 6), new Vector2(3.4f,8.5f), false);
	}
     */

    public void CanSelected(int i, int j)
	{
		Properties property = GameData.manager.ReturnObjectOfIJPos (i, j);
		property.canSelected = false;
	}
}
