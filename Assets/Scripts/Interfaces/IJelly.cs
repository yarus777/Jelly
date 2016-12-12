/// <summary>
/// Желе(Герой)
/// </summary>
public interface IJelly{
	/// <summary>
	/// Изменить состояние желе(тип картинки)
	/// </summary>
	/// <param name="state">State.</param>
	void SetStatePicture(StateObjects state);
	/// <summary>
	/// Удаление желе с задержкой
	/// </summary>
	/// <param name="delay">Время до удаления.</param>
	void PrepareDelete();

	void DeleteObject();

	void Visible(bool state);
}
