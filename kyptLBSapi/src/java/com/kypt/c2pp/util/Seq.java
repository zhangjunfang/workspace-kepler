package com.kypt.c2pp.util;

public class Seq {
	
	static long seq=0;
	
	public  static synchronized   long   getSeq()   
	  {   
		  if(seq<99999999){
			  seq=seq+1;
		  }else{
			  seq = 0;
		  }
	      return   seq;   
	  } 
	
}
