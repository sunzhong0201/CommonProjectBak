using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace validateCode.Controllers
{
    public class CutImageController : Controller
    {
        //
        // GET: /CutImage/

        public ActionResult Index()
        {
            return View();
        }

        
		public  ActionResult  cutImage()
		{

            int x = Convert.ToInt32(Request["x"]);
            int y = Convert.ToInt32(Request["y"]);
            int width = Convert.ToInt32(Request["width"]);
            int height = Convert.ToInt32(Request["height"]);
            string imagePath = Request["imagePath"];
            using (Bitmap map = new Bitmap(width, height))
            {
                using (Graphics g = Graphics.FromImage(map))
                {
                    using (Image img = Image.FromFile(Request.MapPath(imagePath)))
                    {
                        //将原图的指定范围画到画布上.
                        //1:表示对哪张图片进行操作
                        //2:画多么大.
                        //3:画原图的哪块区域
                        g.DrawImage(img, new Rectangle(0, 0, width, height), new Rectangle(x, y, width, height), GraphicsUnit.Pixel);
                        string fileNewName = Guid.NewGuid().ToString();
                        string fullDir = "/Upload/" + fileNewName + ".jpg";

                        try
                        {
                            map.Save(Request.MapPath(fullDir), System.Drawing.Imaging.ImageFormat.Jpeg);//保存图片.
                        }
                        catch (Exception)
                        {
                            
                            throw;
                        }
                    
                        //一定要将截取后的图片路径存储到数据库中。
                       // Response.Write(fullDir);
                        return Content(fullDir); 
                    }
                }
            }
			
		}


        public ActionResult Upload()
        {
            HttpFileCollectionBase collection =Request.Files;
            bool isSucess = false;
            if (collection.Count > 0)
            {
                HttpPostedFileBase file =Request.Files["Filedata"];
                if (file != null)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string fileExt = Path.GetExtension(fileName);
                    if (fileExt == ".jpg")
                    {
                        string dir = "/Upload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
                        if (!Directory.Exists(Request.MapPath(dir)))
                        {
                            Directory.CreateDirectory(Request.MapPath(dir));
                        }
                        string newfileName = Guid.NewGuid().ToString();
                        string fullDir = dir + newfileName + fileExt;
                        file.SaveAs(Request.MapPath(fullDir));
                        isSucess = true;
                        using (Image img = Image.FromFile(Request.MapPath(fullDir)))
                        {
                           //Response.Write("ok:" + fullDir + ":" + img.Width + ":" + img.Height);
                            var result = "ok:" + fullDir + ":" + img.Width + ":" + img.Height;
                            return Content(result);
                        }
                      
                        //file.SaveAs(Request.MapPath("/UploadImage/" + fileName));
                        //Response.Write("/UploadImage/" + fileName);
                    }
                }
            }
            //if (!isSucess)
            //{
            //  // Response.Write("no:上传失败!!");
            //    return View("no:上传失败!!");
            //}
            return Content("no:上传失败!!");
        }

				

    }
}
