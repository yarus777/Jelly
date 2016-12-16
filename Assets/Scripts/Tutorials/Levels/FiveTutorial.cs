public class FiveTutorial : BaseTutorial {
    public FiveTutorial() {
        maxStep = 2;
    }

    public override void Step1() {
        TemplateSelectTutorial(new[] {30, 25, 20, 15, 16, 17, 22}, true, StatementShadow.Off, StatementShadow.Off, 4.2f,
            StringConstants.GetTextTutorial(StringConstants.Level.Five, 0));
    }

    public override void Step2() {
        TemplateSelectTutorial(new[] {24, 23, 17}, false, StatementShadow.Off, StatementShadow.Off, 4.2f,
            StringConstants.GetTextTutorial(StringConstants.Level.Five, 1));
    }

    /*public override void Step3 ()
	{
		TemplatePopupTutorial (false, StatementShadow.Off, StatementShadow.Off, 10, StringConstants.GetTextTutorial(StringConstants.Level.Five,2), new Vector2(3.4f,8.5f), false);
	}

	public override void Step4 ()
	{
		TemplatePopupTutorial (true, StatementShadow.Off, StatementShadow.Off, 10, StringConstants.GetTextTutorial(StringConstants.Level.Five,3), new Vector2(3.4f,8.5f), false);
	}
	
	public override void Step5 ()
	{
		TemplatePopupTutorial (true, StatementShadow.Off, StatementShadow.Off, 10, StringConstants.GetTextTutorial(StringConstants.Level.Five,4), new Vector2(3.4f,8.5f), false);
	}*/
}