using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using VisaApplicationSystem.Models;
using VisaApplicationSystem.Repository;
using Login = VisaApplicationSystem.Models.Login;
using System.Web.Security;
using System.Web;

namespace VisaApplicationSystem.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        /// <summary>
        /// Display welcome page with session user name
        /// </summary>
        /// <returns>The view page return user home page</returns>
        [Authorize]
        public ActionResult UserIndex()
        {
            return View();
        }
        /// <summary>
        ///  Display application page
        /// </summary>
        /// <returns>The view page return apply visa page </returns>
        [Authorize]
        public ActionResult ApplyVisa()
        {
            try
            {
                int userId;
                if (HttpContext.Session["LoginId"] != null && int.TryParse(HttpContext.Session["LoginId"].ToString(), out userId))
                {
                    UserRepository userRepository = new UserRepository();
                    List<Visa> visaType = userRepository.GetAllVisaTypes();
                    return View(visaType);
                }
                else
                {
                    HttpContext.Session.Clear();
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
        ///  Display aplication page
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The view page return application form page</returns>
        [Authorize]
        [HttpGet]
        public ActionResult VisaApply(int id)
        {
            try
            {
                int userId;
                if (HttpContext.Session["LoginId"] != null && int.TryParse(HttpContext.Session["LoginId"].ToString(), out userId))
                {
                    UserRepository userRepository = new UserRepository();
                    Visa visaType = userRepository.GetAllVisaTypeById(id);
                    //add constructur
                    ApplicationForm applicationForm = new ApplicationForm();
                    applicationForm.visaId = id;
                    applicationForm.visaName = visaType.visaName;
                    applicationForm.visaTitle = visaType.visaTitle;
                    applicationForm.visaDiscription = visaType.visaDescription;
                    applicationForm.registrationID = userId;
                    applicationForm.isPersonalInformation = visaType.isPersonalInformation;
                    applicationForm.isPhoto = visaType.photo;
                    applicationForm.isPAN = visaType.PAN;
                    applicationForm.isAadhar = visaType.aadhar;
                    applicationForm.isGovenmentProof = visaType.governmentProof;
                    applicationForm.isPassport = visaType.passport;
                    applicationForm.isEmployeeProof = visaType.employeeProof;
                    applicationForm.isEducationProof = visaType.educationProof;
                    applicationForm.isBankProof = visaType.bankProof;
                    applicationForm.isToeflCertification = visaType.toeflCertification;
                    applicationForm.isVisitorProof = visaType.visitorProof;
                    return View(applicationForm);
                }
                else
                {
                    HttpContext.Session.Clear();
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
        ///  Display visa appliy page
        /// </summary>
        /// <param name="application">carry application model data</param>
        /// <param name="submitButton">divide accouding to the button value</param>
        /// <returns>The view page redirect to apply visa page</returns>
        [Authorize]
        [HttpPost]
        public ActionResult VisaApply(ApplicationForm application, string submitButton)
        {
            try
            {
                int userId;
                if (HttpContext.Session["LoginId"] != null && int.TryParse(HttpContext.Session["LoginId"].ToString(), out userId))
                {
                    UserRepository userRepository = new UserRepository();
                    List<Visa> visaType = userRepository.GetAllVisaTypes();
                    if (submitButton == "Submit application")
                    {
                        application.status = Status.Submitted;
                    }
                    else if (submitButton == "Draft application")
                    {
                        application.status = Status.Drafted;
                    }
                   userRepository.CreateApplication(application);
                    return RedirectToAction("ApplyVisa");
                }
                else
                {
                    HttpContext.Session.Clear();
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
        ///  Display draft application page
        /// </summary>
        /// <param name="model">carry application data</param>
        /// <returns>The view page return json bool treu</returns>
        [Authorize]
        public ActionResult Draft(ApplicationForm model)
        {
            try
            {
                int userId;
                if (HttpContext.Session["LoginId"] != null && int.TryParse(HttpContext.Session["LoginId"].ToString(), out userId))
                {
                    model.status = Status.Drafted;
                    UserRepository userRepository = new UserRepository();
                    List<Visa> visaType = userRepository.GetAllVisaTypes();
                    userRepository.CreateApplication(model);
                    return Json(new { success = true });
                }
                else
                {
                    HttpContext.Session.Clear();
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
        /// display app the application for the given user
        /// </summary>
        /// <returns>The view page return all application according to the user</returns>
        [Authorize]
        [HttpGet] 
        public ActionResult MyApplication()
        {
            try
            {
                int userId;
                if (HttpContext.Session["LoginId"] != null && int.TryParse(HttpContext.Session["LoginId"].ToString(), out userId))
                {
                    UserRepository userRepository = new UserRepository();
                    List<ApplicationForm> applicaation = userRepository.GetApplicationByUserID(userId);
                    return View(applicaation);
                }
                else
                {
                    HttpContext.Session.Clear();
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
        /// Display all the application with status
        /// </summary>
        /// <returns>The view page return all application with status</returns>
        [Authorize]
        [HttpGet]
        public ActionResult CheckStatus()
        {
            try
            {
                int userId;
                if (HttpContext.Session["LoginId"] != null && int.TryParse(HttpContext.Session["LoginId"].ToString(), out userId))
                {
                    UserRepository userRepository = new UserRepository();
                    List<ApplicationForm> applicaation = userRepository.GetApplicationByUserID(userId);
                    return View(applicaation);
                }
                else
                {
                    HttpContext.Session.Clear();
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
        /// display application with message
        /// </summary>
        /// <returns>The view page return application message which was remarked by vco</returns>
        [Authorize]
        [HttpGet]
        public ActionResult Message()
        {
            try
            {
                int userId;
                if (HttpContext.Session["LoginId"] != null && int.TryParse(HttpContext.Session["LoginId"].ToString(), out userId))
                {
                    UserRepository userRepository = new UserRepository();
                    List<ApplicationForm> applicaation = userRepository.GetApplicationMessage(userId);
                    return View(applicaation);
                }
                else
                {
                    HttpContext.Session.Clear();
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
        /// Display all the draft application
        /// </summary>
        /// <returns>The view page return draft application for the uniqe user</returns>
        [Authorize]
        [HttpGet]
        public ActionResult DraftApplication()
        {
            try
            {
                int userId;
                if (HttpContext.Session["LoginId"] != null && int.TryParse(HttpContext.Session["LoginId"].ToString(), out userId))
                {
                    UserRepository userRepository = new UserRepository();
                    List<ApplicationForm> applicaation = userRepository.GetApplicationDraft(userId);
                    return View(applicaation);
                }
                else
                {
                    HttpContext.Session.Clear();
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
        /// display edit page
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The view page return applicaton edit page</returns>
        [Authorize]
        [HttpGet]
        public ActionResult EditDraft(int id)
        {
            try
            {
                int userId;
                if (HttpContext.Session["LoginId"] != null && int.TryParse(HttpContext.Session["LoginId"].ToString(), out userId))
                {
                    VCORepository vcoRepository = new VCORepository();
                    ApplicationPayload application = vcoRepository.GetApplicationForm(id);
                    application.returnd = application.returnDate.ToString("yyyy-MM-dd");
                    return View(application);
                }
                else
                {
                    HttpContext.Session.Clear();
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
        /// Display edit draft page
        /// </summary>
        /// <param name="application"></param>
        /// <returns>The view page redirect to the draft application page</returns>
        [Authorize]
        [HttpPost]
        public ActionResult EditDraft(ApplicationPayload application)
        {
            try
            {
                int userId;
                if (HttpContext.Session["LoginId"] != null && int.TryParse(HttpContext.Session["LoginId"].ToString(), out userId))
                {
                    //to change submitted testing purpose
                    application.status = Status.Drafted;
                    UserRepository userRepository = new UserRepository();
                    userRepository.DraftApplication(application);
                    return RedirectToAction("DraftApplication");
                }
                else
                {
                    HttpContext.Session.Clear();
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
        /// display all the draft application
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The view page redirect to the draft aplication page</returns>
        [Authorize]
        [HttpGet]
        public ActionResult DeleteDraft(int id)
        {
            try
            {
                int userId;
                if (HttpContext.Session["LoginId"] != null && int.TryParse(HttpContext.Session["LoginId"].ToString(), out userId))
                {
                    UserRepository userRepository = new UserRepository();
                    userRepository.DeleteApplication(id);
                    return RedirectToAction("DraftApplication", "User");
                }
                else
                {
                    HttpContext.Session.Clear();
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
        /// display all the application 
        /// </summary>
        /// <returns>>The view page return all the application for the uniiqe user</returns>
        [Authorize]
        [HttpGet]
        public ActionResult GeneratePDF()
        {
            try
            {
                int userId;
                if (HttpContext.Session["LoginId"] != null && int.TryParse(HttpContext.Session["LoginId"].ToString(), out userId))
                {
                    UserRepository userRepository = new UserRepository();
                    List<ApplicationForm> applicaation = userRepository.GeneratePdf(userId);
                    return View(applicaation);
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
        /// dosein display anything return file
        /// </summary>
        /// <param name="id"></param>
        /// <returns>>The view page return pdf file fro the uniqe application id</returns>
        [Authorize]
        [HttpGet]
        public ActionResult GeneratePDFM(int id)
        {
            try
            {
                UserRepository userRepository = new UserRepository();
                byte[] pdfBytes = userRepository.GeneratePdfBytes(id);
                Console.WriteLine(pdfBytes);
                return File(pdfBytes, "application/pdf", "generated.pdf");
            }
            catch (Exception ex)
            {
                LogError(ex);
                return View("Error");
            }
            


        }
        /// <summary>
        /// display all the application
        /// </summary>
        /// <param name="id"></param>
        /// <returns>>The view page redirect my applicaton page adter successful deletion of application</returns>
        [Authorize]
        [HttpGet]
        public ActionResult DeleteApplication(int id)
        {
            try
            {
                UserRepository userRepository = new UserRepository();
                userRepository.DeleteApplication(id);
                return RedirectToAction("MyApplication");
            }
            catch (Exception ex)
            {
                LogError(ex);
                return View("Error");
            }
            
        }
        /// <summary>
        /// Display change password page
        /// </summary>
        /// <returns>>The view page return change password page</returns>
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
        /// Display change password page
        /// </summary>
        /// <param name="login"></param>
        /// <returns>The view page redirect to the new password page after valid password typed</returns>
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
        /// Display new password page
        /// </summary>
        /// <returns>The view page return new password page</returns>
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
        /// <summary>Display new password page
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <returns>The view page redirect to the change password page</returns>
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




        /// <summary>
        /// Display profile
        /// </summary>
        /// <returns>The view page return user profile page</returns>
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
        /// Display profile
        /// </summary>
        /// <param name="registration"></param>
        /// <returns>The view page redirect to the profile page</returns>
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
        /// Display profile
        /// </summary>
        /// <param name="registrationID"></param>
        /// <param name="file"></param>
        /// <returns>The view page return redirct to the profile page of the user</returns>
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
        /// <summary>
        /// Display sogn out button
        /// </summary>
        /// <returns>The view page return sign out page</returns>
        [Authorize]
        [HttpGet]
        public ActionResult SignOutVco()
        {
            FormsAuthentication.SignOut();
            HttpContext.Session.Clear();
            HttpContext.Session.Abandon();
            HttpContext.Session.RemoveAll();
            return View();// Redirect to the login page

        }
        /// <summary>
        /// display sign out button
        /// </summary>
        /// <param name="forgotPassword"></param>
        /// <returns>The view page redirect to the login page</returns>
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
