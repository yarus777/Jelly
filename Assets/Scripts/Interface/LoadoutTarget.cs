using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadoutTarget : MonoBehaviour {
	public List<Sprite> iconsAll;
	public SpriteRenderer icon;
	public TextMesh targetName;
	public TextMesh targetCount;
	public TextMesh limit;

	void Awake()
	{
		InitTarget ();
	}

	private void InitTarget()
	{
		GameData.parser.ParseLevel (GameData.numberLoadLevel);
		SetIcon (GameData.parser.levelType);
		string targetText = GetTargetName (GameData.parser.levelType);
		string countTarget = GetTargetCount (GameData.parser.levelGoal)
							 +GetLimit (GameData.parser.limitType, GameData.parser.countLimit);

		targetName.text = targetText;
		targetCount.text = countTarget;
	}

	public void SetIcon(Task task)
	{
		icon.sprite = iconsAll[((int)task)-1];
	}

	public string GetTargetName(Task task)
	{
		string targetName = "";
		switch(task)
		{
			case Task.Points:
				//targetName = "Reach points:";
				targetName = StringConstants.GetText(StringConstants.TextType.ReachPoints)+":";
			
				break;
			case Task.Save:
				//targetName = "Save jelly:";
				targetName = StringConstants.GetText(StringConstants.TextType.ReachSaveJelly)+":";
				break;
			case Task.ClearJam:
				//targetName = "Clean chocolate:";
				targetName = StringConstants.GetText(StringConstants.TextType.ReachCleanChocolate)+":";
				break;
			case Task.Diamond:
				//targetName = "Lower pots:";
				targetName = StringConstants.GetText(StringConstants.TextType.ReachLowerPot)+":";
				break;
			case Task.Feed1:
				//targetName = "Fill the bags:";
				targetName = StringConstants.GetText(StringConstants.TextType.ReachFillBags)+":";
				break;
			case Task.Feed2:
				//targetName = "Fill the bucket:";
				targetName = StringConstants.GetText(StringConstants.TextType.ReachFillBucket)+":";
				break;
			case Task.Dig:
				//targetName = "Clean cookies:";
				targetName = StringConstants.GetText(StringConstants.TextType.ReachCleanCoockie)+":";
				break;
		}

		return targetName;
	}

	public string GetTargetCount(int count)
	{
		string targetCount = StringConstants.GetText(StringConstants.TextType.Count)+": "+count.ToString();
		return targetCount;
	}

	public string GetLimit(Limit name, int count)
	{
		string limit = "";
		switch(name)
		{
			case Limit.Moves:
				limit = "\n"+StringConstants.GetText(StringConstants.TextType.Moves)+": "+count.ToString();
				break;
		}
		return limit;
	}
}
