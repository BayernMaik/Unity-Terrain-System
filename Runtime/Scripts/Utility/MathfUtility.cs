using UnityEngine;

public static class MathfUtility
{
    #region PI
    private static float _PI2 = 6.283185307f;
    public static float PI2 { get { return _PI2; } }
    #endregion

    #region Closer To Zero
    /// <summary>
    /// Find the value closer to Zero
    /// </summary>
    /// <param name="valueA"></param>
    /// <param name="valueB"></param>
    /// <returns></returns>
    public static float CloserToZero(float valueA, float valueB)
    {
        if (Mathf.Abs(valueA) <= Mathf.Abs(valueB))
        {
            return valueA;
        }
        else
        {
            return valueB;
        }
    }
    #endregion

    #region Closer To Value
    /// <summary>
    /// Find the value closer to TargetValue
    /// </summary>
    /// <param name="targetValue"></param>
    /// <param name="valueA"></param>
    /// <param name="valueB"></param>
    /// <returns></returns>
    public static float CloserToValue(float targetValue, float valueA, float valueB)
    {
        if (Mathf.Abs(targetValue - valueA) <= Mathf.Abs(targetValue - valueB))
        {
            return valueA;
        }
        else
        {
            return valueB;
        }
    }
    #endregion

    #region Further From Value
    public static float FurtherFromValue(float targetValue, float valueA, float valueB)
    {
        if (Mathf.Abs(targetValue - valueA) >= Mathf.Abs(targetValue - valueB))
        {
            return valueA;
        }
        else
        {
            return valueB;
        }
    }
    #endregion

    #region Difference
    /// <summary>
    /// Calculate Absolute Difference between two Values
    /// </summary>
    /// <param name="valueA"></param>
    /// <param name="valueB"></param>
    /// <returns></returns>
    public static float Difference(float valueA, float valueB)
    {
        return Mathf.Abs(valueA - valueB);
    }
    #endregion

    #region Linear Damp Toward Zero
    public static float LinearDampTowardZero(ref float value, float step)
    {
        if (value > 0.0f)
        {
            value = Mathf.Max(value -= step, 0.0f);
        }
        else
        {
            value = Mathf.Min(value += step, 0.0f);
        }
        return value;
    }
    #endregion
}