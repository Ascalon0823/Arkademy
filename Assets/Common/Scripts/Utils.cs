using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static Vector3 DirFromAngle(float angleInDgr, float parentAngle = 0f)
    {
        angleInDgr += parentAngle;
        return new Vector3(Mathf.Sin(angleInDgr * Mathf.Deg2Rad),0f, Mathf.Cos(angleInDgr*Mathf.Deg2Rad));
    }
}
