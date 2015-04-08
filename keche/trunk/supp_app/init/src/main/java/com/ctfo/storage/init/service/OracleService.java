/**
 * 2014-5-22OracleService.java
 */
package com.ctfo.storage.init.service;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Map;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.init.dao.OracleDataSource;
import com.ctfo.storage.init.model.TbDvr3G;
import com.ctfo.storage.init.model.TbDvrSer;
import com.ctfo.storage.init.model.TbOrg;
import com.ctfo.storage.init.model.TbOrgInfo;
import com.ctfo.storage.init.model.TbPredefinedMsg;
import com.ctfo.storage.init.model.TbProductType;
import com.ctfo.storage.init.model.TbSim;
import com.ctfo.storage.init.model.TbSpOperator;
import com.ctfo.storage.init.model.TbSpRole;
import com.ctfo.storage.init.model.TbTerminal;
import com.ctfo.storage.init.model.TbTerminalOem;
import com.ctfo.storage.init.model.TbTerminalParam;
import com.ctfo.storage.init.model.TbTerminalProtocol;
import com.ctfo.storage.init.model.TbVehicle;
import com.ctfo.storage.init.model.ThTransferHistory;
import com.ctfo.storage.init.model.TrOperatorRole;
import com.ctfo.storage.init.model.TrRoleFunction;
import com.ctfo.storage.init.model.TrServiceunit;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTbDvr3GThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTbDvrSerThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTbOrgInfoThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTbOrgThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTbPredefinedMsgThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTbProductTypeThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTbSimThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTbSpOperatorThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTbSpRoleThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTbTerminalOemThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTbTerminalParamThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTbTerminalProtocolThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTbVehicleThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendThTransferHistoryThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTrOperatorRoleThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTrRoleFunctionThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTrServiceunitThread;
import com.ctfo.storage.init.parse.MQSendThread.MQTbTerminalThread;



/**
 * OracleService
 * 
 * 
 * @author huangjincheng
 * 2014-5-22上午09:49:52
 * 
 */
public class OracleService {
	
	private static final Logger logger = LoggerFactory.getLogger(OracleService.class);
	private Connection conn = null;
	private ResultSet rs = null;
	private Map<String, String> sqlMap;
	private PreparedStatement tbDvr3GStatement = null;
	
	private PreparedStatement tbDvrSerStatement = null;
	
	private PreparedStatement tbOrgStatement = null;
	
	private PreparedStatement tbOrgInfoStatement = null;
	
	private PreparedStatement tbPredefinedMsgStatement = null;
	
	private PreparedStatement tbProductTypeStatement = null;
	
	private PreparedStatement tbSimStatement = null;
	
	private PreparedStatement tbSpOperatorStatement = null;
	
	private PreparedStatement tbSpRoleStatement = null;
	
	private PreparedStatement tbTerminalStatement = null;
	
	private PreparedStatement tbTerminalOemStatement = null;
	
	private PreparedStatement tbTerminalParamStatement = null;
	
	private PreparedStatement tbTerminalProtocolStatement = null;
	
	private PreparedStatement tbVehicleStatement = null;
	
	private PreparedStatement thTransferHistoryStatement = null;
	
	private PreparedStatement trOperatorRoleStatement = null;
	
	private PreparedStatement trRoleFunctionStatement = null;
	
	private PreparedStatement trServiceunitStatement = null;

	
	/**
	 * 获取3G视频信息
	 * @param sql
	 * @param tbDvr3G
	 */
	public void tbDvr3GSelect(MQSendTbDvr3GThread tbDvr3GThread){
		try {
			conn = OracleDataSource.getConnection();
			tbDvr3GStatement = conn.prepareStatement(sqlMap.get("sql_TbDvr3G"));
			rs = tbDvr3GStatement.executeQuery();
			while (rs.next()) {
				TbDvr3G tbDvr3G = new TbDvr3G();
				tbDvr3G.setChannelNum(rs.getLong("CHANNEL_NUM"));
				tbDvr3G.setCreateBy(rs.getString("CREATE_BY"));
				tbDvr3G.setCreateTime(rs.getLong("CREATE_TIME"));
				tbDvr3G.setDvrId(rs.getString("DVR_ID"));
				tbDvr3G.setDvrNo(rs.getString("DVR_NO"));
				tbDvr3G.setDvrSimNum(rs.getString("DVR_SIMNUM"));
				tbDvr3G.setDvrserId(rs.getString("DVRSER_ID"));
				tbDvr3G.setEnableFlag(rs.getString("ENABLE_FLAG"));
				tbDvr3G.setEntId(rs.getString("ENT_ID"));
				tbDvr3G.setRegStatus(rs.getInt("REG_STATUS"));
				tbDvr3G.setUpdateBy(rs.getString("UPDATE_BY"));
				tbDvr3G.setUpdateTime(rs.getString("UPDATE_TIME"));
				tbDvr3G.setQueneName("TB_DVR");
				tbDvr3GThread.put(tbDvr3G);
			}
			TbDvr3G tbDvr3G = new TbDvr3G();
			tbDvr3G.setQueneName("END");
			tbDvr3GThread.put(tbDvr3G);
		} catch (SQLException e) {
			logger.error(e.getMessage(), e); 
		}finally{
			try {
				if(tbDvr3GStatement != null){
					tbDvr3GStatement.close();
				}
				if(conn != null){
					conn.close();
				}
				if(rs != null){
					rs.close();
				}
				
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
		
		
	}
	
	
	public void tbDvrSerSelect(MQSendTbDvrSerThread tbDvrSerThread){
		try {
			conn = OracleDataSource.getConnection();
			tbDvrSerStatement = conn.prepareStatement(sqlMap.get("sql_TbDvrSer"));
			rs = tbDvrSerStatement.executeQuery();	
			while (rs.next()) {
				TbDvrSer tbDvrSer = new TbDvrSer();
				tbDvrSer.setChannelNum(rs.getLong("CHANNEL_NUM"));
				tbDvrSer.setCreateBy(rs.getString("CREATE_BY"));
				tbDvrSer.setCreateTime(rs.getLong("CREATE_TIME"));
				tbDvrSer.setDvrSerMakerCode(rs.getString("DVR_MAKER_CODE"));
				tbDvrSer.setDvrSerCity(rs.getString("DVRSER_CITY"));
				tbDvrSer.setDvrSerId(rs.getString("DVRSER_ID"));
				tbDvrSer.setDvrSerIp(rs.getString("DVRSER_IP"));	
				tbDvrSer.setDvrSerName(rs.getString("DVRSER_NAME"));
				tbDvrSer.setDvrSerPassword(rs.getString("DVRSER_PASSWORD"));
				tbDvrSer.setDvrSerPort(rs.getString("DVRSER_PORT"));
				tbDvrSer.setDvrSerProvince(rs.getString("DVRSER_PROVINCE"));
				tbDvrSer.setDvrSerUsername(rs.getString("DVRSER_USERNAME"));
				tbDvrSer.setEnableFlag(rs.getString("ENABLE_FLAG"));
				tbDvrSer.setRegIp(rs.getString("REG_IP"));
				tbDvrSer.setRegPort(rs.getString("REG_PORT"));
				tbDvrSer.setUpdateBy(rs.getString("UPDATE_BY"));
				tbDvrSer.setUpdateTime(rs.getLong("UPDATE_TIME"));
				tbDvrSer.setQueneName("TB_DVRSER_TEST");
				tbDvrSerThread.put(tbDvrSer);
			}
			TbDvrSer tbDvrSer = new TbDvrSer();
			tbDvrSer.setQueneName("END");
			tbDvrSerThread.put(tbDvrSer);
		} catch (SQLException e) {
			logger.error(e.getMessage(), e);
		}finally{
			try {
				if(tbDvrSerStatement != null){
					tbDvrSerStatement.close();
				}
				if(conn != null){
					conn.close();
				}
				if(rs != null){
					rs.close();
				}
				
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
		
		
	}
	
	/**
	 * 获取企业组织表
	 * @param sql
	 * @param tbOrg
	 */
	public void tbOrgSelect(MQSendTbOrgThread tbOrgThread){
		try {
			conn = OracleDataSource.getConnection();
			tbOrgStatement = conn.prepareStatement(sqlMap.get("sql_TbOrg"));
			rs = tbOrgStatement.executeQuery();	
			while (rs.next()) {
				TbOrg tbOrg = new TbOrg();
				//tbOrg.setCenterCode(rs.getString("CENTER_CODE"));
				tbOrg.setCreateBy(rs.getString("CREATE_BY"));
				tbOrg.setCreateTime(rs.getLong("CREATE_TIME"));
				tbOrg.setEnableFlag(rs.getString("ENABLE_FLAG"));
				tbOrg.setEntId(rs.getString("ENT_ID"));
				tbOrg.setEntName(rs.getString("ENT_NAME"));	
				tbOrg.setEntState(rs.getString("ENT_STATE"));
				tbOrg.setEntType(rs.getLong("ENT_TYPE"));
				tbOrg.setIsCompany(rs.getLong("ISCOMPANY"));
				tbOrg.setMemo(rs.getString("MEMO"));
				tbOrg.setParentId(rs.getString("PARENT_ID"));
				tbOrg.setSeqCode(rs.getString("SEQ_CODE"));
				tbOrg.setUpdateBy(rs.getString("UPDATE_BY"));
				tbOrg.setUpdateTime(rs.getLong("UPDATE_TIME"));
				tbOrg.setQueneName("TB_ORG_INFO");
				tbOrgThread.put(tbOrg);
			}
			TbOrg tbOrg = new TbOrg();
			tbOrg.setQueneName("END");
			tbOrgThread.put(tbOrg);
		} catch (SQLException e) {
			logger.error("存储组织信息异常:" + e.getMessage(), e);
			
		}finally{
			try {
				if(conn != null){
					conn.close();
				}
				if(tbOrgStatement != null){
					tbOrgStatement.close();
				}
				if(rs != null){
					rs.close();
				}
			} catch (SQLException e) {
				logger.error("存储组织信息关闭资源异常:" + e.getMessage(), e);
			}
		}
		
	}
	/**
	 * 获取企业组织明细表
	 * @param sql
	 * @param tbOrg
	 */
	public void tbOrgInfoSelect(MQSendTbOrgInfoThread tbOrgInfoThread){
		try {
			conn = OracleDataSource.getConnection();
			tbOrgInfoStatement = conn.prepareStatement(sqlMap.get("sql_TbOrgInfo"));
			rs = tbOrgInfoStatement.executeQuery();
			while(rs.next()){
				TbOrgInfo tbOrgInfo = new TbOrgInfo();
				tbOrgInfo.setBusinessLicense(rs.getString("BUSINESS_LICENSE"));
				tbOrgInfo.setBusinessScope(rs.getString("BUSINESS_SCOPE"));
				tbOrgInfo.setCertificateOffice(rs.getString("CERTIFICATE_OFFICE"));
				tbOrgInfo.setCorpBoss(rs.getLong("CORP_BOSS"));
				tbOrgInfo.setCorpBusinessno(rs.getString("CORP_BUSINESSNO"));
				tbOrgInfo.setCorpCity(rs.getString("CORP_CITY"));
				tbOrgInfo.setCorpCode(rs.getString("CORP_CODE"));
				tbOrgInfo.setCorpCountry(rs.getString("CORP_COUNTRY"));
				tbOrgInfo.setCorpEconomytype(rs.getString("CORP_ECONOMYTYPE"));
				tbOrgInfo.setCorpLevel(rs.getString("CORP_LEVEL"));
				tbOrgInfo.setCorpPaystate(rs.getLong("CORP_PAYSTATE"));
				tbOrgInfo.setCorpPaytype(rs.getLong("CORP_PAYTYPE"));
				tbOrgInfo.setCorpProvince(rs.getString("CORP_PROVINCE"));
				tbOrgInfo.setCorpQuale(rs.getString("CORP_QUALE"));
				tbOrgInfo.setCreateUtc(rs.getLong("CREATE_UTC"));
				tbOrgInfo.setEntId(rs.getString("ENT_ID"));
				tbOrgInfo.setIsdeteam(rs.getString("ISDETEAM"));
				tbOrgInfo.setLicenceNo(rs.getString("LICENCE_NO"));
				tbOrgInfo.setLicenceWord(rs.getString("LICENCE_WORD"));
				tbOrgInfo.setOrgAddress(rs.getString("ORG_ADDRESS"));
				tbOrgInfo.setOrgCfax(rs.getString("ORG_CFAX"));
				tbOrgInfo.setOrgCmail(rs.getString("ORG_CMAIL"));
				tbOrgInfo.setOrgCname(rs.getString("ORG_CNAME"));
				tbOrgInfo.setOrgCno(rs.getString("ORG_CNO"));
				tbOrgInfo.setOrgCphone(rs.getString("ORG_CPHONE"));
				tbOrgInfo.setOrgCzip(rs.getString("ORG_CZIP"));
				tbOrgInfo.setOrgLogo(rs.getString("ORG_LOGO"));
				tbOrgInfo.setOrgMem(rs.getString("ORG_MEM"));
				tbOrgInfo.setOrgShortname(rs.getString("ORG_SHORTNAME"));
				tbOrgInfo.setSpecialId(rs.getString("SPECIAL_ID"));
				tbOrgInfo.setUrl(rs.getString("URL"));
				tbOrgInfo.setQueneName("TB_ORGANIZATION");
				tbOrgInfoThread.put(tbOrgInfo);
			}
			TbOrgInfo tbOrgInfo = new TbOrgInfo();
			tbOrgInfo.setQueneName("END");
			tbOrgInfoThread.put(tbOrgInfo);
		}catch (SQLException e) {
			logger.error(e.getMessage(), e);
		}finally{
			try {
				if(conn != null){
					conn.close();
				}
				if(tbOrgInfoStatement != null){
					tbOrgInfoStatement.close();
				}
				if(rs != null){
					rs.close();
				}
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
		
		
	}
	
	/**
	 * 获取预定义消息表
	 * @param sql
	 * @param tbOrg
	 */
	public void tbPredefinedMsgSelect(MQSendTbPredefinedMsgThread tbPredefinedMsgThread){
		try {
			conn = OracleDataSource.getConnection();
			tbPredefinedMsgStatement = conn.prepareStatement(sqlMap.get("sql_TbPredefinedMsg"));
			rs = tbPredefinedMsgStatement.executeQuery();
			while(rs.next()){
				TbPredefinedMsg tbPredefinedMsg = new TbPredefinedMsg();
				tbPredefinedMsg.setCreateUtc(rs.getLong("CREATE_UTC"));
				tbPredefinedMsg.setEnableFlag(rs.getInt("ENABLE_FLAG"));
				tbPredefinedMsg.setIsShared(rs.getInt("IS_SHARED"));
				tbPredefinedMsg.setMsgBody(rs.getString("MSG_BODY"));
				tbPredefinedMsg.setMsgId(rs.getString("MSG_ID"));
				tbPredefinedMsg.setMsgIdx(rs.getString("MSG_IDX"));
				tbPredefinedMsg.setMsgType(rs.getInt("MSG_TYPE"));
				tbPredefinedMsg.setOpId(rs.getString("OP_ID"));
				tbPredefinedMsg.setQueneName("TB_PREDEFINED_MSG");
				tbPredefinedMsgThread.put(tbPredefinedMsg);
			}
			TbPredefinedMsg tbPredefinedMsg = new TbPredefinedMsg();
			tbPredefinedMsg.setQueneName("END");
			tbPredefinedMsgThread.put(tbPredefinedMsg);
		}catch (SQLException e) {
			logger.error(e.getMessage(), e);
		}finally{
			try {
				if(conn != null){
					conn.close();
				}
				if(tbPredefinedMsgStatement != null){
					tbPredefinedMsgStatement.close();
				}
				if(rs != null){
					rs.close();
				}
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
		
		
	}
	
	/**
	 * 获取车辆类型表
	 * @param sql
	 * @param tbOrg
	 */
	public void tbProductTypeSelect(MQSendTbProductTypeThread tbProductThread){
		try {
			conn = OracleDataSource.getConnection();
			tbProductTypeStatement = conn.prepareStatement(sqlMap.get("sql_TbProductType"));
			rs = tbProductTypeStatement.executeQuery();
			while(rs.next()){
				TbProductType tbProductType = new TbProductType();
				tbProductType.setCodeInx(rs.getLong("CODE_IDX"));
				tbProductType.setEnableFlag(rs.getString("ENABLE_FLAG"));
				tbProductType.setProdCode(rs.getString("PROD_CODE"));
				tbProductType.setProdDesc(rs.getString("PROD_DESC"));
				tbProductType.setProdName(rs.getString("PROD_NAME"));
				tbProductType.setVbrandCode(rs.getString("VBRAND_CODE"));
				tbProductType.setQueneName("SYS_PRODUCT_TYPE");
				tbProductThread.put(tbProductType);
				
			}		
			TbProductType tbProductType = new TbProductType();
			tbProductType.setQueneName("END");
			tbProductThread.put(tbProductType);
		}catch (SQLException e) {
			logger.error(e.getMessage(), e);
		}finally{
			try {
				if(conn != null){
					conn.close();
				}
				if(tbProductTypeStatement != null){
					tbProductTypeStatement.close();
				}
				if(rs != null){
					rs.close();
				}
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
		
		
	}
	
	
	/**
	 * 获取sim卡信息表
	 * @param sql
	 * @param tbOrg
	 */
	public void tbSimSelect(MQSendTbSimThread tbSimThread){
		try {
			conn = OracleDataSource.getConnection();
			tbSimStatement = conn.prepareStatement(sqlMap.get("sql_TbSim"));
			rs = tbSimStatement.executeQuery();
			while(rs.next()){
				TbSim tbSim  = new TbSim();
				tbSim.setApn(rs.getString("APN"));
				tbSim.setBusinessId(rs.getString("BUSINESS_ID"));
				tbSim.setCity(rs.getString("CITY"));
				tbSim.setCommaddr(rs.getString("COMMADDR"));
				tbSim.setCreateBy(rs.getString("CREATE_BY"));
				tbSim.setCreateTime(rs.getLong("CREATE_TIME"));
				tbSim.setDeliveryStatus(rs.getLong("DELIVERY_STATUS"));
				tbSim.setEnableFlag(rs.getString("ENABLE_FLAG"));
				tbSim.setEntId(rs.getString("ENT_ID"));
				tbSim.setExpireTime(rs.getLong("EXPIRE_TIME"));
				tbSim.setIccidElectron(rs.getString("ICCID_ELECTRON"));
				tbSim.setIccidPrint(rs.getString("ICCID_PRINT"));
				tbSim.setImsi(rs.getString("IMSI"));
				tbSim.setLastPayTime(rs.getLong("LAST_PAY_TIME"));
				tbSim.setOpenTime(rs.getLong("OPEN_TIME"));
				tbSim.setPassword(rs.getString("PASSWORD"));
				tbSim.setPin(rs.getString("PIN"));
				tbSim.setProvince(rs.getString("PROVINCE"));
				tbSim.setPuk(rs.getString("PUK"));
				tbSim.setRealcommanddr(rs.getString("REALCOMMADDR"));
				tbSim.setSid(rs.getString("SID"));
				tbSim.setSimState(rs.getString("SIM_STATE"));
				tbSim.setSudesc(rs.getString("SUDESC"));
				tbSim.setSvcStart(rs.getLong("SVC_START"));
				tbSim.setSvcStop(rs.getLong("SVC_STOP"));
				tbSim.setUpdateBy(rs.getString("UPDATE_BY"));
				tbSim.setUpdateTime(rs.getLong("UPDATE_TIME"));
				tbSim.setQueneName("TB_SIM");
				tbSimThread.put(tbSim);
				
			}
			TbSim tbSim  = new TbSim();
			tbSim.setQueneName("END");
			tbSimThread.put(tbSim);
		}catch (SQLException e) {
			logger.error(e.getMessage(), e);
		}finally{
			try {
				if(conn != null){
					conn.close();
				}
				if(tbSimStatement != null){
					tbSimStatement.close();
				}
				if(rs != null){
					rs.close();
				}
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
		
	}
	
	/**
	 * 获取系统访问用户表
	 * @param sql
	 * @param tbOrg
	 */
	public void tbSpOperatorSelect(MQSendTbSpOperatorThread tbSpOperatorThread){
		try {
			conn = OracleDataSource.getConnection();
			tbSpOperatorStatement = conn.prepareStatement(sqlMap.get("sql_TbSpOperator"));
			rs = tbSpOperatorStatement.executeQuery();
			while(rs.next()){
				TbSpOperator tbSpOperator = new TbSpOperator();
				tbSpOperator.setCreateBy(rs.getString("CREATE_BY"));
				tbSpOperator.setCreateTime(rs.getLong("CREATE_TIME"));
				tbSpOperator.setEnableFlag(rs.getString("ENABLE_FLAG"));
				tbSpOperator.setEntId(rs.getString("ENT_ID"));
				tbSpOperator.setImsi(rs.getString("IMSI"));
				tbSpOperator.setIsMember(rs.getString("IS_MEMBER"));
				tbSpOperator.setOpAddress(rs.getString("OP_ADDRESS"));
				tbSpOperator.setOpAuthcode(rs.getString("OP_AUTHCODE"));
				tbSpOperator.setOpBirth(rs.getLong("OP_BIRTH"));
				tbSpOperator.setOpCity(rs.getString("OP_CITY"));
				tbSpOperator.setOpCountry(rs.getString("OP_COUNTRY"));
				tbSpOperator.setOpDuty(rs.getString("OP_DUTY"));
				tbSpOperator.setOpEmail(rs.getString("OP_EMAIL"));
				tbSpOperator.setOpEndutc(rs.getLong("OP_ENDUTC"));
				tbSpOperator.setOpFax(rs.getString("OP_FAX"));
				tbSpOperator.setOpId(rs.getString("OP_ID"));
				tbSpOperator.setOpIdentityId(rs.getString("OP_IDENTITY_ID"));
				tbSpOperator.setOpLoginname(rs.getString("OP_LOGINNAME"));
				tbSpOperator.setOpMem(rs.getString("OP_MEM"));
				tbSpOperator.setOpMobile(rs.getString("OP_MOBILE"));
				tbSpOperator.setOpName(rs.getString("OP_NAME"));
				tbSpOperator.setOpPass(rs.getString("OP_PASS"));
				tbSpOperator.setOpPhone(rs.getString("OP_PHONE"));
				tbSpOperator.setOpProvince(rs.getString("OP_PROVINCE"));
				tbSpOperator.setOpSex(rs.getString("OP_SEX"));
				tbSpOperator.setOpStartutc(rs.getLong("OP_STARTUTC"));
				tbSpOperator.setOpStatus(rs.getString("OP_STATUS"));
				tbSpOperator.setOpSuper(rs.getString("OP_SUPER"));
				tbSpOperator.setOpType(rs.getString("OP_TYPE"));
				tbSpOperator.setOpWorkid(rs.getString("OP_WORKID"));
				tbSpOperator.setOpZip(rs.getString("OP_ZIP"));
				tbSpOperator.setPhoto(rs.getString("PHOTO"));
				tbSpOperator.setSkinStyle(rs.getString("SKIN_STYLE"));
				tbSpOperator.setUpdateBy(rs.getString("UPDATE_BY"));
				tbSpOperator.setUpdateTime(rs.getLong("UPDATE_TIME"));
				//tbSpOperator.setIsCenter(rs.getInt("IS_CENTER"));
				tbSpOperator.setQueneName("SYS_SP_OPERATOR");
				tbSpOperatorThread.put(tbSpOperator);
			}
			TbSpOperator tbSpOperator = new TbSpOperator();
			tbSpOperator.setQueneName("END");
			tbSpOperatorThread.put(tbSpOperator);
		}catch (SQLException e) {
			logger.error(e.getMessage(), e);
		}finally{
			try {
				if(conn != null){
					conn.close();
				}
				if(tbSpOperatorStatement != null){
					tbSpOperatorStatement.close();
				}
				if(rs != null){
					rs.close();
				}
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
		
		
	}
	
	/**
	 * 获取角色表
	 * @param sql
	 * @param tbOrg
	 */
	public void tbSpRoleSelect(MQSendTbSpRoleThread tbSpRoleThread){
		try {
			conn = OracleDataSource.getConnection();
			tbSpRoleStatement = conn.prepareStatement(sqlMap.get("sql_TbSpRole"));
			rs = tbSpRoleStatement.executeQuery();
			while(rs.next()){
				TbSpRole tbSpRole = new TbSpRole();
				tbSpRole.setCreateBy(rs.getString("CREATE_BY"));
				tbSpRole.setCreateTime(rs.getLong("CREATE_TIME"));
				tbSpRole.setEnableFlag(rs.getString("ENABLE_FLAG"));
				tbSpRole.setEntId(rs.getString("ENT_ID"));
				tbSpRole.setRoleDesc(rs.getString("ROLE_DESC"));
				tbSpRole.setRoleId(rs.getString("ROLE_ID"));
				tbSpRole.setRoleName(rs.getString("ROLE_NAME"));		
				tbSpRole.setRoleStatus(rs.getLong("ROLE_STATUS"));
				tbSpRole.setRoleType(rs.getString("ROLE_TYPE"));
				tbSpRole.setUpdateBy(rs.getString("UPDATE_BY"));
				tbSpRole.setUpdateTime(rs.getLong("UPDATE_TIME"));
				tbSpRole.setQueneName("SYS_SP_ROLE");
				tbSpRoleThread.put(tbSpRole);
			}
			TbSpRole tbSpRole = new TbSpRole();
			tbSpRole.setQueneName("END");
			tbSpRoleThread.put(tbSpRole);
		}catch (SQLException e) {
			logger.error(e.getMessage(), e);
		}finally{
			try {
				if(conn != null){
					conn.close();
				}
				if(tbSpRoleStatement != null){
					tbSpRoleStatement.close();
				}
				if(rs != null){
					rs.close();
				}
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
		
	}
	
	/**
	 * 获取终端表信息表
	 * @param sql
	 * @param tbOrg
	 */
	public void tbTerminalSelect(MQTbTerminalThread tbTerminalThread){
		try {
			conn = OracleDataSource.getConnection();
			tbTerminalStatement = conn.prepareStatement(sqlMap.get("sql_TbTerminal"));
			rs = tbTerminalStatement.executeQuery();
			while(rs.next()){
				TbTerminal tbTerminal = new TbTerminal();
				tbTerminal.setAuthCode(rs.getString("AUTH_CODE"));
				tbTerminal.setCommunicateId(rs.getString("COMMUNICATE_ID"));
				tbTerminal.setConfigId(rs.getLong("CONFIG_ID"));
				tbTerminal.setCreateBy(rs.getString("CREATE_BY"));
				tbTerminal.setCreateTime(rs.getLong("CREATE_TIME"));
				tbTerminal.setDeliveryStatus(rs.getInt("DELIVERY_STATUS"));
				tbTerminal.setEnableFlag(rs.getString("ENABLE_FLAG"));
				tbTerminal.setEntId(rs.getString("ENT_ID"));
				tbTerminal.setFirmwareVersion(rs.getString("FIRMWARE_VERSION"));
				tbTerminal.setHardwareVersion(rs.getString("HARDWARE_VERSION"));
				tbTerminal.setOemCode(rs.getString("OEM_CODE"));
				tbTerminal.setRegStatus(rs.getInt("REG_STATUS"));
				tbTerminal.setTerEcost(rs.getLong("TER_ECOST"));
				tbTerminal.setTerEdate(rs.getLong("TER_EDATE"));
				tbTerminal.setTerEperson(rs.getString("TER_EPERSON"));
				tbTerminal.setTerHardver(rs.getString("TER_HARDVER"));
				tbTerminal.setTerMdate(rs.getLong("TER_MDATE"));
				tbTerminal.setTerMem(rs.getString("TER_MEM"));
				tbTerminal.setTerPrice(rs.getLong("TER_PRICE"));
				tbTerminal.setTerSoftver(rs.getString("TER_SOFTVER"));
				tbTerminal.setTerState(rs.getInt("TER_STATE"));
				tbTerminal.setTerUtype(rs.getLong("TER_UTYPE"));
				tbTerminal.setTid(rs.getString("TID"));
				tbTerminal.setTmac(rs.getString("TMAC"));
				tbTerminal.setTmodelCode(rs.getString("TMODEL_CODE"));
				tbTerminal.setTprotocolId(rs.getString("TPROTOCOL_ID"));
				tbTerminal.setUpdateBy(rs.getString("UPDATE_BY"));
				tbTerminal.setUpdateTime(rs.getLong("UPDATE_TIME"));
				tbTerminal.setVideoId(rs.getString("VIDEO_ID"));
				tbTerminal.setQueneName("TB_TERMINAL");
				tbTerminalThread.put(tbTerminal);
			}
			TbTerminal tbTerminal = new TbTerminal();
			tbTerminal.setQueneName("END");
			tbTerminalThread.put(tbTerminal);
		}catch (SQLException e) {
			logger.error(e.getMessage(), e);
		}finally{
			try {
				if(conn != null){
					conn.close();
				}
				if(tbTerminalStatement != null){
					tbTerminalStatement.close();
				}
				if(rs != null){
					rs.close();
				}
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
		}
		}
		
		
	}
	
	/**
	 * 获取终端厂家编码表
	 * @param sql
	 * @param tbOrg
	 */
	public void tbTerminalOemSelect(MQSendTbTerminalOemThread tbTerminalOemThread){
		try {
			conn = OracleDataSource.getConnection();
			tbTerminalOemStatement = conn.prepareStatement(sqlMap.get("sql_TbTerminalOem"));
			rs = tbTerminalOemStatement.executeQuery();
			while(rs.next()){
				TbTerminalOem tbTerminalOem = new TbTerminalOem();
				tbTerminalOem.setAddress(rs.getString("ADDRESS"));
				tbTerminalOem.setBoss(rs.getString("BOSS"));
				tbTerminalOem.setCellphone(rs.getString("CELLPHONE"));
				tbTerminalOem.setConcateAddress(rs.getString("CONCATE_ADDRESS"));
				tbTerminalOem.setConcatePerson(rs.getString("CONCATE_PERSON"));
				tbTerminalOem.setCreateBy(rs.getString("CREATE_BY"));
				tbTerminalOem.setCreateTime(rs.getString("CREATE_TIME"));
				tbTerminalOem.setEmail(rs.getString("EMAIL"));
				tbTerminalOem.setEnableFlag(rs.getString("ENABLE_FLAG"));
				tbTerminalOem.setEnterpriseCity(rs.getString("ENTERPRISE_CITY"));
				tbTerminalOem.setEnterpriseCountry(rs.getString("ENTERPRISE_COUNTRY"));
				tbTerminalOem.setEnterpriseProvince(rs.getString("ENTERPRISE_PROVINCE"));
				tbTerminalOem.setFax(rs.getString("FAX"));
				tbTerminalOem.setFullName(rs.getString("FULL_NAME"));
				tbTerminalOem.setOemCode(rs.getString("OEM_CODE"));
				tbTerminalOem.setOemDesc(rs.getString("OEM_DESC"));
				tbTerminalOem.setOemType(rs.getString("OEM_TYPE"));
				tbTerminalOem.setShortName(rs.getString("SHORT_NAME"));
				tbTerminalOem.setTel(rs.getString("TEL"));
				tbTerminalOem.setUpdateBy(rs.getString("UPDATE_BY"));
				tbTerminalOem.setUpdateTime(rs.getString("UPDATE_TIME"));
				tbTerminalOem.setWebAddress(rs.getString("WEB_ADDRESS"));
				tbTerminalOem.setZipCode(rs.getString("ZIP_CODE"));
				tbTerminalOem.setQueneName("SYS_TERMINAL_OEM");
				tbTerminalOemThread.put(tbTerminalOem);
			}
			TbTerminalOem tbTerminalOem = new TbTerminalOem();
			tbTerminalOem.setQueneName("END");
			tbTerminalOemThread.put(tbTerminalOem);
		}catch (SQLException e) {
			logger.error(e.getMessage(), e);
		}finally{
			try {
				if(conn != null){
					conn.close();
				}
				if(tbTerminalOemStatement != null){
					tbTerminalOemStatement.close();
				}
				if(rs != null){
					rs.close();
				}
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
		
		
	}
	
	
	/**
	 * 获取终端参数信息表
	 * @param sql
	 * @param tbOrg
	 */
	public void tbTerminalParamSelect(MQSendTbTerminalParamThread tbTerminalParamThread){
		try {
			conn = OracleDataSource.getConnection();
			tbTerminalParamStatement = conn.prepareStatement(sqlMap.get("sql_TbTerminalParam"));
			rs = tbTerminalParamStatement.executeQuery();
			while(rs.next()){
				TbTerminalParam tbTerminalParam = new TbTerminalParam();
				tbTerminalParam.setCreateBy(rs.getString("CREATE_BY"));
				tbTerminalParam.setCreateTime(rs.getLong("CREATE_TIME"));
				tbTerminalParam.setParamId(rs.getString("PARAM_ID"));
				tbTerminalParam.setParamType(rs.getString("PARAM_TYPE"));
				tbTerminalParam.setParamValue(rs.getString("PARAM_VALUE"));
				tbTerminalParam.setParentCode(rs.getString("PARENT_CODE"));
				tbTerminalParam.setSeq(rs.getString("SEQ"));
				tbTerminalParam.settMac(rs.getString("T_MAC"));
				tbTerminalParam.setTid(rs.getString("TID"));
				tbTerminalParam.setUpdateBy(rs.getString("UPDATE_BY"));
				tbTerminalParam.setUpdateTime(rs.getLong("UPDATE_TIME"));
				tbTerminalParam.setQueneName("TB_TERMINAL_PARAM");
				tbTerminalParamThread.put(tbTerminalParam);
			}
			TbTerminalParam tbTerminalParam = new TbTerminalParam();
			tbTerminalParam.setQueneName("END");
			tbTerminalParamThread.put(tbTerminalParam);
		}catch (SQLException e) {
			logger.error(e.getMessage(), e);
		}finally{
			try {
				if(conn != null){
					conn.close();
				}
				if(tbTerminalParamStatement != null){
					tbTerminalParamStatement.close();
				}
				if(rs != null){
					rs.close();
				}
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
		
		
	}
	
	/**
	 * 获取终端协议信息表
	 * @param sql
	 * @param tbOrg
	 */
	public void tbTerminalProtocolSelect(MQSendTbTerminalProtocolThread tbTerminalProtocolThread){
		try {
			conn = OracleDataSource.getConnection();
			tbTerminalProtocolStatement = conn.prepareStatement(sqlMap.get("sql_TbTerminalProtocol"));
			rs = tbTerminalProtocolStatement.executeQuery();
			while(rs.next()){
				TbTerminalProtocol tbTerminalProtocol = new TbTerminalProtocol();
				tbTerminalProtocol.setCreateBy(rs.getString("CREATE_BY"));
				tbTerminalProtocol.setCreateTime(rs.getLong("CREATE_TIME"));
				tbTerminalProtocol.setOemCode(rs.getString("OEM_CODE"));
				tbTerminalProtocol.setTerminalTypeId(rs.getString("TERMINAL_TYPE_ID"));
				tbTerminalProtocol.setTprotocolId(rs.getLong("TPROTOCOL_ID"));
				tbTerminalProtocol.setTprotocolName(rs.getString("TPROTOCOL_NAME"));
				tbTerminalProtocol.setUpdateBy(rs.getString("UPDATE_BY"));
				tbTerminalProtocol.setUpdateTime(rs.getLong("UPDATE_TIME"));
				tbTerminalProtocol.setValidFlag(rs.getString("VALID_FLAG"));
				tbTerminalProtocol.setVasetTime(rs.getLong("VASET_TIME"));
				tbTerminalProtocol.setVasetUserId(rs.getString("VASET_USER_ID"));
				tbTerminalProtocol.setQueneName("TB_TERMINAL_PROTOCOL");
				tbTerminalProtocolThread.put(tbTerminalProtocol);
			}		
			TbTerminalProtocol tbTerminalProtocol = new TbTerminalProtocol();
			tbTerminalProtocol.setQueneName("END");
			tbTerminalProtocolThread.put(tbTerminalProtocol);
		}catch (SQLException e) {
			logger.error(e.getMessage(), e);
		}finally{
			try {
				if(conn != null){
					conn.close();
				}
				if(tbTerminalProtocolStatement != null){
					tbTerminalProtocolStatement.close();
				}
				if(rs != null){
					rs.close();
				}
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
		
		
	}
	
	/**
	 * 获取车辆信息表
	 * @param sql
	 * @param tbOrg
	 */
	public void tbVehicleSelect(MQSendTbVehicleThread tbVehicleThread){
		try {
			conn = OracleDataSource.getConnection();
			tbVehicleStatement = conn.prepareStatement(sqlMap.get("sql_TbVehicle"));
			rs = tbVehicleStatement.executeQuery();
			while(rs.next()){
				TbVehicle tbVehicle = new TbVehicle();
				tbVehicle.setAnnualAuditTime(rs.getLong("ANNUAL_AUDIT_TIME"));
				tbVehicle.setAnnualAuditValidityTime(rs.getLong("ANNUAL_AUDIT_VALIDITY_TIME"));
				tbVehicle.setAreaCode(rs.getString("AREA_CODE"));
				tbVehicle.setAttachedToTime(rs.getLong("ATTACHED_TO_TIME"));
				tbVehicle.setAuditAlarmDays(rs.getLong("AUDIT_ALARM_DAYS"));
				tbVehicle.setAutoSn(rs.getString("AUTO_SN"));
				tbVehicle.setBuyNo(rs.getString("BUY_NO"));
				tbVehicle.setCertificateState(rs.getString("CERTIFICATE_STATE"));
				tbVehicle.setCertificateType(rs.getString("CERTIFICATE_TYPE"));
				tbVehicle.setCityId(rs.getString("CITY_ID"));
				tbVehicle.setCoachLevel(rs.getString("COACH_LEVEL"));
				tbVehicle.setCounty(rs.getString("COUNTY"));
				tbVehicle.setCreateBy(rs.getString("CREATE_BY"));
				tbVehicle.setCreateTime(rs.getLong("CREATE_TIME"));
				tbVehicle.setCurbWeight(rs.getLong("CURB_WEIGHT"));
				tbVehicle.setDeliveryStatus(rs.getLong("DELIVERY_STATUS"));
				tbVehicle.setEbrandCode(rs.getString("EBRAND_CODE"));
				tbVehicle.setEmodelCode(rs.getString("EMISSION_STANDARD"));
				tbVehicle.setEmodelCode(rs.getString("EMODEL_CODE"));
				tbVehicle.setEnableFlag(rs.getString("ENABLE_FLAG"));
				tbVehicle.setEndTime(rs.getLong("ENDTIME"));
				tbVehicle.setEngineDisplacement(rs.getLong("ENGINE_DISPLACEMENT"));
				tbVehicle.setEngineNo(rs.getString("ENGINE_NO"));
				tbVehicle.setEntId(rs.getString("ENT_ID"));
				tbVehicle.setFirstInstalTime(rs.getLong("FIRST_INSTAL_TIME"));
				tbVehicle.setInnerCode(rs.getString("INNER_CODE"));
				tbVehicle.setInsuranceState(rs.getString("INSURANCE_STATE"));
				tbVehicle.setKm100Oiluse(rs.getLong("KM100_OILUSE"));
				tbVehicle.setMaintenanceState(rs.getString("MAINTENANCE_STATE"));
				tbVehicle.setMaximalPeople(rs.getLong("MAXIMAL_PEOPLE"));
				tbVehicle.setOiltypeId(rs.getString("OILTYPE_ID"));
				tbVehicle.setOriginCode(rs.getString("ORIGIN_CODE"));
				tbVehicle.setOutNumber(rs.getLong("OUT_NUMBER"));
				tbVehicle.setPlateColor(rs.getString("PLATE_COLOR"));
				tbVehicle.setProdCode(rs.getString("PROD_CODE"));
				tbVehicle.setProgId(rs.getString("PROG_ID"));
				tbVehicle.setRearAxleRate(rs.getDouble("REAR_AXLE_RATE"));
				tbVehicle.setRegStatus(rs.getInt("REG_STATUS"));
				tbVehicle.setReleaseDate(rs.getLong("RELEASE_DATE"));
				tbVehicle.setReviewState(rs.getString("REVIEW_STATE"));
				tbVehicle.setRoadTransport(rs.getString("ROAD_TRANSPORT"));
				tbVehicle.setSalePrice(rs.getLong("SALE_PRICE"));
				tbVehicle.setServiceNo(rs.getString("SERVICE_NO"));
				tbVehicle.setSignTime(rs.getLong("SIGNTIME"));
				tbVehicle.setStandardOil(rs.getLong("STANDARD_OIL"));
				tbVehicle.setStandardRotate(rs.getLong("STANDARD_ROTATE"));
				tbVehicle.setTotalMass(rs.getLong("TOTAL_MASS"));
				tbVehicle.setTranstypeCode(rs.getString("TRANSTYPE_CODE"));
				tbVehicle.setTyreR(rs.getDouble("TYRE_R"));
				tbVehicle.setUpdateBy(rs.getString("UPDATE_BY"));
				tbVehicle.setUpdateTime(rs.getLong("UPDATE_TIME"));
				tbVehicle.setVbrandCode(rs.getString("VBRAND_CODE"));
				tbVehicle.setVehicleBuydate(rs.getLong("VEHICLE_BUYDATE"));
				tbVehicle.setVehicleCap(rs.getLong("VEHICLE_CAP"));
				tbVehicle.setVehicleColor(rs.getString("VEHICLE_COLOR"));
				tbVehicle.setVehicleMem(rs.getString("VEHICLE_MEM"));
				tbVehicle.setVehicleMennum(rs.getLong("VEHICLE_MENNUM"));
				tbVehicle.setVehicleNo(rs.getString("VEHICLE_NO"));
				tbVehicle.setVehicleOperationId(rs.getString("VEHICLE_OPERATION_ID"));
				tbVehicle.setVehicleOperationState(rs.getString("VEHICLE_OPERATION_STATE"));
				tbVehicle.setVehiclePic(rs.getString("VEHICLE_PIC"));
				tbVehicle.setVehicleProperties(rs.getString("VEHICLE_PROPERTIES"));
				tbVehicle.setVehicleRegdate(rs.getLong("VEHICLE_REGDATE"));
				tbVehicle.setVehicleRegisterNoTime(rs.getLong("VEHICLE_REGISTER_NO_TIME"));
				tbVehicle.setVehicleState(rs.getString("VEHICLE_STATE"));
				tbVehicle.setVehicleTon(rs.getDouble("VEHICLE_TON"));
				tbVehicle.setVehicleType(rs.getString("VEHICLE_TYPE"));
				tbVehicle.setVhAccess(rs.getLong("VH_ACCESS"));
				tbVehicle.setVid(rs.getString("VID"));
				tbVehicle.setVinCode(rs.getString("VIN_CODE"));
				tbVehicle.setVoltage(rs.getString("VOLTAGE"));
				tbVehicle.setVsRefId(rs.getString("VS_REF_ID"));
				tbVehicle.setVtypeId(rs.getString("VTYPE_ID"));
				tbVehicle.setWatt(rs.getLong("WATT"));
				tbVehicle.setWheelBase(rs.getLong("WHEELBASE"));
				tbVehicle.setQueneName("TB_VEHICLE");
				tbVehicleThread.put(tbVehicle);			
			}
			TbVehicle tbVehicle = new TbVehicle();
			tbVehicle.setQueneName("END");
			tbVehicleThread.put(tbVehicle);			
		}catch (SQLException e) {
			logger.error(e.getMessage(), e);
		}finally{
			try {
				if(conn != null){
					conn.close();
				}
				if(tbVehicleStatement != null){
					tbVehicleStatement.close();
				}
				if(rs != null){
					rs.close();
				}
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
		
		
	}
	
	
	/**
	 * 获取转组历史
	 * @param sql
	 * @param tbOrg
	 */
	public void thTransferHistorySelect(MQSendThTransferHistoryThread thTransferHistoryThread){
		try {
			conn = OracleDataSource.getConnection();
			thTransferHistoryStatement = conn.prepareStatement(sqlMap.get("sql_ThTransferHistory"));
			rs = thTransferHistoryStatement.executeQuery();
			while(rs.next()){
				ThTransferHistory thTransferHistory = new ThTransferHistory();
				thTransferHistory.setGoalId(rs.getString("GOAL_ID"));
				thTransferHistory.setId(rs.getString("ID"));
				thTransferHistory.setOpId(rs.getString("OP_ID"));
				thTransferHistory.setOpTime(rs.getLong("OP_TIME"));
				thTransferHistory.setSourceId(rs.getString("SOURCE_ID"));
				thTransferHistory.setTransferId(rs.getString("TRANSFER_ID"));
				thTransferHistory.setTransferType(rs.getInt("TRANSFER_TYPE"));
				thTransferHistory.setQueneName("TH_TRANSFER_HISTORY");
				thTransferHistoryThread.put(thTransferHistory);

			}
			ThTransferHistory thTransferHistory = new ThTransferHistory();
			thTransferHistory.setQueneName("END");
			thTransferHistoryThread.put(thTransferHistory);
		}catch (SQLException e) {
			logger.error(e.getMessage(), e);
		}finally{
			try {
				if(conn != null){
					conn.close();
				}
				if(thTransferHistoryStatement != null){
					thTransferHistoryStatement.close();
				}
				if(rs != null){
					rs.close();
				}
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
		
	}
	
	
	/**
	 * 获取用户角色关系表
	 * @param sql
	 * @param tbOrg
	 */
	public void trOperatorRoleSelect(MQSendTrOperatorRoleThread trOperatorRoleThread){
		try {
			conn = OracleDataSource.getConnection();
			trOperatorRoleStatement = conn.prepareStatement(sqlMap.get("sql_TrOperatorRole"));
			rs = trOperatorRoleStatement.executeQuery();
			while(rs.next()){
				TrOperatorRole trOperatorRole = new TrOperatorRole();
				trOperatorRole.setAutoId(rs.getString("AUTO_ID"));
				trOperatorRole.setOpId(rs.getString("OP_ID"));
				trOperatorRole.setRoleId(rs.getString("ROLE_ID"));
				trOperatorRole.setQueneName("TR_OPERATOR_ROLE");
				trOperatorRoleThread.put(trOperatorRole);
				//list.add(trOperatorRole);
			}
			TrOperatorRole trOperatorRole = new TrOperatorRole();
			trOperatorRole.setQueneName("END");
			trOperatorRoleThread.put(trOperatorRole);
		}catch (SQLException e) {
			logger.error(e.getMessage(), e);
		}finally{
			try {
				if(conn != null){
					conn.close();
				}
				if(trOperatorRoleStatement != null){
					trOperatorRoleStatement.close();
				}
				if(rs != null){
					rs.close();
				}
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
	}
	
	/**
	 * 获取菜单权限角色关系表
	 * @param trRoleFunctionThread 
	 * @param sql
	 * @param tbOrg
	 */
	public void trRoleFunctionSelect(MQSendTrRoleFunctionThread trRoleFunctionThread){
		try {
			conn = OracleDataSource.getConnection();
			trRoleFunctionStatement = conn.prepareStatement(sqlMap.get("sql_TrRoleFunction"));
			rs = trRoleFunctionStatement.executeQuery();
			while(rs.next()){
				TrRoleFunction trRoleFunction = new TrRoleFunction();
				trRoleFunction.setCreateBy(rs.getString("CREATE_BY"));
				trRoleFunction.setCreateTime(rs.getLong("CREATE_TIME"));
				trRoleFunction.setEnableFlag(rs.getString("ENABLE_FLAG"));
				trRoleFunction.setFunId(rs.getString("FUN_ID"));
				trRoleFunction.setRoleId(rs.getString("ROLE_ID"));
				trRoleFunction.setUpdateBy(rs.getString("UPDATE_BY"));
				trRoleFunction.setUpdateTime(rs.getLong("UPDATE_TIME"));
				trRoleFunction.setQueneName("TR_ROLE_FUNCTION");
				trRoleFunctionThread.put(trRoleFunction);
				//list.add(trRoleFunction);
			}
			TrRoleFunction trRoleFunction = new TrRoleFunction();
			trRoleFunction.setQueneName("END");
			trRoleFunctionThread.put(trRoleFunction);
		}catch (SQLException e) {
			logger.error(e.getMessage(), e);
		}finally{
			try {
				if(conn != null){
					conn.close();
				}
				if(trRoleFunctionStatement != null){
					trRoleFunctionStatement.close();
				}
				if(rs != null){
					rs.close();
				}
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
		
	}
	
	/**
	 * 获取车卡终端关系表
	 * @param sql
	 * @param tbOrg
	 */
	public void trServiceunitSelect(MQSendTrServiceunitThread trServiceunitThread){
		try {
			conn = OracleDataSource.getConnection();
			trServiceunitStatement = conn.prepareStatement(sqlMap.get("sql_TrServiceunit"));
			rs = trServiceunitStatement.executeQuery();
			while(rs.next()){
				TrServiceunit trServiceunit = new TrServiceunit();
				trServiceunit.setCreateTime(rs.getLong("CREATE_TIME"));
				trServiceunit.setCreateUser(rs.getString("CREATE_USER"));
				trServiceunit.setDvrId(rs.getString("DVR_ID"));
				trServiceunit.setModelName(rs.getString("MODELNAME"));
				trServiceunit.setRemark(rs.getString("REMARK"));
				trServiceunit.setSid(rs.getString("SID"));	
				trServiceunit.setSuid(rs.getString("SUID"));
				trServiceunit.setTid(rs.getString("TID"));
				trServiceunit.setUpdateTime(rs.getLong("UPDATE_TIME"));
				trServiceunit.setUpdateUser(rs.getString("UPDATE_USER"));
				trServiceunit.setVid(rs.getString("VID"));
				trServiceunit.setQueneName("TR_SERVICEUNIT");
				trServiceunitThread.put(trServiceunit);
			}
			TrServiceunit trServiceunit = new TrServiceunit();
			trServiceunit.setQueneName("END");
			trServiceunitThread.put(trServiceunit);
		}catch (SQLException e) {
			logger.error(e.getMessage(), e);
		}finally{
			try {
				if(conn != null){
					conn.close();
				}
				if(trServiceunitStatement != null){
					trServiceunitStatement.close();
				}
				if(rs != null){
					rs.close();
				}
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
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
