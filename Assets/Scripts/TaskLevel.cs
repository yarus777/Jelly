using UnityEngine;
using System.Collections;

public class TaskLevel{
	private Task task;
	private int current;
	private int goal;

	public TaskLevel(Task task, int goal)
	{
		this.task = task;
		this.goal = goal;
		current = 0;
	}

	public bool EndTask()
	{

		return current >= goal;
	}

	public Task GetTaskType()
	{
		return task;
	}

	public int GetGoal()
	{
		return goal;
	}

	public int GetCurrent()
	{
		return current;
	}

	public void SetCurrent(int count)
	{
		current += count;
		if(current>goal)
		{
			current = goal;
		}
	}

	public string NameTask()
	{
		switch(task)
		{
			case Task.Points:
				//return "Score: ";
				return StringConstants.GetText(StringConstants.TextType.Points).ToLower();
			case Task.Save:
				//return "Save: ";
			return StringConstants.GetText(StringConstants.TextType.Save).ToLower();
			case Task.ClearJam:
				//return "Jams: ";
			return StringConstants.GetText(StringConstants.TextType.Chocolate).ToLower();
			case Task.Diamond:
				//return "Pot down: ";
			return StringConstants.GetText(StringConstants.TextType.Pot).ToLower();
			case Task.Feed1:
				//return "Feed 1: ";
			return StringConstants.GetText(StringConstants.TextType.Bags).ToLower();
			case Task.Feed2:
				//return "Feed 2: ";
			return StringConstants.GetText(StringConstants.TextType.Bucket).ToLower();
			case Task.Dig:
				//return "Dig string: ";
			return StringConstants.GetText(StringConstants.TextType.Cookie).ToLower();
			default:
				return "";
		}
	}
}
