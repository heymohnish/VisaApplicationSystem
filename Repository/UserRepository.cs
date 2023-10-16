using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisaApplicationSystem.Models;
using static System.Net.Mime.MediaTypeNames;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

using System.Xml.Linq;
using Image = iTextSharp.text.Image;
using Login = VisaApplicationSystem.Models.Login;

namespace VisaApplicationSystem.Repository
{
    public class UserRepository: BaseDatabaseConnection
    {
        public List<Visa> GetAllVisaTypes()
        {
            List<Visa> visaTypes = new List<Visa>();

            try
            {
                InitializeConnection();
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_GetAllVisaType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Visa visaType = new Visa
                            {
                                // Populate the VisaType properties from the reader
                                visaID = (int)reader["VisaId"],
                                visaName = reader["VisaName"].ToString(),
                                visaTitle = reader["VisaTitle"].ToString(),
                                visaDescription = reader["VisaDescription"].ToString(),

                                // Add other properties as needed
                            };

                            visaTypes.Add(visaType);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Handle the exception as needed
            }
            finally
            {
                connection.Close();
            }

            return visaTypes;
        }
        public Visa GetAllVisaTypeById(int id)
        {
            Visa visaType = null;

            try
            {
                InitializeConnection();
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_GetVisaTypeById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@VisaID", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            visaType = new Visa
                            {
                                // Populate the VisaType properties from the reader
                                visaID = (int)reader["VisaId"],
                                visaName = reader["VisaName"].ToString(),
                                visaTitle = reader["VisaTitle"].ToString(),
                                visaDescription = reader["VisaDescription"].ToString(),
                                adminID = (int)reader["AdminID"],
                                PAN = (bool)reader["PAN"],
                                aadhar = (bool)reader["Aadhar"],
                                governmentProof = (bool)reader["GovenmentProof"],
                                photo = (bool)reader["Photo"],
                                passport = (bool)reader["Passport"],
                                employeeProof = (bool)reader["EmployeeProof"],
                                educationProof = (bool)reader["EducationProof"],
                                bankProof = (bool)reader["BankProof"],
                                toeflCertification = (bool)reader["ToeflCertification"],
                                visitorProof = (bool)reader["VisitorProof"],
                                isPersonalInformation = (bool)reader["PersonalInformation"]

                            };

                        }
                    }
                }
             return visaType;
            }
            finally
            {
                connection.Close();
            }

           
        }

        public void CreateApplication(ApplicationForm model)
        {
            Visa visaType = null;
            ApplicationDB applicationDB = new ApplicationDB(model);
           
            try
            {
                InitializeConnection();
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_InsertApplicationForm", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@VisaName", applicationDB.visaName);
                    command.Parameters.AddWithValue("@VisaTitle", applicationDB.visaTitle);
                    command.Parameters.AddWithValue("@VisaDescription", applicationDB.visaDiscription);
                    command.Parameters.AddWithValue("@VisaID", applicationDB.visaId);
                    command.Parameters.AddWithValue("@RegistrationID", applicationDB.registrationID);
                    // Modify the code for parameters that can be null
                    command.Parameters.AddWithValue("@FullName", applicationDB.fullName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@DateOfBirth", applicationDB.dateOfBirth);

                    command.Parameters.AddWithValue("@Nationality", applicationDB.nationality ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Gender", applicationDB.gender ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PassportNumber", applicationDB.passportNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PassportExpiryDate", applicationDB.passportExpiryDate);
                    command.Parameters.AddWithValue("@PhoneNumber", applicationDB.phoneNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Email", applicationDB.email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ResidentialAddress", applicationDB.residentialAddress ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PurposeOfTravel", applicationDB.purposeOfTravel ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@DepartureDate", applicationDB.departureDate);
                    command.Parameters.AddWithValue("@ReturnDate", applicationDB.returnDate);
                    // Add similar handling for other nullable parameters

                    // For binary fields, you can use byte arrays and handle null values like this
                    if (applicationDB.photo != null)
                    {
                        command.Parameters.Add("@Photo", SqlDbType.VarBinary).Value = applicationDB.photo;
                    }
                    else
                    {
                        command.Parameters.Add("@Photo", SqlDbType.VarBinary).Value = DBNull.Value;
                    }
                    if (applicationDB.PAN != null)
                    {
                        command.Parameters.Add("@PAN", SqlDbType.VarBinary).Value = applicationDB.PAN;
                    }
                    else
                    {
                        command.Parameters.Add("@PAN", SqlDbType.VarBinary).Value = DBNull.Value;

                    }
                    
                    if (applicationDB.aadhar != null)
                    {
                        command.Parameters.Add("@Aadhar", SqlDbType.VarBinary).Value = applicationDB.aadhar;
                    }
                    else
                    {
                        command.Parameters.Add("@Aadhar", SqlDbType.VarBinary).Value = DBNull.Value;
                    }
                    if (applicationDB.govenmentProof != null)
                    {
                        command.Parameters.Add("@GovernmentProof", SqlDbType.VarBinary).Value = applicationDB.govenmentProof;
                    }
                    else
                    {
                        command.Parameters.Add("@GovernmentProof", SqlDbType.VarBinary).Value = DBNull.Value;
                    }
                    if (applicationDB.passport != null)
                    {
                        command.Parameters.Add("@Passport", SqlDbType.VarBinary).Value = applicationDB.passport;
                    }
                    else
                    {
                        command.Parameters.Add("@Passport", SqlDbType.VarBinary).Value = DBNull.Value;
                    }
                    if (applicationDB.employeeProof != null)
                    {
                        command.Parameters.Add("@EmployeeProof", SqlDbType.VarBinary).Value = applicationDB.employeeProof;
                    }
                    else
                    {
                        command.Parameters.Add("@EmployeeProof", SqlDbType.VarBinary).Value = DBNull.Value;
                    }
                    if (applicationDB.educationProof != null)
                    {
                        command.Parameters.Add("@EducationProof", SqlDbType.VarBinary).Value = applicationDB.educationProof;
                    }
                    else
                    {
                        command.Parameters.Add("@EducationProof", SqlDbType.VarBinary).Value = DBNull.Value;
                    }

                    if (applicationDB.bankProof != null)
                    {
                        command.Parameters.Add("@BankProof", SqlDbType.VarBinary).Value = applicationDB.bankProof;
                    }
                    else
                    {
                        command.Parameters.Add("@BankProof", SqlDbType.VarBinary).Value = DBNull.Value;
                    }
                    if (applicationDB.toeflCertification != null)
                    {
                        command.Parameters.Add("@ToeflCertification", SqlDbType.VarBinary).Value = applicationDB.toeflCertification;
                    }
                    else
                    {
                        command.Parameters.Add("@ToeflCertification", SqlDbType.VarBinary).Value = DBNull.Value;
                    }
                    if (applicationDB.visitorProof != null)
                    {
                        command.Parameters.Add("@VisitorProof", SqlDbType.VarBinary).Value = applicationDB.visitorProof;
                    }
                    else
                    {
                        command.Parameters.Add("@VisitorProof", SqlDbType.VarBinary).Value = DBNull.Value;
                    }
                    // Handling BIT parameters (boolean)
                    command.Parameters.AddWithValue("@IsPersonalInformation", applicationDB.isPersonalInformation);
                    command.Parameters.AddWithValue("@IsPhoto", applicationDB.isPhoto);
                    command.Parameters.AddWithValue("@IsPAN", applicationDB.isPAN);
                    command.Parameters.AddWithValue("@IsAadhar", applicationDB.isAadhar);
                    command.Parameters.AddWithValue("@IsGovernmentProof", applicationDB.isGovenmentProof);
                    command.Parameters.AddWithValue("@IsPassport", applicationDB.isPassport);
                    command.Parameters.AddWithValue("@IsEmployeeProof", applicationDB.isEmployeeProof);
                    command.Parameters.AddWithValue("@IsEducationProof", applicationDB.isEducationProof);
                    command.Parameters.AddWithValue("@IsBankProof", applicationDB.isBankProof);
                    command.Parameters.AddWithValue("@IsToeflCertification", applicationDB.isToeflCertification);
                    command.Parameters.AddWithValue("@IsVisitorProof", applicationDB.isVisitorProof);
                    // Add similar handling for other BIT parameters

                    // For INT and NVARCHAR(MAX) parameters, handle null or default values accordingly
                    command.Parameters.AddWithValue("@Status", applicationDB.status);
                    command.Parameters.AddWithValue("@MessageVCO", applicationDB.messageVCO ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@MessageUser", applicationDB.messageUser ?? (object)DBNull.Value);
                    // Add similar handling for other parameters

                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                connection.Close();
            }

        }

        public List<ApplicationForm> GetApplicationByUserID(int userId)
        {
            List<ApplicationForm> applications = new List<ApplicationForm>();

            try
            {
                InitializeConnection();
                connection.Open();

                // Define your SQL query

                using (SqlCommand command = new SqlCommand("sp_GetApplicationByUserID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // Add parameters
                    command.Parameters.AddWithValue("@RegistrationID", userId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Create a new ApplicationForm object and map the data from the reader
                            ApplicationForm application = new ApplicationForm
                            {
                                // Map properties from the reader to the model
                                applicationID = Convert.ToInt32(reader["ApplicationID"]),
                                visaName = reader["VisaName"].ToString(),
                                visaTitle = reader["VisaTitle"].ToString(),
                                status = (Status)reader["Status"],
                                applicantName = reader["FullName"].ToString(),
                            };
                            applications.Add(application);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
            }
            finally
            {
                connection.Close();
            }

            return applications;
        }
        public List<ApplicationForm> GeneratePdf(int userId)
        {
            List<ApplicationForm> applications = new List<ApplicationForm>();

            try
            {
                InitializeConnection();
                connection.Open();

                // Define your SQL query

                using (SqlCommand command = new SqlCommand("sp_GetApplicationByUserID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // Add parameters
                    command.Parameters.AddWithValue("@RegistrationID", userId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Create a new ApplicationForm object and map the data from the reader
                            ApplicationForm application = new ApplicationForm
                            {
                                // Map properties from the reader to the model
                                applicationID = Convert.ToInt32(reader["ApplicationID"]),
                                visaName = reader["VisaName"].ToString(),
                                visaTitle = reader["VisaTitle"].ToString(),
                                status = (Status)reader["Status"],
                            };

                            applications.Add(application);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
            }
            finally
            {
                connection.Close();
            }

            return applications;
        }
        public List<ApplicationForm> GetApplicationMessage(int userId)
        {
            List<ApplicationForm> applications = new List<ApplicationForm>();

            try
            {
                InitializeConnection();
                connection.Open();

                // Define your SQL query

                using (SqlCommand command = new SqlCommand("sp_GetApplicationByUserID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // Add parameters
                    command.Parameters.AddWithValue("@RegistrationID", userId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ApplicationForm application = new ApplicationForm
                            {
                                applicationID = Convert.ToInt32(reader["ApplicationID"]),
                                visaName = reader["VisaName"].ToString(),
                                messageUser = reader["MessageUser"].ToString(),
                            };
                            applications.Add(application);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
            }
            finally
            {
                connection.Close();
            }

            return applications;
        }
        public List<ApplicationForm> GetApplicationDraft(int userId)
        {
            List<ApplicationForm> applications = new List<ApplicationForm>();

            try
            {
                InitializeConnection();
                connection.Open();

                // Define your SQL query

                using (SqlCommand command = new SqlCommand("sp_GetApplicationDraft", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // Add parameters
                    command.Parameters.AddWithValue("@RegistrationID", userId);

                    command.Parameters.AddWithValue("@Status", 5);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ApplicationForm application = new ApplicationForm
                            {
                                applicationID = Convert.ToInt32(reader["ApplicationID"]),
                                visaName = reader["VisaName"].ToString(),
                                visaTitle= reader["VisaTitle"].ToString(),
                            };
                            applications.Add(application);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
            }
            finally
            {
                connection.Close();
            }

            return applications;
        }

        public void DeleteApplication(int id)
        {
            try
            {
                InitializeConnection();
                connection.Open();

                // Define your SQL query

                using (SqlCommand command = new SqlCommand("sp_DeleteApplication", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // Add parameters
                    command.Parameters.AddWithValue("@ApplicationID", id);

                    command.ExecuteReader();
                    
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
            }
            finally
            {
                connection.Close();
            }
        }

        public void NewPassword(Password password)
        {
            try
            {
                
                InitializeConnection();
                connection.Open();
                AdminRepository adminRepository = new AdminRepository();
                // Define your SQL query
                string salt = adminRepository.GenerateRandomSalt();
                string hashPassword = adminRepository.HashPassword(password.newPassword, salt);
                using (SqlCommand command = new SqlCommand("sp_ChangePassword", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // Add parameters
                    command.Parameters.AddWithValue("@RegistrationID", password.registerID);
                    command.Parameters.AddWithValue("@PasswordHash", hashPassword);
                    command.Parameters.AddWithValue("@Salt", salt);
                    command.ExecuteReader();

                }
            }
            finally
            {
                connection.Close();
            }
        }
        public void DraftApplication(ApplicationPayload applicationDB)
        {
            {

                try
                {
                    InitializeConnection();
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("sp_UpdateApplicationForm", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ApplicationID", applicationDB.applicationID);
                        command.Parameters.AddWithValue("@VisaName", applicationDB.visaName);
                        command.Parameters.AddWithValue("@VisaTitle", applicationDB.visaTitle);
                        command.Parameters.AddWithValue("@VisaDescription", applicationDB.visaDiscription);
                        command.Parameters.AddWithValue("@VisaID", applicationDB.visaId);
                        command.Parameters.AddWithValue("@RegistrationID", applicationDB.registrationID);
                        // Modify the code for parameters that can be null
                        command.Parameters.AddWithValue("@FullName", applicationDB.fullName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DateOfBirth", applicationDB.dateOfBirth);

                        command.Parameters.AddWithValue("@Nationality", applicationDB.nationality ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Gender", applicationDB.gender ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PassportNumber", applicationDB.passportNumber ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PassportExpiryDate", applicationDB.passportExpiryDate);
                        command.Parameters.AddWithValue("@PhoneNumber", applicationDB.phoneNumber ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Email", applicationDB.email ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ResidentialAddress", applicationDB.residentialAddress ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PurposeOfTravel", applicationDB.purposeOfTravel ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DepartureDate", applicationDB.departureDate);
                        command.Parameters.AddWithValue("@ReturnDate", applicationDB.returnDate);
                        // Add similar handling for other nullable parameters

                        // For binary fields, you can use byte arrays and handle null values like this
                        if (applicationDB.photo != null)
                        {
                            command.Parameters.Add("@Photo", SqlDbType.VarBinary).Value = applicationDB.photo;
                        }
                        else
                        {
                            command.Parameters.Add("@Photo", SqlDbType.VarBinary).Value = DBNull.Value;
                        }
                        if (applicationDB.PAN != null)
                        {
                            command.Parameters.Add("@PAN", SqlDbType.VarBinary).Value = applicationDB.PAN;
                        }
                        else
                        {
                            command.Parameters.Add("@PAN", SqlDbType.VarBinary).Value = DBNull.Value;

                        }

                        if (applicationDB.aadhar != null)
                        {
                            command.Parameters.Add("@Aadhar", SqlDbType.VarBinary).Value = applicationDB.aadhar;
                        }
                        else
                        {
                            command.Parameters.Add("@Aadhar", SqlDbType.VarBinary).Value = DBNull.Value;
                        }
                        if (applicationDB.govenmentProof != null)
                        {
                            command.Parameters.Add("@GovernmentProof", SqlDbType.VarBinary).Value = applicationDB.govenmentProof;
                        }
                        else
                        {
                            command.Parameters.Add("@GovernmentProof", SqlDbType.VarBinary).Value = DBNull.Value;
                        }
                        if (applicationDB.passport != null)
                        {
                            command.Parameters.Add("@Passport", SqlDbType.VarBinary).Value = applicationDB.passport;
                        }
                        else
                        {
                            command.Parameters.Add("@Passport", SqlDbType.VarBinary).Value = DBNull.Value;
                        }
                        if (applicationDB.employeeProof != null)
                        {
                            command.Parameters.Add("@EmployeeProof", SqlDbType.VarBinary).Value = applicationDB.employeeProof;
                        }
                        else
                        {
                            command.Parameters.Add("@EmployeeProof", SqlDbType.VarBinary).Value = DBNull.Value;
                        }
                        if (applicationDB.educationProof != null)
                        {
                            command.Parameters.Add("@EducationProof", SqlDbType.VarBinary).Value = applicationDB.educationProof;
                        }
                        else
                        {
                            command.Parameters.Add("@EducationProof", SqlDbType.VarBinary).Value = DBNull.Value;
                        }

                        if (applicationDB.bankProof != null)
                        {
                            command.Parameters.Add("@BankProof", SqlDbType.VarBinary).Value = applicationDB.bankProof;
                        }
                        else
                        {
                            command.Parameters.Add("@BankProof", SqlDbType.VarBinary).Value = DBNull.Value;
                        }
                        if (applicationDB.toeflCertification != null)
                        {
                            command.Parameters.Add("@ToeflCertification", SqlDbType.VarBinary).Value = applicationDB.toeflCertification;
                        }
                        else
                        {
                            command.Parameters.Add("@ToeflCertification", SqlDbType.VarBinary).Value = DBNull.Value;
                        }
                        if (applicationDB.visitorProof != null)
                        {
                            command.Parameters.Add("@VisitorProof", SqlDbType.VarBinary).Value = applicationDB.visitorProof;
                        }
                        else
                        {
                            command.Parameters.Add("@VisitorProof", SqlDbType.VarBinary).Value = DBNull.Value;
                        }
                        // Handling BIT parameters (boolean)
                        command.Parameters.AddWithValue("@IsPersonalInformation", applicationDB.isPersonalInformation);
                        command.Parameters.AddWithValue("@IsPhoto", applicationDB.isPhoto);
                        command.Parameters.AddWithValue("@IsPAN", applicationDB.isPAN);
                        command.Parameters.AddWithValue("@IsAadhar", applicationDB.isAadhar);
                        command.Parameters.AddWithValue("@IsGovernmentProof", applicationDB.isGovenmentProof);
                        command.Parameters.AddWithValue("@IsPassport", applicationDB.isPassport);
                        command.Parameters.AddWithValue("@IsEmployeeProof", applicationDB.isEmployeeProof);
                        command.Parameters.AddWithValue("@IsEducationProof", applicationDB.isEducationProof);
                        command.Parameters.AddWithValue("@IsBankProof", applicationDB.isBankProof);
                        command.Parameters.AddWithValue("@IsToeflCertification", applicationDB.isToeflCertification);
                        command.Parameters.AddWithValue("@IsVisitorProof", applicationDB.isVisitorProof);
                        // Add similar handling for other BIT parameters

                        // For INT and NVARCHAR(MAX) parameters, handle null or default values accordingly
                        command.Parameters.AddWithValue("@Status", applicationDB.status);
                        command.Parameters.AddWithValue("@MessageVCO", applicationDB.messageVCO ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@MessageUser", applicationDB.messageUser ?? (object)DBNull.Value);
                        // Add similar handling for other parameters

                        command.ExecuteNonQuery();
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public byte[] GeneratePdfBytes(int id)
        {
            Document document = new Document();
            try
            {
                VCORepository repository = new VCORepository();
                ApplicationPayload payload = repository.GetApplicationForm(id);
                using (MemoryStream stream = new MemoryStream())
                {
                    Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                    PdfWriter writer = PdfWriter.GetInstance(document, stream);
                    document.Open();
                    string path = "C:\\Users\\Admin\\Desktop\\mohan\\VisaApplicationSystem\\Content\\Image\\Government_of_India_logo.svg.jpg";
                    if (!string.IsNullOrEmpty(path))
                    {
                        Image image = Image.GetInstance(path);
                        image.ScaleAbsolute(170f, 70f);
                        document.Add(image);
                    }
                    document.Add(new Paragraph(" "));
                    Paragraph applicationIdParagraph = new Paragraph();
                    applicationIdParagraph.Add(new Chunk("Application ID: ", boldFont)); // Apply bold font
                    applicationIdParagraph.Add(payload.applicationID.ToString());
                    document.Add(applicationIdParagraph);
                    applicationIdParagraph = new Paragraph();
                    applicationIdParagraph.Add(new Chunk("Visa Name: ", boldFont)); // Apply bold font
                    applicationIdParagraph.Add(payload.visaName.ToString());
                    document.Add(applicationIdParagraph);
                    applicationIdParagraph = new Paragraph();
                    applicationIdParagraph.Add(new Chunk("Visa Title: ", boldFont)); // Apply bold font
                    applicationIdParagraph.Add(payload.visaTitle.ToString());
                    document.Add(applicationIdParagraph);
                    applicationIdParagraph = new Paragraph();
                    applicationIdParagraph.Add(new Chunk("Visa Description: ", boldFont)); // Apply bold font
                    applicationIdParagraph.Add(payload.visaDiscription.ToString());
                    document.Add(applicationIdParagraph);
                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph(" "));
                    if (payload.isPersonalInformation)
                    {
                        document.Add(new Paragraph("Personal Details :", boldFont));
                        document.Add(new Paragraph(" "));
                        PdfPTable table = new PdfPTable(2); // 2 columns
                        table.WidthPercentage = 100; // 100% width
                        table.DefaultCell.Border = Rectangle.NO_BORDER;
                        PdfPCell cell = new PdfPCell(new Phrase("Full Name", boldFont)); // Apply bold font
                        cell.Border = Rectangle.NO_BORDER; // Remove border for this cell
                        table.AddCell(cell);
                        table.AddCell(payload.fullName);
                        cell = new PdfPCell(new Phrase("Date of Birth", boldFont)); // Apply bold font
                        cell.Border = Rectangle.NO_BORDER; // Remove border for this cell
                        table.AddCell(cell);
                        table.AddCell(payload.dateOfBirth.ToString("MM/dd/yyyy"));
                        cell = new PdfPCell(new Phrase("Nationality", boldFont)); // Apply bold font
                        cell.Border = Rectangle.NO_BORDER; // Remove border for this cell
                        table.AddCell(cell);
                        table.AddCell(payload.nationality);
                        cell = new PdfPCell(new Phrase("Gender", boldFont)); // Apply bold font
                        cell.Border = Rectangle.NO_BORDER; // Remove border for this cell
                        table.AddCell(cell);
                        table.AddCell(payload.gender);
                        cell = new PdfPCell(new Phrase("Passport No", boldFont)); // Apply bold font
                        cell.Border = Rectangle.NO_BORDER; // Remove border for this cell
                        table.AddCell(cell);
                        table.AddCell(payload.passportNumber);
                        cell = new PdfPCell(new Phrase("Passport expire", boldFont)); // Apply bold font
                        cell.Border = Rectangle.NO_BORDER; // Remove border for this cell
                        table.AddCell(cell);
                        table.AddCell(payload.passportExpiryDate.ToString("MM/dd/yyyy"));
                        cell = new PdfPCell(new Phrase("Phone No", boldFont)); // Apply bold font
                        cell.Border = Rectangle.NO_BORDER; // Remove border for this cell
                        table.AddCell(cell);
                        table.AddCell(payload.phoneNumber);
                        cell = new PdfPCell(new Phrase("Email", boldFont)); // Apply bold font
                        cell.Border = Rectangle.NO_BORDER; // Remove border for this cell
                        table.AddCell(cell);
                        table.AddCell(payload.email);
                        cell = new PdfPCell(new Phrase("Address", boldFont)); // Apply bold font
                        cell.Border = Rectangle.NO_BORDER; // Remove border for this cell
                        table.AddCell(cell);
                        table.AddCell(payload.residentialAddress);
                        cell = new PdfPCell(new Phrase("Purpose of travel", boldFont)); // Apply bold font
                        cell.Border = Rectangle.NO_BORDER; // Remove border for this cell
                        table.AddCell(cell);
                        table.AddCell(payload.purposeOfTravel);
                        cell = new PdfPCell(new Phrase("Departure date", boldFont)); // Apply bold font
                        cell.Border = Rectangle.NO_BORDER; // Remove border for this cell
                        table.AddCell(cell);
                        table.AddCell(payload.departureDate.ToString("MM/dd/yyyy"));
                        cell = new PdfPCell(new Phrase("Return date", boldFont)); // Apply bold font
                        cell.Border = Rectangle.NO_BORDER; // Remove border for this cell
                        table.AddCell(cell);
                        table.AddCell(payload.returnDate.ToString("MM/dd/yyyy"));
                        document.Add(table);
                    }
                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph(" "));

                    //Supportive documents
                    document.Add(new Paragraph("Supportive Documents :", boldFont));
                    document.Add(new Paragraph(" "));

                    if (payload.isPhoto)
                    {
                        document.Add(new Paragraph("Photo :", boldFont));
                        document.Add(new Paragraph(" "));
                        if (payload.photo != null && payload.photo.Length > 0)
                        {
                            Image image = Image.GetInstance(payload.photo);
                            image.ScaleAbsolute(170f, 70f);
                            document.Add(image);
                        }
                    }
                    // Check if isPAN is true, and if so, add the "PAN" label and the image
                    if (payload.isPAN)
                    {
                        // Add "PAN:" label
                        document.Add(new Paragraph("PAN:", boldFont)); // Apply bold font
                        document.Add(new Paragraph(" ")); // Add some spacing
                        if (payload.PAN != null && payload.PAN.Length > 0)
                        {
                            Image panImage = Image.GetInstance(payload.PAN);
                            panImage.ScaleAbsolute(170f, 70f); // Adjust the width and height as needed
                            document.Add(panImage);
                        }
                    }
                    // Check if isAadhar is true, and if so, add the "Aadhar" label and the image
                    if (payload.isAadhar)
                    {
                        // Add "Aadhar:" label
                        document.Add(new Paragraph("Aadhar:", boldFont)); // Apply bold font
                        document.Add(new Paragraph(" ")); // Add some spacing
                        if (payload.aadhar != null && payload.aadhar.Length > 0)
                        {
                            Image aadharImage = Image.GetInstance(payload.aadhar);
                            aadharImage.ScaleAbsolute(170f, 70f); // Adjust the width and height as needed
                            document.Add(aadharImage);
                        }
                    }
                    // Check if isGovenmentProof is true, and if so, add the "Government Proof" label and the image
                    if (payload.isGovenmentProof)
                    {
                        // Add "Government Proof:" label
                        document.Add(new Paragraph("Government Proof:", boldFont)); // Apply bold font
                        document.Add(new Paragraph(" ")); // Add some spacing
                        if (payload.govenmentProof != null && payload.govenmentProof.Length > 0)
                        {
                            Image governmentProofImage = Image.GetInstance(payload.govenmentProof);
                            governmentProofImage.ScaleAbsolute(170f, 70f); // Adjust the width and height as needed
                            document.Add(governmentProofImage);
                        }
                    }
                    // Check if isPassport is true, and if so, add the "Passport" label and the image
                    if (payload.isPassport)
                    {
                        // Add "Passport:" label
                        document.Add(new Paragraph("Passport:", boldFont)); // Apply bold font
                        document.Add(new Paragraph(" ")); // Add some spacing
                        if (payload.passport != null && payload.passport.Length > 0)
                        {
                            Image passportImage = Image.GetInstance(payload.passport);
                            passportImage.ScaleAbsolute(170f, 70f); // Adjust the width and height as needed
                            document.Add(passportImage);
                        }
                    }
                    // Check if isEmployeeProof is true, and if so, add the "Employee Proof" label and the image
                    if (payload.isEmployeeProof)
                    {
                        // Add "Employee Proof:" label
                        document.Add(new Paragraph("Employee Proof:", boldFont)); // Apply bold font
                        document.Add(new Paragraph(" ")); // Add some spacing
                        if (payload.employeeProof != null && payload.employeeProof.Length > 0)
                        {
                            Image employeeProofImage = Image.GetInstance(payload.employeeProof);
                            employeeProofImage.ScaleAbsolute(170f, 70f); // Adjust the width and height as needed
                            document.Add(employeeProofImage);
                        }
                    }
                    // Check if isEducationProof is true, and if so, add the "Education Proof" label and the image
                    if (payload.isEducationProof)
                    {
                        // Add "Education Proof:" label
                        document.Add(new Paragraph("Education Proof:", boldFont)); // Apply bold font
                        document.Add(new Paragraph(" ")); // Add some spacing
                        if (payload.educationProof != null && payload.educationProof.Length > 0)
                        {
                            Image educationProofImage = Image.GetInstance(payload.educationProof);
                            educationProofImage.ScaleAbsolute(170f, 70f); // Adjust the width and height as needed
                            document.Add(educationProofImage);
                        }
                    }
                    // Check if isBankProof is true, and if so, add the "Bank Proof" label and the image
                    if (payload.isBankProof)
                    {
                        // Add "Bank Proof:" label
                        document.Add(new Paragraph("Bank Proof:", boldFont)); // Apply bold font
                        document.Add(new Paragraph(" ")); // Add some spacing
                        if (payload.bankProof != null && payload.bankProof.Length > 0)
                        {
                            Image bankProofImage = Image.GetInstance(payload.bankProof);
                            bankProofImage.ScaleAbsolute(170f, 70f); // Adjust the width and height as needed
                            document.Add(bankProofImage);
                        }
                    }
                    // Check if isToeflCertification is true, and if so, add the "TOEFL Certification" label and the image
                    if (payload.isToeflCertification)
                    {
                        // Add "TOEFL Certification:" label
                        document.Add(new Paragraph("TOEFL Certification:", boldFont)); // Apply bold font
                        document.Add(new Paragraph(" ")); // Add some spacing
                        if (payload.toeflCertification != null && payload.toeflCertification.Length > 0)
                        {
                            Image toeflCertificationImage = Image.GetInstance(payload.toeflCertification);
                            toeflCertificationImage.ScaleAbsolute(170f, 70f); // Adjust the width and height as needed
                            document.Add(toeflCertificationImage);
                        }
                    }
                    // Check if isVisitorProof is true, and if so, add the "Visitor Proof" label and the image
                    if (payload.isVisitorProof)
                    {
                        // Add "Visitor Proof:" label
                        document.Add(new Paragraph("Visitor Proof:", boldFont)); // Apply bold font
                        document.Add(new Paragraph(" ")); // Add some spacing
                        if (payload.visitorProof != null && payload.visitorProof.Length > 0)
                        {
                            Image visitorProofImage = Image.GetInstance(payload.visitorProof);
                            visitorProofImage.ScaleAbsolute(170f, 70f); // Adjust the width and height as needed
                            document.Add(visitorProofImage);
                        }
                    }
                    string approve = "C:\\Users\\Admin\\Desktop\\mohan\\VisaApplicationSystem\\Content\\Image\\approved.png";
                    string rejected = "C:\\Users\\Admin\\Desktop\\mohan\\VisaApplicationSystem\\Content\\Image\\Rejected.png";
                    if (payload.status != null)
                    {
                        // Create a paragraph for "Status" label and set its alignment to right
                        Paragraph labelParagraph = new Paragraph("Status", boldFont);
                        labelParagraph.Alignment = Element.ALIGN_RIGHT; // Right-align the label
                        document.Add(labelParagraph); // Add the label

                        document.Add(new Paragraph(" ")); // Add some spacing

                        if (payload.status == Status.Approved)
                        {
                            // Add "Approved" label
                            Paragraph statusParagraph = new Paragraph("Approved", boldFont);
                            statusParagraph.Alignment = Element.ALIGN_RIGHT; // Right-align the status label
                            document.Add(statusParagraph); // Add the status label

                            // Add the "Approved" image (assuming you have an 'approve' image)
                            Image approveImage = Image.GetInstance(approve);
                            approveImage.ScaleAbsolute(70f, 70f); // Adjust the width and height as needed
                            float xPosition = PageSize.A4.Width - approveImage.ScaledWidth;

                            // Calculate the y-coordinate for bottom alignment (assuming A4 page size)
                            float yPosition = 0;

                            // Set the absolute position to align it to the right-bottom corner
                            approveImage.SetAbsolutePosition(xPosition, yPosition);
                            document.Add(approveImage); // Add the image
                        }
                        else if (payload.status == Status.Rejected)
                        {
                            // Add "Rejected" label
                            Paragraph statusParagraph = new Paragraph("Rejected", boldFont);
                            statusParagraph.Alignment = Element.ALIGN_RIGHT; // Right-align the status label
                            document.Add(statusParagraph); // Add the status label

                            // Add the "Rejected" image (assuming you have a 'rejected' image)
                            Image rejectedImage = Image.GetInstance(rejected);
                            rejectedImage.ScaleAbsolute(70f, 70f); // Adjust the width and height as needed
                            float xPosition = PageSize.A4.Width - rejectedImage.ScaledWidth;

                            // Calculate the y-coordinate for bottom alignment (assuming A4 page size)
                            float yPosition = 0;

                            // Set the absolute position to align it to the right-bottom corner
                            rejectedImage.SetAbsolutePosition(xPosition, yPosition);
                            document.Add(rejectedImage); // Add the image
                        }

                        // Add a line break for spacing

                    }





                    document.Close();
                    return stream.ToArray();
                }
            }
            catch (Exception ex)
            {
                document.Close();
                Console.WriteLine("An error occurred during PDF generation: " + ex.Message);
                return null; // Or return an error byte array, or rethrow the exception
            }
        }

        
    }
}