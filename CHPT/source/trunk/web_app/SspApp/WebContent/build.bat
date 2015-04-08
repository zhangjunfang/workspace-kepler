@echo off
echo "%~dp0"
pause



node r.js -o cssIn=css/import.css out=css/built.css optimizeCss=standard



node r.js -o cssIn=css/wood/import.base.css out=css/wood/base.css optimizeCss=standard



node r.js -o cssIn=css/navy/import.base.css out=css/navy/base.css optimizeCss=standard



node r.js -o cssIn=css/Black/import.base.css out=css/Black/base.css optimizeCss=standard



node r.js -o cssIn=css/import.base.css out=css/base.css optimizeCss=standard



pause

copy .\script\main.js .\script\main_tmp.js 
node r.js -o build.js


xcopy .\temp\plugin\ligerui\skins\ctfo\css\ligerui-dialog.css .\dist\plugin\ligerui\skins\ctfo\css\  /d /c /y
xcopy .\temp\plugin\jquery\jquery_loadmask\jquery.loadmask.css .\dist\plugin\jquery\jquery_loadmask\  /d /c /y 
xcopy .\temp\plugin\jquery\jquery_loadmask\jquery.loadmask.js .\dist\plugin\jquery\jquery_loadmask\  /d /c /y 
xcopy .\temp\plugin\jquery\jquery_loadmask\loading.gif .\dist\plugin\jquery\jquery_loadmask\  /d /c /y 
xcopy .\temp\plugin\ligerui\js\ligerui.all.ctfo.js .\dist\plugin\ligerui\js\  /d /c /y 

xcopy .\temp\model\*.js .\dist\model\  /d /c /y 
xcopy .\temp\model\operations\*.js .\dist\model\operations\  /d /c /y 
xcopy .\temp\model\home\*.js .\dist\model\home\  /d /c /y 
xcopy .\temp\model\universalTree\*.js .\dist\model\universalTree\  /d /c /y 
xcopy .\temp\model\vehicleDashBoard\*.js .\dist\model\vehicleDashBoard\  /d /c /y 
xcopy .\temp\control\*.js .\dist\control\  /d /c /y 
xcopy .\temp\i18n\*.js .\dist\i18n\  /d /c /y 
xcopy .\temp\util\*.js .\dist\util\  /d /c /y
xcopy .\temp\plugin\*.js .\dist\plugin\  /d /c /y
xcopy .\temp\plugin\doT\*.js .\dist\plugin\doT\  /d /c /y

xcopy .\temp\plugin\ligerui\js\core\base.js .\dist\plugin\ligerui\js\core\  /d /c /y  

xcopy .\temp\plugin\ligerui\js\plugins\ligerDialog.js .\dist\plugin\ligerui\js\plugins\  /d /c /y

xcopy .\temp\plugin\ligerui\js\ligerui.all.ctfo.js .\dist\plugin\ligerui\js\  /d /c /y

xcopy .\temp\plugin\highcharts3.0.2\js\highcharts.js .\dist\plugin\highcharts3.0.2\js\  /d /c /y

xcopy .\temp\plugin\amcharts\swfobject.js .\dist\plugin\amcharts\  /d /c /y

xcopy .\temp\build.txt .\dist\  /d /c /y 
xcopy .\script\main.js .\dist\  /d /c /y


xcopy .\temp\plugin\requirejs\*.js .\dist\plugin\requirejs\  /d /c /y  

xcopy .\temp\plugin\doT\doT.js .\dist\plugin\doT\  /d /c /y 


xcopy .\temp\plugin\jquery\jquery_validation\jquery.validate.js .\dist\plugin\jquery\jquery_validation\  /d /c /y

xcopy .\temp\plugin\jquery\jquery.cookie.js .\dist\plugin\jquery\  /d /c /y

xcopy .\temp\plugin\jquery\jquery-1.8.1.min.js .\dist\plugin\jquery\  /d /c /y

  
xcopy .\temp\plugin\jquery\jquery.form.js .\dist\plugin\jquery\  /d /c /y
xcopy .\temp\plugin\jquery\jquery_webedit\scripts\jHtmlArea-0.7.5.js .\dist\plugin\jquery\jquery_webedit\scripts\  /d /c /y

xcopy .\temp\plugin\jquery\jquery_tiptip\jquery.tipTip.minified.js .\dist\plugin\jquery\jquery_tiptip\  /d /c /y

xcopy .\temp\plugin\jquery\jquery_validation\*.js .\dist\plugin\jquery\jquery_validation\  /d /c /y

xcopy .\temp\plugin\kindeditor-4.1.10\*.js .\dist\plugin\kindeditor-4.1.10\  /d /c /y
xcopy .\temp\plugin\kindeditor-4.1.10\lang\*.js .\dist\plugin\kindeditor-4.1.10\lang\  /d /c /y

xcopy .\temp\plugin\jquery\jquery_pagination\jquery.pagination.js .\dist\plugin\jquery\jquery_pagination\  /d /c /y


xcopy .\temp\main_tmp.js .\dist\main.js  /d /c /y
xcopy .\temp\main.login.js .\dist\main.login.js  /d /c /y

pause
del .\script\main_tmp.js /f /q /a
rmdir /q /s .\temp\ 

rem "finish"

pause