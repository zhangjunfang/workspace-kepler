package com.ctfo.advice.service;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.List;
import java.util.Map;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.advice.dao.OracleDataSource;
import com.ctfo.advice.model.ConverTableEntity;
import com.ctfo.advice.model.MqEntity;
import com.ctfo.advice.util.ConfigLoader;
import com.ctfo.advice.util.Constant;
import com.ctfo.advice.util.ConverTableLoader;
import com.ctfo.advice.util.Tools;

/**
 * 提供对修改类数据的统一格式
 * @author yinshuaiwei
 *
 */
class UpdatePackageData implements PackageService {
	private static final Logger logger = LoggerFactory.getLogger(UpdatePackageData.class);
	private Connection conn = null;
	private ResultSet rs = null;
	private Map<String, String> sqlMap;
	private PreparedStatement statement = null;
	private MqEntity mqEntity;
	private List<ConverTableEntity> converTableEntitys;
	
	public UpdatePackageData(MqEntity mqEntity){
		this.mqEntity = mqEntity;
		sqlMap = ConfigLoader.getSqlMap();
		converTableEntitys = ConverTableLoader.getInstance().getConverTable(mqEntity.getBusinessKey());
	}

	@Override
	public String packageData() {
		StringBuilder value = new StringBuilder();
		try {
			conn = OracleDataSource.getConnection();
			for(ConverTableEntity converTableEntity : converTableEntitys){
				statement = conn.prepareStatement(sqlMap.get(converTableEntity.getSqlKey()).replace("?", "'" + mqEntity.getValue() + "'"));
				rs = statement.executeQuery();
				if(null != rs && null != converTableEntity && converTableEntity.getColumns().size() > 0){
					try {
						List<String> columnArray = converTableEntity.getColumns();
						int lenght = columnArray.size();
//						builder.append(mqEntity.getOperateType());
//						builder.append(Constant.CONSTANT_SPLIT_ATTRIBUTE);
						StringBuilder builder = new StringBuilder();
						while(rs.next()){
							builder.append("{");
							for(int i = 0 ; i < lenght; i++){
								String column = columnArray.get(i);
								if(i == lenght-1){
									builder.append(column + Constant.CONSTANT_SPLIT_ATTRIBUTE + rs.getObject(column));
									builder.append(Constant.CONSTANT_SPLIT_OVER);
								}else{
									builder.append(column + Constant.CONSTANT_SPLIT_ATTRIBUTE + rs.getObject(column) + Constant.CONSTANT_SPLIT_ATTRIBTE_VALUE);
								}
							}
							builder.append("}");
							builder.append(Constant.CONSTANT_SPLIT_ATTRIBUTE);
						}
						value.append(mqEntity.getOperateType());
						value.append(Constant.CONSTANT_SPLIT_ATTRIBUTE);
						if(builder.length() > 0){//如果builder大于0，说明查询到数据，且结尾时有多余符号，因此需要截掉多余的符号
							value.append(Tools.strToBase64(builder.substring(0,builder.length()-1)));
						}
					} catch (SQLException e) {
						logger.error("读取数据库记录异常:"+e.getMessage(), e); 
					}
				}
			}
		} catch (SQLException e) {
			logger.error(e.getMessage(), e); 
		}finally{
			try {
				if(conn != null){
					conn.close();
				}
				if(statement != null){
					statement.close();
				}
				if(rs != null){
					rs.close();
				}
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
		return value.toString();
	}

	/**
	 * 获取sqlMap的值
	 * @return sqlMap  
	 */
	public Map<String, String> getSqlMap() {
		return sqlMap;
	}

	/**
	 * 设置sqlMap的值
	 * @param sqlMap
	 */
	public void setSqlMap(Map<String, String> sqlMap) {
		this.sqlMap = sqlMap;
	}

}
