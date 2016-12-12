using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Общие свойства игрового объекта.
/// </summary>
public class Properties : CacheTransform {
	#region Variables
	/// <summary>
	/// The state.
	/// </summary>
	public StateObjects state;
	/// <summary>
	/// Функционал jelly.
	/// </summary>
	public IJelly iJelly;
	/// <summary>
	/// Функционал цвета.
	/// </summary>
	public IColor iColor;
	/// <summary>
	/// Функционал баллов.
	/// </summary>
	public IPoints iPoints;
	/// <summary>
	/// Функционал джема.
	/// </summary>
	public IJam iJam;
	/// <summary>
	/// Функционал кристалла
	/// </summary>
	public IDiamond iDiamond;
	/// <summary>
	/// Функционал BlackHero.
	/// </summary>
	public IBlackHero iBlackHero;
	public IElectro iElectro;
	public IStone iStone;

	public IPuddle iPuddle;


	public IIce iIce;

	public ISnow iSnow;

	public IBomb iBomb;

	public IFeed2 iFeed2;

	public ISlime iSlime;

	public IPrism iPrism;
	


	/// <summary>
	/// Тип объекта
	/// </summary>
	public ObjectTypes type;
	/// <summary>
	/// The speed move.
	/// </summary>
	public float speedMove;
	/// <summary>
	/// The acceleration.
	/// </summary>
	public float acceleration;
	/// <summary>
	/// Смещался ли объект.
	/// </summary>
	public bool isOffseted = false;
	/// <summary>
	/// Должен ли двигаться объект.
	/// </summary>
	public bool isMoving = false;
	/// <summary>
	/// отвечает за то, может ли объект двигаться.
	/// </summary>
	public bool canMove;

	/// <summary>
	/// Возможность выделения объекта
	/// </summary>
	public bool canSelected;
	/// <summary>
	/// Непроходимый
	/// </summary>
	public bool isBypass;
	/// <summary>
	/// Пролетаемый объект.
	/// </summary>
	public bool isPassable;
	/// <summary>
	/// Был ли удален объект
	/// </summary>
	public bool _isDelete = false;
	/// <summary>
	/// The delay delete.
	/// </summary>
	public float delayDelete = 0f;

	private float timeAnimationScore = 0.333f;
	private float startTimeAnimationScore;

	public ObjectTypes createPUs = ObjectTypes.Empty;
	public Colors selectedColor = Colors.Empty;

	/// <summary>
	/// Был ли удален объект
	/// </summary>
	public bool isDelete{
		set{
			_isDelete = value;
		}
		get{
			return _isDelete;
		}
	}

	public bool deleteLine = false;


	/// <summary>
	/// Список точек траектории, по которой смещается объект.
	/// </summary>
	public List<Vector3> pointsTrajectory = new List<Vector3> ();
	/// <summary>
	/// The last position point.
	/// </summary>
	public Vector3 lastPosPoint;
	/// <summary>
	/// Позиция в которую должен сместиться объект.
	/// </summary>
	public Vector3 toPos;

	#endregion

	void Awake()
	{
		iColor = new ColorObject ();
	}

	// Use this for initialization
	void Start () {

	}
	

	/// <summary>
	/// Gets the state.
	/// </summary>
	/// <returns>The state.</returns>
	public StateObjects GetState()
	{
		return state;
	}

	/// <summary>
	/// Sets the state.
	/// </summary>
	/// <param name="state">State.</param>
	public void SetState(StateObjects state)
	{
		this.state = state;
		if(iJelly!=null)
		{
			iJelly.SetStatePicture (state);
		}
		else if(iPuddle != null)
		{
			iPuddle.Active(state);
		}
		else if(iBomb!=null)
		{
			((IBomb)iBomb).Active(state);
		}
		else if(iElectro!=null)
		{
			((IElectro)iElectro).Active(state);
		}
		else if(iPrism!=null)
		{
			((IPrism)iPrism).Active(state);
		}
	}

	/// <summary>
	/// Gets the type object.
	/// </summary>
	/// <returns>The type object.</returns>
	public ObjectTypes GetTypeObject()
	{
		return type;
	}

	/// <summary>
	/// Sets the type object.
	/// </summary>
	/// <param name="type">Type.</param>
	public void SetTypeObject(ObjectTypes type)
	{
		this.type = type;
	}

	/// <summary>
	/// Move this instance.
	/// </summary>
	public void Move()
	{
		if (pointsTrajectory.Count > 0) 
		{
			if(transform.localPosition.y > pointsTrajectory[0].y)
			{
				transform.localPosition = Vector3.MoveTowards(transform.localPosition, pointsTrajectory[0], speedMove);
				Visible();
			//	speedMove += acceleration;
				Invoke("Move", GamePlay.timePhysics);
			}
			else 
			{
				lastPosPoint = pointsTrajectory[0];
				pointsTrajectory.RemoveAt(0);
				Invoke("Move", GamePlay.timePhysics);
			}
		} 
		else 
		{
			transform.localPosition = lastPosPoint;
			toPos = lastPosPoint;
			isOffseted = false;
			isMoving = false;	
			Visible();
		}
	}

	public void AddPoints()
	{
		int countPoint = iPoints.GetPoint();
		if(GamePlay.bonusTime)
		{
//			Debug.Log("Bonus!");
			countPoint*=GameData.multiplyBonusTimePoints;
		}

		GameData.score+=countPoint;
		GamePlay.AddTaskValue(Task.Points, countPoint);
		GameData.starManager.SetFull ();
	}

	#region Animation Score
	
	public void AnimationScore() 
	{
		GamePlay.CreateScoreText (this);
		startTimeAnimationScore = Time.time;
		WaitAnimationScore ();
	}
	
	private void WaitAnimationScore()
	{
		if(Time.time - startTimeAnimationScore < timeAnimationScore)
		{
			Invoke("WaitAnimationScore", GamePlay.timePhysics);
			return;
		}

		DeleteObject ();
	}
	#endregion

	private void DeleteObject()
	{
		switch(type)
		{
			case ObjectTypes.Jelly: 
				iJelly.DeleteObject();
				break;
			case ObjectTypes.Diamond:
				iDiamond.PrepareDelete();
				break;
			case ObjectTypes.Electro:
				iElectro.DeleteObject();
				break;
			case ObjectTypes.BlackJelly:
				iBlackHero.DeleteObject();
				break;
			case ObjectTypes.Bomb:
				iBomb.DeleteObject();
				break;
			case ObjectTypes.Feed2:	
				iFeed2.PrepareDelete();
				break;
			case ObjectTypes.Slime:	
				iSlime.DeleteObject();
				break;
			case ObjectTypes.Puddle:
				((Puddle)iPuddle).DeleteObject();
				break;
			case ObjectTypes.Prism:
				((Prism)iPrism).DeleteObject();
				break;

//			case ObjectTypes.Ice:
//				iIce.DeleteObject();
//				break;
		}
	}

	/// <summary>
	/// Проверяет и устанавливает возможность видимости объекта
	/// </summary>
	public void Visible()
	{
		switch(type)
		{
			case ObjectTypes.Jelly:
				iJelly.Visible(InField());
				break;
			case ObjectTypes.BlackJelly:
				iBlackHero.Visible(InField());
				break;
			case ObjectTypes.Diamond:
				iDiamond.Visible(InField());
				break;
			case ObjectTypes.StoneJelly:
				iStone.Visible(InField());
				break;
			case ObjectTypes.Puddle:
				iPuddle.Visible(InField());
				break;
			case ObjectTypes.Ice:
				iIce.Visible(InField());
				break;
		}
	}

	/// <summary>
	/// Проверяет и устанавливает возможность выделения объекта
	/// </summary>
	public void ActiveObject(bool state)
	{
		canSelected = state;
	}

	/// <summary>
	/// Проверка находится ли объект на поле
	/// </summary>
	public bool InField()
	{
		return (transform.position.y>= GameField.startPos.y-GameData.distanceBetwObject/2)
			&&(transform.position.y<GameField.startPos.y+GameData.sizeYVisible*GameData.distanceBetwObject);
	}

	public void CreatePUs()
	{
		switch(createPUs)
		{
			case ObjectTypes.Bomb:
				CreatePUsType(createPUs);
				break;
			case ObjectTypes.Electro:
				CreatePUsType(createPUs);
				break;
			case ObjectTypes.Prism:
				CreatePUsType(createPUs);
				break;
		}
	}

	private void CreatePUsType(ObjectTypes type)
	{
		int[] posIJ = GameData.manager.ReturnIJPosObject (this);
		Properties obj = GameField.CreateGameObject (transform.localPosition, GameData.pool.GetObject (type, selectedColor), selectedColor, type, 0);
		GameData.manager.AddObject (posIJ [0], posIJ [1], obj);
	}
	
}
