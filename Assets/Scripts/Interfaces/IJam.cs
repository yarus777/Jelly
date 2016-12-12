/// <summary>
/// Джем
/// </summary>
public interface IJam{
	void PrepareDelete();
	/// <summary>
	/// Изменить состояние джема(тип картинки)
	/// </summary>
	/// <param name="typeImg">Type image.</param>
	void SetTypeImage(Jams typeImg);
	/// <summary>
	/// Изменить картинку для джема
	/// </summary>
	void ChangeImage();
}
