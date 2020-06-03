using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ExcelDataReader;
using UnityEngine;

public  class ExcelReader
{
    private static string excelRootPath = Application.dataPath+"/Editor/ExcelConvert/ExcelTable/";
    public static DataSet ReadExcel(string excelName)
    {
        using (var stream=File.Open(excelRootPath+excelName,FileMode.Open,FileAccess.Read))
        {
            using (var reader=ExcelReaderFactory.CreateReader(stream))
            {
                DataSet result = reader.AsDataSet();
                return result;
            }
        }
    }
}
