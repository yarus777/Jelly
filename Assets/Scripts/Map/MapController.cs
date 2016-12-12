//#define BuyLife

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Text;

public class MapController : MonoBehaviour {

//	private float startZoom = 17f;
//	private int minWidth = 480;
//	private int minHeight = 800;
	//private Vector3 startPosCam;
	private Camera activeCamera;

	private float zoneScaleW = 23.4f;
	private float zoneScaleH = 31.1555f;

//	private float centerZoneX = 0;
	private float centerZoneY = -20.87485f;

	private float speedMove = 10f;

//	private float cafWH;

//	private Vector2 delta = Vector2.zero;

//	private float halfW;
//	private float halfH;

	public List<Transform> levels;

	public RuntimeAnimatorController activeButtonController;
    public GameObject buttonsParent;
    public enum EStateLocation
	{
		Kitchen,
		Glade,
	}

	//movedown inersia
	private Vector3 startPosD;
	private Vector3 toPosD;
//	private float speedD = 5f;
	private float startTimeD;

//	private bool isInersia = false;
//	private float duration = 0;

	void Awake()	
	{

        //Debug.Log(Application.bundleIdentifier);
		GamePlay.mapController = this;

		OrderObjects.AddObjects ();


		//start
		speedMove *= Camera.main.aspect;
		activeCamera = Camera.main;
		CameraSize ();
		
		//startPosCam = new Vector3 (0, centerZoneY+activeCamera.orthographicSize,-10);
//		GamePlay.interfaceMap = StateInterfaceMap.Start;
		
		Input.multiTouchEnabled = false;
		
		GamePlay.EnableButtonsMap(true);

		GamePlay.stateUI = EUI.Map;

        ButtonNumber[] gO = buttonsParent.GetComponentsInChildren<ButtonNumber>();
        levels = new List<Transform>();
        foreach (ButtonNumber g in gO)
        {
            levels.Add(g.transform);
        }


    }

	// Use this for initialization
	void Start () {


		int lastLevel = PlayerPrefs.GetInt ("lastOpenLevel",0);
		//Debug.Log ("lastOpenLevel: " + lastLevel);
		if(lastLevel<=5)
		{
			//startPosCam = new Vector3 (0, centerZoneY+activeCamera.orthographicSize,-10);
		}
		else if(lastLevel>6)
		{
			activeCamera.transform.position =new Vector3(0,levels[lastLevel-1].position.y,-20);
		}

		Transform activeButton;
        if (lastLevel != 0)
            activeButton = levels[lastLevel - 1];
        else
            activeButton = levels[0];
  //      if (lastLevel >= GameData.allLevels)
  //{
  //	activeButton = levels [lastLevel-1];
  //}
  //else
  //{

            //}
        if (activeButton.GetComponent<ButtonNumber>().state != ButtonNumber.EStateButton.Disable)
		{
			activeButton.gameObject.AddComponent<Animator> ();
			activeButton.GetComponent<Animator> ().runtimeAnimatorController = activeButtonController;
		}
		else
		{
			activeButton = levels [lastLevel - 1];
			activeButton.gameObject.AddComponent<Animator> ();
			activeButton.GetComponent<Animator> ().runtimeAnimatorController = activeButtonController;
		}
	}

	void OnGUI()
	{
#if BuyLife
		if(GUI.Button(new Rect(Screen.width-50,300,50,50), "+Life"))
		{
			GamePlay.ChangeCountLife(1);
			PlayerPrefs.SetInt("countLife",GamePlay.currentCountLife);
			GamePlay.mapInterface.UpdateLife();
		}
#endif
	}

	private void Move(Touch touch)
	{
		if (
			activeCamera.transform.position.y - touch.deltaPosition.y*speedMove/activeCamera.pixelHeight*activeCamera.orthographicSize >= centerZoneY+activeCamera.orthographicSize
		    &&
			activeCamera.transform.position.y - touch.deltaPosition.y*speedMove/activeCamera.pixelHeight*activeCamera.orthographicSize <= centerZoneY+zoneScaleH*4-activeCamera.orthographicSize
		    )
		{
//			activeCamera.transform.position = new Vector3 (activeCamera.transform.position.x, activeCamera.ScreenToWorldPoint(touch.position).y, activeCamera.transform.position.z);
			activeCamera.transform.Translate(0, - touch.deltaPosition.y*speedMove/activeCamera.pixelHeight*activeCamera.orthographicSize, 0);
//			duration = -(touch.deltaPosition.y/touch.deltaPosition.y);
		}
	}

	private void Move(float offset)
	{
		if (
			activeCamera.transform.position.y - offset*0.5f >= centerZoneY+activeCamera.orthographicSize
			&&
			activeCamera.transform.position.y - offset*0.5f <= centerZoneY+zoneScaleH*4-activeCamera.orthographicSize
			)
		{
			activeCamera.transform.position -= new Vector3 (0, offset*0.5f, 0);
		}
	}

	private void CameraSize()
	{

		float ortographicSize = activeCamera.orthographicSize;
		float aspect = Camera.main.aspect;
		float sizeUnitX = ortographicSize*aspect *2;
		float coefUnits = zoneScaleW/sizeUnitX;
		activeCamera.orthographicSize *= coefUnits;
	}

	
}
