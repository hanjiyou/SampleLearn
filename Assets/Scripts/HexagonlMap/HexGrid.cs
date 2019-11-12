using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 网格 控制网格的大小 六边形的坐标 标签的生成
/// </summary>
public class HexGrid : MonoBehaviour
{
    public int width = 6;
    public int height = 6;
    public HexCell cellPrefab;
    public Text cellLabelPrefab;
    private Canvas gridCanvas;
    HexMesh hexMesh;
    HexCell[] cells;
    public Color defaultColor = Color.white;
    public Color touchedColor = Color.magenta;
    void Awake () {
        LogTool.Log("hhh");
        cells = new HexCell[height * width];
        gridCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();
        for (int z = 0, i = 0; z < height; z++) {
            for (int x = 0; x < width; x++) {
                CreateCell(x, z, i++);
            }
        }
    }

    private void Start()
    {
        hexMesh.TriangulateAllHexCell(cells);
    }

    void CreateCell (int x, int z, int i) {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);//-z/2是水平调整 取消一部分偏移
        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f);
        
        HexCell cell = cells[i] = Instantiate(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
        cell.color = defaultColor;

        if (x > 0)//从每一行第二个开始 设置邻居(第1个被设置)
        {
            cell.SetNeighbor(HexDirection.W,cells[i-1]);
        }
        if (z > 0) {
            if ((z & 1) == 0) {//通过与运算 判断z为偶数
                cell.SetNeighbor(HexDirection.SE, cells[i - width]);
                if (x > 0) {//偶数行 从第2个设置西南
                    cell.SetNeighbor(HexDirection.SW, cells[i - width - 1]);
                }
            }
            else//奇数行
            {
                cell.SetNeighbor(HexDirection.SW, cells[i - width]);//奇数行所有网格都有西南
                if (x < width - 1)//奇数行除了最后一个 都有东南
                {
                    cell.SetNeighbor(HexDirection.SE, cells[i - width + 1]); 
                }
            }
        }
        
        Text label = Instantiate(cellLabelPrefab);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition =
            new Vector2(position.x, position.z);
        label.text = cell.coordinates.ToStringOnSeparateLines();
    }
    
    public void ColorCell(Vector3 position,Color color)
    {
        position = transform.InverseTransformPoint(position);//将世界坐标转换为本地坐标        
        HexCoordinates coordinates = HexCoordinates.FromPosition(position);        
        LogTool.Log("touched at " + coordinates.ToString());
        int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
        HexCell cell = cells[index];
        cell.color = color;
        hexMesh.TriangulateAllHexCell(cells);
    }
}
