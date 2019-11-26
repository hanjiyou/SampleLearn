using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class DetailedInfoBoxExample : MonoBehaviour
{
    [DetailedInfoBox("消息盒子标题", "点击后提示文本", InfoMessageType = InfoMessageType.None)]
    public string NoneTypeMessage = "无";

    [DetailedInfoBox("标题1", "标题2", InfoMessageType = InfoMessageType.Warning)]
    public string WarningTypeMessage = "a";

    [DetailedInfoBox("标题1","标题2:www.baidu.com",InfoMessageType=InfoMessageType.Warning
        ,VisibleIf ="VisibleFunction")]
    public string HasJudgeVis;

    public bool VisibleFunction()
    {
        if (string.IsNullOrEmpty(HasJudgeVis))
        {
            return true;
        }

        return false;
    }
}
