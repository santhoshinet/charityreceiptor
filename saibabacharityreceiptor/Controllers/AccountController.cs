using System;
using System.Collections.Generic;
using System.Linq;
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

                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
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
            Response.Redirect("/index.html");
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