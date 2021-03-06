using System;

[Serializable]
public class JMHonor {
	public delegate void AddHonorDelegate(JMHonor honor);
	public event AddHonorDelegate OnAddHonor;

	public EHonorType type;
	public bool isBoolType = false;
	public float count = 0;

	public void TakeHorror(){
		if (OnAddHonor != null) {
			OnAddHonor (this);
		}
	}
}
