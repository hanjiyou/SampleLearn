using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 六边形网格组件类
/// </summary>
[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{
    Mesh hexMesh;
    List<Vector3> vertices;
    List<int> triangles;
    void Awake () {
        GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
        hexMesh.name = "Hex Mesh";
        vertices = new List<Vector3>();
        triangles = new List<int>();
    }
    /// <summary>
    /// 三角化所有的网格单元
    /// </summary>
    /// <param name="cells"></param>
    public void TriangulateAllHexCell (HexCell[] cells) {
        hexMesh.Clear();
        vertices.Clear();
        triangles.Clear();
        for (int i = 0; i < cells.Length; i++) {
            Triangulate(cells[i]);
        }
        hexMesh.vertices = vertices.ToArray();
        hexMesh.triangles = triangles.ToArray();
        hexMesh.RecalculateNormals();
    }
    void Triangulate (HexCell cell) {
        Vector3 center = cell.transform.localPosition;
        for (int i = 0; i < 6; i++)
        {
            AddTriangle(
                center,
                center + HexMetrics.corners[i%6],
                center + HexMetrics.corners[(i+1)%6]
            );
        }
     
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
}
