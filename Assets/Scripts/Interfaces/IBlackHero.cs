/// <summary>
/// Черный монстрик
/// </summary>
public interface IBlackHero{
	void Attack(int count);
	void SetHp (int value);
	void DeleteObject();
	void PrepareDelete();
	void Visible(bool state);
}
