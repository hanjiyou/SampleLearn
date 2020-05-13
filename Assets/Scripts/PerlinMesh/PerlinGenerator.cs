using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerlinGenerator : MonoBehaviour
{
    public RawImage _noiseRawImage;
    public int _perlinTexWidth;
    public int _perlinTexHeight;

    public Vector2 _originPos;//平面采样区原点（偏移）

    public float _noiseScale;//控制噪声图的平滑程度

    private Texture2D perlinTexture;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            GenerateNoiseTexture();
        }
    }

    void GenerateNoiseTexture()
    {
        perlinTexture=new Texture2D(_perlinTexWidth,_perlinTexHeight);
        for (int i = 0; i < _perlinTexWidth; i++)
        {
            for (int j = 0; j < _perlinTexHeight; j++)
            {
                perlinTexture.SetPixel(i,j,CalNoiseColor(i,j));
            }
        }
        perlinTexture.Apply();
        _noiseRawImage.texture=perlinTexture;
    }
    
    /// <summary>
    /// 计算产生柏林噪声图的像素。 输入一样时，输出也必将一样
    /// 要想随机，一般都是随机偏移 originPos
    /// 也可以Mathf.PerlinNoise(Time.time * xScale, 0.0f); 只生成和使用一维噪声，控制波浪动画等
    /// </summary>
    /// <param name="xPixIndex">纹理的横坐标</param>
    /// <param name="yPixIndex">纹理纵坐标</param>
    /// <returns></returns>
    Color CalNoiseColor(int xPixIndex,int yPixIndex)
    {
        float xCoordinate = _originPos.x + (float)xPixIndex/_perlinTexWidth* _noiseScale  ;
        float yCoordinate = _originPos.y + (float)yPixIndex/_perlinTexHeight*_noiseScale;
        float perlin = Mathf.PerlinNoise(xCoordinate, yCoordinate);
        Color perlinNoise=new Color(perlin,perlin,perlin);

        return perlinNoise;
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
