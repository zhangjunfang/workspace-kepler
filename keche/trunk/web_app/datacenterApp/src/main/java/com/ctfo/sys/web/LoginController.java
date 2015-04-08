package com.ctfo.sys.web;

import java.awt.BasicStroke;
import java.awt.Color;
import java.awt.Font;
import java.awt.Graphics;
import java.awt.Graphics2D;
import java.awt.geom.Line2D;
import java.awt.image.BufferedImage;
import java.util.HashMap;
import java.util.Map;
import java.util.Random;

import javax.imageio.ImageIO;
import javax.servlet.http.Cookie;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

import com.ctfo.common.local.obj.PaginationResult;
import com.ctfo.common.local.web.BaseController;
import com.ctfo.common.util.CookieUtil;
import com.ctfo.common.util.DesUtil;
import com.ctfo.common.util.RedisJsonUtil;
import com.ctfo.common.util.StaticSession;
import com.ctfo.sys.beans.TbSpOperator;
import com.ctfo.sys.service.LoginService;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 登录<br>
 * 描述： 登录<br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交慧联信息科技有限公司 <br>
 * ----------------------------------------------------------------------------- <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2014-5-23</td>
 * <td>xuehui</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author xuehui
 * @since JDK1.6
 */
@Controller
public class LoginController extends BaseController {

	@Autowired
	private LoginService loginService;

	/**
	 * 登录
	 * 
	 * @param request
	 * @param response
	 * @return
	 */
	@RequestMapping("/login.do")
	public String login(HttpServletRequest request, HttpServletResponse response) {
		PaginationResult<TbSpOperator> result = new PaginationResult<TbSpOperator>();
		try {
			String opLoginname = request.getParameter("requestParam.equal.opLoginname");
			String opPass = request.getParameter("requestParam.equal.opPass");
			String parentOrgCode = request.getParameter("requestParam.equal.corpCode");
			String imgCode = request.getParameter("imgCode");
			if (!checkCode(imgCode, request)) { // 判断验证码是否正确
				return this.returnInfoForJS(response, StaticSession.OP_CHECKCODE);
			}
			Map<String, String> map = new HashMap<String, String>();
			map.put("opLoginname", opLoginname);
			map.put("opPass", opPass);
			map.put("parentOrgCode", parentOrgCode);

			result = loginService.findOperatorLogin(map); // 查询用户信息
			if (result.getResultJudge().equals(StaticSession.MESSAGE_SUCCESS)) {
				// 登录成功,用户信息放入redis缓存
				TbSpOperator operator = (TbSpOperator) result.getData().toArray()[0];
				String key = operator.getOpId() + StaticSession.SYS_MARKING_PREFIX_CENTER;
				String jsonValue = RedisJsonUtil.objectToJson(operator);
				this.writeJedisDao.setTempValueAndExpireTime(key, jsonValue, 2 * 60 * 60);
				// 用户id放入cookie
				Cookie cookie = CookieUtil.getInstance().getCookie(request, StaticSession.COOKIE_USERID);
				String value = DesUtil.getInstance().encryptStr(operator.getOpId()) + "_" + String.valueOf(System.currentTimeMillis());
				if (null != cookie) {
					CookieUtil.getInstance().deleteCookie(request, response, cookie);
				}
				CookieUtil.setCookie(request, response, StaticSession.COOKIE_USERID, value, 2 * 3600 * 1000);
			}
			return this.returnInfoForJS(response, result.getResultJudge());
		} catch (Exception e) {
			return this.returnInfoForJS(response, StaticSession.MESSAGE_ERROR);
		}
	}

	/**
	 * 登出
	 * 
	 * @param request
	 * @param response
	 * @return
	 */
	@RequestMapping("/logout.do")
	public String logout(HttpServletRequest request, HttpServletResponse response) {
		try {
			String[] objId = CookieUtil.getInstance().getOpIdByCookie(request, StaticSession.COOKIE_USERID);
			if (objId != null && !"null".equals(objId) && objId.length > 0) {
				// 从缓存中取出用户登录信息
				String opId = DesUtil.getInstance().decryptStr(objId[0]) + StaticSession.SYS_MARKING_PREFIX_CENTER; // 用户id
				String value = this.readJedisDao.getTempCacheValue(opId);
				if (null != value && !"".equals(value)) {
					this.writeJedisDao.destroyTempCacheById(opId);
				}
			}
			Cookie cookie = CookieUtil.getInstance().getCookie(request, StaticSession.COOKIE_USERID);
			CookieUtil.getInstance().deleteCookie(request, response, cookie);
			return this.returnInfoForJS(response, StaticSession.MESSAGE_SUCCESS);
		} catch (Exception e) {
			return this.returnInfoForJS(response, StaticSession.MESSAGE_ERROR);
		}
	}

	/**
	 * 检查验证码
	 * 
	 * @param code
	 *            验证码
	 * @return boolean
	 */
	public boolean checkCode(String code, HttpServletRequest request) {
		if (code != null) {
			try {
				String value = this.readJedisDao.getTempCacheValue(request.getSession().getId());
				this.writeJedisDao.destroyTempCacheById(request.getSession().getId());
				if (code.trim().toUpperCase().equals(value)) {
					return true;
				}
			} catch (Exception e) {
				e.printStackTrace();
				return true;
			}
		}
		return false;
	}

	/**
	 * 生成验证码
	 * 
	 * @param request
	 * @param response
	 */
	@RequestMapping("/rondamImage.do")
	public void rondamImage(HttpServletRequest request, HttpServletResponse response) {
		// 禁止缓存
		response.setHeader("Pragma", "No-cache");
		response.setHeader("Cache-Control", "No-cache");
		response.setDateHeader("Expires", 0);
		// 指定生成的响应是图片
		response.setContentType("image/jpeg");

		int width = 70;
		int height = 19;
		BufferedImage image = new BufferedImage(width, height, 1);
		Graphics g = image.getGraphics();
		Graphics2D g2d = (Graphics2D) g;
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
		for (int i = 0; i < 4; ++i) {
			itmp = random.nextInt(10) + 48;

			char ctmp = (char) itmp;
			sRand = sRand + String.valueOf(ctmp);
			Color color = new Color(20 + random.nextInt(155), 20 + random.nextInt(155), 20 + random.nextInt(155));
			g.setColor(color);

			g.drawString(String.valueOf(ctmp), 15 * i + 10, 14);
		}
		// request.getSession().setAttribute("RandomCheckCode", sRand);
		g.dispose();
		// ByteArrayOutputStream output = new ByteArrayOutputStream();
		try {
			// ImageOutputStream imageOut = ImageIO.createImageOutputStream(output);
			this.writeJedisDao.setTempValueAndExpireTime(request.getSession().getId(), sRand, 60);
			ImageIO.write(image, "JPEG", response.getOutputStream());
		} catch (Exception e) {
			e.printStackTrace();
		}

	}

	private Color getRandColor(int s, int e) {
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
