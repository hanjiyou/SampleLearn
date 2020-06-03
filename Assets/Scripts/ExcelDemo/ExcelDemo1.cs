using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ExcelDemo1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DataSet dataSet= ExcelReader.ReadExcel("Hero/HeroCard.xlsx");
        foreach (DataRow rows in dataSet.Tables[0].Rows)
        {
            string rowContent="";
            foreach (DataColumn cols in dataSet.Tables[0].Columns)
            {
                rowContent += rows[cols].ToString()+" ";
            }
                LogTool.Log("hhh "+rowContent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
