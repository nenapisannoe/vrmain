using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBuilder : MonoBehaviour
{
    public static Action<int> onCollectable;
    [SerializeField] GameObject[] tiles;

    private void OnEnable() 
    {
        onCollectable += ActivateTile;
    }

    private void OnDisable() 
    {
        onCollectable -= ActivateTile;
    }
    void ActivateTile(int tile)
    {
        if(tile <= tiles.Length)
            tiles[tile].SetActive(true);
    }
}
