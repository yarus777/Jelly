using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialManager{
	public bool needStep = false; 

	private int[] numLevelsForTutorial = {1, 2, 4, 5, 7, 10, 15, 21, 31, 41, 45, 51};
	private BaseTutorial[] levelForTutorial = {new FirstTutorial(), new SecondTutorial(), new FourthTutorial(), new FiveTutorial(), 
												new SeventhTutorial(), new TeenTutorial(), new FifteenthTutorial(),
												new TwentyOneTutorial(), new ThirtyOneTutorial(), new FortyOneTutorial(), new FortyFiveTutorial(),
												new FiftiOneTutorial() 
	 										  };

	private BaseTutorial currentTutorial = null;

	public TutorialManager(bool isAchivement = false)
	{

            levelIsTutorial(GameData.numberLoadLevel);
		
	}

	public bool levelIsTutorial(int number)
	{
		int i = 0;
		foreach(int levelNum in numLevelsForTutorial)
		{
			if(levelNum == number)
			{
				currentTutorial = levelForTutorial[i];
				return true;
			}
			i++;
		}

		return false;
	}

	public void StartTutorial()
	{
		if(currentTutorial!=null)
		{
			currentTutorial.StartTutorial();
		}
	}

	public void NextStep()
	{
        Debug.Log("currentTutorial " + currentTutorial);
		if(currentTutorial!=null)
		{
            
			currentTutorial.NextStepTutorial ();
		}
	}

	public void StopTutorial()
	{
//		currentTutorial.ResetTutorial ();
		currentTutorial = null;
	}

	public bool isTutorial()
	{
		return currentTutorial != null;
	}

	public void ResetTutorial()
	{
		if(currentTutorial!=null)
		{
			currentTutorial.ResetTutorial();
		}
	}

	public bool CorrectTutorial(List<Properties> selectedObjects)
	{
		if(currentTutorial!=null)
		{
			if(!currentTutorial.CorrectTutorial(selectedObjects))
			{
				return false;
			}
		}
		return true;
	}

	public bool isBonusTime()
	{
		if(currentTutorial!=null)
		{
			return currentTutorial.isBonusTime ();
		}

		return false;
	}

}
