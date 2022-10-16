using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector2IntUtility
{
    public static Vector2Int Divide(Vector2Int vector2Int, int value)
    {
        vector2Int.x /= value;
        vector2Int.y /= value;
        return vector2Int;
    }
}
