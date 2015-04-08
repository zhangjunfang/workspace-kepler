using System.Data;

namespace CloudPlatSocket.Model
{
    /// <summary>
    /// 三包服务附件对象
    /// </summary>
    public class ThreeMaintainModel : FileModel
    {
        public static string _table = "tb_maintain_three_guaranty_accessory";

        #region --构造函数
        public ThreeMaintainModel(DataRow dr)
        {
            if (dr.Table.Columns.Contains("accessory_id"))
            {
                this.Id = dr["accessory_id"].ToString();
            }

            if (dr.Table.Columns.Contains("accessory_details"))
            {
                this.Path = dr["accessory_details"].ToString();
            }
            if (this.Path.Length > 0)
            {
                int start = this.Path.IndexOf('.') + 1;
                this.FileType = this.Path.Substring(start, this.Path.Length - start);
            }
        }
        #endregion        
    }
}
