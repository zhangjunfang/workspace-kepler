::卸载windwos服务-数据
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe -u  HXCDataServiceWinService.exe
pause
::卸载windwos服务-文件
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe -u  HXCFileServiceWinService.exe
pause
::卸载windwos服务-新文件
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe -u  HXCFileTransferServiceWinService.exe
pause
::卸载windwos服务-session
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe -u  HXCSessionServiceWinService.exe
pause