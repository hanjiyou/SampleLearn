using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 自定义六边形地图网格组件类
/// </summary>
[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{
    Mesh hexMesh;
    List<Vector3> vertices;
    List<int> triangles;
    private MeshCollider meshCollider;
    private List<Color> colors;
    void Awake () {
        GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
        meshCollider = gameObject.AddComponent<MeshCollider>();
        hexMesh.name = "Hex Mesh";
        vertices = new List<Vector3>();
        triangles = new List<int>();
        colors = new List<Color>();
    }
    /// <summary>
    /// 三角化所有的网格单元
    /// </summary>
    /// <param name="cells"></param>
    public void TriangulateAllHexCell (HexCell[] cells) {
        hexMesh.Clear();
        vertices.Clear();
        triangles.Clear();
        colors.Clear();
        
        for (int i = 0; i < cells.Length; i++) {
            Triangulate(cells[i]);
        }
        hexMesh.vertices = vertices.ToArray();
        hexMesh.triangles = triangles.ToArray();
        hexMesh.colors = colors.ToArray();
        hexMesh.RecalculateNormals();
        meshCollider.sharedMesh = hexMesh;//在完成三角化之后将我们的网格物体设置给碰撞器。
    }
    void Triangulate (HexCell cell) {
        for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++) {
            Triangulate(d, cell);
        }
    }
    void Triangulate (HexDirection direction, HexCell cell) {
    //纯色区域三角化使用单一颜色
        Vector3 center = cell.transform.localPosition;
        Vector3 v1 = center + HexMetrics.GetFirstSolidCorner(direction);
        Vector3 v2 = center + HexMetrics.GetSecondSolidCorner(direction);
 
        AddTriangle(center, v1, v2);
        AddTriangleColor(cell.color);
    //梯形混色区域
        Vector3 v3 = center + HexMetrics.GetFirstCorner(direction);
        Vector3 v4 = center + HexMetrics.GetSecondCorner(direction);
 
        AddQuad(v1, v2, v3, v4);
 
        HexCell prevNeighbor = cell.GetNeighbor(direction.Previous()) ?? cell;
        HexCell neighbor = cell.GetNeighbor(direction) ?? cell;
        HexCell nextNeighbor = cell.GetNeighbor(direction.Next()) ?? cell;
 
        AddQuadColor(
            cell.color,
            cell.color,
            (cell.color + prevNeighbor.color + neighbor.color) / 3f,
            (cell.color + neighbor.color + nextNeighbor.color) / 3f
        );
    }
    /// <summary>
    /// 添加三角形
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <param name="v3"></param>
    void AddTriangle (Vector3 v1, Vector3 v2, Vector3 v3) {
        int vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        triangles.Add(vertexIndex);   
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
    }
    /// <summary>
    /// 设置三角形颜色（分别对三个顶点）
    /// </summary>
    /// <param name="color"></param>
    void AddTriangleColor (Color c) {
        colors.Add(c);
        colors.Add(c);
        colors.Add(c);
    }
    /// <summary>
    /// 添加四边梯形(对mesh的三角集合来说 一个四边形=顺时针顺序添加两个三角形 2个顶点重复即4个顶点)
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <param name="v3"></param>
    /// <param name="v4"></param>
    void AddQuad (Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4) {
        int vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        vertices.Add(v4);
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 2);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
        triangles.Add(vertexIndex + 3);
    }
 
    void AddQuadColor (Color c1, Color c2, Color c3, Color c4) {
        colors.Add(c1);
        colors.Add(c2);
        colors.Add(c3);
        colors.Add(c4);
    }
}
