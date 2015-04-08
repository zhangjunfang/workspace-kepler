::卸载windwos服务-数据
sc delete HXCDataServiceWinService
pause
::卸载windwos服务-文件
sc delete HXCFileServiceWinService
pause
::卸载windwos服务-新文件
sc delete HXCFileTransferServiceWinService
pause
::卸载windwos服务-session
sc delete HXCSessionServiceWinService
pause