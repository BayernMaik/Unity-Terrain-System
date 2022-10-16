using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeUtility
{
    private static string[] _months = new string[]
    {
        "January",
        "February",
        "March",
        "April",
        "May",
        "June",
        "Jule",
        "August",
        "September",
        "October",
        "November",
        "December"
    };
    public static string Month(int _monthIndex)
    {
        if ((_monthIndex >= 0) && (_monthIndex <= 12))
        {
            return _months[_monthIndex];
        }
        else
        {
            return "";
        }
    }
}
