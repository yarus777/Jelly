using UnityEngine;
using System.Collections;

public static class ConstantSort{

	public static int GetSort(OrderLayers layer)
	{
		return ((int)layer)*(-1);
	}
}
