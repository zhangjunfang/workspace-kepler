package com.ctfo.statistics.alarm.handler;

import static org.junit.Assert.fail;

import java.io.File;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.junit.Test;

import com.ctfo.statistics.alarm.common.Cache;
import com.ctfo.statistics.alarm.common.ConfigLoader;
import com.ctfo.statistics.alarm.common.Utils;
import com.ctfo.statistics.alarm.dao.OracleConnectionPool;
import com.ctfo.statistics.alarm.dao.RedisConnectionPool;
import com.ctfo.statistics.alarm.model.FatigueRules;
import com.ctfo.statistics.alarm.model.NightRules;
import com.ctfo.statistics.alarm.model.OverspeedRules;
import com.ctfo.statistics.alarm.model.StatisticsParma;
import com.ctfo.statistics.alarm.model.TrackFile;
import com.ctfo.statistics.alarm.model.VehicleInfo;
import com.ctfo.statistics.alarm.service.OracleService;
import com.ctfo.statistics.alarm.task.AlarmAnalysisTask;

public class AlarmAnalysisTest {
	public AlarmAnalysisTest(){
		try {
			ConfigLoader.init(new String[]{"-d" , "E:/WorkSpace/trank/AlarmStatistics/src/config.xml", "E:/WorkSpace/trank/AlarmStatistics/src/system.properties"});
			OracleConnectionPool.init(Utils.getOracleProperties(ConfigLoader.config));
			RedisConnectionPool.init(Utils.getRedisProperties(ConfigLoader.config));
			OracleService.init();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	@Test
	public void testProcess() {
		try {
			VehicleInfo vehicleInfo = new VehicleInfo();
			vehicleInfo.setPlate("plate");
			vehicleInfo.setEntId("111");
			vehicleInfo.setEntName("test1");
			vehicleInfo.setTeamId("1");
			vehicleInfo.setTeamName("team1");
			vehicleInfo.setSpeedThreshold(90); 
			String vid = "1";
			vehicleInfo.setVid(vid); 
			String time = "20140619/000000";
			SimpleDateFormat sdf = new SimpleDateFormat("yyyyMMdd/HHmmss");
			@SuppressWarnings("unused")
			long utcBegin = sdf.parse(time).getTime();
			long utc = sdf.parse("20140619/023031").getTime();
			long startUtc = utc - 864000;
			long endUtc = utc + 864000;
			
			Map<String, VehicleInfo> vmap = new HashMap<String, VehicleInfo>();
			vmap.put(vid, vehicleInfo);
			Cache.putVehicleInfo(vmap);
			
//			疲劳驾驶规则
			Map<String,List<FatigueRules>> fatMap = new HashMap<String, List<FatigueRules>>();
			List<FatigueRules> fat = new ArrayList<FatigueRules>();
			FatigueRules f = new FatigueRules();
			f.setVid(vid);
			f.setStartUtc(startUtc);
			f.setEndUtc(endUtc);
			fat.add(f);
			fatMap.put(vid, fat);
			Cache.putAllFatigueRules(fatMap);
			
//			夜间非法运营规则
			Map<String,List<NightRules>> nightMap = new HashMap<String, List<NightRules>>();
			List<NightRules> night = new ArrayList<NightRules>();
			NightRules n = new NightRules();
			n.setVid(vid);
			n.setStartUtc(startUtc);
			n.setEndUtc(endUtc);
			n.setRunTotal(1000); 
			night.add(n);
			nightMap.put(vid, night);
			Cache.putAllNightRules(nightMap); 
			
//			超速规则
			Map<String,List<OverspeedRules>> speedMap = new HashMap<String, List<OverspeedRules>>();
			List<OverspeedRules> speed = new ArrayList<OverspeedRules>();
			OverspeedRules s = new OverspeedRules();
			s.setVid(vid);
//			startUtc = utc + 864000 + 864000;
//			endUtc = utc + 864000;
			s.setStartUtc(startUtc);
			s.setEndUtc(endUtc);
			s.setSpeedLimit(700); 
			speed.add(s);
			speedMap.put(vid, speed);
			Cache.putAllOverspeedRules(speedMap); 
			
			
			List<String> list = new ArrayList<String>();
			list.add("72447154:18471929:20140619/023011:60:0::,2,:72444558:18473292:0:607410:31404::11176:3::::2:1800:::125:2052:0:-1:::0::::::1:-1:31278613:0:1403168744784");
			list.add("72447154:18471929:20140619/023021:60:0::,4,:72444558:18473292:0:607420:31404::11176:3::::2:1000:::125:2052:0:-1:::0::::::1:-1:31278613:0:1403168744784");
//			list.add("72447154:18471929:20140619/165731:60:0::,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,:72444558:18473292:0:607430:31404::11176:3::::2:1000:::125:2052:0:-1:::0::::::1:-1:31278613:0:1403168744784");
			list.add("72447154:18471929:20140619/023031:60:0::,1,:72444558:18473292:0:607430:31404::11176:3::::2:40:::125:2052:0:-1:::0::::::1:-1:31278613:0:1403168744784");
			list.add("72447154:18471929:20140619/023041:60:0::,,1,:72444558:18473292:0:607440:31404::11176:3::::2:1000:::125:2052:0:-1:::0::::::1:-1:31278613:0:1403168744784");
			list.add("72447154:18471929:20140619/023051:60:0::,,1,:72444558:18473292:0:607450:31404::11176:3::::2:1000:::125:2052:0:-1:::0::::::1:-1:31278613:0:1403168744784");
			TrackFile trackFile = new TrackFile();
			trackFile.setName(vid);
			trackFile.setList(list);
			File file = new File("");
			List<File> fileList = new ArrayList<File>();
			fileList.add(file);
			AlarmAnalysisTask t = new AlarmAnalysisTask(1, fileList, new StatisticsParma());
			t.process(trackFile);
			
			Thread.sleep(1000); 
			

			
//			for(int i = 0; i< 24;i++){
//				String path = "D:\\test\\track\\2014\\10\\09\\" + i + ".txt" ;
//				List<String> list = FileUtils.readLines(new File(path));
//				TrackFile trackFile = new TrackFile();
//				vid = "" + String.valueOf(i);
//				Map<String, VehicleInfo> map = new HashMap<String, VehicleInfo>();
//				map.put(vid, vehicleInfo);
//				Cache.putVehicleInfo(map);
//				trackFile.setName("" + i);
//				
//				trackFile.setList(list);
//				TrackFileProcess t = new TrackFileProcess(1);
//				t.process(trackFile);
//			}
//			Thread.sleep(3000); 
		} catch (Exception e) {
			e.printStackTrace();
			fail("Not yet implemented");
		}
	}

}
