using UnityEngine;
using System.Collections;

public class ParentObject: CacheTransform {

	public ParentObject()
	{
		GameField.parentObject = this;
	}
}
