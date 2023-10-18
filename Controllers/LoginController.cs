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
        /// Display an login page
        /// </summary>
        /// <returns>The view page return login page</returns>
        public ActionResult Login()
        {
            try
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
                Response.Cache.SetNoStore();
                return View();

            }
            catch (Exception ex)
            {
                LogError(ex);
                return View("Error");
            }
        }
        /// <summary>
        /// Display Login page
        /// </summary>
        /// <param name="login">xarrys user name and password</param>
        /// <returns>The view page return index page according to the role</returns>
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
        /// Sisplay Signup page
        /// </summary>
        /// <returns>The view page return sign up page</returns>
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
        /// Display sigup page
        /// </summary>
        /// <param name="registration">carrys registration model</param>
        /// <returns>The view page return to the login page after succefull registration as an user</returns>
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
        //error log file
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