using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using saibabacharityreceiptor.Models;
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
                                    orderby c.DateReceived
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
                                                                                  Name = receipt.FirstName,
                                                                                  OnDateTime = receipt.DateReceived,
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
                                    orderby c.DateReceived
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
                                                                                  Name = receipt.FirstName,
                                                                                  OnDateTime = receipt.DateReceived,
                                                                                  ReceiptNumber = receipt.ReceiptNumber,
                                                                                  RecurringDates = receipt.RecurringDates
                                                                              }).ToList();
                    ViewData["pageIndex"] = pageIndex;
                    if (pageIndex <= 0)
                        ViewData["HasPrevious"] = false;
                    else
                        ViewData["HasPrevious"] = true;

                    var totalrecords = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                        where c.ReceiptType.Equals(ReceiptType.RecurringReceipt)
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
        public ActionResult MerchandiseReceipts(int pageIndex)
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (Checkauthorization(scope, User.Identity.Name))
                {
                    var receipts = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                    where c.ReceiptType.Equals(ReceiptType.MerchandiseReceipt)
                                    orderby c.DateReceived
                                    select c).Skip(pageIndex * NoOfRecordsPerPage).Take(NoOfRecordsPerPage).ToList();
                    var localRegularReceipts = receipts.Select(receipt => new LocalMerchandiseReceipt
                                                                              {
                                                                                  Address = receipt.Address,
                                                                                  Contact = receipt.Contact,
                                                                                  DonationReceiverName = receipt.DonationReceiver.Username,
                                                                                  Email = receipt.Email,
                                                                                  Name = receipt.FirstName,
                                                                                  OnDateTime = receipt.DateReceived,
                                                                                  ReceiptNumber = receipt.ReceiptNumber,
                                                                                  MerchandiseItem = receipt.MerchandiseItem,
                                                                                  Value = receipt.FmvValue
                                                                              }).ToList();
                    ViewData["pageIndex"] = pageIndex;
                    if (pageIndex <= 0)
                        ViewData["HasPrevious"] = false;
                    else
                        ViewData["HasPrevious"] = true;

                    var totalrecords = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                        where c.ReceiptType.Equals(ReceiptType.MerchandiseReceipt)
                                        select c).Count();

                    if (totalrecords > (pageIndex + 1) * NoOfRecordsPerPage)
                        ViewData["HasNext"] = true;
                    else
                        ViewData["HasNext"] = false;

                    ViewData["MerchandiseReceipts"] = localRegularReceipts;
                    return View();
                }
                ViewData["Status"] = "You are not authorized to do this operation";
                return View("PartialViewStatus");
            }
            return RedirectToAction("LogOn", "Account");
        }

        [Authorize]
        [HttpGet]
        public ActionResult ServicesReceipts(int pageIndex)
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (Checkauthorization(scope, User.Identity.Name))
                {
                    var receipts = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                    where c.ReceiptType.Equals(ReceiptType.ServicesReceipt)
                                    orderby c.DateReceived
                                    select c).Skip(pageIndex * NoOfRecordsPerPage).Take(NoOfRecordsPerPage).ToList();
                    var localServicesReceipts = receipts.Select(receipt => new LocalServicesReceipt
                    {
                        Address = receipt.Address,
                        Contact = receipt.Contact,
                        DonationReceiverName = receipt.DonationReceiver.Username,
                        Email = receipt.Email,
                        Name = receipt.FirstName,
                        OnDateTime = receipt.DateReceived,
                        ReceiptNumber = receipt.ReceiptNumber,
                        HoursServed = receipt.HoursServed
                    }).ToList();
                    ViewData["pageIndex"] = pageIndex;
                    if (pageIndex <= 0)
                        ViewData["HasPrevious"] = false;
                    else
                        ViewData["HasPrevious"] = true;

                    var totalrecords = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                        where c.ReceiptType.Equals(ReceiptType.ServicesReceipt)
                                        select c).Count();

                    if (totalrecords > (pageIndex + 1) * NoOfRecordsPerPage)
                        ViewData["HasNext"] = true;
                    else
                        ViewData["HasNext"] = false;

                    ViewData["ServicesReceipts"] = localServicesReceipts;
                    return View();
                }
                ViewData["Status"] = "You are not authorized to do this operation";
                return View("PartialViewStatus");
            }
            return RedirectToAction("LogOn", "Account");
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

        [Authorize]
        [HttpGet]
        public ActionResult SearchReceipts()
        {
            ViewData["selectedTypeOfReceipt"] = string.Empty;
            ViewData["SeachReceipts"] = new List<ReceiptData>();
            ViewData["typeofreceipts"] = new List<string> { "All", "Regular", "Recurrence", "Merchandise", "Service" };
            return View(new SearchModel { PageIndex = 0, EndDate = DateTime.Now, StartDate = DateTime.Now.AddMonths(-1) });
        }

        [Authorize]
        [HttpPost]
        public ActionResult SearchReceipts(SearchModel searchModel)
        {
            ViewData["selectedTypeOfReceipt"] = string.Empty;
            ViewData["SeachReceipts"] = new List<ReceiptData>();
            ViewData["typeofreceipts"] = new List<string> { "All", "Regular", "Recurrence", "Merchandise", "Service" };
            if (ModelState.IsValid)
            {
                ViewData["selectedTypeOfReceipt"] = searchModel.TypeOfReceipt;
                ReceiptType receiptType = ReceiptType.GeneralReceipt;
                switch (searchModel.TypeOfReceipt)
                {
                    case "Regular":
                        {
                            receiptType = ReceiptType.GeneralReceipt;
                            break;
                        }
                    case "Recurrence":
                        {
                            receiptType = ReceiptType.RecurringReceipt;
                            break;
                        }
                    case "Merchandise":
                        {
                            receiptType = ReceiptType.MerchandiseReceipt;
                            break;
                        }
                    case "Service":
                        {
                            receiptType = ReceiptType.ServicesReceipt;
                            break;
                        }
                    case "All":
                        {
                            break;
                        }
                }
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                List<Receipt> receipts;
                if (searchModel.TypeOfReceipt != "All")
                    receipts = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                where c.ReceiptType.Equals(receiptType)
                                orderby c.DateReceived
                                select c).Skip(searchModel.PageIndex * searchModel.Maxrecordsperpage).Take(searchModel.Maxrecordsperpage).ToList();
                else
                    receipts = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                orderby c.DateReceived
                                select c).Skip(searchModel.PageIndex * searchModel.Maxrecordsperpage).Take(searchModel.Maxrecordsperpage).ToList();
                var localRegularReceipts = receipts.Select(receipt => new ReceiptData
                {
                    ReceiptNumber = receipt.ReceiptNumber,
                    FirstName = receipt.FirstName,
                    Mi = receipt.Mi,
                    LastName = receipt.LastName,
                    DateReceived = receipt.DateReceived,
                    ReceiptType = receipt.ReceiptType.ToString()
                }).ToList();
                ViewData["pageIndex"] = searchModel.PageIndex;
                if (searchModel.PageIndex <= 0)
                    ViewData["HasPrevious"] = false;
                else
                    ViewData["HasPrevious"] = true;
                var totalrecords = 0;
                if (searchModel.TypeOfReceipt != "All")
                    totalrecords = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                    where c.ReceiptType.Equals(receiptType)
                                    select c).Count();
                else
                    totalrecords = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                    select c).Count();
                if (totalrecords > (searchModel.PageIndex + 1) * NoOfRecordsPerPage)
                    ViewData["HasNext"] = true;
                else
                    ViewData["HasNext"] = false;

                ViewData["SeachReceipts"] = localRegularReceipts;
                return View();
            }
            return View();
        }
    }
}