using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LineManager {
	private List<Line> lines = new List<Line>();
	private float offset = 1;

	public void CreateLineAtPositions(Properties current, Properties last, Colors color)
	{
		Vector3 position = new Vector3((current.transform.localPosition.x + last.transform.localPosition.x)/2, 
		                               (current.transform.localPosition.y + last.transform.localPosition.y)/2, 
		                               current.transform.localPosition.z + offset);
		int[] curIJ = GameData.manager.ReturnIJPosObject (current);
		int[] lastIJ = GameData.manager.ReturnIJPosObject (last);
		Vector2 curA = new Vector2 (curIJ [0], curIJ [1]);
		Vector2 lastB = new Vector2 (lastIJ [0], lastIJ [1]);

		float angle = AngleRotation(curA, lastB);
		CreateLine (position,angle, color);
	}

	public void CreateLine(Vector3 position, float angle, Colors color)
	{
		GameObject go;
		go = MonoBehaviour.Instantiate(GameData.pool.GetObject(ObjectTypes.Line, color)) as GameObject;
		go.transform.parent = GameField.parentObject.transform;
		go.transform.localPosition = position;
		go.transform.Rotate (0, 0, angle);
		lines.Add (go.GetComponent<Line> ());
	}

	public void DeleteLine(int index)
	{
		lines [index].Delete ();
		lines.RemoveAt (index);
	}

	public int Count()
	{
		return lines.Count;
	}

	public void Remove()
	{
		foreach(Line line in lines)
		{
			line.Delete();
		}
		lines.Clear ();
	}

	public float AngleRotation(Vector2 a, Vector2 b)
	{
		float angle = 0f;

		if(a.x != b.x && a.y == b.y)
		{
			angle = 0f;
		}
		else if(a.x == b.x && a.y != b.y)
		{
			angle = 90f;
		}
		else if((a.x - b.x)*(a.y - b.y) > 0)
		{
			angle = 45f;
		}
		else 
		{
			angle = -45;
		}
		return angle;
	}
}
