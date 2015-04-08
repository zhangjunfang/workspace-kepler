package com.ctfo.statusservice.service;


import com.alibaba.druid.pool.DruidDataSource;
import com.ctfo.statusservice.dao.OracleConnectionPool;
import com.ctfo.statusservice.model.OracleProperties;
import com.ctfo.statusservice.task.OrgActiveMQ;

public class OrgActiveMQTest {

	@SuppressWarnings("unused")
	public static void main(String[] args) {
		DruidDataSource dds = new DruidDataSource();
		dds.setOracle(true);
		dds.setUrl("jdbc:oracle:thin:@192.168.100.53:1521:ORCl");
		dds.setUsername("kcpt");
		dds.setPassword("kcpt");
		dds.setTestOnBorrow(false);
		dds.setTestOnReturn(false);
		dds.setName("DruidDataSource"); 
		
		OracleConnectionPool ocp = new OracleConnectionPool(dds);
		
		OrgActiveMQ oa = new OrgActiveMQ();
		oa.setBrokerURL("tcp://192.168.100.52:61616");
		OracleProperties oracleProperties = new OracleProperties();
		oracleProperties.setSql_orgParentSync("SELECT ORG.ENT_ID AS MOTORCADE,PAR.ENT_NAME,  ',' || (SELECT WM_CONCAT(T.ENT_ID) FROM KCPT.TB_ORGANIZATION T WHERE T.ENABLE_FLAG = 1 AND T.ENT_TYPE = 1  START WITH T.ENT_ID = ORG.PARENT_ID CONNECT BY PRIOR T.PARENT_ID = T.ENT_ID) || ',' PARENT_ID  FROM KCPT.TB_ORGANIZATION ORG,  KCPT.TB_ORGANIZATION PAR WHERE ORG.PARENT_ID=PAR.ENT_ID  AND ORG.ENABLE_FLAG = 1 AND ORG.ENT_TYPE = 2"); 
		oa.setOracleProperties(oracleProperties);
		oa.setTopicName("t_org");
		oa.start();
	}

}
