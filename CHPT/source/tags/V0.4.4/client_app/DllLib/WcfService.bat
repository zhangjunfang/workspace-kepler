::安装windwos服务-数据
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe  HXCDataServiceWinService.exe

::安装windwos服务-文件
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe  HXCFileServiceWinService.exe

::安装windwos服务-新文件
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe  HXCFileTransferServiceWinService.exe

::安装windwos服务-session
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe  HXCSessionServiceWinService.exe
pause

net start HXCDataService
net start HXCFileService
net start HXCFileTransferService
net start HXCSessionService
pause 