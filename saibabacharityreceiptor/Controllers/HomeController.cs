using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using saibabacharityreceiptor.Models;
using saibabacharityreceiptorDL;
using Telerik.OpenAccess;

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
                ViewData["PostAction"] = string.Empty;
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
                    var receivedTime = Convert.ToDateTime(model.DateReceived).ToUniversalTime();
                    var receipt = new Receipt
                                      {
                                          Address = model.Address,
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
            if (Checkauthorization(scope, User.Identity.Name))
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
                        var receivedTime = Convert.ToDateTime(model.DateReceived).ToUniversalTime();
                        var receipt = receipts[0];
                        receipt.Address = model.Address;
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
                ViewData["PostAction"] = string.Empty;
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
                    var receivedTime = Convert.ToDateTime(model.DateReceived).ToUniversalTime();
                    var receipt = new Receipt
                    {
                        Address = model.Address,
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
            if (Checkauthorization(scope, User.Identity.Name))
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
                        var receivedTime = Convert.ToDateTime(model.DateReceived).ToUniversalTime();
                        var receipt = receipts[0];
                        receipt.Address = model.Address;
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
                ViewData["PostAction"] = string.Empty;
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
                    var receivedTime = Convert.ToDateTime(model.DateReceived).ToUniversalTime();
                    var receipt = new Receipt
                                      {
                                          Address = model.Address,
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
            if (Checkauthorization(scope, User.Identity.Name))
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
                        var receivedTime = Convert.ToDateTime(model.DateReceived).ToUniversalTime();
                        var receipt = receipts[0];
                        receipt.Address = model.Address;
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
                ViewData["PostAction"] = string.Empty;
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
                    var receivedTime = Convert.ToDateTime(model.DateReceived).ToUniversalTime();
                    var receipt = new Receipt
                                      {
                                          Address = model.Address,
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
            if (Checkauthorization(scope, User.Identity.Name))
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
                        var receivedTime = Convert.ToDateTime(model.DateReceived).ToUniversalTime();
                        receipt.Address = model.Address;
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

        [HttpGet]
        public ActionResult EditReceipt(string recpId)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOnPage", "Account");
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            if (Checkauthorization(scope, User.Identity.Name))
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
                    if (Checkauthorization(scope, User.Identity.Name))
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

        private static bool Checkauthorization(IObjectScope scope, string username)
        {
            List<User> users = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                where c.Username.ToLower().Equals(username.ToLower())
                                select c).ToList();
            if (users.Count > 0 && users[0].IsheDonationReceiver)
                return true;
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
                    var receiptData = new ReceiptData { Name = receipts[0].FirstName };
                    ViewData["Receipt_Data"] = receiptData;
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
        public string DownloadReceipt(string recpId)
        {
            return "this feature is under construction.";
        }
    }

    public struct ReceiptData
    {
        public string Name;
    }
}