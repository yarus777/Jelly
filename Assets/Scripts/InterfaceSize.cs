using UnityEngine;
using System.Collections;

public class InterfaceSize : CacheTransform {
	public float sizeInterfaceX;
	public float sizeInterfaceY;
	
	void Start () {
		SizeWidth ();
	}

	private void SizeWidth()
	{
		float ortographicSize = Camera.main.orthographicSize;
		float aspect = Camera.main.aspect;
		float sizeUnitX = ortographicSize*aspect *2;
		transform.localScale = new Vector3 (sizeUnitX/sizeInterfaceX, sizeUnitX/sizeInterfaceX, 1);
//		Vector3 cameraPos = Camera.main.transform.position;
		if(sizeInterfaceY >0)
		{
			transform.position = new Vector3 (transform.position.x,
			                                  transform.position.y+Camera.main.orthographicSize-sizeInterfaceY*transform.localScale.y/0.7991653f,
			                                  transform.position.z);
		}

	}

    void Update()
    {
        
    }

}
