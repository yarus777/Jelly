/// <summary>
/// Очки
/// </summary>
public interface IPoints {
	int GetPoint();
	/// <summary>
	/// Количество бонус очков
	/// </summary>
	/// <param name="bonusPoint">Bonus point.</param>
	void SetBonusPoint(int count);
}
