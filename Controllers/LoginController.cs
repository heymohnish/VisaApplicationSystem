using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using VisaApplicationSystem.Models;
using VisaApplicationSystem.Repository;

namespace VisaApplicationSystem.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
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
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(Login login)
        {
            try
            {
                LoginRepository loginRepository = new LoginRepository();
                Registration registration = loginRepository.GetLogin(login);
                int userId;
                if (HttpContext.Session["LoginId"] != null && int.TryParse(HttpContext.Session["LoginId"].ToString(), out userId))
                {
                    Console.WriteLine("User ID: " + userId);
                }
                if (registration != null)
                {
                    if (registration.isVerified)
                    {
                        if (registration.role.Equals("Admin"))
                        {
                            ViewBag.registrationID = registration.registrationID;
                            FormsAuthentication.SetAuthCookie(login.userName, false);
                            return RedirectToAction("IndexHome", "Admin");
                        }
                        else if (registration.role.Equals("VCO"))
                        {
                            ViewBag.registrationID = registration.registrationID;
                            FormsAuthentication.SetAuthCookie(login.userName, false);
                            return RedirectToAction("Index", "VCO");
                        }
                        else if (registration.role.Equals("User"))
                        {
                            ViewBag.registrationID = registration.registrationID;
                            FormsAuthentication.SetAuthCookie(login.userName, false);
                            return RedirectToAction("UserIndex", "User");
                        }
                    }
                }
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
        public ActionResult Signup()
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
        /// <param name="registration"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Signup(Registration registration)
        {
            try
            {
                Console.WriteLine(registration.address);
                AdminRepository adminRepository = new AdminRepository();
                registration.role = "User";
                registration.salt = adminRepository.GenerateRandomSalt();
                registration.passwordHash = adminRepository.HashPassword(registration.password, registration.salt);
                adminRepository.InsertRegistration(registration);
                return RedirectToAction("Login", "Login");

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