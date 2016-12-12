/// <summary>
/// Бомба
/// </summary>
public interface IBomb {
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
}
