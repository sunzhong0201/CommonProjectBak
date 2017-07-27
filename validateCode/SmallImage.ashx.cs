using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace WebDemo.Web.FileUpload
{
    /// <summary>
    /// SmallImage 的摘要说明
    /// </summary>
    public class SmallImage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "image/jpeg";

            HttpPostedFile file = context.Request.Files["imgFile"];
            if (file == null)
            {
                context.Response.End();
            }

            string originImgPath = "/Upload/" + Guid.NewGuid().ToString() + file.FileName;
            string originPath = context.Request.MapPath(originImgPath);
            file.SaveAs(originPath);


            string smallImgPath = "/Upload/Small-" + Guid.NewGuid().ToString() + file.FileName;
            string smallPath = context.Request.MapPath(smallImgPath);
            

            #region 我们自己写的生成缩略图的代码
            ////把上传的文件做成了一个Image对象。
            //Image image = Image.FromStream(file.InputStream);

            //Bitmap smallImg = new Bitmap(100, 100);
            ////在缩略图上创建画布
            //Graphics g = Graphics.FromImage(smallImg);

            //g.DrawImage(image, new Rectangle(0, 0, 100, 100), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

            //string path = "/Upload/Small-" + Guid.NewGuid().ToString() + file.FileName;
            //smallImg.Save(context.Request.MapPath(path));

            //MemoryStream ms = new MemoryStream();
            //smallImg.Save(ms, ImageFormat.Jpeg);//把缩略图写到内存流里面去了

            //context.Response.BinaryWrite(ms.ToArray()); 
            #endregion

            Common.ImageHelper.MakeThumbnail(originPath,
                smallPath,
                100,
                100,
                "W");

            context.Response.WriteFile(smallImgPath);


        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}