using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace validateCode.Controllers
{
    public class PageController : Controller
    {
        //
        // GET: /Page/  分页例子
        public string NavString { get; set; }
        public ActionResult Index()
        {
            int pageIndex = int.Parse(Request["pageIndex"] ?? "1");
            int pageSize = 5;
            int total = 0;


            //NewsList = mainService.LoadPageData(pageIndex, pageSize, out total);
            total = 62;

            NavString = Common.LaomaPager.ShowPageNavigate(pageSize, pageIndex, total);
            ViewData["NavString"] = NavString;
            return View();


        }



    }
}
