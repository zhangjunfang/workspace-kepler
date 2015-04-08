package com.ctfo.sys.controller;

import java.awt.BasicStroke;
import java.awt.Color;
import java.awt.Font;
import java.awt.Graphics;
import java.awt.Graphics2D;
import java.awt.geom.Line2D;
import java.awt.image.BufferedImage;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.LineNumberReader;
import java.util.Date;
import java.util.Enumeration;
import java.util.HashMap;
import java.util.Map;
import java.util.Random;

import javax.imageio.ImageIO;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

import redis.clients.jedis.Jedis;
import redis.clients.jedis.JedisPool;

import com.ctfo.basic.controller.BaseController;
import com.ctfo.local.exception.CtfoAppException;
import com.ctfo.sys.beans.SysSpOperator;
import com.ctfo.sys.service.LoginService;
import com.ctfo.sys.service.TbOrgService;
import com.ctfo.util.StringUtil;

@Controller
@SuppressWarnings({ "unused" })
public class LoginController extends BaseController{

	@Autowired
	private LoginService loginService;
	
	@Autowired
	private TbOrgService tbOrgService;
	
	/** redis主连接池 */
	@Autowired
	private JedisPool writeJedisPool;
	
	/**
	 * 
	 * @description:登录接口
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月3日下午1:49:41
	 * @modifyInformation：
	 */
	@RequestMapping("/login.do")
	public String login(HttpServletRequest request,HttpServletResponse response){
		SysSpOperator operator = null;
		SysSpOperator operatorPd = null;
		SysSpOperator operatorOc = null;
		
		try {
			 String opLoginname  = request.getParameter("requestParam.equal.opLoginname");
			 String opPass = request.getParameter("requestParam.equal.opPass");
			 String imgCode  = request.getParameter("imgCode");
			 String parentOrgCode = request.getParameter("requestParam.equal.corpCode");
			 String randomCheckCode = (String) request.getSession().getAttribute("RandomCheckCode");
			 String ip = getRemoteAddress(request);//获取用户的登陆IP地址
//			 String macAddress = getMACAddress(ip);//获取用户所用机器的MAC地址			
			 if(StringUtil.isBlank(randomCheckCode) || !randomCheckCode.equals(imgCode)){
				 return this.returnInfoForJS(response,false,"验证码输入错误！");
			 }
			
			 Map<String,Object> map = new HashMap<String,Object>();
			 map.put("opLoginname", opLoginname);
			 map.put("opPass", opPass);
			 map.put("parentOrgCode", parentOrgCode);
			 operator = loginService.login(map);
			 if(operator == null){
				 return this.returnInfoForJS(response,false,"对不起，不存在该用户！");
			 }else{
				 operatorPd = loginService.loginPd(map);	
				 if(operatorPd == null){
					 return this.returnInfoForJS(response,false,"对不起，密码错误！");
				 }else{
					 operatorOc = loginService.loginOc(map);
					 if(operatorOc == null){
						 return this.returnInfoForJS(response,false,"对不起，企业编码错误！");
					 }else{
						 HttpSession session =  request.getSession();
						 session.setAttribute("ip", ip);//登陆IP地址
						 session.setAttribute("loadTime", new Date());//用户登陆的时间
						 session.setAttribute("SysSpOperator", operator);
						 
						 Jedis client = writeJedisPool.getResource();
//						 client.select(2);
						 client.hset("LOGIN", opLoginname, String.valueOf(System.currentTimeMillis()));
						 
						 return this.returnInfoForJS(response,true,"登录成功！!");
					 }
				 }
			 }
			 
/*			 if(operator != null){
				 HttpSession session =  request.getSession();
				 session.setAttribute("ip", ip);//登陆IP地址
				 session.setAttribute("loadTime", new Date());//用户登陆的时间
				 session.setAttribute("SysSpOperator", operator);
				 return this.returnInfoForJS(response,true,"登录成功！!");
			 }else{
				 return this.returnInfoForJS(response,false,"对不起，不存在该用户！");
			 }*/
		} catch (CtfoAppException e) {
			return this.returnInfoForJS(response, false, e.getMessage().replaceAll("'|\\n", " "));
		}
	}
	
	/**
	 * 
	 * @param request
	 * @return 返回IP地址
	 */
	public String getRemoteAddress(HttpServletRequest request) {  
        String ip = request.getHeader("x-forwarded-for");  
        if (ip == null || ip.length() == 0 || ip.equalsIgnoreCase("unknown")) {  
            ip = request.getHeader("Proxy-Client-IP");  
        }  
        if (ip == null || ip.length() == 0 || ip.equalsIgnoreCase("unknown")) {  
            ip = request.getHeader("WL-Proxy-Client-IP");  
        }  
        if (ip == null || ip.length() == 0 || ip.equalsIgnoreCase("unknown")) {  
            ip = request.getRemoteAddr();  
        }  
        return ip;  
    }  
	
	/**
	 * 
	 * @param ip
	 * @return MAC ADDRESS
	 */
	public String getMACAddress(String ip) {  
		 String str = "";
	        String macAddress = "";
	        try {
	            Process p = Runtime.getRuntime().exec("nbtstat -a " + ip);
	            InputStreamReader ir = new InputStreamReader(p.getInputStream());
	            LineNumberReader input = new LineNumberReader(ir);
	            for (int i = 1; i < 100; i++) {
	                str = input.readLine();
	                if (str != null) {
	                    //if (str.indexOf("MAC Address") > 1) {
	                    if (str.indexOf("MAC") > 1) {
	                        macAddress = str.substring(
	                                str.indexOf("=") + 2, str.length());
	                        break;
	                    }
	                }
	            }
	        } catch (IOException e) {
	            e.printStackTrace(System.out);
	        }
	        return macAddress;
    }  
	
	/**
	 * 
	 * @description:注销接口
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月3日下午1:49:56
	 * @modifyInformation：
	 */
	@RequestMapping("/loginOut.do")
	public String loginOut(HttpServletRequest request,HttpServletResponse response){
		Jedis client = writeJedisPool.getResource();
//		client.select(2);
		SysSpOperator sso = (SysSpOperator) request.getSession().getAttribute("SysSpOperator");
		String opLoginname = sso.getOpLoginname();
		
	    request.getSession().removeAttribute("SysSpOperator");// 清除session信息
		Enumeration<String> e = request.getSession().getAttributeNames();
		while(e.hasMoreElements()){
			request.getSession().removeAttribute(e.nextElement());
		}
		client.hdel("LOGIN", opLoginname);  
		return this.returnInfoForJS(response,true,"注销成功！!");
	}
	
	@RequestMapping("/checkCode.do")
	public void checkCode(HttpServletRequest request,HttpServletResponse response){
	    //禁止缓存
	    response.setHeader("Pragma", "No-cache");
	    response.setHeader("Cache-Control", "No-cache");
	    response.setDateHeader("Expires", 0);
	    // 指定生成的响应是图片
	    response.setContentType("image/jpeg");

  		int width = 70;
	    int height = 19;
	    BufferedImage image = new BufferedImage(width, height, 1);
	    Graphics g = image.getGraphics();
	    Graphics2D g2d = (Graphics2D)g;
	    Random random = new Random();
	    Font mFont = new Font("宋体", 1, 17);
	    g.setColor(getRandColor(200, 250));
	    g.fillRect(0, 0, width, height);
	    g.setFont(mFont);
	    g.setColor(getRandColor(220, 250));
	    for (int i = 0; i < 130; ++i) {
			int x = random.nextInt(width - 1);
			int y = random.nextInt(height - 1);
			int x1 = random.nextInt(6) + 1;
			int y1 = random.nextInt(12) + 1;
			BasicStroke bs = new BasicStroke(2F, 0, 2);
			Line2D line = new Line2D.Double(x, y, x + x1, y + y1);
			g2d.setStroke(bs);
			g2d.draw(line);
	    }
	    String sRand = "";  
	    int itmp = 0;
	    for (int i = 0; i < 4; ++i)
	    {
			itmp = random.nextInt(10) + 48;
			
			char ctmp = (char)itmp;
			sRand = sRand + String.valueOf(ctmp);
			Color color = new Color(20 + random.nextInt(155), 20 + random.nextInt(155), 20 + random.nextInt(155));
			g.setColor(color);
			
			g.drawString(String.valueOf(ctmp), 15 * i + 10, 14);
	    }
	    request.getSession().setAttribute("RandomCheckCode", sRand);
	    g.dispose();
	    //ByteArrayOutputStream output = new ByteArrayOutputStream();
	    try
	    {

	      //ImageOutputStream imageOut = ImageIO.createImageOutputStream(output);
	    	ImageIO.write(image,"JPEG",response.getOutputStream());
	    }
	    catch (Exception e) {
	    	e.printStackTrace();
	    }
	}
	private Color getRandColor(int s, int e){
		Random random = new Random();
		if (s > 255)
		  s = 255;
		if (e > 255)
		  e = 255;
		int r = s + random.nextInt(e - s);
		int g = s + random.nextInt(e - s);
		int b = s + random.nextInt(e - s);
		return new Color(r, g, b);
	}
}
