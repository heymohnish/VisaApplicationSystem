using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using VisaApplicationSystem.Models;
using VisaApplicationSystem.Repository;
using System.IO;
using System.Web.Security;

namespace VisaApplicationSystem.Controllers
{
    public class AdminController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>View page</returns>
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
            }catch (Exception ex)
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

        public ActionResult IndexHome()
        {
            try
            {
                Registration registration = new Registration();
                return View(registration);
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
        public ActionResult CreateVisa()
        {
            try
            {
                Visa visa=new Visa();
                visa.adminID = (int)HttpContext.Session["LoginId"];
                return View(visa);
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
        /// <param name="visa"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult CreateVisa(Visa visa)
        {
            try
            {
                AdminRepository adminRepository = new AdminRepository();
                if (visa.visaTitle != null)
                {
                    visa.adminID = (int)HttpContext.Session["LoginId"];
                    adminRepository.InsertVisaType(visa);
                }
                return RedirectToAction("GetVisa", "Admin");
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
        public ActionResult GetVisa()
        {
            try
            {
                AdminRepository adminRepository = new AdminRepository();
                List<Visa> visaList = adminRepository.GetAllVisaTypes();
                return View(visaList);

            }
            catch (Exception ex)
            {
                LogError(ex);
                return View("Error");
            }
            
        }

        /* https://localhost:44329/Admin/GetVisa*/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public ActionResult Details(int id)
        {
            try
            {
                AdminRepository adminRepository = new AdminRepository();
                Visa visa = adminRepository.GetVisaType(id);
                return View(visa);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return View("Error");
            }
            
        }
        /*https://localhost:44329/Admin/Edit/1*/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                AdminRepository adminRepository = new AdminRepository();
                Visa visa = adminRepository.GetVisaType(id);
                return View(visa);
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
        /// <param name="visa"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult Edit(Visa visa)
        {
            try
            {
                Console.WriteLine(visa.visaTitle);
                AdminRepository adminRepository = new AdminRepository();
                adminRepository.UpdateVisaType(visa);
                return RedirectToAction("GetVisa", "Admin");

            }
            catch (Exception ex)
            {
                LogError(ex);
                return View("Error");
            }
            
        }
        /*https://localhost:44329/Admin/Delete/1*/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                AdminRepository adminRepository = new AdminRepository();
                 adminRepository.DeleteVisaType(id);
                return RedirectToAction("GetVisa", "Admin");
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
        public ActionResult GetAllAdmin()
        {
            try
            {
                AdminRepository adminRepository = new AdminRepository();
                List<Registration> visaList = adminRepository.GetAllByRole("Admin");
                return View(visaList);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return View("Error");
            }
            
        }
        // DeleteAdmin
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public ActionResult DeleteAdmin(int id)
        {
            try
            {
                AdminRepository adminRepository = new AdminRepository();
                adminRepository.DeleteByID(id);
                return RedirectToAction("GetAllAdmin", "Admin");
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
        public ActionResult GetAllUser()
        {
            try
            {
                AdminRepository adminRepository = new AdminRepository();
                List<Registration> registerList = adminRepository.GetAllByRole("User");
                return View(registerList);

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
        public ActionResult DeleteUser(int id)
        {
            try
            {
                AdminRepository adminRepository = new AdminRepository();
                adminRepository.DeleteByID(id);
                return RedirectToAction("GetAllUser", "Admin");
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
        public ActionResult GetAllVCO()
        {
            try
            {
                AdminRepository adminRepository = new AdminRepository();
                List<Registration> registerList = adminRepository.GetAllByRole("VCO");
                return View(registerList);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return View("Error");
            }
            
        }
        // DeleteAdmin
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public ActionResult DeleteVCO(int id)
        {
            try
            {
                AdminRepository adminRepository = new AdminRepository();
                adminRepository.DeleteByID(id);
                return RedirectToAction("GetAllVCO", "Admin");
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
        public ActionResult AddAdmin()
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
        /// <param name="registration"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult AddAdmin(Registration registration)
        {
            try
            {
                int userId;
                if (HttpContext.Session["LoginId"] != null && int.TryParse(HttpContext.Session["LoginId"].ToString(), out userId))
                {
                    Console.WriteLine(registration.address);
                    AdminRepository adminRepository = new AdminRepository();
                    registration.role = "Admin";
                    // Generate a random salt (you can use a suitable method to create a random salt)
                    registration.salt = adminRepository.GenerateRandomSalt();
                    registration.adminID = userId;
                    // Hash the user's entered password with the salt
                    registration.passwordHash = adminRepository.HashPassword(registration.password, registration.salt);
                    adminRepository.InsertRegistration(registration);
                    return RedirectToAction("AddAdmin", "Admin");
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
        public ActionResult AddVCO()
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
        /// <param name="registration"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult AddVCO(Registration registration)
        {
            try
            {
                int userId;
                if (HttpContext.Session["LoginId"] != null && int.TryParse(HttpContext.Session["LoginId"].ToString(), out userId))
                {
                    Console.WriteLine(registration.address);
                    AdminRepository adminRepository = new AdminRepository();
                    registration.role = "VCO";
                    registration.adminID = userId;
                    registration.salt = adminRepository.GenerateRandomSalt();
                    registration.passwordHash = adminRepository.HashPassword(registration.password, registration.salt);
                    adminRepository.InsertRegistration(registration);
                    return RedirectToAction("AddVCO", "Admin");
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
        /// <param name="statusFilter"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public ActionResult Application(string statusFilter)
        {
            try
            {
                AdminRepository adminRepository = new AdminRepository();
                Status statusModel;
                int status;
                if (Enum.TryParse(statusFilter, out statusModel))
                {
                    status = (int)statusModel;
                }
                else
                {
                    status = 6;
                }

                List<ApplicationPayload> applicationPayloads = adminRepository.GetAllApplicationAdmin(status);
                return View(applicationPayloads);
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
        public ActionResult DeleteApplication(int id)
        {
            try
            {
                UserRepository userRepository = new UserRepository();
                // userRepository.DeleteApplication(id);
                return RedirectToAction("Application");
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
                    return View();
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
                    return View();
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public ActionResult ContactUs()
        {
            try
            {
                AdminRepository adminRepository = new AdminRepository();
                List<Contact> contact = adminRepository.GetAllContact();
                return View(contact);

            }
            catch (Exception ex) {
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
        public ActionResult DeleteContact(int id)
        {
            try
            {
                AdminRepository adminRepository = new AdminRepository();
                adminRepository.DeleteContactUs(id);
                return RedirectToAction("ContactUs");

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
                    registration.registrationID=userId;
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut(); 
           
            HttpContext.Session.Clear();
            HttpContext.Session.Abandon();
            HttpContext.Session.RemoveAll();
            return RedirectToAction("Login", "Login"); // Redirect to the login page

        }
        [HttpGet]
        public ActionResult SignOutAdmin()
        {
            FormsAuthentication.SignOut();
            HttpContext.Session.Clear();
            HttpContext.Session.Abandon();
            HttpContext.Session.RemoveAll();
            return View();// Redirect to the login page

        }
        [HttpPost]
        public ActionResult SignOutAdmin(ForgotPassword forgotPassword)
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

    }

}
