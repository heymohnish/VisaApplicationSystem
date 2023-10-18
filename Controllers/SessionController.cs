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
        /// <summary>
        /// Once the session expire it will redirect to this page
        /// </summary>
        /// <returns> The view page return session time out page</returns>
        [HttpGet]
        public ActionResult SessionTimeOut()
        {
            return View();
        }
    }

}