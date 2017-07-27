using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;

namespace validateCode.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        //验证码 demo
		public  FileResult  ShowValidateCode()
		{
            ValidateCode vc = new ValidateCode();
            string code = vc.CreateValidateCode(4);

            Session["validateCode"] = code;

            byte[] bytes=vc.CreateValidateGraphic(code);
            return File(bytes, "image/jpeg");
          
		}

				
       

    }
}
