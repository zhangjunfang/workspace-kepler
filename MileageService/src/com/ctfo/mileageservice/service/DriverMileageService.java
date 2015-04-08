package com.ctfo.mileageservice.service;

import java.util.ArrayList;
import java.util.List;
import java.util.UUID;
import com.ctfo.mileageservice.model.DriverDetailBean;
import com.ctfo.mileageservice.model.VehicleMessageBean;
import com.ctfo.mileageservice.util.DateTools;




/**
 * 文件名：VehicleStatusService.java
 * 功能：
 *
 * @author huangjincheng
 * 2014-9-25上午9:45:27
 * 
 */
public class DriverMileageService {
	
	///private static final Logger logger = LoggerFactory.getLogger(DriverMileageService.class);
	
	private List<DriverDetailBean> driverDetaillist = new ArrayList<DriverDetailBean>();
	private DriverDetailBean driverDetail = null;

	Long dayFirstMileage = 0L;
	Long dayLastMileage = 0L;

	
	boolean isflag = false;
	
	private long utc;
	private String vid;
	
	private VehicleMessageBean lastLocBean=null;
	
	private Long tmpLastMileage=-1L;
	
	
	
	
	public DriverMileageService(long utc,String vid){
		this.utc = utc;
		this.vid = vid;
	}
	
	public void executeAnalyser(VehicleMessageBean trackBean,boolean isLastRow) throws Exception{
		// 当前值
		Long gpsTime = trackBean.getUtc();

		Long mileage = trackBean.getMileage();
		String driverId = trackBean.getDriverId();

		// 备份值
		if (lastLocBean == null) {
			lastLocBean = trackBean;
		}
		Long lastGpsTime = lastLocBean.getUtc();
		Long lastMileage = lastLocBean.getMileage();
		String lastDriverId = lastLocBean.getDriverId();

		long mg = 0;
	

	
		// 处理里程油耗：排除里程油耗在内部为-1的情况
		if (mileage > -1) {
			tmpLastMileage = mileage;
		}
		if (tmpLastMileage > -1) {
			if (mileage == -1) {
				mileage = tmpLastMileage;
			}
			if (lastMileage == -1) {
				lastMileage = tmpLastMileage;
			}
		}

		// 过滤突增数据
		mg = mileage - lastMileage;
		/*****
		 * 过滤异常数据,包括里程和油耗, 里程为异常数据则本次里程负值为0,
		 */
		if(mg >= 0 && mg <= DateTools.accountTimeIntervalVale(gpsTime,lastGpsTime,5,10f)){
			// 不做处理
		} else {
			mg = 0;
		}

		// 驾驶明细分析
		if (driverDetail == null) {
			if (driverId != null && !"".equals(driverId)) {
				driverDetail = new DriverDetailBean();
				driverDetail.setDetailId(UUID.randomUUID().toString().replace("-", ""));
				driverDetail.setVid(vid);
				driverDetail.setStatDate(this.utc);
				driverDetail.setBeginVmb(trackBean);
			}
		}else {
	
			driverDetail.addMileage(mg);
			
			//System.out.println("MILEAGE:"+driverDetail.getMileage()+":"+trackBean.getTrackStr());
			// 判断本次驾驶是否结束
			if (!driverId.equals(lastDriverId) || isLastRow) {
				// 驾驶员切换时需要结束上次驾驶记录
				driverDetail.setEndVmb(trackBean);
				//System.out.println("MILEAGE:"+driverDetail.getMileage()+":"+trackBean.getTrackStr());
				driverDetaillist.add(driverDetail);
				driverDetail = null;
			}
	
			if (driverDetail == null && driverId != null && !"".equals(driverId) && !isLastRow) {
				driverDetail = new DriverDetailBean();
				driverDetail.setDetailId(UUID.randomUUID().toString().replace("-", ""));
				driverDetail.setVid(vid);
				driverDetail.setStatDate(this.utc);
				driverDetail.setBeginVmb(trackBean);
			}
		}
		
		lastLocBean = trackBean;
	}

	/**
	 * @return the driverDetaillist
	 */
	public List<DriverDetailBean> getDriverDetaillist() {
		return driverDetaillist;
	}

	/**
	 * @param driverDetaillist the driverDetaillist to set
	 */
	public void setDriverDetaillist(List<DriverDetailBean> driverDetaillist) {
		this.driverDetaillist = driverDetaillist;
	}

	/**
	 * @return the driverDetail
	 */
	public DriverDetailBean getDriverDetail() {
		return driverDetail;
	}

	/**
	 * @param driverDetail the driverDetail to set
	 */
	public void setDriverDetail(DriverDetailBean driverDetail) {
		this.driverDetail = driverDetail;
	}
	
	
}
	

