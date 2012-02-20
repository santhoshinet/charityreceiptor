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
                    ViewData["RecordIndex"] = (pageIndex * NoOfRecordsPerPage) + 1;
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
                                                                                  FirstName = receipt.FirstName,
                                                                                  LastName = receipt.LastName,
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
                    ViewData["RecordIndex"] = (pageIndex * NoOfRecordsPerPage) + 1;
                    var receipts = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                    where c.ReceiptType.Equals(ReceiptType.RecurringReceipt)
                                    orderby c.DateReceived
                                    select c).Skip(pageIndex * NoOfRecordsPerPage).Take(NoOfRecordsPerPage).ToList();
                    var localRegularReceipts = receipts.Select(receipt => new LocalRecurrenceReceipt
                                                                              {
                                                                                  Address = receipt.Address,
                                                                                  Contact = receipt.Contact,
                                                                                  DonationReceiverName = receipt.DonationReceiver.Username,
                                                                                  Email = receipt.Email,
                                                                                  FirstName = receipt.FirstName,
                                                                                  LastName = receipt.LastName,
                                                                                  OnDateTime = receipt.DateReceived,
                                                                                  ReceiptNumber = receipt.ReceiptNumber,
                                                                                  RecurringDatas = receipt.RecurringDetails.Select(rd => new RecurrenceData { Amount = rd.Amount, Date = rd.DueDate.ToString("MM/dd/yyy"), ModeOfPayment = rd.ModeOfPayment.ToString() }).ToList()
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
                    ViewData["RecordIndex"] = (pageIndex * NoOfRecordsPerPage) + 1;
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
                                                                                  FirstName = receipt.FirstName,
                                                                                  LastName = receipt.LastName,
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
                    ViewData["RecordIndex"] = (pageIndex * NoOfRecordsPerPage) + 1;
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
                        FirstName = receipt.FirstName,
                        LastName = receipt.LastName,
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
            ViewData["IsheAdmin"] = false;
            ViewData["IsheDonationReceiver"] = false;
            return false;
        }

        [Authorize]
        [HttpGet]
        public ActionResult SearchReceipts()
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (Checkauthorization(scope, User.Identity.Name))
                {
                    ViewData["selectedTypeOfReceipt"] = string.Empty;
                    ViewData["SeachReceipts"] = new List<ReceiptData>();
                    ViewData["typeofreceipts"] = new List<string> { "All", "Regular", "Recurrence", "Merchandise", "Service" };
                    return
                        View(new SearchModel { PageIndex = 0, EndDate = DateTime.Now, StartDate = DateTime.Now });
                }
                ViewData["Status"] = "You are not authorized to do this operation";
                return View("PartialViewStatus");
            }
            return RedirectToAction("LogOn", "Account");
        }

        [Authorize]
        [HttpPost]
        public ActionResult SearchReceipts(SearchModel searchModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (Checkauthorization(scope, User.Identity.Name))
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
                        List<Receipt> receipts;
                        int maxrecordsperpage = Convert.ToInt32(searchModel.Maxrecordsperpage);
                        if (searchModel.TypeOfReceipt != "All")
                            receipts = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                        where c.ReceiptType.Equals(receiptType) && c.DateReceived >= searchModel.StartDate && c.DateReceived <= searchModel.EndDate
                                        orderby c.DateReceived
                                        select c).Skip(searchModel.PageIndex * maxrecordsperpage).Take(
                                            maxrecordsperpage).ToList();
                        else
                            receipts = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                        where c.DateReceived >= searchModel.StartDate && c.DateReceived <= searchModel.EndDate
                                        orderby c.DateReceived
                                        select c).Skip(searchModel.PageIndex * maxrecordsperpage).Take(
                                            maxrecordsperpage).ToList();
                        var localRegularReceipts = receipts.Select(receipt => new ReceiptData
                                                                                  {
                                                                                      ReceiptNumber =
                                                                                          receipt.ReceiptNumber,
                                                                                      FirstName = receipt.FirstName,
                                                                                      Mi = receipt.Mi,
                                                                                      LastName = receipt.LastName,
                                                                                      DateReceived =
                                                                                          receipt.DateReceived,
                                                                                      ReceiptType =
                                                                                          receipt.ReceiptType.ToString()
                                                                                  }).ToList();
                        ViewData["pageIndex"] = searchModel.PageIndex;
                        if (searchModel.PageIndex <= 0)
                            ViewData["HasPrevious"] = false;
                        else
                            ViewData["HasPrevious"] = true;
                        int totalrecords;
                        if (searchModel.TypeOfReceipt != "All")
                            totalrecords = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                            where c.ReceiptType.Equals(receiptType) && c.DateReceived >= searchModel.StartDate && c.DateReceived <= searchModel.EndDate
                                            select c).Count();
                        else
                            totalrecords = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                            where c.DateReceived >= searchModel.StartDate && c.DateReceived <= searchModel.EndDate
                                            select c).Count();
                        if (totalrecords > (searchModel.PageIndex + 1) * NoOfRecordsPerPage)
                            ViewData["HasNext"] = true;
                        else
                            ViewData["HasNext"] = false;

                        ViewData["SeachReceipts"] = localRegularReceipts;
                        return View(searchModel);
                    }
                    return View(searchModel);
                }
                ViewData["Status"] = "You are not authorized to do this operation";
                return View("PartialViewStatus");
            }
            return RedirectToAction("LogOn", "Account");
        }

        [Authorize]
        [HttpGet]
        public ActionResult ExporttoExcel()
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (Checkauthorization(scope, User.Identity.Name))
                {
                    ViewData["selectedTypeOfReceipt"] = string.Empty;
                    ViewData["SeachReceipts"] = new List<ReceiptData>();
                    ViewData["typeofreceipts"] = new List<string> { "All", "Regular", "Recurrence", "Merchandise", "Service" };
                    return
                        View(new ExporttoExcelModel { EndDate = DateTime.Now, StartDate = DateTime.Now });
                }
                ViewData["Status"] = "You are not authorized to do this operation";
                return View("PartialViewStatus");
            }
            return RedirectToAction("LogOn", "Account");
        }

        [Authorize]
        [HttpPost]
        public ActionResult ExporttoExcel(ExporttoExcelModel exporttoExcelModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                if (Checkauthorization(scope, User.Identity.Name))
                {
                    ViewData["selectedTypeOfReceipt"] = string.Empty;
                    ViewData["SeachReceipts"] = new List<ReceiptData>();
                    ViewData["typeofreceipts"] = new List<string> { "All", "Regular", "Recurrence", "Merchandise", "Service" };
                    if (ModelState.IsValid)
                    {
                        ViewData["selectedTypeOfReceipt"] = exporttoExcelModel.TypeOfReceipt;
                        ReceiptType receiptType = ReceiptType.GeneralReceipt;
                        switch (exporttoExcelModel.TypeOfReceipt)
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
                        List<Receipt> receipts;
                        if (exporttoExcelModel.TypeOfReceipt != "All")
                            receipts = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                        where c.ReceiptType.Equals(receiptType) && c.DateReceived >= exporttoExcelModel.StartDate && c.DateReceived <= exporttoExcelModel.EndDate
                                        orderby c.DateReceived
                                        select c).ToList();
                        else
                            receipts = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                        where c.DateReceived >= exporttoExcelModel.StartDate && c.DateReceived <= exporttoExcelModel.EndDate
                                        orderby c.DateReceived
                                        select c).ToList();

                        Response.AppendHeader("Content-Disposition", "attachment;filename=ExporttoExcel.csv");
                        Response.Charset = "UTF-8";
                        Response.ContentEncoding = System.Text.Encoding.UTF8;
                        Response.ContentType = "application/text/csv";
                        String csvOutput =
                            "Receipt ID, Receipt Type,First Name,MI,Last Name,Address 1,Address 2,City,State,Zip Code,Email,Contact,Date Received,Issued Date,Donation Amount,Donation Amount in words,Recurring Payment Details,Merchandise Item,Quantity,Value,Service Type,Hours Served,Rate per hour,FMV Value,Mode of Payment,Received By";
                        foreach (Receipt receipt in receipts)
                        {
                            double totalAmount;
                            if (receipt.ReceiptType == ReceiptType.RecurringReceipt)
                            {
                                totalAmount = receipt.RecurringDetails.Sum(recurringDetail => Convert.ToDouble(recurringDetail.Amount));
                            }
                            else
                                totalAmount = Convert.ToDouble(receipt.DonationAmount);
                            // adding data
                            csvOutput += Environment.NewLine;
                            csvOutput += receipt.ReceiptNumber;
                            csvOutput += "," + receipt.ReceiptType;
                            csvOutput += "," + receipt.FirstName;
                            csvOutput += "," + receipt.Mi;
                            csvOutput += "," + receipt.LastName;
                            csvOutput += "," + receipt.Address;
                            csvOutput += "," + receipt.Address2;
                            csvOutput += "," + receipt.City;
                            csvOutput += "," + receipt.State;
                            csvOutput += "," + receipt.ZipCode;
                            csvOutput += "," + receipt.Email;
                            csvOutput += "," + receipt.Contact;
                            if (receipt.ReceiptType == ReceiptType.RecurringReceipt)
                                csvOutput += ",";
                            else
                                csvOutput += "," + receipt.DateReceived.ToString("MM/dd/yyyy");
                            csvOutput += "," + receipt.IssuedDate.ToString("MM/dd/yyyy");
                            csvOutput += "," + totalAmount;
                            csvOutput += "," + receipt.DonationAmountinWords;
                            string recurringDates = receipt.RecurringDetails.Aggregate(" ", (current, recurringDetail) => current + ("(" + recurringDetail.DueDate.ToString("MM/dd/yyyy") + "-" + recurringDetail.ModeOfPayment + "-" + recurringDetail.Amount + ")"));
                            csvOutput += "," + recurringDates;
                            csvOutput += "," + receipt.MerchandiseItem;
                            csvOutput += "," + receipt.Quantity;
                            csvOutput += "," + receipt.FmvValue;
                            csvOutput += "," + receipt.ServiceType;
                            csvOutput += "," + receipt.HoursServed;
                            csvOutput += "," + receipt.RatePerHrOrDay;
                            csvOutput += "," + receipt.FmvValue;
                            csvOutput += "," + receipt.ModeOfPayment;
                            csvOutput += "," + receipt.DonationReceiver.Username;
                        }

                        Response.Write(csvOutput);
                        Response.End();
                        return View(exporttoExcelModel);
                    }
                    return View(exporttoExcelModel);
                }
                ViewData["Status"] = "You are not authorized to do this operation";
                return View("PartialViewStatus");
            }
            return RedirectToAction("LogOn", "Account");
        }
    }
}