----------------------------定制接收器开发说明----------------------------
1. 打包
将TRCarReceiver中的CarEPReceiver和CarReceiver这两个类打成receiver-x.x.jar包，
其他的配置文件和jar包都不要打进去，放到/RTCarService/WEB-INF/lib下

2. 导入
RTCarService/WEB-INF/lib下已有
jts-1.12.jar、log4j-1.2.17.jar、RTCarProject-2.2.7.jar这3个jar包，不能删除

3. 配置文件说明
#####---------------------------app.properties----------------------#######
# GPS数据接收端口 (默认端口:4000; 不用改动)
carPort=4000
# 权限数据接收端口(默认端口:4000; 不用改动)
epPort=4001
# 数据是否压缩(默认:true; 保持与发送端设置一致)
gzip=true
# 测试环境初始化模拟数据加载(默认：false; 有发送端时为false)
dfirst=false
# 是否初始化空间聚合模块(默认：false; 用于数字统计)
dagg=false
# 内部空间聚合间隔时间(默认:120; >=120秒)
daggDelay=120
# 权限接收器实现类名(默认:com.zjxl.transmap.core.CarEPReceiver; 根据CarEPReceiver具体路径配置)
epreceiver=com.zjxl.transmap.core.CarEPReceiver
# GPS数据接收器实现类名(默认:com.zjxl.transmap.core.CarReceiver; 根据CarReceiver具体路径配置)
carreceiver=com.zjxl.transmap.core.CarReceiver
#####---------------------------end----------------------#######


RTCarService提供默认的接收器,协议参考《实时GIS Server协议》文档，示例参考本工程代码。
如果用户需自定义接收器来接收数据，可继承com.zjxl.transmap.rt.server.IReceiver接口。
过程如下：
1）实现接口打包输出，将jar包放在/RTCarService/WEB-INF/lib
   注意: 本工程Lib目录下的初始第三方jar包不要复制到/RTCarService/WEB-INF/lib

2）修改/RTCarService/WEB-INF/classes/app.properties
    # 权限接收器实现类名
    epreceiver=com.zjxl.transmap.rt.CarEPReceiver（替换自定义类名）
    # GPS数据接收器实现类名
    carreceiver=com.zjxl.transmap.rt.CarReceiver（替换自定义类名）

3）更新/RTCarService/WEB-INF/classes/mg_car图标库（原图保留，有重复名可覆盖）
   图标命名规则为:  state(车辆状态)_angle(正北顺时夹角)