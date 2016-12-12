using UnityEngine;
using System.Collections;

/// <summary>
/// Управление картой
/// </summary>
public class MapController2 : MonoBehaviour {
	#region Borders
	/// <summary>
	/// Нижний бэк граница
	/// </summary>
 	public SpriteRenderer borderBottom;
	/// <summary>
	/// Верхний бэк граница
	/// </summary>
	public SpriteRenderer borderTop;
	/// <summary>
	/// Нижняя точка граница
	/// </summary>
	private float bottom;
	/// <summary>
	/// Верхняя точка граница
	/// </summary>
	private float top;
	/// <summary>
	/// Нижняя максимальная точка граница
	/// </summary>
	private float bottomFull;
	/// <summary>
	/// Верхняя максимальная точка граница
	/// </summary>
	private float topFull;
	#endregion

	#region CameraMoving
	private float startPosY;
	private float lastPosCamera;
	private float path;
	private float time;
	private float acceleration;
	private bool isScrolled = false;
    public static bool _isInputEnabled = true;

    #endregion


	void Awake(){
		GameData.mapController2 = this;
		
		//вычисление нижней максимальной границы
		bottomFull = borderBottom.transform.position.y - borderBottom.bounds.size.y / 2f + Camera.main.orthographicSize;
		//вычисление верхней максимальной границы
		topFull = borderTop.transform.position.y + borderTop.bounds.size.y / 2f - Camera.main.orthographicSize;
		//вычисление нижней границы возврата
		bottom = borderBottom.transform.position.y + Camera.main.orthographicSize;
		//вычисление верхней границы возврата
		top = borderTop.transform.position.y - Camera.main.orthographicSize;
		//установление позиции камеры
		Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x, bottom , Camera.main.transform.position.z);
	}

    void Start()
    {
        ProgressController.instance.PanelVisibilityChanged += OnAchievementsPanelVisibilityChanged;
    }

    void OnDestroy()
    {
        ProgressController.instance.PanelVisibilityChanged -= OnAchievementsPanelVisibilityChanged;
    }

    private void OnAchievementsPanelVisibilityChanged(bool visibility)
    {
        _isInputEnabled = !visibility;
        //GamePlay.enableButtonInterface = !visibility;
    }

	void Update()
	{
        //Debug.Log("_isInputEnabled " + _isInputEnabled);

	    if (!_isInputEnabled)
	    {
	        return;
	    }

	    if (!GamePlay.enableButtonInterface)

	    {
	        return;
	    }
       
		if(GamePlay.interfaceMap == StateInterfaceMap.Start)
		{
			if(Input.GetMouseButtonDown(0))
			{
				isScrolled = true;
				startPosY = lastPosCamera = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

			}
			if (Input.GetMouseButtonUp (0)) 
			{
				isScrolled = false;
                StartCoroutine(Inersia(Camera.main.ScreenToWorldPoint(Input.mousePosition).y));
            }
			if(Input.GetMouseButton(0))
			{
				Move(startPosY - Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
			}
		}
	}

	/// <summary>
	/// Движение камеры
	/// </summary>
	/// <param name="offset">Offset.</param>
	void Move(float offset)
	{
		if (UpdateApp.stopOther) return;
		if(isScrolled)
		{
			// уменьшение дрожания на планшетах, сброс показателей
			if (Mathf.Abs(offset) < 0.01)
			{
				path = 0;
				time = 0.01f;
				return;
			}
			else
			{
				path+=Mathf.Abs(offset);
				time+=Time.deltaTime;
				lastPosCamera = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
			}
			float newPosY = Camera.main.transform.position.y + offset;
			if (newPosY > topFull) 
			{
				newPosY = topFull;
			} 
			else if(newPosY<bottomFull)
			{
				newPosY = bottomFull;
			}
			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,newPosY,Camera.main.transform.position.z);
		}
	}

	/// <summary>
	/// Инерция камеры
	/// </summary>
	/// <param name="currentPosY">Current position y.</param>
	IEnumerator Inersia(float currentPosY)
	{
		float dirrection = (lastPosCamera - startPosY)/ Mathf.Abs(lastPosCamera - startPosY);
		// 40 - скорость движения
		float offset =  path / time/40 *dirrection;

		float lastTime = Time.time;
		while(Camera.main.transform.position.y-offset<topFull&& Camera.main.transform.position.y-offset>bottomFull&&!isScrolled)
		{

			Camera.main.transform.position -= new Vector3 (0, offset, 0);

			// скорость возврата 20
			offset*= 1 - (Time.time-lastTime)/20;

			if(Mathf.Abs(offset)>0.1)//0.001
			{
				yield return new WaitForSeconds(GamePlay.timePhysics);
			}
			else
			{
				break;
			}
		}

		//плавность 6
		offset = Mathf.Abs (Camera.main.transform.position.y - bottom)/6;
		while(Camera.main.transform.position.y<bottom&&!isScrolled)
		{

			Camera.main.transform.position += new Vector3 (0, offset, 0);
			yield return new WaitForSeconds(GamePlay.timePhysics);
		}

		//плавность 6
		offset = Mathf.Abs (Camera.main.transform.position.y - top)/6;
		while(Camera.main.transform.position.y>top&&!isScrolled)
		{
			
			Camera.main.transform.position -= new Vector3 (0, offset, 0);
			yield return new WaitForSeconds(GamePlay.timePhysics);
		}

		yield return null;
	}

}
