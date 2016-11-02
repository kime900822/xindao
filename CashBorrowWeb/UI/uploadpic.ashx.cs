using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace CashBorrowWeb.UI
{
    /// <summary>
    /// uploadpic 的摘要说明
    /// </summary>
    public class uploadpic : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpPostedFile file = context.Request.Files[0];
            if (file != null)
            {
                result r = new result();
                try
                {
                    //图片保存的文件夹路径
                    string path = context.Server.MapPath("../uploads/titleimages");
                    //每天上传的图片一个文件夹
                    string folder = DateTime.Now.ToString("yyyyMMdd");
                    //如果文件夹不存在，则创建
                    if (!Directory.Exists(path +"/"+ folder))
                    {
                        Directory.CreateDirectory(path + "/" + folder);
                    }
                    //上传图片的扩展名
                    string type = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                    //保存图片的文件名
                    string saveName = Guid.NewGuid().ToString() + type;
                    //保存图片
                    file.SaveAs(path + "/" + folder + "/" + saveName);
                    r.statusCode = 200;
                    r.message = "上传成功！";
                    r.filename =  folder + "/" + saveName;
                    context.Response.Write(new JavaScriptSerializer().Serialize(r));
                }
                catch(Exception e1)
                {
                    r.statusCode = 300;
                    r.message = e1.Message;

                    context.Response.Write(new JavaScriptSerializer().Serialize(r));
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }

    public class result {
        public int statusCode { set; get; }

        public string message { set; get; }


        public string filename { set; get; }
    }


}