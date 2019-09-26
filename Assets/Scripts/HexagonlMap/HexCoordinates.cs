/// <summary>
/// 六边形坐标系结构体
/// </summary>
[System.Serializable]
public struct HexCoordinates
{
    public int X { get; private set; }
 
    public int Z { get; private set; }
 
    public HexCoordinates (int x, int z) {
        X = x;
        Z = z;
    }
    public static HexCoordinates FromOffsetCoordinates (int x, int z) {
        return new HexCoordinates(x-z/2, z);//修正那些X坐标让它们沿直线排开 取消水平调整
    }
    public override string ToString () {
        return "(" + X.ToString() + ", " + Z.ToString() + ")";
    }
 
    public string ToStringOnSeparateLines () {
        return X.ToString() + "\n" + Z.ToString();
    }
}
