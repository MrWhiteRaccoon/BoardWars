using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenHolder : MonoBehaviour {

    public int teamIndex;

    [HideInInspector]
    public bool[] isFree;

    public Transform[] slots;
    public Transform[] scorePoints;

    private void Start()
    {
        InitialiseHolder();
    }

    private void InitialiseHolder()
    {
        isFree = new bool[slots.Length];
        for (int i = 0; i < isFree.Length; i++)
        {
            isFree[i] = true;
        }
    }
}
