public class FourthTutorial : BaseTutorial {
    public FourthTutorial() {
        maxStep = 2;
    }

    public override void Step1() {

        TemplateSelectTutorial(new[] {14, 19, 23, 17, 12}, true, StatementShadow.Off, StatementShadow.Off, 2.27f,
            StringConstants.GetTextTutorial(StringConstants.Level.Fourth, 0));
    }

    public override void Step2() {
        TemplateSelectTutorial(new[] {12, 7, 1}, false, StatementShadow.Off, StatementShadow.Off, 14,
            StringConstants.GetTextTutorial(StringConstants.Level.Fourth, 1));
    }

    /*public override void Step3 ()
	{
		TemplatePopupTutorial (false, StatementShadow.Off, StatementShadow.Off, 10, StringConstants.GetTextTutorial(StringConstants.Level.Fourth,2), new Vector2(53.4f,8.5f), false);
	}

	public override void Step4 ()
	{
		TemplatePopupTutorial (true, StatementShadow.Off, StatementShadow.Off, 10, StringConstants.GetTextTutorial(StringConstants.Level.Fourth,3), new Vector2(3.4f,8.5f), false);
	}

	public override void Step5 ()
	{
		TemplatePopupTutorial (true, StatementShadow.Off, StatementShadow.Off, 10, StringConstants.GetTextTutorial(StringConstants.Level.Fourth,4), new Vector2(3.4f,8.5f), false);
	}*/
}