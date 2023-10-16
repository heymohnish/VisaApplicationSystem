using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisaApplicationSystem.Models;
using VisaApplicationSystem.Repository;

namespace VisaApplicationSystem.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            try
            {
                return View();

            }
            catch (Exception ex)
            {
                LogError(ex);
                return View("Error");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            try
            {
                ViewBag.Message = "Your application description page.";

                return View();
            }
            catch (Exception ex)
            {
                LogError(ex);
                return View("Error");
            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Contact()
        {
            try
            {
                ViewBag.Message = "Your contact page.";

                return View();

            }
            catch (Exception ex)
            {
                LogError(ex);
                return View("Error");
            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Contact(Contact contact)
        {
            try
            {
                AdminRepository adminRepository = new AdminRepository();
                adminRepository.CreateContactMessage(contact);

                return View();

            }
            catch (Exception ex)
            {
                LogError(ex);
                return View("Error");
            }
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ForgetPassword()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                LogError(ex);
                return View("Error");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="forgotPassword"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ForgetPassword(ForgotPassword forgotPassword)
        {
            try
            {
               LoginRepository loginRepository = new LoginRepository();
               loginRepository.ForgetPassword(forgotPassword);
                return RedirectToAction("Login","Login");
            }
            catch (Exception ex)
            {
                LogError(ex);
                return View("Error");
            }

        }
        private void LogError(Exception ex)
        {
            string logPath = Server.MapPath("~/Content/Log/error.log"); // Adjust the path to your log directory

            using (StreamWriter writer = new StreamWriter(logPath, true))
            {
                writer.WriteLine($"[{DateTime.Now}] Error: {ex.Message}");
                writer.WriteLine($"Stack Trace: {ex.StackTrace}");
                writer.WriteLine();
            }
        }
    }
}