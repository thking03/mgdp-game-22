using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControl : MonoBehaviour
{
    public bool moveable;
    public Vector3 position;

    private void Start()
    {
        position = transform.position;
    }
}
