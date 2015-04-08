using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CloudPlatSocket.Model
{
    /// <summary>
    /// 附件对象
    /// </summary>
    public class FileModel
    {
        #region --构造函数
        public FileModel()
        {

        }
        public static FileModel CreateModel(DataRow dr)
        {
            if (dr.Table.TableName == AttachmentModel._table)
            {
                return new AttachmentModel(dr);
            }
            else if (dr.Table.TableName == ThreeMaintainModel._table)
            {
                return new ThreeMaintainModel(dr);
            }
            else if (dr.Table.TableName == MaintainModel._table)
            {
                return new MaintainModel(dr);
            }
            return new FileModel();
        }
        #endregion

        /// <summary>
        /// 附件ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 附件路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string FileType { get; set; }
    }
}
