package com.ctfo.common.filter;

import java.text.SimpleDateFormat;
import java.util.Date;

import org.springframework.beans.propertyeditors.CustomDateEditor;
import org.springframework.web.bind.WebDataBinder;
import org.springframework.web.bind.annotation.InitBinder;
import org.springframework.web.bind.support.WebBindingInitializer;
import org.springframework.web.context.request.WebRequest;

public class DateConverter implements WebBindingInitializer {

	@InitBinder
	public void initBinder(WebDataBinder arg0, WebRequest arg1) {
		SimpleDateFormat df = new SimpleDateFormat("yyyy-MM-dd HH:mm");
		arg0.registerCustomEditor(Date.class, new CustomDateEditor(df, true));
	}

}
