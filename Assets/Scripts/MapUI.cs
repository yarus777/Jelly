using UnityEngine;
using System.Collections;

public class MapUI : MonoBehaviour {

    public SpriteRenderer[] spriteRenderers;

    void Awake()
    {
        GamePlay.mapUI = this;
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    void Update()
    {
        
    }
}
