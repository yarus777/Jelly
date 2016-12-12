/// <summary>
/// Объект из редактора. Хранит 2 объекта(основной слой, background), цвет, здоровье background объекта, нужно ли создавать объект
/// </summary>
public struct SParseObject {
	/// <summary>
	/// Основной слой
	/// </summary>
	public ObjectTypes gObject;
	/// <summary>
	/// Background
	/// </summary>
	public ObjectTypes background;
	public Colors color;

	/// <summary>
	/// Здоровье объекта
	/// </summary>
	public int hpObject;
	/// <summary>
	/// Здоровье background.
	/// </summary>
	public int hpBackground;
	/// <summary>
	/// Нужно ли создавать объект(если в редакторе пустая строка, объект не создается)
	/// </summary>
	public bool needCreated;

}
