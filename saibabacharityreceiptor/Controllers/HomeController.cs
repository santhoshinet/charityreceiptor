﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using saibabacharityreceiptorDL;

namespace saibabacharityreceiptor.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var modeofpayments = new List<string> { "Cash", "Cheque", "Online", "Mobile", "Goods" };
            ViewData["modeOfPayment"] = modeofpayments;
            var scope = ObjectScopeProvider1.ObjectScope();
            var receivers = (from c in scope.GetOqlQuery<DonationReceivers>().ExecuteEnumerable()
                             select c).ToList();
            var donationReceivers = receivers.Select(receiver => receiver.Name).ToList();
            ViewData["donationReceivers"] = donationReceivers;
            return View();
        }
    }
}