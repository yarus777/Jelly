/// <summary>
/// Электро монстрик
/// </summary>
public interface IElectro{
	void DeleteObject();
	/// <summary>
	/// Удаление монстрика с задержкой
	/// </summary>
	void PrepareDelete();

	bool stateActive 
	{
		set;
		get;
	}

	void Active(StateObjects state);
}
