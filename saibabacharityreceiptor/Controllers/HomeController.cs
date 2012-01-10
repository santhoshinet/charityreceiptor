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
            var scope = ObjectScopeProvider1.ObjectScope();
            if (Checkauthorization(scope, User.Identity.Name))
            {
                LoadReceiptValuesFromDb(scope);
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
            var scope = ObjectScopeProvider1.ObjectScope();
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
                                          OnDateTime = receivedTime,
                                          Name = model.Name,
                                          ReceiptType = ReceiptType.GeneralReceipt
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
                                receipt.ModeOfPayment = ModeOfPayment.Cheque;
                                break;
                            }
                        case "Goods":
                            {
                                receipt.ModeOfPayment = ModeOfPayment.Goods;
                                break;
                            }
                    }
                    scope.Add(receipt);
                    scope.Transaction.Commit();
                    ViewData["ReceiptID"] = receipt.ReceiptNumber;
                    return View("Printoptions");
                }
                ModelState.AddModelError("", "Unable to generate receipt due to invalid parameter passed.");
                return View();
            }
            ViewData["Status"] = "You are not authorized to do this operation";
            return View("PartialViewStatus");
        }

        [Authorize]
        [HttpGet]
        public ActionResult MerchandiseReceipt()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            var scope = ObjectScopeProvider1.ObjectScope();
            if (Checkauthorization(scope, User.Identity.Name))
            {
                LoadReceiptValuesFromDb(scope);
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
            var scope = ObjectScopeProvider1.ObjectScope();
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
                                          Value = model.Value,
                                          DonationReceiver = donationReceiver[0],
                                          Email = model.Email,
                                          OnDateTime = receivedTime,
                                          Name = model.Name,
                                          ReceiptType = ReceiptType.MerchandiseReceipt
                                      };
                    scope.Add(receipt);
                    scope.Transaction.Commit();
                    ViewData["ReceiptID"] = receipt.ReceiptNumber;
                    return View("Printoptions");
                }
                ModelState.AddModelError("", "Unable to generate receipt due to invalid parameter passed.");
                return View();
            }
            ViewData["Status"] = "You are not authorized to do this operation";
            return View("PartialViewStatus");
        }

        [Authorize]
        [HttpGet]
        public ActionResult ServicesReceipt()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            var scope = ObjectScopeProvider1.ObjectScope();
            if (Checkauthorization(scope, User.Identity.Name))
            {
                LoadReceiptValuesFromDb(scope);
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
            var scope = ObjectScopeProvider1.ObjectScope();
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
                                          HoursServed = Convert.ToInt32(model.HoursServed),
                                          DonationReceiver = donationReceiver[0],
                                          Email = model.Email,
                                          OnDateTime = receivedTime,
                                          Name = model.Name,
                                          ReceiptType = ReceiptType.ServicesReceipt
                                      };
                    scope.Add(receipt);
                    scope.Transaction.Commit();
                    ViewData["ReceiptID"] = receipt.ReceiptNumber;
                    return View("Printoptions");
                }
                ModelState.AddModelError("", "Unable to generate receipt due to invalid parameter passed.");
                return View();
            }
            ViewData["Status"] = "You are not authorized to do this operation";
            return View("PartialViewStatus");
        }

        [Authorize]
        [HttpGet]
        public ActionResult RecurringReceipt()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            var scope = ObjectScopeProvider1.ObjectScope();
            if (Checkauthorization(scope, User.Identity.Name))
            {
                LoadReceiptValuesFromDb(scope);
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
            var scope = ObjectScopeProvider1.ObjectScope();
            if (Checkauthorization(scope, User.Identity.Name))
            {
                var donationReceiver = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                        where
                                            c.IsheDonationReceiver.Equals(true) &&
                                            c.Username.Equals(Request.Form["CmbDonationReceivedBy"])
                                        select c).ToList();
                if (donationReceiver.Count > 0)
                {
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
                                          OnDateTime = receivedTime,
                                          Name = model.Name,
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
                                receipt.ModeOfPayment = ModeOfPayment.Cheque;
                                break;
                            }
                        case "Goods":
                            {
                                receipt.ModeOfPayment = ModeOfPayment.Goods;
                                break;
                            }
                    }
                    receipt.ReceiptType = ReceiptType.GeneralReceipt;
                    foreach (string date in model.RecurrenceDates.Split(','))
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
                ModelState.AddModelError("", "Unable to generate receipt due to invalid parameter passed.");
                return View();
            }
            ViewData["Status"] = "You are not authorized to do this operation";
            return View("PartialViewStatus");
        }

        [HttpGet]
        public ActionResult PrintReceipt(string recpId)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            var scope = ObjectScopeProvider1.ObjectScope();
            if (Checkauthorization(scope, User.Identity.Name))
            {
                List<Receipt> receipts = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                          where c.ReceiptNumber.ToLower().Equals(recpId.ToLower())
                                          select c).ToList();
                if (receipts.Count > 0)
                {
                    var receiptData = new ReceiptData { Name = receipts[0].Name };
                    ViewData["Receipt_Data"] = receiptData;
                    return View();
                }
                ViewData["Status"] = "The receipt not found for the given id.";
                return View("Status");
            }
            ViewData["Status"] = "You are not authorized to do this operation";
            return View("PartialViewStatus");
        }

        [HttpGet]
        public string DownloadReceipt(string recpId)
        {
            return "this feature is under construction.";
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
    }

    public struct ReceiptData
    {
        public string Name;
    }
}