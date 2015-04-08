using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data;
using System.Reflection;
using SYSModel;
using BLL;
using HXC_FuncUtility;

namespace HXCServerWinForm
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
                    HXCServerWinForm.CommonCtrl.CheckAllChildNodes(node, nodeChecked);
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
        //public static bool CheckVideoValidate(PermissionCtrl.VideoCtrl.VideoType videoType) 
        //{

        //    bool result = false;
        //    switch (videoType)
        //    {
        //        case PermissionCtrl.VideoCtrl.VideoType.HaiKang:
        //            result= PermissionCtrl.VideoCtrl.HKVideo.Validated();
        //            break;
        //        case PermissionCtrl.VideoCtrl.VideoType.RuiMing:
        //            result = PermissionCtrl.VideoCtrl.RMVideo.Validated();
        //            break;
        //        case PermissionCtrl.VideoCtrl.VideoType.YiMeng:
        //            result = PermissionCtrl.VideoCtrl.YMVideo.Validated();
        //            break;
        //        case PermissionCtrl.VideoCtrl.VideoType.DaHua:
        //            result = PermissionCtrl.VideoCtrl.DHVideo.Validated();
        //            break;
        //        case PermissionCtrl.VideoCtrl.VideoType.YouWei:
        //            result = PermissionCtrl.VideoCtrl.YWVideo.Validated();
        //            break;
        //        //case PermissionCtrl.VideoCtrl.VideoType.BoShiJie:
        //        //    result = PermissionCtrl.VideoCtrl.BSJVideo.Validated();
        //        //    break;
        //    }

        //    return result;
        //}



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
            if (HXCServerWinForm.GlobalStaticObj.gLoginDataSet != null && HXCServerWinForm.GlobalStaticObj.gLoginDataSet.Tables[2].Rows.Count > 0)
            {
                DataView dv = new DataView();
                dv = HXCServerWinForm.GlobalStaticObj.gLoginDataSet.Tables[2].DefaultView;
                dv.RowFilter = "parent_id='" + Pid + "'";
                return dv.ToTable();
            }
            else
            {
                return null;
            }
            /* list 筛选
              List<sys_function> result = functionList.FindAll(delegate(sys_function bk)
            {
                return bk.parent_id == FunID;
            });
             */
        }

        /// <summary>
        /// 窗体名 放射出窗体
        /// </summary>
        /// <param name="strUserControlName">命名空间.窗体名</param>
        /// <returns>用户控件-功能窗体</returns>
        public static UserControl TraverseForm(string strUserControlName)
        {
            UserControl uc = null;
            if (strUserControlName == null || strUserControlName == "")
                return uc;
            //"TestWebSErv.Form2";
            Assembly assembly = Assembly.GetExecutingAssembly();
            object obj = assembly.CreateInstance(strUserControlName);
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
        public static void BindComboBoxByDictionarr(ComboBox cbo, string code,bool isAll)
        {
            DataTable dt = GetDictionarrByCode(code,isAll);
            cbo.DataSource = dt;
            cbo.ValueMember = "dic_id";
            cbo.DisplayMember = "dic_name";
        }

        public static void BindComboBoxByDictionarr(DataGridViewComboBoxColumn dgvcbc, string code)
        {
            DataTable dt = GetDictionarrByCode(code, false);
            dgvcbc.DataSource = dt;
            dgvcbc.ValueMember = "dic_id";
            dgvcbc.DisplayMember = "dic_name";
        }

        /// <summary>
        /// 根据父级编码,获取所有子级字典
        /// </summary>
        /// <param name="code"></param>
        /// <param name="isAll">是否有全部</param>
        /// <returns></returns>
        public static DataTable GetDictionarrByCode(string code,bool isAll)
        {
            SQLObj sqlObj = new SQLObj();
            sqlObj.cmdType = CommandType.Text;
            sqlObj.sqlString = @"select b.dic_id,b.dic_name from sys_dictionaries a 
inner join sys_dictionaries b on a.dic_id=b.parent_id
where a.dic_code=@dic_code";
            sqlObj.Param = new Dictionary<string, ParamObj>();
            sqlObj.Param.Add("dic_code", new ParamObj("dic_code", code, SysDbType.VarChar, 40));
            DataSet ds = DBHelper.GetDataSet("", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, sqlObj);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count==0)
            {
                return null;
            }
            DataTable dt = ds.Tables[0];
            if (isAll)
            {
                DataRow dr = dt.NewRow();
                dr["dic_id"] = string.Empty;
                dr["dic_name"] = "全部";
                dt.Rows.InsertAt(dr, 0);
            }
            return dt;
        }
        
        /// <summary>
        /// 字典码表 编码和名称获取id
        /// </summary>
        /// <param name="dic_code">字典编码</param>
        /// <param name="dic_name">字典名称</param>
        /// <returns></returns>
        public static string GetDictionarrIDByCode(string dic_code,string dic_name)
        {
           return DBHelper.GetSingleValue("",GlobalStaticObj_Server.DbPrefix+GlobalStaticObj_Server.CommAccCode, "sys_dictionaries", "dic_id", "dic_code='" + dic_code + "' and dic_name='" + dic_name + "'", "");
        }
        
    }
}
