using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using saibabacharityreceiptorDL;
using Telerik.OpenAccess;

namespace saibabacharityreceiptor.Controllers
{
    public class ReportsController : Controller
    {
        private const int NoOfRecordsPerPage = 20;

        [Authorize]
        [HttpGet]
        public ActionResult RegularReceipts(int pageIndex)
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (Checkauthorization(scope, User.Identity.Name))
                {
                    var receipts = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                    where c.ReceiptType.Equals(ReceiptType.GeneralReceipt)
                                    orderby c.OnDateTime
                                    select c).Skip(pageIndex * NoOfRecordsPerPage).Take(NoOfRecordsPerPage).ToList();
                    var localRegularReceipts = receipts.Select(receipt => new LocalRegularReceipt
                                                                              {
                                                                                  Address = receipt.Address,
                                                                                  Contact = receipt.Contact,
                                                                                  DonationAmount = receipt.DonationAmount,
                                                                                  DonationAmountinWords = receipt.DonationAmountinWords,
                                                                                  DonationReceiverName = receipt.DonationReceiver.Username,
                                                                                  Email = receipt.Email,
                                                                                  ModeOfPayment = receipt.ModeOfPayment,
                                                                                  Name = receipt.Name,
                                                                                  OnDateTime = receipt.OnDateTime,
                                                                                  ReceiptNumber = receipt.ReceiptNumber
                                                                              }).ToList();
                    ViewData["pageIndex"] = pageIndex;
                    if (pageIndex <= 0)
                        ViewData["HasPrevious"] = false;
                    else
                        ViewData["HasPrevious"] = true;

                    var totalrecords = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                        where c.ReceiptType.Equals(ReceiptType.GeneralReceipt)
                                        select c).Count();

                    if (totalrecords > (pageIndex + 1) * NoOfRecordsPerPage)
                        ViewData["HasNext"] = true;
                    else
                        ViewData["HasNext"] = false;

                    ViewData["RegularReceipts"] = localRegularReceipts;
                    return View();
                }
                ViewData["Status"] = "You are not authorized to do this operation";
                return View("PartialViewStatus");
            }
            return RedirectToAction("LogOn", "Account");
        }

        [Authorize]
        [HttpGet]
        public ActionResult RecurringReceipts(int pageIndex)
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (Checkauthorization(scope, User.Identity.Name))
                {
                    var receipts = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                    where c.ReceiptType.Equals(ReceiptType.RecurringReceipt)
                                    orderby c.OnDateTime
                                    select c).Skip(pageIndex * NoOfRecordsPerPage).Take(NoOfRecordsPerPage).ToList();
                    var localRegularReceipts = receipts.Select(receipt => new LocalRecurrenceReceipt
                                                                              {
                                                                                  Address = receipt.Address,
                                                                                  Contact = receipt.Contact,
                                                                                  DonationAmount = receipt.DonationAmount,
                                                                                  DonationAmountinWords = receipt.DonationAmountinWords,
                                                                                  DonationReceiverName = receipt.DonationReceiver.Username,
                                                                                  Email = receipt.Email,
                                                                                  ModeOfPayment = receipt.ModeOfPayment,
                                                                                  Name = receipt.Name,
                                                                                  OnDateTime = receipt.OnDateTime,
                                                                                  ReceiptNumber = receipt.ReceiptNumber,
                                                                                  RecurringDates = receipt.RecurringDates
                                                                              }).ToList();
                    ViewData["pageIndex"] = pageIndex;
                    if (pageIndex <= 0)
                        ViewData["HasPrevious"] = false;
                    else
                        ViewData["HasPrevious"] = true;

                    var totalrecords = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                        where c.ReceiptType.Equals(ReceiptType.GeneralReceipt)
                                        select c).Count();

                    if (totalrecords > (pageIndex + 1) * NoOfRecordsPerPage)
                        ViewData["HasNext"] = true;
                    else
                        ViewData["HasNext"] = false;

                    ViewData["RecurringReceipts"] = localRegularReceipts;
                    return View();
                }
                ViewData["Status"] = "You are not authorized to do this operation";
                return View("PartialViewStatus");
            }
            return RedirectToAction("LogOn", "Account");
        }

        [Authorize]
        [HttpGet]
        public ActionResult MerchandiseReceipts()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult ServicesReceipts()
        {
            return View();
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
    }
}