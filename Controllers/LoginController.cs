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
                ClearAll();
                return View();
            }
            catch (Exception ex)
            {
                LogError(ex);
                return View();
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
                if (HttpContext.Session["Role"] != null && HttpContext.Session["Role"].Equals("Admin") && HttpContext.Session["LoginId"] != null && int.TryParse(HttpContext.Session["LoginId"].ToString(), out userId))
                {
                    Console.WriteLine("User ID: " + userId);
                }
                if (registration != null)
                {
                    if (registration.isVerified)
                    {
                        if (registration.role.Equals("Admin") && registration.roleBase.Equals(RoleBase.Admin))
                        {
                            FormsAuthentication.SetAuthCookie(login.userName, false);
                            return RedirectToAction("IndexHome", "Admin");
                        }
                        else if (registration.roleBase.Equals(RoleBase.VCO))
                        {
                            FormsAuthentication.SetAuthCookie(login.userName, false);

                            return RedirectToAction("Index", "VCO");
                        }
                        else if (registration.roleBase.Equals(RoleBase.User))
                        {
                            FormsAuthentication.SetAuthCookie(login.userName, false);
                            return RedirectToAction("UserIndex", "User");
                        }
                        else
                        {
                            // Registration is not verified, so set an error message in ViewBag.
                            ViewBag.ErrorMessage = "Your registration is not verified. Please verify your registration.";
                        }
                    }
                    else
                    {
                        // Handle the case where no registration is found (e.g., invalid login credentials).
                        ViewBag.ErrorMessage = "Invalid login credentials. Please try again.";
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
                registration.roleBase = RoleBase.User;
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
        //clear all cache file auth cookies
        public void ClearAll()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
            FormsAuthentication.SignOut();
            HttpContext.Session.Clear();
            HttpContext.Session.Abandon();
            HttpContext.Session.RemoveAll();
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