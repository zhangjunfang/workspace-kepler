package com.caits.analysisserver.bean;

import java.util.ArrayDeque;
import java.util.ArrayList;
import java.util.Deque;
import java.util.Iterator;
import java.util.List;
@SuppressWarnings("unused")
public class OilMonitorBean2 {
	private boolean isAccOpen = false; // 标记ACC开
	
	private long accOpenGpsTime = 0; // 标记ACC开GpsTime
	
	private Deque<OilmassChangedDetail> accOpenOilQeque = new ArrayDeque<OilmassChangedDetail>(3); 
	
	private boolean isAccClose = false; // 标记ACC关
	
	private long accCloseGpsTime = 0; // 标记ACC关GpsTime 

	private Deque<OilmassChangedDetail> accCloseOilQeque = new ArrayDeque<OilmassChangedDetail>(3); 
	
	//新加状态
	private String prevStatus; //上一条记录车辆状态  R 行车 S 停车
	private long prevGpsTime;//上一条记录gps时间
	

	private OilmassChangedDetail prevOilmassChangedDetail;
	
	private List<OilmassChangedDetail> oilMonitor_ls = new ArrayList<OilmassChangedDetail>();//油量监控结果数据
	private List<OilmassChangedDetail> S_oilMonitor_ls = new ArrayList<OilmassChangedDetail>();//停车状态下油量监控数据缓存，行车或分析结束时合并到oilMonitor_ls
	
	private List<OilmassChangedDetail> R_1min_ls = new ArrayList<OilmassChangedDetail>();//行车状态 1分钟数据缓存
	private List<OilmassChangedDetail> S_1min_ls = new ArrayList<OilmassChangedDetail>();//停车状态 1分钟数据缓存
	
	private double preAvgoil=0.0;//上一次油量平均值
	
	//新算法变量
	private List<OilmassChangedDetail> onemin_ls = new ArrayList<OilmassChangedDetail>();//行车状态 1分钟数据缓存
	private List<OilmassChangedDetail> last_onemin_ls = new ArrayList<OilmassChangedDetail>();//行车状态 上1分钟数据缓存
	
	public Deque<OilmassChangedDetail> getAccOpenOilQeque() {
		return accOpenOilQeque;
	}

	public void setAccOpenOilQeque(Deque<OilmassChangedDetail> accOpenOilQeque) {
		this.accOpenOilQeque = accOpenOilQeque;
	}
	
	public void cloneAccOpenDeque(Deque<OilmassChangedDetail> qeque){
		accOpenOilQeque.clear();
		Iterator<OilmassChangedDetail> it = qeque.iterator();
		while(it.hasNext()){
			accOpenOilQeque.addLast(it.next());
		}
	}

	public Deque<OilmassChangedDetail> getAccCloseOilQeque() {
		return accCloseOilQeque;
	}

	public void setAccCloseOilQeque(Deque<OilmassChangedDetail> accCloseOilQeque) {
		this.accCloseOilQeque = accCloseOilQeque;
	}

	public boolean isAccOpen() {
		return isAccOpen;
	}

	public void setAccOpen(boolean isAccOpen) {
		this.isAccOpen = isAccOpen;
	}

	public long getAccOpenGpsTime() {
		return accOpenGpsTime;
	}

	public void setAccOpenGpsTime(long accOpenGpsTime) {
		this.accOpenGpsTime = accOpenGpsTime;
	}

	public boolean isAccClose() {
		return isAccClose;
	}

	public void setAccClose(boolean isAccClose) {
		this.isAccClose = isAccClose;
	}

	public long getAccCloseGpsTime() {
		return accCloseGpsTime;
	}

	public void setAccCloseGpsTime(long accCloseGpsTime) {
		this.accCloseGpsTime = accCloseGpsTime;
	}
	
	public int getAccCloseAvgOil(){
		int temp =0;
		Iterator<OilmassChangedDetail> oilIt = accCloseOilQeque.iterator();
		while(oilIt.hasNext()){
			temp = temp + oilIt.next().getOil();
		}// End while
		
		return temp / accCloseOilQeque.size();
	}
	
	public int getAccOpenAvgOil(){
		int temp =0;
		Iterator<OilmassChangedDetail> oilIt = accOpenOilQeque.iterator();
		while(oilIt.hasNext()){
			temp = temp + oilIt.next().getOil();
		}// End while
	
		return temp / accOpenOilQeque.size();
	}
	
	/****
	 * 根据一分钟所有点获取ACC开状态下点。
	 * 判断逻辑：一分钟油箱油量值不为0的点，去掉最大值和最小值，求平均值
	 * 
	 * 
	 * @return
	 */
	/*public OilmassChangedDetail getAccOpenOilPoint(){
		int temp =0;
		int minV = 0;
		int maxV = 0;
		int count = 0;
		Iterator<OilmassChangedDetail> oilIt = accOpenOilQeque.iterator();
		OilmassChangedDetail oil = null;
		
		while(oilIt.hasNext()){
			oil = oilIt.next();
			int v = oil.getCurr_oilmass();
			if(count != 0){
				minV = Math.min(v, minV);
				maxV = Math.max(v, maxV);
				
			}else{
				maxV = minV = v;
			}
			temp = temp + v;
			count++;
		}// End while
		
		if(count >2){
			temp = (temp - maxV - minV)/(count -2); 
		}else{
			temp = temp/count;	
		}
		
		if(oil != null){ 
			oil.setCurr_oilmass(temp/10); 
		}
		
		return oil;
	}*/
	
	/****
	 * 根据一分钟所有点获取ACC开状态下点。
	 * 判断逻辑：一分钟油箱油量值不为0的点，去掉最大值和最小值，求平均值
	 * 
	 * 
	 * @return
	 */
/*	public OilmassChangedDetail getCloseOpenOilPoint(){
		int temp =0;
		int minV = 0;
		int maxV = 0;
		int count = 0;
		Iterator<OilmassChangedDetail> oilIt = accCloseOilQeque.iterator();
		OilmassChangedDetail oil = null;
		
		while(oilIt.hasNext()){
			oil = oilIt.next();
			int v = oil.getCurr_oilmass();
			if(count != 0){
				minV = Math.min(v, minV);
				maxV = Math.max(v, maxV);
				
			}else{
				maxV = minV = v;
			}
			temp = temp + v;
			count++;
		}// End while
		
		if(count >2){
			temp = (temp - maxV - minV)/(count -2); 
		}else{
			temp = temp/count;	
		}
		if(oil != null){
			oil.setCurr_oilmass(temp/10);
		}
		return oil;
	}*/
	
	
	public void resetValue(){
		isAccOpen = false; // 标记ACC开
		
		accOpenGpsTime = 0; // 标记ACC开GpsTime 
		
		isAccClose = false; // 标记ACC关
		
		accCloseGpsTime = 0; // 标记ACC关GpsTime 
		
		accOpenOilQeque.clear();
		
		accCloseOilQeque.clear();
	}

	public String getPrevStatus() {
		return prevStatus;
	}

	public void setPrevStatus(String prevStatus) {
		this.prevStatus = prevStatus;
	}

	public long getPrevGpsTime() {
		return prevGpsTime;
	}

	public void setPrevGpsTime(long prevGpsTime) {
		this.prevGpsTime = prevGpsTime;
	}
	/**
	 * 偷油加油判断
	 * 
	 * 实现思路：
	 * 行车时油量监控：排除异常点后每分钟输出一个平均油量，一分钟结束点时间即为监控点时间,油量变动为正常
	 * 停车时油量监控：对停车时数据每分钟生成一个油量监控点，油量、时间、类型同行车时设置；存入停车油量监控缓存中；
	 * 				      对缓存中数据进行分析，以油量正常点为基准判断油量变动点
	 * @param currStatus
	 * @param po
	 */
	public void addTrackPoint(String currStatus,OilmassChangedDetail po){
		//判断加油或偷油
		if ("S".equals(currStatus)){
			//如果上次行车数据未输出，则输出行车数据
			if (R_1min_ls!=null&&R_1min_ls.size()>0){
				oilMonitor_ls.add(getOilMonitorPointOfR());
				//清空
				R_1min_ls=null;
				R_1min_ls = new ArrayList<OilmassChangedDetail>();
			}
			if (S_1min_ls.size()>0){
				OilmassChangedDetail firstDetail = S_1min_ls.get(0);
				if ((po.getUtc()-firstDetail.getUtc())>1*60*1000){					
					S_oilMonitor_ls.add(getOilMonitorPointOfS());
					//清空
					S_1min_ls=null;
					S_1min_ls = new ArrayList<OilmassChangedDetail>();
				}
			}
			S_1min_ls.add(po);
		}else if ("R".equals(currStatus)){
			//行车时首先对停车时产生的油量监控数据进行合并
			mergeStopedMonitorPoint();
			if (R_1min_ls.size()>0){
				//判断异常点
				OilmassChangedDetail lastDetail = R_1min_ls.get(R_1min_ls.size()-1);
				//单位时间油量变化 L/s  按照时间计算，降速大于0.5升/秒、升速大于1升/秒，则后点是异常点；
				double oneSecondOilChange = ((po.getCurr_oilmass()-lastDetail.getCurr_oilmass())*0.1)/((po.getUtc()-lastDetail.getUtc())/1000);
				if (oneSecondOilChange>1||oneSecondOilChange<-0.5){
					//异常点进行排除
				}else{
					//判断是否够一分钟，够一分钟时向缓存中添加数据
					OilmassChangedDetail firstDetail = R_1min_ls.get(0);
					if ((po.getUtc()-firstDetail.getUtc())>1*60*1000){
						oilMonitor_ls.add(getOilMonitorPointOfR());
						//清空
						R_1min_ls=null;
						R_1min_ls = new ArrayList<OilmassChangedDetail>();
					}
					R_1min_ls.add(po);
				}
			}else{
				R_1min_ls.add(po);
			}
		}
	}
	
	/**
	 * 获取行车状态下油量监控点
	 * @return
	 */
	public OilmassChangedDetail getOilMonitorPointOfR(){
		OilmassChangedDetail lastDetail = R_1min_ls.get(R_1min_ls.size()-1);
		//计算平均油量
		Double totalOilOfOneMin = 0.0;
		for (int i = 0;i<R_1min_ls.size();i++){
			totalOilOfOneMin += R_1min_ls.get(i).getCurr_oilmass();
		}
		double avgOil = totalOilOfOneMin/R_1min_ls.size();
		
		if (preAvgoil>0.0){
			if (avgOil<=preAvgoil){
				lastDetail.setCurr_oilmass(avgOil);
				preAvgoil = avgOil;
			}else{
				lastDetail.setCurr_oilmass(preAvgoil);
			}
		}else{
			lastDetail.setCurr_oilmass(avgOil);
			preAvgoil = avgOil;
		}
		
		lastDetail.setChangeType("00");
		return lastDetail;
	}
	
	/**
	 * 获取停车状态下油量监控点油量值
	 * @return
	 */
	public OilmassChangedDetail getOilMonitorPointOfS(){
		OilmassChangedDetail lastDetail = S_1min_ls.get(S_1min_ls.size()-1);
		//计算平均油量
		Double totalOilOfOneMin = 0.0;
		for (int i = 0;i<S_1min_ls.size();i++){
			totalOilOfOneMin += S_1min_ls.get(i).getCurr_oilmass();
		}
		
		double avgOil = totalOilOfOneMin/S_1min_ls.size();
		
		//停车状态下
		if (preAvgoil>0.0){
			//如果本次油量-上次油量的 所获结果的绝对值>=5L,则
			if (avgOil-preAvgoil>=5*10){//加油
				lastDetail.setCurr_oilmass(avgOil);
				preAvgoil = avgOil;
			}else if (avgOil-preAvgoil>=0){//油量正常
				lastDetail.setCurr_oilmass(preAvgoil);
			}else{//油量减小
					lastDetail.setCurr_oilmass(avgOil);
					preAvgoil = avgOil;
			}
		}else{
			lastDetail.setCurr_oilmass(avgOil);
			preAvgoil = avgOil;
		}
		
		lastDetail.setChangeType("00");
		return lastDetail;
	}
	/**
	 * 合并停车时产生的监控点
	 */
  public void mergeStopedMonitorPoint(){
	  if (S_1min_ls!=null&&S_1min_ls.size()>0){
		  S_oilMonitor_ls.add(getOilMonitorPointOfS());
		  S_1min_ls = null;
		  S_1min_ls = new ArrayList<OilmassChangedDetail>();
	  }
	  if (S_oilMonitor_ls!=null&&S_oilMonitor_ls.size()>0){
		  
		  OilmassChangedDetail firstVo = S_oilMonitor_ls.get(0);
		  OilmassChangedDetail lastVo = S_oilMonitor_ls.get(S_oilMonitor_ls.size()-1);
		  //如果开始点和结束点相差小于2分钟,则不进行加油偷油判断
		  if ((lastVo.getUtc()-firstVo.getUtc())<2*60*1000){
			  //判断油量
			  double tmpOil1 = oilMonitor_ls.get(oilMonitor_ls.size()-1).getCurr_oilmass();
			  for (int i=0;i<S_oilMonitor_ls.size();i++){
				  OilmassChangedDetail tmpVo = S_oilMonitor_ls.get(i);
				  if (tmpVo.getCurr_oilmass()>tmpOil1){
					  tmpVo.setCurr_oilmass(tmpOil1);
				  }else{
					  tmpOil1 = tmpVo.getCurr_oilmass();
				  }
				  tmpVo.setChangeType("00");
				  oilMonitor_ls.add(tmpVo);
			  }
		  }else{
			  //进行加油偷油判断
			  String changeFlag = ""; // -1 偷油  1加油
			  int normalNum = 0;
			  boolean addOilFlag = false;
			  boolean lessOilFlag = false;
			  List<OilmassChangedDetail> addOilTmpList = new ArrayList<OilmassChangedDetail>();
			  OilmassChangedDetail prevVo = S_oilMonitor_ls.get(0);
			  OilmassChangedDetail baseVo = S_oilMonitor_ls.get(0);
			  
			  /*baseVo.setChangeType("00");
			  oilMonitor_ls.add(baseVo);*/
			  //正常 正常 添加基准点 变更基准点为当前点
			  //正常 加油/偷油  添加基准点 累计变动记录 
			  //加油/偷油  加油/偷油  添加基准点 累计变动记录
			  //偷油/加油  加油/偷油  添加基准点 累计变动记录
			  double changeOilAll = 0;
			  String lastChangeFlag = "";
			  for (int i=1;i<S_oilMonitor_ls.size();i++){
				  OilmassChangedDetail tmpVo = S_oilMonitor_ls.get(i);
				  double changeOil = tmpVo.getCurr_oilmass()-prevVo.getCurr_oilmass();
				  
				  if (changeOil>=5*10){//本次加油
					  if ("1".equals(changeFlag)){//上次加油
						  prevVo.setChangeType("00");
						  prevVo.setCurr_oilmass(baseVo.getCurr_oilmass());
						  oilMonitor_ls.add(prevVo);
						  changeOilAll += Math.abs(changeOil);
					  }else if ("-1".equals(changeFlag)){//上次偷油
						  if (changeOilAll>=10){//变动量大于等于10L，则记为偷油
							  prevVo.setChangeType("01");
							  prevVo.setChange_oilmass(changeOilAll);
						  }
						  oilMonitor_ls.add(prevVo);
						  changeOilAll = Math.abs(changeOil);
						  changeFlag = "1";
						  baseVo = prevVo;
					  }else{//上次正常
						  prevVo.setChangeType("00");
						  oilMonitor_ls.add(prevVo);
						  changeOilAll = Math.abs(changeOil);
						  changeFlag = "1";
						  baseVo = prevVo;
					  }
				  }else if (changeOil<=-5*10){//本次偷油
					  if ("1".equals(changeFlag)){//上次加油
						  if (changeOilAll>=20){//变动量大于等于20L，则记为加油
							  prevVo.setChangeType("10");
							  prevVo.setChange_oilmass(changeOilAll);
						  }
						  oilMonitor_ls.add(prevVo);
						  changeOilAll = Math.abs(changeOil);
						  changeFlag = "-1";
						  baseVo = prevVo;
					  }else if ("-1".equals(changeFlag)){//上次偷油
						  prevVo.setChangeType("00");
						  prevVo.setCurr_oilmass(baseVo.getCurr_oilmass());
						  oilMonitor_ls.add(prevVo);
						  changeOilAll += Math.abs(changeOil); 
					  }else{//上次正常
						  prevVo.setChangeType("00");
						  oilMonitor_ls.add(prevVo);
						  changeOilAll = Math.abs(changeOil);
						  changeFlag = "-1";
						  baseVo = prevVo;
					  }
				  }else{//本次正常
					  if ("1".equals(changeFlag)){//上次加油
						  if (changeOilAll>=20){//变动量大于等于20L，则记为加油
							  prevVo.setChangeType("10");
							  prevVo.setChange_oilmass(changeOilAll);
						  }
					  }else if ("-1".equals(changeFlag)){//上次偷油
						  if (changeOilAll>=10){//变动量大于等于10L，则记为偷油
							  prevVo.setChangeType("01");
							  prevVo.setChange_oilmass(changeOilAll);
						  }
					  }else{//上次正常
						  prevVo.setChangeType("00");
					  }
					  oilMonitor_ls.add(prevVo);
					  changeOilAll = 0;
					  changeFlag = "1";
					  baseVo = prevVo;
				  }
				  prevVo = tmpVo;
			  }
			  //停车最后一个点加油或偷油判断
			  if ("1".equals(changeFlag)){//上次加油
				  if (changeOilAll>=20){//变动量大于等于20L，则记为加油
					  prevVo.setChangeType("10");
					  prevVo.setChange_oilmass(changeOilAll);
				  }
			  }else if ("-1".equals(changeFlag)){//上次偷油
				  if (changeOilAll>=10){//变动量大于等于10L，则记为偷油
					  prevVo.setChangeType("01");
					  prevVo.setChange_oilmass(changeOilAll);
				  }
			  }else{//上次正常
				  prevVo.setChangeType("00");
			  }
			  oilMonitor_ls.add(prevVo);
			  changeOilAll = 0;
			  changeFlag = "";
			  //****************
			  /*for (int i=1;i<S_oilMonitor_ls.size();i++){				  
				  OilmassChangedDetail tmpVo = S_oilMonitor_ls.get(i);
				  
				  double changeOil = (tmpVo.getCurr_oilmass()-prevVo.getCurr_oilmass())*0.1;
				  if (changeOil>=5){//加油
					  if ("-1".equals(changeFlag)){
						  //合并数据
						  addOilChangePoint(changeFlag,addOilTmpList.get(addOilTmpList.size()-1),baseVo,addOilTmpList);
						  addOilTmpList = null;
						  addOilTmpList = new ArrayList<OilmassChangedDetail>();
						  baseVo = prevVo;
					  }
					  changeFlag = "1";
					  
				  }else if (changeOil<=-5){//偷油
					  if ("1".equals(changeFlag)){
						  //合并数据
						  addOilChangePoint(changeFlag,addOilTmpList.get(addOilTmpList.size()-1),baseVo,addOilTmpList);

						  addOilTmpList = null;
						  addOilTmpList = new ArrayList<OilmassChangedDetail>();
						  baseVo = prevVo;
					  }
					  changeFlag = "-1";
					  
				  }else{//正常 偷油或加油结束两分钟后无再次偷油或加油
					  normalNum++;
					  if (normalNum>=2){
						  //合并数据
						  addOilChangePoint(changeFlag,addOilTmpList.get(addOilTmpList.size()-3),baseVo,addOilTmpList);
						  //当前点的前两个点追加入结果list
						  addOilTmpList.get(addOilTmpList.size()-2).setChangeType("00");
						  addOilTmpList.get(addOilTmpList.size()-1).setChangeType("00");
						  oilMonitor_ls.add(addOilTmpList.get(addOilTmpList.size()-2));
						  oilMonitor_ls.add(addOilTmpList.get(addOilTmpList.size()-1));
						  changeFlag = "";
						  addOilTmpList = null;
						  addOilTmpList = new ArrayList<OilmassChangedDetail>();
						  
					  }
				  }
				  
				  if ("".equals(changeFlag)){
					  tmpVo.setChangeType("00");
					  oilMonitor_ls.add(tmpVo);
					  baseVo = tmpVo;
				  }else{
					  addOilTmpList.add(tmpVo);
				  }
				  
				  prevVo = tmpVo;
			  }*/
		  }
		  //合并后清空停车点缓存
		  S_oilMonitor_ls = null;
		  S_oilMonitor_ls = new ArrayList<OilmassChangedDetail>();
	  }
  }
  
  /**
   * 当日数据处理结束时合并未完成数据
   */
  public void mergeAndCloseMonitorPoint(){
	  if (R_1min_ls!=null&&R_1min_ls.size()>0){
			oilMonitor_ls.add(getOilMonitorPointOfR());
			//清空
			R_1min_ls=null;
			R_1min_ls = new ArrayList<OilmassChangedDetail>();
		}
	  mergeStopedMonitorPoint();
  }
  /**
   * 把加油偷油数据合并入结果list中
   */
  public void addOilChangePoint(String type,OilmassChangedDetail lastVo,OilmassChangedDetail baseVo,List<OilmassChangedDetail> ls){
	  double changOil =Math.abs(lastVo.getCurr_oilmass() - baseVo.getCurr_oilmass());
	  boolean flag = false;
	  if ("1".equals(type)&&changOil*0.1>20){
		  flag = true;
		  lastVo.setChangeType("10");
		  lastVo.setChange_oilmass(changOil);
		  oilMonitor_ls.add(lastVo);
	  }else if ("-1".equals(type)&&changOil*0.1>10){
		  flag = true;
		  lastVo.setChangeType("01");
		  lastVo.setChange_oilmass(changOil);
		  oilMonitor_ls.add(lastVo);
	  }
	  
	  if (!flag){
		  for (int i=0;i<ls.size();i++){
			  OilmassChangedDetail tmpVo = ls.get(i);
			  tmpVo.setChangeType("00");
			  oilMonitor_ls.add(tmpVo);
		  }
	  }
  }

public List<OilmassChangedDetail> getOilMonitor_ls() {
	return oilMonitor_ls;
}

public void setOilMonitor_ls(List<OilmassChangedDetail> oilMonitor_ls) {
	this.oilMonitor_ls = oilMonitor_ls;
}

/**
 * 曲线平滑处理
 * 1、正常情况下车辆油耗应逐步减小
 * 
 */
public void antiAliased(){
	if (oilMonitor_ls!=null&&oilMonitor_ls.size()>0){
		List<OilmassChangedDetail> tmpLs = new ArrayList<OilmassChangedDetail>();
		double prevOil = oilMonitor_ls.get(0).getCurr_oilmass();
		for (int i = 0;i<oilMonitor_ls.size();i++){
			OilmassChangedDetail tmpPo = oilMonitor_ls.get(i);
			
			if ("00".equals(tmpPo.getChangeType())){
				if (prevOil<tmpPo.getCurr_oilmass()){
					tmpPo.setCurr_oilmass(prevOil);
				}else{
					prevOil = tmpPo.getCurr_oilmass();
				}
			}else{
				prevOil = tmpPo.getCurr_oilmass();
			}
			
			tmpLs.add(tmpPo);
		}
		
		oilMonitor_ls = tmpLs;
	}
}


//算法2
public void addTrackPoint2(String currStatus,OilmassChangedDetail po){
		if (onemin_ls.size()>0){
			boolean flag = true;
			if ("R".equals(currStatus)){
				//判断异常点
				OilmassChangedDetail lastDetail = onemin_ls.get(onemin_ls.size()-1);
				//单位时间油量变化 L/s  按照时间计算，降速大于0.5升/秒、升速大于1升/秒，则后点是异常点；
				double oneSecondOilChange = ((po.getCurr_oilmass()-lastDetail.getCurr_oilmass())*0.1)/((po.getUtc()-lastDetail.getUtc())/1000);
				if (oneSecondOilChange>1||oneSecondOilChange<-0.5){
					//异常点进行排除
					flag = false;
				}
			}
			
			if (flag){
				//判断时间是否连续，如果是连续时间点则走正常流程，如果中间时间点中断，则丢弃或把这半分钟数据合并到上次记录中
				OilmassChangedDetail lastDetail = onemin_ls.get(onemin_ls.size()-1);
				if ((po.getUtc() - lastDetail.getUtc())<30*1000){//连续两条数据时间间隔小于30s认为数据是连续数据
					//判断是否够一分钟，够一分钟时向缓存中添加数据
					OilmassChangedDetail firstDetail = onemin_ls.get(0);
					if ((po.getUtc()-firstDetail.getUtc())>1*60*1000){
						S_oilMonitor_ls.add(getOilMonitorPointOfR2());
						//清空
						last_onemin_ls = onemin_ls;
						onemin_ls=null;
						onemin_ls = new ArrayList<OilmassChangedDetail>();
					}
					onemin_ls.add(po);
				}else{//把不连续的数据合并到上一点，重新计算上一点的均值
					//如果当前1分钟点集  中第一个点和上一点集最后一个点不连续，则丢弃当前1分钟点集，重新收集记录
					if (last_onemin_ls!=null&&last_onemin_ls.size()>0){
						OilmassChangedDetail lastGropDetail = last_onemin_ls.get(last_onemin_ls.size()-1);
						OilmassChangedDetail firstDetail = onemin_ls.get(0);
						
						if ((firstDetail.getUtc()-lastGropDetail.getUtc())<30*1000){
							//重新计算上一点数据
							last_onemin_ls.addAll(onemin_ls);
							onemin_ls = last_onemin_ls;
							
							S_oilMonitor_ls.remove(S_oilMonitor_ls.size()-1);
							S_oilMonitor_ls.add(getOilMonitorPointOfR2());
							//清空
							last_onemin_ls = onemin_ls;
							onemin_ls=null;
							onemin_ls = new ArrayList<OilmassChangedDetail>();
							onemin_ls.add(po);
						}else{
							onemin_ls=null;
							onemin_ls = new ArrayList<OilmassChangedDetail>();
							onemin_ls.add(po);
						}
					}else{
						//第一条记录即不连续，则拿当前数据为第一条数据
						onemin_ls=null;
						onemin_ls = new ArrayList<OilmassChangedDetail>();
						onemin_ls.add(po);
					}
				}
			}
		}else{
			onemin_ls.add(po);
		}
	}

public OilmassChangedDetail getOilMonitorPointOfR2(){
	OilmassChangedDetail lastDetail = onemin_ls.get(onemin_ls.size()-1);
	//计算平均油量
	Double totalOilOfOneMin = 0.0;
	for (int i = 0;i<onemin_ls.size();i++){
		totalOilOfOneMin += onemin_ls.get(i).getCurr_oilmass();
	}
	double avgOil = totalOilOfOneMin/onemin_ls.size();
	
	lastDetail.setCurr_oilmass(avgOil);
	lastDetail.setChangeType("00");
	return lastDetail;
}
/**
 * 标记监控点
 */
public void signMonitorPoint(){
	if (onemin_ls!=null&&onemin_ls.size()>0){
		  S_oilMonitor_ls.add(getOilMonitorPointOfR2());
		  onemin_ls = null;
		  onemin_ls = new ArrayList<OilmassChangedDetail>();
	  }
	  if (S_oilMonitor_ls!=null&&S_oilMonitor_ls.size()>0){
		  
			  //进行加油偷油判断
			  String changeFlag = ""; // -1 偷油  1加油
			  int normalNum = 0;
			  boolean addOilFlag = false;
			  boolean lessOilFlag = false;
			  List<OilmassChangedDetail> addOilTmpList = new ArrayList<OilmassChangedDetail>();
			  OilmassChangedDetail prevVo = S_oilMonitor_ls.get(0);
			  OilmassChangedDetail baseVo = S_oilMonitor_ls.get(0);
			  
			  /*baseVo.setChangeType("00");
			  oilMonitor_ls.add(baseVo);*/
			  //正常 正常 添加基准点 变更基准点为当前点
			  //正常 加油/偷油  添加基准点 累计变动记录 
			  //加油/偷油  加油/偷油  添加基准点 累计变动记录
			  //偷油/加油  加油/偷油  添加基准点 累计变动记录
			  double changeOilAll = 0;
			  String lastChangeFlag = "";
			  for (int i=1;i<S_oilMonitor_ls.size();i++){
				  OilmassChangedDetail tmpVo = S_oilMonitor_ls.get(i);
				  double changeOil = tmpVo.getCurr_oilmass()-prevVo.getCurr_oilmass();
				  
				  if (changeOil>=5*10){//本次加油
					  if ("1".equals(changeFlag)){//上次加油
						  prevVo.setChangeType("00");
						  prevVo.setCurr_oilmass(baseVo.getCurr_oilmass());

						  oilMonitor_ls.add(prevVo);
						  changeOilAll += Math.abs(changeOil);
					  }else if ("-1".equals(changeFlag)){//上次偷油
						  if (changeOilAll>=10*10){//变动量大于等于10L，则记为偷油
							  prevVo.setChangeType("01");
							  prevVo.setChange_oilmass(changeOilAll);
						  }else{
							  if (prevVo.getCurr_oilmass()>baseVo.getCurr_oilmass()){
								  prevVo.setCurr_oilmass(baseVo.getCurr_oilmass());
							  }
						  }
						  oilMonitor_ls.add(prevVo);
						  changeOilAll = Math.abs(changeOil);
						  changeFlag = "1";
						  baseVo = prevVo;
					  }else{//上次正常
						  prevVo.setChangeType("00");
						  if (prevVo.getCurr_oilmass()>baseVo.getCurr_oilmass()){
							  prevVo.setCurr_oilmass(baseVo.getCurr_oilmass());
						  }
						  oilMonitor_ls.add(prevVo);
						  changeOilAll = Math.abs(changeOil);
						  changeFlag = "1";
						  baseVo = prevVo;
					  }
				  }else if (changeOil<=-5*10){//本次偷油
					  if ("1".equals(changeFlag)){//上次加油
						  if (changeOilAll>=20*10){//变动量大于等于20L，则记为加油
							  prevVo.setChangeType("10");
							  prevVo.setChange_oilmass(changeOilAll);
							  changeFlag = "-1";
						  }else{
							  if (prevVo.getCurr_oilmass()>baseVo.getCurr_oilmass()){
								  prevVo.setCurr_oilmass(baseVo.getCurr_oilmass());
								  tmpVo.setCurr_oilmass(baseVo.getCurr_oilmass());
							  }
							  changeFlag = "";
						  }
						  oilMonitor_ls.add(prevVo);
						  changeOilAll = Math.abs(changeOil);
						  
						  baseVo = prevVo;
					  }else if ("-1".equals(changeFlag)){//上次偷油
						  prevVo.setChangeType("00");
						  prevVo.setCurr_oilmass(baseVo.getCurr_oilmass());
						  oilMonitor_ls.add(prevVo);
						  changeOilAll += Math.abs(changeOil); 
					  }else{//上次正常
						  prevVo.setChangeType("00");
						  if (prevVo.getCurr_oilmass()>baseVo.getCurr_oilmass()){
							  prevVo.setCurr_oilmass(baseVo.getCurr_oilmass());
						  }
						  oilMonitor_ls.add(prevVo);
						  changeOilAll = Math.abs(changeOil);
						  changeFlag = "-1";
						  baseVo = prevVo;
					  }
				  }else{//本次正常
					  if ("1".equals(changeFlag)){//上次加油
						  if (changeOilAll>=20*10){//变动量大于等于20L，则记为加油
							  prevVo.setChangeType("10");
							  prevVo.setChange_oilmass(changeOilAll);
						  }else{
							  if (prevVo.getCurr_oilmass()>baseVo.getCurr_oilmass()){
								  prevVo.setCurr_oilmass(baseVo.getCurr_oilmass());
								  tmpVo.setCurr_oilmass(baseVo.getCurr_oilmass());
							  }
						  }
					  }else if ("-1".equals(changeFlag)){//上次偷油
						  if (changeOilAll>=10*10){//变动量大于等于10L，则记为偷油
							  prevVo.setChangeType("01");
							  prevVo.setChange_oilmass(changeOilAll);
						  }else{
							  if (prevVo.getCurr_oilmass()>baseVo.getCurr_oilmass()){
								  prevVo.setCurr_oilmass(baseVo.getCurr_oilmass());
								  tmpVo.setCurr_oilmass(baseVo.getCurr_oilmass());
							  }
						  }
					  }else{//上次正常
						  prevVo.setChangeType("00");
						  if (prevVo.getCurr_oilmass()>baseVo.getCurr_oilmass()){
							  prevVo.setCurr_oilmass(baseVo.getCurr_oilmass());
							  tmpVo.setCurr_oilmass(baseVo.getCurr_oilmass());
						  }
					  }
					  oilMonitor_ls.add(prevVo);
					  changeOilAll = 0;
					  changeFlag = "";
					  baseVo = prevVo;
				  }
				  prevVo = tmpVo;
			  }
			  //停车最后一个点加油或偷油判断
			  if ("1".equals(changeFlag)){//上次加油
				  if (changeOilAll>=20*10){//变动量大于等于20L，则记为加油
					  prevVo.setChangeType("10");
					  prevVo.setChange_oilmass(changeOilAll);
				  }
			  }else if ("-1".equals(changeFlag)){//上次偷油
				  if (changeOilAll>=10*10){//变动量大于等于10L，则记为偷油
					  prevVo.setChangeType("01");
					  prevVo.setChange_oilmass(changeOilAll);
				  }
			  }else{//上次正常
				  prevVo.setChangeType("00");
			  }
			  oilMonitor_ls.add(prevVo);
			  changeOilAll = 0;
			  changeFlag = "";
		
		  //合并后清空停车点缓存
		  S_oilMonitor_ls = null;
		  S_oilMonitor_ls = new ArrayList<OilmassChangedDetail>();
	  }
}

/**
 *  不进行加油偷油分析，直接输出1分钟数据
 */
public void copyMonitorData(){
	if (onemin_ls!=null&&onemin_ls.size()>0){
		  S_oilMonitor_ls.add(getOilMonitorPointOfR2());
		  onemin_ls = null;
		  onemin_ls = new ArrayList<OilmassChangedDetail>();
	  }
	  if (S_oilMonitor_ls!=null&&S_oilMonitor_ls.size()>0){
		  oilMonitor_ls.addAll(S_oilMonitor_ls);
	  }
}

/**
 * 不进行任何算法过滤，直接存储原始数据
 * @param currStatus
 * @param po
 */
public void addTrackPoint3(String currStatus,OilmassChangedDetail po){
	S_oilMonitor_ls.add(po);
}

}

