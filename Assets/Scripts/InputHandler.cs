using UnityEngine;
using System.Collections;

public class InputHandler : CacheTransform {
	private Ray ray;
	private RaycastHit raycastHit;

	// Update is called once per frame
	void Update () {
//		#if UNITY_ANDROID || UNITY_IOS
//		if(Input.touchCount == 1)
//		{
//		#endif
			if(GamePlay.isInput)
			{
				if(Input.GetMouseButton(0))
				{
					OnSelect ();
				}
				else if(Input.GetMouseButtonUp(0))
				{
					GamePlay.DeleteObjects();
				}
			}
//		#if UNITY_ANDROID || UNITY_IOS
//		}
//		#endif

	}

	void OnSelect()
	{
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if(Physics.Raycast(ray, out raycastHit))
		{
			GamePlay.AddObjects(raycastHit.transform);
		}
	}
}
