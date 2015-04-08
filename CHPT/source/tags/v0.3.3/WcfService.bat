::安装windwos服务-数据
sc create HXCDataServiceWinService binpath= "%cd%\HXCDataServiceWinService.exe" 
pause
::安装windwos服务-文件
sc create HXCFileServiceWinService binpath= "%cd%\HXCFileServiceWinService.exe" 
pause
::安装windwos服务-新文件
sc create HXCFileTransferServiceWinService binpath= "%cd%\HXCFileTransferServiceWinService.exe" 
pause
::安装windwos服务-session
sc create HXCSessionServiceWinService binpath= "%cd%\HXCSessionServiceWinService.exe" 
pause

net start HXCDataServiceWinService
net start HXCFileServiceWinService
net start HXCFileTransferServiceWinService
net start HXCSessionServiceWinService
pause 