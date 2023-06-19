using Unity.Mathematics;
using UnityEngine;

public static class MathematicsExtensions
{
    public static bool Approximately(this float2 a, float2 b)
    {
        return Mathf.Approximately(a.x, b.x)
            && Mathf.Approximately(a.y, b.y);
    }

    public static bool Approximately(this float3 a, float3 b)
    {
        return Mathf.Approximately(a.x, b.x) 
            && Mathf.Approximately(a.y, b.y) 
            && Mathf.Approximately(a.z, b.z);
    }
}
