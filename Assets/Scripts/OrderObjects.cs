using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class OrderObjects{
	public static Dictionary<string, int> objects = new Dictionary<string, int>();

	public static void AddObjects()
	{
		ClearObjects ();
		objects.Add (OrderLayers.Empty.ToString(), 0);
		objects.Add (OrderLayers.TopPowerUp.ToString(), -1);
		objects.Add (OrderLayers.PowerUp.ToString(), -2);
		objects.Add (OrderLayers.BottomPowerUp.ToString(), -3);
		objects.Add (OrderLayers.TopMainObject.ToString(), -4);
		objects.Add (OrderLayers.MainObject.ToString(), -5);
		objects.Add (OrderLayers.BottomMainObject.ToString(), -6);
		objects.Add (OrderLayers.Line.ToString(), -7);
		objects.Add (OrderLayers.TopBackObject.ToString(), -8);
		objects.Add (OrderLayers.BackObject.ToString(), -9);
		objects.Add (OrderLayers.BottomBackObject.ToString(), -10);
		objects.Add (OrderLayers.Grid.ToString(), -11);
		objects.Add (OrderLayers.Background.ToString(), -12);
	}

	public static int GetOrderLayer(OrderLayers layer)
	{
		if(objects.ContainsKey(layer.ToString()))
		{
			return objects[layer.ToString()];
		}
		return 0;
	}

	public static void ClearObjects()
	{
		objects.Clear ();
	}

}
