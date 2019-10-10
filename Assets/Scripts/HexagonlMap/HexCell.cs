using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 六边形邻居方向枚举
/// </summary>
public enum HexDirection {
    NE=0, E, SE, SW, W, NW
} 
/// <summary>
/// 六边形邻居方向枚举扩展方法
/// </summary>
public static class HexDirectionExtensions { 
    public static HexDirection Opposite (this HexDirection direction) { 
        return (int)direction < 3 ? (direction + 3) : (direction - 3); 
    } 
    
    /// <summary>
    /// 获取指定方向的前一个方向(循环，即如果是第一个则返回最后一个)
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static HexDirection Previous (this HexDirection direction) {
        return direction == HexDirection.NE ? HexDirection.NW : (direction - 1);
    }
    
    /// <summary>
    /// 获取指定方向的后一个方向(循环，即如果是最后一个则返回第一个)
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static HexDirection Next (this HexDirection direction) {
        return direction == HexDirection.NW ? HexDirection.NE : (direction + 1);
    }
} 
/// <summary>
/// 六边形网格单元，里面定义了六边形单元持有的属性
/// </summary>
public class HexCell : MonoBehaviour
{
    public HexCoordinates coordinates;
    public Color color;
    [SerializeField] 
    HexCell[] neighbors;//当前六边形的邻居(有6条边，6个邻居)
    public HexCell GetNeighbor (HexDirection direction) { 
        return neighbors[(int)direction]; 
    } 
    public void SetNeighbor (HexDirection direction, HexCell cell) { 
        neighbors[(int)direction] = cell; 
        cell.neighbors[(int)direction.Opposite()] = this; 
    } 

}
