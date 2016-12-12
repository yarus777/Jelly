using UnityEngine;
/// <summary>
/// Призма
/// </summary>
public interface IPrism {
	void DeleteObject();
	/// <summary>
	/// Удаление бомбы с задержкой
	/// </summary>
	void PrepareDelete();
	
	bool stateActive 
	{
		set;
		get;
	}
	
	void Active(StateObjects state);

	Colors GetColor();

	GameObject GetEffect();

	void AddPosEffect(Properties target);

	void Pause();

	void Resume();

	void RandomColor();

	void SetSpeed(float speed);

	void State(PrismState state);
}
