using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct SListArroundObjects {
	public Colors color;
	public List<Properties> list;

	public SListArroundObjects(Colors color, List<Properties> list)
	{
		this.color = color;
		this.list = list;
	}
}
