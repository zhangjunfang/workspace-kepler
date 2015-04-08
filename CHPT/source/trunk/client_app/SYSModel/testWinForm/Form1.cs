using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Skin;
using DBUtility;
using SYSModel;
using System.Data.SqlClient;
namespace testWinForm
{
    public partial class Form1 : Form
    {
        SkinOperation skin = new SkinOperation();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetSkin();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            skin.WriteSkin("default");//设置皮肤
            SetSkin();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            skin.WriteSkin("skin1");//设置当前皮肤
            SetSkin();
        }

        /// <summary>
        /// 重新设置当前皮肤的资源
        /// </summary>
        void SetSkin()
        {
            foreach (Control ctr in this.Controls)
            {
                if (ctr is Button)
                {
                    Button btn = (Button)ctr;
                    btn.BackgroundImage = skin.ReadImage("btn", "backgroundImage");//获取图片
                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            BackColor = skin.ReadColor("main", "backgroundColor", Color.Yellow);//获取颜色，如果找不到，则以后面的默认
        }

        private void btnOPBackup_Click(object sender, EventArgs e)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionManageStringWrite"].ToString();
            #region 用例1
//            SqlParameter[] testParamArray = new SqlParameter[8]
//                {
//                new SqlParameter("@role_id0","1"),
//                new SqlParameter("@role_name1","testDDDtest"),
//                new SqlParameter("@state1","state111"),                
//                new SqlParameter("@role_id1","1"),
//                new SqlParameter("@role_name2","test888test999"),
//                new SqlParameter("@state2","state222"),
//                new SqlParameter("@role_id2","2"),
//                new SqlParameter("@role_code0","2"),
//                };
//            string testSQLStr = @"SELECT [role_id],[role_code],[role_name],[remark],[data_sources],[state],[enable_flag]
//                                  ,[create_by] ,[create_time],[update_by],[update_time]  FROM [sys_role] where [role_id] = @role_id0;
//                                  UPDATE [sys_role] SET [role_name]=@role_name1,remark='三个testD',[state]=@state1 where [role_id] = @role_id1  and [role_code] = '1';
//                                  UPDATE [sys_role] SET remark='三个test888',[role_name]=@role_name2,[state]=@state2 where [role_id] = @role_id2  and [role_code] = @role_code0;
//                                  SELECT [role_id],[role_code],[role_name],[remark],[data_sources],[state],[enable_flag]
//                                              ,[create_by] ,[create_time],[update_by],[update_time]  FROM [sys_role] where [role_id] = @role_id0;";
            #endregion
            #region 用例2
//            SqlParameter[] testParamArray = new SqlParameter[4]
//                {
//                new SqlParameter("@role_id0","1"),
//                new SqlParameter("@role_name1","111testPPP"),
//                new SqlParameter("@role_id1","1"),  
//                new SqlParameter("@role_id2","2")
//                };

//            string testSQLStr = @"SELECT [role_id],[role_code],[role_name],[remark],[data_sources],[state],[enable_flag]
//                                  ,[create_by] ,[create_time],[update_by],[update_time]  FROM [sys_role] where [role_id] = @role_id0;
//                                  UPDATE [sys_role]   SET [role_name] = @role_name1   where [role_id] = @role_id1  or [role_id] = @role_id2;                                  
//                                  SELECT [role_id],[role_code],[role_name],[remark],[data_sources],[state],[enable_flag]
//                                  ,[create_by] ,[create_time],[update_by],[update_time]  FROM [sys_role] where [role_id] = @role_id0;";
            #endregion
            #region 用例3
            //SqlParameter[] testParamArray = new SqlParameter[3]
            //    {
            //    new SqlParameter("@role_name0","testAAA"),
            //    new SqlParameter("@role_id0","1"),
            //    new SqlParameter("@role_code0","1")
            //    };

            //string testSQLStr = @"UPDATE [sys_role]   SET [role_name] = @role_name0   where [role_id] = @role_id0  and [role_code] = @role_code0";
            #endregion
            #region 用例4
            //SqlParameter[] testParamArray = new SqlParameter[3]
            //    {
            //    new SqlParameter("@role_name0","testBBB"),
            //    new SqlParameter("@role_id0","1"),
            //    new SqlParameter("@role_code0","1")
            //    };

            //string testSQLStr = @"UPDATE [sys_role]   SET [role_name] = @role_name0   where [role_id] = @role_id0  and [role_code] = @role_code0;";
            #endregion
            #region 用例5
            //SqlParameter[] testParamArray = new SqlParameter[6]
            //    {
            //    new SqlParameter("@role_name0","testAAA"),
            //    new SqlParameter("@role_id0","1"),
            //    new SqlParameter("@role_code0","1"),
            //    new SqlParameter("@role_name1","testBBB"),
            //    new SqlParameter("@role_id1","2"),
            //    new SqlParameter("@role_code1","2")
            //    };

            //string testSQLStr = @"UPDATE [sys_role]   SET [role_name] = @role_name0   where [role_id] = @role_id0  and [role_code] = @role_code0;UPDATE [sys_role]   SET [role_name] = @role_name1   where [role_id] = @role_id1  and [role_code] = @role_code1;";

            string testSQLStr = "  update  tb_CustomerSer_member_setInfo_projrct  set  service_project_discount='1' , remark='' , update_by='1' , update_time='635493914800000000'  where  setInfo_projrct_id='0173da7d-1bd5-46bc-9eec-d9014eb56c28'  ;   update  tb_CustomerSer_member_setInfo_projrct  set  service_project_discount='6' , remark='' , update_by='1' , update_time='635493914800000000'  where  setInfo_projrct_id='d98e7bd4-e196-4c5d-b56e-a976c75168e7' ;  update  tb_CustomerSer_member_setInfo_parts  set  parts_discount='1' , remark='' , update_by='1' , update_time='635493914800000000' where setInfo_parts_id='b186ff8b-0937-4052-9f7f-724a8fccf5dc';";
            SqlHelper.ExecuteNonQuery(connStr, CommandType.Text, testSQLStr, new UserIDOP() { OPName = "测试操作", UserID = "-999" }, null);
            #endregion


            //SqlHelper.ExecuteNonQuery(connStr, CommandType.Text,testSQLStr, new UserIDOP() { OPName = "测试操作", UserID = "-999" }, testParamArray);
        }

        private void btnTestYTService_Click(object sender, EventArgs e)
        {
            //yuTongWebService.WebServ_YT_BasicData1.LoadBusModel();
            //DateTime dt = DateTime.Now.AddDays(-1);
            //yuTongWebService.WebServ_YT_BasicData1.LoadPart(dt.ToString("yyyy-MM-dd HH:mm:ss"));
            yuTongWebService.WebServ_YT_BasicData.LoadPart("2014-03-01");
            //yuTongWebService.WebServ_YT_BasicData1.LoadProdImprovement(dt.ToString("yyyy-MM-dd HH:mm:ss"));
            //yuTongWebService.WebServ_YT_BasicData1.UpLoadPartSales();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmReport report = new frmReport();
            report.Show();
        }
        
    }
}
