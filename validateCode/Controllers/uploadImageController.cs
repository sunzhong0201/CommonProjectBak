using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace validateCode.Controllers
{
    public class uploadImageController : Controller
    {


       
	    public  ActionResult  Index()
	    {
		    return View();
	    }

     

				
        //
        // GET: /uploadImage/

        public JsonResult upload()
        {


            //Response.ContentType = "image/jpeg";

            HttpPostedFileBase file = Request.Files["imgFile"];
            if (file == null)
            {
               Response.End();
            }

            string originImgPath = "/Upload/" + Guid.NewGuid().ToString() + file.FileName;
            string originPath = Request.MapPath(originImgPath);
            file.SaveAs(originPath);


            string smallImgPath = "/Upload/Small-" + Guid.NewGuid().ToString() + file.FileName;
            string smallPath = Request.MapPath(smallImgPath);


            #region 我们自己写的生成缩略图的代码
            ////把上传的文件做成了一个Image对象。
            //Image image = Image.FromStream(file.InputStream);

            //Bitmap smallImg = new Bitmap(100, 100);
            ////在缩略图上创建画布
            //Graphics g = Graphics.FromImage(smallImg);

            //g.DrawImage(image, new Rectangle(0, 0, 100, 100), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

            //string path = "/Upload/Small-" + Guid.NewGuid().ToString() + file.FileName;
            //smallImg.Save(Request.MapPath(path));

            //MemoryStream ms = new MemoryStream();
            //smallImg.Save(ms, ImageFormat.Jpeg);//把缩略图写到内存流里面去了

            //Response.BinaryWrite(ms.ToArray()); 
            #endregion

            Common.ImageHelper.MakeThumbnail(originPath,
                smallPath,
                100,
                100,
                "W");

            //Response.WriteFile(smallImgPath);
            var result = new { result = "ok", url = smallImgPath };
            return Json(result);
        }

    }
}
