﻿//#define DebugInfo

using System.Collections.Generic;
using Assets.Scripts.MyScripts.Scenes;
using UnityEngine;

public abstract class BaseTutorial {
    protected int currentStep;
    protected int maxStep = 0;
    protected bool correctLine;
    protected int[] currentLine;
    private bool isAchivements;

    private List<GameObject> fingers = new List<GameObject>();

    protected bool bonusTime;

    public void StartTutorial() {
//		GamePlay.pauseCollider.enabled = false;
        Debug.Log("Start tutorial");
        GamePlay.isTutorialActive = true;
        NextStepTutorial();
    }

    public void NextStepTutorial() {
        StepTutorial();
#if DebugInfo
		Debug.Log ("Step: " +(currentStep+1)+" completed");
#endif
        currentStep++;
        if (currentStep == maxStep + 1) {
            StopTutorial();
        }
    }

    public bool CorrectTutorial(List<Properties> selectedObjects) {
        if (currentLine == null) {
            return true;
        }

        if (selectedObjects.Count != currentLine.Length) {
            return false;
        }
        if (correctLine) {
            var i = 0;
            foreach (var property in selectedObjects) {
                if (!ComparePositionObjects(currentLine[i], property)) {
                    return false;
                }
                i++;
            }
        }
        return true;
    }

    private bool ComparePositionObjects(int number, Properties property) {
        var properties = GameData.manager.GetAllObects(false);
        return properties[number] == property;
    }

    public void StepTutorial() {
        switch (currentStep) {
            case 0:
                Step1();
                break;
            case 1:
                Step2();
                break;
            case 2:
                Step3();
                break;
            case 3:
                Step4();
                break;
            case 4:
                Step5();
                break;
            case 5:
                Step6();
                break;
            case 6:
                Step7();
                break;
            case 7:
                Step8();
                break;
            case 8:
                Step9();
                break;
        }
    }

    public virtual void Step1() {
    }

    public virtual void Step2() {
    }

    public virtual void Step3() {
    }

    public virtual void Step4() {
    }

    public virtual void Step5() {
    }

    public virtual void Step6() {
    }

    public virtual void Step7() {
    }

    public virtual void Step8() {
    }

    public virtual void Step9() {
    }


    private void StopTutorial() {
        ResetTutorial();
        GamePlay.tutorial.StopTutorial();
        GamePlay.isTutorialActive = false;
		Debug.Log("StopTutorial");


        if (bonusTime) {
            bonusTime = false;
            GamePlay.lvlManager.Stroke();
        }
    }

    public void ResetTutorial() {
        if (GamePlay.finger != null) {
            GamePlay.finger.DestroyFinger();
        }

        var allObj = GameData.manager.GetAllObects(true);

        foreach (var property in allObj) {
            if (property.iJam == null) {
                property.canSelected = true;
            }
            if (property.GetComponentInChildren<SpriteRenderer>() != null) {
                property.GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }

            if (property.GetTypeObject() == ObjectTypes.Ice) {
                foreach (var spriteRenderer in property.GetComponentsInChildren<SpriteRenderer>()) {
                    spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
                    spriteRenderer.GetComponent<Animator>().enabled = true;
                }
            }
            if (property.GetTypeObject() == ObjectTypes.Puddle) {
                foreach (var spriteRenderer in property.GetComponentsInChildren<SpriteRenderer>()) {
                    spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
                }
                property.GetComponentInChildren<TextMesh>().color = new Color(1f, 1f, 1f, 1f);
            }

            if (property.GetTypeObject() == ObjectTypes.Feed2) {
                foreach (var spriteRenderer in property.GetComponentsInChildren<SpriteRenderer>()) {
                    spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
                }
                property.GetComponentInChildren<TextMesh>().color = new Color(1f, 1f, 1f, 1f);
            }
        }

        SwitchBack(StatementShadow.On);
//		SwitchGameInterface (StatementShadow.On);
        DestroyTeacher();
        DestroyFinger();

#if DebugInfo
		Debug.Log ("ResetTutorial");
#endif
    }

    //Selected line
    public void TutorialMove(int[] numbersJellyActive) {
        var allObj = GameData.manager.GetAllObects(false);
        foreach (var property in allObj) {
            property.canSelected = false;
            if (property.GetComponentInChildren<SpriteRenderer>() != null) {
                property.GetComponentInChildren<SpriteRenderer>().color = new Color(0.4f, 0.4f, 0.4f, 1f);
            }
        }


        ObjectTypes[] typesSelected = {
            ObjectTypes.Jelly,
            ObjectTypes.Bomb,
            ObjectTypes.Electro,
            ObjectTypes.Prism,
            ObjectTypes.Puddle
//													ObjectTypes.Ice
        };

        foreach (var number in numbersJellyActive) {
            foreach (var type in typesSelected) {
                if (type == allObj[number].GetTypeObject()) {
                    allObj[number].canSelected = true;
                }
            }

            if (allObj[number].GetComponentInChildren<SpriteRenderer>() != null) {
                allObj[number].GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
        }

        allObj = GameData.manager.GetAllObjectsOfType(ObjectTypes.Jam);
        foreach (var property in allObj) {
            if (property.GetComponentInChildren<SpriteRenderer>() != null) {
                property.GetComponentInChildren<SpriteRenderer>().color = new Color(0.4f, 0.4f, 0.4f, 1f);
            }
        }

        allObj = GameData.manager.GetAllObjectsOfTypeWithNull(ObjectTypes.Jam);

        foreach (var number in numbersJellyActive) {
            if (allObj[number] != null) {
                allObj[number].canSelected = true;
                if (allObj[number].GetComponentInChildren<SpriteRenderer>() != null) {
                    allObj[number].GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                }
            }
        }

        allObj = GameData.manager.GetAllObjectsOfType(ObjectTypes.Ice);
        foreach (var property in allObj) {
            foreach (var spriteRenderer in property.GetComponentsInChildren<SpriteRenderer>()) {
                spriteRenderer.GetComponent<Animator>().enabled = false;
                spriteRenderer.color = new Color(0.4f, 0.4f, 0.4f, 1f);
            }
        }

        allObj = GameData.manager.GetAllObjectsOfTypeWithNull(ObjectTypes.Ice);

        foreach (var number in numbersJellyActive) {
            if (allObj[number] != null) {
                foreach (var spriteRenderer in allObj[number].GetComponentsInChildren<SpriteRenderer>()) {
                    spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
                    spriteRenderer.GetComponent<Animator>().enabled = true;
                }
            }
        }

        allObj = GameData.manager.GetAllObjectsOfType(ObjectTypes.Puddle);
        foreach (var property in allObj) {
            foreach (var spriteRenderer in property.GetComponentsInChildren<SpriteRenderer>()) {
//				spriteRenderer.GetComponent<Animator>().enabled = false;
                spriteRenderer.color = new Color(0.4f, 0.4f, 0.4f, 1f);
            }
            property.GetComponentInChildren<TextMesh>().color = new Color(0.4f, 0.4f, 0.4f, 1f);
        }

        allObj = GameData.manager.GetAllObjectsOfTypeWithNull(ObjectTypes.Puddle);

        foreach (var number in numbersJellyActive) {
            if (allObj[number] != null) {
                foreach (var spriteRenderer in allObj[number].GetComponentsInChildren<SpriteRenderer>()) {
                    spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
//					spriteRenderer.GetComponent<Animator>().enabled = true;
                }
                allObj[number].GetComponentInChildren<TextMesh>().color = new Color(1f, 1f, 1f, 1f);
            }
        }

        allObj = GameData.manager.GetAllObjectsOfType(ObjectTypes.Feed2);
        foreach (var property in allObj) {
            foreach (var spriteRenderer in property.GetComponentsInChildren<SpriteRenderer>()) {
                //				spriteRenderer.GetComponent<Animator>().enabled = false;
                spriteRenderer.color = new Color(0.4f, 0.4f, 0.4f, 1f);
            }
            property.GetComponentInChildren<TextMesh>().color = new Color(0.4f, 0.4f, 0.4f, 1f);
        }

        allObj = GameData.manager.GetAllObjectsOfTypeWithNull(ObjectTypes.Feed2);

        foreach (var number in numbersJellyActive) {
            if (allObj[number] != null) {
                foreach (var spriteRenderer in allObj[number].GetComponentsInChildren<SpriteRenderer>()) {
                    spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
                    //					spriteRenderer.GetComponent<Animator>().enabled = true;
                }
                allObj[number].GetComponentInChildren<TextMesh>().color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }

    //createFinger
    public void InitFinger(int[] numbers) {
        GamePlay.finger = MonoBehaviour.Instantiate(Resources.Load<Finger>("Prefabs/Finger"));
        //		GamePlay.finger.transform.position = new Vector3 (0.55f, 5, -1);
        if (!GamePlay.isAnimationFinger) {
            var allObj = GameData.manager.GetAllObects(false);
            var positions = new List<Vector3>();

            foreach (var number in numbers) {
                positions.Add(new Vector3(allObj[number].transform.position.x,
                    allObj[number].transform.position.y,
                    -1f));
            }

            GamePlay.finger.InitCoordinate(positions);
            GamePlay.finger.PlayAnimation();
            GamePlay.isAnimationFinger = true;
        }
    }

    public void InitFinger(int number) {
        fingers.Add(MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Tutorials/finger")));
        fingers[fingers.Count - 1].transform.parent = GamePlay.allObjectManager.transform;
        var allObj = GameData.manager.GetAllObects(false);
        fingers[fingers.Count - 1].transform.localPosition = allObj[number].transform.localPosition;
        fingers[fingers.Count - 1].transform.localPosition += new Vector3(0, -GameData.distanceBetwObject/2, -1);
    }

    public void InitFinger(Vector2 position, Transform parent) {
        fingers.Add(MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Tutorials/finger")));
        Debug.Log("parent: " + parent);
        if (parent != null) {
            fingers[fingers.Count - 1].transform.parent = parent;
        }       
        fingers[fingers.Count - 1].transform.localPosition = new Vector3(position.x, position.y, -3);
        fingers[fingers.Count - 1].transform.localScale = new Vector3(50f,50f,1f);
    }

    public void SwitchBack(StatementShadow state) {
        switch (state) {
            case StatementShadow.On:
                foreach (var image in LoadBg.images)
                {
                    image.color = new Color(1f, 1f, 1f, 1f);
                }
                break;
            case StatementShadow.Off:
                foreach (var image in LoadBg.images) {
                    image.color = new Color(0.4f, 0.4f, 0.4f, 1f);

                }
                break;
        }
    }

    public void SwitchGameInterface(StatementShadow state) {
        switch (state) {
            case StatementShadow.On:
                /*GamePlay.gameUI.score.color = new Color(GamePlay.gameUI.score.color.r,
                    GamePlay.gameUI.score.color.g,
                    GamePlay.gameUI.score.color.b,
                    1f);
                GamePlay.gameUI.nameScore.color = new Color(GamePlay.gameUI.nameScore.color.r,
                    GamePlay.gameUI.nameScore.color.g,
                    GamePlay.gameUI.nameScore.color.b,
                    1f);
                GamePlay.gameUI.nameLimit.color = new Color(GamePlay.gameUI.nameLimit.color.r,
                    GamePlay.gameUI.nameLimit.color.g,
                    GamePlay.gameUI.nameLimit.color.b,
                    1f);
                GamePlay.gameUI.countLimit.color = new Color(GamePlay.gameUI.countLimit.color.r,
                    GamePlay.gameUI.countLimit.color.g,
                    GamePlay.gameUI.countLimit.color.b,
                    1f);
                GamePlay.gameUI.targetName.color = new Color(GamePlay.gameUI.targetName.color.r,
                    GamePlay.gameUI.targetName.color.g,
                    GamePlay.gameUI.targetName.color.b,
                    1f);
                GamePlay.gameUI.targetCount.color = new Color(GamePlay.gameUI.targetCount.color.r,
                    GamePlay.gameUI.targetCount.color.g,
                    GamePlay.gameUI.targetCount.color.b,
                    1f);
                GamePlay.gameUI.back.color = new Color(1f, 1f, 1f, 1f);
                foreach (var spriteRenderer in GamePlay.gameUI.stars.GetComponentsInChildren<SpriteRenderer>()) {
                    spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
                }*/
                break;
            case StatementShadow.Off:
                /*GamePlay.gameUI.score.color = new Color(GamePlay.gameUI.score.color.r,
                    GamePlay.gameUI.score.color.g,
                    GamePlay.gameUI.score.color.b,
                    0.6f);
                GamePlay.gameUI.nameScore.color = new Color(GamePlay.gameUI.nameScore.color.r,
                    GamePlay.gameUI.nameScore.color.g,
                    GamePlay.gameUI.nameScore.color.b,
                    0.6f);
                GamePlay.gameUI.nameLimit.color = new Color(GamePlay.gameUI.nameLimit.color.r,
                    GamePlay.gameUI.nameLimit.color.g,
                    GamePlay.gameUI.nameLimit.color.b,
                    0.6f);
                GamePlay.gameUI.countLimit.color = new Color(GamePlay.gameUI.countLimit.color.r,
                    GamePlay.gameUI.countLimit.color.g,
                    GamePlay.gameUI.countLimit.color.b,
                    0.6f);
                GamePlay.gameUI.targetName.color = new Color(GamePlay.gameUI.targetName.color.r,
                    GamePlay.gameUI.targetName.color.g,
                    GamePlay.gameUI.targetName.color.b,
                    0.6f);
                GamePlay.gameUI.targetCount.color = new Color(GamePlay.gameUI.targetCount.color.r,
                    GamePlay.gameUI.targetCount.color.g,
                    GamePlay.gameUI.targetCount.color.b,
                    0.6f);
                GamePlay.gameUI.back.color = new Color(0.4f, 0.4f, 0.4f, 1f);
                foreach (var spriteRenderer in GamePlay.gameUI.stars.GetComponentsInChildren<SpriteRenderer>()) {
                    spriteRenderer.color = new Color(0.4f, 0.4f, 0.4f, 1f);
                }*/
                break;
        }
    }

    public void LoadTeacher(float y, string text) {
        var obj = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Tutorials/Teacher"));
        obj.transform.position = new Vector3(5.6f, y, -5);
        GamePlay.teacher = obj.GetComponent<TeacherManager>();
        GamePlay.teacher.UpdateText(text);
    }

    public void SetTextTeacher(string text) {
        GamePlay.teacher.UpdateText(text);
    }

    public void DestroyTeacher() {
        if (GamePlay.teacher != null) {
            MonoBehaviour.Destroy(GamePlay.teacher.gameObject);
        }
    }

    public void DestroyFinger() {
        foreach (var finger in fingers) {
            if (finger != null) {
                MonoBehaviour.Destroy(finger);
            }
        }
    }

    public void CreateContinueTap() {
        MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Tutorials/ContinueTap"));
    }

    public void TemplateSelectTutorial(int[] lineSelect, bool stateCorrect, StatementShadow stateBack,
        StatementShadow stateGameUI, float posTeacherY, string textTutorial) {
        currentLine = lineSelect;
        correctLine = stateCorrect;
        InitFinger(currentLine);
        TutorialMove(currentLine);
        SwitchBack(stateBack);
//		SwitchGameInterface (stateGameUI);
        LoadTeacher(posTeacherY, textTutorial);
    }

    public void TemplateShowTutorial(int[] lineSelect, StatementShadow stateBack, StatementShadow stateGameUI,
        float posTeacherY, string textTutorial, bool isFinger, bool isTeacher) {
//		currentLine = lineSelect;
        if (isFinger) {
            foreach (var line in lineSelect) {
                InitFinger(line);
            }
        }
        TutorialMove(lineSelect);
        SwitchBack(stateBack);
//		SwitchGameInterface (stateGameUI);
        if (isTeacher) {
            LoadTeacher(posTeacherY, textTutorial);
            CreateContinueTap();
        }
    }

    public void TemplatePopupTutorial(bool resetTutorial, StatementShadow stateBack, StatementShadow stateGameUI,
        float posTeacherY, string textTutorial, Vector2 positionFinger, bool isFinger) {
        if (resetTutorial) {
            ResetTutorial();
        }
        currentLine = new int[] {};
        TutorialMove(currentLine);
        SwitchBack(stateBack);
//		SwitchGameInterface (stateGameUI);
        LoadTeacher(posTeacherY, textTutorial);
        if (isFinger) {
            InitFinger(new Vector2(positionFinger.x, positionFinger.y), GamePlay.gameUI.gameObject.transform);
            //InitFinger(new Vector2(positionFinger.x, positionFinger.y), GameObject.Find("UICanvas").transform);
            
        }
        CreateContinueTap();
    }

    public bool isBonusTime() {
        return bonusTime;
    }
}