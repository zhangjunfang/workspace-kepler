package com.ctfo.savecenter;

import java.util.*;
import java.io.*;
import java.sql.*;
import com.lingtu.xmlconf.XmlConf;
/**
 * 读取配置文件类
 * @author yangyi
 *
 */
public class SaveCenterConfig
{
    XmlConf conf=null;

    /**
     * constructor
     * @param xmlfile the xml file name
     * @throws Exception xml error
     */
    public SaveCenterConfig(String xmlfile) throws Exception
    {
        conf=new XmlConf(xmlfile);
    }

    /**
     * refresh the xml file
     */
    public void refreshXmlFile()
    {
        try
        {
            conf.updateXml();
        }
        catch (Exception ex)
        {
            ex.printStackTrace();
        }
    }

    /**
     * check xml config
     * @return true if check passed,false means error
     */
    public boolean checkConfig()
    {
        boolean result=true;
        String temp=null;
        temp=conf.getStringValue("ManagePort");
        if(temp==null)//if no such config node
        {
            conf.setGlobalStatus("缺少配置项ManagePort");
            return false;
        }
        temp=conf.getStringValue("ServicePort");
        if(temp==null)
        {
            conf.setGlobalStatus("缺少配置项ServicePort");
            return false;
        }
        temp=conf.getStringValue("SMSAddr");
        if(temp==null)
        {
            conf.setGlobalStatus("缺少配置项SMSAddr");
            return false;
        }
        temp=conf.getStringValue("AlarmServiceHosts");
        if(temp==null)
        {
            conf.setGlobalStatus("缺少配置项AlarmServiceHosts");
            return false;
        }
        else
        {
            StringTokenizer st=new StringTokenizer(temp,"|");
            try
            {
                while(st.hasMoreTokens())
                {
                    String server=st.nextToken();
                    StringTokenizer st1=new StringTokenizer(server,":");
                    Integer.parseInt(st1.nextToken());
                    st1.nextToken();
                    Integer.parseInt(st1.nextToken());
                }
                conf.setCheckResult("AlarmServiceHosts","");
            }
            catch(Exception e)
            {
                conf.setCheckResult("AlarmServiceHosts","格式有错误");
                return false;
            }
        }
        temp=conf.getStringValue("ThreadCount");
        if(temp==null)
        {
            conf.setGlobalStatus("缺少配置项ThreadCount");
            return false;
        }
        String dbdriver=conf.getStringValue("database|JDBCDriver");
        String dburl=conf.getStringValue("database|JDBCUrl");
        String dbuser=conf.getStringValue("database|JDBCUser");
        String dbpass=conf.getStringValue("database|JDBCPassword");
        try//check database connnection
        {
            Class.forName(dbdriver);
            Connection con=DriverManager.getConnection(dburl,dbuser,dbpass);
            con.close();
            conf.setCheckResult("database","");//success
            conf.setGlobalStatus("");//all check passed , set the global error info to none
        }
        catch (Exception ex)
        {
            conf.setCheckResult("database","can not login to database!");//set the node error info
            conf.setGlobalStatus("不能连接数据库");//set the global error info
            result=false;
        }
        return result;
    }

    /**
     * conver a xml config file to service specific config file.
     * @param config the service specific config file
     * @throws Exception convert error
     */
    public void convertConfig(String config) throws Exception
    {
        conf.writeConfig(new File(config));
    }

    public static void main(String args[])
    {
        if(args.length<1||
            (!args[0].equalsIgnoreCase("-convert")&&!args[0].equalsIgnoreCase("-check")&&!args[0].equalsIgnoreCase("-convertforce")))
        {
            System.out.println("Usage: TrackAnalyserConfig -command args");
            System.out.println();
            System.out.println("where commands include:");
            System.out.println("\t-convert\tconvert the xml config file to service specific config file.");
            System.out.println("\t-convertforce\tconvert the xml config file to service specific config file,ignor check error.");
            System.out.println("\t-check\tcheck the xml config file.");
            return;
        }
        if(args[0].equalsIgnoreCase("-check"))
        {
            if(args.length!=2)
                System.out.println("Usage: TrackAnalyserConfig -check xml_file");
            else
            {
                SaveCenterConfig taConfig=null;
                System.out.println("Parsing "+args[1]+" ...");
                try
                {
                    taConfig=new SaveCenterConfig(args[1]);
                }
                catch(Exception e)
                {
                    e.printStackTrace();
                    System.out.println("Parsing "+args[1]+" failed,check break off.");
                    return;
                }
                System.out.println("Parsing finished,now checking ...");
                if(taConfig.checkConfig())
                    System.out.println("Check passed.");
                else
                    System.out.println("Check not passed.");
                taConfig.refreshXmlFile();
            }
        }
        else if(args[0].equalsIgnoreCase("-convert"))
        {
            if(args.length!=3)
                System.out.println("Usage: TrackAnalyserConfig -convert xml_file config_file");
            else
            {
                SaveCenterConfig taConfig=null;
                System.out.println("Parsing "+args[1]+" ...");
                try
                {
                    taConfig=new SaveCenterConfig(args[1]);
                }
                catch(Exception e)
                {
                    e.printStackTrace();
                    System.out.println("Parsing "+args[1]+" failed,check break off.");
                    return;
                }
                System.out.println("Parsing finished,now checking ...");
                if(taConfig.checkConfig())
                    System.out.println("Check passed,now converting ...");
                else
                {
                    System.out.println("Check not passed,convert break off.");
                    taConfig.refreshXmlFile();
                    return;
                }
                taConfig.refreshXmlFile();
                try
                {
                    taConfig.convertConfig(args[2]);
                    System.out.println("Convert finished.");
                }
                catch(Exception e)
                {
                    e.printStackTrace();
                    System.out.println("Convert error occured.");
                }
            }
        }
        else if(args[0].equalsIgnoreCase("-convertforce"))
        {
            if(args.length!=3)
                System.out.println("Usage: TrackAnalyserConfig -convertforce xml_file config_file");
            else
            {
                SaveCenterConfig taConfig=null;
                System.out.println("Parsing "+args[1]+" ...");
                try
                {
                    taConfig=new SaveCenterConfig(args[1]);
                }
                catch(Exception e)
                {
                    e.printStackTrace();
                    System.out.println("Parsing "+args[1]+" failed,convert break off.");
                    return;
                }
                System.out.println("Start converting ...");
                try
                {
                    taConfig.convertConfig(args[2]);
                    System.out.println("Convert finished.");
                }
                catch(Exception e)
                {
                    e.printStackTrace();
                    System.out.println("Convert error occured.");
                }
            }
        }
    }
}