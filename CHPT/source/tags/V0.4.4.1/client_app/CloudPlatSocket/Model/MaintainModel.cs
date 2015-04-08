using System.Data;

namespace CloudPlatSocket.Model
{
    /// <summary>
    /// 维修管理 附件
    /// </summary>
    public class MaintainModel : FileModel
    {
        public static string _table = "tb_maintain_accessory";

        #region --构造函数
        public MaintainModel(DataRow dr)
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
