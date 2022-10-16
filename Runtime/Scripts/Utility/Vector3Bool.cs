using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3Bool
{
    private bool _x;
    public bool x
    {
        get { return _x; }
        set { _x = value; }
    }
    private bool _y;
    public bool y
    {
        get { return _y; }
        set { _y = value; }
    }
    private bool _z;
    public bool z
    {
        get { return _z; }
        set { _z = value; }
    }

    public Vector3Bool(bool x, bool y, bool z)
    {
        _x = x;
        _y = y;
        _z = z;
    }
}
