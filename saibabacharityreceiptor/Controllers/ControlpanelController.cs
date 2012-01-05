using System;
using System.Collections.Generic;
using System.Linq;
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
        //
        // GET: /Account/Register
        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult AddUser(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);
                if (createStatus == MembershipCreateStatus.Success)
                {
                    var scope = ObjectScopeProvider1.ObjectScope();
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

        [HttpGet]
        public ActionResult Home()
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (Checkauthorization(scope, User.Identity.Name))
                {
                    return View();
                }
                ViewData["Status"] = "You are not authorized to do this operation";
                return View("Status");
            }
            return RedirectToAction("LogOn", "Account");
        }

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
                return View("Status");
            }
            return RedirectToAction("LogOn", "Account");
        }

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
            return RedirectToAction("LogOn", "Account");
        }

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
                            user.Username = model.UserName;
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
            return RedirectToAction("LogOn", "Account");
        }

        private static bool Checkauthorization(IObjectScope scope, string username)
        {
            List<User> users = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                where c.Username.ToLower().Equals(username.ToLower())
                                select c).ToList();
            if (users.Count > 0 && users[0].IsheAdmin)
                return true;
            return false;
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
            }
            catch (Exception)
            {
                return "";
            }
            return "";
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
                }
                return "You are not authorized to do this operation";
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