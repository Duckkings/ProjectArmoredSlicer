using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CaculateDataBase
{

        public static Vector3 CaculateEuler(Vector3 euler)
    {
        Vector3 result = euler;
        if (result.x > 180)
        {
            result.x -= 360;
        }
        if (result.y > 180)
        {
            result.y -= 360;
        }
        if (result.z > 180)
        {
            result.z -= 360;
        }
        return result;
    }

    
        public static float Lerp(float a, float b, float t)
    {
        if (Mathf.Abs(a - b) < 0.1f)
        {
            return b;
        }
        return Mathf.Lerp(a, b, t);
    }

    public static Vector3 Vector3Lerp(Vector3 a, Vector3 b, float t)
    {
        if (Vector3.Distance(a,b) < 0.1f)
        {
            return b;
        }
        return Vector3.Lerp(a, b, t);
    }
}
