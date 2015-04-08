using System.Text;
using System.IO;

namespace CloudPlatSocket
{
    public class FileHelper
    {
        /// <summary> 获取文件流 
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static FileStream GetFileStream(string filePath)
        {
            if (File.Exists(filePath))
            {
                FileInfo file = new FileInfo(filePath);
                FileStream fs = file.OpenRead();
                return fs;
            }
            return null;
        }
        /// <summary> 获取文件流 
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static byte[] GetFileInByte(string filePath)
        {
            if (File.Exists(filePath))
            {
                FileInfo file = new FileInfo(filePath);
                FileStream fs = file.OpenRead();
                byte[] data = new byte[fs.Length];
                fs.Read(data, 0, data.Length);
                return data;
            }
            return null;
        }
        /// <summary> 获取文件流 
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static string GetFileString(string filePath)
        {
            if (File.Exists(filePath))
            {
                FileInfo file = new FileInfo(filePath);
                FileStream fs = file.OpenRead();
                byte[] data = new byte[fs.Length];
                fs.Read(data, 0, data.Length);
                return Encoding.UTF8.GetString(data);
            }
            return string.Empty;
        }
    }
}
