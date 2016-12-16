using UnityEngine;

public class FortyFiveTutorial : BaseTutorial {
    public FortyFiveTutorial() {
        maxStep = 6;
    }

    public override void Step1() {
        TemplateShowTutorial(new[] {1, 3, 5, 20}, StatementShadow.Off, StatementShadow.Off, 10f,
            StringConstants.GetTextTutorial(StringConstants.Level.FortyFive, 0), true, true);
    }

    public override void Step2() {
        ResetTutorial();
        TemplateSelectTutorial(new[] {31, 32, 33, 34}, false, StatementShadow.Off, StatementShadow.Off, 6f,
            StringConstants.GetTextTutorial(StringConstants.Level.FortyFive, 1));
    }

    public override void Step3() {
        ResetTutorial();
        TemplateShowTutorial(new[] {26}, StatementShadow.Off, StatementShadow.Off, 4f,
            StringConstants.GetTextTutorial(StringConstants.Level.FortyFive, 2), true, true);
    }

    public override void Step4() {
        ResetTutorial();
        TemplateSelectTutorial(new[] {25, 32, 27}, false, StatementShadow.Off, StatementShadow.Off, 4f,
            StringConstants.GetTextTutorial(StringConstants.Level.FortyFive, 3));
//		TemplateShowTutorial (new int[]{26}, StatementShadow.Off, StatementShadow.Off, 4f, StringConstants.level45 [2], false, false);
    }

    public override void Step5() {
        TemplatePopupTutorial(true, StatementShadow.Off, StatementShadow.Off, 10,
            StringConstants.GetTextTutorial(StringConstants.Level.FortyFive, 4), new Vector2(-3.4f, 8.5f), false);
    }

    public override void Step6() {
        TemplatePopupTutorial(true, StatementShadow.Off, StatementShadow.Off, 10,
            StringConstants.GetTextTutorial(StringConstants.Level.FortyFive, 5), new Vector2(-3.4f, 8.5f), true);
        GamePlay.gameUI.targetName.color = new Color(GamePlay.gameUI.targetName.color.r,
            GamePlay.gameUI.targetName.color.g,
            GamePlay.gameUI.targetName.color.b,
            1f);
        GamePlay.gameUI.targetCount.color = new Color(GamePlay.gameUI.targetCount.color.r,
            GamePlay.gameUI.targetCount.color.g,
            GamePlay.gameUI.targetCount.color.b,
            1f);

//		GamePlay.gameUI.panels[0].color = new Color(1f,1f,1f,1f);
    }
}