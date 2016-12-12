using UnityEngine;
using System.Collections;

public class ColorObject : IColor {
	public Colors color = Colors.Empty;

	#region IColor implementation
	
	public Colors GetColor ()
	{
		return color;
	}

	public void SetColor (Colors color)
	{
		this.color = color;
	}
	
	#endregion



}
