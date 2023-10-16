using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisaApplicationSystem.Repository;

namespace VisaApplicationSystem.Controllers
{
    public class FormController : Controller
    {
        // GET: Validation
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CheckUserNameExists(string userName)
        {
            FormRepository validationRepository = new FormRepository();
            bool k = validationRepository.GetUser(userName);
            return Json(k);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CheckEmailExists(string userName)
        {
            FormRepository validationRepository = new FormRepository();
            bool k = validationRepository.GetEmail(userName);
            return Json(k);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetStates(string country)
        {
            FormRepository validationRepository = new FormRepository();
            var states = validationRepository.GetStates(country);
            return Json(states, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetCountry()
        {
            FormRepository validationRepository = new FormRepository();
            var country = validationRepository.GetCountry();
            return Json(country, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCities(string state)
        {
            FormRepository validationRepository = new FormRepository();
            var city = validationRepository.GetCity(state);
            return Json(city, JsonRequestBehavior.AllowGet);
        }
        
    }
}