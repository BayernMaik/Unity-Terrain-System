using UnityEngine;
using System;

public class Vector2Utility
{
    // Floor
    public static Vector2 floor(Vector2 vector2)
    {
        return new Vector2(Mathf.Floor(vector2.x), Mathf.Floor(vector2.y));
    }
    // Modulo
    public static Vector2 mod289(Vector2 x)
    {
        return x - floor(x * (1.0f / 289.0f)) * 289.0f;
    }
    // Fractional
    public static Vector2 frac(Vector2 x)
    {
        return x - floor(x);
    }
    // Square
    public static Vector2 sqrt(Vector2 vector2)
    {
        return new Vector2(vector2.x * vector2.x, vector2.y * vector2.y);
    }

    /// <summary>
    /// Clamp Individual Values of a Vector2
    /// </summary>
    /// <param name="vector2"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static Vector2 Clamp(Vector2 vector2, Vector2 min, Vector2 max)
    {
        vector2.x = Mathf.Clamp(vector2.x, min.x, max.x);
        vector2.y = Mathf.Clamp(vector2.y, min.y, max.y);
        return vector2;
    }

    #region Approximately
    public static bool Approximately(Vector2 vector2A, Vector2 vector2B)
    {
        return Mathf.Approximately(vector2A.x, vector2B.x) && Mathf.Approximately(vector2A.y, vector2B.y);
    }
    #endregion

    // In Bounds
    #region InBounds
    public static bool InBounds(Vector2 targetVector2, Vector2 minBounds, Vector2 maxBounds)
    {
        return
            (targetVector2.x >= minBounds.x) && (targetVector2.x <= maxBounds.x) &&
            (targetVector2.y >= minBounds.y) && (targetVector2.y <= maxBounds.y);
    }
    #endregion

}

public static class ExtensionMethods_Vector2
{
    #region Floor
    public static Vector2 Floor(this Vector2 vector2)
    {
        return new Vector2(Mathf.Floor(vector2.x), Mathf.Floor(vector2.y));
    }
    #endregion

    #region Clamp
    /// <summary>
    /// Clamp Individual Values of a Vector2
    /// </summary>
    /// <param name="vector2"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static Vector2 Clamp(this Vector2 vector2, Vector2 min, Vector2 max)
    {
        vector2.x = Mathf.Clamp(vector2.x, min.x, max.x);
        vector2.y = Mathf.Clamp(vector2.y, min.y, max.y);
        return vector2;
    }
    #endregion

    // InRangeZero
    #region InRangeZero
    public static bool InRangeZero(this Vector2 vector2, Vector2 vector2Range)
    {
        return
            (vector2.x >= 0) && (vector2.x <= vector2Range.x) &&
            (vector2.y >= 0) && (vector2.y <= vector2Range.y);
    }
    #endregion
}