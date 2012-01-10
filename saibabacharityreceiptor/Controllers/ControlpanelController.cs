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
            if (User.Identity.IsAuthenticated)
                return View();
            return RedirectToAction("Logon", "Account");
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddUser(RegisterModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.ObjectScope();
                if (Checkauthorization(scope, User.Identity.Name))
                {
                    if (ModelState.IsValid)
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
                            FormsAuthentication.SetAuthCookie(model.UserName, true /* createPersistentCookie */);
                            ViewData["Status"] = "User added successfully.";
                            return View("Status");
                            //return RedirectToAction("Index", "Home");
                        }
                        ModelState.AddModelError("", ErrorCodeToString(createStatus));
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
                if (Checkauthorization(scope, User.Identity.Name))
                    return View();
                ViewData["Status"] = "You are not authorized to do this operation";
                return View("PartialViewStatus");
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
                if (Checkauthorization(scope, User.Identity.Name))
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

        [Authorize]
        [HttpGet]
        public ActionResult Edituser(string uid)
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (Checkauthorization(scope, User.Identity.Name))
                {
                    List<User> users = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                        where c.Id.Equals(uid)
                                        select c).ToList();
                    if (users.Count > 0)
                        return
                            View(new RegisterModel
                                     {
                                         Admin = users[0].IsheAdmin,
                                         DonationReceiver = users[0].IsheDonationReceiver,
                                         Email = users[0].Email,
                                         UserId = users[0].Id,
                                         UserName = users[0].Username
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
        public ActionResult Edituser(RegisterModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (Checkauthorization(scope, User.Identity.Name))
                {
                    if (!string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.UserName) && !string.IsNullOrEmpty(model.UserId))
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
                    ModelState.Remove("Password");
                    ModelState.Remove("Confirm password");
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
                    if (Checkauthorization(scope, User.Identity.Name))
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
                if (Checkauthorization(scope, User.Identity.Name))
                    return View();
                ViewData["Status"] = "You are not authorized to do this operation";
                return View("PartialViewStatus");
            }
            return RedirectToAction("LogOn", "Account");
        }

        [Authorize]
        [HttpPost]
        public ActionResult ImportfromExcel(ExcelModels model, HttpPostedFileBase excelFile)
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (Checkauthorization(scope, User.Identity.Name))
                {
                    if (ModelState.IsValid)
                    {
                        OleDbConnection connection = null;
                        try
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
                                connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                            else if (Path.GetExtension(filePath) == ".xlsx")
                                connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            if (!string.IsNullOrEmpty(connectionString))
                            {
                                connection = new OleDbConnection(connectionString);
                                var cmd = new OleDbCommand { CommandType = CommandType.Text, Connection = connection };
                                var dAdapter = new OleDbDataAdapter(cmd);
                                var dtExcelRecords = new DataTable();
                                connection.Open();
                                DataTable dtExcelSheetName = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                if (dtExcelSheetName != null && dtExcelSheetName.Rows.Count > 0)
                                {
                                    string getExcelSheetName = dtExcelSheetName.Rows[0]["Table_Name"].ToString();
                                    cmd.CommandText = "SELECT * FROM [" + getExcelSheetName + "]";
                                    dAdapter.SelectCommand = cmd;
                                    dAdapter.Fill(dtExcelRecords);

                                    // Started to create recepts now
                                    foreach (DataRow dataRow in dtExcelRecords.Rows)
                                    {
                                        string receiptType = string.Empty,
                                               name = string.Empty,
                                               address = string.Empty,
                                               email = string.Empty,
                                               contact = string.Empty,
                                               donationamount = string.Empty,
                                               donationAmountinwords = string.Empty,
                                               merchandiseItem = string.Empty,
                                               value = string.Empty,
                                               hoursServed = string.Empty,
                                               recurringDates = string.Empty,
                                               modeOfPayment = string.Empty,
                                               receivedBy = string.Empty;
                                        if (dataRow[0] != null && !string.IsNullOrEmpty(dataRow[0].ToString()))
                                            receiptType = dataRow[0].ToString();
                                        if (dataRow[1] != null && !string.IsNullOrEmpty(dataRow[1].ToString()))
                                            name = dataRow[1].ToString();
                                        if (dataRow[2] != null && !string.IsNullOrEmpty(dataRow[2].ToString()))
                                            address = dataRow[2].ToString();
                                        if (dataRow[3] != null && !string.IsNullOrEmpty(dataRow[3].ToString()))
                                            email = dataRow[3].ToString();
                                        if (dataRow[4] != null && !string.IsNullOrEmpty(dataRow[4].ToString()))
                                            contact = dataRow[4].ToString();
                                        if (dataRow[5] != null && !string.IsNullOrEmpty(dataRow[5].ToString()))
                                            donationamount = dataRow[5].ToString();
                                        if (dataRow[6] != null && !string.IsNullOrEmpty(dataRow[6].ToString()))
                                            donationAmountinwords = dataRow[6].ToString();
                                        if (dataRow[7] != null && !string.IsNullOrEmpty(dataRow[7].ToString()))
                                            merchandiseItem = dataRow[7].ToString();
                                        if (dataRow[8] != null && !string.IsNullOrEmpty(dataRow[8].ToString()))
                                            value = dataRow[8].ToString();
                                        if (dataRow[9] != null && !string.IsNullOrEmpty(dataRow[9].ToString()))
                                            hoursServed = dataRow[9].ToString();
                                        if (dataRow[10] != null && !string.IsNullOrEmpty(dataRow[10].ToString()))
                                            recurringDates = dataRow[10].ToString();
                                        if (dataRow[11] != null && !string.IsNullOrEmpty(dataRow[11].ToString()))
                                            modeOfPayment = dataRow[11].ToString();
                                        if (dataRow[12] != null && !string.IsNullOrEmpty(dataRow[12].ToString()))
                                            receivedBy = dataRow[12].ToString();
                                        if (!string.IsNullOrEmpty(receiptType) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(receivedBy))
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
                                                                      Name = name,
                                                                      Address = address,
                                                                      Email = email,
                                                                      OnDateTime = DateTime.Now,
                                                                      Contact = contact,
                                                                      DonationReceiver = receiver[0]
                                                                  };
                                                switch (receiptType.ToLower().Trim())
                                                {
                                                    case "regular receipt":
                                                    case "recurring payment receipt":
                                                        {
                                                            if (string.IsNullOrEmpty(modeOfPayment) || string.IsNullOrEmpty(donationamount) || string.IsNullOrEmpty(donationAmountinwords))
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
                                                    case "recurring payment receipt":
                                                        {
                                                            string[] dates = recurringDates.Split(',');
                                                            foreach (string date in dates)
                                                            {
                                                                try
                                                                {
                                                                    receipt.RecurringDates.Add(Convert.ToDateTime(date));
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
                                                            if (string.IsNullOrEmpty(merchandiseItem) || string.IsNullOrEmpty(value))
                                                                continue;
                                                            receipt.MerchandiseItem = merchandiseItem;
                                                            receipt.Value = value;
                                                            break;
                                                        }
                                                    case "services receipt":
                                                        {
                                                            if (string.IsNullOrEmpty(merchandiseItem) || string.IsNullOrEmpty(hoursServed))
                                                                continue;
                                                            receipt.MerchandiseItem = merchandiseItem;
                                                            try
                                                            {
                                                                receipt.HoursServed = Convert.ToInt32(hoursServed);
                                                            }
                                                            catch (Exception)
                                                            {
                                                                continue;
                                                            }
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
                                                Thread.Sleep(500);
                                            }
                                        }
                                    }
                                }
                                connection.Close();
                            }
                            ViewData["Status"] = "Excel import process completed successfully.";
                            return View("PartialViewStatus");
                        }
                        catch (Exception ex)
                        {
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

        private bool Checkauthorization(IObjectScope scope, string username)
        {
            List<User> users = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                where c.Username.ToLower().Equals(username.ToLower())
                                select c).ToList();
            if (users.Count > 0 && users[0].IsheAdmin)
            {
                ViewData["IsheAdmin"] = users[0].IsheAdmin;
                ViewData["IsheDonationReceiver"] = users[0].IsheDonationReceiver;
                return true;
            }
            return false;
        }

        public string Deleteuser(string userid)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var scope = ObjectScopeProvider1.GetNewObjectScope();
                    if (Checkauthorization(scope, User.Identity.Name))
                    {
                        List<User> users = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                            where c.Id.Equals(userid)
                                            select c).ToList();
                        if (users.Count > 0)
                        {
                            foreach (var user in users)
                            {
                                Membership.DeleteUser(user.Username);
                                scope.Transaction.Begin();
                                scope.Remove(user);
                                scope.Transaction.Commit();
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