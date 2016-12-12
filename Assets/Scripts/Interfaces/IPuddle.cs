using UnityEngine;
using System.Collections;

public interface IPuddle {
	void Attack(int count);
	void SetCurHp(int value);
	void SetMaxHp(int value);
	void UpdateCountHp();
	void Visible(bool state);
	void Active (StateObjects state);

	bool stateActive 
	{
		set;
		get;
	}
}
