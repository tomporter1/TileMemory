using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coord : MonoBehaviour
{
    [SerializeField]
    private int x, y;

    public Coord(int x = 0, int y = 0)
    {
        this.x = x;
        this.y = y;
    }

    public static bool operator ==(Coord coord1, Coord coord2)
    {
        if (coord1.GetX() == coord2.GetX() && coord1.GetY() == coord2.GetY())
            return true;
        return false;
    }

    public static bool operator !=(Coord coord1, Coord coord2)
    {
        if (coord1.GetX() == coord2.GetX() && coord1.GetY() == coord2.GetY())
            return false;
        return true;
    }

    public int GetX()
    {
        return x;
    }

    public int GetY()
    {
        return y;
    }

    public override string ToString()
    {
        return "(" + x + ", " + y + ")";
    }

    internal void SetValue(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
