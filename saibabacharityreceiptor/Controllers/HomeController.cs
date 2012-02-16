using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using saibabacharityreceiptor.Models;
using saibabacharityreceiptorDL;
using Telerik.OpenAccess;
using Font = iTextSharp.text.Font;
using Image = iTextSharp.text.Image;
using Rectangle = System.Drawing.Rectangle;

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
                                    ReceiptNumber = Utilities.GenerateReceiptId(),
                                    DateReceived = DateTime.Now,
                                    IssuedDate = DateTime.Now
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
                if (model.SignatureImage != null)
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
                        // saving image here
                        try
                        {
                            var signature = new SignatureImage { Filename = model.SignatureImage.FileName };
                            Stream fileStream = model.SignatureImage.InputStream;
                            int fileLength = model.SignatureImage.ContentLength;
                            signature.Filedata = new byte[fileLength];
                            fileStream.Read(signature.Filedata, 0, fileLength);
                            signature.MimeType = model.SignatureImage.ContentType;
                            signature.ID = Guid.NewGuid();
                            receipt.SignatureImage = signature;
                        }
                        catch
                        {
                        }
                        scope.Add(receipt);
                        scope.Transaction.Commit();
                        ViewData["ReceiptID"] = receipt.ReceiptNumber;
                        return View("Printoptions");
                    }
                }
                LoadReceiptValuesFromDb(scope);
                var modeofpayments = new List<string> { "Cash", "Cheque", "Online", "Mobile" };
                ViewData["modeOfPayment"] = modeofpayments;
                ViewData["PostAction"] = "RegularReceipt";
                ViewData["selectedModeOfPayment"] = Request.Form["cmbModeOfPayment"];
                ViewData["selectedDonationReceivedBy"] = Request.Form["CmbDonationReceivedBy"];
                ModelState.AddModelError("SignatureImage", "Please input signature image.");
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
                        if (model.SignatureImage != null)
                        {
                            // saving image here
                            try
                            {
                                var signature = new SignatureImage { Filename = model.SignatureImage.FileName };
                                Stream fileStream = model.SignatureImage.InputStream;
                                int fileLength = model.SignatureImage.ContentLength;
                                signature.Filedata = new byte[fileLength];
                                fileStream.Read(signature.Filedata, 0, fileLength);
                                signature.MimeType = model.SignatureImage.ContentType;
                                signature.ID = Guid.NewGuid();
                                receipt.SignatureImage = signature;
                            }
                            catch
                            { }
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
                    ReceiptNumber = Utilities.GenerateReceiptId(),
                    DateReceived = DateTime.Now,
                    IssuedDate = DateTime.Now
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
                if (model.SignatureImage != null)
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
                        for (int i = 0; i < model.RecurrenceDates.Count(); i++)
                        {
                            try
                            {
                                var recurringDetails = new RecurringDetails
                                                           {
                                                               DueDate = Convert.ToDateTime(model.RecurrenceDates[i]),
                                                               Amount = model.RecurrenceAmount[i]
                                                           };
                                switch (model.RecurrenceModeofPayment[i])
                                {
                                    case "Cash":
                                        {
                                            recurringDetails.ModeOfPayment = ModeOfPayment.Cash;
                                            break;
                                        }
                                    case "Cheque":
                                        {
                                            recurringDetails.ModeOfPayment = ModeOfPayment.Cheque;
                                            break;
                                        }
                                    case "Mobile":
                                        {
                                            recurringDetails.ModeOfPayment = ModeOfPayment.Mobile;
                                            break;
                                        }
                                    case "Goods":
                                        {
                                            recurringDetails.ModeOfPayment = ModeOfPayment.Goods;
                                            break;
                                        }
                                    case "Online":
                                        {
                                            recurringDetails.ModeOfPayment = ModeOfPayment.Online;
                                            break;
                                        }
                                }
                                receipt.RecurringDetails.Add(recurringDetails);
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                        // saving image here
                        try
                        {
                            var signature = new SignatureImage { Filename = model.SignatureImage.FileName };
                            Stream fileStream = model.SignatureImage.InputStream;
                            int fileLength = model.SignatureImage.ContentLength;
                            signature.Filedata = new byte[fileLength];
                            fileStream.Read(signature.Filedata, 0, fileLength);
                            signature.MimeType = model.SignatureImage.ContentType;
                            signature.ID = Guid.NewGuid();
                            receipt.SignatureImage = signature;
                        }
                        catch
                        {
                        }
                        scope.Add(receipt);
                        scope.Transaction.Commit();
                        ViewData["ReceiptID"] = receipt.ReceiptNumber;
                        return View("Printoptions");
                    }
                }
                LoadReceiptValuesFromDb(scope);
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
                        receipt.RecurringDetails.Clear();
                        for (int i = 0; i < model.RecurrenceDates.Count(); i++)
                        {
                            try
                            {
                                var recurringDetails = new RecurringDetails
                                                           {
                                                               DueDate = Convert.ToDateTime(model.RecurrenceDates[i]),
                                                               Amount = model.RecurrenceAmount[i]
                                                           };
                                switch (model.RecurrenceModeofPayment[i])
                                {
                                    case "Cash":
                                        {
                                            recurringDetails.ModeOfPayment = ModeOfPayment.Cash;
                                            break;
                                        }
                                    case "Cheque":
                                        {
                                            recurringDetails.ModeOfPayment = ModeOfPayment.Cheque;
                                            break;
                                        }
                                    case "Mobile":
                                        {
                                            recurringDetails.ModeOfPayment = ModeOfPayment.Mobile;
                                            break;
                                        }
                                    case "Goods":
                                        {
                                            recurringDetails.ModeOfPayment = ModeOfPayment.Goods;
                                            break;
                                        }
                                    case "Online":
                                        {
                                            recurringDetails.ModeOfPayment = ModeOfPayment.Online;
                                            break;
                                        }
                                }
                                receipt.RecurringDetails.Add(recurringDetails);
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                        if (model.SignatureImage != null)
                        {
                            // saving image here
                            try
                            {
                                var signature = new SignatureImage { Filename = model.SignatureImage.FileName };
                                Stream fileStream = model.SignatureImage.InputStream;
                                int fileLength = model.SignatureImage.ContentLength;
                                signature.Filedata = new byte[fileLength];
                                fileStream.Read(signature.Filedata, 0, fileLength);
                                signature.MimeType = model.SignatureImage.ContentType;
                                signature.ID = Guid.NewGuid();
                                receipt.SignatureImage = signature;
                            }
                            catch
                            { }
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
                                          ReceiptNumber = Utilities.GenerateReceiptId(),
                                          DateReceived = DateTime.Now,
                                          IssuedDate = DateTime.Now
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
                if (model.SignatureImage != null)
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
                        // saving image here
                        try
                        {
                            var signature = new SignatureImage { Filename = model.SignatureImage.FileName };
                            Stream fileStream = model.SignatureImage.InputStream;
                            int fileLength = model.SignatureImage.ContentLength;
                            signature.Filedata = new byte[fileLength];
                            fileStream.Read(signature.Filedata, 0, fileLength);
                            signature.MimeType = model.SignatureImage.ContentType;
                            signature.ID = Guid.NewGuid();
                            receipt.SignatureImage = signature;
                        }
                        catch
                        {
                        }
                        scope.Add(receipt);
                        scope.Transaction.Commit();
                        ViewData["ReceiptID"] = receipt.ReceiptNumber;
                        return View("Printoptions");
                    }
                }
                LoadReceiptValuesFromDb(scope);
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

                        if (model.SignatureImage != null)
                        {
                            // saving image here
                            try
                            {
                                var signature = new SignatureImage { Filename = model.SignatureImage.FileName };
                                Stream fileStream = model.SignatureImage.InputStream;
                                int fileLength = model.SignatureImage.ContentLength;
                                signature.Filedata = new byte[fileLength];
                                fileStream.Read(signature.Filedata, 0, fileLength);
                                signature.MimeType = model.SignatureImage.ContentType;
                                signature.ID = Guid.NewGuid();
                                receipt.SignatureImage = signature;
                            }
                            catch
                            { }
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
                                           ReceiptNumber = Utilities.GenerateReceiptId(),
                                           DateReceived = DateTime.Now,
                                           IssuedDate = DateTime.Now
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
                if (model.SignatureImage != null)
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
                        // saving image here
                        try
                        {
                            var signature = new SignatureImage { Filename = model.SignatureImage.FileName };
                            Stream fileStream = model.SignatureImage.InputStream;
                            int fileLength = model.SignatureImage.ContentLength;
                            signature.Filedata = new byte[fileLength];
                            fileStream.Read(signature.Filedata, 0, fileLength);
                            signature.MimeType = model.SignatureImage.ContentType;
                            signature.ID = Guid.NewGuid();
                            receipt.SignatureImage = signature;
                        }
                        catch
                        {
                        }
                        scope.Add(receipt);
                        scope.Transaction.Commit();
                        ViewData["ReceiptID"] = receipt.ReceiptNumber;
                        return View("Printoptions");
                    }
                }
                LoadReceiptValuesFromDb(scope);
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

                        if (model.SignatureImage != null)
                        {
                            // saving image here
                            try
                            {
                                var signature = new SignatureImage { Filename = model.SignatureImage.FileName };
                                Stream fileStream = model.SignatureImage.InputStream;
                                int fileLength = model.SignatureImage.ContentLength;
                                signature.Filedata = new byte[fileLength];
                                fileStream.Read(signature.Filedata, 0, fileLength);
                                signature.MimeType = model.SignatureImage.ContentType;
                                signature.ID = Guid.NewGuid();
                                receipt.SignatureImage = signature;
                            }
                            catch
                            { }
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
                                                    Email = receipt.Email,
                                                    ReceiptNumber = receipt.ReceiptNumber,
                                                    City = receipt.City,
                                                    FirstName = receipt.FirstName,
                                                    LastName = receipt.LastName,
                                                    Mi = receipt.Mi,
                                                    State = receipt.State,
                                                    ZipCode = receipt.ZipCode,
                                                    IssuedDate = receipt.IssuedDate,
                                                    RecurrenceAmount = receipt.RecurringDetails.Select(m => m.Amount).ToList().ToArray(),
                                                    RecurrenceDates = receipt.RecurringDetails.Select(m => m.DueDate).ToList().ToArray(),
                                                    RecurrenceModeofPayment = receipt.RecurringDetails.Select(m => m.ModeOfPayment.ToString()).ToList().ToArray()
                                                };
                                List<RecurrenceData> recurrenceData = receipt.RecurringDetails.Select(rd => new RecurrenceData { Amount = rd.Amount, Date = rd.DueDate.ToString("MM/dd/yyy"), ModeOfPayment = rd.ModeOfPayment.ToString() }).ToList();
                                ViewData["RecurringDetails"] = recurrenceData;
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
                                foreach (var recurringDetail in receipt.RecurringDetails)
                                {
                                    scope.Transaction.Begin();
                                    scope.Remove(recurringDetail);
                                    scope.Transaction.Commit();
                                }
                                scope.Transaction.Begin();
                                if (receipt.SignatureImage != null)
                                    scope.Remove(receipt.SignatureImage);
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
            //if (!User.Identity.IsAuthenticated)
            //    return RedirectToAction("LogOn", "Account");
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
                                                       RecurringDetails = receipts[0].RecurringDetails.Select(recurrenceDetail => new RecurrenceData {Amount = recurrenceDetail.Amount,Date = recurrenceDetail.DueDate.ToString("MMM dd yyyy"),ModeOfPayment = recurrenceDetail.ModeOfPayment.ToString()} ).ToList() ,
                                                       ServiceType = receipts[0].ServiceType,
                                                       State = receipts[0].State,
                                                       ZipCode = receipts[0].ZipCode,
                                                       SignatureId = (receipts[0].SignatureImage == null) ? "" : receipts[0].SignatureImage.ID.ToString()
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
                                                                          RecurringDetails = receipt.RecurringDetails.Select(recurrenceDetail => new RecurrenceData { Amount = recurrenceDetail.Amount, Date = recurrenceDetail.DueDate.ToString("MMM dd yyyy"), ModeOfPayment = recurrenceDetail.ModeOfPayment.ToString() }).ToList(),
                                                                          ServiceType = receipt.ServiceType,
                                                                          State = receipt.State,
                                                                          ZipCode = receipt.ZipCode,
                                                                          SignatureId = (receipts[0].SignatureImage == null) ? "" : receipts[0].SignatureImage.ID.ToString()
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

            ViewData["status"] = "progress";
            ViewData["RecID"] = recpId;

            var t = new Thread(new ThreadStart(GeneratePdf));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
            while (ViewData["status"].ToString() == "progress")
            {
                Thread.Sleep(1000);
            }

            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Receipt-{0}.pdf", recpId));
            Response.Buffer = true;
            Response.Clear();
            var output = (MemoryStream)ViewData["memoryStream"];
            Response.OutputStream.Write(output.GetBuffer(), 0, output.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.End();

            return (FileStreamResult)ViewData["content"];
        }

        private void GeneratePdf()
        {
            var wb = new WebBrowser { ScrollBarsEnabled = false, ScriptErrorsSuppressed = true };
            wb.Navigate("http://www.shirdisaibabaaz.org/PrintReceipt/" + ViewData["RecID"]);
            while (wb.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
            }
            Thread.Sleep(1000);
            if (wb.Document != null)
            {
                if (wb.Document.Body != null)
                {
                    int width = wb.Document.Body.ScrollRectangle.Width;
                    int height = wb.Document.Body.ScrollRectangle.Height;
                    wb.Width = width;
                    wb.Height = height;
                    var bmp = new Bitmap(width, height);
                    wb.DrawToBitmap(bmp, new Rectangle(0, 0, width, height));
                    string filename = Server.MapPath("~/temp/" + DateTime.Now.ToString("ddmmmyyyyHHss") + ".bmp");
                    bmp.Save(filename);

                    // Create a Document object
                    var document = new Document(PageSize.A4, 50, 50, 25, 25);
                    var output = new MemoryStream();
                    PdfWriter writer = PdfWriter.GetInstance(document, output);
                    document.Open();
                    PdfContentByte cb = writer.DirectContent;
                    //add header image
                    var headerlogo = Image.GetInstance(filename);
                    document.Add(headerlogo);
                    document.Close();

                    ViewData["memoryStream"] = output;
                    var fileStream = new FileStreamResult(Response.OutputStream, "application/pdf");
                    ViewData["content"] = fileStream;
                }
            }
            ViewData["status"] = "done";
        }

        /*
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
                                                       RecurringDetails = receipts[0].RecurringDetails.Select(recurrenceDetail => new RecurrenceData {Amount = recurrenceDetail.Amount,Date = recurrenceDetail.DueDate.ToString("MMM dd yyyy"),ModeOfPayment = recurrenceDetail.ModeOfPayment.ToString()} ).ToList() ,
                                                       ServiceType = receipts[0].ServiceType,
                                                       State = receipts[0].State,
                                                       ZipCode = receipts[0].ZipCode,
                                                       SignatureId = (receipts[0].SignatureImage == null) ? "" : receipts[0].SignatureImage.ID.ToString()
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
                                                                              Address2 = receipt.Address2,
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
                                                                              RecurringDetails = receipt.RecurringDetails.Select(recurrenceDetail => new RecurrenceData { Amount = recurrenceDetail.Amount, Date = recurrenceDetail.DueDate.ToString("MMM dd yyyy"), ModeOfPayment = recurrenceDetail.ModeOfPayment.ToString() }).ToList(),
                                                                              ServiceType = receipt.ServiceType,
                                                                              State = receipt.State,
                                                                              ZipCode = receipt.ZipCode,
                                                                              SignatureId = (receipts[0].SignatureImage == null) ? "" : receipts[0].SignatureImage.ID.ToString()
                                                                          }).ToList();
                    }
                }

                // Create a Document object
                var document = new Document(PageSize.A4, 50, 50, 25, 25);
                var output = new MemoryStream();
                PdfWriter writer = PdfWriter.GetInstance(document, output);
                document.Open();
                PdfContentByte cb = writer.DirectContent;
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
                    TwoField(document, "Email", receiptData.Email, "Phone:", receiptData.Contact);
                    NewLine(document);
                    switch (receiptData.ReceiptType)
                    {
                        case "GeneralReceipt":
                            {
                                SingleField(document, "Donation received date:", receiptData.DateReceived.ToString("dd  MMM  yyyy"));
                                NewLine(document);
                                SingleField(document, "Donation amount received in $:", receiptData.DonationAmount);
                                NewLine(document);
                                SingleField(document, "Donation received in words:", receiptData.DonationAmountinWords);
                                NewLine(document);
                                SingleField(document, "Mode of donation:", receiptData.ModeOfPayment);
                                NewLine(document);

                                try
                                {
                                    var logo = iTextSharp.text.Image.GetInstance(SignatureController.SignatureImage(receiptData.SignatureId));
                                    logo.ScaleAbsoluteWidth(140f);
                                    logo.ScaleAbsoluteHeight(40f);
                                    logo.SetAbsolutePosition(395, 400);
                                    document.Add(logo);
                                }
                                catch (Exception)
                                {
                                }

                                cb.SetLineWidth(0.1f);
                                cb.Rectangle(40f, 370f, 500f, 90f);
                                cb.Stroke();

                                break;
                            }
                        case "RecurringReceipt":
                            {
                                SingleField(document, "Donation received date:", receiptData.DateReceived.ToString("dd  MMM  yyyy"));
                                NewLine(document);
                                NewLine(document);
                                TableHeaders(document, "Recurring ID", "Date received", "Mode of donation",
                                             "Amount in $");
                                NewLine(document);
                                int recurringId = 1;
                                foreach (var recurringDetail in receiptData.RecurringDetails)
                                {
                                    TableValues(document, recurringId++.ToString(), recurringDetail.Date, recurringDetail.ModeOfPayment, recurringDetail.Amount);
                                    NewLine(document);
                                }
                                break;
                            }
                        case "MerchandiseReceipt":
                            {
                                SingleField(document, "Donation received date:", receiptData.DateReceived.ToString("dd  MMM  yyyy"));
                                NewLine(document);
                                SingleField(document, "Goods received:", receiptData.MerchandiseItem);
                                NewLine(document);
                                SingleField(document, "Goods FMV in $:", receiptData.FmvValue);
                                NewLine(document);

                                try
                                {
                                    var logo = iTextSharp.text.Image.GetInstance(SignatureController.SignatureImage(receiptData.SignatureId));
                                    logo.ScaleAbsoluteWidth(140f);
                                    logo.ScaleAbsoluteHeight(40f);
                                    logo.SetAbsolutePosition(395, 400);
                                    document.Add(logo);
                                }
                                catch (Exception)
                                {
                                }

                                cb.SetLineWidth(0.1f);
                                cb.Rectangle(40f, 380f, 500f, 90f);
                                cb.Stroke();

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
                                SingleField(document, "FMV in $:", receiptData.FmvValue);

                                try
                                {
                                    var logo = iTextSharp.text.Image.GetInstance(SignatureController.SignatureImage(receiptData.SignatureId));
                                    logo.ScaleAbsoluteWidth(140f);
                                    logo.ScaleAbsoluteHeight(40f);
                                    logo.SetAbsolutePosition(395, 400);
                                    document.Add(logo);
                                }
                                catch (Exception)
                                {
                                }

                                cb.SetLineWidth(0.1f);
                                cb.Rectangle(40f, 375f, 500f, 90f);
                                cb.Stroke();

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

         */

        private const string FontName = "Sanserif";

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
            Font arial = FontFactory.GetFont(FontName, 9f, baseColor);
            document.Add(new Anchor(field1 + " ", arial));
            baseColor = new BaseColor(Color.Black);
            arial = FontFactory.GetFont(FontName, 9f, baseColor);
            document.Add(new Anchor(value1, arial));
        }

        private static void Notes(Document document, string field1, string value1)
        {
            var baseColor = new BaseColor(Color.Gray);
            Font arial = FontFactory.GetFont(FontName, 8f, baseColor);
            document.Add(new Anchor(field1 + " ", arial));
            baseColor = new BaseColor(Color.Black);
            arial = FontFactory.GetFont(FontName, 8f, baseColor);
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

        private static void TableHeaders(Document document, string field1, string field2, string field3, string field4)
        {
            var baseColor = new BaseColor(Color.Black);
            Font arial = FontFactory.GetFont(FontName, 9f, baseColor);

            document.Add(new Anchor(field1, arial));
            for (int i = 0; i < 25; i++)
                document.Add(new Anchor(" ", new Font()));
            document.Add(new Anchor(field2, arial));
            for (int i = 0; i < 25; i++)
                document.Add(new Anchor(" ", new Font()));
            document.Add(new Anchor(field3, arial));
            for (int i = 0; i < 25; i++)
                document.Add(new Anchor(" ", new Font()));
            document.Add(new Anchor(field4, arial));
        }

        private static void TableValues(Document document, string value1, string value2, string value3, string value4)
        {
            var baseColor = new BaseColor(Color.Gray);
            Font arial = FontFactory.GetFont(FontName, 9f, baseColor);

            document.Add(new Anchor(value1, arial));

            if (string.IsNullOrEmpty(value1))
                value1 = string.Empty;

            int count = 40 - value1.Length;
            if (count < 0)
                count = count * -1;
            for (int i = 0; i < count; i++)
                document.Add(new Anchor(" ", new Font()));

            document.Add(new Anchor(value2, arial));

            count = 40 - value2.Length;
            if (count < 0)
                count = count * -1;
            for (int i = 0; i < count; i++)
                document.Add(new Anchor(" ", new Font()));

            document.Add(new Anchor(value3, arial));

            count = 40 - value3.Length;
            if (count < 0)
                count = count * -1;
            for (int i = 0; i < count; i++)
                document.Add(new Anchor(" ", new Font()));

            document.Add(new Anchor(value4, arial));
        }

        private static void NewLine(Document document)
        {
            document.Add(new Phrase("\n"));
        }

        private static void Header(Document document, string title)
        {
            var baseColor = new BaseColor(Color.Black);
            Font arial = FontFactory.GetFont(FontName, 11f, baseColor);
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
            Font arial = FontFactory.GetFont(FontName, 10f, baseColor);
            document.Add(new Paragraph(theme, arial) { IndentationLeft = 200 });
        }

        private static void Theme(Document document, string theme)
        {
            var baseColor = new BaseColor(Color.Red);
            Font arial = FontFactory.GetFont(FontName, 10f, baseColor);
            document.Add(new Paragraph(theme, arial) { IndentationLeft = 180 });
        }

        //private static MemoryStream Barcode(string recpId)
        //{
        //    try
        //    {
        //        const int thickness = 30;
        //        const string code = "Code 128";
        //        const int scale = 1;

        //        var font = new BCGFont(new System.Drawing.Font(FontName, 1));
        //        var colorBlack = new BCGColor(Color.Black);
        //        var colorWhite = new BCGColor(Color.White);
        //        Type codeType = (from kvp in Utilities.CodeType where kvp.Value == code select kvp.Key).FirstOrDefault();
        //        var temporaryBarcode = (BarCode.Barcode)Activator.CreateInstance(codeType);
        //        var codebar = (BCGBarcode1D)Activator.CreateInstance(temporaryBarcode.Code);
        //        codebar.setThickness(thickness);
        //        codebar.setScale(scale);
        //        MethodInfo method = temporaryBarcode.Code.GetMethod("setStart");
        //        if (method != null)
        //        {
        //            method.Invoke(codebar, new object[] { "A" });
        //        }
        //        codebar.setBackgroundColor(colorWhite);
        //        codebar.setForegroundColor(colorBlack);
        //        codebar.setFont(font);
        //        codebar.parse(Utilities.Encrypt(recpId));
        //        var drawing = new BCGDrawing(colorWhite);
        //        drawing.setBarcode(codebar);
        //        drawing.draw();
        //        var stream = new MemoryStream();
        //        drawing.finish(ImageFormat.Jpeg, stream);
        //        return stream;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
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

        public List<RecurrenceData> RecurringDetails { get; set; }

        public string ModeOfPayment { get; set; }

        public string DonationReceiverName { get; set; }

        public DateTime DateReceived { get; set; }

        public string ReceiptType { get; set; }

        public string GroupId { get; set; }

        public DateTime IssuedDate { get; set; }

        public string SignatureId { get; set; }
    }

    public struct RecurrenceData
    {
        public string Amount { get; set; }

        public string Date { get; set; }

        public string ModeOfPayment { get; set; }
    }
}