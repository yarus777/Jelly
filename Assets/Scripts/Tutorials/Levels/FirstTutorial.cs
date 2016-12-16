using UnityEngine;

public class FirstTutorial : BaseTutorial {
    public FirstTutorial() {
        maxStep = 6;
    }

    public override void Step1() {
        TemplateSelectTutorial(new[] {17, 12, 7}, false, StatementShadow.Off, StatementShadow.Off, 14,
            StringConstants.GetTextTutorial(StringConstants.Level.First, 0));
    }

    public override void Step2() {
        TemplateSelectTutorial(new[] {10, 6, 7, 8, 14}, false, StatementShadow.Off, StatementShadow.Off, 14,
            StringConstants.GetTextTutorial(StringConstants.Level.First, 1));
    }

    public override void Step3() {
        TemplatePopupTutorial(false, StatementShadow.Off, StatementShadow.Off, 10,
            StringConstants.GetTextTutorial(StringConstants.Level.First, 2), new Vector2(-3.4f, 8.5f), true);
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

    public override void Step4() {
        TemplatePopupTutorial(true, StatementShadow.Off, StatementShadow.Off, 10,
            StringConstants.GetTextTutorial(StringConstants.Level.First, 4), new Vector2(3.4f, 8.5f), true);
        GamePlay.gameUI.score.color = new Color(GamePlay.gameUI.score.color.r,
            GamePlay.gameUI.score.color.g,
            GamePlay.gameUI.score.color.b,
            1f);
        GamePlay.gameUI.nameScore.color = new Color(GamePlay.gameUI.nameScore.color.r,
            GamePlay.gameUI.nameScore.color.g,
            GamePlay.gameUI.nameScore.color.b,
            1f);

//		GamePlay.gameUI.panels[2].color = new Color(1f,1f,1f,1f);
    }

    public override void Step5() {
        TemplatePopupTutorial(true, StatementShadow.Off, StatementShadow.Off, 10,
            StringConstants.GetTextTutorial(StringConstants.Level.First, 3), new Vector2(0f, 7.5f), true);
        foreach (var spriteRenderer in GamePlay.gameUI.stars.GetComponentsInChildren<SpriteRenderer>()) {
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }

//		GamePlay.gameUI.panels[1].color = new Color(1f,1f,1f,1f);
    }

    public override void Step6() {
        TemplatePopupTutorial(true, StatementShadow.Off, StatementShadow.Off, 10,
            StringConstants.GetTextTutorial(StringConstants.Level.First, 5), new Vector2(-3.4f, 8.5f), true);
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