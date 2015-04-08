package com.ctfo.storage.dispatch.service;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.util.List;
import java.util.Map;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.dispatch.dao.MySqlDataSource;
import com.ctfo.storage.dispatch.model.TbDvr3G;
import com.ctfo.storage.dispatch.model.TbDvrSer;
import com.ctfo.storage.dispatch.model.TbOrg;
import com.ctfo.storage.dispatch.model.TbOrgInfo;
import com.ctfo.storage.dispatch.model.TbPredefinedMsg;
import com.ctfo.storage.dispatch.model.TbProductType;
import com.ctfo.storage.dispatch.model.TbSim;
import com.ctfo.storage.dispatch.model.TbSpOperator;
import com.ctfo.storage.dispatch.model.TbSpRole;
import com.ctfo.storage.dispatch.model.TbTerminal;
import com.ctfo.storage.dispatch.model.TbTerminalOem;
import com.ctfo.storage.dispatch.model.TbTerminalParam;
import com.ctfo.storage.dispatch.model.TbTerminalProtocol;
import com.ctfo.storage.dispatch.model.TbVehicle;
import com.ctfo.storage.dispatch.model.ThTransferHistory;
import com.ctfo.storage.dispatch.model.TrOperatorRole;
import com.ctfo.storage.dispatch.model.TrRoleFunction;
import com.ctfo.storage.dispatch.model.TrServiceunit;

/**
 * MySqlService
 * 
 * 
 * @author huangjincheng
 * 2014-5-15上午10:15:38
 * 
 */
public class MySqlService {
	private static final Logger logger = LoggerFactory.getLogger(MySqlService.class);
	
	private Connection conn = null;
	
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

	private Map<String, String> sqlMap;
	
	
	
	
	/**
	 * 批量存储3G视频信息
	 * @param sql
	 * @param tbDvr3G
	 */
	public void tbDvr3GSave(List<TbDvr3G> list){
		try {		
			conn = MySqlDataSource.getConnection();
			tbDvr3GStatement = conn.prepareStatement(sqlMap.get("sql_TbDvr3G"));
			for(TbDvr3G tbDvr3G : list){
				tbDvr3GStatement.setString(1, tbDvr3G.getChannelNum());
				tbDvr3GStatement.setString(2, tbDvr3G.getCreateBy());
				tbDvr3GStatement.setString(3, tbDvr3G.getCreateTime());
				tbDvr3GStatement.setString(4, tbDvr3G.getDvrId());
				tbDvr3GStatement.setString(5, tbDvr3G.getDvrNo());
				tbDvr3GStatement.setString(6, tbDvr3G.getDvrSimNum());
				tbDvr3GStatement.setString(7, tbDvr3G.getDvrserId());
				tbDvr3GStatement.setString(8, tbDvr3G.getEnableFlag());
				tbDvr3GStatement.setString(9, tbDvr3G.getEntId());
				tbDvr3GStatement.setInt(10, tbDvr3G.getRegStatus());
				tbDvr3GStatement.setString(11, tbDvr3G.getUpdateBy());
				tbDvr3GStatement.setString(12, tbDvr3G.getUpdateTime());
				
				tbDvr3GStatement.addBatch(); 
			}
			tbDvr3GStatement.executeBatch();
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
				
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
	}
	
	
	
	/**
	 * 批量存储3G视频服务器信息
	 * @param sql
	 * @param tbDvrSer
	 */
	public void tbDvrSerSave(List<TbDvrSer> list){
		try {
			conn = MySqlDataSource.getConnection();
			tbDvrSerStatement = conn.prepareStatement(sqlMap.get("sql_TbDvrSer"));
			for(TbDvrSer tbDvrSer : list){				
				tbDvrSerStatement.setString(1, tbDvrSer.getChannelNum());
				tbDvrSerStatement.setString(2, tbDvrSer.getCreateBy());
				tbDvrSerStatement.setString(3, tbDvrSer.getCreateTime());
				tbDvrSerStatement.setString(4, tbDvrSer.getDvrSerMakerCode());
				tbDvrSerStatement.setString(5, tbDvrSer.getDvrSerCity());
				tbDvrSerStatement.setString(6, tbDvrSer.getDvrSerId());
				tbDvrSerStatement.setString(7, tbDvrSer.getDvrSerIp());	
				tbDvrSerStatement.setString(8, tbDvrSer.getDvrSerName());
				tbDvrSerStatement.setString(9, tbDvrSer.getDvrSerPassword());
				tbDvrSerStatement.setString(10, tbDvrSer.getDvrSerPort());
				tbDvrSerStatement.setString(11, tbDvrSer.getDvrSerProvince());
				tbDvrSerStatement.setString(12, tbDvrSer.getDvrSerUsername());
				tbDvrSerStatement.setString(13, tbDvrSer.getEnableFlag());
				tbDvrSerStatement.setString(14, tbDvrSer.getRegIp());
				tbDvrSerStatement.setString(15, tbDvrSer.getRegPort());
				tbDvrSerStatement.setString(16, tbDvrSer.getUpdateBy());
				tbDvrSerStatement.setString(17, tbDvrSer.getUpdateTime());
				
				tbDvrSerStatement.addBatch();
			}
				
			tbDvrSerStatement.executeBatch();
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
				
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
		
		
	}
	
	/**
	 * 批量存储企业组织表
	 * @param sql
	 * @param tbOrg
	 */
	public void tbOrgSave(List<TbOrg> list){
		try {
			conn = MySqlDataSource.getConnection();
			tbOrgStatement = conn.prepareStatement(sqlMap.get("sql_TbOrg"));
			for(TbOrg tbOrg:list){
				tbOrgStatement.setString(1, tbOrg.getCenterCode());
				tbOrgStatement.setString(2, tbOrg.getCreateBy());
				tbOrgStatement.setString(3, tbOrg.getCreateTime());
				tbOrgStatement.setString(4, tbOrg.getEnableFlag());
				tbOrgStatement.setString(5, tbOrg.getEntId());
				tbOrgStatement.setString(6, tbOrg.getEntName());	
				tbOrgStatement.setString(7, tbOrg.getEntState());
				tbOrgStatement.setLong(8, tbOrg.getEntType());
				tbOrgStatement.setLong(9, tbOrg.getIsCompany());
				tbOrgStatement.setString(10, tbOrg.getMemo());
				tbOrgStatement.setString(11, tbOrg.getParentId());
				tbOrgStatement.setString(12, tbOrg.getSeqCode());
				tbOrgStatement.setString(13, tbOrg.getUpdateBy());
				tbOrgStatement.setString(14, tbOrg.getUpdateTime());
				tbOrgStatement.addBatch();
			}
			tbOrgStatement.executeBatch();
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
			} catch (SQLException e) {
				logger.error("存储组织信息关闭资源异常:" + e.getMessage(), e);
			}
		}
		
	}
	/**
	 * 批量存储企业组织明细表
	 * @param sql
	 * @param tbOrg
	 */
	public void tbOrgInfoSave(List<TbOrgInfo> list){
		try {
			conn = MySqlDataSource.getConnection();
			tbOrgInfoStatement = conn.prepareStatement(sqlMap.get("sql_TbOrgInfo"));
			for(TbOrgInfo tbOrgInfo:list){
				tbOrgInfoStatement.setString(1, tbOrgInfo.getBusinessLicense());
				tbOrgInfoStatement.setString(2, tbOrgInfo.getBusinessScope());
				tbOrgInfoStatement.setString(3, tbOrgInfo.getCertificateOffice());
				tbOrgInfoStatement.setLong(4, tbOrgInfo.getCorpBoss());
				tbOrgInfoStatement.setString(5,tbOrgInfo.getCorpBusinessno());
				tbOrgInfoStatement.setString(6,tbOrgInfo.getCorpCity());
				tbOrgInfoStatement.setString(7,tbOrgInfo.getCorpCode());
				tbOrgInfoStatement.setString(8,tbOrgInfo.getCorpCountry());
				tbOrgInfoStatement.setString(9,tbOrgInfo.getCorpEconomytype());
				tbOrgInfoStatement.setString(10,tbOrgInfo.getCorpLevel());
				tbOrgInfoStatement.setLong(11,tbOrgInfo.getCorpPaystate());
				tbOrgInfoStatement.setLong(12,tbOrgInfo.getCorpPaytype());
				tbOrgInfoStatement.setString(13,tbOrgInfo.getCorpProvince());
				tbOrgInfoStatement.setString(14,tbOrgInfo.getCorpQuale());
				tbOrgInfoStatement.setLong(15,tbOrgInfo.getCreateUtc());
				tbOrgInfoStatement.setString(16,tbOrgInfo.getEntId());
				tbOrgInfoStatement.setString(17, tbOrgInfo.getIsdeteam());
				tbOrgInfoStatement.setString(18, tbOrgInfo.getLicenceNo());
				tbOrgInfoStatement.setString(19, tbOrgInfo.getLicenceWord());
				tbOrgInfoStatement.setString(20, tbOrgInfo.getOrgAddress());
				tbOrgInfoStatement.setString(21, tbOrgInfo.getOrgCfax());
				tbOrgInfoStatement.setString(22, tbOrgInfo.getOrgCmail());
				tbOrgInfoStatement.setString(23, tbOrgInfo.getOrgCname());
				tbOrgInfoStatement.setString(24, tbOrgInfo.getOrgCno());
				tbOrgInfoStatement.setString(25, tbOrgInfo.getOrgCphone());
				tbOrgInfoStatement.setString(26, tbOrgInfo.getOrgCzip());
				tbOrgInfoStatement.setString(27, tbOrgInfo.getOrgLogo());
				tbOrgInfoStatement.setString(28, tbOrgInfo.getOrgMem());
				tbOrgInfoStatement.setString(29, tbOrgInfo.getOrgShortname());
				tbOrgInfoStatement.setString(30, tbOrgInfo.getSpecialId());
				tbOrgInfoStatement.setString(31, tbOrgInfo.getUrl());
				
				tbOrgInfoStatement.addBatch();
			}
			tbOrgInfoStatement.executeBatch();
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
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
	}
	
	/**
	 * 批量存储预定义消息表
	 * @param sql
	 * @param tbOrg
	 */
	public void tbPredefinedMsgSave(List<TbPredefinedMsg> list){
		try {
			conn = MySqlDataSource.getConnection();
			tbPredefinedMsgStatement = conn.prepareStatement(sqlMap.get("sql_TbPredefinedMsg"));
			for(TbPredefinedMsg tbPredefinedMsg:list){
				tbPredefinedMsgStatement.setLong(1, tbPredefinedMsg.getCreateUtc());
				tbPredefinedMsgStatement.setInt(2, tbPredefinedMsg.getEnableFlag());
				tbPredefinedMsgStatement.setInt(3, tbPredefinedMsg.getIsShared());
				tbPredefinedMsgStatement.setString(4, tbPredefinedMsg.getMsgBody());
				tbPredefinedMsgStatement.setString(5, tbPredefinedMsg.getMsgId());
				tbPredefinedMsgStatement.setString(6, tbPredefinedMsg.getMsgIdx());
				tbPredefinedMsgStatement.setLong(7, tbPredefinedMsg.getMsgType());
				tbPredefinedMsgStatement.setString(8, tbPredefinedMsg.getOpId());
				tbPredefinedMsgStatement.setString(9, tbPredefinedMsg.getCenterSourceIp());
				
				tbPredefinedMsgStatement.addBatch();
			}
			tbPredefinedMsgStatement.executeBatch();
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
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
	}
	
	/**
	 * 批量存储车辆类型表
	 * @param sql
	 * @param tbOrg
	 */
	public void tbProductTypeSave(List<TbProductType> list){
		try {
			conn = MySqlDataSource.getConnection();
			tbProductTypeStatement = conn.prepareStatement(sqlMap.get("sql_TbProductType"));
			for(TbProductType tbProductType:list){
				tbProductTypeStatement.setLong(1, tbProductType.getCodeInx());
				tbProductTypeStatement.setString(2, tbProductType.getEnableFlag());
				tbProductTypeStatement.setString(3, tbProductType.getProdCode());
				tbProductTypeStatement.setString(4, tbProductType.getProdDesc());
				tbProductTypeStatement.setString(5, tbProductType.getProdName());
				tbProductTypeStatement.setString(6, tbProductType.getVbrandCode());
				tbProductTypeStatement.addBatch();
			}
			tbProductTypeStatement.executeBatch();
		
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
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
	}
	
	
	/**
	 * 批量存储sim卡信息表
	 * @param sql
	 * @param tbOrg
	 */
	public void tbSimSave(List<TbSim> list){
		try {
			conn = MySqlDataSource.getConnection();
			tbSimStatement = conn.prepareStatement(sqlMap.get("sql_TbSim"));
			for(TbSim tbSim:list){
				tbSimStatement.setString(1, tbSim.getApn());
				tbSimStatement.setString(2, tbSim.getBusinessId());
				tbSimStatement.setString(3, tbSim.getCity());
				tbSimStatement.setString(4, tbSim.getCommaddr());
				tbSimStatement.setString(5, tbSim.getCreateBy());
				tbSimStatement.setString(6, tbSim.getCreateTime());
				tbSimStatement.setLong(7, tbSim.getDeliveryStatus());
				tbSimStatement.setString(8, tbSim.getEnableFlag());
				tbSimStatement.setString(9, tbSim.getEntId());
				tbSimStatement.setLong(10, tbSim.getExpireTime());
				tbSimStatement.setString(11, tbSim.getIccidElectron());
				tbSimStatement.setString(12, tbSim.getIccidPrint());
				tbSimStatement.setString(13, tbSim.getImsi());
				tbSimStatement.setLong(14, tbSim.getLastPayTime());
				tbSimStatement.setLong(15, tbSim.getOpenTime());
				tbSimStatement.setString(16, tbSim.getPassword());
				tbSimStatement.setString(17, tbSim.getPin());
				tbSimStatement.setString(18, tbSim.getProvince());
				tbSimStatement.setString(19, tbSim.getPuk());
				tbSimStatement.setString(20, tbSim.getRealcommanddr());
				tbSimStatement.setString(21, tbSim.getSid());
				tbSimStatement.setString(22, tbSim.getSimState());
				tbSimStatement.setString(23, tbSim.getSudesc());
				tbSimStatement.setLong(24, tbSim.getSvcStart());
				tbSimStatement.setLong(25, tbSim.getSvcStop());
				tbSimStatement.setString(26, tbSim.getUpdateBy());
				tbSimStatement.setString(27, tbSim.getUpdateTime());
				
				tbSimStatement.addBatch();
				
			}
			tbSimStatement.executeBatch();	
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
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
	}
	
	/**
	 * 批量存储系统访问用户表
	 * @param sql
	 * @param tbOrg
	 */
	public void tbSpOperatorSave(List<TbSpOperator> list){
		try {
			conn = MySqlDataSource.getConnection();
			tbSpOperatorStatement = conn.prepareStatement(sqlMap.get("sql_TbSpOperator"));
			for(TbSpOperator tbSpOperator:list){
				tbSpOperatorStatement.setString(1, tbSpOperator.getCreateBy());
				tbSpOperatorStatement.setString(2, tbSpOperator.getCreateTime());
				tbSpOperatorStatement.setString(3, tbSpOperator.getEnableFlag());
				tbSpOperatorStatement.setString(4, tbSpOperator.getEntId());
				tbSpOperatorStatement.setString(5, tbSpOperator.getImsi());
				tbSpOperatorStatement.setString(6, tbSpOperator.getIsMember());
				tbSpOperatorStatement.setString(7, tbSpOperator.getOpAddress());
				tbSpOperatorStatement.setString(8, tbSpOperator.getOpAuthcode());
				tbSpOperatorStatement.setLong(9, tbSpOperator.getOpBirth());
				tbSpOperatorStatement.setString(10, tbSpOperator.getOpCity());
				tbSpOperatorStatement.setString(11, tbSpOperator.getOpCountry());
				tbSpOperatorStatement.setString(12, tbSpOperator.getOpDuty());
				tbSpOperatorStatement.setString(13, tbSpOperator.getOpEmail());
				tbSpOperatorStatement.setLong(14, tbSpOperator.getOpEndutc());
				tbSpOperatorStatement.setString(15, tbSpOperator.getOpFax());
				tbSpOperatorStatement.setString(16, tbSpOperator.getOpId());
				tbSpOperatorStatement.setString(17, tbSpOperator.getOpIdentityId());
				tbSpOperatorStatement.setString(18, tbSpOperator.getOpLoginname());
				tbSpOperatorStatement.setString(19, tbSpOperator.getOpMem());
				tbSpOperatorStatement.setString(20, tbSpOperator.getOpMobile());
				tbSpOperatorStatement.setString(21, tbSpOperator.getOpName());
				tbSpOperatorStatement.setString(22, tbSpOperator.getOpPass());
				tbSpOperatorStatement.setString(23, tbSpOperator.getOpPhone());
				tbSpOperatorStatement.setString(24, tbSpOperator.getOpProvince());
				tbSpOperatorStatement.setString(25, tbSpOperator.getOpSex());
				tbSpOperatorStatement.setLong(26, tbSpOperator.getOpStartutc());
				tbSpOperatorStatement.setString(27, tbSpOperator.getOpStatus());
				tbSpOperatorStatement.setString(28, tbSpOperator.getOpSuper());
				tbSpOperatorStatement.setString(29, tbSpOperator.getOpType());
				tbSpOperatorStatement.setString(30, tbSpOperator.getOpWorkid());
				tbSpOperatorStatement.setString(31, tbSpOperator.getOpZip());
				tbSpOperatorStatement.setString(32, tbSpOperator.getPhoto());
				tbSpOperatorStatement.setString(33, tbSpOperator.getSkinStyle());
				tbSpOperatorStatement.setString(34, tbSpOperator.getUpdateBy());
				tbSpOperatorStatement.setString(35, tbSpOperator.getUpdateTime());
				tbSpOperatorStatement.setInt(36, tbSpOperator.getIsCenter());
				
				tbSpOperatorStatement.addBatch();
			}
			tbSpOperatorStatement.executeBatch();
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
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
	}
	
	/**
	 * 批量存储角色表
	 * @param sql
	 * @param tbOrg
	 */
	public void tbSpRoleSave(List<TbSpRole> list){
		try {
			conn = MySqlDataSource.getConnection();
			tbSpRoleStatement = conn.prepareStatement(sqlMap.get("sql_TbSpRole"));
			for(TbSpRole tbSpRole:list){
				tbSpRoleStatement.setLong(1, tbSpRole.getCreateBy());
				tbSpRoleStatement.setLong(2, tbSpRole.getCreateTime());
				tbSpRoleStatement.setString(3,tbSpRole.getEnableFlag());
				tbSpRoleStatement.setString(4, tbSpRole.getEntId());
				tbSpRoleStatement.setString(5, tbSpRole.getRoleDesc());
				tbSpRoleStatement.setString(6, tbSpRole.getRoleId());
				tbSpRoleStatement.setString(7, tbSpRole.getRoleName());		
				tbSpRoleStatement.setLong(8, tbSpRole.getRoleStatus());
				tbSpRoleStatement.setString(9, tbSpRole.getRoleType());
				tbSpRoleStatement.setLong(10, tbSpRole.getUpdateBy());
				tbSpRoleStatement.setLong(11, tbSpRole.getUpdateTime());
				
				tbSpRoleStatement.addBatch();
			}
			tbSpRoleStatement.executeBatch();
			
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
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
	}
	
	/**
	 * 批量存储终端表信息表
	 * @param sql
	 * @param tbOrg
	 */
	public void tbTerminalSave(List<TbTerminal> list){
		try {
			conn = MySqlDataSource.getConnection();
			tbTerminalStatement = conn.prepareStatement(sqlMap.get("sql_TbTerminal"));
			for(TbTerminal tbTerminal:list){
				tbTerminalStatement.setString(1, tbTerminal.getAuthCode());
				tbTerminalStatement.setString(2, tbTerminal.getCommunicateId());
				tbTerminalStatement.setString(3, tbTerminal.getConfigId());
				tbTerminalStatement.setString(4, tbTerminal.getCreateBy());
				tbTerminalStatement.setLong(5, tbTerminal.getCreateTime());
				tbTerminalStatement.setInt(6, tbTerminal.getDeliveryStatus());
				tbTerminalStatement.setString(7, tbTerminal.getEnableFlag());
				tbTerminalStatement.setString(8, tbTerminal.getEntId());
				tbTerminalStatement.setString(9, tbTerminal.getFirmwareVersion());
				tbTerminalStatement.setString(10, tbTerminal.getHardwareVersion());
				tbTerminalStatement.setString(11, tbTerminal.getOemCode());
				tbTerminalStatement.setLong(12, tbTerminal.getRegStatus());
				tbTerminalStatement.setLong(13, tbTerminal.getTerEcost());
				tbTerminalStatement.setLong(14, tbTerminal.getTerEdate());
				tbTerminalStatement.setString(15, tbTerminal.getTerEperson());
				tbTerminalStatement.setString(16, tbTerminal.getTerHardver());
				tbTerminalStatement.setLong(17, tbTerminal.getTerMdate());
				tbTerminalStatement.setString(18, tbTerminal.getTerMem());
				tbTerminalStatement.setLong(19, tbTerminal.getTerPrice());
				tbTerminalStatement.setString(20, tbTerminal.getTerSoftver());
				tbTerminalStatement.setLong(21, tbTerminal.getTerState());
				tbTerminalStatement.setLong(22, tbTerminal.getTerUtype());
				tbTerminalStatement.setString(23, tbTerminal.getTid());
				tbTerminalStatement.setString(24, tbTerminal.getTmac());
				tbTerminalStatement.setString(25, tbTerminal.getTmodelCode());
				tbTerminalStatement.setString(26, tbTerminal.getTprotocolId());
				tbTerminalStatement.setString(27, tbTerminal.getUpdateBy());
				tbTerminalStatement.setLong(28, tbTerminal.getUpdateTime());
				tbTerminalStatement.setString(29, tbTerminal.getVideoId());
				
				tbTerminalStatement.addBatch();
			}
			tbTerminalStatement.executeBatch();
			
			
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
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
		}
		}
	}
	
	/**
	 * 批量存储终端厂家编码表
	 * @param sql
	 * @param tbOrg
	 */
	public void tbTerminalOemSave(List<TbTerminalOem> list){
		try {
			conn = MySqlDataSource.getConnection();
			tbTerminalOemStatement = conn.prepareStatement(sqlMap.get("sql_TbTerminalOem"));
			for(TbTerminalOem tbTerminalOem:list){
				tbTerminalOemStatement.setString(1, tbTerminalOem.getAddress());
				tbTerminalOemStatement.setString(2, tbTerminalOem.getBoss());
				tbTerminalOemStatement.setString(3, tbTerminalOem.getCellphone());
				tbTerminalOemStatement.setString(4, tbTerminalOem.getConcateAddress());
				tbTerminalOemStatement.setString(5, tbTerminalOem.getConcatePerson());
				tbTerminalOemStatement.setString(6, tbTerminalOem.getCreateBy());
				tbTerminalOemStatement.setString(7, tbTerminalOem.getCreateTime());
				tbTerminalOemStatement.setString(8, tbTerminalOem.getEmail());
				tbTerminalOemStatement.setString(9, tbTerminalOem.getEnableFlag());
				tbTerminalOemStatement.setString(10, tbTerminalOem.getEnterpriseCity());
				tbTerminalOemStatement.setString(11, tbTerminalOem.getEnterpriseCountry());
				tbTerminalOemStatement.setString(12, tbTerminalOem.getEnterpriseProvince());
				tbTerminalOemStatement.setString(13, tbTerminalOem.getFax());
				tbTerminalOemStatement.setString(14, tbTerminalOem.getFullName());
				tbTerminalOemStatement.setString(15, tbTerminalOem.getOemCode());
				tbTerminalOemStatement.setString(16, tbTerminalOem.getOemDesc());
				tbTerminalOemStatement.setString(17, tbTerminalOem.getOemType());
				tbTerminalOemStatement.setString(18, tbTerminalOem.getShortName());
				tbTerminalOemStatement.setString(19, tbTerminalOem.getTel());
				tbTerminalOemStatement.setString(20, tbTerminalOem.getUpdateBy());
				tbTerminalOemStatement.setString(21, tbTerminalOem.getUpdateTime());
				tbTerminalOemStatement.setString(22, tbTerminalOem.getWebAddress());
				tbTerminalOemStatement.setString(23, tbTerminalOem.getZipCode());
			}
			tbTerminalOemStatement.execute();
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
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
	}
	
	
	/**
	 * 批量存储终端参数信息表
	 * @param sql
	 * @param tbOrg
	 */
	public void tbTerminalParamSave(List<TbTerminalParam> list){
		try {
			conn = MySqlDataSource.getConnection();
			tbTerminalParamStatement = conn.prepareStatement(sqlMap.get("sql_TbTerminalParam"));
			for(TbTerminalParam tbTerminalParam : list){
				tbTerminalParamStatement.setString(1, tbTerminalParam.getCreateBy());
				tbTerminalParamStatement.setString(2, tbTerminalParam.getCreateTime());
				tbTerminalParamStatement.setString(3, tbTerminalParam.getParamId());
				tbTerminalParamStatement.setString(4, tbTerminalParam.getParamType());
				tbTerminalParamStatement.setString(5, tbTerminalParam.getParamValue());
				tbTerminalParamStatement.setString(6, tbTerminalParam.getParentCode());
				tbTerminalParamStatement.setString(7, tbTerminalParam.getSeq());
				tbTerminalParamStatement.setString(8, tbTerminalParam.gettMac());
				tbTerminalParamStatement.setString(9, tbTerminalParam.getTid());
				tbTerminalParamStatement.setString(10, tbTerminalParam.getUpdateBy());
				tbTerminalParamStatement.setString(11, tbTerminalParam.getUpdateTime());
				tbTerminalParamStatement.addBatch();
				
			}
			tbTerminalParamStatement.executeBatch();
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
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
	}
	
	/**
	 * 批量存储终端协议信息表
	 * @param sql
	 * @param tbOrg
	 */
	public void tbTerminalProtocolSave(List<TbTerminalProtocol> list){
		try {
			conn = MySqlDataSource.getConnection();
			tbTerminalProtocolStatement = conn.prepareStatement(sqlMap.get("sql_TbTerminalProtocol"));
			for(TbTerminalProtocol tbTerminalProtocol : list){
				tbTerminalProtocolStatement.setString(1, tbTerminalProtocol.getCreateBy());
				tbTerminalProtocolStatement.setString(2, tbTerminalProtocol.getCreateTime());
				tbTerminalProtocolStatement.setString(3, tbTerminalProtocol.getOemCode());
				tbTerminalProtocolStatement.setString(4, tbTerminalProtocol.getTerminalTypeId());
				tbTerminalProtocolStatement.setString(5, tbTerminalProtocol.getTprotocolId());
				tbTerminalProtocolStatement.setString(6, tbTerminalProtocol.getTprotocolName());
				tbTerminalProtocolStatement.setString(7, tbTerminalProtocol.getUpdateBy());
				tbTerminalProtocolStatement.setString(8, tbTerminalProtocol.getUpdateTime());
				tbTerminalProtocolStatement.setString(9, tbTerminalProtocol.getValidFlag());
				tbTerminalProtocolStatement.setString(10, tbTerminalProtocol.getVasetTime());
				tbTerminalProtocolStatement.setString(11, tbTerminalProtocol.getVasetUserId());
				
				tbTerminalProtocolStatement.addBatch();				
			}
			tbTerminalProtocolStatement.executeBatch();			
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
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
	}
	
	/**
	 * 批量存储车辆信息表
	 * @param sql
	 * @param tbOrg
	 */
	public void tbVehicleSave(List<TbVehicle> list){
		try {
			conn = MySqlDataSource.getConnection();
			tbVehicleStatement = conn.prepareStatement(sqlMap.get("sql_TbVehicle"));
			for(TbVehicle tbVehicle : list){
				tbVehicleStatement.setLong(1, tbVehicle.getAnnualAuditTime());
				tbVehicleStatement.setLong(2, tbVehicle.getAnnualAuditValidityTime());
				tbVehicleStatement.setString(3, tbVehicle.getAreaCode());
				tbVehicleStatement.setLong(4, tbVehicle.getAttachedToTime());
				tbVehicleStatement.setLong(5, tbVehicle.getAuditAlarmDays());
				tbVehicleStatement.setString(6, tbVehicle.getAutoSn());
				tbVehicleStatement.setString(7, tbVehicle.getBuyNo());
				tbVehicleStatement.setString(8, tbVehicle.getCertificateState());
				tbVehicleStatement.setString(9, tbVehicle.getCertificateType());
				tbVehicleStatement.setString(10, tbVehicle.getCityId());
				tbVehicleStatement.setString(11, tbVehicle.getCoachLevel());
				tbVehicleStatement.setString(12, tbVehicle.getCounty());
				tbVehicleStatement.setString(13, tbVehicle.getCreateBy());
				tbVehicleStatement.setLong(14, tbVehicle.getCreateTime());
				tbVehicleStatement.setLong(15, tbVehicle.getCurbWeight());
				tbVehicleStatement.setLong(16, tbVehicle.getDeliveryStatus());
				tbVehicleStatement.setString(17, tbVehicle.getEbrandCode());
				tbVehicleStatement.setString(18, tbVehicle.getEmodelCode());
				tbVehicleStatement.setString(19, tbVehicle.getEmodelCode());
				tbVehicleStatement.setString(20, tbVehicle.getEnableFlag());
				tbVehicleStatement.setLong(21, tbVehicle.getEndTime());
				tbVehicleStatement.setLong(22, tbVehicle.getEngineDisplacement());
				tbVehicleStatement.setString(23, tbVehicle.getEngineNo());
				tbVehicleStatement.setString(24, tbVehicle.getEntId());
				tbVehicleStatement.setLong(25, tbVehicle.getFirstInstalTime());
				tbVehicleStatement.setString(26, tbVehicle.getInnerCode());
				tbVehicleStatement.setString(27, tbVehicle.getInsuranceState());
				tbVehicleStatement.setLong(28, tbVehicle.getKm100Oiluse());
				tbVehicleStatement.setString(29, tbVehicle.getMaintenanceState());
				tbVehicleStatement.setLong(30, tbVehicle.getMaximalPeople());
				tbVehicleStatement.setString(31, tbVehicle.getOiltypeId());
				tbVehicleStatement.setString(32, tbVehicle.getOriginCode());
				tbVehicleStatement.setLong(33, tbVehicle.getOutNumber());
				tbVehicleStatement.setString(34, tbVehicle.getPlateColor());
				tbVehicleStatement.setString(35, tbVehicle.getProdCode());
				tbVehicleStatement.setString(36, tbVehicle.getProgId());
				tbVehicleStatement.setDouble(37, tbVehicle.getRearAxleRate());
				tbVehicleStatement.setLong(38, tbVehicle.getRegStatus());
				tbVehicleStatement.setLong(39, tbVehicle.getReleaseDate());
				tbVehicleStatement.setString(40, tbVehicle.getReviewState());
				tbVehicleStatement.setString(41, tbVehicle.getRoadTransport());
				tbVehicleStatement.setLong(42, tbVehicle.getSalePrice());
				tbVehicleStatement.setString(43, tbVehicle.getServiceNo());
				tbVehicleStatement.setLong(44, tbVehicle.getSignTime());
				tbVehicleStatement.setLong(45, tbVehicle.getStandardOil());
				tbVehicleStatement.setLong(46, tbVehicle.getStandardRotate());
				tbVehicleStatement.setLong(47, tbVehicle.getTotalMass());
				tbVehicleStatement.setString(48, tbVehicle.getTranstypeCode());
				tbVehicleStatement.setDouble(49, tbVehicle.getTyreR());
				tbVehicleStatement.setString(50, tbVehicle.getUpdateBy());
				tbVehicleStatement.setLong(51, tbVehicle.getUpdateTime());
				tbVehicleStatement.setString(52, tbVehicle.getVbrandCode());
				tbVehicleStatement.setLong(53, tbVehicle.getVehicleBuydate());
				tbVehicleStatement.setLong(54, tbVehicle.getVehicleCap());
				tbVehicleStatement.setString(55, tbVehicle.getVehicleColor());
				tbVehicleStatement.setString(56, tbVehicle.getVehicleMem());
				tbVehicleStatement.setLong(57, tbVehicle.getVehicleMennum());
				tbVehicleStatement.setString(58, tbVehicle.getVehicleNo());
				tbVehicleStatement.setString(59, tbVehicle.getVehicleOperationId());
				tbVehicleStatement.setString(60, tbVehicle.getVehicleOperationState());
				tbVehicleStatement.setString(61, tbVehicle.getVehiclePic());
				tbVehicleStatement.setString(62, tbVehicle.getVehicleProperties());
				tbVehicleStatement.setLong(63, tbVehicle.getVehicleRegdate());
				tbVehicleStatement.setLong(64, tbVehicle.getVehicleRegisterNoTime());
				tbVehicleStatement.setString(65, tbVehicle.getVehicleState());
				tbVehicleStatement.setDouble(66, tbVehicle.getVehicleTon());
				tbVehicleStatement.setString(67, tbVehicle.getVehicleType());
				tbVehicleStatement.setLong(68, tbVehicle.getVhAccess());
				tbVehicleStatement.setString(69, tbVehicle.getVid());
				tbVehicleStatement.setString(70, tbVehicle.getVinCode());
				tbVehicleStatement.setString(71, tbVehicle.getVoltage());
				tbVehicleStatement.setString(72, tbVehicle.getVsRefId());
				tbVehicleStatement.setString(73, tbVehicle.getVtypeId());
				tbVehicleStatement.setLong(74, tbVehicle.getWatt());
				tbVehicleStatement.setLong(75, tbVehicle.getWheelBase());
				
				tbVehicleStatement.addBatch();				
			}
			
			tbVehicleStatement.executeBatch();	
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
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
	}
	
	
	/**
	 * 批量存储转组历史
	 * @param sql
	 * @param tbOrg
	 */
	public void thTransferHistorySave(List<ThTransferHistory> list){
		try {
			conn = MySqlDataSource.getConnection();
			thTransferHistoryStatement = conn.prepareStatement(sqlMap.get("sql_ThTransferHistory"));
			for(ThTransferHistory thTransferHistory : list){
				thTransferHistoryStatement.setString(1,thTransferHistory.getGoalId());
				thTransferHistoryStatement.setString(2,thTransferHistory.getId());
				thTransferHistoryStatement.setString(3,thTransferHistory.getOpId());
				thTransferHistoryStatement.setLong(4,thTransferHistory.getOpTime());
				thTransferHistoryStatement.setString(5,thTransferHistory.getSourceId());
				thTransferHistoryStatement.setString(6,thTransferHistory.getTransferId());
				thTransferHistoryStatement.setInt(7,thTransferHistory.getTransferType());
				thTransferHistoryStatement.addBatch();
			}
			thTransferHistoryStatement.executeBatch();		
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
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
	}
	
	
	/**
	 * 批量存储用户角色关系表
	 * @param sql
	 * @param tbOrg
	 */
	public void trOperatorRoleSave(List<TrOperatorRole> list){
		try {
			conn = MySqlDataSource.getConnection();
			trOperatorRoleStatement = conn.prepareStatement(sqlMap.get("sql_TrOperatorRole"));
			for(TrOperatorRole trOperatorRole : list){
				trOperatorRoleStatement.setString(1, trOperatorRole.getAutoId());
				trOperatorRoleStatement.setString(2, trOperatorRole.getOpId());
				trOperatorRoleStatement.setString(3, trOperatorRole.getRoleId());
				
				trOperatorRoleStatement.addBatch();
			}	
			trOperatorRoleStatement.executeBatch();			
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
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
	}
	
	/**
	 * 批量存储菜单权限角色关系表
	 * @param sql
	 * @param tbOrg
	 */
	public void trRoleFunctionSave(List<TrRoleFunction> list){
		try {
			conn = MySqlDataSource.getConnection();
			trRoleFunctionStatement = conn.prepareStatement(sqlMap.get("sql_TrRoleFunction"));
			for(TrRoleFunction trRoleFunction : list){
				trRoleFunctionStatement.setString(1, trRoleFunction.getCreateBy());
				trRoleFunctionStatement.setString(2, trRoleFunction.getCreateTime());
				trRoleFunctionStatement.setString(3, trRoleFunction.getEnableFlag());
				trRoleFunctionStatement.setString(4, trRoleFunction.getFunId());
				trRoleFunctionStatement.setString(5, trRoleFunction.getRoleId());
				trRoleFunctionStatement.setString(6, trRoleFunction.getUpdateBy());
				trRoleFunctionStatement.setString(7, trRoleFunction.getUpdateTime());
				trRoleFunctionStatement.addBatch();
			}
			trRoleFunctionStatement.executeBatch();			
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
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
	}
	
	/**
	 * 批量存储车卡终端关系表
	 * @param sql
	 * @param tbOrg
	 */
	public void trServiceunitSave(List<TrServiceunit> list){
		try {
			conn = MySqlDataSource.getConnection();
			trServiceunitStatement = conn.prepareStatement(sqlMap.get("sql_TrServiceunit"));
			for(TrServiceunit trServiceunit : list){
				trServiceunitStatement.setString(1, trServiceunit.getCreateTime());
				trServiceunitStatement.setString(2, trServiceunit.getCreateUser());
				trServiceunitStatement.setString(3, trServiceunit.getDvrId());
				trServiceunitStatement.setString(4, trServiceunit.getModelName());
				trServiceunitStatement.setString(5, trServiceunit.getRemark());
				trServiceunitStatement.setString(6, trServiceunit.getSid());
				trServiceunitStatement.setString(7, trServiceunit.getSuid());
				trServiceunitStatement.setString(8, trServiceunit.getTid());
				trServiceunitStatement.setString(9, trServiceunit.getUpdateTime());
				trServiceunitStatement.setString(10, trServiceunit.getUpdateUser());
				trServiceunitStatement.setString(11, trServiceunit.getVid());
				
				trServiceunitStatement.addBatch();
			}	
			trServiceunitStatement.executeBatch();						
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
			} catch (SQLException e2) {
				logger.error("关闭资源异常:"+e2.getMessage(), e2); 
			}
		}
	}

	/**
	 * @return 获取 sqlMap
	 */
	public Map<String, String> getSqlMap() {
		return sqlMap;
	}
	/**
	 * 设置sqlMap
	 * @param sqlMap sqlMap 
	 */
	public void setSqlMap(Map<String, String> sqlMap) {
		this.sqlMap = sqlMap;
	}
}
