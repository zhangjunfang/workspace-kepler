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
            SqlParameter[] testParamArray = new SqlParameter[6]
                {
                new SqlParameter("@role_id0","1"),
                new SqlParameter("@role_name1","111testXXX"),
                new SqlParameter("@role_id1","1"),
                new SqlParameter("@role_name2","222testDDD"),
                new SqlParameter("@role_id2","2"),
                new SqlParameter("@role_code0","2"),
                };
            string testSQLStr = @"SELECT [role_id],[role_code],[role_name],[remark],[data_sources],[state],[enable_flag]
                                  ,[create_by] ,[create_time],[update_by],[update_time]  FROM [sys_role] where [role_id] = @role_id0;
                                  UPDATE [sys_role]   SET [role_name] = @role_name1   where [role_id] = @role_id1  and [role_code] = '1';
                                  UPDATE [sys_role]   SET [role_name] = @role_name2   where [role_id] = @role_id2  and [role_code] = @role_code0;
                                  SELECT [role_id],[role_code],[role_name],[remark],[data_sources],[state],[enable_flag]
                                              ,[create_by] ,[create_time],[update_by],[update_time]  FROM [sys_role] where [role_id] = @role_id0;";
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
            #endregion


            SqlHelper.ExecuteNonQuery(connStr, CommandType.Text,testSQLStr, new UserIDOP() { OPName = "测试操作", UserID = "-999" }, testParamArray);
        }
        
    }
}
