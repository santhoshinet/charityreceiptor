using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using BarcodeGenerator;
using iTextSharp.text;
using iTextSharp.text.pdf;
using saibabacharityreceiptor.Models;
using saibabacharityreceiptorDL;
using Telerik.OpenAccess;
using Font = iTextSharp.text.Font;

namespace saibabacharityreceiptor.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        [HttpGet]
        public ActionResult RegularReceipt()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            if (Checkauthorization(scope, User.Identity.Name))
            {
                LoadReceiptValuesFromDb(scope);
                var modeofpayments = new List<string> { "Cash", "Cheque", "Online", "Mobile" };
                ViewData["modeOfPayment"] = modeofpayments;
                ViewData["PostAction"] = "RegularReceipt";
                ViewData["selectedModeOfPayment"] = string.Empty;
                ViewData["selectedDonationReceivedBy"] = string.Empty;
                return View(new RegularReceiptModels
                                      {
                                          ReceiptNumber = Utilities.GenerateReceiptId()
                                      });
            }
            ViewData["Status"] = "You are not authorized to do this operation";
            return View("PartialViewStatus");
        }

        [Authorize]
        [HttpPost]
        public ActionResult RegularReceipt(RegularReceiptModels model)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            if (Checkauthorization(scope, User.Identity.Name))
            {
                var donationReceiver = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                        where
                                            c.IsheDonationReceiver.Equals(true) &&
                                            c.Username.Equals(Request.Form["CmbDonationReceivedBy"])
                                        select c).ToList();
                if (donationReceiver.Count > 0)
                {
                    scope.Transaction.Begin();
                    var receivedTime = Convert.ToDateTime(model.DateReceived);
                    var receipt = new Receipt
                                      {
                                          Address = model.Address,
                                          Address2 = model.Address2,
                                          Contact = model.Contact,
                                          ReceiptNumber = model.ReceiptNumber,
                                          DonationAmount = model.DonationAmount,
                                          DonationAmountinWords = model.DonationAmountinWords,
                                          DonationReceiver = donationReceiver[0],
                                          Email = model.Email,
                                          DateReceived = receivedTime,
                                          FirstName = model.FirstName,
                                          ReceiptType = ReceiptType.GeneralReceipt,
                                          City = model.City,
                                          LastName = model.LastName,
                                          Mi = model.Mi,
                                          State = model.State,
                                          ZipCode = model.ZipCode,
                                          IssuedDate = model.IssuedDate
                                      };
                    switch (Request.Form["cmbModeOfPayment"])
                    {
                        case "Cash":
                            {
                                receipt.ModeOfPayment = ModeOfPayment.Cash;
                                break;
                            }
                        case "Cheque":
                            {
                                receipt.ModeOfPayment = ModeOfPayment.Cheque;
                                break;
                            }
                        case "Mobile":
                            {
                                receipt.ModeOfPayment = ModeOfPayment.Mobile;
                                break;
                            }
                        case "Goods":
                            {
                                receipt.ModeOfPayment = ModeOfPayment.Goods;
                                break;
                            }
                        case "Online":
                            {
                                receipt.ModeOfPayment = ModeOfPayment.Online;
                                break;
                            }
                    }
                    scope.Add(receipt);
                    scope.Transaction.Commit();
                    ViewData["ReceiptID"] = receipt.ReceiptNumber;
                    return View("Printoptions");
                }
                ViewData["PostAction"] = "RegularReceipt";
                ViewData["selectedModeOfPayment"] = Request.Form["cmbModeOfPayment"];
                ViewData["selectedDonationReceivedBy"] = Request.Form["CmbDonationReceivedBy"];
                ModelState.AddModelError("", "Unable to generate receipt due to invalid parameter passed.");
                return View();
            }
            ViewData["Status"] = "You are not authorized to do this operation";
            return View("PartialViewStatus");
        }

        [Authorize]
        [HttpPost]
        public ActionResult UpdateRegularReceipt(RegularReceiptModels model)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOnPage", "Account");
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            if (CheckAdminauthorization(scope, User.Identity.Name))
            {
                var donationReceiver = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                        where
                                            c.IsheDonationReceiver.Equals(true) &&
                                            c.Username.Equals(Request.Form["CmbDonationReceivedBy"])
                                        select c).ToList();
                if (donationReceiver.Count > 0)
                {
                    List<Receipt> receipts = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                              where c.ReceiptNumber.ToLower().Equals(model.ReceiptNumber.ToLower())
                                              select c).ToList();
                    if (receipts.Count > 0)
                    {
                        scope.Transaction.Begin();
                        var receivedTime = Convert.ToDateTime(model.DateReceived);
                        var receipt = receipts[0];
                        receipt.Address = model.Address;
                        receipt.Address2 = model.Address2;
                        receipt.Contact = model.Contact;
                        receipt.ReceiptNumber = model.ReceiptNumber;
                        receipt.DonationAmount = model.DonationAmount;
                        receipt.DonationAmountinWords = model.DonationAmountinWords;
                        receipt.DonationReceiver = donationReceiver[0];
                        receipt.Email = model.Email;
                        receipt.DateReceived = receivedTime;
                        receipt.FirstName = model.FirstName;
                        receipt.LastName = model.LastName;
                        receipt.City = model.City;
                        receipt.State = model.State;
                        receipt.City = model.City;
                        receipt.ZipCode = model.ZipCode;
                        receipt.IssuedDate = model.IssuedDate;
                        receipt.ReceiptType = ReceiptType.GeneralReceipt;
                        switch (Request.Form["cmbModeOfPayment"])
                        {
                            case "Cash":
                                {
                                    receipt.ModeOfPayment = ModeOfPayment.Cash;
                                    break;
                                }
                            case "Cheque":
                                {
                                    receipt.ModeOfPayment = ModeOfPayment.Cheque;
                                    break;
                                }
                            case "Mobile":
                                {
                                    receipt.ModeOfPayment = ModeOfPayment.Mobile;
                                    break;
                                }
                            case "Goods":
                                {
                                    receipt.ModeOfPayment = ModeOfPayment.Goods;
                                    break;
                                }
                            case "Online":
                                {
                                    receipt.ModeOfPayment = ModeOfPayment.Online;
                                    break;
                                }
                        }
                        scope.Add(receipt);
                        scope.Transaction.Commit();
                        ViewData["Status"] = "Updated successfully.";
                        return View("Status");
                    }
                }
                ViewData["Status"] = "Unable to generate receipt due to invalid parameter passed.";
                return View("Status");
            }
            ViewData["Status"] = "You are not authorized to do this operation";
            return View("Status");
        }

        [Authorize]
        [HttpGet]
        public ActionResult RecurringReceipt()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            if (Checkauthorization(scope, User.Identity.Name))
            {
                LoadReceiptValuesFromDb(scope);
                ViewData["PostAction"] = "RecurringReceipt";
                ViewData["selectedModeOfPayment"] = string.Empty;
                ViewData["selectedDonationReceivedBy"] = string.Empty;
                var receiptModel = new RecurringReceipt
                {
                    ReceiptNumber = Utilities.GenerateReceiptId()
                };
                return View(receiptModel);
            }
            ViewData["Status"] = "You are not authorized to do this operation";
            return View("PartialViewStatus");
        }

        [Authorize]
        [HttpPost]
        public ActionResult RecurringReceipt(RecurringReceipt model)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            if (Checkauthorization(scope, User.Identity.Name))
            {
                var donationReceiver = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                        where
                                            c.IsheDonationReceiver.Equals(true) &&
                                            c.Username.Equals(Request.Form["CmbDonationReceivedBy"])
                                        select c).ToList();
                if (donationReceiver.Count > 0)
                {
                    scope.Transaction.Begin();
                    var receivedTime = Convert.ToDateTime(model.DateReceived);
                    var receipt = new Receipt
                    {
                        Address = model.Address,
                        Address2 = model.Address2,
                        Contact = model.Contact,
                        ReceiptNumber = model.ReceiptNumber,
                        DonationAmount = model.DonationAmount,
                        DonationAmountinWords = model.DonationAmountinWords,
                        DonationReceiver = donationReceiver[0],
                        Email = model.Email,
                        DateReceived = receivedTime,
                        FirstName = model.FirstName,
                        City = model.City,
                        LastName = model.LastName,
                        Mi = model.Mi,
                        State = model.State,
                        ZipCode = model.ZipCode,
                        IssuedDate = model.IssuedDate,
                        ReceiptType = ReceiptType.RecurringReceipt
                    };
                    switch (Request.Form["cmbModeOfPayment"])
                    {
                        case "Cash":
                            {
                                receipt.ModeOfPayment = ModeOfPayment.Cash;
                                break;
                            }
                        case "Cheque":
                            {
                                receipt.ModeOfPayment = ModeOfPayment.Cheque;
                                break;
                            }
                        case "Mobile":
                            {
                                receipt.ModeOfPayment = ModeOfPayment.Mobile;
                                break;
                            }
                        case "Goods":
                            {
                                receipt.ModeOfPayment = ModeOfPayment.Goods;
                                break;
                            }
                        case "Online":
                            {
                                receipt.ModeOfPayment = ModeOfPayment.Online;
                                break;
                            }
                    }
                    foreach (string date in model.RecurrenceDates)
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
                    scope.Add(receipt);
                    scope.Transaction.Commit();
                    ViewData["ReceiptID"] = receipt.ReceiptNumber;
                    return View("Printoptions");
                }
                ViewData["PostAction"] = "RecurringReceipt";
                ViewData["selectedModeOfPayment"] = Request.Form["cmbModeOfPayment"];
                ViewData["selectedDonationReceivedBy"] = Request.Form["CmbDonationReceivedBy"];
                ModelState.AddModelError("", "Unable to generate receipt due to invalid parameter passed.");
                return View();
            }
            ViewData["Status"] = "You are not authorized to do this operation";
            return View("PartialViewStatus");
        }

        [Authorize]
        [HttpPost]
        public ActionResult UpdateRecurringReceipt(RecurringReceipt model)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            if (CheckAdminauthorization(scope, User.Identity.Name))
            {
                var donationReceiver = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                        where
                                            c.IsheDonationReceiver.Equals(true) &&
                                            c.Username.Equals(Request.Form["CmbDonationReceivedBy"])
                                        select c).ToList();
                if (donationReceiver.Count > 0)
                {
                    List<Receipt> receipts = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                              where c.ReceiptNumber.ToLower().Equals(model.ReceiptNumber.ToLower())
                                              select c).ToList();
                    if (receipts.Count > 0)
                    {
                        scope.Transaction.Begin();
                        var receivedTime = Convert.ToDateTime(model.DateReceived);
                        var receipt = receipts[0];
                        receipt.Address = model.Address;
                        receipt.Address2 = model.Address2;
                        receipt.Contact = model.Contact;
                        receipt.ReceiptNumber = model.ReceiptNumber;
                        receipt.DonationAmount = model.DonationAmount;
                        receipt.DonationAmountinWords = model.DonationAmountinWords;
                        receipt.DonationReceiver = donationReceiver[0];
                        receipt.Email = model.Email;
                        receipt.DateReceived = receivedTime;
                        receipt.FirstName = model.FirstName;
                        receipt.Mi = model.Mi;
                        receipt.LastName = model.LastName;
                        receipt.City = model.City;
                        receipt.State = model.State;
                        receipt.ZipCode = model.ZipCode;
                        receipt.ReceiptType = ReceiptType.RecurringReceipt;
                        receipt.IssuedDate = model.IssuedDate;
                        switch (Request.Form["cmbModeOfPayment"])
                        {
                            case "Cash":
                                {
                                    receipt.ModeOfPayment = ModeOfPayment.Cash;
                                    break;
                                }
                            case "Cheque":
                                {
                                    receipt.ModeOfPayment = ModeOfPayment.Cheque;
                                    break;
                                }
                            case "Mobile":
                                {
                                    receipt.ModeOfPayment = ModeOfPayment.Mobile;
                                    break;
                                }
                            case "Goods":
                                {
                                    receipt.ModeOfPayment = ModeOfPayment.Goods;
                                    break;
                                }
                            case "Online":
                                {
                                    receipt.ModeOfPayment = ModeOfPayment.Online;
                                    break;
                                }
                        }
                        receipt.RecurringDates.Clear();
                        foreach (string date in model.RecurrenceDates)
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
                        scope.Add(receipt);
                        scope.Transaction.Commit();
                        ViewData["Status"] = "Updated successfully.";
                        return View("Status");
                    }
                }
                ViewData["Status"] = "Unable to generate receipt due to invalid parameter passed.";
                return View("Status");
            }
            ViewData["Status"] = "You are not authorized to do this operation";
            return View("Status");
        }

        [Authorize]
        [HttpGet]
        public ActionResult MerchandiseReceipt()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            if (Checkauthorization(scope, User.Identity.Name))
            {
                LoadReceiptValuesFromDb(scope);
                ViewData["PostAction"] = "MerchandiseReceipt";
                ViewData["selectedDonationReceivedBy"] = string.Empty;
                var receiptModel = new MerchandiseReceipt
                                      {
                                          ReceiptNumber = Utilities.GenerateReceiptId()
                                      };
                return View(receiptModel);
            }
            ViewData["Status"] = "You are not authorized to do this operation";
            return View("PartialViewStatus");
        }

        [Authorize]
        [HttpPost]
        public ActionResult MerchandiseReceipt(MerchandiseReceipt model)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            if (Checkauthorization(scope, User.Identity.Name))
            {
                var donationReceiver = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                        where
                                            c.IsheDonationReceiver.Equals(true) &&
                                            c.Username.Equals(Request.Form["CmbDonationReceivedBy"])
                                        select c).ToList();
                if (donationReceiver.Count > 0)
                {
                    scope.Transaction.Begin();
                    var receivedTime = Convert.ToDateTime(model.DateReceived);
                    var receipt = new Receipt
                                      {
                                          Address = model.Address,
                                          Address2 = model.Address2,
                                          Contact = model.Contact,
                                          ReceiptNumber = model.ReceiptNumber,
                                          MerchandiseItem = model.MerchandiseItem,
                                          FmvValue = model.Value,
                                          DonationReceiver = donationReceiver[0],
                                          Email = model.Email,
                                          DateReceived = receivedTime,
                                          FirstName = model.FirstName,
                                          City = model.City,
                                          LastName = model.LastName,
                                          Mi = model.Mi,
                                          Quantity = model.Quanity,
                                          State = model.State,
                                          ZipCode = model.ZipCode,
                                          IssuedDate = model.IssuedDate,
                                          ReceiptType = ReceiptType.MerchandiseReceipt
                                      };
                    scope.Add(receipt);
                    scope.Transaction.Commit();
                    ViewData["ReceiptID"] = receipt.ReceiptNumber;
                    return View("Printoptions");
                }
                ViewData["PostAction"] = "MerchandiseReceipt";
                ViewData["selectedDonationReceivedBy"] = Request.Form["CmbDonationReceivedBy"];
                ModelState.AddModelError("", "Unable to generate receipt due to invalid parameter passed.");
                return View();
            }
            ViewData["Status"] = "You are not authorized to do this operation";
            return View("PartialViewStatus");
        }

        [Authorize]
        [HttpPost]
        public ActionResult UpdateMerchandiseReceipt(MerchandiseReceipt model)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            if (CheckAdminauthorization(scope, User.Identity.Name))
            {
                var donationReceiver = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                        where
                                            c.IsheDonationReceiver.Equals(true) &&
                                            c.Username.Equals(Request.Form["CmbDonationReceivedBy"])
                                        select c).ToList();
                if (donationReceiver.Count > 0)
                {
                    List<Receipt> receipts = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                              where c.ReceiptNumber.ToLower().Equals(model.ReceiptNumber.ToLower())
                                              select c).ToList();
                    if (receipts.Count > 0)
                    {
                        scope.Transaction.Begin();
                        var receivedTime = Convert.ToDateTime(model.DateReceived);
                        var receipt = receipts[0];
                        receipt.Address = model.Address;
                        receipt.Address2 = model.Address2;
                        receipt.Contact = model.Contact;
                        receipt.ReceiptNumber = model.ReceiptNumber;
                        receipt.MerchandiseItem = model.MerchandiseItem;
                        receipt.FmvValue = model.Value;
                        receipt.DonationReceiver = donationReceiver[0];
                        receipt.Email = model.Email;
                        receipt.DateReceived = receivedTime;
                        receipt.FirstName = model.FirstName;
                        receipt.Mi = model.Mi;
                        receipt.LastName = model.LastName;
                        receipt.City = model.City;
                        receipt.State = model.State;
                        receipt.ZipCode = model.ZipCode;
                        receipt.Quantity = model.Quanity;
                        receipt.IssuedDate = model.IssuedDate;
                        receipt.ReceiptType = ReceiptType.MerchandiseReceipt;
                        scope.Add(receipt);
                        scope.Transaction.Commit();
                        ViewData["Status"] = "Updated successfully.";
                        return View("Status");
                    }
                }
                ViewData["Status"] = "Unable to generate receipt due to invalid parameter passed.";
                return View("Status");
            }
            ViewData["Status"] = "You are not authorized to do this operation";
            return View("Status");
        }

        [Authorize]
        [HttpGet]
        public ActionResult ServicesReceipt()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            if (Checkauthorization(scope, User.Identity.Name))
            {
                LoadReceiptValuesFromDb(scope);
                ViewData["PostAction"] = "ServicesReceipt";
                ViewData["selectedDonationReceivedBy"] = string.Empty;
                var receiptModel = new ServicesReceipt
                                       {
                                           ReceiptNumber = Utilities.GenerateReceiptId()
                                       };
                return View(receiptModel);
            }
            ViewData["Status"] = "You are not authorized to do this operation";
            return View("PartialViewStatus");
        }

        [Authorize]
        [HttpPost]
        public ActionResult ServicesReceipt(ServicesReceipt model)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            if (Checkauthorization(scope, User.Identity.Name))
            {
                var donationReceiver = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                        where
                                            c.IsheDonationReceiver.Equals(true) &&
                                            c.Username.Equals(Request.Form["CmbDonationReceivedBy"])
                                        select c).ToList();
                if (donationReceiver.Count > 0)
                {
                    scope.Transaction.Begin();
                    var receivedTime = Convert.ToDateTime(model.DateReceived);
                    var receipt = new Receipt
                                      {
                                          Address = model.Address,
                                          Address2 = model.Address2,
                                          Contact = model.Contact,
                                          ReceiptNumber = model.ReceiptNumber,
                                          ServiceType = model.ServiceType,
                                          HoursServed = Convert.ToInt32(model.HoursServed),
                                          DonationReceiver = donationReceiver[0],
                                          Email = model.Email,
                                          DateReceived = receivedTime,
                                          FirstName = model.FirstName,
                                          Mi = model.Mi,
                                          City = model.City,
                                          LastName = model.LastName,
                                          State = model.State,
                                          ZipCode = model.ZipCode,
                                          ReceiptType = ReceiptType.ServicesReceipt,
                                          FmvValue = model.FmvValue.ToString(),
                                          IssuedDate = model.IssuedDate,
                                          RatePerHrOrDay = model.RateperHour.ToString()
                                      };
                    scope.Add(receipt);
                    scope.Transaction.Commit();
                    ViewData["ReceiptID"] = receipt.ReceiptNumber;
                    return View("Printoptions");
                }
                ViewData["PostAction"] = "ServicesReceipt";
                ViewData["selectedDonationReceivedBy"] = Request.Form["CmbDonationReceivedBy"];
                ModelState.AddModelError("", "Unable to generate receipt due to invalid parameter passed.");
                return View();
            }
            ViewData["Status"] = "You are not authorized to do this operation";
            return View("PartialViewStatus");
        }

        [Authorize]
        [HttpPost]
        public ActionResult UpdateServicesReceipt(ServicesReceipt model)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            if (CheckAdminauthorization(scope, User.Identity.Name))
            {
                var donationReceiver = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                        where
                                            c.IsheDonationReceiver.Equals(true) &&
                                            c.Username.Equals(Request.Form["CmbDonationReceivedBy"])
                                        select c).ToList();
                if (donationReceiver.Count > 0)
                {
                    List<Receipt> receipts = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                              where c.ReceiptNumber.ToLower().Equals(model.ReceiptNumber.ToLower())
                                              select c).ToList();
                    if (receipts.Count > 0)
                    {
                        scope.Transaction.Begin();
                        var receipt = receipts[0];
                        var receivedTime = Convert.ToDateTime(model.DateReceived);
                        receipt.Address = model.Address;
                        receipt.Address2 = model.Address2;
                        receipt.Contact = model.Contact;
                        receipt.ReceiptNumber = model.ReceiptNumber;
                        receipt.HoursServed = Convert.ToInt32(model.HoursServed);
                        receipt.DonationReceiver = donationReceiver[0];
                        receipt.Email = model.Email;
                        receipt.DateReceived = receivedTime;
                        receipt.ReceiptType = ReceiptType.ServicesReceipt;
                        receipt.City = model.City;
                        receipt.FirstName = model.FirstName;
                        receipt.FmvValue = model.FmvValue.ToString();
                        receipt.HoursServed = model.HoursServed;
                        receipt.LastName = model.LastName;
                        receipt.Mi = model.Mi;
                        receipt.RatePerHrOrDay = model.RateperHour.ToString();
                        receipt.ServiceType = model.ServiceType;
                        receipt.ZipCode = model.ZipCode;
                        receipt.State = model.State;
                        receipt.IssuedDate = model.IssuedDate;
                        scope.Add(receipt);
                        scope.Transaction.Commit();
                        ViewData["Status"] = "Updated successfully.";
                        return View("Status");
                    }
                }
                ViewData["Status"] = "Unable to generate receipt due to invalid parameter passed.";
                return View("Status");
            }
            ViewData["Status"] = "You are not authorized to do this operation";
            return View("Status");
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditReceipt(string recpId)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOnPage", "Account");
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            if (CheckAdminauthorization(scope, User.Identity.Name))
            {
                List<Receipt> receipts = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                          where c.ReceiptNumber.ToLower().Equals(recpId.ToLower())
                                          select c).ToList();
                if (receipts.Count > 0)
                {
                    LoadReceiptValuesFromDb(scope);
                    var receipt = receipts[0];
                    switch (receipt.ReceiptType)
                    {
                        case ReceiptType.GeneralReceipt:
                            {
                                var model = new RegularReceiptModels
                                                {
                                                    Address = receipt.Address,
                                                    Address2 = receipt.Address2,
                                                    Contact = receipt.Contact,
                                                    DateReceived = receipt.DateReceived,
                                                    DonationAmount = receipt.DonationAmount,
                                                    DonationAmountinWords = receipt.DonationAmountinWords,
                                                    Email = receipt.Email,
                                                    City = receipt.City,
                                                    FirstName = receipt.FirstName,
                                                    LastName = receipt.LastName,
                                                    Mi = receipt.Mi,
                                                    ReceiptNumber = receipt.ReceiptNumber,
                                                    State = receipt.State,
                                                    ZipCode = receipt.ZipCode,
                                                    IssuedDate = receipt.IssuedDate
                                                };
                                ViewData["PostAction"] = "UpdateRegularReceipt";
                                ViewData["selectedModeOfPayment"] = receipt.ModeOfPayment.ToString();
                                ViewData["selectedDonationReceivedBy"] = receipt.DonationReceiver.Username;
                                return View("EditRegularReceipt", model);
                            }
                        case ReceiptType.RecurringReceipt:
                            {
                                var model = new RecurringReceipt
                                                {
                                                    Address = receipt.Address,
                                                    Address2 = receipt.Address2,
                                                    Contact = receipt.Contact,
                                                    DateReceived = receipt.DateReceived,
                                                    DonationAmount = receipt.DonationAmount,
                                                    DonationAmountinWords = receipt.DonationAmountinWords,
                                                    Email = receipt.Email,
                                                    ReceiptNumber = receipt.ReceiptNumber,
                                                    City = receipt.City,
                                                    FirstName = receipt.FirstName,
                                                    LastName = receipt.LastName,
                                                    Mi = receipt.Mi,
                                                    State = receipt.State,
                                                    ZipCode = receipt.ZipCode,
                                                    IssuedDate = receipt.IssuedDate,
                                                    RecurrenceDates = receipt.RecurringDates.Select(recurringDate => recurringDate.ToString()).ToArray()
                                                };
                                ViewData["PostAction"] = "UpdateRecurringReceipt";
                                ViewData["selectedModeOfPayment"] = receipt.ModeOfPayment.ToString();
                                ViewData["selectedDonationReceivedBy"] = receipt.DonationReceiver.Username;
                                return View("EditRecurringReceipt", model);
                            }
                        case ReceiptType.MerchandiseReceipt:
                            {
                                var model = new MerchandiseReceipt
                                {
                                    Address = receipt.Address,
                                    Address2 = receipt.Address2,
                                    Contact = receipt.Contact,
                                    DateReceived = receipt.DateReceived,
                                    Email = receipt.Email,
                                    ReceiptNumber = receipt.ReceiptNumber,
                                    MerchandiseItem = receipt.MerchandiseItem,
                                    Value = receipt.FmvValue,
                                    City = receipt.City,
                                    FirstName = receipt.FirstName,
                                    LastName = receipt.LastName,
                                    Mi = receipt.Mi,
                                    Quanity = receipt.Quantity,
                                    State = receipt.State,
                                    ZipCode = receipt.ZipCode,
                                    IssuedDate = receipt.IssuedDate
                                };
                                ViewData["selectedDonationReceivedBy"] = receipt.DonationReceiver.Username;
                                ViewData["PostAction"] = "UpdateMerchandiseReceipt";
                                return View("EditMerchandiseReceipt", model);
                            }
                        case ReceiptType.ServicesReceipt:
                            {
                                var model = new ServicesReceipt
                                {
                                    Address = receipt.Address,
                                    Address2 = receipt.Address2,
                                    Contact = receipt.Contact,
                                    DateReceived = receipt.DateReceived,
                                    Email = receipt.Email,
                                    ReceiptNumber = receipt.ReceiptNumber,
                                    HoursServed = receipt.HoursServed,
                                    City = receipt.City,
                                    FirstName = receipt.FirstName,
                                    FmvValue = Convert.ToInt32(receipt.FmvValue),
                                    LastName = receipt.LastName,
                                    Mi = receipt.Mi,
                                    RateperHour = Convert.ToInt32(receipt.RatePerHrOrDay),
                                    IssuedDate = receipt.IssuedDate,
                                    ServiceType = receipt.ServiceType,
                                    State = receipt.State,
                                    ZipCode = receipt.ZipCode
                                };
                                ViewData["selectedDonationReceivedBy"] = receipt.DonationReceiver.Username;
                                ViewData["PostAction"] = "UpdateServicesReceipt";
                                return View("EditServicesReceipt", model);
                            }
                    }
                }
                ViewData["Status"] = "The receipt not found for the given id.";
                return View("Status");
            }
            ViewData["Status"] = "You are not authorized to do this operation";
            return View("PartialViewStatus");
        }

        [Authorize]
        [HttpGet]
        public ActionResult PrintOptions(string recpId)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            if (Checkauthorization(scope, User.Identity.Name))
            {
                ViewData["ReceiptID"] = recpId;
                return View();
            }
            ViewData["Status"] = "You are not authorized to do this operation";
            return View("PartialViewStatus");
        }

        public string Deletereport(string recpId)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var scope = ObjectScopeProvider1.GetNewObjectScope();
                    if (CheckAdminauthorization(scope, User.Identity.Name))
                    {
                        List<Receipt> receipts = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                                  where c.ReceiptNumber.ToLower().Equals(recpId.ToLower())
                                                  select c).ToList();
                        if (receipts.Count > 0)
                        {
                            foreach (var receipt in receipts)
                            {
                                scope.Transaction.Begin();
                                scope.Remove(receipt);
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

        private bool Checkauthorization(IObjectScope scope, string username)
        {
            List<User> users = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                where c.Username.ToLower().Equals(username.ToLower())
                                select c).ToList();
            if (users.Count > 0 && (users[0].IsheDonationReceiver || users[0].IsheAdmin))
            {
                ViewData["IsheAdmin"] = users[0].IsheAdmin;
                ViewData["IsheDonationReceiver"] = users[0].IsheDonationReceiver;
                return true;
            }
            ViewData["IsheAdmin"] = false;
            ViewData["IsheDonationReceiver"] = false;
            return false;
        }

        private bool CheckAdminauthorization(IObjectScope scope, string username)
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

        private void LoadReceiptValuesFromDb(IObjectScope scope)
        {
            var modeofpayments = new List<string> { "Cash", "Cheque", "Online", "Mobile", "Goods" };
            ViewData["modeOfPayment"] = modeofpayments;
            var receivers = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                             where c.IsheDonationReceiver.Equals(true)
                             select c).ToList();
            var donationReceivers = receivers.Select(receiver => receiver.Username).ToList();
            ViewData["donationReceivers"] = donationReceivers;
        }

        [Authorize]
        [HttpGet]
        public ActionResult PrintReceipt(string recpId)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            if (Checkauthorization(scope, User.Identity.Name))
            {
                List<Receipt> receipts = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                          where c.ReceiptNumber.ToLower().Equals(recpId.ToLower())
                                          select c).ToList();
                if (receipts.Count > 0)
                {
                    var receiptDatas = new List<ReceiptData>
                                           {
                                               new ReceiptData
                                                   {
                                                       FirstName = receipts[0].FirstName,
                                                       Address = receipts[0].Address,
                                                       Address2 = receipts[0].Address2,
                                                       City = receipts[0].City,
                                                       Contact = receipts[0].Contact,
                                                       DateReceived = receipts[0].DateReceived,
                                                       DonationAmount = receipts[0].DonationAmount,
                                                       DonationAmountinWords = receipts[0].DonationAmountinWords,
                                                       DonationReceiverName = receipts[0].DonationReceiver.Username,
                                                       Email = receipts[0].Email,
                                                       FmvValue = receipts[0].FmvValue,
                                                       GroupId = receipts[0].GroupId,
                                                       HoursServed = receipts[0].HoursServed,
                                                       IssuedDate = receipts[0].IssuedDate,
                                                       LastName = receipts[0].LastName,
                                                       MerchandiseItem = receipts[0].MerchandiseItem,
                                                       Mi = receipts[0].Mi,
                                                       ModeOfPayment = receipts[0].ModeOfPayment.ToString(),
                                                       Quantity = receipts[0].Quantity,
                                                       RatePerHrOrDay = receipts[0].RatePerHrOrDay,
                                                       ReceiptNumber = receipts[0].ReceiptNumber,
                                                       ReceiptType = receipts[0].ReceiptType.ToString(),
                                                       RecurringDates = receipts[0].RecurringDates,
                                                       ServiceType = receipts[0].ServiceType,
                                                       State = receipts[0].State,
                                                       ZipCode = receipts[0].ZipCode
                                                   }
                                           };
                    ViewData["Receipt_Data"] = receiptDatas;
                    return View();
                    /*switch (receipts[0].ReceiptType)
                    {
                        case ReceiptType.GeneralReceipt:
                            {
                                return View("PrintRegularReceipt");
                            }
                        case ReceiptType.RecurringReceipt:
                            {
                                return View("PrintRecurringReceipt");
                            }
                        case ReceiptType.MerchandiseReceipt:
                            {
                                return View("PrintMerchandiseReport");
                            }
                        case ReceiptType.ServicesReceipt:
                            {
                                return View("PrintServicesReceipt");
                            }
                    }*/
                }
                receipts = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                            where c.GroupId != null && c.GroupId.ToLower().Equals(recpId.ToLower())
                            select c).ToList();
                if (receipts.Count > 0)
                {
                    var receiptDatas = receipts.Select(receipt => new ReceiptData
                                                                      {
                                                                          FirstName = receipt.FirstName,
                                                                          Address = receipt.Address,
                                                                          Address2 = receipts[0].Address2,
                                                                          City = receipt.City,
                                                                          Contact = receipt.Contact,
                                                                          DateReceived = receipt.DateReceived,
                                                                          DonationAmount = receipt.DonationAmount,
                                                                          DonationAmountinWords = receipt.DonationAmountinWords,
                                                                          DonationReceiverName = receipt.DonationReceiver.Username,
                                                                          Email = receipt.Email,
                                                                          FmvValue = receipt.FmvValue,
                                                                          GroupId = receipt.GroupId,
                                                                          HoursServed = receipt.HoursServed,
                                                                          IssuedDate = receipt.IssuedDate,
                                                                          LastName = receipt.LastName,
                                                                          MerchandiseItem = receipt.MerchandiseItem,
                                                                          Mi = receipt.Mi,
                                                                          ModeOfPayment = receipt.ModeOfPayment.ToString(),
                                                                          Quantity = receipt.Quantity,
                                                                          RatePerHrOrDay = receipt.RatePerHrOrDay,
                                                                          ReceiptNumber = receipt.ReceiptNumber,
                                                                          ReceiptType = receipt.ReceiptType.ToString(),
                                                                          RecurringDates = receipt.RecurringDates,
                                                                          ServiceType = receipt.ServiceType,
                                                                          State = receipt.State,
                                                                          ZipCode = receipt.ZipCode
                                                                      }).ToList();
                    ViewData["Receipt_Data"] = receiptDatas;
                    return View();
                }
                ViewData["Status"] = "The receipt not found for the given id.";
                return View("Status");
            }
            ViewData["Status"] = "You are not authorized to do this operation";
            return View("PartialViewStatus");
        }

        [Authorize]
        [HttpGet]
        public FileStreamResult DownloadReceipt(string recpId)
        {
            if (!User.Identity.IsAuthenticated)
                return null;
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            if (Checkauthorization(scope, User.Identity.Name))
            {
                List<Receipt> receipts = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                          where c.ReceiptNumber.ToLower().Equals(recpId.ToLower())
                                          select c).ToList();
                var receiptDatas = new List<ReceiptData>();
                if (receipts.Count > 0)
                {
                    receiptDatas = new List<ReceiptData>
                                           {
                                               new ReceiptData
                                                   {
                                                       FirstName = receipts[0].FirstName,
                                                       Address = receipts[0].Address,
                                                       City = receipts[0].City,
                                                       Contact = receipts[0].Contact,
                                                       DateReceived = receipts[0].DateReceived,
                                                       DonationAmount = receipts[0].DonationAmount,
                                                       DonationAmountinWords = receipts[0].DonationAmountinWords,
                                                       DonationReceiverName = receipts[0].DonationReceiver.Username,
                                                       Email = receipts[0].Email,
                                                       FmvValue = receipts[0].FmvValue,
                                                       GroupId = receipts[0].GroupId,
                                                       HoursServed = receipts[0].HoursServed,
                                                       IssuedDate = receipts[0].IssuedDate,
                                                       LastName = receipts[0].LastName,
                                                       MerchandiseItem = receipts[0].MerchandiseItem,
                                                       Mi = receipts[0].Mi,
                                                       ModeOfPayment = receipts[0].ModeOfPayment.ToString(),
                                                       Quantity = receipts[0].Quantity,
                                                       RatePerHrOrDay = receipts[0].RatePerHrOrDay,
                                                       ReceiptNumber = receipts[0].ReceiptNumber,
                                                       ReceiptType = receipts[0].ReceiptType.ToString(),
                                                       RecurringDates = receipts[0].RecurringDates,
                                                       ServiceType = receipts[0].ServiceType,
                                                       State = receipts[0].State,
                                                       ZipCode = receipts[0].ZipCode
                                                   }
                                           };
                }
                else
                {
                    receipts = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                where c.GroupId != null && c.GroupId.ToLower().Equals(recpId.ToLower())
                                select c).ToList();
                    if (receipts.Count > 0)
                    {
                        receiptDatas = receipts.Select(receipt => new ReceiptData
                                                                          {
                                                                              FirstName = receipt.FirstName,
                                                                              Address = receipt.Address,
                                                                              City = receipt.City,
                                                                              Contact = receipt.Contact,
                                                                              DateReceived = receipt.DateReceived,
                                                                              DonationAmount = receipt.DonationAmount,
                                                                              DonationAmountinWords =
                                                                                  receipt.DonationAmountinWords,
                                                                              DonationReceiverName =
                                                                                  receipt.DonationReceiver.Username,
                                                                              Email = receipt.Email,
                                                                              FmvValue = receipt.FmvValue,
                                                                              GroupId = receipt.GroupId,
                                                                              HoursServed = receipt.HoursServed,
                                                                              IssuedDate = receipt.IssuedDate,
                                                                              LastName = receipt.LastName,
                                                                              MerchandiseItem = receipt.MerchandiseItem,
                                                                              Mi = receipt.Mi,
                                                                              ModeOfPayment =
                                                                                  receipt.ModeOfPayment.ToString(),
                                                                              Quantity = receipt.Quantity,
                                                                              RatePerHrOrDay = receipt.RatePerHrOrDay,
                                                                              ReceiptNumber = receipt.ReceiptNumber,
                                                                              ReceiptType = receipt.ReceiptType.ToString(),
                                                                              RecurringDates = receipt.RecurringDates,
                                                                              ServiceType = receipt.ServiceType,
                                                                              State = receipt.State,
                                                                              ZipCode = receipt.ZipCode
                                                                          }).ToList();
                    }
                }

                // Create a Document object
                var document = new Document(PageSize.A4, 50, 50, 25, 25);
                var output = new MemoryStream();
                PdfWriter writer = PdfWriter.GetInstance(document, output);
                document.Open();
                //add header image
                //Image headerlogo = Image.GetInstance("C:\\Temp\\citylogo.png");
                //document.Add(headerlogo);
                foreach (ReceiptData receiptData in receiptDatas)
                {
                    for (int i = 0; i < 8; i++)
                        NewLine(document);
                    Header(document, "Donation Receipt");
                    NewLine(document);
                    ReceiptId(document, receiptData.ReceiptNumber);
                    NewLine(document);
                    ThreeField(document, "First name:", receiptData.FirstName, "MI:", receiptData.Mi, "Last Name:", receiptData.LastName);
                    NewLine(document);
                    TwoField(document, "Address1:", "", "Address2:", "");
                    NewLine(document);
                    TwoField(document, "", receiptData.Address, "", receiptData.Address2);
                    NewLine(document);
                    ThreeField(document, "City:", receiptData.City, "State:", receiptData.State, "Zip:", receiptData.ZipCode);
                    NewLine(document);
                    TwoField(document, "Email", receiptData.Email, "Phone:", receiptData.Address2);
                    NewLine(document);
                    switch (receiptData.ReceiptType)
                    {
                        case "GeneralReceipt":
                            {
                                SingleField(document, "Donation received date:", receiptData.DateReceived.ToString("dd  MMM  yyyy"));
                                NewLine(document);
                                SingleField(document, "Donation amount received in USD:", receiptData.DonationAmount);
                                NewLine(document);
                                SingleField(document, "Donation received in words:", receiptData.DonationAmountinWords);
                                NewLine(document);
                                SingleField(document, "Mode of donation:", receiptData.ModeOfPayment);
                                NewLine(document);
                                break;
                            }
                        case "RecurringReceipt":
                            {
                                break;
                            }
                        case "MerchandiseReceipt":
                            {
                                SingleField(document, "Donation received date:", receiptData.DateReceived.ToString("dd  MMM  yyyy"));
                                NewLine(document);
                                SingleField(document, "Goods received:", receiptData.MerchandiseItem);
                                NewLine(document);
                                SingleField(document, "Goods FMV in USD:", receiptData.FmvValue);
                                NewLine(document);
                                break;
                            }
                        case "ServicesReceipt":
                            {
                                SingleField(document, "Service received date:", receiptData.DateReceived.ToString("dd  MMM  yyyy"));
                                NewLine(document);
                                SingleField(document, "Service type:", receiptData.ServiceType);
                                NewLine(document);
                                TwoField(document, "Service Duration (No.of hrs/ day):", receiptData.HoursServed.ToString(), "Rate per hr/day", receiptData.RatePerHrOrDay);
                                NewLine(document);
                                SingleField(document, "FMV in USD:", receiptData.FmvValue);
                                break;
                            }
                    }
                    for (int i = 0; i < 3; i++)
                        NewLine(document);
                    TwoField(document, "Donation received by:", receiptData.DonationReceiverName, "Signature:", "");
                    NewLine(document);
                    TwoField(document, "Shridi Saibaba Temple Arizona", "", "Issued Date:", receiptData.IssuedDate.ToString("dd MMM yyyy"));
                    for (int i = 0; i < 3; i++)
                        NewLine(document);
                    Notes(document, "* No goods or services were provided in exchange for these contributions.", "");
                    NewLine(document);
                    Notes(document, "* This document is necessary for any available federal income tax deduction for your contribution. Please retain it for your records.", "");
                    NewLine(document);
                    NewLine(document);
                    Theme(document, "“Dharma will put an end to Karma”");
                    NewLine(document);
                    Thanks(document, "Thank You – Jai Sairam!");
                    NewLine(document);
                    PdfContentByte cb = writer.DirectContent;
                    var code128 = new Barcode128
                    {
                        CodeType = iTextSharp.text.pdf.Barcode.CODE128,
                        ChecksumText = true,
                        GenerateChecksum = true,
                        StartStopText = true,
                        Code = receiptData.ReceiptNumber
                    };
                    iTextSharp.text.Image image = code128.CreateImageWithBarcode(cb,
                                                                                 new BaseColor(Color.Black),
                                                                                 new BaseColor(Color.White));
                    image.IndentationLeft = 150f;
                    image.SpacingBefore = 150f;
                    image.Left = 150f;
                    image.GetLeft(150f);
                    image.Alignment = 1;
                    document.Add(image);
                }
                document.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Receipt-{0}.pdf", recpId));
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(output.GetBuffer(), 0, output.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.End();
                return new FileStreamResult(Response.OutputStream, "application/pdf");
            }
            return null;
        }

        private static void TwoField(Document document, string field1, string value1, string field2, string value2)
        {
            SingleField(document, field1, value1);
            if (string.IsNullOrEmpty(value1))
                value1 = string.Empty;
            if (string.IsNullOrEmpty(field1))
                field1 = string.Empty;
            int count = value1.Length + field1.Length - 80;
            if (count < 0)
                count = count * -1;
            for (int i = 0; i < count; i++)
                document.Add(new Anchor(" ", new Font()));
            SingleField(document, field2, value2);
        }

        private static void SingleField(Document document, string field1, string value1)
        {
            var baseColor = new BaseColor(Color.Gray);
            Font arial = FontFactory.GetFont("Arial", 9f, baseColor);
            document.Add(new Anchor(field1 + " ", arial));
            baseColor = new BaseColor(Color.Black);
            arial = FontFactory.GetFont("Arial", 9f, baseColor);
            document.Add(new Anchor(value1, arial));
        }

        private static void Notes(Document document, string field1, string value1)
        {
            var baseColor = new BaseColor(Color.Gray);
            Font arial = FontFactory.GetFont("Arial", 8f, baseColor);
            document.Add(new Anchor(field1 + " ", arial));
            baseColor = new BaseColor(Color.Black);
            arial = FontFactory.GetFont("Arial", 8f, baseColor);
            document.Add(new Anchor(value1, arial));
        }

        private static void ThreeField(Document document, string field1, string value1, string field2, string value2, string field3, string value3)
        {
            SingleField(document, field1, value1);

            if (string.IsNullOrEmpty(value1))
                value1 = string.Empty;
            if (string.IsNullOrEmpty(field1))
                field1 = string.Empty;
            int count = value1.Length + field1.Length - 50;
            if (count < 0)
                count = count * -1;
            for (int i = 0; i < count; i++)
                document.Add(new Anchor(" ", new Font()));

            SingleField(document, field2, value2);

            if (string.IsNullOrEmpty(value2))
                value2 = string.Empty;
            if (string.IsNullOrEmpty(field2))
                field2 = string.Empty;
            count = value2.Length + field2.Length - 50;
            if (count < 0)
                count = count * -1;
            for (int i = 0; i < count; i++)
                document.Add(new Anchor(" ", new Font()));

            SingleField(document, field3, value3);
        }

        private static void NewLine(Document document)
        {
            document.Add(new Phrase("\n"));
        }

        private static void Header(Document document, string title)
        {
            var baseColor = new BaseColor(Color.Black);
            Font arial = FontFactory.GetFont("Arial", 11f, baseColor);
            document.Add(new Paragraph(title, arial) { IndentationLeft = 220 });
        }

        private static void ReceiptId(Document document, string receiptId)
        {
            var arial = new Font(Font.FontFamily.TIMES_ROMAN, 10f, Font.BOLD, new BaseColor(163, 21, 21)); //FontFactory.GetFont("Arial", 10f, baseColor);
            document.Add(new Anchor("Receipt #:", arial));
            document.Add(new Anchor("  " + receiptId, arial));
        }

        private static void Thanks(Document document, string theme)
        {
            var baseColor = new BaseColor(Color.Black);
            Font arial = FontFactory.GetFont("Arial", 10f, baseColor);
            document.Add(new Paragraph(theme, arial) { IndentationLeft = 200 });
        }

        private static void Theme(Document document, string theme)
        {
            var baseColor = new BaseColor(Color.Red);
            Font arial = FontFactory.GetFont("Arial", 10f, baseColor);
            document.Add(new Paragraph(theme, arial) { IndentationLeft = 180 });
        }

        private static MemoryStream Barcode(string recpId)
        {
            try
            {
                const string fontString = "Arial";
                const int thickness = 30;
                const string code = "Code 128";
                const int scale = 1;

                var font = new BCGFont(new System.Drawing.Font(fontString, 1));
                var colorBlack = new BCGColor(Color.Black);
                var colorWhite = new BCGColor(Color.White);
                Type codeType = (from kvp in Utilities.CodeType where kvp.Value == code select kvp.Key).FirstOrDefault();
                var temporaryBarcode = (BarCode.Barcode)Activator.CreateInstance(codeType);
                var codebar = (BCGBarcode1D)Activator.CreateInstance(temporaryBarcode.Code);
                codebar.setThickness(thickness);
                codebar.setScale(scale);
                MethodInfo method = temporaryBarcode.Code.GetMethod("setStart");
                if (method != null)
                {
                    method.Invoke(codebar, new object[] { "A" });
                }
                codebar.setBackgroundColor(colorWhite);
                codebar.setForegroundColor(colorBlack);
                codebar.setFont(font);
                codebar.parse(Utilities.Encrypt(recpId));
                var drawing = new BCGDrawing(colorWhite);
                drawing.setBarcode(codebar);
                drawing.draw();
                var stream = new MemoryStream();
                drawing.finish(ImageFormat.Jpeg, stream);
                return stream;
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }
    }

    public struct ReceiptData
    {
        public string ReceiptNumber { get; set; }

        public string FirstName { get; set; }

        public string Mi { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string Email { get; set; }

        public string Contact { get; set; }

        public string DonationAmount { get; set; }

        public string DonationAmountinWords { get; set; }

        public string MerchandiseItem { get; set; }

        public string ServiceType { get; set; }

        public string Quantity { get; set; }

        public string FmvValue { get; set; }

        public int HoursServed { get; set; }

        public string RatePerHrOrDay { get; set; }

        public IList<DateTime> RecurringDates { get; set; }

        public string ModeOfPayment { get; set; }

        public string DonationReceiverName { get; set; }

        public DateTime DateReceived { get; set; }

        public string ReceiptType { get; set; }

        public string GroupId { get; set; }

        public DateTime IssuedDate { get; set; }
    }
}