package com.ctfo.basic.controller;



import java.io.File;
import java.io.IOException;
import java.io.OutputStream;
import java.io.PrintWriter;
import java.util.List;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import net.sf.json.JSONObject;

import org.apache.poi2.hssf.usermodel.HSSFWorkbook;
import com.ctfo.context.FrameworkContext;
import com.ctfo.local.exception.CtfoAppException;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.ExcelExport;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.util.JsonUtil;
import com.ctfo.util.StringUtil;

/**
 * 开发人： 张波
 * 开发日期： 2013-7-11 上午10:13:26
 * 功能描述：
 */
public class BaseController {
	
	/**
	 * 消息：操作成功
	 */
	protected final static String MES_SUCCESS_OPERATE = "操作成功!";

	/**
	 * 消息：添加成功
	 */
	protected final static String MES_SUCCESS_ADD = "添加成功!";

	/**
	 * 消息：修改成功
	 */
	protected final static String MES_SUCCESS_MODIFY = "修改成功!";

	/**
	 * 消息：删除成功
	 */
	protected final static String MES_SUCCESS_REMOVE = "删除成功!";

	/**
	 * 文本编码
	 */
	protected final static String ContentENCOD = "UTF-8";

	/**
	 * 文本类型
	 */
	protected final static String ContentTypeHTML = "text/html";
	
	/**
	 * 开发人 ： 张波
	 * 开发日期： 2013-7-11 上午10:14:11
	 * 功能描述：构建分页信息
	 * 方法的参数和返回值
	 * @param request
	 * @return
	 * DynamicSqlParameter 
	 */
	protected DynamicSqlParameter getPageParam(HttpServletRequest request){
		 DynamicSqlParameter param=new DynamicSqlParameter();
	      String page=request.getParameter("requestParam.page");
	      String num = request.getParameter("requestParam.pagesize");
	      String rows = request.getParameter("requestParam.rows");
	      String order=request.getParameter("requestParam.equal.sortorder");
	      String sort=request.getParameter("requestParam.equal.sortname");
	      if(StringUtil.isNotBlank(page)){
	    	  param.setPage(Integer.parseInt(page));
	      }else{
	    	  param.setPage(1);
	      }
	      if(StringUtil.isNotBlank(rows)){
	    	  param.setRows(Integer.parseInt(rows));
	      } else {
	    	  param.setRows(30);
	      }
	      if(StringUtil.isNotBlank(num)){
	    	  param.setPagesize(Integer.parseInt(rows)*Integer.parseInt(num));
	      }else{
	    	  if(StringUtil.isNotBlank(rows)){
	    		  param.setPagesize(Integer.parseInt(rows));
	    	  } else {
	    		  param.setPagesize(30);
	    	  }
	    	 
	      }
	      if(StringUtil.isNotBlank(order)){
	    	  param.setOrder(order);
	      }
	      if(StringUtil.isNotBlank(sort)){
	    	  param.setSort(sort);
	      }
	      return param;
	}	
	/**
	* 开发人：张波
	* 开发日期: 2013-7-11  上午10:43:40
	* 功能描述: 直接输出HTML文本
	* 方法的参数和返回值: 
	* @param html
	* @return
	*/
	protected String writeHTML(HttpServletResponse response,String html){
		try {
			response.setContentType("text/html; charset=UTF-8"); // 设置 content-type
			response.setCharacterEncoding("UTF-8");  // 设置响应数据编码格式 (输出)
			PrintWriter out = response.getWriter();
			out.println(html);
		} catch (IOException e) {
			
		}
		return null;
	}

    /**
     * 开发人： 张波
     * 开发时间： 2013-7-11 下午02:19:30
     * 功能描述：返回json格式的操作信息，用于前台对数据操作请求之后的判断
     * 方法的参数和返回值
     * @param opFlag  布尔值，true：opState返回success，否则返回failure
     * @param opInfo  操作的返回信息，用于前台页面的信息展示和判断等操作
     * @throws AppException
     * void 
     */
    public String returnInfoForJS(HttpServletResponse response,boolean opFlag,String opInfo){
    	if(StringUtil.isBlank(opInfo))
    		opInfo = "";
		JSONObject obj = new JSONObject();
		obj.put("displayMessage", opFlag?"success":"failure");
		obj.put("opInfo", opInfo);
		return this.writeHTML(response,obj.toString());
    }
    
    /**
     * 开发人： 张波
     * 开发时间： 2013-7-11 上午09:54:18
     * 功能描述：返回json格式的操作信息，用于前台对数据操作请求之后的判断
     * 方法的参数和返回值
     * @param opFlag  布尔值，true：opState返回success，否则返回failure
     * @param opInfo  操作的返回信息，用于前台页面的信息展示
     * @param opValue 操作的返回信息，用于前台页面接收的参数
     * @return
     * String 
     */
    public String returnInfoForJS(HttpServletResponse response,boolean opFlag,String opInfo,Object opValue){
    	if(StringUtil.isBlank(opInfo))
    		opInfo = "";
		JSONObject obj = new JSONObject();
		obj.put("opState", opFlag?"success":"failure");
		obj.put("opInfo", opInfo);
		obj.put("opValue", opValue);
		return this.writeHTML(response,obj.toString());
    }
    
    
    /**
     * @author ：张波
     * @since： 2013-9-30 下午3:14:23
     * 功能描述：获取excel模板路径文件
     * 方法的参数和返回值
     * @param request
     * @param excelSrc
     * @return
     * @throws CtfoAppException
     * File 
     */
    protected File getExcelTemplate(HttpServletRequest request,String excelSrc) throws CtfoAppException{
 		if(excelSrc==null){
 			excelSrc = "export.xls";
 		}
 		if(!excelSrc.startsWith("/")&&!excelSrc.startsWith("\\")){
 			String uri = request.getRequestURI();
 			uri = uri.substring(request.getContextPath().length(),uri.lastIndexOf("/"));
 			excelSrc = uri+"/"+excelSrc;
 		}	
 		File f = new File(FrameworkContext.getAppPath()+excelSrc);
		if(!f.exists()){
			throw new CtfoAppException("Excel模板["+f.getAbsolutePath().replaceAll("\\\\", "/")+"]不存在!");
		}
		return f;
     }
    

	/**
	 * @author ：张波
	 * @since： 2013-9-30 下午3:10:16
	 * 功能描述：通过设定的Excel模板文件导出数据为Excel格式
	 * 方法的参数和返回值
	 * @param search
	 * @param data
	 * @param excelSrc
	 * @param excelName
	 * @param request
	 * @param response
	 * void 
	 * ==================================
	 * 修改历史
	 * 修改人        修改时间      修改原因及内容
	 *
	 * ==================================
	 */
	@SuppressWarnings("rawtypes")
	public void saExcel(Object search,List data,String excelSrc,String excelName,HttpServletRequest request,HttpServletResponse response) {
		try {
			File f = getExcelTemplate(request,excelSrc);
			ExcelExport xlsExport=new ExcelExport();
			HSSFWorkbook wb=xlsExport.genExcel(search, f, data);
			response.setCharacterEncoding("UTF-8");
			response.setContentType("application/zip"); 
			response.setHeader("Content-disposition", "attachment; filename=\""
					+ new String((excelName+".xls").getBytes("GBK"),"ISO8859-1") + "\"");
			wb.write(response.getOutputStream());
		} catch (Exception e) {
		   e.printStackTrace();
		}
	}
	/**
	* 开发人：张波
	* 开发日期: 
	* 功能描述: 在打开添加和修改界面时主表及从表数据初始化后再调用此方法
	* 方法的参数和返回值: 
	* @param isNew
	*/
    protected void initOtherData(HttpServletRequest request,boolean isNew) {
		
	}
    
    
    
    
    /**
     * @author ： 张波
     * @since： 2013-9-30 下午3:11:47
     * 功能描述：获取请求路径地址
     * 方法的参数和返回值
     * @param request
     * @return
     * String 
     */
    public String getUrl(HttpServletRequest request)
    {
      StringBuffer urlBuffer = request.getRequestURL();
      String url = urlBuffer.substring(0, urlBuffer.indexOf(request.getRequestURI()))+request.getContextPath();
      return url;
    }
    
    /**
     * @author ： 陈园
     * @since： 2013-9-30 下午3:12:55
     * 功能描述：josn转成Grid
     * 方法的参数和返回值
     * @param result
     * @return
     * @throws CtfoAppException
     * String 
     */
    @SuppressWarnings({ "rawtypes" })
	protected String jsonFormatToGrid(PaginationResult result)
    	    throws CtfoAppException
    	  {
    	    try
    	    {
    	      if (result != null)
    	      {
    	        String json = JsonUtil.getJsonforGrid(String.valueOf(result.getTotalCount()), (List)result.getData());
    	        return json;
    	      }

    	      return "{\"Rows\":[],\"Total\":\"0\"}";
    	    }
    	    catch (Exception e)
    	    {
    	      throw new CtfoAppException(e);
    	    }
    	  }
    /**
	 * 统一输出接口[页面显示方式]
	 * 
	 * @param results
	 */
	protected void printWriter(HttpServletResponse response,Object results) {
		String resultStr = null;
		OutputStream out = null;
		try {
			if (null != results) {
				resultStr = String.valueOf(results);
			} else {
				resultStr = "";
			}
			//设置需要的response格式对象
			response.setCharacterEncoding(ContentENCOD);
			response.setContentType(ContentTypeHTML);
			out = response.getOutputStream();
			out.write(resultStr.getBytes(ContentENCOD));
		} catch (IOException e) {
			e.printStackTrace();
		} finally {
			if (out != null) {
				try {
					out.close();
				} catch (IOException e) {
					e.printStackTrace();
				}
			}
		}
	}

}
