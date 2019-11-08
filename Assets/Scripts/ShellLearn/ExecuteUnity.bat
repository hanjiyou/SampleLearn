@REM @Author: Marte
@REM @Date:   2019-11-08 17:17:53
@REM @Last Modified by:   Marte
@REM Modified time: 2019-11-08 19:12:25
::执行unity命令时，必须把unity 关闭，否则操作会取消
set logPath="C:\Users\sgame\Desktop\Log.log"
set unityExePath="D:\WorkSoftwareInstall\Unity2018.3.7\Unity\Editor\Unity.exe"
set projectPath="D:\WorkSpace\SampleLearn"
::判断Unity是否运行中
::TASKLIST /V /S localhost /U %username%>%logPath%
::"type"命令 打印指定文件 通过"|"进行过滤 "Find"命令表示查找
TYPE %logPath% |FIND "Unity.exe"
::errorlevel表示上一条命令的执行结果。返回的值只有两个，0表示”成功”、1表示”失败”
if %errorlevel% NEQ 0 goto START_BAT_EXECUTE_UNITY
else goto  UNITY_IS_RUNNING

::如果unity正在运行
:UNITY_IS_RUNNING
taskkill /f /im untiy.exe
ping 127.0.0.1 -n 1> nul
goto START_BAT_EXECUTE_UNITY

::bat命令执行u3d脚本的静态函数
:START_BAT_EXECUTE_UNITY
%unityExePath% -projectPath %projectPath% -quit -batchmode -executeMethod MyEditorScript.RunTest
PAUSE
