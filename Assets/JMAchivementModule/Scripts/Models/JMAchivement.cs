using System;

[Serializable]
public class JMAchivement {
	public string description = "";
	public float maxProgress = 0;
	public JMHonor[] honors = null;

	public bool isComplete(float currentProgress) {
		return currentProgress >= maxProgress;
	}

	public void TakeHonors(){
		foreach (JMHonor honor in honors) {
			honor.TakeHorror ();
		}
	}
}
