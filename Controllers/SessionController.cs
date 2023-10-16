using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VisaApplicationSystem.Controllers
{
    public class SessionController : Controller
    {
        // GET: Session
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SessionTimeOut()
        {
            return View();
        }
        //
    }

}