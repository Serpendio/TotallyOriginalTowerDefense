using UnityEngine;

public static class Vector2Ext
{
    public static Vector3 To3D(this Vector2 vector)
        => new Vector3(vector.x, vector.y);
}
