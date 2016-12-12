using UnityEngine;
using System;

[Serializable]
public class JMAchivementPack {
	public string id = "";
	public string name = "";
	public JMAchivement[] jmAchivements = null;
	private float currentProgress = 0;
	public int countTakeHonor = 0;

	public void SetAchivement(string id){
		this.id = id;
	}

	public float GetCurrentProgress(){
		return currentProgress;
	}

	public bool IsAchivementPack(){
		return jmAchivements.Length > countTakeHonor;
	}

	public int CountNeedTakeHonor(){
		return CountStars() - countTakeHonor;
	}

	public string GetName(){
		return name;
	}

	public string GetDescriptionText(){
		return jmAchivements [countTakeHonor].description;
	}

	public int CountStars(){
		int count = 0;
		foreach (JMAchivement achivement in jmAchivements) {
			if (achivement.isComplete (currentProgress)) {
				count++;
			}
		}
		return count;
	}

	public void AddProgress(float count){
		currentProgress += count;
		PlayerPrefs.SetFloat ("jmAchivementProgress_" + id, currentProgress);
	}

	public void SetProgress(float count){
		currentProgress = count;
		PlayerPrefs.SetFloat ("jmAchivementProgress_" + id, currentProgress);
	}

	public void TakeHonor(){
		jmAchivements [countTakeHonor].TakeHonors ();
		countTakeHonor++;
		PlayerPrefs.SetInt ("jmAchivementHonnors_" + id, countTakeHonor);
	}

	public string GetAchivementProgressText(){
		if (currentProgress > jmAchivements [countTakeHonor].maxProgress) {
			return jmAchivements [countTakeHonor].maxProgress + "/" + jmAchivements [countTakeHonor].maxProgress;
		} 
		else {
			return currentProgress + "/" + jmAchivements [countTakeHonor].maxProgress;
		}
	}

	public float GetAchivementProgress(){
		if (currentProgress > jmAchivements [countTakeHonor].maxProgress) {
			return 1f;
		} 
		else {
			return currentProgress / jmAchivements [countTakeHonor].maxProgress;
		}
	}
}
