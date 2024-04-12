using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webapp.Controllers
{
    public class HomeController : BaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        
    }
}