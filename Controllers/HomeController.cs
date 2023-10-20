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
        /// Display home page
        /// </summary>
        /// <returns>The view page return home page</returns>
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
        /// Display About us page
        /// </summary>
        /// <returns>The view page return about page</returns>
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
        /// Display Contact us page
        /// </summary>
        /// <returns>The view page return contact us page</returns>
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
        /// Display Contact us page
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>The view page return contact us page</returns>
        [HttpPost]
        public ActionResult Contact(Contact contact)
        {
            try
            {
                AdminRepository adminRepository = new AdminRepository();
                adminRepository.CreateContactMessage(contact);
                ViewBag.SuccessMessage = "Your message has been successfully sent.";
                return View();

            }
            catch (Exception ex)
            {
                LogError(ex);
                return View("Error");
            }
           
        }
        /// <summary>
        /// Display Forgot password page
        /// </summary>
        /// <returns>The view page return forgot password page</returns>
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
        /// Display Forgot password page
        /// </summary>
        /// <param name="forgotPassword"></param>
        /// <returns>The view page redirect to the login page</returns>
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
        //error log file write
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