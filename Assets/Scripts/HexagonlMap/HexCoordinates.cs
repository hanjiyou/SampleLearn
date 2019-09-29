﻿using UnityEngine;

/// <summary>
/// 六边形坐标系结构体
/// </summary>
[System.Serializable]
public struct HexCoordinates
{
    [SerializeField]
    private int x, z;
 
    public int X {
        get {
            return x;
        }
    }
 
    public int Z {
        get {
            return z;
        }
    }
 
    public HexCoordinates (int x, int z) {
        this.x = x;
        this.z = z;
    }

    public int Y
    {
        get { return -X - Z; }
    }
    public static HexCoordinates FromOffsetCoordinates (int x, int z) {
        return new HexCoordinates(x-z/2, z);//修正那些X坐标让它们沿直线排开 取消水平调整
    }
    public override string ToString () {
        return "(" +X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")";
    }
 
    public string ToStringOnSeparateLines () {
        return X.ToString() + "\n" + Y.ToString() + "\n" + Z.ToString();
    }
}
