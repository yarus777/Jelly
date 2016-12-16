using UnityEngine;

public class TwentyOneTutorial : BaseTutorial {
    public TwentyOneTutorial() {
        maxStep = 3;
    }

    public override void Step1() {
        TemplateSelectTutorial(new[] {12, 7, 2}, false, StatementShadow.Off, StatementShadow.Off, 14,
            StringConstants.GetTextTutorial(StringConstants.Level.TwentyOne, 0));
    }

    public override void Step2() {
        TemplatePopupTutorial(false, StatementShadow.Off, StatementShadow.Off, 8,
            StringConstants.GetTextTutorial(StringConstants.Level.TwentyOne, 1), new Vector2(-3.4f, 8.5f), true);
        TemplateShowTutorial(new[] {37}, StatementShadow.Off, StatementShadow.Off, 8f,
            StringConstants.GetTextTutorial(StringConstants.Level.TwentyOne, 2), true, false);

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

    public override void Step3() {
        ResetTutorial();
        TemplatePopupTutorial(false, StatementShadow.Off, StatementShadow.Off, 10,
            StringConstants.GetTextTutorial(StringConstants.Level.TwentyOne, 2), new Vector2(-3.4f, 8.5f), false);
    }
}