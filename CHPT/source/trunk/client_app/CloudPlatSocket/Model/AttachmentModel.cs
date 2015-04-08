using System.Data;

namespace CloudPlatSocket.Model
{
    /// <summary>
    /// 附件对象
    /// </summary>
    public class AttachmentModel : FileModel
    {
        public static string _table = "attachment_info";

        #region --构造函数
        public AttachmentModel(DataRow dr)
        {
            if (dr.Table.Columns.Contains("att_id"))
            {
                this.Id = dr["att_id"].ToString();
            }

            if (dr.Table.Columns.Contains("att_path"))
            {
                this.Path = dr["att_path"].ToString();
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
