package com.hcr.web.controller;

import javax.servlet.http.HttpServletRequest;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import com.ruishenh.dubbo.example.DemoService;

@Controller
@RequestMapping("/test")
public class TestController {
	
	private DemoService demoService; 
	
	@ResponseBody
	@RequestMapping(value = "/json")
	Object index(HttpServletRequest request, Model view) {
		System.out.println("LOG.............");
		return "";
	}
	
	@ResponseBody
	@RequestMapping(value = "/dubbo")
	Object testDubbo(HttpServletRequest request, Model view) {
		System.out.println("LOG.............");
		if (demoService==null) {
			return demoService.returnHello();
		}
		return "";
	}

	public DemoService getDemoService() {
		return demoService;
	}

	public void setDemoService(DemoService demoService) {
		this.demoService = demoService;
	}
}