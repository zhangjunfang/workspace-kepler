/**
 * Copyright (c) 2012, CTFO Group, Ltd. All rights reserved.
 */
package com.caits.analysisserver.addin.kcpt.statisticanalysis;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.bean.AssessMonthExtendInfo;
import com.caits.analysisserver.bean.TbCheckmonthSetBean;
import com.caits.analysisserver.database.DBAdapter;
import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.database.SQLPool;
import com.ctfo.generator.pk.GeneratorPK;

/**
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： StatisticAnalysisServer<br>
 * 功能： 自动按周期延展考核设置，包含考核月随时间自动延展，评分设置、油耗设置随考核月延展自动延展<br>
 * 描述： <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
 * -----------------------------------------------------------------------------
 * <br>
 * 修改历史<br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2012-5-18</td>
 * <td>刘小磊</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font><br>
 * 
 * @version 1.0
 * 
 * @author 刘小磊
 * @since JDK1.6
 */
public class AutoExtendAssessSetThread {
    private static final Logger log = LoggerFactory.getLogger(AutoExtendAssessSetThread.class);

    private Connection dbConnection;

    public void initAnalyser() throws SQLException {
        dbConnection = OracleConnectionPool.getConnection();
        dbConnection.setAutoCommit(false);
    }

    public void run() {
        // 执行任务
        log.info("【启动】自动延展考核周期设置任务！");
        int excuteResult = extendAssessMonthAddAllThatFollowMonth();
        if (excuteResult == 0) {
            log.info("【出错】自动延展考核周期设置任务！");
        } else if (excuteResult == 1) {
            log.info("【结束】自动延展考核周期设置任务！");
        }

        // 关闭连接
        if (dbConnection != null) {
            try {
                dbConnection.close();
            } catch (SQLException e) {
                log.error("【关闭连接出错】自动延展考核周期设置任务！", e);
            }
        }
    }

    /**
     * 延展考核月周期设置以及跟随考核月延展而延展的其他考核设置
     * @return 执行状态：0，失败；1，成功。
     */
    private int extendAssessMonthAddAllThatFollowMonth() {
        List<TbCheckmonthSetBean> allEntsLastEndTime = new ArrayList<TbCheckmonthSetBean>();
        List<TbCheckmonthSetBean> monthSetListToExtend = new ArrayList<TbCheckmonthSetBean>();
        List<AssessMonthExtendInfo> assessMonthExtendInfoList = new ArrayList<AssessMonthExtendInfo>();
        Long currentTime = new Date().getTime();

        // 查询各企业时间最靠后的考核月结束时间列表：查询ent_id和其最大end_time。
        PreparedStatement preStmt1 = null;
        ResultSet resultSet1 = null;
        StringBuffer allEnt = new StringBuffer();
        try {
        	
        	// 查询所有企业的最后一个考核月结束时间
            preStmt1 = dbConnection.prepareStatement(SQLPool.getinstance().getSql("sql_extendAssessSet_selectLastMonthsEndTime"));
            resultSet1 = preStmt1.executeQuery();
            while (resultSet1.next()) {
                TbCheckmonthSetBean tempBean = new TbCheckmonthSetBean();
                String ent_id = resultSet1.getString("ENT_ID");
                tempBean.setEntId(ent_id);
                tempBean.setEndTime(resultSet1.getLong("LAST_END_TIME"));
                allEntsLastEndTime.add(tempBean);
                allEnt.append(ent_id);
                allEnt.append(",");
            }
        } catch (SQLException e) {
            log.error("查询所有企业的最后一个考核月结束时间失败", e);
            rollback(dbConnection);
            return 0;
        } finally {
        	log.info("查询各企业列表：" + allEnt.toString());
        	allEnt.delete(0, allEnt.length());
            DBAdapter.close(preStmt1);
            DBAdapter.close(resultSet1);
        }

        // 查询各企业时间最靠后并且已结束的考核月列表，此列表即为需延展考核月列表：
        // 遍历之前列表，当end_time小于当前时间时，以ent_id和end_time为条件查询单挑记录将之放入到即将延展周期的考核周期列表中.
        PreparedStatement preStmt2 = null;
        ResultSet resultSet2 = null;
        try {
        	// 根据ent_id和end_time查询一条考核月记录
            preStmt2 = dbConnection.prepareStatement(SQLPool.getinstance().getSql("sql_extendAssessSet_selectByEntAndEndTime"));
            for (TbCheckmonthSetBean tempBean : allEntsLastEndTime) {
                if (tempBean.getEndTime() < currentTime) {
                    preStmt2.setString(1, tempBean.getEntId());
                    preStmt2.setLong(2, tempBean.getEndTime());
                    resultSet2 = preStmt2.executeQuery();
                    if (resultSet2.next()) {
                        monthSetListToExtend.add(generateCompleteBean(resultSet2));
                    }
                    allEnt.append(tempBean.getEntId());
                    allEnt.append(",");
                }
            }
        } catch (SQLException e) {
            log.error("以ent_id和end_time为条件查询单条记录失败", e);
            rollback(dbConnection);
            return 0;
        } finally {
        	log.info("check后需要延展企业ID列表：" + allEnt.toString());
        	allEnt.delete(0, allEnt.length());
            DBAdapter.close(preStmt2);
            DBAdapter.close(resultSet2);
        }

        // 遍历需延展考核月列表，修改部分字段将其变为延展考核月列表，
        // 同时记录考核月延展信息列表供后续延展其他跟随考核月延展而延展的设置使用.
        //PreparedStatement preStmtGetSeq = null;
        ResultSet resultSetGetSeq = null;
        try {
        	// 查询序列SEQ_ID的值
            //preStmtGetSeq = dbConnection.prepareStatement(SQLPool.getinstance().getSql( "sql_extendAssessSet_selectNextValueOfSeqId"));
            for (TbCheckmonthSetBean bean : monthSetListToExtend) {
                // check_time_id：主键，查询SEQ_ID获取。
                String seqId = GeneratorPK.instance().getPKString();
                /*resultSetGetSeq = preStmtGetSeq.executeQuery();
                if (resultSetGetSeq.next()) {
                    seqId = resultSetGetSeq.getString(1);
                } else {
                    // 抛出异常交由try catch语句统一日志打印和renturn错误码操作
                    log.error("获取序列SEQ_ID失败");
                }*/

                // 记录原主键和新增主键并将之加入到考核月延展信息列表中。
                AssessMonthExtendInfo assessMonthExtendInfo = new AssessMonthExtendInfo();
                assessMonthExtendInfo.setBaseId(bean.getCheckTimeId());
                assessMonthExtendInfo.setExtendedId(seqId);
                assessMonthExtendInfoList.add(assessMonthExtendInfo);

                // 改变新的主键
                bean.setCheckTimeId(seqId);

                // check_time_code: 考核月显示值，按字面意义加一月。
                try {
                    String tempStr = bean.getCheckTimeCode();
                    SimpleDateFormat format = new SimpleDateFormat("yyyy-MM");
                    Date tempDate = format.parse(tempStr);
                    Calendar tmpCalendar = Calendar.getInstance();
                    tmpCalendar.setTime(tempDate);
                    tmpCalendar.add(Calendar.MONTH, 1);
                    String newCheckTimeCode = format.format(tmpCalendar.getTime());
                    bean.setCheckTimeCode(newCheckTimeCode);
                    bean.setCheckTimeDesc(newCheckTimeCode);
                } catch (ParseException e) {
                    // 抛出异常交由try catch语句统一日志打印和renturn错误码操作
                    log.error("生成新的考核月显示字符串（check_time_code）出错", e);
                }

                // start_time,end_time：按自然月加1（注：前期已限制两个时间都不能是月末，此处无需针对月末做特殊处理）。
                Calendar startTimeCalendar = Calendar.getInstance();
                startTimeCalendar.setTimeInMillis(bean.getStartTime());
                int day = startTimeCalendar.get(Calendar.DAY_OF_MONTH);
                startTimeCalendar.add(Calendar.MONTH, 1);
                
                Calendar endTimeCalendar = Calendar.getInstance();
                endTimeCalendar.setTimeInMillis(bean.getEndTime());
                if(day != 1){ // 则不为自然月
                	endTimeCalendar.add(Calendar.MONTH, 1);
                }else{ //则设置为自然月
                	endTimeCalendar.add(Calendar.MONTH, 2);
                	endTimeCalendar.set(Calendar.DAY_OF_MONTH, 0);
                }
                bean.setStartTime(startTimeCalendar.getTimeInMillis());
                bean.setEndTime(endTimeCalendar.getTimeInMillis());

                // 创建人信息字段：ID，-1；时间，当前时间；修改人信息字段：ID，null；时间，null；生效标志字段：1
                bean.setCreateBy("-1");
                bean.setCreateTime(currentTime);
                bean.setModifyBy(null);
                bean.setModifyTime(null);
                bean.setEnableFlag("1");
            }
        } catch (Exception e) {
            log.error(e.getMessage(), e);
            rollback(dbConnection);
            return 0;
        } finally {
            //DBAdapter.close(preStmtGetSeq);
            DBAdapter.close(resultSetGetSeq);
        }

        // 即将插入数据库，提交事务以开启新事务。
        try {
            dbConnection.commit();
        } catch (SQLException e) {
            log.error("提交事务失败", e);
            rollback(dbConnection);
            return 0;
        }

        // 将延展考核月列表保存到数据库。
        PreparedStatement preStmtInsert = null;
        try {
        	// 插入一条新记录到tb_checkmonth_set表中
            preStmtInsert = dbConnection.prepareStatement(SQLPool.getinstance().getSql("sql_extendAssessSet_insertToMonthSet"));
            for (TbCheckmonthSetBean bean : monthSetListToExtend) {
            	log.info("企业ID>>" + bean.getEntId() + "---延展考核月>>" +  bean.getCheckTimeCode());
                preStmtInsert.setString(1, bean.getCheckTimeId());
                preStmtInsert.setString(2, bean.getEntId());
                preStmtInsert.setString(3, bean.getCheckTimeCode());
                preStmtInsert.setString(4, bean.getCheckTimeDesc());
                preStmtInsert.setLong(5, bean.getStartTime());
                preStmtInsert.setLong(6, bean.getEndTime());
                preStmtInsert.setString(7, bean.getCreateBy());
                preStmtInsert.setLong(8, bean.getCreateTime());
                // 修改人和修改时间是null不用插入
                preStmtInsert.setString(9, bean.getEnableFlag());

                preStmtInsert.addBatch();
            }

            preStmtInsert.executeBatch();

        } catch (SQLException e) {
            log.error("插入延展考核月列表到数据库失败", e);
            rollback(dbConnection);
            return 0;
        } finally {
            DBAdapter.close(preStmtInsert);
        }
        
        // 提交事务以统一插入数据。
        try {
            dbConnection.commit();
        } catch (SQLException e) {
            log.error("提交延展结果插入数据库事务时出错", e);
            rollback(dbConnection);
            return 0;
        }

        // 根据考核月延展信息列表延展考核评分设置和考核油耗设置
        try {
            extendOilAssessSetByMonthExtend(assessMonthExtendInfoList);
            dbConnection.commit();
            
            extendScoreAssessSetByMonthExtend(assessMonthExtendInfoList);
        } catch (SQLException e) {
            log.error("延展随考核月而延展的考核评分设置和考核油耗设置失败", e);
            rollback(dbConnection);
            return 0;
        }

        // 提交事务以统一插入数据。
        try {
            dbConnection.commit();
        } catch (SQLException e) {
            log.error("提交延展结果插入数据库事务时出错", e);
            rollback(dbConnection);
            return 0;
        }

        return 1;
    };

    /**
     * 依据考核月延展信息延展油耗设置，此方法的数据库提交和异常捕获均有调用方法处理，自身不处理。
     * @param assessMonthExtendInfoList 考核月延展信息
     * @throws SQLException
     */
    private void extendOilAssessSetByMonthExtend(
            List<AssessMonthExtendInfo> assessMonthExtendInfoList) throws SQLException {
        // 使用insert by select 的形式直接以sql语句执行延展。
        // 根据基础考核月Id从tb_assessoil_set表中查询数据，修改部分字段后插入到原表。
        // 主键字段，对应序列的下一个值
        // check_time_id：延展考核月Id
        // 创建人信息字段：ID，-1；时间，当前时间；修改人信息字段：ID，null；时间，null；
        PreparedStatement preStmt = null;
        PreparedStatement preStmt0 = null;
        ResultSet rs = null;
        try {
            long curTime = new Date().getTime();
            // >复制并修改check_time_id后插入到tb_assessoil_set表中
            preStmt = dbConnection.prepareStatement(SQLPool.getinstance().getSql( "sql_queryAssessSetWithCheckTimeId"));
            preStmt0 = dbConnection.prepareStatement(SQLPool.getinstance().getSql( "sql_saveAssessSetInfo"));
            
            for (AssessMonthExtendInfo extendInfo : assessMonthExtendInfoList) {
                preStmt.setString(1, extendInfo.getBaseId());
                rs = preStmt.executeQuery();
                //循环保存扩展的考核月信息
                int count=0;
                while(rs.next()){
                	preStmt0.setString(1, GeneratorPK.instance().getPKString());
                	preStmt0.setString(2, rs.getString("EFFECTTYPE_CODE"));
                	preStmt0.setString(3, extendInfo.getExtendedId());
                	preStmt0.setString(4, rs.getString("VBRAND_CODE"));
                	preStmt0.setString(5, rs.getString("VMODULE_CODE"));
                	preStmt0.setString(6, rs.getString("CLASSLINE_ID"));
                	preStmt0.setString(7, rs.getString("VID"));
                	preStmt0.setFloat(8, rs.getFloat("ASSESS_VALUE"));
                	preStmt0.setString(9, rs.getString("CORP_ID"));
                	preStmt0.setString(10, rs.getString("CORP_NAME"));
                	preStmt0.setString(11, rs.getString("TEAM_ID"));
                	preStmt0.setString(12, rs.getString("TEAM_NAME"));
                	preStmt0.setString(13, "-1");
                	preStmt0.setLong(14, curTime);
                	preStmt0.setNull(15, Types.VARCHAR);
                	preStmt0.setNull(16, Types.VARCHAR);
                	preStmt0.setString(17, rs.getString("ENABLE_FLAG"));
                	
                	preStmt0.addBatch();
                	
                	count++;
                	if (count>=100){
                		preStmt0.executeBatch();
                		count = 0 ;
                	}
                }
                if (count>0){
                	preStmt0.executeBatch();
                }
            }

        } finally {
        	if (rs!=null){
        		rs.close();
        	}
            DBAdapter.close(preStmt);
            DBAdapter.close(preStmt0);
        }
    }

    /**
     * 依据考核月延展信息延展评分设置，此方法的数据库提交和异常捕获均有调用方法处理，自身不处理。
     * @param assessMonthExtendInfoList 考核月延展信息
     * @throws SQLException
     */
    private void extendScoreAssessSetByMonthExtend(
            List<AssessMonthExtendInfo> assessMonthExtendInfoList) throws SQLException {
        // 使用insert by select 的形式直接以sql语句执行延展。
        // 根据基础考核月Id从b_assessoil_set表中查询数据，修改部分字段后插入到原表。
        // 主键字段，对应序列的下一个值
        // check_time_id：延展考核月Id
        // 创建人信息字段：ID，-1；时间，当前时间；修改人信息字段：ID，null；时间，null；
        PreparedStatement preStmt = null;
        PreparedStatement preStmt0 = null;
        ResultSet rs = null;
        long curTime = new Date().getTime();
        //// 复制并修改check_time_id后插入到tb_vehicle_score表中
        int count=0;
        preStmt = dbConnection.prepareStatement(SQLPool.getinstance().getSql( "sql_queryVehicleScoreSetWithCheckTimeId"));
        preStmt0 = dbConnection.prepareStatement(SQLPool.getinstance().getSql( "sql_saveTbVehicleScore"));
        for (AssessMonthExtendInfo extendInfo : assessMonthExtendInfoList) {
        	preStmt.setString(1, extendInfo.getBaseId());
        	rs = preStmt.executeQuery();
        	while(rs.next()){
        		preStmt0.setString(1, GeneratorPK.instance().getPKString());
        		preStmt0.setString(2, extendInfo.getExtendedId());
        		preStmt0.setLong(3, rs.getLong("OIL_RIGHT"));
        		preStmt0.setLong(4, rs.getLong("SPEEDING_RIGHT"));
        		preStmt0.setLong(5, rs.getLong("SPEEDING_MIN"));
        		preStmt0.setLong(6, rs.getLong("SPEEDING_MAX"));
        		preStmt0.setLong(7, rs.getLong("RPM_RIGHT"));
        		preStmt0.setLong(8, rs.getLong("RPM_MIN"));
        		preStmt0.setLong(9, rs.getLong("RPM_MAX"));
        		preStmt0.setLong(10, rs.getLong("LONGIDLE_RIGHT"));
        		
        		preStmt0.setLong(11, rs.getLong("LONGIDLE_MIN"));
        		preStmt0.setLong(12, rs.getLong("LONGIDLE_MAX"));
        		preStmt0.setLong(13, rs.getLong("GEARGLIDE_RIGHT"));
        		preStmt0.setLong(14, rs.getLong("GEARGLIDE_MIN"));
        		preStmt0.setLong(15, rs.getLong("GEARGLIDE_MAX"));
        		preStmt0.setLong(16, rs.getLong("URGENT_SPEEDUP_RIGHT"));
        		preStmt0.setLong(17, rs.getLong("URGENT_SPEEDUP_MIN"));
        		preStmt0.setLong(18, rs.getLong("URGENT_SPEEDUP_MAX"));
        		preStmt0.setLong(19, rs.getLong("AIRCOND_RIGHT"));
        		preStmt0.setLong(20, rs.getLong("AIRCOND_MIN"));
        		
        		preStmt0.setLong(21, rs.getLong("AIRCOND_MAX"));
        		preStmt0.setLong(22, rs.getLong("ECONOMIC_RIGHT"));
        		preStmt0.setLong(23, rs.getLong("ECONOMIC_MIN"));
        		preStmt0.setLong(24, rs.getLong("ECONOMIC_MAX"));
        		preStmt0.setLong(25, rs.getLong("SAFE_RIGHT"));
        		preStmt0.setLong(26, rs.getLong("ANSPEED_RIGHT"));
        		preStmt0.setLong(27, rs.getLong("ANSPEED_MIN"));
        		preStmt0.setLong(28, rs.getLong("ANSPEED_MAX"));
        		preStmt0.setLong(29, rs.getLong("ANGEARGLIDE_RIGHT"));
        		preStmt0.setLong(30, rs.getLong("ANGEARGLIDE_MIN"));
        		
        		preStmt0.setLong(31, rs.getLong("ANGEARGLIDE_MAX"));
        		preStmt0.setLong(32, rs.getLong("ANURGENT_SPEEDUP_RIGHT"));
        		preStmt0.setLong(33, rs.getLong("ANURGENT_SPEEDUP_MIN"));
        		preStmt0.setLong(34, rs.getLong("ANURGENT_SPEEDUP_MAX"));
        		preStmt0.setLong(35, rs.getLong("FATIGUE_RIGHT"));
        		preStmt0.setLong(36, rs.getLong("FATIGUE_MIN"));
        		preStmt0.setLong(37, rs.getLong("FATIGUE_MAX"));
        		preStmt0.setLong(38, rs.getLong("OILWEAR_RIGHT"));
        		preStmt0.setLong(39, rs.getLong("OILWEARCHK_RIGHT"));
        		preStmt0.setString(40, "-1");
        		
                preStmt0.setLong(41, curTime);
        		preStmt0.setNull(42, Types.VARCHAR);
        		preStmt0.setNull(43, Types.INTEGER);
        		preStmt0.setString(44, rs.getString("ENABLE_FLAG"));
        		preStmt0.setString(45, rs.getString("CORP_ID"));
        		preStmt0.setLong(46, rs.getLong("SPECIAL_RECORD_FLAG"));
        		preStmt0.setLong(47, rs.getLong("URGENT_SPEEDDOWN_RIGHT"));
        		preStmt0.setLong(48, rs.getLong("URGENT_SPEEDDOWN_MIN"));
        		preStmt0.setLong(49, rs.getLong("URGENT_SPEEDDOWN_MAX"));
        		preStmt0.setLong(50, rs.getLong("ANURGENT_SPEEDDOWN_RIGHT"));
        		
        		preStmt0.setLong(51, rs.getLong("ANURGENT_SPEEDDOWN_MIN"));
        		preStmt0.setLong(52, rs.getLong("ANURGENT_SPEEDDOWN_MAX"));
        		
        		preStmt0.addBatch();
        		count++;
        		if (count>=100){
        			preStmt0.executeBatch();
        			count=0;
        		}
        	}
        	if (count>0){
        		preStmt0.executeBatch();
        	}
        }
    }

    /**
     * 将TbCheckmonthSetBean从ResultSet当前记录转换生成bean
     * @param resultSet
     * @return
     * @throws SQLException
     */
    private TbCheckmonthSetBean generateCompleteBean(ResultSet resultSet) throws SQLException {
        TbCheckmonthSetBean bean = new TbCheckmonthSetBean();
        bean.setCheckTimeId(resultSet.getString("CHECK_TIME_ID"));
        bean.setEntId(resultSet.getString("ENT_ID"));
        bean.setCheckTimeCode(resultSet.getString("CHECK_TIME_CODE"));
        bean.setCheckTimeDesc(resultSet.getString("CHECK_TIME_DESC"));
        bean.setStartTime(resultSet.getLong("START_TIME"));
        bean.setEndTime(resultSet.getLong("END_TIME"));
        bean.setCreateBy(resultSet.getString("CREATE_BY"));
        bean.setCreateTime(resultSet.getLong("CREATE_TIME"));
        bean.setModifyBy(resultSet.getString("MODIFY_BY"));
        bean.setModifyTime(resultSet.getLong("MODIFY_TIME"));
        bean.setEnableFlag(resultSet.getString("ENABLE_FLAG"));

        return bean;
    }

    /**
     * 数据库回滚
     * @param connection
     */
    private void rollback(Connection connection) {
        try {
            connection.rollback();
        } catch (SQLException e) {
            log.error("回滚数据失败", e);
        }
    }

    public static void main(String[] args) {
        AutoExtendAssessSetThread autoExtendAssessSetThread = new AutoExtendAssessSetThread();
        try {
            autoExtendAssessSetThread.initAnalyser();
        } catch (Exception e) {
            e.printStackTrace();
        }
        autoExtendAssessSetThread.run();
    }

}
