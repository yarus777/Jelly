/// <summary>
/// Кристалл.
/// </summary>
public interface IDiamond {
	void DeleteObject();

	void PrepareDelete();
	/// <summary>
	/// Находится ли объект в нижней ячейке
	/// </summary>
	/// <returns><c>true</c> if this instance is last postion j; otherwise, <c>false</c>.</returns>
	bool IsLastPostionJ();

	void Visible(bool state);
}
