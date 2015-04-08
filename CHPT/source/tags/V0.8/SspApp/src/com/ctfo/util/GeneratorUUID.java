package com.ctfo.util;



import org.doomdark.uuid.UUID;
import org.doomdark.uuid.UUIDGenerator;

public class GeneratorUUID {

	public static String generateUUID(){
		UUIDGenerator generator = UUIDGenerator.getInstance();
		UUID uuid = generator.generateRandomBasedUUID();
		String strUUID = uuid.toString();
		strUUID = strUUID.replaceAll("-","");
		return strUUID;
	}
	
	
}
