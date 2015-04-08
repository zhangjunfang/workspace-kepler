using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data;
using System.Reflection;
using SYSModel;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using System.IO;
using ServiceStationClient.ComponentUI.TextBox;

namespace HXCPcClient
{
    public enum FunctionName
    {
        None,
        SystemManagement,//系统管理
        DataManagement,//数据管理
        BusinessAnalysis,//经营分析
        CustomerService,//客户服务
        FinancialManagement,//财务管理
        AccessoriesBusiness,//配件业务
        RepairBusiness//维修业务
    }
    public class CommonCtrl
    {

        /// <summary>
        /// 树节点状态改变
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="nodeChecked"></param>
        public static void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
        {
            treeNode.Checked = nodeChecked;
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0)
                {
                    CheckAllChildNodes(node, nodeChecked);
                }
            }
        }
        /// <summary>
        /// tree绑定值
        /// </summary>
        /// <param name="Nds">tree.Nodes</param>
        /// <param name="parentId">根节点pid</param>
        /// <param name="dv">数据集DataView</param>
        public static void InitTree(TreeNodeCollection Nds, string parentId, DataView dv)
        {
            TreeNode tmpNd;
            dv.RowFilter = "parent_id='" + parentId + "'";
            foreach (DataRowView drv in dv)
            {
                tmpNd = new TreeNode();
                tmpNd.Tag = drv;
                tmpNd.Text = drv["name"].ToString(); //name
                tmpNd.Name = drv["id"].ToString();//id
                Nds.Add(tmpNd);
                InitTree(tmpNd.Nodes, drv["id"].ToString(), dv);
            }
        }
        /// <summary>
        /// 设置TreeView选中节点
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="selectStrName">选中节点name</param>
        public static void SelectTreeView(TreeViewEx treeView, string selectStrName)
        {
            treeView.Focus();
            TreeNode[] selectnode = treeView.Nodes.Find(IsNullToString(selectStrName), true);
            if (selectnode != null && selectnode.Length > 0)
            {
                treeView.SelectedNode = selectnode[0];//选中
                if (selectnode[0].Parent != null)
                    selectnode[0].Parent.Expand();//展开父级
            }
            //for (int i = 0; i < treeView.Nodes.Count; i++)
            //{
            //    for (int j = 0; j < treeView.Nodes[i].Nodes.Count; j++)
            //    {
            //        if (treeView.Nodes[i].Nodes[j].Name == selectStrName)
            //        {
            //            treeView.SelectedNode = treeView.Nodes[i].Nodes[j];//选中
            //            //treeView.Nodes[i].Nodes[j].Checked = true;
            //            treeView.Nodes[i].Expand();//展开父级
            //            return;
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 用户密码复杂验证
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool PasswordValidate(string password)
        {
            try
            {
                Regex regex1 = new Regex(@"^[_#!$@\%\^\&\*\(\)\>\<\/\\da-zA-Z0-9]*$");
                Regex regex2 = new Regex(@"^[0-9]*$");
                Regex regex3 = new Regex(@"^[a-zA-Z]*$");
                Regex regex4 = new Regex(@"^[_#!@$\%\^\&\*\(\)\>\<\/]*$");

                if (regex1.IsMatch(password)
                 && !regex2.IsMatch(password)
                 && !regex3.IsMatch(password)
                 && !regex4.IsMatch(password))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 菜单列表 返回下一级列表
        /// </summary>
        /// <param name="Pid">上级id</param>
        /// <returns></returns>
        public static DataTable getDataChildsByPid(string Pid)
        {
            return LocalCache.GetParentFunction(Pid);
        }

        /// <summary>
        /// 窗体名 放射出窗体
        /// </summary>
        /// <param name="strUserControlName">命名空间.窗体名</param>
        /// <returns>用户控件-功能窗体</returns>
        public static UserControl TraverseForm(string strUserControlName)
        {
            if (strUserControlName == null || strUserControlName.Length == 0)
            {
                return null;
            }
            UserControl uc = null;
            object obj = Assembly.GetExecutingAssembly().CreateInstance(strUserControlName);
            if (obj != null)
            {
                uc = obj as UserControl;
            }
            return uc;
        }

        /// <summary>
        /// 对象转string时检查是否是null，如果是则返回“”
        /// </summary>
        /// <param name="obj">要转string的对象</param>
        /// <returns>转换后的string</returns>
        public static string IsNullToString(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return string.Empty;
            }
            else
            {
                return obj.ToString();
            }
        }

        /// <summary>
        /// 从字典表中绑定下拉列表
        /// </summary>
        /// <param name="cbo">下拉框</param>
        /// <param name="code">字典编码</param>
        /// <param name="isAll">是否有全部</param>
        public static void BindComboBoxByDictionarr(ComboBox cbo, string code, bool isAll)
        {
            DataTable dt = GetDictByCode(code, isAll);
            cbo.DataSource = dt;
            cbo.ValueMember = "dic_id";
            cbo.DisplayMember = "dic_name";
        }
        public static void BindComboBoxByDictionarr(DataGridViewComboBoxColumn dgvcbc, string code)
        {
            DataTable dt = GetDictByCode(code, false);
            dgvcbc.DataSource = dt;
            dgvcbc.ValueMember = "dic_id";
            dgvcbc.DisplayMember = "dic_name";
        }


        /// <summary>
        /// 根据表名获取数据绑定到下拉框
        /// </summary>
        /// <remarks> add by kord</remarks>
        /// <param name="cbo">下拉框</param>
        /// <param name="table">表名</param>
        /// <param name="valueColName">值列名</param>
        /// <param name="displayColName">显示值列名</param>
        /// <param name="isAll">是否有全部</param>
        public static void BindComboBoxByTable(ComboBox cbo, string table, String valueColName, String displayColName, Boolean isAll)
        {
            var dt = GetDictionarrByTableInfo(table, valueColName, displayColName, isAll);
            cbo.DataSource = dt;
            cbo.ValueMember = valueColName;
            cbo.DisplayMember = displayColName;
        }
        /// <summary>
        /// 根据父级编码,获取所有子级字典
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="valueColName">值列名</param>
        /// <param name="displayColName">显示值列名</param>
        /// <param name="isAll">是否有全部</param>
        /// <returns></returns>
        public static DataTable GetDictionarrByTableInfo(string table, String valueColName, String displayColName, bool isAll)
        {
            var ds1 = DBHelper.GetTable("", table, valueColName + "," + displayColName, "", "", "");
            if (ds1 == null || ds1.Rows.Count == 0)
            {
                return null;
            }
            if (!isAll) return ds1;
            var dr = ds1.NewRow();
            dr[valueColName] = string.Empty;
            dr[displayColName] = "全部";
            ds1.Rows.InsertAt(dr, 0);
            return ds1;
        }

        /// <summary>
        /// 根据父级编码,获取所有子级字典
        /// </summary>
        /// <param name="code"></param>
        /// <param name="isAll">是否有全部</param>
        /// <returns></returns>
        public static DataTable GetDictByCode(string code, bool isAll)
        {
            if (LocalCache.DtDict == null)
            {
                LocalCache.Update(CacheList.Dict);
            }
            DataTable dt = LocalCache.DtDict.Clone();
            if (LocalCache.DtDict != null
                && LocalCache.DtDict.Rows.Count > 0)
            {
                DataRow[] drs = LocalCache.DtDict.Select("dic_code = '" + code + "'");
                if (drs.Length > 0)
                {
                    drs = LocalCache.DtDict.Select("parent_id = '" + drs[0]["dic_id"] + "'");
                    foreach (DataRow dr in drs)
                    {
                        dt.ImportRow(dr);
                    }
                }
            }
            if (isAll)
            {
                DataRow dr = dt.NewRow();
                dr["dic_id"] = string.Empty;
                dr["dic_name"] = "全部";
                dt.Rows.InsertAt(dr, 0);
            }
            return dt;
        }


        #region --缓存数据绑定

        /// <summary> 码表-Dgv下拉框绑定 
        /// </summary>
        /// <param name="dgvcbc"></param>
        /// <param name="code"></param>
        public static void DgCmbBindDict(DataGridViewComboBoxColumn dgvcmb, string code)
        {
            DataTable dt = GetDictByCode(code, false);
            dgvcmb.DataSource = dt;
            dgvcmb.ValueMember = "dic_id";
            dgvcmb.DisplayMember = "dic_name";
        }
        /// <summary> 码表-下拉框绑定 
        /// </summary>
        /// <param name="cobox"></param>
        /// <param name="code"></param>
        public static void CmbBindDict(ComboBox cobox, string code)
        {
            CmbBindDict(cobox, code, true);
        }
        /// <summary> 绑定码表
        /// </summary>
        /// <param name="cobox"></param>
        /// <param name="code"></param>
        /// <param name="isAll">是否显示全部</param>
        public static void CmbBindDict(ComboBox cobox, string code, bool isAll)
        {
            if (code == null || code.Length == 0)
            {
                return;
            }

            DataTable dt = null;
            if (LocalCache.DtDict == null)
            {
                LocalCache.Update(CacheList.Dict);
            }

            if (LocalCache.DtDict == null)
            {
                return;
            }
            dt = LocalCache.DtDict.Clone();

            if (LocalCache.DtDict != null
            && LocalCache.DtDict.Rows.Count > 0)
            {
                DataRow[] drs = LocalCache.DtDict.Select("dic_code = '" + code + "'");
                if (drs.Length > 0)
                {
                    drs = LocalCache.DtDict.Select("parent_id = '" + drs[0]["dic_id"] + "'");
                    foreach (DataRow dr in drs)
                    {
                        dt.ImportRow(dr);
                    }
                }
            }
            else
            {
                dt = LocalCache.DtDict;
            }

            List<ListItem> list = new List<ListItem>();
            if (isAll)
            {
                list.Add(new ListItem(string.Empty, "全部"));
            }
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new ListItem(dr["dic_id"], dr["dic_name"].ToString()));
            }
            cobox.DataSource = list;
            cobox.ValueMember = "Value";
            cobox.DisplayMember = "Text";
        }

        /// <summary>
        /// 根据公司Id获取部门
        /// </summary>
        /// <param name="comId"></param>
        /// <returns></returns>
        public static DataTable GetDeptByCom(string comId)
        {
            if (LocalCache.DtDept == null)
            {
                LocalCache.Update(CacheList.Org);
            }

            DataTable dt = null;
            if (comId != null && comId.Length > 0)
            {
                dt = LocalCache.DtDept.Clone();
                if (LocalCache.DtDept != null
                && LocalCache.DtDept.Rows.Count > 0)
                {
                    DataRow[] drs = LocalCache.DtDict.Select("com_id = '" + comId + "'");
                    foreach (DataRow dr in drs)
                    {
                        dt.ImportRow(dr);
                    }
                }
            }
            else
            {
                dt = LocalCache.DtDept;
            }
            return dt;
        }
        /// <summary> 部门-Dgv下拉绑定
        /// </summary>
        /// <param name="dgvcmb"></param>
        /// <param name="comId"></param>
        public static void DgCmbBindDeptment(DataGridViewComboBoxColumn dgvcmb, string comId)
        {
            DataTable dt = GetDeptByCom(comId);
            if (dt != null)
            {
                dgvcmb.DataSource = dt;
                dgvcmb.ValueMember = "org_id";
                dgvcmb.DisplayMember = "org_name";
            }
        }
        /// <summary> 绑定部门
        /// </summary>
        /// <param name="cobox"></param>
        /// <param name="comId">公司Id</param>
        public static void CmbBindDeptment(ComboBox cobox, string comId)
        {
            CombBindDeptment(cobox, comId, true);
        }
        /// <summary> 绑定部门 
        /// </summary>
        /// <param name="cobox"></param>
        /// <param name="comId"></param>
        /// <param name="isAll"></param>
        public static void CombBindDeptment(ComboBox cobox, string comId, bool isAll)
        {
            DataTable dt = GetDeptByCom(comId);

            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem("", "全部"));
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new ListItem(dr["org_id"], dr["org_name"].ToString()));
            }
            cobox.DataSource = list;
            cobox.ValueMember = "Value";
            cobox.DisplayMember = "Text";
        }


        public static DataTable GetUserByOrg(string orgId)
        {
            if (LocalCache.DtUser == null)
            {
                LocalCache.Update(CacheList.User);
            }

            DataTable dt = null;
            if (orgId != null && orgId.Length > 0)
            {
                dt = LocalCache.DtUser.Clone();
                if (LocalCache.DtUser != null
                && LocalCache.DtUser.Rows.Count > 0)
                {
                    DataRow[] drs = LocalCache.DtUser.Select("org_id = '" + orgId + "'");
                    foreach (DataRow dr in drs)
                    {
                        dt.ImportRow(dr);
                    }
                }
            }
            else
            {
                dt = LocalCache.DtUser;
            }
            return dt;
        }
        /// <summary> 用户-Dgv下拉框绑定
        /// </summary>
        /// <param name="dgvcmb"></param>
        /// <param name="code"></param>
        public static void DgCmbBindUser(DataGridViewComboBoxColumn dgvcmb, string orgId)
        {
            DataTable dt = GetUserByOrg(orgId);
            if (dt != null)
            {
                dgvcmb.DataSource = dt;
                dgvcmb.ValueMember = "user_id";
                dgvcmb.DisplayMember = "user_name";
            }
        }
        /// <summary> 绑定用户
        /// </summary>
        /// <param name="cobox"></param>
        /// <param name="orgId">组织Id</param>
        public static void CmbBindUser(ComboBox cobox, string orgId)
        {
            CmbBindUser(cobox, orgId, true);
        }
        /// <summary> 绑定用户
        /// </summary>
        /// <param name="cobox"></param>
        /// <param name="orgId"></param>
        /// <param name="isAll"></param>
        public static void CmbBindUser(ComboBox cobox, string orgId, bool isAll)
        {
            if (LocalCache.DtUser == null)
            {
                LocalCache.Update(CacheList.User);
            }

            List<ListItem> list = new List<ListItem>();
            if (isAll)
            {
                list.Add(new ListItem(string.Empty, "全部"));
            }

            if (orgId != null && orgId.Length > 0)
            {
                if (LocalCache.DtUser != null
                    && LocalCache.DtUser.Rows.Count > 0)
                {
                    DataRow[] drs = LocalCache.DtUser.Select("org_id = '" + orgId + "'");
                    foreach (DataRow dr in drs)
                    {
                        list.Add(new ListItem(dr["user_id"], dr["user_name"].ToString()));
                    }
                }
            }

            if (list.Count > 0)
            {
                cobox.DataSource = list;
                cobox.ValueMember = "Value";
                cobox.DisplayMember = "Text";
            }
        }


        /// <summary> 绑定省份
        /// </summary>
        /// <param name="cmb">绑定容器</param>
        /// <param name="_default">默认显示</param>
        public static void CmbBindProvice(ComboBox cmb, string _default)
        {
            if (LocalCache.DtArea == null)
            {
                LocalCache.Update(CacheList.Area);
            }

            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem(string.Empty, _default));

            if (LocalCache.DtArea != null
                && LocalCache.DtArea.Rows.Count > 0)
            {
                DataRow[] drs = LocalCache.DtArea.Select("AREA_LEVEL = '0'");
                foreach (DataRow dr in drs)
                {
                    list.Add(new ListItem(dr["AREA_CODE"], dr["AREA_NAME"].ToString()));
                }
            }


            if (list.Count > 0)
            {
                cmb.DataSource = list;
                cmb.ValueMember = "Value";
                cmb.DisplayMember = "Text";
            }
        }
        /// <summary> 绑定城市
        /// </summary>
        /// <param name="cmb">绑定容器</param>
        /// <param name="proviceId">省份编码</param>
        /// <param name="_default">默认显示</param>
        public static void CmbBindCity(ComboBox cmb, string proviceId, string _default)
        {
            if (LocalCache.DtArea == null)
            {
                LocalCache.Update(CacheList.Area);
            }

            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem(string.Empty, _default));

            if (!string.IsNullOrEmpty(proviceId))
            {
                if (LocalCache.DtArea != null
                    && LocalCache.DtArea.Rows.Count > 0)
                {
                    DataRow[] drs = LocalCache.DtArea.Select("PARENT_CODE = '" + proviceId + "'");
                    foreach (DataRow dr in drs)
                    {
                        list.Add(new ListItem(dr["AREA_CODE"], dr["AREA_NAME"].ToString()));
                    }
                }
            }

            if (list.Count > 0)
            {
                cmb.DataSource = list;
                cmb.ValueMember = "Value";
                cmb.DisplayMember = "Text";
            }
        }
        /// <summary> 绑定乡镇
        /// </summary>
        /// <param name="cmb">绑定容器</param>
        /// <param name="proviceId">城市编码</param>
        /// <param name="_default">默认显示</param>
        public static void CmbBindCountry(ComboBox cmb, string cityId, string _default)
        {
            if (LocalCache.DtArea == null)
            {
                LocalCache.Update(CacheList.Area);
            }

            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem(string.Empty, _default));

            if (!string.IsNullOrEmpty(cityId))
            {
                if (LocalCache.DtArea != null
                    && LocalCache.DtArea.Rows.Count > 0)
                {
                    DataRow[] drs = LocalCache.DtArea.Select("PARENT_CODE = '" + cityId + "'");
                    foreach (DataRow dr in drs)
                    {
                        list.Add(new ListItem(dr["AREA_CODE"], dr["AREA_NAME"].ToString()));
                    }
                }
            }

            if (list.Count > 0)
            {
                cmb.DataSource = list;
                cmb.ValueMember = "Value";
                cmb.DisplayMember = "Text";
            }
        }
        #endregion

        #region --数据导入导出
        //导出Excel文件
        public static void OutPutExcel(DataGridView dgv, Stream stream, string path)
        {
            int rowLen = dgv.Rows.Count;
            int colLen = dgv.ColumnCount;
            if (rowLen > 0)
            {
                StreamWriter sw = new StreamWriter(stream, System.Text.Encoding.GetEncoding("UTF-8"));
                string tempStr = "";
                try
                {
                    List<int> cols = new List<int>();
                    for (int i = 0; i < colLen; i++)
                    {
                        Type type = dgv.Columns[i].GetType();
                        if (dgv.Columns[i].Visible
                            && (type == typeof(DataGridViewTextBoxColumn)
                            || type == typeof(DataGridViewComboBoxColumn)))
                        {
                            if (tempStr.Length > 0)
                            {
                                tempStr += "\t";
                            }
                            tempStr += dgv.Columns[i].HeaderText;
                            cols.Add(i);
                        }
                    }
                    sw.WriteLine(tempStr);

                    tempStr = string.Empty;

                    for (int j = 0; j < rowLen; j++)
                    {
                        tempStr = string.Empty;
                        for (int k = 0; k < colLen; k++)
                        {
                            if (cols.Contains(k))
                            {
                                if (tempStr.Length > 0)
                                {
                                    tempStr += "\t";
                                }
                                if (dgv.Rows[j].Cells[k].Value != null)
                                {
                                    tempStr += dgv.Rows[j].Cells[k].FormattedValue.ToString();
                                }
                            }
                        }
                        sw.WriteLine(tempStr);
                    }
                    sw.Close();
                    stream.Close();
                }
                catch (Exception ex)
                {
                    if (sw != null)
                    {
                        sw.Close();
                    }
                    if (stream != null)
                    {
                        stream.Close();
                    }
                }
            }
        }
        #endregion

        #region --打印预览

        #endregion
    }
}
