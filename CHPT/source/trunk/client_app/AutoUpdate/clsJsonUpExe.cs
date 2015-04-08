using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoUpdate
{
    public class clsJsonUpExe
    {
        /// <summary>
        /// success成功 fail失败 error 系统异常
        /// </summary>
        public string flag { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string version { get; set; }

        /// <summary>
        /// 程序包地址
        /// </summary>
        public string address { get; set; }       

        /// <summary>
        /// 升级文件大小
        /// </summary>
        public long size { get; set; }
    }
}
