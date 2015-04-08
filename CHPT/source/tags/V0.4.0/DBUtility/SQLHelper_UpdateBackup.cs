using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Text;
using SYSModel;
namespace DBUtility
{
    public class SQLHelper_UpdateBackup
    {
        private static List<string> _tables = null;
        private static readonly Object locker = new Object();
        public static List<string> Tables
        {
            get
            {
                if (_tables == null)
                {
                    lock (locker)
                    {
                        if (_tables == null)
                        {
                            _tables = System.Configuration.ConfigurationManager.AppSettings["BackUpTables"].Split(',').ToList<string>();
                        }
                    }
                }
                return _tables;
            }
        }


        /// </summary> sql语句处理
        /// <param name="connString">连接连接字符串</param>
        /// <param name="cmdText">sql语句</param>
        /// <param name="modifyCmdText">修改后的字串</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>返回是否修改的标识</returns>
        public static bool TryUpdateSQLBackup(string connString, string cmdText, out string modifyCmdText, params SqlParameter[] commandParameters)
        {
            bool modifyFlag = false;
            StringBuilder strBuilder = new StringBuilder(cmdText);
            string patternStr = ";";
            Regex regSChar = new Regex(patternStr, RegexOptions.IgnoreCase);
            Match m = null;
            Match mUpdate = null;
            Match m1 = null;
            int index = 0;
            int insertSQLLength = 0;

            string patUpdate = "update ";
            Regex regUpdate0 = new Regex(patUpdate, RegexOptions.IgnoreCase);
            mUpdate = regUpdate0.Match(cmdText, index);
            if (mUpdate.Success)
            {
                m1 = regSChar.Match(cmdText, mUpdate.Index);
                string updateStr = string.Empty;
                modifyFlag = true;
                if (!m1.Success)
                {
                    updateStr = cmdText;
                }
                else
                {
                    updateStr = cmdText.Substring(mUpdate.Index, m1.Index - mUpdate.Index);
                }
                string tableName = string.Empty;
                IList<SqlParameter> sqlParamsList = new List<SqlParameter>();
                Dictionary<string, string> setValueDic = new Dictionary<string, string>();
                string selStr = ProcessUpdateSQL(updateStr, out tableName, out sqlParamsList, out setValueDic, commandParameters);
                string tabName = tableName.TrimStart('[').TrimEnd(']');
                if (Tables.Contains(tabName))
                {
                    bool existTableFlag = ExistsTable(connString, tableName);
                    DataSet dsFields = new DataSet();
                    IList<string> pkList = new List<string>();
                    string creaTableSQL = CreateTable(connString, !existTableFlag, tableName, out dsFields, out pkList);
                    if (!existTableFlag)
                    {
                        SqlHelper.ExecuteNonQueryTran(connString, CommandType.Text, creaTableSQL, new UserIDOP() { UserID = "-999", OPName = "建备份表" }, null);
                    }
                    string insertSQL = CreateInsertSQL(connString, tableName, dsFields, pkList, selStr, setValueDic, sqlParamsList.ToArray<SqlParameter>());
                    strBuilder.Insert(mUpdate.Index, insertSQL);
                    insertSQLLength = insertSQL.Length;
                }
            }
            string pat = "(;| )update ";
            Regex regUpdate = new Regex(pat, RegexOptions.IgnoreCase);
            if (mUpdate.Success)
            {
                if (m1.Success)
                {

                    //m = regUpdate.Match(mySB.ToString(), m1.Index + insertSQLLength);
                    m = regUpdate.Match(cmdText, m1.Index);
                }
                else
                {
                    modifyCmdText = strBuilder.ToString();
                    return modifyFlag;
                }
            }
            else
            {
                //m = regUpdate.Match(mySB.ToString(), index);
                m = regUpdate.Match(cmdText, index);
            }

            //insertSQLLength = 0;
            while (m.Success)
            {
                //while (index < cmdText.Length)
                //{
                //m0 = regUpdate.Match(cmdText, index);
                //m1 = regSChar.Match(mySB.ToString(), m.Index + 6);
                m1 = regSChar.Match(cmdText, m.Index + 6);
                //m0 = Regex.Match(cmdText, " update ");
                //m1 = Regex.Match(cmdText, ";");       
                string updateStr = string.Empty;
                modifyFlag = true;
                //updateStr = mySB.ToString().Substring(m.Index, m1.Index - m.Index);
                updateStr = cmdText.Substring(m.Index, m1.Index - m.Index);

                string tableName = string.Empty;
                IList<SqlParameter> sqlParamsList = new List<SqlParameter>();
                Dictionary<string, string> setValueDic = new Dictionary<string, string>();
                string selStr = ProcessUpdateSQL(updateStr, out tableName, out sqlParamsList, out setValueDic, commandParameters);
                string tabName = tableName.TrimStart('[').TrimEnd(']');
                if (Tables.Contains(tabName))
                {
                    //StringBuilder insertStrSB = new StringBuilder();
                    bool existTableFlag = ExistsTable(connString, tableName);
                    DataSet dsFields = new DataSet();
                    IList<string> pkList = new List<string>();
                    string creaTableSQL = CreateTable(connString, !existTableFlag, tableName, out dsFields, out pkList);
                    //获取源表所有的字段列名
                    //生成建表语句，表名_backUp
                    //加入建表脚本
                    //insertStrSB.Append(creaTableSQL);
                    if (!existTableFlag)
                    {
                        SqlHelper.ExecuteNonQueryTran(connString, CommandType.Text, creaTableSQL, new UserIDOP() { UserID = "-999", OPName = "建备份表" }, null);
                    }
                    //获取源表DataSet
                    //遍历记录生成 Insert语句集合
                    //如果存在表，存在关联记录ID和父记录ID编号同源表该行记录的记录ID
                    //则将该条记录的父记录ID改为新插入的记录的记录ID,
                    //新插入的该条记录的父记录ID改为源表该行记录的记录ID
                    string insertSQL = CreateInsertSQL(connString, tableName, dsFields, pkList, selStr, setValueDic, sqlParamsList.ToArray<SqlParameter>());
                    //insertStrSB.Append(insertSQL);                          
                    //modifyFlag为true,则采用事务型执行调用 
                    if (!string.IsNullOrEmpty(insertSQL))
                    {
                        strBuilder.Insert(m.Index + insertSQLLength, insertSQL);
                        insertSQLLength = insertSQLLength + insertSQL.Length;
                    }
                }
                //index = m1.Index+1;
                //if (m1.Index < 1)
                //{
                //    break;
                //}         
                m = m.NextMatch();
            }
            modifyCmdText = strBuilder.ToString();
            return modifyFlag;
        }

        private static string ProcessUpdateSQL(string updateStr, out string tableName, out IList<SqlParameter> sqlParamsList, out Dictionary<string, string> setValueDic, params SqlParameter[] commandParameters)
        {
            bool spaceFlag = false;
            int i = 4;
            int sL = 0;
            int eL = 0;
            while (!spaceFlag)
            {
                i = i + 1;
                string s = updateStr.Substring(i, 1);
                spaceFlag = string.IsNullOrWhiteSpace(s);
            }
            while (spaceFlag)
            {
                i = i + 1;
                string s = updateStr.Substring(i, 1);
                spaceFlag = string.IsNullOrWhiteSpace(s);
                sL = i;
            }
            while (!spaceFlag)
            {
                i = i + 1;
                string s = updateStr.Substring(i, 1);
                spaceFlag = string.IsNullOrWhiteSpace(s);
                eL = i;
            }
            tableName = updateStr.Substring(sL, eL - sL);
            string pat = " where ";
            Regex whUpdate = new Regex(pat, RegexOptions.IgnoreCase);
            Match m0 = null;
            m0 = whUpdate.Match(updateStr, eL);
            string whereStr = string.Empty;
            if (m0.Index > -1)
            {
                whereStr = updateStr.Substring(m0.Index, updateStr.Length - m0.Index);
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(" select * from ");
            sb.Append(tableName + " ");
            sb.Append(whereStr + " ;");


            //int index = 5;
            //int pindex = 5;
            //IList<SqlParameter> ParamsList = new List<SqlParameter>();
            //while (index < updateStr.Length - 1)
            //{

            //    string param = findSQLParam(updateStr, pindex, out index);

            //    pindex = index;

            //    if (string.IsNullOrEmpty(param))
            //    {
            //        break;
            //    }
            //  for (int praI = 0; praI < commandParameters.Length; praI++)
            //  {
            //      if (commandParameters[praI].ParameterName == param)
            //      {
            //          ParamsList.Add(commandParameters[praI]);
            //      }
            //  }

            //}

            int index = 5;
            string pat_Symbol = "@";
            IList<SqlParameter> ParamsList = new List<SqlParameter>();
            Regex regSymbol = new Regex(pat_Symbol, RegexOptions.IgnoreCase);
            Match mSymbol = regSymbol.Match(updateStr, index);

            while (mSymbol.Success)
            {
                bool SymbolFlag = false;
                int symBolP = 0;
                symBolP = mSymbol.Index;
                while (!SymbolFlag)
                {
                    string c = string.Empty;
                    if (symBolP > (updateStr.Length - 1))
                    {
                        symBolP = updateStr.Length;
                        SymbolFlag = true;
                    }
                    else
                    {
                        c = updateStr.Substring(symBolP, 1);
                    }
                    if (!SymbolFlag)
                    {
                        SymbolFlag = string.IsNullOrWhiteSpace(c);
                        if (!SymbolFlag)
                        {
                            SymbolFlag = string.Equals(c, ",");
                        }
                        symBolP = symBolP + 1;
                    }
                }
                string param = updateStr.Substring(mSymbol.Index, symBolP - mSymbol.Index);
                param = param.TrimEnd(' ');
                param = param.TrimEnd(',');
                param = param.TrimEnd(';');
                for (int praI = 0; praI < commandParameters.Length; praI++)
                {
                    if (commandParameters[praI].ParameterName == param)
                    {
                        if (!ParamsList.Contains(commandParameters[praI]))
                        {
                            ParamsList.Add(commandParameters[praI]);
                        }
                    }
                }
                mSymbol = mSymbol.NextMatch();
            }

            #region 查找赋值参数值
            Dictionary<string, string> paramValueDic = new Dictionary<string, string>();
            string patSet = " set ";
            Regex regSet = new Regex(patSet, RegexOptions.IgnoreCase);
            Match mSet = regSet.Match(updateStr, 7);
            int endIndex = updateStr.Length;

            string pat_Symbol0 = "=";
            Regex regSymbol0 = new Regex(pat_Symbol0, RegexOptions.IgnoreCase);
            Match mSymbol0 = null;



            string paramSQLStr = updateStr;
            if (m0.Success)
            {
                endIndex = m0.Index;
                paramSQLStr = updateStr.Substring(0, endIndex);
            }
            if (mSet.Success)
            {
                bool controlFlag = false;
                int setIndex = mSet.Index;
                int sIndex = 0;
                int eIndex = 0;
                while (eIndex < endIndex)
                {
                    mSymbol0 = regSymbol0.Match(paramSQLStr, eIndex);
                    if (!mSymbol0.Success)
                    {
                        break;
                    }
                    bool fistChar = false;

                    while ((!controlFlag) && (setIndex < paramSQLStr.Length - 1))
                    {
                        string s0 = paramSQLStr.Substring(setIndex, 1);
                        if (!fistChar && string.IsNullOrWhiteSpace(s0))
                        {
                            setIndex = setIndex + 1;
                        }
                        fistChar = true;
                        s0 = paramSQLStr.Substring(setIndex, 1);
                        if (string.Equals(s0, ","))
                        {
                            controlFlag = true;
                            setIndex = setIndex + 1;
                            string s1 = paramSQLStr.Substring(setIndex, 1);
                            if (!string.IsNullOrWhiteSpace(s1))
                            {
                                sIndex = setIndex;
                                controlFlag = true;
                                break;
                            }
                            else
                            {
                                controlFlag = false;
                            }
                        }
                        else
                        {
                            setIndex = setIndex + 1;
                            controlFlag = string.IsNullOrWhiteSpace(s0);
                            sIndex = setIndex;
                        }
                    }
                    bool equalFlag = false;
                    bool spaceNullChar = false;
                    while (controlFlag && (setIndex < paramSQLStr.Length - 1))
                    {

                        string s = paramSQLStr.Substring(setIndex, 1);
                        controlFlag = string.IsNullOrWhiteSpace(s);
                        if (controlFlag)
                        {
                            spaceNullChar = true;
                            setIndex = setIndex + 1;
                            continue;
                        }
                        else if (spaceNullChar)
                        {
                            if (!string.IsNullOrWhiteSpace(s))
                            {
                                if (string.Equals(s, "="))
                                {
                                    equalFlag = true;
                                    eIndex = setIndex;
                                    break;
                                }
                                sIndex = setIndex;
                                break;
                            }
                        }
                        setIndex = setIndex + 1;
                        if (string.Equals(s, "="))
                        {
                            equalFlag = true;
                            eIndex = setIndex;
                            break;
                        }
                        if (!controlFlag)
                        {
                            controlFlag = string.Equals(s, "[");
                        }
                        controlFlag = !string.IsNullOrWhiteSpace(s);
                    }
                    if (!equalFlag)
                    {
                        while ((!controlFlag) && (setIndex < paramSQLStr.Length - 1))
                        {

                            string s = paramSQLStr.Substring(setIndex, 1);
                            setIndex = setIndex + 1;
                            controlFlag = string.IsNullOrWhiteSpace(s);
                            if (!controlFlag)
                            {
                                controlFlag = string.Equals(s, "=");
                            }
                            eIndex = setIndex;
                        }
                    }
                    string field = string.Empty;
                    if (eIndex > sIndex)
                    {
                        field = paramSQLStr.Substring(sIndex, eIndex - sIndex);
                        field = field.TrimStart(',');
                        field = field.TrimEnd('=');
                        field = field.TrimStart(' ').TrimEnd(' ');
                        field = field.TrimStart('[').TrimEnd(']');
                    }
                    else
                    {
                        eIndex = paramSQLStr.Length;
                    }
                    controlFlag = false;
                    mSymbol0 = regSymbol0.Match(paramSQLStr, eIndex - 2);
                    string paramSymbol = string.Empty;
                    if (mSymbol0.Success)
                    {
                        setIndex = mSymbol0.Index;
                        while ((!controlFlag) && (setIndex < paramSQLStr.Length - 1))
                        {
                            setIndex = setIndex + 1;
                            string s = paramSQLStr.Substring(setIndex, 1);
                            if (!controlFlag)
                            {
                                controlFlag = string.Equals(s, "@");
                            }
                            if (!controlFlag)
                            {
                                controlFlag = string.Equals(s, "'");
                            }
                            controlFlag = !string.IsNullOrWhiteSpace(s);
                            sIndex = setIndex;
                        }

                        int bracketsFlag = 0;

                        while (controlFlag && (setIndex < paramSQLStr.Length - 1))
                        {
                            setIndex = setIndex + 1;
                            string s = paramSQLStr.Substring(setIndex, 1);
                            if (string.Equals(s, "("))
                            {
                                bracketsFlag = bracketsFlag + 1;
                            }
                            else if (string.Equals(s, ")"))
                            {
                                bracketsFlag = bracketsFlag - 1;
                            }
                            else
                            {
                                controlFlag = !string.IsNullOrWhiteSpace(s);
                                if (controlFlag)
                                {
                                    if (bracketsFlag == 0)
                                    {
                                        controlFlag = !string.Equals(s, ",");
                                    }
                                }
                            }
                            eIndex = setIndex;
                        }
                        paramSymbol = paramSQLStr.Substring(sIndex, eIndex - sIndex + 1);
                        paramSymbol = paramSymbol.TrimEnd(',');
                        paramSymbol = paramSymbol.TrimStart(' ').TrimEnd(' ');
                        paramSymbol = paramSymbol.TrimStart('\'').TrimEnd('\'');
                    }
                    if (!string.IsNullOrEmpty(field))
                    {
                        paramValueDic.Add(field.ToLower(), paramSymbol);
                    }
                }
            }
            #endregion
            setValueDic = paramValueDic;
            sqlParamsList = ParamsList;
            return sb.ToString();
        }

        private static bool ExistsTable(string connString, string tableName)
        {
            tableName = tableName.TrimStart('[');
            tableName = tableName.TrimEnd(']');
            string SQLStr = string.Format("SELECT object_id FROM sys.objects WHERE object_id = OBJECT_ID(N'{0}') AND type in (N'U')", tableName + "_BackUp");
            object id = SqlHelper.ExecuteScalar_Backup(connString, System.Data.CommandType.Text, SQLStr, new UserIDOP() { UserID = "-999", OPName = "查表存在" }, null);
            return (id == null) ? false : true;
        }

        private static string findSQLParam(string connString, string updateStr, int SIndex, out int index)
        {
            string pat = "@";
            Regex regUpdate = new Regex(pat, RegexOptions.IgnoreCase);
            Match m0 = regUpdate.Match(updateStr, SIndex);
            bool spaceFlag = false;
            int i = m0.Index;
            if (i < 1)
            {
                index = updateStr.Length - 1;
                return string.Empty;
            }
            else
            {
                while (!spaceFlag)
                {
                    i = i + 1;
                    if (i < updateStr.Length - 1)
                    {
                        string c = updateStr.Substring(i, 1);
                        spaceFlag = string.IsNullOrWhiteSpace(c);
                    }
                    else
                    {
                        i = i + 1;
                        spaceFlag = true;
                    }
                }
                index = i;
            }
            string param = updateStr.Substring(m0.Index + 1, i - m0.Index - 1);
            param = param.TrimEnd(';');
            index = index + param.Length;
            return param;
        }

        private static string CreateTable(string connString, bool createFlag, string tableName, out DataSet fields, out IList<string> pkList)
        {
            tableName = tableName.TrimStart('[');
            tableName = tableName.TrimEnd(']');
            string SQLStr = string.Format(@"SELECT 
                                           表名=case when a.colorder=1 then d.name else '' end, 
                                           字段名=a.name, 
                                           标识=case when COLUMNPROPERTY(a.id,a.name,'IsIdentity')=1 then '√' else '' end, 
                                           主键=case when exists(SELECT 1 FROM sysobjects where xtype= 'PK' and name in ( 
                                           SELECT name FROM sysindexes WHERE indid in( 
                                           SELECT indid FROM sysindexkeys WHERE id = a.id AND colid=a.colid 
                                           ))) then '1' else '0' end, 
                                           类型=b.name, 
                                           占用字节数=a.length, 
                                           长度=COLUMNPROPERTY(a.id,a.name, 'PRECISION'), 
                                           小数位数=isnull(COLUMNPROPERTY(a.id,a.name, 'Scale'),0), 
                                           允许空=case when a.isnullable=1 then '1'else '0' end, 
                                           默认值=isnull(e.text, ''), 
                                           字段说明=isnull(g.[value], '') 
                                           FROM syscolumns a 
                                           left join systypes b on a.xtype=b.xusertype 
                                           inner join sysobjects d on a.id=d.id and d.xtype= 'U' and d.name <> 'dtproperties' and d.name = '{0}'
                                           left join syscomments e on a.cdefault=e.id 
                                           left join sys.extended_properties g on a.id=g.major_id and a.colid=g.minor_id and g.name='MS_Description' 
                                           order by a.id,a.colorder ", tableName);
            //           string SQLStr0 = string.Format(@" select c.name as fieldName , t.name as dataTypeName , c.prec as dataLength  from syscolumns c inner join systypes t on c.xusertype=t.xusertype 
            //                                            where objectproperty(id,'IsUserTable')=1 and id=object_id('[{0}]')", tableName);
            DataSet dsFields = SqlHelper.ExecuteDataSet_Backup(connString, System.Data.CommandType.Text, SQLStr, new UserIDOP() { UserID = "-999", OPName = "查表设计" }, null);
            fields = dsFields;
            StringBuilder mySB = new StringBuilder();

            if (createFlag)
            {
                mySB.Append("CREATE TABLE [" + tableName + "_BackUp](");
                mySB.Append("[Record_id] [varchar](40) NOT NULL,");
            }
            List<string> keyList = new List<string>();
            string pat = "int";
            Regex regInt = new Regex(pat, RegexOptions.IgnoreCase);
            Match m0 = null;

            foreach (DataRowView dr in dsFields.Tables[0].DefaultView)
            {
                if (dr[3].ToString() == "1")
                {
                    keyList.Add(dr[1].ToString());
                }
                if (createFlag)
                {
                    string dataTyep = dr[4].ToString();
                    string ziDuan = string.Format("[{0}] [{1}] ", dr[1].ToString(), dataTyep);
                    m0 = regInt.Match(dataTyep);
                    if (m0.Length > 0)
                    {
                        ziDuan += dr[8].ToString() == "1" ? " NULL," : " NOT NULL,";
                    }
                    else
                    {
                        ziDuan += string.Format("({0}) {1},", dr[6].ToString() + (dr[7].ToString() == "0" ? string.Empty : ("," + dr[7].ToString())), dr[8].ToString() == "1" ? "NULL" : "NOT NULL");
                    }
                    mySB.Append(ziDuan);
                }
            }
            if (createFlag)
            {
                mySB.Append("[PRecord_id] [varchar](40) NOT NULL,");
                mySB.Append("[Record_id_CreateTime] [bigint] NOT NULL,");
                mySB.Append("[Record_id_UpdateTime] [bigint] NOT NULL,");
                mySB.Append("[Record_id_UpdateBy] [bigint] NOT NULL ");
                mySB.Append(@" CONSTRAINT [pk_" + tableName + "_BackUp_" + "Record_id] PRIMARY KEY NONCLUSTERED ([Record_id] ASC)" +
                          "WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY] ");
            }
            pkList = keyList;
            return mySB.ToString();
        }

        private static string CreateInsertSQL(string connString, string tableName, DataSet fields, IList<string> pkList, string selStr, Dictionary<string, string> setValueDic, SqlParameter[] commandParameters)
        {
            tableName = tableName.TrimStart('[');
            tableName = tableName.TrimEnd(']');

            StringBuilder mySB = new StringBuilder();
            DataSet dsFields = SqlHelper.ExecuteDataSet_Backup(connString, System.Data.CommandType.Text, selStr, new UserIDOP() { UserID = "-999", OPName = "查源表表数据" }, commandParameters);
            bool insertSQLflag = false;
            for (int i = 0; i < dsFields.Tables[0].Rows.Count; i++)
            {
                string selBackUp = "select * from " + tableName + "_BackUp where PRecord_id = '-999' ";
                string pkStr = string.Empty;
                int c = 0;
                foreach (string s in pkList)
                {
                    selBackUp += " and " + s + "='" + dsFields.Tables[0].Rows[i][s].ToString() + "'";
                    if (c == 0)
                    {
                        pkStr += s + "='" + dsFields.Tables[0].Rows[i][s].ToString() + "'";
                    }
                    else
                    {
                        pkStr += " and " + s + "='" + dsFields.Tables[0].Rows[i][s].ToString() + "'";
                    }
                    c++;
                }
                selBackUp += " order by [Record_id_CreateTime] asc ;";
                DataSet dsBackUp = SqlHelper.ExecuteDataSet_Backup(connString, System.Data.CommandType.Text, selBackUp, new UserIDOP() { UserID = "-999", OPName = "查拷贝表数据" }, null);
                //if (!(dsBackUp.Tables[0].Rows.Count == 1 || dsBackUp.Tables[0].Rows.Count == 0))
                //{
                //    throw new Exception("拷贝表:" + tableName + "_BackUp" + " 记录存在 ERROR!;" + "sql语句内容：" + selBackUp);
                //}
                //string ID = System.Guid.NewGuid().ToString();
                //if (dsBackUp.Tables[0].Rows.Count > 0)
                //{
                //    for (int b = 0; b < dsBackUp.Tables[0].Rows.Count; b++)
                //    {
                //        mySB.Append(" update " + tableName + "_BackUp ");
                //        mySB.Append(" set PRecord_id = '" + ID + "' , " + " Record_id_UpdateTime='" + DateTime.UtcNow.Ticks.ToString() + "' ");
                //        mySB.Append(" where Record_id = '" + dsBackUp.Tables[0].Rows[b]["Record_id"].ToString() + "' ;");
                //    }
                //}               
                string ID = System.Guid.NewGuid().ToString();
                if (dsBackUp.Tables[0].Rows.Count > 1)
                {
                    for (int b = 0; b < dsBackUp.Tables[0].Rows.Count; b++)
                    {
                        if (b == dsBackUp.Tables[0].Rows.Count - 1)
                        {
                            mySB.Append(" update " + tableName + "_BackUp ");
                            mySB.Append(" set PRecord_id = '" + ID + "' , " + " Record_id_UpdateTime='" + DateTime.UtcNow.Ticks.ToString() + "' ");
                            mySB.Append(" where Record_id = '" + dsBackUp.Tables[0].Rows[0]["Record_id"].ToString() + "' ;");
                        }
                        else
                        {
                            mySB.Append(" update " + tableName + "_BackUp ");
                            mySB.Append(" set PRecord_id = '" + dsBackUp.Tables[0].Rows[b + 1]["Record_id"].ToString() + "' , " + " Record_id_UpdateTime='" + DateTime.UtcNow.Ticks.ToString() + "' ");
                            mySB.Append(" where Record_id = '" + dsBackUp.Tables[0].Rows[b]["Record_id"].ToString() + "' ;");
                        }
                    }
                }
                else if (dsBackUp.Tables[0].Rows.Count == 1)
                {
                    mySB.Append(" update " + tableName + "_BackUp ");
                    mySB.Append(" set PRecord_id = '" + ID + "' , " + " Record_id_UpdateTime='" + DateTime.UtcNow.Ticks.ToString() + "' ");
                    mySB.Append(" where Record_id = '" + dsBackUp.Tables[0].Rows[0]["Record_id"].ToString() + "' ;");
                }

                mySB.Append("INSERT INTO " + tableName + "_BackUp (");
                mySB.Append("[Record_id],");
                for (int f = 0; f < fields.Tables[0].Rows.Count; f++)
                {
                    mySB.Append("[" + fields.Tables[0].Rows[f][1].ToString() + "]" + ",");
                }
                mySB.Append("[PRecord_id],[Record_id_CreateTime],[Record_id_UpdateTime],[Record_id_UpdateBy]");
                mySB.Append(" ) VALUES ( ");
                mySB.Append("'" + ID + "'" + ",");

                for (int v = 0; v < fields.Tables[0].Rows.Count; v++)
                {
                    string field = fields.Tables[0].Rows[v][1].ToString();
                    //string dataTypeStr = dsFields.Tables[0].Columns[field].DataType.ToString();

                    if (dsFields.Tables[0].Rows[i][field] == null)
                    {
                        mySB.Append("null,");
                    }
                    else if (string.IsNullOrEmpty(dsFields.Tables[0].Rows[i][field].ToString()))
                    {
                        mySB.Append("null,");
                    }
                    else if (dsFields.Tables[0].Columns[field].DataType == typeof(System.Byte[]))
                    {
                        string str = System.Text.Encoding.Unicode.GetString((byte[])dsFields.Tables[0].Rows[i][field]);
                        mySB.Append("'" + str + "'" + ",");
                    }
                    else
                    {
                        mySB.Append("'" + dsFields.Tables[0].Rows[i][field] + "'" + ",");
                    }
                    if (setValueDic.ContainsKey(field))
                    {
                        string dataValue = string.Empty;
                        if (dsFields.Tables[0].Columns[field].DataType == typeof(System.Byte[]))
                        {
                            if (dsFields.Tables[0].Rows[i][field] == null || dsFields.Tables[0].Rows[i][field] == DBNull.Value)
                            {

                            }
                            else
                            {
                                dataValue = System.Text.Encoding.Unicode.GetString((byte[])dsFields.Tables[0].Rows[i][field]);
                            }
                        }
                        else
                        {
                            if (dsFields.Tables[0].Rows[i][field] == null || dsFields.Tables[0].Rows[i][field] == DBNull.Value)
                            {

                            }
                            else
                            {
                                dataValue = dsFields.Tables[0].Rows[i][field].ToString().Trim();
                            }
                        }
                        string paramValue = setValueDic[field.ToLower()].ToString();
                        if (paramValue.Contains("EncryptByPassPhrase"))
                        {
                            int i0 = paramValue.IndexOf('(');
                            int i1 = paramValue.IndexOf(',');
                            int i2 = paramValue.IndexOf(')');
                            string keyS = paramValue.Substring(i0, i1 - i0);
                            string value = paramValue.Substring(i1, i2 - i1);
                            i0 = value.IndexOf('\'');
                            i1 = value.LastIndexOf('\'');
                            if (i0 != -1 && i1 != -1 && i1 > i0)
                            {
                                paramValue = value.Substring(i0 + 1, i1 - i0 - 1).Trim();
                                string selEncrpStr = "select DECRYPTBYPASSPHRASE" + keyS + "," + field + ")" + " from " + tableName + " where " + pkStr;
                                object objByteArry = SqlHelper.ExecuteScalar_Backup(connString, System.Data.CommandType.Text, selEncrpStr, new UserIDOP() { UserID = "-999", OPName = "查拷加密列数据" }, null);
                                if (objByteArry != null)
                                {
                                    dataValue = System.Text.Encoding.Unicode.GetString((byte[])objByteArry).Trim();
                                }
                                if (dataValue != paramValue)
                                {
                                    insertSQLflag = true;
                                }
                            }
                        }
                        else
                        {

                            string updateValue = string.Empty;
                            if (paramValue.Contains('@'))
                            {
                                foreach (SqlParameter param in commandParameters)
                                {
                                    if (param.ParameterName == paramValue)
                                    {
                                        updateValue = param.Value.ToString();
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                updateValue = paramValue;
                            }

                            if ((updateValue.Trim() != dataValue) && (field != "update_time"))
                            {
                                insertSQLflag = true;//前后两次有不同的值，才允许往备份表中插入备份记录
                            }
                        }
                    }
                }
                mySB.Append("'-999',");
                mySB.Append("'" + DateTime.UtcNow.Ticks.ToString() + "',");
                mySB.Append("'" + DateTime.UtcNow.Ticks.ToString() + "',");
                mySB.Append("'-999'");
                mySB.Append(" ); ");
            }
            if (!insertSQLflag)
            {
                return string.Empty;
            }
            return mySB.ToString();
        }
    }
}
