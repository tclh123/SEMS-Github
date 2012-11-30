using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;

namespace SEMS.BLL
{
    public class IO
    {
        /// <summary>
        /// 将文件保存到后台临时文件目录
        /// </summary>
        /// <param name="file">保存路径</param>
        /// <returns></returns>
        static public string SaveToAdminTemp(HttpPostedFileBase file)
        {
            var ran = new Random();
            var dir = ConfigurationManager.AppSettings["AdminTempDirectory"].ToString();
            var physicalPath = HttpContext.Current.Request.PhysicalApplicationPath + dir;
            if (!Directory.Exists(physicalPath))
            {
                Directory.CreateDirectory(physicalPath);
            }

            //服务器上的UpLoadFile文件夹必须有读写权限
            var path = physicalPath
                + DateTime.Now.ToString("yyyyMMddHHmmss")
                + ran.Next(99999).ToString()
                + Path.GetExtension(file.FileName);

            file.SaveAs(path);

            return path;
        }

        /// <summary>
        /// 清空后台临时文件目录
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        static public bool ClearAdminTemp()
        {
            try
            {
                var dir = ConfigurationManager.AppSettings["AdminTempDirectory"].ToString();
                var physicalPath = HttpContext.Current.Request.PhysicalApplicationPath + dir;
                if (!Directory.Exists(physicalPath))
                {
                    return false;
                }
                DeleteFolder(physicalPath);
                if (!Directory.Exists(physicalPath))
                {
                    Directory.CreateDirectory(physicalPath);
                }
                return true;
            }
            catch(Exception e)
            {
                throw e;
                //return false;
            }
        }

        /// <summary>  
        /// 用递归方法删除文件夹目录及文件  
        /// </summary>  
        /// <param name="dir">带文件夹名的路径</param>   
        static public bool DeleteFolder(string dir)
        {
            try
            {
                if (Directory.Exists(dir)) //如果存在这个文件夹删除之   
                {
                    foreach (string d in Directory.GetFileSystemEntries(dir))
                    {
                        if (File.Exists(d))
                            File.Delete(d); //直接删除其中的文件                          
                        else
                            DeleteFolder(d); //递归删除子文件夹   
                    }
                    Directory.Delete(dir, true); //删除已空文件夹                   
                }
                return true;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="path">在服务器上的相对路径</param>
        /// <returns>物理路径</returns>
        static public string Download(string path)
        {
            var dir = ConfigurationManager.AppSettings["AdminDownloadDirectory"].ToString();
            return HttpContext.Current.Request.PhysicalApplicationPath + dir + path;
        }
    }
}