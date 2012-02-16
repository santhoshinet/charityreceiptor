using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using saibabacharityreceiptor.Models;
using saibabacharityreceiptorDL;

namespace saibabacharityreceiptor.Controllers
{
    [HandleError]
    public class AccountController : Controller
    {
        public IFormsAuthenticationService FormsService { get; set; }

        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }

        [HttpGet]
        public ActionResult Index()
        {
            Response.Redirect("/Index.html");
            return null;
        }

        [HttpGet]
        public ActionResult LogOn()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogOnPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                List<User> logOnFailures = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                            where c.Username.ToLower().Trim().Equals(model.UserName.ToLower().Trim())
                                            select c).ToList();
                if (logOnFailures.Count > 0 && logOnFailures[0].Failcount > 2 && DateTime.Now.Subtract(logOnFailures[0].Lasttriedtime).TotalMinutes < 10)
                {
                    ModelState.AddModelError("", "The authentication is failed in three consequence times, please wait for ten minites and try again.");
                    return View(model);
                }
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    if (logOnFailures.Count > 0)
                    {
                        var logOnFailure = logOnFailures[0];
                        scope.Transaction.Begin();
                        logOnFailure.Failcount = 0;
                        scope.Add(logOnFailure);
                        scope.Transaction.Commit();
                    }
                    FormsAuthentication.SetAuthCookie(model.UserName, true);

                    /*var ticket = new FormsAuthenticationTicket(1, model.UserName, DateTime.Now, DateTime.Now.AddDays(30), true, "");
                    var strEncryptedTicket = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, strEncryptedTicket);
                    Response.Cookies.Add(cookie); */

                    HttpCookie myCookie = FormsAuthentication.GetAuthCookie(model.UserName, true);
                    myCookie.Domain = "shirdisaibabaaz.org";
                    myCookie.Path = "/";
                    myCookie.Expires = DateTime.Now.AddDays(1);
                    Response.AppendCookie(myCookie);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("home", "Controlpanel");
                }
                if (logOnFailures.Count > 0)
                {
                    var logOnFailure = logOnFailures[0];
                    scope.Transaction.Begin();
                    if (logOnFailure.Failcount == 0)
                        logOnFailure.Lasttriedtime = DateTime.Now;
                    logOnFailure.Failcount += 1;
                    scope.Add(logOnFailure);
                    scope.Transaction.Commit();
                }
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Response.Redirect("/LogOn");
            return null;
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded = false;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    if (currentUser != null)
                        changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        public string Seed()
        {
            CreateUser("santhosh", "santhoshonet@gmail.com");
            return "done";
        }

        private static void CreateUser(string username, string email)
        {
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            List<User> users = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                where c.Username.ToLower().Trim().Equals(username.Trim().ToLower())
                                select c).ToList();
            Membership.DeleteUser(username);
            Membership.CreateUser(username, "password@123", email);
            if (users.Count == 0)
            {
                scope.Transaction.Begin();
                var user = new User
                {
                    Email = email,
                    Failcount = 0,
                    IsheAdmin = true,
                    IsheDonationReceiver = true,
                    Username = username.Trim().ToLower()
                };
                scope.Add(user);
                scope.Transaction.Commit();
            }
            else
            {
                foreach (var user in users)
                {
                    scope.Transaction.Begin();
                    user.IsheDonationReceiver = true;
                    user.IsheAdmin = true;
                    scope.Add(user);
                    scope.Transaction.Commit();
                }
            }
        }
    }
}