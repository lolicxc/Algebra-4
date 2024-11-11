using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyTools
{
    public static Vector3 Min(Vector3 a, Vector3 b)
    {
        return new Vector3(Mathf.Min(a.x, b.x), Mathf.Min(a.y, b.y), Mathf.Min(a.z, b.z));
    }

    public static Vector3 Max(Vector3 a, Vector3 b)
    {
        return new Vector3(Mathf.Max(a.x, b.x), Mathf.Max(a.y, b.y), Mathf.Max(a.z, b.z));
    }

    public static float Min(float a, float b)
    {
        return (a < b) ? a : b;
    }

    public static float Max(float a, float b)
    {
        return (a > b) ? a : b;
    }

    public static float DotProduct(Vector3 a, Vector3 b)
    {
        return (a.x * b.x) + (a.y * b.y) + (a.z * b.z);
    }

    public static Vector3 CrossProduct(Vector3 a, Vector3 b)
    {
        float x = (a.y * b.z) - (a.z * b.y);
        float y = (a.z * b.x) - (a.x * b.z);
        float z = (a.x * b.y) - (a.y * b.x);
        return new Vector3(x, y, z);
    }
}
