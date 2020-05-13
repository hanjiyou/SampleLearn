using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    public int w;
    public int h;
    public Vector2 CenterPos;
    public float NoiseScale;
    public AnimationCurve AnimationCurve;
    private float[,] noiseResult;

    private Mesh _mesh;
    Vector3[] _vertices ;

    private int[] _triangles;//共有(w-1)*(h-1)个正方形  长度必须是3的倍数 内容是顶点数组的索引

    private Vector3[] _normals;//每个顶点的法线 长度和顶点一样 对应顶点的下标存储法线
    // Start is called before the first frame update
    void Start()
    {
        _vertices = new Vector3[w * h];
        _triangles = new int[(w - 1) * (h - 1) * 6];
        _normals=new Vector3[_vertices.Length];
        _mesh = transform.GetComponent<MeshFilter>().mesh;
        noiseResult = GetNoiseData(w,h,CenterPos,NoiseScale);
        CalculHeight();
        GeneratorMesh();
    }

    void CalculHeight()
    {
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                noiseResult[i, j] *= AnimationCurve.Evaluate(noiseResult[i, j]) * NoiseScale;//根据噪声图和动画曲线共同控制地图的高 为了让高度起伏更真实
            }
        }
    }

    void CalculNormal()
    {
        int triangleCount = _triangles.Length / 3;
        for (int i = 0; i <triangleCount ; i++)
        {
            int triangleIndex = i * 3;//三角形 对应的下标起点 
            int curTraIndexA = _triangles[triangleIndex];
            int curTraIndexB= _triangles[triangleIndex+1];
            int curTraIndexC = _triangles[triangleIndex+2];
            Vector3 curTraNormal = CalSurfaceNormalFromIndex(curTraIndexA, curTraIndexB, curTraIndexC);//计算得到当前三角面的法线

            _normals[curTraIndexA] += curTraNormal;
            _normals[curTraIndexB] += curTraNormal;
            _normals[curTraIndexC] += curTraNormal;

        }
    }
    
    private int triangleIndex = 0;
    void GeneratorMesh()
    {
  

        for (int i = 0; i < h; i++)//行下标 <高
        {
            for (int j = 0; j < w; j++)//列下标 <宽
            {
                _vertices[i*w+j]=new Vector3(i,noiseResult[i,j]*NoiseScale,j);

                if ((i < h - 1) && (j < w - 1))
                {
                    //左手定则 顺时针顺序 像三角形数组中填充 对应顶点的下标
                    _triangles[triangleIndex] = i * w + j;
                    _triangles[triangleIndex+1] = (i+1)*w+j+1;
                    _triangles[triangleIndex + 2] = (i + 1) * w + j;
                
                    _triangles[triangleIndex+3] = (i+1)*w+j+1;
                    _triangles[triangleIndex+4] = i * w + j;;
                    _triangles[triangleIndex +5] = i * w + j+1;
                    triangleIndex += 6;
                }
            }
        }
        CalculNormal();
        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;
        _mesh.normals = _normals;
        _mesh.RecalculateNormals();
        
    }
    
    /// <summary>
    /// 通过三个顶点的下标 计算对应三角形的表面法线
    /// </summary>
    /// <param name="index1"></param>
    /// <param name="index2"></param>
    /// <param name="index3"></param>
    /// <returns></returns>
    Vector3 CalSurfaceNormalFromIndex(int index1,int index2,int index3)
    {
        Vector3 pA = _vertices[index1];
        Vector3 pB = _vertices[index2];
        Vector3 pC = _vertices[index3];
        //因为顶点顺序遵循左手定则 则叉乘方向是已知的
        Vector3 lineAB = pB - pA;
        Vector3 lineAC = pC - pA;
        Vector3 normal = Vector3.Cross(lineAB, lineAC);
        return normal;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public float[,] GetNoiseData(int w,int h,Vector2 centerPos,float noiseScale)
    {
        float[,] results=new float[w,h];
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                float xCoordinate = centerPos.x + (float) i / w * noiseScale;
                float yCoordinate = centerPos.y + (float) j / h * noiseScale;
                float perlin = Mathf.PerlinNoise(xCoordinate, yCoordinate);
                results[i, j] = perlin;
            }
        }
        return results;
    }
}
