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
    public class VCOController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Index()
        {
            try
            {
                
                int userId;
                if (HttpContext.Session["LoginId"] != null && int.TryParse(HttpContext.Session["LoginId"].ToString(), out userId))
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("SessionTimeOut", "Session");
                }
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
        [Authorize]
        [HttpGet]
        public ActionResult Applications()
        {
            try
            {
               
                int userId;
                if (HttpContext.Session["LoginId"] != null && int.TryParse(HttpContext.Session["LoginId"].ToString(), out userId))
                {
                    VCORepository controller = new VCORepository();

                    var down = controller.GetSubmitedApplication();
                    var user = Session["LoginId"];
                    if (user != null)
                    {
                        return View(down);
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("SessionTimeOut", "Session");
                }

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
        [Authorize]
        [HttpGet]
        public ActionResult Update()
        {
            try
            {
                
                int userId;
                if (HttpContext.Session["LoginId"] != null && int.TryParse(HttpContext.Session["LoginId"].ToString(), out userId))
                {
                    VCORepository controller = new VCORepository();

                    return View(controller.GetAllApplication());
                }
                else
                {
                    return RedirectToAction("SessionTimeOut", "Session");
                }

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
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public ActionResult UpdateApplication(int id)
        {
            try
            {
                int userId;
                if (HttpContext.Session["LoginId"] != null && int.TryParse(HttpContext.Session["LoginId"].ToString(), out userId))
                {
                    VCORepository repository = new VCORepository();
                ApplicationPayload application = repository.GetApplicationForm(id);
                return View(application);
                }
                else
                {
                    return RedirectToAction("SessionTimeOut", "Session");
                }
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
        /// <param name="application"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult UpdateApplication(ApplicationPayload application)
        {
            try
            {
                int userId;
                if (HttpContext.Session["LoginId"] != null && int.TryParse(HttpContext.Session["LoginId"].ToString(), out userId))
                {
                    VCORepository repository = new VCORepository();
                repository.UpdateStatus(application);
                return RedirectToAction("Update");
                }
                else
                {
                    return RedirectToAction("SessionTimeOut", "Session");
                }
            
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
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public ActionResult GeneratePDF(int id)
        {
            try
            {
                UserRepository userRepository = new UserRepository();
                byte[] pdfBytes = userRepository.GeneratePdfBytes(id);
                return File(pdfBytes, "application/pdf", "generated.pdf");

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
        [Authorize]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            try
            {
                int userId;
                if (HttpContext.Session["LoginId"] != null && int.TryParse(HttpContext.Session["LoginId"].ToString(), out userId))
                {

                    return View();
                }
                else
                {
                    return RedirectToAction("SessionTimeOut", "Session");
                }
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
        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(Login login)
        {
            try
            {
                int userId;
                if (HttpContext.Session["LoginId"] != null && int.TryParse(HttpContext.Session["LoginId"].ToString(), out userId))
                {
                    login.userName = Session["UserName"].ToString();
                    LoginRepository loginRepository = new LoginRepository();
                    Registration registration = loginRepository.GetLogin(login);
                    if (registration.isVerified)
                    {
                        return RedirectToAction("NewPassword");
                    }
                    else
                    {
                        return View();
                    }

                }
                else
                {
                    return RedirectToAction("SessionTimeOut", "Session");
                }
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
        [Authorize]
        [HttpGet]
        public ActionResult NewPassword()
        {
            try
            {
                int userId;
                if (HttpContext.Session["LoginId"] != null && int.TryParse(HttpContext.Session["LoginId"].ToString(), out userId))
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("SessionTimeOut", "Session");
                }

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
        /// <param name="password"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult NewPassword(Password password)
        {
            try
            {
                int userId;
                if (HttpContext.Session["LoginId"] != null && int.TryParse(HttpContext.Session["LoginId"].ToString(), out userId))
                {
                    if (password != null && password.newPassword != null && password.conformPassword != null)
                    {
                        password.registerID = userId;
                        UserRepository userRepository = new UserRepository();
                        userRepository.NewPassword(password);

                    }
                    return RedirectToAction("ChangePassword");
                }
                else
                {
                    return RedirectToAction("SessionTimeOut", "Session");
                }

            }
            catch (Exception ex)
            {
                LogError(ex);
                return View("Error");
            }
            
        }
        [HttpGet]
        public ActionResult SignOutVco()
        {
            return View();// Redirect to the login page

        }
        [HttpPost]
        public ActionResult SignOutVco(ForgotPassword forgotPassword)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
            FormsAuthentication.SignOut();
            HttpContext.Session.Clear();
            HttpContext.Session.Abandon();
            HttpContext.Session.RemoveAll();
            return RedirectToAction("Login", "Login");

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SignOut()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
            FormsAuthentication.SignOut();
            HttpContext.Session.Clear();
            HttpContext.Session.Abandon();
            HttpContext.Session.RemoveAll();
            return RedirectToAction("Index"); // Redirect to the login page

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public ActionResult Profile()
        {
            try
            {
                int userId;
                if (HttpContext.Session["LoginId"] != null && int.TryParse(HttpContext.Session["LoginId"].ToString(), out userId))
                {
                    AdminRepository adminRepository = new AdminRepository();
                    Registration registration = adminRepository.GetProfileById(userId);


                    return View(registration);
                }
                else
                {
                    return RedirectToAction("SessionTimeOut", "Session");
                }
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
        [Authorize]
        [HttpPost]
        public ActionResult Profile(Registration registration)
        {
            try
            {
                int userId;
                if (HttpContext.Session["LoginId"] != null && int.TryParse(HttpContext.Session["LoginId"].ToString(), out userId))
                {
                    registration.registrationID = userId;
                    AdminRepository adminRepository = new AdminRepository();
                    adminRepository.GetUpdateProfile(registration);
                    return RedirectToAction("Profile");
                }
                else
                {
                    return RedirectToAction("SessionTimeOut", "Session");
                }
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
        /// <param name="registrationID"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult UploadPicture(int registrationID, HttpPostedFileBase file)
        {
            try
            {
                int userId;
                if (HttpContext.Session["LoginId"] != null && int.TryParse(HttpContext.Session["LoginId"].ToString(), out userId))
                {
                    byte[] imageData;
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(file.ContentLength);
                    }
                    LoginRepository loginRepository = new LoginRepository();
                    loginRepository.UploadImage(registrationID, imageData);

                    return RedirectToAction("Profile");
                }
                else
                {
                    return RedirectToAction("SessionTimeOut", "Session");
                }
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
        //return RedirectToAction("Session","SessionTimeOut");
    }
}
