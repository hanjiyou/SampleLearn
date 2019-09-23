﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++) 
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)//一次循环体绘制一个四方形：两个三角形。三角形对应的长度为3的数组，按照顺时针绘画让-z可见
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
                mesh.triangles = triangles;
                yield return wait;
            }
        }
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
