using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 生成网格简单测试
/// ①先设置Mesh对象的顶点数据集合vertices(V3[])
/// ②triangles决定网格显示的形状(int[])，从头到尾部，三个元素绘制成一个三角形。其中，每个元素的内容是顶点的下标，即三个顶点下标绘制成一个三角形。
/// ③网格顶点的顺序建议是顺时针，根据左手定则，顺时针-z轴可见。
/// </summary>
[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class GenerateMesh : MonoBehaviour
{
    public int xSize = 5;
    public int ySize = 6;
    private Vector3[] vertices;
    private Mesh mesh;
    private void Awake()
    {
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        WaitForSeconds wait=new WaitForSeconds(0.05f);
        vertices=new Vector3[(xSize+1)*(ySize+1)];
        yield return wait; 
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
            }
        }
        mesh.vertices = vertices;
        
        int[] triangles = new int[xSize* ySize * 6];
        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++) //vi是三角形第一个顶点的下标 t1是
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)//一次循环体绘制一个四方形：两个三角形。三角形对应的长度为3的数组，按照顺时针绘画让-z可见 ti += 6意味着一次循环完毕 跳过这次循环的两个三角形，开始绘制第三个三角形。
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
                mesh.triangles = triangles;
                yield return wait;
            }
        }

        mesh.RecalculateNormals();
//        triangles[0] = 0;
//        triangles[1] =xSize+1;
//        triangles[2] =1;
//        triangles[3] = xSize + 3;//该点不会显示 因为未组成三角形
//        mesh.triangles = triangles;
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++) 
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}
