using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.IO;

namespace HXCCommon.DotNetFile
{
    /// <summary>
    /// 文件上传帮助类
    /// </summary>
    public class UploadHelper
    {
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="path">保存路径</param>
        /// <param name="filleupload">上传文件控件</param>
        /// <returns></returns>
        public static string FileUpload(string path, FileUpload filleupload)
        {
            try
            {
                bool fileOk = false;
                //取得文件的扩展名,并转换成小写
                string fileExtension = System.IO.Path.GetExtension(filleupload.FileName).ToLower();
                //文件格式
                string[] allowExtension = { ".rar", ".zip", ".rar", ".ios", ".jpg", ".png", "bmp", ".gif", ".txt" };
                if (filleupload.HasFile)
                {
                    //对上传的文件的类型进行一个个匹对
                    for (int i = 0; i < allowExtension.Length; i++)
                    {
                        if (fileExtension == allowExtension[i])
                        {
                            fileOk = true;
                            break;
                        }
                    }
                }
                //如果符合条件，则上传
                if (fileOk)
                {
                    if (Directory.Exists(path) == false)//如果不存在就创建file文件夹
                    {
                        Directory.CreateDirectory(path);
                    }
                    if (!FileHelper.IsExistFile(path + filleupload.FileName))
                    {
                        int Size = filleupload.PostedFile.ContentLength / 1024 / 1024;
                        if (Size > 10)
                        {
                            return "上传失败,文件过大";
                        }
                        else
                        {
                            filleupload.PostedFile.SaveAs(path + filleupload.FileName);
                            return "上传成功";
                        }
                    }
                    else
                    {
                        return "上传失败,文件已存在";
                    }

                }
                else
                {
                    return "不支持【" + fileExtension + "】文件格式";
                }
            }
            catch (Exception)
            {
                return "上传失败";
            }
        }
    }
}
