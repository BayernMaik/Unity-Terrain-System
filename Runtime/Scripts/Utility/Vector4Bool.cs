using UnityEngine;

public class Vector4Bool
{
    private bool _x, _y, _z, _w;
    public bool x
    {
        get { return _x; }
        set { _x = value; }
    }
    public bool y
    {
        get { return _y; }
        set { _y = value; }
    }
    public bool z
    {
        get { return _z; }
        set { _z = value; }
    }
    public bool w
    {
        get { return _w; }
        set { _w = value; }
    }

    public Vector4Bool(bool xyzw = false)
    {
        _x = _y = _z = _w = xyzw;
    }
    public Vector4Bool(bool x, bool y, bool z, bool w)
    {
        _x = x;
        _y = y;
        _z = z;
        _w = w;
    }
}
