using UnityEngine;

public class Platform
{
    public static bool OnEditor =>
#if UNITY_EDITOR
        true;
#else
        false;
#endif
}