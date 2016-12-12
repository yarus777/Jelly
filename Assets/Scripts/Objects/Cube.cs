using UnityEngine;
using System.Collections;

/// <summary>
/// Управление кубиком
/// </summary>
public class Cube : CacheTransform {
	private SpriteRenderer spriteRenderer;

	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void Start()
	{
		transform.localPosition = new Vector3 (transform.position.x, transform.position.y, 2);
	}

	public void Visible()
	{
		spriteRenderer.enabled = inField ();
		//spriteRenderer.enabled = false;
	}

	/// <summary>
	/// Проверка находится ли объект на поле
	/// </summary>
	private bool inField()
	{
		return (transform.position.y>= GameField.startPos.y-GameData.distanceBetwObject/2)
			&&(transform.position.y<GameField.startPos.y+GameData.sizeYVisible*GameData.distanceBetwObject-0.5f);
	}
}
