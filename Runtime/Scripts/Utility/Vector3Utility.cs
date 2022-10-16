using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3Utility
{
    // Floor
    #region Flool
    public static Vector3 floor(Vector3 vector3)
    {
        return new Vector3(Mathf.Floor(vector3.x), Mathf.Floor(vector3.y), Mathf.Floor(vector3.z));
    }
    #endregion
    // Permutation
    #region Permutation
    public static Vector3 permute(Vector3 vector3)
    {
        return mod289((34.0f * multiply(add(vector3, 1.0f), vector3)));
    }
    #endregion
    // Modulo
    #region Mod289
    public static Vector3 mod289(Vector3 x)
    {
        return x - floor(x * (1.0f / 289.0f)) * 289.0f;
    }
    #endregion
    #region Mod7
    public static Vector3 mod7(Vector3 vector3)
    {
        return vector3 - floor(vector3 * (1.0f / 7.0f)) * 7.0f;
    }
    #endregion
    // Add
    #region Add
    public static Vector3 add(Vector3 vector3, float value)
    {
        return new Vector3(vector3.x + value, vector3.y + value, vector3.z + value);
    }
    public static Vector3 add(Vector3 vector3, params float[] values)
    {
        for (int i = 0; i < values.Length; i++)
        {
            vector3 = add(vector3, values[i]);
        }
        return vector3;
    }
    #endregion
    // Subtract
    #region Subtract
    public static Vector3 subtract(Vector3 vector3, float value)
    {
        return new Vector3(vector3.x - value, vector3.y - value, vector3.z - value);
    }
    public static Vector3 subtract(float value, Vector3 vector3)
    {
        return new Vector3(value - vector3.x, value - vector3.y, value - vector3.z);
    }
    #endregion
    // Multiply
    #region Multiply
    public static Vector3 multiply(Vector3 vector3A, Vector3 vector3B)
    {
        return new Vector3(vector3A.x * vector3B.x, vector3A.y * vector3B.y, vector3A.z * vector3B.z);
    }
    #endregion
    // Fraction
    #region Fraction
    public static Vector3 frac(Vector3 vector3)
    {
        return vector3 - floor(vector3);
    }
    #endregion

    // NaN
    #region NaN
    /// <summary>
    /// Check if any Vector3 component is NaN
    /// </summary>
    /// <param name="vector3"></param>
    /// <returns></returns>
    public static bool isNan(Vector3 vector3)
    {
        return !float.IsNaN(vector3.x) && !float.IsNaN(vector3.y) && !float.IsNaN(vector3.z); 
    }
    #endregion

    // Velocity
    #region VelocityInDirection
    public static float VelocityInDirection(Vector3 direction, Vector3 velocity)
    {
        if (Vector3.Dot(velocity, direction) < 0.0f)
        {
            return -(velocity * Vector3.Dot(velocity.normalized, direction)).magnitude;
        }
        else
        {
            return (velocity * Vector3.Dot(velocity.normalized, direction)).magnitude;
        }
    }
    #endregion

    // Approximately
    #region Approximately
    public static bool Approximately(Vector3 vector3A, Vector3 vector3B)
    {
        return 
            Mathf.Approximately(vector3A.x, vector3B.x) && 
            Mathf.Approximately(vector3A.y, vector3B.y) &&
            Mathf.Approximately(vector3A.z, vector3B.z);
    }
    #endregion

    // Abs
    #region Abs
    public static Vector3 Abs(Vector3 vector3)
    {
        vector3.x = Mathf.Abs(vector3.x);
        vector3.y = Mathf.Abs(vector3.y);
        vector3.z = Mathf.Abs(vector3.z);
        return vector3;
    }
    #endregion

    // Pow
    #region Pow
    public static Vector3 Pow(Vector3 vector3, float pow)
    {
        vector3.x = Mathf.Pow(vector3.x, pow);
        vector3.y = Mathf.Pow(vector3.y, pow);
        vector3.z = Mathf.Pow(vector3.z, pow);

        return vector3;
    }
    #endregion

    // Signed Pow
    #region Signed Pow
    private static Vector3 _signedPowVector;
    public static Vector3 SignedPow(Vector3 vector3, float pow)
    {
        // Get Signature of Vector Components
        _signedPowVector.x = vector3.x < 0.0f ? -1.0f : 1.0f;
        _signedPowVector.y = vector3.y < 0.0f ? -1.0f : 1.0f;
        _signedPowVector.z = vector3.z < 0.0f ? -1.0f : 1.0f;
        // Vector Pow
        _signedPowVector.x *= Mathf.Pow(vector3.x, pow);
        _signedPowVector.y *= Mathf.Pow(vector3.y, pow);
        _signedPowVector.z *= Mathf.Pow(vector3.z, pow);
        
        return _signedPowVector;
    }
    #endregion
}
public static class Vector3ExtensionMethods
{
    // RotateAround
    public static Vector3 rotateAroundXAxisOffset(this Vector3 vector3, Vector2 offsetAngle)
    {
        // X
        float xSin = Mathf.Sin(offsetAngle.x * Mathf.Deg2Rad);
        float xCos = Mathf.Cos(offsetAngle.x * Mathf.Deg2Rad);

        vector3.x = xSin;
        vector3.z = xCos;

        // Y
        float ySin = Mathf.Sin(offsetAngle.y * Mathf.Deg2Rad);
        float yCos = Mathf.Cos(offsetAngle.y * Mathf.Deg2Rad);

        vector3.y = vector3.z * ySin;
        vector3.z = vector3.z * yCos;

        return new Vector3(vector3.x, vector3.y, vector3.z);
    }
    public static Vector3 rotateAroundYAxisOffset(this Vector3 vector3, Vector2 offsetAngle)
    {
        // Y
        float ySin = Mathf.Sin(offsetAngle.y * Mathf.Deg2Rad);
        float yCos = Mathf.Cos(offsetAngle.y * Mathf.Deg2Rad);

        vector3.y = ySin;
        vector3.z = yCos;

        // X
        float xSin = Mathf.Sin(offsetAngle.x * Mathf.Deg2Rad);
        float xCos = Mathf.Cos(offsetAngle.x * Mathf.Deg2Rad);

        vector3.x = vector3.z * xSin;
        vector3.z = vector3.z * xCos;

        return new Vector3(vector3.x, vector3.y, vector3.z);
    }
    public static Vector3 rotateAroundYAxisOffset(this Vector3 vector3, Vector2 offsetAngle, float radius)
    {
        // Y
        float ySin = Mathf.Sin(offsetAngle.y * Mathf.Deg2Rad) * radius;
        float yCos = Mathf.Cos(offsetAngle.y * Mathf.Deg2Rad) * radius;

        vector3.y = ySin;
        vector3.z = yCos;

        // X
        float xSin = Mathf.Sin(offsetAngle.x * Mathf.Deg2Rad) * radius;
        float xCos = Mathf.Cos(offsetAngle.x * Mathf.Deg2Rad) * radius;

        vector3.x = vector3.z * xSin;
        vector3.z = vector3.z * xCos;

        return new Vector3(vector3.x, vector3.y, vector3.z);
    }
    public static Vector3 rotateAroundOffset(this Vector3 vector3, Vector2 offsetAngle)
    {
        /*
        float distance = Vector3.Distance(vector3, center);
        float xDiff = vector3.x - center.x;
        float yDiff = vector3.y - center.y;
        float zDiff = vector3.z - center.z;
        */
        // Position
        // X
        float xSin = Mathf.Sin(offsetAngle.x * Mathf.Deg2Rad);
        float xCos = Mathf.Cos(offsetAngle.x * Mathf.Deg2Rad);

        // Y
        float ySin = Mathf.Sin(offsetAngle.y * Mathf.Deg2Rad);
        float yCos = Mathf.Cos(offsetAngle.y * Mathf.Deg2Rad);

        vector3.x = xSin;
        vector3.y = ySin;
        vector3.z = xCos;

        //vector3.x = vector3.x;
        vector3.y = vector3.y * xCos;
        vector3.z = vector3.z * yCos;


        return new Vector3(vector3.x, vector3.y, vector3.z);
    }

    #region FollowOrbit
    private static Vector3 _followOrbitVector;
    public static Vector3 FollowOrbit(this Vector3 vector3, Vector2 offsetAngle, float radius)
    {
        float ySin = Mathf.Sin(offsetAngle.y * Mathf.Deg2Rad); // Elevation
        float yCos = Mathf.Cos(offsetAngle.y * Mathf.Deg2Rad);
        float xSin = Mathf.Sin(offsetAngle.x * Mathf.Deg2Rad); // Horizontal
        float xCos = Mathf.Cos(offsetAngle.x * Mathf.Deg2Rad);

        _followOrbitVector.y = ySin;
        _followOrbitVector.z = yCos;
        _followOrbitVector.x = _followOrbitVector.z * xSin;
        _followOrbitVector.z = _followOrbitVector.z * xCos;        

        return vector3 + (_followOrbitVector * radius);
    }
    #endregion

    public static void Clamp(this Vector3 vector3, Vector3 min, Vector3 max)
    {
        vector3.x = Mathf.Clamp(vector3.x, min.x, max.x);
        vector3.y = Mathf.Clamp(vector3.y, min.y, max.y);
        vector3.z = Mathf.Clamp(vector3.z, min.z, max.z);
    }


    // Approximately
    #region Approximately
    public static bool Approximately(this Vector3 vector3, Vector3 vector)
    {
        return
            Mathf.Approximately(vector3.x, vector.x) &&
            Mathf.Approximately(vector3.y, vector.y) &&
            Mathf.Approximately(vector3.z, vector.z);
    }
    #endregion

    // Round
    #region Round To Decimals
    public static Vector3 RoundToOneDecimal(this Vector3 vector3)
    {
        vector3.x = Mathf.Round(vector3.x * 100.0f) * 0.1f;
        vector3.y = Mathf.Round(vector3.y * 100.0f) * 0.1f;
        vector3.z = Mathf.Round(vector3.z * 100.0f) * 0.1f;
        return vector3;
    }
    public static Vector3 RoundToTwoDecimals(this Vector3 vector3)
    {
        vector3.x = Mathf.Round(vector3.x * 100.0f) * 0.01f;
        vector3.y = Mathf.Round(vector3.y * 100.0f) * 0.01f;
        vector3.z = Mathf.Round(vector3.z * 100.0f) * 0.01f;
        return vector3;
    }
    public static Vector3 RoundToThreeDecimals(this Vector3 vector3)
    {
        vector3.x = Mathf.Round(vector3.x * 100.0f) * 0.001f;
        vector3.y = Mathf.Round(vector3.y * 100.0f) * 0.001f;
        vector3.z = Mathf.Round(vector3.z * 100.0f) * 0.001f;
        return vector3;
    }
    #endregion
    #region Floor To Decimals
    public static Vector3 FloorToOneDecimal(this Vector3 vector3)
    {
        vector3.x = Mathf.Floor(vector3.x * 100.0f) * 0.1f;
        vector3.y = Mathf.Floor(vector3.y * 100.0f) * 0.1f;
        vector3.z = Mathf.Floor(vector3.z * 100.0f) * 0.1f;
        return vector3;
    }
    #endregion


    // In Bounds
    #region In Bounds
    /// <summary>
    /// True if Vector3 is within or equal bound limits
    /// </summary>
    /// <param name="vector3"></param>
    /// <param name="vector3min"></param>
    /// <param name="vector3max"></param>
    /// <returns>True if Vector3 within or equal bound limits</returns>
    public static bool InBounds(this Vector3 vector3, Vector3 vector3min, Vector3 vector3max)
    {
        return
            (vector3.x >= vector3min.x) && (vector3.x <= vector3max.x) &&
            (vector3.y >= vector3min.y) && (vector3.y <= vector3max.y) &&
            (vector3.z >= vector3min.z) && (vector3.z <= vector3max.z);
    }
    /// <summary>
    /// True if Vector3 is within or equal absoluteBound limits
    /// </summary>
    /// <param name="vector3"></param>
    /// <param name="absoluteBounds"></param>
    /// <returns></returns>
    public static bool InBounds(this Vector3 vector3, Vector3 absoluteBounds)
    {
        return
            (Mathf.Abs(vector3.x) <= absoluteBounds.x) &&
            (Mathf.Abs(vector3.y) <= absoluteBounds.y) &&
            (Mathf.Abs(vector3.z) <= absoluteBounds.z);
    }

    /// <summary>
    /// True if Vector3 is within or equal absoluteBound limits
    /// </summary>
    /// <param name="vector3"></param>
    /// <param name="absoluteBounds"></param>
    /// <returns></returns>
    public static void InBounds(this Vector3 vector3, Vector3 absoluteBounds, ref Vector3Bool vector3Bool)
    {
        vector3Bool.x = (Mathf.Abs(vector3.x) <= absoluteBounds.x);
        vector3Bool.y = (Mathf.Abs(vector3.y) <= absoluteBounds.y);
        vector3Bool.z = (Mathf.Abs(vector3.z) <= absoluteBounds.z);
    }
    #endregion

    // Above Threshold
    #region Above Threshold
    /// <summary>
    /// Vector3Bool component true if vector3 is within threshold bounds in negative and positive direction
    /// </summary>
    /// <param name="vector3"></param>
    /// <param name="threshold"></param>
    /// <param name="vector3Bool"></param>
    public static void AboveThreshold(this Vector3 vector3, Vector3 threshold, ref Vector3Bool vector3Bool)
    {
        vector3Bool.x = Mathf.Abs(vector3.x) > threshold.x;
        vector3Bool.y = Mathf.Abs(vector3.y) > threshold.y;
        vector3Bool.z = Mathf.Abs(vector3.z) > threshold.z;
    }
    #endregion
}