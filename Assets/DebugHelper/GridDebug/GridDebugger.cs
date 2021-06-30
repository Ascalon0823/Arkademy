using System.Collections;
using System.Collections.Generic;
using Arkademy;
using UnityEngine;

public class GridDebugger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var g = new Grid<int>(3, 4);
        Debug.Log(g.ToString());
    }
}