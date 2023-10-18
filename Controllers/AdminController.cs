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
        /// Displays the main page for authorized users.
        /// </summary>
        /// <returns>The view page for authorized users.</returns>
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
        /// Displays the home page for authorized users.
        /// </summary>
        /// <returns>The view page for the home page.</returns>
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
        /// Displays the page for creating a new visa entry.
        /// </summary>
        /// <returns>The view page for creating a new visa entry.</returns>
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
        /// Processes the creation of a new visa entry based on the provided data.
        /// </summary>
        /// <param name="visa">The Visa object containing the visa details.</param>
        /// <returns>A redirection to the "GetVisa" action in the "Admin" controller.</returns>
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
        /// Displays the list of available visa types.
        /// </summary>
        /// <returns>The view page containing the list of available visa types.</returns>
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

        /// <summary>
        /// Displays the details of a specific visa type.
        /// </summary>
        /// <param name="id">The unique identifier of the visa type to retrieve details for.</param>
        /// <returns>The view page containing the details of the specified visa type.</returns>
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
        /// <summary>
        /// Displays the details of a specific visa type for edit.
        /// </summary>
        /// <param name="id"> the uniqe visa id can be edited</param>
        /// <returns> give webpage with datas</returns>
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
        /// when we click submit button it carry data to edit th visa
        /// </summary>
        /// <param name="visa"> Visa model datas</param>
        /// <returns> redirect to the getvisa</returns>
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
        /// <summary>
        /// delete the visa by given id
        /// </summary>
        /// <param name="id"> carry visa id</param>
        /// <returns>rediret to the same page</returns>
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
        /// Display all the admin who enrolled
        /// </summary>
        /// <returns>The view page for all the admin details<returns>
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
        /// <summary>
        /// Delete the admin by id
        /// </summary>
        /// <param name="id"> carry registration id of the admin</param>
        /// <returns>rediect to the same page</returns>
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
        /// Display all the user one who enrolled
        /// </summary>
        /// <returns>the view page display all the user</returns>
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
        /// Deletes the user by given id
        /// </summary>
        /// <param name="id"> unique registration id will be passed</param>
        /// <returns>The view page return all the user </returns>
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
        /// Display all the VCO who register
        /// </summary>
        /// <returns>The view page return all the vco</returns>
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
        /// <summary>
        /// Delete the VCO fro the given uniqe id
        /// </summary>
        /// <param name="id">uniqe registration id will be passed</param>
        /// <returns> The view page redirect get all vco page </returns>
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
        /// Display a form for adding new admin
        /// </summary>
        /// <returns>The view page return form</returns>
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
        /// once the admin form filled it carry the admin detail
        /// </summary>
        /// <param name="registration"> model consist of admin data</param>
        /// <returns>The view page redirct to the admin form fields</returns>
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
        /// Display a form for adding new VCO
        /// </summary>
        /// <returns>The view page return form</returns>
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
        /// once the admin form filled it carry the vco detail
        /// </summary>
        /// <param name="registration"> model consist of vco data</param>
        /// <returns>The view page redirct to the vco form fields</returns>
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
        /// intially displays all the application
        /// </summary>
        /// <param name="statusFilter">intially it consist a string all</param>
        /// <returns>The view page return application</returns>
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
        /// Display all the application user submits carys an application id which should be deleted
        /// </summary>
        /// <param name="id"> id carrys of application id </param>
        /// <returns>The view page redirct to the application action</returns>
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
        /// it dosnt display anything returns the file in pdf file format 
        /// </summary>
        /// <param name="id">id carrys application id </param>
        /// <returns>returns the file in pdf file format</returns>
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
        /// Display a from for typing password
        /// </summary>
        /// <returns>Display a from for typing password conform password page</returns>
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
        /// Display a from for typing password
        /// </summary>
        /// <param name="login"> carrys Login model</param>
        /// <returns>The view page redirct to the new password page oncethe givven password is correct</returns>
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
        /// display froms which is confrom password and new password
        /// </summary>
        /// <returns>The view page returns new password page</returns>
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
        /// once the conform password and ne password given it change the pass word 
        /// </summary>
        /// <param name="password"></param>
        /// <returns>The view page redirct o the change password</returns>
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
        /// display all the contacted persons faq from the home page
        /// </summary>
        /// <returns>The view page rturn all the contacted person faqs</returns>
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
        /// display all the contacted persons faq from the home page and delets the given id
        /// </summary>
        /// <param name="id"> contacted person unique id</param>
        /// <returns>The view page rturn all the contacted person faqs</returns>
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
        /// Display the profile of the admin
        /// </summary>
        /// <returns>The view page rturn profile</returns>
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
        /// Display profile of the admin
        /// </summary>
        /// <param name="registration">one on press submit it cary data that to be updated</param>
        /// <returns>The view page rturn admin profile</returns>
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
        /// display profile of the admin
        /// </summary>
        /// <param name="registrationID"></param>
        /// <param name="file"> carry a file formated image</param>
        /// <returns>The view page rturn admin profile</returns>
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
        //To write error log file
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
        /// <summary>
        /// Display an logut button 
        /// </summary>
        /// <returns>The view page return logout button page</returns>
        [HttpGet]
        public ActionResult SignOutAdmin()
        {
            return View();// Redirect to the login page

        }
        /// <summary>
        /// once the button pressed clears all the session and signout auth funtion
        /// </summary>
        /// <param name="forgotPassword"></param>
        /// <returns>The view page return login page</returns>

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
