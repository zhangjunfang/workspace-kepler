using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStationClient.ComponentUI.TextBox;
using System.Data;

namespace HXCPcClient.CommonClass
{
    /// <summary>
    /// 速查
    /// </summary>
    public class Quick
    {
        #region 客户速查
        /// <summary>
        /// 绑定客户速查
        /// </summary>
        /// <param name="txtCustCode"></param>
        public void BindCustomer(TextChooser txtCustCode)
        {
            //设置客户编码速查
            txtCustCode.SetBindTable("tb_customer", "cust_code", "cust_name");
            txtCustCode.GetDataSourced += new TextChooser.GetDataSourceHandler(txtcCustName_GetDataSourced);
            //txtCustCode.DataBacked += new TextChooser.DataBackHandler(txtcCustName_DataBacked);
        }

        void txtcCustName_GetDataSourced(TextChooser tc, string sqlString)
        {
            try
            {
                string fileds = "cust_name,cust_code";
                sqlString = sqlString.Replace("*", fileds);
                DataTable dvt = CommonFuncCall.GetDataSource(sqlString);
                tc.SetDataSource(dvt);
                if (dvt != null)
                {
                    tc.Search();
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 供应商速查
        /// <summary>
        /// 绑定供应商速查
        /// </summary>
        /// <param name="txtcSupplier"></param>
        public void BindSupplier(TextChooser txtcSupplier)
        {
            //设置供应商编码速查
            txtcSupplier.SetBindTable("tb_supplier", "sup_code", "sup_full_name");
            txtcSupplier.GetDataSourced += new TextChooser.GetDataSourceHandler(txtcSupplier_GetDataSourced);
        }

        void txtcSupplier_GetDataSourced(TextChooser tc, string sqlString)
        {
            try
            {
                string fileds = "sup_code,sup_full_name";
                sqlString = sqlString.Replace("*", fileds);
                DataTable dvt = CommonFuncCall.GetDataSource(sqlString);
                tc.SetDataSource(dvt);
                if (dvt != null)
                {
                    tc.Search();
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion

        #region 配件速查
        /// <summary>
        /// 绑定配件速查
        /// </summary>
        /// <param name="txtcParts"></param>
        public void BindParts(TextChooser txtcParts)
        {
            txtcParts.SetBindTable("tb_parts", "ser_parts_code", "parts_name");
            txtcParts.GetDataSourced += new TextChooser.GetDataSourceHandler(txtcParts_GetDataSourced);
        }

        void txtcParts_GetDataSourced(TextChooser tc, string sqlString)
        {
            try
            {
                string fileds = "parts_name,ser_parts_code";
                sqlString = sqlString.Replace("*", fileds);
                DataTable dvt = CommonFuncCall.GetDataSource(sqlString);
                tc.SetDataSource(dvt);
                if (dvt != null)
                {
                    tc.Search();
                }
            }
            catch (Exception ex)
            {
                HXCPcClient.GlobalStaticObj.GlobalLogService.WriteLog(ex);
            }
        }
        #endregion
    }
}
