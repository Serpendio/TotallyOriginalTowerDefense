using UnityEngine;

public static class Vector3Ext
{
    public static Vector2 To2D(this Vector3 vector)
        => new Vector2(vector.x, vector.y);
}
