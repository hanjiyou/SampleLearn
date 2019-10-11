using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 六边形基本属性类
/// </summary>
public class HexMetrics
{
    public const float outerRadius = 10f;
    public const float innerRadius = outerRadius * 0.866025404f;
    public const float solidFactor = 0.75f;//纯色区域边长占原六边形的比(不混色的部分)
    public const float blendFactor = 1f - solidFactor;//混色区域占比
    
    public static Vector3[] corners = {
        new Vector3(0f, 0f, outerRadius),
        new Vector3(innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(0f, 0f, -outerRadius),
        new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(-innerRadius, 0f, 0.5f * outerRadius)
    };
    /// <summary>
    /// 获取指定方向的边上的第一个点
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static Vector3 GetFirstCorner (HexDirection direction) {
        return corners[(int)direction];
    }
    /// <summary>
    /// 获取指定方向的边上的第二个点
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static Vector3 GetSecondCorner (HexDirection direction) {
        return corners[((int)direction + 1)%6];
    }
    public static Vector3 GetFirstSolidCorner (HexDirection direction) {
        return corners[(int)direction] * solidFactor;
    }
 
    public static Vector3 GetSecondSolidCorner (HexDirection direction) {
        return corners[((int)direction + 1)%6] * solidFactor;
    }
    //获取三角形临边的桥（center与v1、v2的交点形成的矢量）
    public static Vector3 GetBridge (HexDirection direction) {
        return (corners[(int)direction] + corners[((int)direction + 1)%6]) *
                blendFactor;
    }
}
