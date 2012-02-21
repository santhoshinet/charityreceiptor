using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using saibabacharityreceiptor.Models;
using saibabacharityreceiptorDL;
using Telerik.OpenAccess;

namespace saibabacharityreceiptor.Controllers
{
    public class ControlpanelController : Controller
    {
        [Authorize]
        [HttpGet]
        public ActionResult AddUser()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Logon", "Account");
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            if (CheckAdminauthorization(scope, User.Identity.Name))
                return View();
            ViewData["Status"] = "You are not authorized to do this operation";
            return View("Status");
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddUser(RegisterModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (CheckAdminauthorization(scope, User.Identity.Name))
                {
                    if (ModelState.IsValid)
                    {
                        int count = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                     where c.Username.ToLower().Equals(model.UserName.ToLower())
                                     select c).Count();
                        if (count == 0)
                        {
                            // Attempt to register the user
                            MembershipCreateStatus createStatus;
                            Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null,
                                                  out createStatus);
                            if (createStatus == MembershipCreateStatus.Success)
                            {
                                scope.Transaction.Begin();
                                var user = new User
                                               {
                                                   Failcount = 0,
                                                   IsheAdmin = model.Admin,
                                                   IsheDonationReceiver = model.DonationReceiver,
                                                   Lasttriedtime = DateTime.Now,
                                                   Username = model.UserName,
                                                   Email = model.Email
                                               };
                                scope.Add(user);
                                scope.Transaction.Commit();
                                //FormsAuthentication.SetAuthCookie(model.UserName, true /* createPersistentCookie */);
                                ViewData["Status"] = "User added successfully.";
                                return View("Status");
                                //return RedirectToAction("Index", "Home");
                            }
                            ModelState.AddModelError("", ErrorCodeToString(createStatus));
                        }
                        ModelState.AddModelError("Username", "Username already exists, try another name.");
                    }
                    // If we got this far, something failed, redisplay form
                    return View(model);
                }
                ViewData["Status"] = "You are not authorized to do this operation";
                return View("Status");
            }
            ViewData["Status"] = "Your session has been expired, please login again and try.";
            return View("Status");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Home()
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                CheckAdminauthorization(scope, User.Identity.Name);
                return View();
            }
            return RedirectToAction("LogOn", "Account");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Users()
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (CheckAdminauthorization(scope, User.Identity.Name))
                {
                    List<User> users = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                        select c).ToList();
                    var localUsers = users.Select(user => new LocalUser
                                                              {
                                                                  Email = user.Email,
                                                                  Id = user.Id,
                                                                  IsheAdmin = user.IsheAdmin,
                                                                  IsheDonationReceiver = user.IsheDonationReceiver,
                                                                  Username = user.Username
                                                              }).ToList();
                    ViewData["UserList"] = localUsers;
                    return View();
                }
                ViewData["Status"] = "You are not authorized to do this operation";
                return View("PartialViewStatus");
            }
            return RedirectToAction("LogOn", "Account");
        }

        [HttpGet]
        public ActionResult Edituser(string uid)
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (CheckAdminauthorization(scope, User.Identity.Name))
                {
                    List<User> users = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                        where c.Id.Equals(uid)
                                        select c).ToList();
                    if (users.Count > 0)
                        return
                            View(new EditUserModel
                                     {
                                         Admin = users[0].IsheAdmin,
                                         DonationReceiver = users[0].IsheDonationReceiver,
                                         Email = users[0].Email,
                                         UserId = users[0].Id,
                                         UserName = users[0].Username,
                                         Password = string.Empty,
                                         ConfirmPassword = string.Empty
                                     });
                    return View();
                }
                ViewData["Status"] = "You are not authorized to do this operation";
                return View("Status");
            }
            ViewData["Status"] = "Your session has been expired, please login again and try.";
            return View("Status");
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edituser(EditUserModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (CheckAdminauthorization(scope, User.Identity.Name))
                {
                    if (model.Password == model.ConfirmPassword)
                    {
                        if (!string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.UserName) &&
                            !string.IsNullOrEmpty(model.UserId))
                        {
                            List<User> users =
                                (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                 where c.Id.Equals(model.UserId)
                                 select c).ToList();
                            foreach (User user in users)
                            {
                                scope.Transaction.Begin();
                                //user.Username = model.UserName;
                                user.Email = model.Email;
                                user.IsheDonationReceiver = model.DonationReceiver;
                                user.IsheAdmin = model.Admin;
                                scope.Add(user);
                                scope.Transaction.Commit();
                            }
                            ViewData["Status"] = "User details update process completed successfully.";
                            return View("Status");
                        }
                        //ModelState.Remove("Password");
                        //ModelState.Remove("Confirm password");
                    }
                    return View();
                }
                ViewData["Status"] = "You are not authorized to do this operation";
                return View("Status");
            }
            ViewData["Status"] = "Your session has been expired, please login again and try.";
            return View("Status");
        }

        [HttpPost]
        public string Viewuserinfo(string userid)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var scope = ObjectScopeProvider1.GetNewObjectScope();
                    if (CheckAdminauthorization(scope, User.Identity.Name))
                    {
                        List<User> users = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                            where c.Id.Equals(userid)
                                            select c).ToList();
                        if (users.Count > 0)
                        {
                            string output = @"<td></td><td>" + users[0].Username + "</td><td>" + users[0].Email +
                                            "</td><td>";
                            if (users[0].IsheDonationReceiver)
                                output += "Yes";
                            else
                                output += "No";
                            output += "</td><td>";
                            if (users[0].IsheAdmin)
                                output += "Yes";
                            else
                                output += "No";
                            output +=
                                @"</td><td style='width: 150px'><span class='delete_button'><img src='/Images/ico-delete.gif' />
                                        delete</span></td><td style='width: 100px'><span class='edit_button' href='/Controlpanel/edituser/" +
                                users[0].Id + "'><img src='/Images/edit.gif' />edit</span></td>";
                            return output;
                        }
                    }
                    return "You are not authorized to do this operation";
                }
                return "You session has been ended, please login again to continue.";
            }
            catch (Exception)
            {
                return "";
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult ImportfromExcel()
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                ViewData["Status"] = string.Empty;
                if (CheckAdminauthorization(scope, User.Identity.Name))
                    return View();
                ViewData["Status"] = "You are not authorized to do this operation";
                return View("PartialViewStatus");
            }
            return RedirectToAction("LogOn", "Account");
        }

        [Authorize]
        [HttpPost]
        public ActionResult ImportfromExcel(ExcelModels model, HttpPostedFileBase excelFile, HttpPostedFileBase signatureFile)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewData["Status"] = string.Empty;
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (CheckAdminauthorization(scope, User.Identity.Name))
                {
                    if (ModelState.IsValid)
                    {
                        OleDbConnection connection = null;
                        try
                        {
                            // saving image here
                            SignatureImage signature;
                            try
                            {
                                signature = new SignatureImage { Filename = model.SignatureFile.FileName };
                                Stream fileStream = model.SignatureFile.InputStream;
                                int fileLength = model.SignatureFile.ContentLength;
                                signature.Filedata = new byte[fileLength];
                                fileStream.Read(signature.Filedata, 0, fileLength);
                                signature.MimeType = model.SignatureFile.ContentType;
                                signature.ID = Guid.NewGuid();
                            }
                            catch
                            {
                                signature = null;
                            }
                            if (signature != null)
                            {
                                string directory = Server.MapPath("/App_Data");
                                if (Directory.Exists(directory))
                                    Directory.CreateDirectory(directory);
                                string filePath = Path.Combine(directory, excelFile.FileName);
                                if (System.IO.File.Exists(filePath))
                                    System.IO.File.Delete(filePath);
                                excelFile.SaveAs(filePath);
                                string connectionString = string.Empty;
                                if (Path.GetExtension(filePath) == ".xls")
                                    connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath +
                                                       ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                                else if (Path.GetExtension(filePath) == ".xlsx")
                                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath +
                                                       ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                                bool isReceptsGenerated = false;
                                var groupId = DateTime.Now.ToString("%s%MM%mm%yy%dd%HH");
                                if (!string.IsNullOrEmpty(connectionString))
                                {
                                    connection = new OleDbConnection(connectionString);
                                    var cmd = new OleDbCommand { CommandType = CommandType.Text, Connection = connection };
                                    var dAdapter = new OleDbDataAdapter(cmd);
                                    var dtExcelRecords = new DataTable();
                                    connection.Open();
                                    DataTable dtExcelSheetName = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
                                                                                                null);
                                    if (dtExcelSheetName != null && dtExcelSheetName.Rows.Count > 0)
                                    {
                                        string getExcelSheetName = dtExcelSheetName.Rows[0]["Table_Name"].ToString();
                                        cmd.CommandText = "SELECT * FROM [" + getExcelSheetName + "]";
                                        dAdapter.SelectCommand = cmd;
                                        dAdapter.Fill(dtExcelRecords);

                                        // Started to create recepts now)
                                        foreach (DataRow dataRow in dtExcelRecords.Rows)
                                        {
                                            string receiptType = string.Empty,
                                                   firstname = string.Empty,
                                                   mi = string.Empty,
                                                   lastname = string.Empty,
                                                   address = string.Empty,
                                                   address2 = string.Empty,
                                                   city = string.Empty,
                                                   state = string.Empty,
                                                   zipcode = string.Empty,
                                                   email = string.Empty,
                                                   contact = string.Empty,
                                                   datereceived = string.Empty,
                                                   issueddate = string.Empty,
                                                   donationamount = string.Empty,
                                                   donationAmountinwords = string.Empty,
                                                   recurringDetails = string.Empty,
                                                   merchandiseItem = string.Empty,
                                                   quantity = string.Empty,
                                                   value = string.Empty,
                                                   servicetype = string.Empty,
                                                   hoursServed = string.Empty,
                                                   rateperhour = string.Empty,
                                                   fmvvalue = string.Empty,
                                                   modeOfPayment = string.Empty,
                                                   receivedBy = string.Empty;
                                            if (dataRow[0] != null && !string.IsNullOrEmpty(dataRow[0].ToString()))
                                                receiptType = dataRow[0].ToString();
                                            if (dataRow[1] != null && !string.IsNullOrEmpty(dataRow[1].ToString()))
                                                firstname = dataRow[1].ToString();
                                            if (dataRow[2] != null && !string.IsNullOrEmpty(dataRow[2].ToString()))
                                                mi = dataRow[2].ToString();
                                            if (dataRow[3] != null && !string.IsNullOrEmpty(dataRow[3].ToString()))
                                                lastname = dataRow[3].ToString();
                                            if (dataRow[4] != null && !string.IsNullOrEmpty(dataRow[4].ToString()))
                                                address = dataRow[4].ToString();
                                            if (dataRow[5] != null && !string.IsNullOrEmpty(dataRow[5].ToString()))
                                                address2 = dataRow[5].ToString();
                                            if (dataRow[6] != null && !string.IsNullOrEmpty(dataRow[6].ToString()))
                                                city = dataRow[6].ToString();
                                            if (dataRow[7] != null && !string.IsNullOrEmpty(dataRow[7].ToString()))
                                                state = dataRow[7].ToString();
                                            if (dataRow[8] != null && !string.IsNullOrEmpty(dataRow[8].ToString()))
                                                zipcode = dataRow[8].ToString();
                                            if (dataRow[9] != null && !string.IsNullOrEmpty(dataRow[9].ToString()))
                                                email = dataRow[9].ToString();
                                            if (dataRow[10] != null && !string.IsNullOrEmpty(dataRow[10].ToString()))
                                                contact = dataRow[10].ToString();
                                            if (dataRow[11] != null && !string.IsNullOrEmpty(dataRow[11].ToString()))
                                                datereceived = dataRow[11].ToString();
                                            if (dataRow[12] != null && !string.IsNullOrEmpty(dataRow[12].ToString()))
                                                issueddate = dataRow[12].ToString();
                                            if (dataRow[13] != null && !string.IsNullOrEmpty(dataRow[13].ToString()))
                                                donationamount = dataRow[13].ToString();
                                            if (dataRow[14] != null && !string.IsNullOrEmpty(dataRow[14].ToString()))
                                                donationAmountinwords = dataRow[14].ToString();
                                            if (dataRow[15] != null && !string.IsNullOrEmpty(dataRow[15].ToString()))
                                                recurringDetails = dataRow[15].ToString();
                                            if (dataRow[16] != null && !string.IsNullOrEmpty(dataRow[16].ToString()))
                                                merchandiseItem = dataRow[16].ToString();
                                            if (dataRow[17] != null && !string.IsNullOrEmpty(dataRow[17].ToString()))
                                                quantity = dataRow[17].ToString();
                                            if (dataRow[18] != null && !string.IsNullOrEmpty(dataRow[18].ToString()))
                                                value = dataRow[18].ToString();
                                            if (dataRow[19] != null && !string.IsNullOrEmpty(dataRow[19].ToString()))
                                                servicetype = dataRow[19].ToString();
                                            if (dataRow[20] != null && !string.IsNullOrEmpty(dataRow[20].ToString()))
                                                hoursServed = dataRow[20].ToString();
                                            if (dataRow[21] != null && !string.IsNullOrEmpty(dataRow[21].ToString()))
                                                rateperhour = dataRow[21].ToString();
                                            if (dataRow[22] != null && !string.IsNullOrEmpty(dataRow[22].ToString()))
                                                fmvvalue = dataRow[22].ToString();
                                            if (dataRow[23] != null && !string.IsNullOrEmpty(dataRow[23].ToString()))
                                                modeOfPayment = dataRow[23].ToString();
                                            if (dataRow[24] != null && !string.IsNullOrEmpty(dataRow[24].ToString()))
                                                receivedBy = dataRow[24].ToString();
                                            if (!string.IsNullOrEmpty(receiptType) && !string.IsNullOrEmpty(firstname) &&
                                                !string.IsNullOrEmpty(receivedBy))
                                            {
                                                List<User> receiver =
                                                    (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                                     where c.Username.ToLower().Equals(receivedBy.ToLower().Trim())
                                                     select c).ToList();
                                                if (receiver.Count > 0)
                                                {
                                                    var receipt = new Receipt
                                                                      {
                                                                          ReceiptNumber = Utilities.GenerateReceiptId(),
                                                                          FirstName = firstname,
                                                                          Mi = mi,
                                                                          LastName = lastname,
                                                                          Address = address,
                                                                          Address2 = address2,
                                                                          City = city,
                                                                          State = state,
                                                                          ZipCode = zipcode,
                                                                          Email = email,
                                                                          Contact = contact,
                                                                          IssuedDate = Convert.ToDateTime(issueddate),
                                                                          DonationReceiver = receiver[0],
                                                                          GroupId = groupId
                                                                      };
                                                    receipt.SignatureImage = signature;
                                                    if (receiptType.ToLower().Trim() != "recurring receipt")
                                                        receipt.DateReceived = Convert.ToDateTime(datereceived);
                                                    switch (receiptType.ToLower().Trim())
                                                    {
                                                        case "regular receipt":
                                                        case "recurring receipt":
                                                            {
                                                                if (string.IsNullOrEmpty(modeOfPayment) ||
                                                                    string.IsNullOrEmpty(donationamount) ||
                                                                    string.IsNullOrEmpty(donationAmountinwords))
                                                                    continue;
                                                                receipt.DonationAmount = donationamount;
                                                                receipt.DonationAmountinWords = donationAmountinwords;
                                                                switch (modeOfPayment.ToLower().Trim())
                                                                {
                                                                    case "cash":
                                                                        {
                                                                            receipt.ModeOfPayment =
                                                                                ModeOfPayment.Cash;
                                                                            break;
                                                                        }
                                                                    case "cheque":
                                                                        {
                                                                            receipt.ModeOfPayment =
                                                                                ModeOfPayment.
                                                                                    Cheque;
                                                                            break;
                                                                        }
                                                                    case "goods":
                                                                        {
                                                                            receipt.ModeOfPayment =
                                                                                ModeOfPayment.
                                                                                    Goods;
                                                                            break;
                                                                        }
                                                                    case "online":
                                                                        {
                                                                            receipt.ModeOfPayment =
                                                                                ModeOfPayment.
                                                                                    Online;
                                                                            break;
                                                                        }
                                                                    case "mobile":
                                                                        {
                                                                            receipt.ModeOfPayment =
                                                                                ModeOfPayment.
                                                                                    Mobile;
                                                                            break;
                                                                        }
                                                                }
                                                                break;
                                                            }
                                                    }
                                                    switch (receiptType.ToLower().Trim())
                                                    {
                                                        case "regular receipt":
                                                            {
                                                                receipt.ReceiptType = ReceiptType.GeneralReceipt;
                                                                break;
                                                            }
                                                        case "recurring receipt":
                                                            {
                                                                receipt.ReceiptType = ReceiptType.RecurringReceipt;
                                                                string[] recurrenceDetials = recurringDetails.Split(')');
                                                                foreach (string recurrenceDetial in recurrenceDetials)
                                                                {
                                                                    try
                                                                    {
                                                                        var values =
                                                                            recurrenceDetial.Replace("(", "").Split('-');
                                                                        if (values.Count() > 2)
                                                                        {
                                                                            var recurring = new RecurringDetails
                                                                                                {
                                                                                                    DueDate =
                                                                                                        Convert.
                                                                                                        ToDateTime(
                                                                                                            values[0]),
                                                                                                    Amount = values[2]
                                                                                                };
                                                                            switch (values[1].ToLower().Trim())
                                                                            {
                                                                                case "cash":
                                                                                    {
                                                                                        recurring.ModeOfPayment =
                                                                                            ModeOfPayment.Cash;
                                                                                        break;
                                                                                    }
                                                                                case "cheque":
                                                                                    {
                                                                                        recurring.ModeOfPayment =
                                                                                            ModeOfPayment.Cheque;
                                                                                        break;
                                                                                    }
                                                                                case "goods":
                                                                                    {
                                                                                        recurring.ModeOfPayment =
                                                                                            ModeOfPayment.Goods;
                                                                                        break;
                                                                                    }
                                                                                case "online":
                                                                                    {
                                                                                        recurring.ModeOfPayment =
                                                                                            ModeOfPayment.Online;
                                                                                        break;
                                                                                    }
                                                                                case "mobile":
                                                                                    {
                                                                                        recurring.ModeOfPayment =
                                                                                            ModeOfPayment.Mobile;
                                                                                        break;
                                                                                    }
                                                                            }
                                                                            receipt.RecurringDetails.Add(recurring);
                                                                        }
                                                                    }
                                                                    catch (Exception)
                                                                    {
                                                                        continue;
                                                                    }
                                                                }
                                                                break;
                                                            }
                                                        case "merchandise receipt":
                                                            {
                                                                if (string.IsNullOrEmpty(merchandiseItem) ||
                                                                    string.IsNullOrEmpty(value) ||
                                                                    string.IsNullOrEmpty(quantity))
                                                                    continue;
                                                                receipt.ReceiptType = ReceiptType.MerchandiseReceipt;
                                                                receipt.MerchandiseItem = merchandiseItem;
                                                                receipt.Quantity = quantity;
                                                                receipt.FmvValue = value;
                                                                break;
                                                            }
                                                        case "services receipt":
                                                            {
                                                                if (string.IsNullOrEmpty(servicetype) ||
                                                                    string.IsNullOrEmpty(hoursServed) ||
                                                                    string.IsNullOrEmpty(rateperhour) ||
                                                                    string.IsNullOrEmpty(fmvvalue))
                                                                    continue;
                                                                receipt.ReceiptType = ReceiptType.ServicesReceipt;
                                                                receipt.ServiceType = servicetype;
                                                                try
                                                                {
                                                                    receipt.HoursServed = Convert.ToInt32(hoursServed);
                                                                }
                                                                catch (Exception)
                                                                {
                                                                    continue;
                                                                }
                                                                receipt.RatePerHrOrDay = rateperhour;
                                                                receipt.FmvValue = fmvvalue;
                                                                break;
                                                            }
                                                        default:
                                                            {
                                                                continue;
                                                            }
                                                    }
                                                    scope.Transaction.Begin();
                                                    scope.Add(receipt);
                                                    scope.Transaction.Commit();
                                                    isReceptsGenerated = true;
                                                    Thread.Sleep(100);
                                                }
                                            }
                                        }
                                    }
                                    connection.Close();
                                }
                                if (isReceptsGenerated)
                                {
                                    ViewData["ReceiptID"] = groupId;
                                    return View("Printoptions");
                                }
                                else
                                {
                                    ViewData["Status"] =
                                        "Excel import process completed successfully. There is some invalid entry found in your excel file.";
                                    return View();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ViewData["Status"] = "Unable to read data from file, please input your data with the specified format.";
                            ModelState.AddModelError("", "Unable to import from excel due to " + ex.Message);
                        }
                        finally
                        {
                            try
                            {
                                if (connection != null)
                                {
                                    connection.Close();
                                    connection.Dispose();
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    // If we got this far, something failed, redisplay form
                    return View(model);
                }
                ViewData["Status"] = "You are not authorized to do this operation";
                return View("PartialViewStatus");
            }
            ViewData["Status"] = "Your session has been expired, please login again and try.";
            return View("PartialViewStatus");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Reports()
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (CheckAdminauthorization(scope, User.Identity.Name))
                    return View();
                ViewData["Status"] = "You are not authorized to do this operation";
                return View("PartialViewStatus");
            }
            return RedirectToAction("LogOn", "Account");
        }

        public string Deleteuser(string userid)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var scope = ObjectScopeProvider1.GetNewObjectScope();
                    if (CheckAdminauthorization(scope, User.Identity.Name))
                    {
                        List<User> users = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                            where c.Id.Equals(userid)
                                            select c).ToList();
                        if (users.Count > 0)
                        {
                            foreach (var user in users)
                            {
                                Membership.DeleteUser(user.Username);
                                /*
                                scope.Transaction.Begin();
                                scope.Remove(user);
                                scope.Transaction.Commit(); */
                            }
                        }
                        return "removed";
                    }
                    return "You are not authorized to do this operation";
                }
                return "You session has been ended, please login again to continue.";
            }
            catch (Exception)
            {
                return "failed";
            }
        }

        public IFormsAuthenticationService FormsService { get; set; }

        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }

        private bool CheckAdminauthorization(IObjectScope scope, string username)
        {
            List<User> users = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                where c.Username.ToLower().Equals(username.ToLower())
                                select c).ToList();
            if (users.Count > 0)
            {
                ViewData["IsheAdmin"] = users[0].IsheAdmin;
                ViewData["IsheDonationReceiver"] = users[0].IsheDonationReceiver;
                if (users[0].IsheAdmin)
                    return true;
            }
            ViewData["IsheAdmin"] = false;
            ViewData["IsheDonationReceiver"] = false;
            return false;
        }

        #region Status Codes

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        #endregion Status Codes
    }
}