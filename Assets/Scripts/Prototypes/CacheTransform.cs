using UnityEngine;
using System.Collections;
/// <summary>
/// Cache transform.
/// </summary>
public class CacheTransform : MonoBehaviour {
	private Transform cacheTransform;

	/// <summary>
	/// Кэшированный трансформ
	/// </summary>
	/// <value>The transform.</value>
	public new Transform transform {
		get {
			if(cacheTransform == null)
			{ 
 				cacheTransform = base.transform;
			}
			return cacheTransform;
		}
		set {
			cacheTransform = value;
		}
	}
}
